// CheckBoxPainter.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Theming.Default
{
    internal class CheckBoxPainter
    {
        public CheckBoxPainter()
        {
        }

        protected SystemResPool ResPool { get { return ThemeEngine.Current.ResPool; } }

        private static Color PrimaryColor = Color.FromArgb(25, 118, 210);
        private static Color PrimaryHover = Color.FromArgb(21, 101, 192);
        private static Color BorderColor = Color.FromArgb(224, 224, 224);
        private static Color TextColor = Color.FromArgb(33, 33, 33);
        private static Color TextDisabled = Color.FromArgb(189, 189, 189);

        public void PaintCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, ElementState state, FlatStyle style, CheckState checkState)
        {
            switch (style)
            {
                case FlatStyle.Standard:
                case FlatStyle.System:
                    switch (state)
                    {
                        case ElementState.Normal:
                            DrawNormalCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Hot:
                            DrawHotCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Pressed:
                            DrawPressedCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Disabled:
                            DrawDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                    }
                    break;
                case FlatStyle.Flat:
                    switch (state)
                    {
                        case ElementState.Normal:
                            DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Hot:
                            DrawFlatHotCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Pressed:
                            DrawFlatPressedCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Disabled:
                            DrawFlatDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                    }
                    break;
                case FlatStyle.Popup:
                    switch (state)
                    {
                        case ElementState.Normal:
                            DrawPopupNormalCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Hot:
                            DrawPopupHotCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Pressed:
                            DrawPopupPressedCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                        case ElementState.Disabled:
                            DrawPopupDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
                            break;
                    }
                    break;
            }
        }

        #region Standard
        public virtual void DrawNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, false);
        }

        public virtual void DrawHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, true, false);
        }

        public virtual void DrawPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, true);
        }

        public virtual void DrawDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, false, true);
        }
        #endregion

        #region FlatStyle
        public virtual void DrawFlatNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, false);
        }

        public virtual void DrawFlatHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, true, false);
        }

        public virtual void DrawFlatPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, true);
        }

        public virtual void DrawFlatDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, false, true);
        }
        #endregion

        #region Popup
        public virtual void DrawPopupNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, false);
        }

        public virtual void DrawPopupHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, true, false);
        }

        public virtual void DrawPopupPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, true);
        }

        public virtual void DrawPopupDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
        {
            DrawModernCheckBox(g, bounds, state, false, false, true);
        }
        #endregion

        #region Modern CheckBox Drawing
        private void DrawModernCheckBox(Graphics g, Rectangle bounds, CheckState state, bool isHot, bool isPressed, bool isDisabled = false)
        {
            bool isChecked = state == CheckState.Checked;
            bool isIndeterminate = state == CheckState.Indeterminate;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            int size = Math.Min(bounds.Width, bounds.Height);
            var rect = new Rectangle(
                bounds.X + (bounds.Width - size) / 2,
                bounds.Y + (bounds.Height - size) / 2,
                size, size);

            Color bgColor, borderColor, checkColor;

            if (isDisabled)
            {
                bgColor = Color.FromArgb(245, 245, 245);
                borderColor = Color.FromArgb(210, 210, 210);
                checkColor = Color.FromArgb(180, 180, 180);
            }
            else if (isChecked || isIndeterminate)
            {
                bgColor = isHot || isPressed ? PrimaryHover : PrimaryColor;
                borderColor = isHot || isPressed ? PrimaryHover : PrimaryColor;
                checkColor = Color.White;
            }
            else
            {
                bgColor = isHot ? Color.FromArgb(240, 245, 255) : Color.White;
                borderColor = isHot ? PrimaryHover : BorderColor;
                checkColor = PrimaryColor;
            }

            int radius = 2;
            using (var path = GetRoundedRectangle(rect, radius))
            {
                using (var brush = new SolidBrush(bgColor))
                    g.FillPath(brush, path);

                using (var pen = new Pen(borderColor, isChecked ? 0 : 2))
                    g.DrawPath(pen, path);

                if (isChecked || isIndeterminate)
                {
                    using (var pen = new Pen(Color.FromArgb(50, Color.White), 1))
                        g.DrawPath(pen, path);
                }
            }

            if (isChecked)
            {
                using (var pen = new Pen(checkColor, 2.5f))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    int padding = 3;
                    int x1 = rect.X + padding + 1;
                    int y1 = rect.Y + rect.Height / 2 + 1;
                    int x2 = rect.X + rect.Width / 2 - 1;
                    int y2 = rect.Y + rect.Height - padding - 1;
                    int x3 = rect.X + rect.Width - padding - 1;
                    int y3 = rect.Y + padding + 1;

                    g.DrawLine(pen, x1, y1, x2, y2);
                    g.DrawLine(pen, x2, y2, x3, y3);
                }
            }
            else if (isIndeterminate)
            {
                using (var pen = new Pen(checkColor, 2.5f))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;

                    int padding = 4;
                    int x1 = rect.X + padding;
                    int y1 = rect.Y + rect.Height / 2;
                    int x2 = rect.X + rect.Width - padding;
                    int y2 = rect.Y + rect.Height / 2;

                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }

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