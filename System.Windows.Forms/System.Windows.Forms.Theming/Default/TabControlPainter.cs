// TabControlPainter.cs - Classic style with rounded green borders

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.Theming.Default
{
    internal class TabControlPainter
    {
        protected SystemResPool ResPool
        {
            get { return ThemeEngine.Current.ResPool; }
        }

        #region private

        private Size defaultItemSize;
        private Point defaultPadding;
        private int minimumTabWidth;
        private Rectangle selectedTabDelta;
        private Point tabPanelOffset;
        private int selectedSpacing;
        private Size rowSpacingNormal;
        private Size rowSpacingButtons;
        private Size rowSpacingFlatButtons;
        private int scrollerWidth;
        private Point focusRectSpacing;
        private Rectangle tabPageSpacing;
        private int colSpacing;
        private int flatButtonSpacing;
        private Point imagePadding;
        private StringFormat defaultFormatting;
        private Rectangle borderThickness;

        // Classic colors with green accent
        private static readonly Color TabBackground = 
            SystemColors.Control;
        
        private static readonly Color TabSelected = 
            SystemColors.Window;
        
        private static readonly Color TabBorder = 
            Color.FromArgb(46, 125, 50); // Green border
        
        private static readonly Color TabText = 
            SystemColors.ControlText;
        
        private static readonly Color TabSelectedText = 
            Color.FromArgb(27, 94, 32); // Dark green text for selected
        
        private static readonly Color TabSelectedBack = 
            Color.FromArgb(245, 255, 245); // Very light green for selected tab background
        
        private static readonly Color Highlight = 
            SystemColors.Highlight;

        #endregion

        #region Properties

        public virtual Size DefaultItemSize
        {
            get { return defaultItemSize; }
            set { defaultItemSize = value; }
        }

        public virtual Point DefaultPadding
        {
            get { return defaultPadding; }
            set { defaultPadding = value; }
        }

        public virtual int MinimumTabWidth
        {
            get { return minimumTabWidth; }
            set { minimumTabWidth = value; }
        }

        public virtual Rectangle SelectedTabDelta
        {
            get { return selectedTabDelta; }
            set { selectedTabDelta = value; }
        }

        public virtual Point TabPanelOffset
        {
            get { return tabPanelOffset; }
            set { tabPanelOffset = value; }
        }

        public virtual int SelectedSpacing
        {
            get { return selectedSpacing; }
            set { selectedSpacing = value; }
        }

        public virtual Size RowSpacingNormal
        {
            get { return rowSpacingNormal; }
            set { rowSpacingNormal = value; }
        }

        public virtual Size RowSpacingButtons
        {
            get { return rowSpacingButtons; }
            set { rowSpacingButtons = value; }
        }

        public virtual Size RowSpacingFlatButtons
        {
            get { return rowSpacingFlatButtons; }
            set { rowSpacingFlatButtons = value; }
        }

        public virtual Point FocusRectSpacing
        {
            get { return focusRectSpacing; }
            set { focusRectSpacing = value; }
        }

        public virtual int ColSpacing
        {
            get { return colSpacing; }
            set { colSpacing = value; }
        }

        public virtual int FlatButtonSpacing
        {
            get { return flatButtonSpacing; }
            set { flatButtonSpacing = value; }
        }

        public virtual Rectangle TabPageSpacing
        {
            get { return tabPageSpacing; }
            set { tabPageSpacing = value; }
        }

        public virtual Point ImagePadding
        {
            get { return imagePadding; }
            set { imagePadding = value; }
        }

        public virtual StringFormat DefaultFormatting
        {
            get { return defaultFormatting; }
            set { defaultFormatting = value; }
        }

        public virtual Rectangle BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; }
        }

        public virtual int ScrollerWidth
        {
            get { return scrollerWidth; }
            set { scrollerWidth = value; }
        }

        public virtual Size RowSpacing(System.Windows.Forms.TabControl tab)
        {
            switch (tab.Appearance)
            {
                case TabAppearance.Normal:
                    return rowSpacingNormal;
                case TabAppearance.Buttons:
                    return rowSpacingButtons;
                case TabAppearance.FlatButtons:
                    return rowSpacingFlatButtons;
                default:
                    throw new Exception("Invalid Appearance value: " + tab.Appearance);
            }
        }

        #endregion

        public TabControlPainter()
        {
            defaultItemSize = new Size(42, 16);
            defaultPadding = new Point(6, 3);
            selectedTabDelta = new Rectangle(2, 2, 4, 3);
            selectedSpacing = 0;

            rowSpacingNormal = new Size(0, 0);
            rowSpacingButtons = new Size(3, 3);
            rowSpacingFlatButtons = new Size(9, 3);

            colSpacing = 0;

            minimumTabWidth = 42;
            scrollerWidth = 17;
            focusRectSpacing = new Point(2, 2);
            tabPanelOffset = new Point(4, 0);
            flatButtonSpacing = 8;
            tabPageSpacing = new Rectangle(4, 2, 3, 4);

            imagePadding = new Point(2, 3);

            defaultFormatting = new StringFormat();
            defaultFormatting.Alignment = StringAlignment.Near;
            defaultFormatting.LineAlignment = StringAlignment.Center;
            defaultFormatting.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip;
            defaultFormatting.HotkeyPrefix = HotkeyPrefix.None;

            borderThickness = new Rectangle(1, 1, 2, 2);
        }

        public virtual Rectangle GetLeftScrollRect(System.Windows.Forms.TabControl tab)
        {
            switch (tab.Alignment)
            {
                case TabAlignment.Top:
                    return new Rectangle(tab.ClientRectangle.Right - (scrollerWidth * 2), tab.ClientRectangle.Top + 1,
                        scrollerWidth, scrollerWidth);
                default:
                    Rectangle panel_rect = GetTabPanelRect(tab);
                    return new Rectangle(tab.ClientRectangle.Right - (scrollerWidth * 2), panel_rect.Bottom + 2,
                        scrollerWidth, scrollerWidth);
            }
        }

        public virtual Rectangle GetRightScrollRect(System.Windows.Forms.TabControl tab)
        {
            switch (tab.Alignment)
            {
                case TabAlignment.Top:
                    return new Rectangle(tab.ClientRectangle.Right - (scrollerWidth), tab.ClientRectangle.Top + 1,
                        scrollerWidth, scrollerWidth);
                default:
                    Rectangle panel_rect = GetTabPanelRect(tab);
                    return new Rectangle(tab.ClientRectangle.Right - (scrollerWidth), panel_rect.Bottom + 2,
                        scrollerWidth, scrollerWidth);
            }
        }

        public Rectangle GetDisplayRectangle(System.Windows.Forms.TabControl tab)
        {
            Rectangle ext = GetTabPanelRect(tab);
            return new Rectangle(ext.Left + tabPageSpacing.X, ext.Top + tabPageSpacing.Y,
                ext.Width - tabPageSpacing.X - tabPageSpacing.Width,
                ext.Height - tabPageSpacing.Y - tabPageSpacing.Height);
        }

        public Rectangle GetTabPanelRect(System.Windows.Forms.TabControl tab)
        {
            Rectangle res = tab.ClientRectangle;

            if (tab.TabCount == 0)
                return res;

            int spacing = RowSpacing(tab).Height;
            int tabOffset = (tab.ItemSize.Height + spacing - selectedTabDelta.Height) * tab.RowCount +
                            selectedTabDelta.Y;
            switch (tab.Alignment)
            {
                case TabAlignment.Top:
                    res.Y += tabOffset;
                    res.Height -= tabOffset;
                    break;
                case TabAlignment.Bottom:
                    res.Height -= tabOffset;
                    break;
                case TabAlignment.Left:
                    res.X += tabOffset;
                    res.Width -= tabOffset;
                    break;
                case TabAlignment.Right:
                    res.Width -= tabOffset;
                    break;
            }

            return res;
        }

        public virtual void Draw(Graphics dc, Rectangle area, TabControl tab)
        {
            DrawBackground(dc, area, tab);

            // Draw panel background and border first
            DrawTabPanel(dc, tab);

            // Draw all tabs
            int start = 0;
            int end = tab.TabPages.Count;
            int delta = 1;

            if (tab.Alignment == TabAlignment.Top)
            {
                start = end;
                end = 0;
                delta = -1;
            }

            if (tab.SizeMode == TabSizeMode.Fixed)
                defaultFormatting.Alignment = StringAlignment.Center;
            else
                defaultFormatting.Alignment = StringAlignment.Near;

            int counter = start;
            for (; counter != end; counter += delta)
            {
                for (int i = tab.SliderPos; i < tab.TabPages.Count; i++)
                {
                    if (i == tab.SelectedIndex)
                        continue;
                    if (counter != tab.TabPages[i].Row)
                        continue;
                    Rectangle rect = tab.GetTabRect(i);
                    if (!rect.IntersectsWith(area))
                        continue;
                    DrawTab(dc, tab.TabPages[i], tab, rect, false);
                }
            }

            if (tab.SelectedIndex != -1 && tab.SelectedIndex >= tab.SliderPos)
            {
                Rectangle rect = tab.GetTabRect(tab.SelectedIndex);
                if (rect.IntersectsWith(area))
                    DrawTab(dc, tab.TabPages[tab.SelectedIndex], tab, rect, true);
            }

            if (tab.ShowSlider)
            {
                Rectangle right = GetRightScrollRect(tab);
                Rectangle left = GetLeftScrollRect(tab);
                DrawScrollButton(dc, right, area, ScrollButton.Right, tab.RightSliderState);
                DrawScrollButton(dc, left, area, ScrollButton.Left, tab.LeftSliderState);
            }
        }

        protected virtual void DrawScrollButton(Graphics dc, Rectangle bounds, Rectangle clippingArea,
            ScrollButton button, PushButtonState state)
        {
            ControlPaint.DrawScrollButton(dc, bounds, button, GetButtonState(state));
        }

        static ButtonState GetButtonState(PushButtonState state)
        {
            switch (state)
            {
                case PushButtonState.Pressed:
                    return ButtonState.Pushed;
                default:
                    return ButtonState.Normal;
            }
        }

        // ===== BACKGROUND =====
        protected virtual void DrawBackground(
            Graphics dc,
            Rectangle area,
            TabControl tab)
        {
            using (var brush = new SolidBrush(TabBackground))
            {
                dc.FillRectangle(brush, area);
            }
        }

        // ===== TAB PANEL BORDER =====
        protected virtual void DrawTabPanel(Graphics dc, TabControl tab)
        {
            Rectangle panelRect = GetTabPanelRect(tab);
            
            if (panelRect.Width > 0 && panelRect.Height > 0)
            {
                // Fill panel background
                using (var brush = new SolidBrush(TabSelected))
                {
                    dc.FillRectangle(brush, panelRect);
                }
                
                // Draw panel border
                using (var pen = new Pen(TabBorder, 1))
                {
                    dc.DrawRectangle(pen, panelRect);
                }
            }
        }

        // ===== TAB WITH ROUNDED GREEN BORDER (NO UNDERLINE) =====
        protected virtual int DrawTab(
            Graphics dc,
            TabPage page,
            TabControl tab,
            Rectangle bounds,
            bool is_selected)
        {
            dc.SmoothingMode = SmoothingMode.AntiAlias;
            dc.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Adjust bounds to avoid overlapping panel border
            Rectangle rect = new Rectangle(
                bounds.X + 2,
                bounds.Y + 1,
                bounds.Width - 4,
                bounds.Height - 1);

            Color backColor, textColor, borderColor;
            int radius = 4;

            if (is_selected)
            {
                backColor = TabSelectedBack;
                textColor = TabSelectedText;
                borderColor = TabBorder;
                
                // Extend selected tab down to cover panel border
                rect.Height += 1;
            }
            else
            {
                backColor = TabBackground;
                textColor = TabText;
                borderColor = TabBorder;
            }

            // Create rounded rectangle path
            using (GraphicsPath path = CreateRoundedRectangleTop(rect, radius))
            {
                // Fill background
                using (var brush = new SolidBrush(backColor))
                {
                    dc.FillPath(brush, path);
                }

                // Draw green border (only top and sides for selected tab)
                using (var pen = new Pen(borderColor, 1))
                {
                    if (is_selected)
                    {
                        // For selected tab, only draw top, left and right borders
                        Rectangle topRect = rect;
                        topRect.Height += 1;
                        using (GraphicsPath topPath = CreateRoundedRectangleTop(topRect, radius))
                        {
                            dc.DrawPath(pen, topPath);
                        }
                    }
                    else
                    {
                        // For unselected tabs, draw full border
                        dc.DrawPath(pen, path);
                    }
                }

                // Light inner glow for selected tab
                if (is_selected)
                {
                    Rectangle innerRect = Rectangle.Inflate(rect, -2, -2);
                    using (GraphicsPath innerPath = CreateRoundedRectangleTop(innerRect, radius - 1))
                    {
                        using (var pen = new Pen(Color.FromArgb(60, 46, 125, 50), 1))
                        {
                            dc.DrawPath(pen, innerPath);
                        }
                    }
                }
            }

            // Tab text - same font for all tabs
            if (!string.IsNullOrEmpty(page.Text))
            {
                using (var brush = new SolidBrush(textColor))
                {
                    var format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter,
                        HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show
                    };

                    Rectangle textRect = new Rectangle(
                        rect.X + 8,
                        rect.Y + 2,
                        rect.Width - 16,
                        rect.Height - 4);

                    dc.DrawString(page.Text, tab.Font, brush, textRect, format);
                }
            }

            // Tab icon (same size for all tabs)
            if (page.ImageIndex != -1 && tab.ImageList != null)
            {
                Image image = tab.ImageList.Images[page.ImageIndex];
                if (image != null)
                {
                    int iconSize = 16;
                    int iconX = rect.X + 4;
                    int iconY = rect.Y + (rect.Height - iconSize) / 2;
                    dc.DrawImage(image, new Rectangle(iconX, iconY, iconSize, iconSize));
                }
            }

            // Focus rectangle
            if (tab.Focused && is_selected && tab.ShowFocusCues)
            {
                Rectangle focusRect = Rectangle.Inflate(rect, -6, -5);
                ControlPaint.DrawFocusRectangle(dc, focusRect);
            }

            return bounds.Width;
        }

        // Rounded rectangle with only top corners rounded (for tabs)
        private GraphicsPath CreateRoundedRectangleTop(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            
            int d = radius * 2;
            
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;
            if (d < 0) d = 0;

            if (d == 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            // Top-left corner
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            // Top-right corner
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            // Bottom-right (straight)
            path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom);
            // Bottom-left (straight)
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            // Close back to top-left
            path.CloseFigure();

            return path;
        }

        public virtual bool HasHotElementStyles(TabControl tabControl)
        {
            return false;
        }
    }
}