using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    public class PatientProfileForm : Form
    {
        private readonly string _email;
        private TextBox _txtFullName, _txtEmail, _txtPhone, _txtAddress, _txtCity;

        public PatientProfileForm(string email)
        {
            _email = email;
            BuildUI();
            LoadProfile();
        }

        private void BuildUI()
        {
            Text            = "My Profile";
            ClientSize      = new Size(500, 510);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition   = FormStartPosition.CenterParent;
            BackColor       = Color.White;

            var header = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 65,
                BackColor = Theme.Dark
            };
            header.MouseDown += (s, e) => WindowHelper.StartDrag(Handle);
            header.Controls.Add(new Label
            {
                Text      = "👤  My Profile",
                Font      = new Font("Segoe UI Semibold", 14F),
                ForeColor = Color.White,
                Location  = new Point(20, 18),
                AutoSize  = true
            });
            var btnX = new Button
            {
                Text      = "×",
                Font      = new Font("Arial", 13F, FontStyle.Bold),
                ForeColor = Color.Silver,
                Location  = new Point(460, 15),
                Size      = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand
            };
            btnX.FlatAppearance.BorderSize = 0;
            btnX.Click += (s, e) => Close();
            header.Controls.Add(btnX);
            Controls.Add(header);

            var content = new Panel
            {
                Location  = new Point(0, 65),
                Size      = new Size(500, 445),
                BackColor = Color.White
            };

            int y = 25;
            _txtFullName = AddField(content, "Full Name",     100, ref y);
            _txtEmail    = AddField(content, "Email Address", 150, ref y);
            _txtEmail.ReadOnly  = true;
            _txtEmail.BackColor = Theme.InputBg;
            _txtPhone   = AddField(content, "Phone Number",  20,  ref y);
            _txtAddress = AddField(content, "Address",       200, ref y);
            _txtCity    = AddField(content, "City",           50, ref y);

            var btnSave = new Button
            {
                Text     = "Save Changes",
                Location = new Point(150, y + 15),
                Size     = new Size(200, 44)
            };
            Theme.PrimaryDark(btnSave);
            btnSave.Click += BtnSave_Click;
            content.Controls.Add(btnSave);

            Controls.Add(content);
        }

        private TextBox AddField(Panel parent, string label, int maxLength, ref int y)
        {
            parent.Controls.Add(new Label
            {
                Text      = label,
                Font      = new Font("Segoe UI Semibold", 9F),
                ForeColor = Theme.TextSec,
                Location  = new Point(40, y),
                AutoSize  = true
            });
            y += 22;

            var txt = new TextBox
            {
                Location  = new Point(40, y),
                Size      = new Size(420, 30),
                MaxLength = maxLength
            };
            Theme.Input(txt);
            parent.Controls.Add(txt);
            y += 60;
            return txt;
        }

        private void LoadProfile()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(
                        "SELECT FirstName, LastName, Email, PhoneNumber, Address, City FROM Patients WHERE Email=@E", conn))
                    {
                        cmd.Parameters.AddWithValue("@E", _email);
                        using (var r = cmd.ExecuteReader())
                        {
                            if (!r.Read()) return;
                            string fn = r["FirstName"]?.ToString() ?? "";
                            string ln = r["LastName"]?.ToString()  ?? "";
                            _txtFullName.Text = (fn + " " + ln).Trim();
                            _txtEmail.Text    = r["Email"]?.ToString()       ?? "";
                            _txtPhone.Text    = Clean(r["PhoneNumber"]?.ToString());
                            _txtAddress.Text  = Clean(r["Address"]?.ToString());
                            _txtCity.Text     = Clean(r["City"]?.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load profile. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Clean(string val) =>
            val == "N/A" || val == null ? "" : val;

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string fullName = _txtFullName.Text.Trim();
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Full name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int sp       = fullName.IndexOf(' ');
            string first = sp > 0 ? fullName.Substring(0, sp) : fullName;
            string last  = sp > 0 ? fullName.Substring(sp + 1).Trim() : "";

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(
                        "UPDATE Patients SET FirstName=@FN, LastName=@LN, PhoneNumber=@Ph, Address=@Ad, City=@Ci WHERE Email=@E",
                        conn))
                    {
                        cmd.Parameters.AddWithValue("@FN", first);
                        cmd.Parameters.AddWithValue("@LN", last);
                        cmd.Parameters.AddWithValue("@Ph", _txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ad", _txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ci", _txtCity.Text.Trim());
                        cmd.Parameters.AddWithValue("@E",  _email);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Profile saved!", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to save profile. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
