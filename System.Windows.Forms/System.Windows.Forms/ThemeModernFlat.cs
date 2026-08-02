// ThemeModernFlat.cs - Simplified modern flat theme

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
    internal class ThemeModernFlat : ThemeWin32Classic
    {
        // ===== COLOR PALETTE =====
        // Accent - Emerald (Green)
        private static readonly Color PrimaryColor =
            Color.FromArgb(15, 0, 210);

        private static readonly Color PrimaryHover =
            Color.FromArgb(15, 100, 210);

        private static readonly Color PrimaryPressed =
            Color.FromArgb(70, 100, 210);

        // Background
        private static readonly Color BackgroundColor =
            Color.FromArgb(247, 248, 250);

        // Controls
        private static readonly Color ControlBackground =
            Color.White;

        private static readonly Color ControlHover =
            Color.FromArgb(245, 250, 248);

        private static readonly Color ControlPressed =
            Color.FromArgb(230, 242, 238);

        // Borders
        private static readonly Color BorderColor =
            Color.FromArgb(220, 224, 222);

        private static readonly Color BorderHover =
            Color.FromArgb(160, 175, 170);

        private static readonly Color BorderFocused =
            Color.FromArgb(35, 150, 120);

        // Text
        private static readonly Color TextColor =
            Color.FromArgb(35, 38, 42);

        private static readonly Color TextSecondary =
            Color.FromArgb(95, 100, 105);

        private static readonly Color TextDisabled =
            Color.FromArgb(175, 178, 180);

        // Selection (Highlight) - Green
        private static readonly Color HighlightColor =
            Color.FromArgb(15, 100, 210);

        private static readonly Color HighlightTextColor =
            Color.White;

        // Shadow
        private static readonly Color ShadowColor =
            Color.FromArgb(30, 0, 0, 0);

        // ToolTip - White background with dark text
        private static readonly Color ToolTipBackColor =
            Color.White;

        private static readonly Color ToolTipTextColor =
            Color.FromArgb(35, 38, 42);

        // ===== StringFormat =====
        private static readonly StringFormat string_format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show,
            Trimming = StringTrimming.None,
            FormatFlags = StringFormatFlags.NoWrap
        };

        private static readonly StringFormat left_format = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center,
            HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show,
            Trimming = StringTrimming.None,
            FormatFlags = StringFormatFlags.NoWrap
        };

        // ===== COLOR PROPERTIES =====
        public override Color ColorScrollBar
        {
            get { return BackgroundColor; }
            set { }
        }

        public override Color ColorDesktop
        {
            get { return BackgroundColor; }
            set { }
        }

        public override Color ColorActiveCaption
        {
            get { return PrimaryColor; }
            set { }
        }

        public override Color ColorInactiveCaption
        {
            get { return Color.FromArgb(189, 195, 199); }
            set { }
        }

        public override Color ColorMenu
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorWindow
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorWindowFrame
        {
            get { return BorderColor; }
            set { }
        }

        public override Color ColorMenuText
        {
            get { return TextColor; }
            set { }
        }

        public override Color ColorWindowText
        {
            get { return TextColor; }
            set { }
        }

        public override Color ColorActiveCaptionText
        {
            get { return Color.White; }
            set { }
        }

        public override Color ColorActiveBorder
        {
            get { return PrimaryColor; }
            set { }
        }

        public override Color ColorAppWorkspace
        {
            get { return BackgroundColor; }
            set { }
        }

        public override Color ColorHighlight
        {
            get { return HighlightColor; }
            set { }
        }

        public override Color ColorHighlightText
        {
            get { return HighlightTextColor; }
            set { }
        }

        public override Color ColorControl
        {
            get { return BackgroundColor; }
            set { }
        }

        public override Color ColorControlDark
        {
            get { return BorderColor; }
            set { }
        }

        public override Color ColorGrayText
        {
            get { return TextDisabled; }
            set { }
        }

        public override Color ColorControlText
        {
            get { return TextColor; }
            set { }
        }

        public override Color ColorInactiveCaptionText
        {
            get { return TextDisabled; }
            set { }
        }

        public override Color ColorControlLight
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorControlDarkDark
        {
            get { return Color.FromArgb(180, 180, 180); }
            set { }
        }

        public override Color ColorControlLightLight
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorButtonFace
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorButtonHighlight
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorButtonShadow
        {
            get { return ControlBackground; }
            set { }
        }

        public override Color ColorInfoText
        {
            get { return ToolTipTextColor; }
            set { }
        }

        public override Color ColorInfo
        {
            get { return ToolTipBackColor; }
            set { }
        }

        public override Color ColorHotTrack
        {
            get { return PrimaryHover; }
            set { }
        }

        public override Color DefaultControlBackColor
        {
            get { return BackgroundColor; }
            set { }
        }

        public override Color DefaultControlForeColor
        {
            get { return TextColor; }
            set { }
        }

        public override Color DefaultWindowBackColor
        {
            get { return ControlBackground; }
        }

        public override Color DefaultWindowForeColor
        {
            get { return TextColor; }
        }

        public override void ResetDefaults()
        {
            base.ResetDefaults();

            // System colors - all via base class
            base.ColorControl = BackgroundColor;
            base.ColorControlLight = ControlBackground;
            base.ColorControlLightLight = ControlBackground;
            base.ColorControlDark = BorderColor;
            base.ColorControlDarkDark = Color.FromArgb(180, 180, 180);
            base.ColorControlText = TextColor;
            base.ColorWindow = ControlBackground;
            base.ColorWindowText = TextColor;
            base.ColorWindowFrame = BorderColor;

            // Highlight - GREEN selection color
            base.ColorHighlight = HighlightColor;
            base.ColorHighlightText = HighlightTextColor;

            base.ColorMenu = ControlBackground;
            base.ColorMenuText = TextColor;
            base.ColorScrollBar = BackgroundColor;
            base.ColorGrayText = TextDisabled;
            base.ColorButtonFace = ControlBackground;
            base.ColorButtonHighlight = HighlightColor;
            base.ColorButtonShadow = BorderColor;
            base.ColorActiveCaption = PrimaryColor;
            base.ColorActiveCaptionText = Color.White;
            base.ColorInactiveCaption = Color.FromArgb(238, 238, 238);
            base.ColorInactiveCaptionText = TextDisabled;

            // ToolTip - White background
            base.ColorInfo = ToolTipBackColor;
            base.ColorInfoText = ToolTipTextColor;

            base.ColorHotTrack = PrimaryHover;
            base.ColorAppWorkspace = BackgroundColor;
            base.ColorActiveBorder = PrimaryColor;
            base.ColorInactiveBorder = Color.FromArgb(200, 200, 200);
        }

        #region ===== BUTTONS =====

        public override void DrawButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds,
            Rectangle clipRectangle)
        {
            DrawModernButton(g, b, textBounds, imageBounds, clipRectangle, false);
        }

        public override void DrawFlatButton(Graphics g, ButtonBase b, Rectangle textBounds, Rectangle imageBounds,
            Rectangle clipRectangle)
        {
            DrawModernButton(g, b, textBounds, imageBounds, clipRectangle, true);
        }

        public override void DrawPopupButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds,
            Rectangle clipRectangle)
        {
            DrawModernButton(g, b, textBounds, imageBounds, clipRectangle, true);
        }

        private void DrawModernButton(Graphics g, ButtonBase button, Rectangle textBounds, Rectangle imageBounds,
            Rectangle clipRectangle, bool isFlat)
        {
            var rect = button.ClientRectangle;
            bool isHot = button.Entered;
            bool isPressed = button.Pressed;
            bool isDisabled = !button.Enabled;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Color backColor;
            Color borderColor;
            Color textColor;

            bool isPrimary = IsDialogPrimary(button);
            bool isCancel = IsDialogCancel(button);

            if (isDisabled)
            {
                backColor = Color.FromArgb(240, 240, 240);
                borderColor = BorderColor;
                textColor = TextDisabled;
            }
            else if (isPressed)
            {
                if (isPrimary)
                    backColor = PrimaryPressed;
                else
                    backColor = ControlPressed;

                borderColor = PrimaryPressed;
                textColor = isPrimary ? Color.White : TextColor;
            }
            else if (isHot)
            {
                if (isPrimary)
                    backColor = PrimaryHover;
                else
                    backColor = ControlHover;

                borderColor = isPrimary ? PrimaryHover : BorderHover;

                textColor = isPrimary ? Color.White : TextColor;
            }
            else
            {
                if (isPrimary)
                {
                    backColor = PrimaryColor;
                    borderColor = PrimaryColor;
                    textColor = Color.White;
                }
                else
                {
                    backColor = isFlat ? Color.Transparent : ControlBackground;
                    borderColor = isFlat ? Color.Transparent : BorderColor;
                    textColor = TextColor;
                }
            }

            int radius = isFlat ? 8 : 10;
            var drawRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            // Shadow only for non-flat buttons
            if (!isFlat && !isDisabled && !isPressed)
            {
                int shadowAlpha = isHot ? 20 : 12;
                using (var shadowBrush = new SolidBrush(Color.FromArgb(shadowAlpha, 0, 0, 0)))
                {
                    var shadowRect = new Rectangle(drawRect.X + 1, drawRect.Y + 3, drawRect.Width, drawRect.Height);
                    using (var path = GetRoundedRectangle(shadowRect, radius))
                        g.FillPath(shadowBrush, path);
                }
            }

            // Background
            using (var path = GetRoundedRectangle(drawRect, radius))
            {
                using (var brush = new SolidBrush(backColor))
                    g.FillPath(brush, path);

                // Border
                int borderWidth = isPrimary ? 0 : (isHot || isPressed ? 1 : 1);
                using (var pen = new Pen(borderColor, borderWidth))
                    g.DrawPath(pen, path);
            }

            int offsetX = isPressed ? 1 : 0;
            int offsetY = isPressed ? 1 : 0;

            if (textBounds != Rectangle.Empty)
                textBounds.Offset(offsetX, offsetY);
            if (imageBounds != Rectangle.Empty)
                imageBounds.Offset(offsetX, offsetY);

            // Image
            if (button.Image != null && imageBounds.Width > 0 && imageBounds.Height > 0)
            {
                float opacity = isDisabled ? 0.4f : 1.0f;
                DrawImageWithOpacity(g, button.Image, imageBounds, opacity);
            }

            // Text
            if (!string.IsNullOrEmpty(button.Text) && textBounds.Width > 0 && textBounds.Height > 0)
            {
                using (var brush = new SolidBrush(textColor))
                {
                    g.DrawString(button.Text, button.Font, brush, textBounds, string_format);
                }
            }

            // Focus
            if (button.Focused && button.Enabled && button.ShowFocusCues && !isFlat)
            {
                using (var pen = new Pen(Color.FromArgb(80, PrimaryColor), 1.5f) { DashStyle = DashStyle.Dot })
                {
                    var focusRect = Rectangle.Inflate(drawRect, -4, -4);
                    g.DrawRectangle(pen, focusRect);
                }
            }
        }

        #endregion

        #region ===== HELPER METHODS =====

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

        private void DrawImageWithOpacity(Graphics g, Image image, Rectangle rect, float opacity)
        {
            if (opacity >= 1.0f)
            {
                g.DrawImage(image, rect);
                return;
            }

            var colorMatrix = new ColorMatrix { Matrix33 = opacity };
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
        }

        private bool IsDialogPrimary(ButtonBase button)
        {
            if (button is Button b)
            {
                Form form = b.FindForm();
                if (form != null && form.AcceptButton == b)
                    return true;

                if (b.DialogResult == DialogResult.OK ||
                    b.DialogResult == DialogResult.Yes)
                    return true;

                string text = b.Text?.ToLowerInvariant() ?? "";
                return text == "ok" ||
                       text == "apply" ||
                       text == "save" ||
                       text == "сохранить" ||
                       text == "применить" ||
                       text == "да" ||
                       text == "yes";
            }

            return false;
        }

        private bool IsDialogCancel(ButtonBase button)
        {
            if (button is Button b)
            {
                Form form = b.FindForm();
                if (form != null && form.CancelButton == b)
                    return true;

                if (b.DialogResult == DialogResult.Cancel ||
                    b.DialogResult == DialogResult.No)
                    return true;

                string text = b.Text?.ToLowerInvariant() ?? "";
                return text == "cancel" ||
                       text == "close" ||
                       text == "отмена" ||
                       text == "закрыть" ||
                       text == "нет" ||
                       text == "no";
            }

            return false;
        }

        #endregion
    }
}