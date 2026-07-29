// RadioButtonPainter.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Theming.Default
{
    internal class RadioButtonPainter
    {
        public RadioButtonPainter()
        {
        }

        protected SystemResPool ResPool { get { return ThemeEngine.Current.ResPool; } }

        private static Color PrimaryColor = Color.FromArgb(25, 118, 210);
        private static Color PrimaryHover = Color.FromArgb(21, 101, 192);
        private static Color BorderColor = Color.FromArgb(224, 224, 224);

        public void PaintRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, ElementState state, FlatStyle style, bool isChecked)
        {
            switch (style)
            {
                case FlatStyle.Standard:
                case FlatStyle.System:
                    switch (state)
                    {
                        case ElementState.Normal:
                            DrawNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Hot:
                            DrawHotRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Pressed:
                            DrawPressedRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Disabled:
                            DrawDisabledRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                    }
                    break;
                case FlatStyle.Flat:
                    switch (state)
                    {
                        case ElementState.Normal:
                            DrawFlatNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Hot:
                            DrawFlatHotRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Pressed:
                            DrawFlatPressedRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Disabled:
                            DrawFlatDisabledRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                    }
                    break;
                case FlatStyle.Popup:
                    switch (state)
                    {
                        case ElementState.Normal:
                            DrawPopupNormalRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Hot:
                            DrawPopupHotRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Pressed:
                            DrawPopupPressedRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                        case ElementState.Disabled:
                            DrawPopupDisabledRadioButton(g, bounds, backColor, foreColor, isChecked);
                            break;
                    }
                    break;
            }
        }

        #region Standard
        public virtual void DrawNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, false);
        }

        public virtual void DrawHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, true, false);
        }

        public virtual void DrawPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, true);
        }

        public virtual void DrawDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, false, true);
        }
        #endregion

        #region FlatStyle
        public virtual void DrawFlatNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, false);
        }

        public virtual void DrawFlatHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, true, false);
        }

        public virtual void DrawFlatPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, true);
        }

        public virtual void DrawFlatDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, false, true);
        }
        #endregion

        #region Popup
        public virtual void DrawPopupNormalRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, false);
        }

        public virtual void DrawPopupHotRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, true, false);
        }

        public virtual void DrawPopupPressedRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, true);
        }

        public virtual void DrawPopupDisabledRadioButton(Graphics g, Rectangle bounds, Color backColor, Color foreColor, bool isChecked)
        {
            DrawModernRadioButton(g, bounds, isChecked, false, false, true);
        }
        #endregion

        #region Modern RadioButton Drawing
        private void DrawModernRadioButton(Graphics g, Rectangle bounds, bool isChecked, bool isHot, bool isPressed, bool isDisabled = false)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int size = Math.Min(bounds.Width, bounds.Height);
            var rect = new Rectangle(
                bounds.X + (bounds.Width - size) / 2,
                bounds.Y + (bounds.Height - size) / 2,
                size, size);

            Color borderColor, innerColor;

            if (isDisabled)
            {
                borderColor = Color.FromArgb(210, 210, 210);
                innerColor = Color.FromArgb(180, 180, 180);
            }
            else if (isChecked)
            {
                borderColor = isHot || isPressed ? PrimaryHover : PrimaryColor;
                innerColor = isHot || isPressed ? PrimaryHover : PrimaryColor;
            }
            else
            {
                borderColor = isHot ? PrimaryHover : BorderColor;
                innerColor = PrimaryColor;
            }

            if (isChecked && !isDisabled)
            {
                using (var shadowBrush = new SolidBrush(Color.FromArgb(40, PrimaryColor)))
                {
                    var shadowRect = new Rectangle(rect.X + 1, rect.Y + 2, rect.Width, rect.Height);
                    g.FillEllipse(shadowBrush, shadowRect);
                }
            }

            using (var pen = new Pen(borderColor, 2))
                g.DrawEllipse(pen, rect);

            if (isChecked)
            {
                var innerRect = Rectangle.Inflate(rect, -6, -6);
                using (var brush = new SolidBrush(Color.White))
                    g.FillEllipse(brush, innerRect);

                var accentRect = Rectangle.Inflate(rect, -8, -8);
                using (var brush = new SolidBrush(Color.FromArgb(50, Color.White)))
                    g.FillEllipse(brush, accentRect);
            }
        }
        #endregion
    }
}