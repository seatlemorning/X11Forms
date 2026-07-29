// ButtonPainter.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Theming.Default
{
    internal class ButtonPainter
    {
        public ButtonPainter()
        {
        }

        protected SystemResPool ResPool { get { return ThemeEngine.Current.ResPool; } }

        // Modern flat style colors
        private static Color PrimaryColor = Color.FromArgb(25, 118, 210);
        private static Color PrimaryHover = Color.FromArgb(21, 101, 192);
        private static Color PrimaryPressed = Color.FromArgb(13, 71, 161);
        private static Color BorderColor = Color.FromArgb(224, 224, 224);
        private static Color BorderHover = Color.FromArgb(25, 118, 210);
        private static Color TextColor = Color.FromArgb(33, 33, 33);
        private static Color TextDisabled = Color.FromArgb(189, 189, 189);

        #region Standard Button
        public virtual void Draw(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
        {
            bool isHot = (state & ButtonThemeState.Entered) != 0;
            bool isPressed = (state & ButtonThemeState.Pressed) != 0;
            bool isDisabled = (state & ButtonThemeState.Disabled) != 0;
            bool isDefault = (state & ButtonThemeState.Default) != 0;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Color bgColor, borderColor, fgColor;
            int radius = 4;

            if (isDisabled)
            {
                bgColor = Color.FromArgb(245, 245, 245);
                borderColor = Color.FromArgb(210, 210, 210);
                fgColor = TextDisabled;
            }
            else if (isPressed)
            {
                bgColor = PrimaryPressed;
                borderColor = PrimaryPressed;
                fgColor = Color.White;
            }
            else if (isDefault)
            {
                bgColor = PrimaryColor;
                borderColor = PrimaryColor;
                fgColor = Color.White;
            }
            else
            {
                bgColor = isHot ? PrimaryHover : Color.White;
                borderColor = isHot ? BorderHover : BorderColor;
                fgColor = isHot ? Color.White : TextColor;
            }

            var rect = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);

            using (var path = GetRoundedRectangle(rect, radius))
            {
                using (var brush = new SolidBrush(bgColor))
                    g.FillPath(brush, path);

                using (var pen = new Pen(borderColor, 1))
                    g.DrawPath(pen, path);
            }
        }
        #endregion

        #region FlatStyle Button
        public virtual void DrawFlat(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor, FlatButtonAppearance appearance)
        {
            bool isHot = (state & ButtonThemeState.Entered) != 0;
            bool isPressed = (state & ButtonThemeState.Pressed) != 0;
            bool isDisabled = (state & ButtonThemeState.Disabled) != 0;
            bool isDefault = (state & ButtonThemeState.Default) != 0;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Color bgColor, borderColor, fgColor;
            int radius = 2;

            if (isDisabled)
            {
                bgColor = Color.FromArgb(245, 245, 245);
                borderColor = Color.FromArgb(210, 210, 210);
                fgColor = TextDisabled;
            }
            else if (isPressed)
            {
                bgColor = PrimaryPressed;
                borderColor = PrimaryPressed;
                fgColor = Color.White;
            }
            else if (isDefault)
            {
                bgColor = PrimaryColor;
                borderColor = PrimaryColor;
                fgColor = Color.White;
            }
            else
            {
                bgColor = isHot ? PrimaryHover : Color.White;
                borderColor = isHot ? BorderHover : BorderColor;
                fgColor = isHot ? Color.White : TextColor;
            }

            using (var path = GetRoundedRectangle(bounds, radius))
            {
                using (var brush = new SolidBrush(bgColor))
                    g.FillPath(brush, path);

                using (var pen = new Pen(borderColor, 1))
                    g.DrawPath(pen, path);
            }
        }
        #endregion

        #region Popup Button
        public virtual void DrawPopup(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
        {
            DrawFlat(g, bounds, state, backColor, foreColor, null);
        }
        #endregion

        #region Helper Methods
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int r = Math.Min(radius, Math.Min(rect.Width / 2, rect.Height / 2));

            if (r <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
            path.AddArc(rect.Right - r * 2, rect.Y, r * 2, r * 2, 270, 90);
            path.AddArc(rect.Right - r * 2, rect.Bottom - r * 2, r * 2, r * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r * 2, r * 2, r * 2, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion
    }
}