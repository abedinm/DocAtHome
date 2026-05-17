using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    public partial class PaymentForm : Form
    {
        private string  _patientEmail;
        private string  _relatedType;
        private int     _relatedId;
        private decimal _amount;

        // Dynamically added card field
        private Label   _lblCardNumber;
        private TextBox _txtCardNumber;

        public PaymentForm(string patientEmail, string relatedType, int relatedId, decimal amount)
        {
            InitializeComponent();
            _patientEmail = patientEmail;
            _relatedType  = relatedType;
            _relatedId    = relatedId;
            _amount       = amount;

            lblAmountValue.Text = $"BDT {_amount:0.00}";
            lblTypeValue.Text   = _relatedType == PaymentType.Appointment ? "Doctor Appointment" : "Nurse Service";

            Theme.Primary(btnConfirm);
            Theme.Outlined(btnCancel, Theme.TextSec);

            // Card number field — shown only when Card is selected
            _lblCardNumber = new Label
            {
                Text      = "CARD NUMBER",
                Font      = new Font("Segoe UI Semibold", 8F),
                ForeColor = Theme.TextSec,
                Location  = new Point(30, 200),
                AutoSize  = true,
                Visible   = false
            };
            _txtCardNumber = new TextBox
            {
                Font        = new Font("Segoe UI", 10F),
                Location    = new Point(30, 220),
                Size        = new Size(360, 28),
                BackColor   = Theme.InputBg,
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength   = 19,
                Visible     = false
            };
            _txtCardNumber.KeyPress += CardNumber_KeyPress;
            _txtCardNumber.TextChanged += CardNumber_Format;

            this.Controls.Add(_lblCardNumber);
            this.Controls.Add(_txtCardNumber);
            _lblCardNumber.BringToFront();
            _txtCardNumber.BringToFront();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            cmbMethod.Items.Clear();
            cmbMethod.Items.Add("Card");
            cmbMethod.Items.Add("Cash");
            cmbMethod.Items.Add("Bkash");
            cmbMethod.SelectedIndex = 0;
            cmbMethod.SelectedIndexChanged += CmbMethod_SelectedIndexChanged;
            CmbMethod_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void CmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCard = cmbMethod.SelectedItem?.ToString() == "Card";
            _lblCardNumber.Visible = isCard;
            _txtCardNumber.Visible = isCard;

            // Expand/contract form and reposition buttons
            int baseY = isCard ? 270 : 230;
            this.ClientSize     = new System.Drawing.Size(420, isCard ? 330 : 290);
            btnConfirm.Location = new System.Drawing.Point(210, baseY);
            btnCancel.Location  = new System.Drawing.Point(50,  baseY);
        }

        private void CardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        private void CardNumber_Format(object sender, EventArgs e)
        {
            string digits = System.Text.RegularExpressions.Regex.Replace(_txtCardNumber.Text, @"\D", "");
            if (digits.Length > 16) digits = digits.Substring(0, 16);
            var formatted = new System.Text.StringBuilder();
            for (int i = 0; i < digits.Length; i++)
            {
                if (i > 0 && i % 4 == 0) formatted.Append("  ");
                formatted.Append(digits[i]);
            }
            int caret = _txtCardNumber.SelectionStart + (formatted.Length - _txtCardNumber.Text.Length);
            _txtCardNumber.TextChanged -= CardNumber_Format;
            _txtCardNumber.Text = formatted.ToString();
            _txtCardNumber.SelectionStart = Math.Max(0, Math.Min(caret, _txtCardNumber.Text.Length));
            _txtCardNumber.TextChanged += CardNumber_Format;
        }

        private void Header_MouseDown(object sender, MouseEventArgs e) =>
            WindowHelper.StartDrag(this.Handle);

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbMethod.Text))
            {
                MessageBox.Show("Please select a payment method.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMethod.SelectedItem?.ToString() == "Card")
            {
                string digits = System.Text.RegularExpressions.Regex.Replace(_txtCardNumber.Text, @"\D", "");
                if (digits.Length != 16 || !ValidationHelper.IsValidLuhn(digits))
                {
                    MessageBox.Show("Please enter a valid 16-digit card number.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    int patientId;
                    using (var cmdPid = new SqlCommand("SELECT PatientID FROM Patients WHERE Email=@E", conn))
                    {
                        cmdPid.Parameters.AddWithValue("@E", _patientEmail);
                        object result = cmdPid.ExecuteScalar();
                        if (result == null) { MessageBox.Show("Patient account not found.", "Error"); return; }
                        patientId = (int)result;
                    }

                    using (var cmd = new SqlCommand(@"
                        INSERT INTO Payments
                        (PatientID, RelatedType, RelatedID, Amount, Method, Status, PaidAt, CreatedAt)
                        VALUES (@PID, @Type, @RID, @Amt, @Method, 'Paid', GETDATE(), GETDATE())", conn))
                    {
                        cmd.Parameters.AddWithValue("@PID",    patientId);
                        cmd.Parameters.AddWithValue("@Type",   _relatedType);
                        cmd.Parameters.AddWithValue("@RID",    _relatedId);
                        cmd.Parameters.AddWithValue("@Amt",    _amount);
                        cmd.Parameters.AddWithValue("@Method", cmbMethod.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Payment recorded successfully. Thank you!",
                    "Payment Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Payment could not be processed. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Cursor = Cursors.Default; }
        }
    }
}
