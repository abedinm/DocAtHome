using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace DocAtHome
{
    internal static class UIHelper
    {
        // ── Shared palette ────────────────────────────────────────────────
        private static readonly Color HeaderBg    = Color.FromArgb(15, 23, 42);
        private static readonly Color HeaderFg    = Color.White;
        private static readonly Color AltRow      = Color.FromArgb(248, 250, 252);
        private static readonly Color SelectionBg = Color.FromArgb(59, 130, 246);
        private static readonly Color GridLine    = Color.FromArgb(226, 228, 233);

        // ── Grid styling ──────────────────────────────────────────────────
        public static void StyleGrid(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;

            var hStyle = dgv.ColumnHeadersDefaultCellStyle;
            hStyle.BackColor = HeaderBg;
            hStyle.ForeColor = HeaderFg;
            hStyle.Font      = new Font("Segoe UI Semibold", 10F);
            hStyle.Padding   = new Padding(8, 0, 0, 0);

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight      = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            var cStyle = dgv.DefaultCellStyle;
            cStyle.Font               = new Font("Segoe UI", 9.5F);
            cStyle.SelectionBackColor = SelectionBg;
            cStyle.SelectionForeColor = Color.White;
            cStyle.Padding            = new Padding(6, 0, 0, 0);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = AltRow;
            dgv.RowTemplate.Height = 36;

            dgv.BorderStyle     = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor       = GridLine;

            dgv.SelectionMode        = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible    = false;
            dgv.AllowUserToAddRows   = false;
            dgv.ReadOnly             = true;
            dgv.AutoSizeColumnsMode  = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor      = Color.White;

            // Prevent flicker
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, dgv, new object[] { true });
        }

        // ── Status / Payment badge colouring ──────────────────────────────
        // Call from CellFormatting. Pass the column names you want coloured.
        public static void FormatStatusCell(DataGridViewCellFormattingEventArgs e,
                                             DataGridView dgv,
                                             params string[] columnNames)
        {
            if (e.Value == null) return;

            string colName = dgv.Columns[e.ColumnIndex].Name;
            foreach (string name in columnNames)
            {
                if (colName != name) continue;

                Color bg, fg;
                switch (e.Value.ToString())
                {
                    case "Pending":
                        bg = Color.FromArgb(251, 191, 36);
                        fg = Color.FromArgb(120, 53, 15);
                        break;
                    case "Accepted":
                    case "Completed":
                    case "Paid":
                        bg = Color.FromArgb(16, 185, 129);
                        fg = Color.White;
                        break;
                    case "Rejected":
                    case "Cancelled":
                    case "Unpaid":
                        bg = Color.FromArgb(239, 68, 68);
                        fg = Color.White;
                        break;
                    default:
                        return;
                }

                e.CellStyle.BackColor            = bg;
                e.CellStyle.ForeColor            = fg;
                e.CellStyle.SelectionBackColor   = bg;
                e.CellStyle.SelectionForeColor   = fg;
                return;
            }
        }

        // ── Time formatting ───────────────────────────────────────────────
        // Call from CellFormatting to convert raw TimeSpan → "9:30 AM"
        public static void FormatTimeCell(DataGridViewCellFormattingEventArgs e,
                                           DataGridView dgv,
                                           string columnName = "Time")
        {
            if (e.Value == null || dgv.Columns[e.ColumnIndex].Name != columnName) return;

            TimeSpan ts;
            if (e.Value is TimeSpan span)
                ts = span;
            else if (!TimeSpan.TryParse(e.Value.ToString(), out ts))
                return;

            e.Value = DateTime.Today.Add(ts).ToString("h:mm tt");
            e.FormattingApplied = true;
        }
    }
}
