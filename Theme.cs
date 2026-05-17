using System.Drawing;
using System.Windows.Forms;

namespace DocAtHome
{
    internal static class Theme
    {
        // ── Palette ───────────────────────────────────────────────────────
        public static readonly Color Dark       = Color.FromArgb(15, 23, 42);
        public static readonly Color DarkHov    = Color.FromArgb(30, 41, 59);
        public static readonly Color Accent     = Color.FromArgb(59, 130, 246);
        public static readonly Color AccentHov  = Color.FromArgb(37, 99, 235);
        public static readonly Color Success    = Color.FromArgb(16, 185, 129);
        public static readonly Color SuccessHov = Color.FromArgb(5, 150, 105);
        public static readonly Color Danger     = Color.FromArgb(239, 68, 68);
        public static readonly Color DangerHov  = Color.FromArgb(220, 38, 38);
        public static readonly Color Warning    = Color.FromArgb(245, 158, 11);
        public static readonly Color Surface    = Color.White;
        public static readonly Color BgLight    = Color.FromArgb(248, 250, 252);
        public static readonly Color InputBg    = Color.FromArgb(241, 245, 249);
        public static readonly Color Border     = Color.FromArgb(226, 232, 240);
        public static readonly Color TextPri    = Color.FromArgb(15, 23, 42);
        public static readonly Color TextSec    = Color.FromArgb(100, 116, 139);
        public static readonly Color TextMuted  = Color.FromArgb(148, 163, 184);

        // ── Button styles ─────────────────────────────────────────────────

        public static void Primary(Button b)
        {
            b.BackColor = Accent; b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI Semibold", 10F); b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.BackColor = AccentHov;
            b.MouseLeave += (s, e) => b.BackColor = Accent;
        }

        public static void PrimaryDark(Button b)
        {
            b.BackColor = Dark; b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI Semibold", 10F); b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.BackColor = DarkHov;
            b.MouseLeave += (s, e) => b.BackColor = Dark;
        }

        public static void SuccessBtn(Button b)
        {
            b.BackColor = Success; b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI Semibold", 10F); b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.BackColor = SuccessHov;
            b.MouseLeave += (s, e) => b.BackColor = Success;
        }

        public static void DangerBtn(Button b)
        {
            b.BackColor = Danger; b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI Semibold", 10F); b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.BackColor = DangerHov;
            b.MouseLeave += (s, e) => b.BackColor = Danger;
        }

        public static void Outlined(Button b, Color c)
        {
            b.BackColor = Color.Transparent; b.ForeColor = c;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = c; b.FlatAppearance.BorderSize = 1;
            b.Font = new Font("Segoe UI Semibold", 10F); b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.BackColor = Color.FromArgb(18, c.R, c.G, c.B);
            b.MouseLeave += (s, e) => b.BackColor = Color.Transparent;
        }

        public static void Ghost(Button b)
        {
            b.BackColor = Color.Transparent; b.ForeColor = TextSec;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI", 9F); b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.ForeColor = Accent;
            b.MouseLeave += (s, e) => b.ForeColor = TextSec;
        }

        public static void SidebarNav(Button b)
        {
            b.BackColor = Color.Transparent; b.ForeColor = TextMuted;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI Semibold", 11F);
            b.TextAlign = ContentAlignment.MiddleLeft; b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) =>
            {
                if (b.BackColor != AccentHov) { b.BackColor = DarkHov; b.ForeColor = Color.White; }
            };
            b.MouseLeave += (s, e) =>
            {
                if (b.BackColor != AccentHov) { b.BackColor = Color.Transparent; b.ForeColor = TextMuted; }
            };
        }

        public static void SidebarLogout(Button b)
        {
            b.BackColor = Color.Transparent; b.ForeColor = Danger;
            b.FlatStyle = FlatStyle.Flat; b.FlatAppearance.BorderSize = 0;
            b.Font = new Font("Segoe UI Semibold", 11F);
            b.TextAlign = ContentAlignment.MiddleLeft; b.Cursor = Cursors.Hand;
            b.MouseEnter += (s, e) => b.ForeColor = Color.FromArgb(252, 165, 165);
            b.MouseLeave += (s, e) => b.ForeColor = Danger;
        }

        // ── Input focus highlight ─────────────────────────────────────────
        public static void Input(TextBox t)
        {
            t.BackColor = InputBg; t.BorderStyle = BorderStyle.FixedSingle;
            t.Font = new Font("Segoe UI", 11F);
            t.Enter += (s, e) => t.BackColor = Color.White;
            t.Leave += (s, e) => t.BackColor = InputBg;
        }
    }
}
