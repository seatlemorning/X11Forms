// TabControlPainter.cs - Clean modern style with subtle borders
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

        private static readonly Color BorderLight = Color.FromArgb(200, 200, 200);

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

        // ===== CLEAN BACKGROUND =====
        protected virtual void DrawBackground(Graphics dc, Rectangle area, TabControl tab)
        {
            using (var brush = new SolidBrush(SystemColors.Control))
                dc.FillRectangle(brush, area);
        }

        // ===== CLEAN MODERN TAB WITH SUBTLE BORDERS =====
        protected virtual int DrawTab(Graphics dc, TabPage page, TabControl tab, Rectangle bounds, bool is_selected)
        {
            dc.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Color backColor;

            if (is_selected)
            {
                backColor = SystemColors.Window;
            }
            else if (page.Entered)
            {
                backColor = Color.FromArgb(245, 248, 250);
            }
            else
            {
                backColor = SystemColors.Control;
            }

            // Fill tab background
            using (var brush = new SolidBrush(backColor))
                dc.FillRectangle(brush, bounds);

            // Draw border - very subtle light gray lines
            using (var pen = new Pen(BorderLight, 1))
            {
                if (is_selected)
                {
                    // Selected tab: border on left, right, top only
                    dc.DrawLine(pen, bounds.X, bounds.Y + 2, bounds.X, bounds.Bottom);
                    dc.DrawLine(pen, bounds.Right - 1, bounds.Y + 2, bounds.Right - 1, bounds.Bottom);
                    dc.DrawLine(pen, bounds.X + 2, bounds.Y, bounds.Right - 2, bounds.Y);
                    
                    // Bottom line - white to merge with panel
                    using (var whitePen = new Pen(SystemColors.Window, 1))
                    {
                        dc.DrawLine(whitePen, bounds.X + 1, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1);
                    }
                }
                else if (page.Entered)
                {
                    // Hover tab: subtle border
                    dc.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
                }
                else
                {
                    // Inactive tab: only top border
                    dc.DrawLine(pen, bounds.X + 2, bounds.Y, bounds.Right - 2, bounds.Y);
                }
            }

            // Draw text - always black, no highlight
            if (!string.IsNullOrEmpty(page.Text))
            {
                using (var brush = new SolidBrush(SystemColors.ControlText))
                {
                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter,
                        HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show
                    };

                    var textRect = new Rectangle(
                        bounds.X + 4,
                        bounds.Y + 2,
                        bounds.Width - 8,
                        bounds.Height - 4);

                    dc.DrawString(page.Text, tab.Font, brush, textRect, format);
                }
            }

            // Focus indicator
            if (page.Parent.Focused && is_selected && tab.ShowFocusCues)
            {
                using (var pen = new Pen(SystemColors.ControlText, 1) { DashStyle = DashStyle.Dot })
                {
                    var focusRect = new Rectangle(
                        bounds.X + 6,
                        bounds.Y + 4,
                        bounds.Width - 12,
                        bounds.Height - 10);
                    dc.DrawRectangle(pen, focusRect);
                }
            }

            return bounds.Width;
        }

        public virtual bool HasHotElementStyles(TabControl tabControl)
        {
            return false;
        }
    }
}