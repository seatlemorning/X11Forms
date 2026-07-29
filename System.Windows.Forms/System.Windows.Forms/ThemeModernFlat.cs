// ThemeModernFlat.cs - Enhanced version with modern visual improvements
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
    internal class ThemeModernFlat : ThemeWin32Classic
    {
        // ===== COLOR PALETTE (Material Design inspired) =====
        private static readonly Color PrimaryColor = Color.FromArgb(25, 118, 210);
        private static readonly Color PrimaryLight = Color.FromArgb(66, 165, 245);
        private static readonly Color PrimaryDark = Color.FromArgb(13, 71, 161);
        private static readonly Color PrimaryHover = Color.FromArgb(21, 101, 192);
        private static readonly Color PrimaryPressed = Color.FromArgb(13, 71, 161);
        
        private static readonly Color AccentColor = Color.FromArgb(255, 87, 34);
        private static readonly Color SuccessColor = Color.FromArgb(76, 175, 80);
        private static readonly Color WarningColor = Color.FromArgb(255, 193, 7);
        private static readonly Color ErrorColor = Color.FromArgb(244, 67, 54);
        
        private static readonly Color BackgroundColor = Color.FromArgb(248, 249, 250);
        private static readonly Color ControlBackground = Color.White;
        private static readonly Color CardBackground = Color.FromArgb(255, 255, 255);
        private static readonly Color BorderColor = Color.FromArgb(224, 224, 224);
        private static readonly Color BorderHover = Color.FromArgb(25, 118, 210);
        private static readonly Color BorderFocus = Color.FromArgb(25, 118, 210);
        
        private static readonly Color TextColor = Color.FromArgb(33, 33, 33);
        private static readonly Color TextSecondary = Color.FromArgb(117, 117, 117);
        private static readonly Color TextDisabled = Color.FromArgb(189, 189, 189);
        private static readonly Color TextHint = Color.FromArgb(158, 158, 158);
        
        // ===== STANDARD StringFormat =====
        private static readonly StringFormat string_format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show,
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };
        
        // ===== ANIMATION SETTINGS =====
        private static readonly float RippleOpacity = 0.3f;
        private static readonly int AnimationDuration = 150;
        private static readonly System.Collections.Hashtable animationStates = new System.Collections.Hashtable();
        
        // ===== ANIMATION STATE CLASS =====
        private class ButtonAnimationState
        {
            public float RippleSize { get; set; }
            public float RippleOpacity { get; set; }
            public DateTime StartTime { get; set; }
            public Point ClickPoint { get; set; }
            public bool IsAnimating { get; set; }
            
            public ButtonAnimationState()
            {
                RippleSize = 0;
                RippleOpacity = 0;
                IsAnimating = false;
                StartTime = DateTime.Now;
            }
        }

        public override void ResetDefaults()
        {
            base.ResetDefaults();
            
            defaultWindowBackColor = ControlBackground;
            defaultWindowForeColor = TextColor;
            
            ColorControl = BackgroundColor;
            ColorControlText = TextColor;
            ColorControlDark = BorderColor;
            ColorControlLight = ControlBackground;
            ColorWindow = ControlBackground;
            ColorWindowText = TextColor;
            ColorHighlight = PrimaryColor;
            ColorHighlightText = Color.White;
            ColorMenu = ControlBackground;
            ColorMenuText = TextColor;
            ColorInfo = PrimaryLight;
            ColorInfoText = Color.White;
            ColorActiveCaption = PrimaryColor;
            ColorActiveCaptionText = Color.White;
            ColorInactiveCaption = Color.FromArgb(238, 238, 238);
            ColorInactiveCaptionText = TextSecondary;
        }

        #region ===== BUTTONS =====
        public override void DrawButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
        {
            DrawModernButton(g, b, textBounds, imageBounds, clipRectangle, false);
        }

        public override void DrawFlatButton(Graphics g, ButtonBase b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
        {
            DrawModernButton(g, b, textBounds, imageBounds, clipRectangle, true);
        }

        public override void DrawPopupButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
        {
            DrawModernButton(g, b, textBounds, imageBounds, clipRectangle, true);
        }

        private void DrawModernButton(Graphics g, ButtonBase button, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle, bool isFlat)
        {
            var rect = button.ClientRectangle;
            bool isHot = button.Entered;
            bool isPressed = button.Pressed;
            bool isDisabled = !button.Enabled;
            bool isDefault = button.IsDefault;
            
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            
            Color backColor1, backColor2, borderColor, textColor;
            
            if (isDisabled)
            {
                backColor1 = Color.FromArgb(245, 245, 245);
                backColor2 = Color.FromArgb(235, 235, 235);
                borderColor = Color.FromArgb(210, 210, 210);
                textColor = TextDisabled;
            }
            else if (isPressed)
            {
                backColor1 = PrimaryPressed;
                backColor2 = PrimaryDark;
                borderColor = PrimaryDark;
                textColor = Color.White;
            }
            else if (isDefault)
            {
                backColor1 = PrimaryColor;
                backColor2 = PrimaryLight;
                borderColor = PrimaryColor;
                textColor = Color.White;
            }
            else
            {
                backColor1 = Color.White;
                backColor2 = Color.FromArgb(252, 252, 252);
                borderColor = isHot ? BorderHover : BorderColor;
                textColor = TextColor;
            }
            
            int radius = isFlat ? 3 : 6;
            var drawRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
            
            if (!isFlat && !isDisabled)
            {
                int shadowDepth = isHot ? 8 : 4;
                int shadowAlpha = isHot ? 35 : 25;
                
                using (var shadowBrush = new SolidBrush(Color.FromArgb(shadowAlpha, 0, 0, 0)))
                {
                    var shadowRect = new Rectangle(drawRect.X + 1, drawRect.Y + shadowDepth / 2, 
                                                  drawRect.Width, drawRect.Height);
                    using (var path = GetRoundedRectangle(shadowRect, radius))
                        g.FillPath(shadowBrush, path);
                }
                
                using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                {
                    var shadowRect = new Rectangle(drawRect.X + 2, drawRect.Y + shadowDepth, 
                                                  drawRect.Width - 2, drawRect.Height - 2);
                    using (var path = GetRoundedRectangle(shadowRect, radius))
                        g.FillPath(shadowBrush, path);
                }
            }
            
            using (var path = GetRoundedRectangle(drawRect, radius))
            {
                if (isFlat && !isDisabled && !isHot && !isPressed && !isDefault)
                {
                    using (var brush = new SolidBrush(backColor1))
                        g.FillPath(brush, path);
                }
                else
                {
                    using (var brush = new LinearGradientBrush(drawRect, backColor1, backColor2, LinearGradientMode.Vertical))
                    {
                        brush.SetSigmaBellShape(0.3f);
                        g.FillPath(brush, path);
                    }
                }
                
                using (var pen = new Pen(borderColor, 1))
                {
                    if (!isDisabled && isHot && !isPressed)
                    {
                        using (var hoverPen = new Pen(PrimaryColor, 2))
                            g.DrawPath(hoverPen, path);
                    }
                    
                    if (!isDisabled && isDefault)
                    {
                        using (var defaultPen = new Pen(PrimaryColor, 2))
                            g.DrawPath(defaultPen, path);
                    }
                    
                    g.DrawPath(pen, path);
                }
                
                if (!isFlat && !isDisabled && !isPressed)
                {
                    using (var highlightBrush = new LinearGradientBrush(
                        new Rectangle(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height / 3),
                        Color.FromArgb(40, Color.White),
                        Color.FromArgb(0, Color.White),
                        LinearGradientMode.Vertical))
                    {
                        using (var highlightPath = GetRoundedRectangle(
                            new Rectangle(drawRect.X + 1, drawRect.Y + 1, drawRect.Width - 2, drawRect.Height / 3),
                            Math.Max(radius - 1, 1)))
                        {
                            g.FillPath(highlightBrush, highlightPath);
                        }
                    }
                }
                
                if (!isDisabled && !isFlat)
                {
                    DrawRippleEffect(g, button, drawRect, radius);
                }
            }
            
            int offsetX = isPressed ? 1 : 0;
            int offsetY = isPressed ? 1 : 0;
            
            if (textBounds != Rectangle.Empty)
                textBounds.Offset(offsetX, offsetY);
            if (imageBounds != Rectangle.Empty)
                imageBounds.Offset(offsetX, offsetY);
            
            if (button.Image != null && imageBounds.Width > 0 && imageBounds.Height > 0)
            {
                float opacity = isDisabled ? 0.4f : (isPressed ? 0.9f : 1.0f);
                DrawImageWithOpacity(g, button.Image, imageBounds, opacity);
            }
            
            if (!string.IsNullOrEmpty(button.Text) && textBounds.Width > 0 && textBounds.Height > 0)
            {
                if (!isFlat && !isDisabled && (isDefault || isPressed))
                {
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
                    {
                        var shadowBounds = new Rectangle(textBounds.X + 1, textBounds.Y + 1, textBounds.Width, textBounds.Height);
                        g.DrawString(button.Text, button.Font, shadowBrush, shadowBounds, string_format);
                    }
                }
                
                using (var brush = new SolidBrush(textColor))
                {
                    g.DrawString(button.Text, button.Font, brush, textBounds, string_format);
                }
            }
            
            if (button.Focused && button.Enabled && button.ShowFocusCues && !isFlat)
            {
                using (var pen = new Pen(Color.FromArgb(120, PrimaryColor), 1.5f) { DashStyle = DashStyle.Dot })
                {
                    var focusRect = Rectangle.Inflate(drawRect, -4, -4);
                    g.DrawRectangle(pen, focusRect);
                }
            }
        }
        
        private void DrawRippleEffect(Graphics g, ButtonBase button, Rectangle rect, int radius)
        {
            var key = button.GetHashCode();
            ButtonAnimationState state = animationStates[key] as ButtonAnimationState;
            
            if (state == null || !state.IsAnimating)
                return;
            
            double elapsed = (DateTime.Now - state.StartTime).TotalMilliseconds;
            double progress = Math.Min(elapsed / AnimationDuration, 1.0);
            double easeOut = 1 - Math.Pow(1 - progress, 2);
            
            float currentSize = (float)(state.RippleSize * easeOut);
            float currentOpacity = (float)(state.RippleOpacity * (1 - easeOut));
            
            if (currentOpacity > 0.01f)
            {
                using (var path = GetRoundedRectangle(rect, radius))
                {
                    using (var region = new Region(path))
                    {
                        var oldClip = g.Clip;
                        g.Clip = region;
                        
                        int rippleX = state.ClickPoint.X - (int)(currentSize / 2);
                        int rippleY = state.ClickPoint.Y - (int)(currentSize / 2);
                        var rippleRect = new Rectangle(rippleX, rippleY, (int)currentSize, (int)currentSize);
                        
                        using (var brush = new SolidBrush(Color.FromArgb((int)(currentOpacity * 255), Color.White)))
                        {
                            g.FillEllipse(brush, rippleRect);
                        }
                        
                        g.Clip = oldClip;
                    }
                }
            }
            
            if (progress >= 1.0)
            {
                state.IsAnimating = false;
                animationStates.Remove(key);
            }
        }
        #endregion

        #region ===== CHECKBOX =====
        public override void DrawCheckBox(Graphics g, CheckBox cb, Rectangle glyphArea, Rectangle textBounds, 
                                         Rectangle imageBounds, Rectangle clipRectangle)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            int size = 18;
            var rect = new Rectangle(glyphArea.X, glyphArea.Y + (glyphArea.Height - size) / 2, size, size);
            
            bool isChecked = cb.Checked;
            bool isEnabled = cb.Enabled;
            bool isHot = cb.Entered;
            
            Color backColor, borderColor, checkColor;
            
            if (!isEnabled)
            {
                backColor = Color.FromArgb(245, 245, 245);
                borderColor = Color.FromArgb(210, 210, 210);
                checkColor = Color.FromArgb(180, 180, 180);
            }
            else
            {
                if (isChecked)
                {
                    backColor = isHot ? PrimaryHover : PrimaryColor;
                    borderColor = isHot ? PrimaryHover : PrimaryColor;
                    checkColor = Color.White;
                }
                else
                {
                    backColor = isHot ? Color.FromArgb(240, 245, 255) : Color.White;
                    borderColor = isHot ? PrimaryHover : BorderColor;
                    checkColor = PrimaryColor;
                }
            }
            
            int radius = 2;
            using (var path = GetRoundedRectangle(rect, radius))
            {
                if (isChecked && isEnabled)
                {
                    using (var shadowBrush = new SolidBrush(Color.FromArgb(40, PrimaryColor)))
                    {
                        var shadowRect = new Rectangle(rect.X + 1, rect.Y + 2, rect.Width, rect.Height);
                        using (var shadowPath = GetRoundedRectangle(shadowRect, radius))
                            g.FillPath(shadowBrush, shadowPath);
                    }
                }
                
                using (var brush = new SolidBrush(backColor))
                    g.FillPath(brush, path);
                
                using (var pen = new Pen(borderColor, isChecked ? 0 : 2))
                    g.DrawPath(pen, path);
                
                if (isChecked)
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
            
            if (!string.IsNullOrEmpty(cb.Text))
            {
                var color = isEnabled ? TextColor : TextDisabled;
                using (var brush = new SolidBrush(color))
                using (var format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show,
                    Trimming = StringTrimming.EllipsisCharacter
                })
                {
                    var textRect = new Rectangle(rect.Right + 8, textBounds.Y, 
                                                Math.Max(5, textBounds.Width - rect.Width - 8), 
                                                textBounds.Height);
                    g.DrawString(cb.Text, cb.Font, brush, textRect, format);
                }
            }
        }
        #endregion

        #region ===== RADIOBUTTON =====
        public override void DrawRadioButton(Graphics g, RadioButton rb, Rectangle glyphArea, Rectangle textBounds, 
                                            Rectangle imageBounds, Rectangle clipRectangle)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            int size = 18;
            var rect = new Rectangle(glyphArea.X, glyphArea.Y + (glyphArea.Height - size) / 2, size, size);
            
            bool isChecked = rb.Checked;
            bool isEnabled = rb.Enabled;
            bool isHot = rb.Entered;
            
            Color borderColor, innerColor;
            
            if (!isEnabled)
            {
                borderColor = Color.FromArgb(210, 210, 210);
                innerColor = Color.FromArgb(180, 180, 180);
            }
            else if (isChecked)
            {
                borderColor = isHot ? PrimaryHover : PrimaryColor;
                innerColor = isHot ? PrimaryHover : PrimaryColor;
            }
            else
            {
                borderColor = isHot ? PrimaryHover : BorderColor;
                innerColor = PrimaryColor;
            }
            
            if (isChecked && isEnabled)
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
            
            if (!string.IsNullOrEmpty(rb.Text))
            {
                var color = isEnabled ? TextColor : TextDisabled;
                using (var brush = new SolidBrush(color))
                using (var format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show,
                    Trimming = StringTrimming.EllipsisCharacter
                })
                {
                    var textRect = new Rectangle(rect.Right + 8, textBounds.Y, 
                                                Math.Max(5, textBounds.Width - rect.Width - 8), 
                                                textBounds.Height);
                    g.DrawString(rb.Text, rb.Font, brush, textRect, format);
                }
            }
        }
        #endregion

        #region ===== GROUPBOX =====
        public override void DrawGroupBox(Graphics dc, Rectangle area, GroupBox box)
        {
            var g = dc;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            var rect = box.ClientRectangle;
            var font = box.Font;
            var text = box.Text;
            
            SizeF textSize = g.MeasureString(text, font);
            int textWidth = (int)textSize.Width + 10;
            int textHeight = (int)textSize.Height;
            
            int yPos = textHeight / 2 + 2;
            int xPos = 12;
            
            using (var pen = new Pen(BorderColor, 1))
            {
                g.DrawLine(pen, rect.X + 10, yPos, rect.X + xPos - 2, yPos);
                g.DrawLine(pen, rect.X + xPos + textWidth + 2, yPos, rect.Right - 10, yPos);
                
                int radius = 4;
                using (var path = new GraphicsPath())
                {
                    g.DrawLine(pen, rect.X + 10, yPos, rect.X + 10, rect.Bottom - 10);
                    g.DrawLine(pen, rect.X + 10, rect.Bottom - 10, rect.Right - 10, rect.Bottom - 10);
                    g.DrawLine(pen, rect.Right - 10, rect.Bottom - 10, rect.Right - 10, yPos);
                }
            }
            
            using (var brush = new SolidBrush(Color.FromArgb(5, PrimaryColor)))
            {
                var bgRect = new Rectangle(rect.X + 10, yPos, rect.Width - 20, rect.Height - yPos - 10);
                g.FillRectangle(brush, bgRect);
            }
            
            if (!string.IsNullOrEmpty(text))
            {
                var color = box.Enabled ? PrimaryColor : TextDisabled;
                using (var brush = new SolidBrush(color))
                using (var format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                })
                {
                    using (var bgBrush = new SolidBrush(Color.White))
                    {
                        var bgRect = new Rectangle(rect.X + xPos - 4, 0, textWidth + 8, textHeight + 2);
                        g.FillRectangle(bgBrush, bgRect);
                    }
                    
                    var textRect = new Rectangle(rect.X + xPos, 0, textWidth, textHeight + 2);
                    g.DrawString(text, font, brush, textRect, format);
                }
            }
        }
        #endregion

        #region ===== TEXTBOX =====
        public override void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea)
        {
            var rect = textBoxBase.ClientRectangle;
            
            using (var brush = new SolidBrush(textBoxBase.BackColor))
                g.FillRectangle(brush, clippingArea);
            
            if (textBoxBase.BorderStyle == BorderStyle.Fixed3D)
            {
                Color borderColor = BorderColor;
                int borderWidth = 1;
                
                if (textBoxBase.Focused)
                {
                    borderColor = BorderFocus;
                    borderWidth = 2;
                    
                    using (var glowBrush = new SolidBrush(Color.FromArgb(30, PrimaryColor)))
                    {
                        var glowRect = new Rectangle(rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4);
                        g.FillRectangle(glowBrush, glowRect);
                    }
                }
                else if (textBoxBase.Entered && textBoxBase.Enabled)
                {
                    borderColor = BorderHover;
                }
                
                using (var pen = new Pen(borderColor, borderWidth))
                    g.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
        }
        #endregion

        #region ===== PROGRESSBAR - MONOLITHIC (NO GRADIENT) =====
        public override void DrawProgressBar(Graphics dc, Rectangle clip_rect, ProgressBar ctrl)
        {
            var g = dc;
            var rect = ctrl.ClientRectangle;
            
            if (rect.Width < 2 || rect.Height < 2) return;
            
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            
            int radius = 3;
            
            // Draw background with rounded corners
            using (var path = GetRoundedRectangle(rect, radius))
            {
                using (var brush = new SolidBrush(Color.FromArgb(235, 235, 235)))
                    g.FillPath(brush, path);
            }
            
            int progress = 0;
            int maxValue = Math.Max(ctrl.Maximum - ctrl.Minimum, 1);
            
            if (ctrl.Style == ProgressBarStyle.Marquee)
            {
                int marqueeWidth = Math.Max(rect.Width / 4, 20);
                int totalWidth = rect.Width + marqueeWidth;
                int marqueePos = (int)((DateTime.Now.Millisecond / 1000.0) * totalWidth) - marqueeWidth;
                progress = Math.Max(0, Math.Min(rect.Width, marqueePos));
                
                var progressRect = new Rectangle(
                    rect.X + progress,
                    rect.Y + 1,
                    Math.Min(marqueeWidth, rect.Width - progress),
                    rect.Height - 2);
                
                if (progressRect.Width > 0 && progressRect.Height > 0)
                {
                    // MONOLITHIC COLOR - using solid color with subtle highlight
                    using (var path = GetRoundedRectangle(progressRect, radius))
                    {
                        // Main solid color
                        using (var brush = new SolidBrush(PrimaryColor))
                            g.FillPath(brush, path);
                        
                        // Subtle highlight on top for depth
                        using (var highlightBrush = new SolidBrush(Color.FromArgb(30, Color.White)))
                        {
                            var highlightRect = new Rectangle(
                                progressRect.X + 2,
                                progressRect.Y + 1,
                                progressRect.Width - 4,
                                progressRect.Height / 2);
                            using (var highlightPath = GetRoundedRectangle(highlightRect, Math.Max(radius - 1, 1)))
                                g.FillPath(highlightBrush, highlightPath);
                        }
                    }
                }
            }
            else
            {
                progress = (int)((ctrl.Value - ctrl.Minimum) * (rect.Width - 2) / maxValue);
                if (progress > 0)
                {
                    var progressRect = new Rectangle(rect.X + 1, rect.Y + 1, progress, rect.Height - 2);
                    
                    // MONOLITHIC COLOR - using solid color with subtle highlight
                    using (var path = GetRoundedRectangle(progressRect, radius))
                    {
                        // Main solid color
                        using (var brush = new SolidBrush(PrimaryColor))
                            g.FillPath(brush, path);
                        
                        // Subtle highlight on top for depth
                        using (var highlightBrush = new SolidBrush(Color.FromArgb(40, Color.White)))
                        {
                            var highlightRect = new Rectangle(
                                progressRect.X + 2,
                                progressRect.Y + 1,
                                progressRect.Width - 4,
                                progressRect.Height / 3);
                            using (var highlightPath = GetRoundedRectangle(highlightRect, Math.Max(radius - 1, 1)))
                                g.FillPath(highlightBrush, highlightPath);
                        }
                        
                        // Subtle shadow at bottom for depth
                        using (var shadowBrush = new SolidBrush(Color.FromArgb(20, Color.Black)))
                        {
                            var shadowRect = new Rectangle(
                                progressRect.X + 2,
                                progressRect.Bottom - progressRect.Height / 3 - 1,
                                progressRect.Width - 4,
                                progressRect.Height / 3);
                            using (var shadowPath = GetRoundedRectangle(shadowRect, Math.Max(radius - 1, 1)))
                                g.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }
            }
            
            // Border
            using (var pen = new Pen(BorderColor, 1))
            using (var path = GetRoundedRectangle(rect, radius))
                g.DrawPath(pen, path);
        }
        #endregion

        #region ===== SCROLLBAR =====
        public override void DrawScrollBar(Graphics dc, Rectangle clip, ScrollBar bar)
        {
            var g = dc;
            var rect = bar.ClientRectangle;
            
            using (var brush = new SolidBrush(Color.FromArgb(248, 249, 250)))
                g.FillRectangle(brush, rect);
            
            base.DrawScrollBar(dc, clip, bar);
        }
        #endregion

        #region ===== MENU =====
        public override void DrawMenuBar(Graphics dc, Menu menu, Rectangle rect)
        {
            var g = dc;
            using (var brush = new SolidBrush(Color.White))
                g.FillRectangle(brush, rect);
            
            using (var pen = new Pen(BorderColor, 1))
                g.DrawLine(pen, rect.X, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            
            base.DrawMenuBar(dc, menu, rect);
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
        
        public void StartRippleAnimation(ButtonBase button, Point clickPoint)
        {
            var key = button.GetHashCode();
            ButtonAnimationState state = animationStates[key] as ButtonAnimationState;
            
            if (state == null)
            {
                state = new ButtonAnimationState();
                animationStates[key] = state;
            }
            
            state.RippleSize = Math.Max(button.Width, button.Height) * 1.5f;
            state.RippleOpacity = RippleOpacity;
            state.ClickPoint = clickPoint;
            state.StartTime = DateTime.Now;
            state.IsAnimating = true;
            
            button.Invalidate();
        }
        #endregion

        #region ===== OTHER METHODS =====
        public override void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style)
        {
            base.ComboBoxDrawBackground(comboBox, g, clippingArea, style);
        }

        public override void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state)
        {
            base.ComboBoxDrawNormalDropDownButton(comboBox, g, clippingArea, area, state);
        }

        public override void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides, Color control_color)
        {
            using (var pen = new Pen(BorderColor, 1))
            {
                if ((sides & Border3DSide.Left) != 0)
                    graphics.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom);
                if ((sides & Border3DSide.Top) != 0)
                    graphics.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.Right, rectangle.Y);
                if ((sides & Border3DSide.Right) != 0)
                    graphics.DrawLine(pen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom);
                if ((sides & Border3DSide.Bottom) != 0)
                    graphics.DrawLine(pen, rectangle.X, rectangle.Bottom - 1, rectangle.Right, rectangle.Bottom - 1);
            }
        }

        public override void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle)
        {
            base.TreeViewDrawNodePlusMinus(treeView, node, dc, x, middle);
        }
        #endregion
    }
}