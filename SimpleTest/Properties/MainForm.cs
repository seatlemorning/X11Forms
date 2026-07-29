using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SimpleTest
{
    public class MainForm : Form
    {
        private TabControl tabControl;
        private TabPage tabButtons;
        private TabPage tabInputs;
        private TabPage tabContainers;
        private TabPage tabProgress;
        private TabPage tabLists;
        private TabPage tabMenus;
        private TabPage tabAdvanced;
        private StatusBar statusBar;
        private Timer timer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "ThemeModernFlat Demo - All Controls";
            this.Size = new Size(1100, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
            this.Font = new Font("Segoe UI", 9F);

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F),
                Padding = new Point(8, 5),
                ItemSize = new Size(120, 32),
                SizeMode = TabSizeMode.Fixed,
                HotTrack = true
            };

            tabButtons = new TabPage("Buttons");
            tabInputs = new TabPage("Inputs");
            tabContainers = new TabPage("Containers");
            tabProgress = new TabPage("Progress");
            tabLists = new TabPage("Lists");
            tabMenus = new TabPage("Menu & Status");
            tabAdvanced = new TabPage("Advanced");

            tabControl.TabPages.AddRange(new TabPage[] { 
                tabButtons, tabInputs, tabContainers, tabProgress, tabLists, tabMenus, tabAdvanced
            });

            SetupButtonsTab();
            SetupInputsTab();
            SetupContainersTab();
            SetupProgressTab();
            SetupListsTab();
            SetupMenusTab();
            SetupAdvancedTab();

            this.Controls.Add(tabControl);
            SetupStatusBar();
            SetupMenu();
        }

        #region Buttons Tab
        private void SetupButtonsTab()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var leftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            var rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // Buttons
            leftPanel.Controls.Add(CreateHeader("Standard Buttons"));
            
            var btn1 = new Button { Text = "Normal Button", Size = new Size(160, 38), Font = new Font("Segoe UI", 10F) };
            btn1.Click += (s, e) => UpdateStatus("Normal button clicked");
            leftPanel.Controls.Add(btn1);

            var btn2 = new Button { Text = "Default Button", Size = new Size(160, 38), Font = new Font("Segoe UI", 10F) };
            btn2.Click += (s, e) => UpdateStatus("Default button clicked");
            leftPanel.Controls.Add(btn2);

            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Flat Buttons"));

            var btn3 = new Button { Text = "Flat Button", Size = new Size(160, 38), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F) };
            btn3.Click += (s, e) => UpdateStatus("Flat button clicked");
            leftPanel.Controls.Add(btn3);

            var btn4 = new Button { Text = "Popup Button", Size = new Size(160, 38), FlatStyle = FlatStyle.Popup, Font = new Font("Segoe UI", 10F) };
            btn4.Click += (s, e) => UpdateStatus("Popup button clicked");
            leftPanel.Controls.Add(btn4);

            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Buttons with Images"));

            var btn5 = new Button { Text = "Open", Size = new Size(160, 38), Image = CreateSampleIcon(), ImageAlign = ContentAlignment.MiddleLeft, TextAlign = ContentAlignment.MiddleRight, Font = new Font("Segoe UI", 10F) };
            btn5.Click += (s, e) => UpdateStatus("Open button clicked");
            leftPanel.Controls.Add(btn5);

            // CheckBoxes & RadioButtons
            rightPanel.Controls.Add(CreateHeader("CheckBox"));

            var cb1 = new CheckBox { Text = "Standard CheckBox", Font = new Font("Segoe UI", 10F), Checked = true, AutoSize = true, Margin = new Padding(5, 5, 5, 10) };
            cb1.CheckedChanged += (s, e) => UpdateStatus($"CheckBox: {(cb1.Checked ? "Checked" : "Unchecked")}");
            rightPanel.Controls.Add(cb1);

            var cb2 = new CheckBox { Text = "Three State CheckBox", Font = new Font("Segoe UI", 10F), ThreeState = true, CheckState = CheckState.Indeterminate, AutoSize = true, Margin = new Padding(5, 5, 5, 10) };
            cb2.CheckStateChanged += (s, e) => UpdateStatus($"Three State: {cb2.CheckState}");
            rightPanel.Controls.Add(cb2);

            var cb3 = new CheckBox { Text = "Disabled CheckBox", Font = new Font("Segoe UI", 10F), Enabled = false, Checked = true, AutoSize = true, Margin = new Padding(5, 5, 5, 10) };
            rightPanel.Controls.Add(cb3);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("RadioButton"));

            var rb1 = new RadioButton { Text = "Radio 1 - Active", Font = new Font("Segoe UI", 10F), Checked = true, AutoSize = true, Margin = new Padding(5, 5, 5, 10) };
            rb1.CheckedChanged += (s, e) => UpdateStatus($"Selected: {rb1.Text}");
            rightPanel.Controls.Add(rb1);

            var rb2 = new RadioButton { Text = "Radio 2 - Inactive", Font = new Font("Segoe UI", 10F), AutoSize = true, Margin = new Padding(5, 5, 5, 10) };
            rb2.CheckedChanged += (s, e) => UpdateStatus($"Selected: {rb2.Text}");
            rightPanel.Controls.Add(rb2);

            var rb3 = new RadioButton { Text = "Disabled Radio", Font = new Font("Segoe UI", 10F), Enabled = false, AutoSize = true, Margin = new Padding(5, 5, 5, 10) };
            rightPanel.Controls.Add(rb3);

            panel.Controls.Add(leftPanel, 0, 0);
            panel.Controls.Add(rightPanel, 1, 0);
            tabButtons.Controls.Add(panel);
        }
        #endregion

        #region Inputs Tab
        private void SetupInputsTab()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var leftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            var rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // TextBoxes
            leftPanel.Controls.Add(CreateHeader("TextBox"));

            var tb1 = new TextBox { Text = "Standard TextBox", Width = 300, Font = new Font("Segoe UI", 10F) };
            leftPanel.Controls.Add(tb1);

            leftPanel.Controls.Add(CreateSpacer());

            var tb2 = new TextBox { Text = "Multiline TextBox\nWith support\nMultiple lines", Width = 300, Height = 80, Multiline = true, Font = new Font("Segoe UI", 10F) };
            leftPanel.Controls.Add(tb2);

            leftPanel.Controls.Add(CreateSpacer());

            var tb3 = new TextBox { Text = "secret123", Width = 300, Font = new Font("Segoe UI", 10F), PasswordChar = '●' };
            leftPanel.Controls.Add(tb3);

            leftPanel.Controls.Add(CreateSpacer());

            var tb4 = new TextBox { Text = "Disabled Field", Width = 300, Font = new Font("Segoe UI", 10F), Enabled = false };
            leftPanel.Controls.Add(tb4);

            // ComboBoxes, DateTimePicker, NumericUpDown
            rightPanel.Controls.Add(CreateHeader("ComboBox"));

            var cb1 = new ComboBox { Width = 250, Font = new Font("Segoe UI", 10F), DropDownStyle = ComboBoxStyle.DropDownList };
            cb1.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
            cb1.SelectedIndex = 0;
            cb1.SelectedIndexChanged += (s, e) => UpdateStatus($"Selected: {cb1.SelectedItem}");
            rightPanel.Controls.Add(cb1);

            rightPanel.Controls.Add(CreateSpacer());

            var cb2 = new ComboBox { Width = 250, Font = new Font("Segoe UI", 10F), DropDownStyle = ComboBoxStyle.DropDown };
            cb2.Items.AddRange(new object[] { "Editable", "ComboBox", "With text input" });
            cb2.Text = "Enter text...";
            rightPanel.Controls.Add(cb2);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("DateTimePicker"));

            var dtp = new DateTimePicker { Width = 250, Font = new Font("Segoe UI", 10F), Value = DateTime.Now, Format = DateTimePickerFormat.Long };
            rightPanel.Controls.Add(dtp);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("NumericUpDown"));

            var nud = new NumericUpDown { Width = 150, Font = new Font("Segoe UI", 10F), Minimum = 0, Maximum = 100, Value = 50, TextAlign = HorizontalAlignment.Center };
            nud.ValueChanged += (s, e) => UpdateStatus($"Value: {nud.Value}");
            rightPanel.Controls.Add(nud);

            panel.Controls.Add(leftPanel, 0, 0);
            panel.Controls.Add(rightPanel, 1, 0);
            tabInputs.Controls.Add(panel);
        }
        #endregion

        #region Containers Tab
        private void SetupContainersTab()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 2,
                RowCount = 2
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // GroupBox
            var gb = new GroupBox { Text = "GroupBox with Controls", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), Padding = new Padding(8) };
            var gbPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10), FlowDirection = FlowDirection.TopDown };
            gbPanel.Controls.Add(new RadioButton { Text = "Option 1", AutoSize = true, Checked = true });
            gbPanel.Controls.Add(new RadioButton { Text = "Option 2", AutoSize = true });
            gbPanel.Controls.Add(new CheckBox { Text = "CheckBox in Group", AutoSize = true });
            gbPanel.Controls.Add(new Button { Text = "Action", Size = new Size(100, 28), Font = new Font("Segoe UI", 9F) });
            gb.Controls.Add(gbPanel);
            panel.Controls.Add(gb, 0, 0);

            // Panel with border
            var pnl1 = new Panel { Dock = DockStyle.Fill, BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White };
            var lbl1 = new Label { Text = "Panel with 3D border\n(modern flat style)", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            pnl1.Controls.Add(lbl1);
            panel.Controls.Add(pnl1, 1, 0);

            // Panel without border
            var pnl2 = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(240, 248, 255), BorderStyle = BorderStyle.None };
            var lbl2 = new Label { Text = "Panel without border\nwith colored background", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            pnl2.Controls.Add(lbl2);
            panel.Controls.Add(pnl2, 0, 1);

            // SplitContainer
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, BorderStyle = BorderStyle.Fixed3D, SplitterDistance = 170, SplitterWidth = 3, BackColor = Color.White };
            split.Panel1.BackColor = Color.FromArgb(240, 248, 255);
            split.Panel2.BackColor = Color.FromArgb(255, 240, 245);
            var lblLeft = new Label { Text = "Left Panel", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F) };
            split.Panel1.Controls.Add(lblLeft);
            var lblRight = new Label { Text = "Right Panel", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F) };
            split.Panel2.Controls.Add(lblRight);
            panel.Controls.Add(split, 1, 1);

            tabContainers.Controls.Add(panel);
        }
        #endregion

        #region Progress Tab
        private void SetupProgressTab()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var leftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            var rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // ProgressBars
            leftPanel.Controls.Add(CreateHeader("ProgressBar"));

            var pb1 = new ProgressBar { Width = 400, Height = 28, Minimum = 0, Maximum = 100, Value = 70 };
            leftPanel.Controls.Add(pb1);

            leftPanel.Controls.Add(CreateSpacer());

            var pb2 = new ProgressBar { Width = 400, Height = 28, Style = ProgressBarStyle.Marquee, MarqueeAnimationSpeed = 25 };
            leftPanel.Controls.Add(pb2);

            leftPanel.Controls.Add(CreateSpacer());

            var btnPanel = new FlowLayoutPanel { Width = 400, FlowDirection = FlowDirection.LeftToRight };
            var btnUp = new Button { Text = "Increase", Size = new Size(120, 30), Font = new Font("Segoe UI", 9F) };
            btnUp.Click += (s, e) => { pb1.Value = Math.Min(100, pb1.Value + 10); UpdateStatus($"Progress: {pb1.Value}%"); };
            var btnDown = new Button { Text = "Decrease", Size = new Size(120, 30), Font = new Font("Segoe UI", 9F) };
            btnDown.Click += (s, e) => { pb1.Value = Math.Max(0, pb1.Value - 10); UpdateStatus($"Progress: {pb1.Value}%"); };
            var btnReset = new Button { Text = "Reset", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnReset.Click += (s, e) => { pb1.Value = 0; UpdateStatus("Progress reset"); };
            btnPanel.Controls.Add(btnUp);
            btnPanel.Controls.Add(btnDown);
            btnPanel.Controls.Add(btnReset);
            leftPanel.Controls.Add(btnPanel);

            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("TrackBar (Horizontal)"));

            var tb1 = new TrackBar { Width = 400, Minimum = 0, Maximum = 100, Value = 50, TickFrequency = 10, TickStyle = TickStyle.BottomRight, LargeChange = 10 };
            var lblTb1 = new Label { Text = "Value: 50", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            tb1.ValueChanged += (s, e) => { lblTb1.Text = $"Value: {tb1.Value}"; UpdateStatus($"TrackBar: {tb1.Value}"); };
            leftPanel.Controls.Add(tb1);
            leftPanel.Controls.Add(lblTb1);

            // Vertical TrackBar & ScrollBar
            rightPanel.Controls.Add(CreateHeader("TrackBar (Vertical)"));

            var tbV = new TrackBar { Height = 150, Width = 45, Orientation = Orientation.Vertical, Minimum = 0, Maximum = 100, Value = 75, TickFrequency = 10, TickStyle = TickStyle.Both };
            var lblTbV = new Label { Text = "Value: 75", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            tbV.ValueChanged += (s, e) => { lblTbV.Text = $"Value: {tbV.Value}"; UpdateStatus($"Vertical TrackBar: {tbV.Value}"); };
            rightPanel.Controls.Add(tbV);
            rightPanel.Controls.Add(lblTbV);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("ScrollBar (Horizontal)"));

            var hsb = new HScrollBar { Width = 350, Minimum = 0, Maximum = 100, Value = 30, LargeChange = 10, SmallChange = 1 };
            var lblHs = new Label { Text = "Scroll: 30", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            hsb.ValueChanged += (s, e) => { lblHs.Text = $"Scroll: {hsb.Value}"; UpdateStatus($"ScrollBar: {hsb.Value}"); };
            rightPanel.Controls.Add(hsb);
            rightPanel.Controls.Add(lblHs);

            panel.Controls.Add(leftPanel, 0, 0);
            panel.Controls.Add(rightPanel, 1, 0);
            tabProgress.Controls.Add(panel);
        }
        #endregion

        #region Lists Tab
        private void SetupListsTab()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 2,
                RowCount = 2
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // ListBox
            var lbGroup = new GroupBox { Text = "ListBox", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F) };
            var lb = new ListBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
            for (int i = 1; i <= 8; i++) lb.Items.Add($"Item {i}");
            lb.SelectedIndexChanged += (s, e) => { if (lb.SelectedItem != null) UpdateStatus($"Selected: {lb.SelectedItem}"); };
            lbGroup.Controls.Add(lb);
            panel.Controls.Add(lbGroup, 0, 0);

            // CheckedListBox
            var clbGroup = new GroupBox { Text = "CheckedListBox", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F) };
            var clb = new CheckedListBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), CheckOnClick = true };
            for (int i = 1; i <= 5; i++) clb.Items.Add($"Item {i}");
            clb.SetItemChecked(0, true);
            clb.SetItemChecked(2, true);
            clb.SetItemChecked(4, true);
            clb.ItemCheck += (s, e) => UpdateStatus($"{(e.NewValue == CheckState.Checked ? "Checked" : "Unchecked")}: {clb.Items[e.Index]}");
            clbGroup.Controls.Add(clb);
            panel.Controls.Add(clbGroup, 1, 0);

            // TreeView
            var tvGroup = new GroupBox { Text = "TreeView", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F) };
            var tv = new TreeView { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
            var root1 = tv.Nodes.Add("Root 1");
            root1.Nodes.Add("Child 1.1");
            root1.Nodes.Add("Child 1.2");
            root1.Nodes.Add("Child 1.3");
            var root2 = tv.Nodes.Add("Root 2");
            root2.Nodes.Add("Child 2.1");
            root2.Nodes.Add("Child 2.2");
            root2.Nodes.Add("Child 2.3");
            tv.AfterSelect += (s, e) => { if (e.Node != null) UpdateStatus($"Selected: {e.Node.Text}"); };
            tvGroup.Controls.Add(tv);
            panel.Controls.Add(tvGroup, 0, 1);

            // ListView
            var lvGroup = new GroupBox { Text = "ListView", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F) };
            var lv = new ListView { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), View = View.Details, FullRowSelect = true, GridLines = true };
            lv.Columns.Add("Name", 120);
            lv.Columns.Add("Value", 120);
            lv.Columns.Add("Status", 100);
            lv.Columns.Add("Date", 120);
            var date = DateTime.Now;
            lv.Items.Add(new ListViewItem(new[] { "Item 1", "Value 1", "Active", date.ToShortDateString() }));
            lv.Items.Add(new ListViewItem(new[] { "Item 2", "Value 2", "Pending", date.AddDays(1).ToShortDateString() }));
            lv.Items.Add(new ListViewItem(new[] { "Item 3", "Value 3", "Done", date.AddDays(-1).ToShortDateString() }));
            lv.Items.Add(new ListViewItem(new[] { "Item 4", "Value 4", "Active", date.AddDays(2).ToShortDateString() }));
            lv.SelectedIndexChanged += (s, e) => { if (lv.SelectedItems.Count > 0) UpdateStatus($"Selected: {lv.SelectedItems[0].SubItems[0].Text}"); };
            lvGroup.Controls.Add(lv);
            panel.Controls.Add(lvGroup, 1, 1);

            tabLists.Controls.Add(panel);
        }
        #endregion

        #region Menu & Status Tab
        private void SetupMenusTab()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 2,
                RowCount = 1
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            var leftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            var rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // Info
            var infoGroup = new GroupBox { Text = "Theme Information", Width = 420, Height = 200, Font = new Font("Segoe UI", 10F) };
            var infoLabel = new Label
            {
                Text = "ThemeModernFlat\n" +
                       "━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                       "Flat Design (Material Design)\n" +
                       "Animations & Ripple Effects\n" +
                       "Gradients & Shadows\n" +
                       "Modern Colors\n" +
                       "Rounded Corners\n" +
                       "Focus Highlight\n" +
                       "Cross-platform\n" +
                       "━━━━━━━━━━━━━━━━━━━━━━━━━━━",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            infoGroup.Controls.Add(infoLabel);
            leftPanel.Controls.Add(infoGroup);

            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Context Menu"));

            var ctxMenu = new ContextMenu();
            ctxMenu.MenuItems.Add(new MenuItem("Action 1", (s, e) => UpdateStatus("Action 1 executed")));
            ctxMenu.MenuItems.Add(new MenuItem("Action 2", (s, e) => UpdateStatus("Action 2 executed")));
            ctxMenu.MenuItems.Add(new MenuItem("-"));
            ctxMenu.MenuItems.Add(new MenuItem("Properties", (s, e) => UpdateStatus("Properties opened")));

            var ctxPanel = new Panel { Size = new Size(420, 80), BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White, ContextMenu = ctxMenu };
            var ctxLabel = new Label { Text = "Right-click here for context menu", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            ctxPanel.Controls.Add(ctxLabel);
            leftPanel.Controls.Add(ctxPanel);

            // Status demo
            rightPanel.Controls.Add(CreateHeader("Status Demo"));

            var statusGroup = new GroupBox { Text = "Status Bar Panels", Width = 420, Height = 150, Font = new Font("Segoe UI", 10F) };
            var statusPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10), FlowDirection = FlowDirection.TopDown };

            var statLabel = new Label { Text = "Current Status: System running", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            statusPanel.Controls.Add(statLabel);

            var btnStatus = new Button { Text = "Change Status", Size = new Size(150, 30), Font = new Font("Segoe UI", 9F) };
            int statusIndex = 0;
            string[] statuses = { "System running", "Warning! Check data", "Error! Intervention required", "Operation in progress..." };
            btnStatus.Click += (s, e) =>
            {
                statusIndex = (statusIndex + 1) % statuses.Length;
                statLabel.Text = $"Current Status: {statuses[statusIndex]}";
                UpdateStatus(statuses[statusIndex]);
                if (statusBar.Panels.Count > 1)
                    statusBar.Panels[1].Text = statuses[statusIndex];
            };
            statusPanel.Controls.Add(btnStatus);
            statusGroup.Controls.Add(statusPanel);
            rightPanel.Controls.Add(statusGroup);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("Timer"));

            var timerGroup = new GroupBox { Text = "Time in Status Bar", Width = 420, Height = 80, Font = new Font("Segoe UI", 10F) };
            var timerLabel = new Label { Text = "Current Time: " + DateTime.Now.ToLongTimeString(), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F) };
            var updateTimer = new Timer { Interval = 1000, Enabled = true };
            updateTimer.Tick += (s, e) => timerLabel.Text = "Current Time: " + DateTime.Now.ToLongTimeString();
            timerGroup.Controls.Add(timerLabel);
            rightPanel.Controls.Add(timerGroup);

            panel.Controls.Add(leftPanel, 0, 0);
            panel.Controls.Add(rightPanel, 1, 0);
            tabMenus.Controls.Add(panel);
        }
        #endregion

        #region Advanced Tab
        private void SetupAdvancedTab()
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true
            };

            panel.Controls.Add(CreateHeader("Additional Controls"));

            // RichTextBox
            panel.Controls.Add(CreateHeader("RichTextBox"));
            var rtb = new RichTextBox
            {
                Width = 500,
                Height = 120,
                Font = new Font("Segoe UI", 10F),
                Text = "RichTextBox with formatting support.\nCan insert text with different styles.\nSupports multiline input."
            };
            panel.Controls.Add(rtb);

            panel.Controls.Add(CreateSpacer());

            // ListView with icons
            panel.Controls.Add(CreateHeader("ListView with Icons"));
            var lvIcons = new ListView
            {
                Width = 500,
                Height = 150,
                View = View.LargeIcon,
                Font = new Font("Segoe UI", 10F)
            };

            var imgList = new ImageList { ImageSize = new Size(32, 32), ColorDepth = ColorDepth.Depth32Bit };
            imgList.Images.Add(CreateColoredIcon(Color.FromArgb(25, 118, 210), "F"));
            imgList.Images.Add(CreateColoredIcon(Color.FromArgb(76, 175, 80), "D"));
            imgList.Images.Add(CreateColoredIcon(Color.FromArgb(255, 87, 34), "T"));
            imgList.Images.Add(CreateColoredIcon(Color.FromArgb(255, 193, 7), "S"));
            lvIcons.LargeImageList = imgList;
            lvIcons.Items.Add(new ListViewItem("Folder", 0));
            lvIcons.Items.Add(new ListViewItem("Document", 1));
            lvIcons.Items.Add(new ListViewItem("Table", 2));
            lvIcons.Items.Add(new ListViewItem("Star", 3));
            panel.Controls.Add(lvIcons);

            panel.Controls.Add(CreateSpacer());

            // LinkLabel
            panel.Controls.Add(CreateHeader("LinkLabel"));
            var linkLabel = new LinkLabel
            {
                Text = "Visit demo website",
                Font = new Font("Segoe UI", 10F),
                AutoSize = true
            };
            linkLabel.LinkClicked += (s, e) => UpdateStatus("Link clicked");
            panel.Controls.Add(linkLabel);

            tabAdvanced.Controls.Add(panel);
        }
        #endregion

        #region Status Bar
        private void SetupStatusBar()
        {
            statusBar = new StatusBar
            {
                Dock = DockStyle.Bottom,
                ShowPanels = true,
                Font = new Font("Segoe UI", 9F)
            };

            var panel1 = new StatusBarPanel { Text = "Ready", Width = 150, BorderStyle = StatusBarPanelBorderStyle.Sunken, Alignment = HorizontalAlignment.Left };
            var panel2 = new StatusBarPanel { Text = "Status: OK", Width = 200, BorderStyle = StatusBarPanelBorderStyle.Raised, Alignment = HorizontalAlignment.Left };
            var panel3 = new StatusBarPanel { Text = DateTime.Now.ToLongTimeString(), Width = 120, BorderStyle = StatusBarPanelBorderStyle.None, Alignment = HorizontalAlignment.Right };

            statusBar.Panels.Add(panel1);
            statusBar.Panels.Add(panel2);
            statusBar.Panels.Add(panel3);

            this.Controls.Add(statusBar);

            timer = new Timer { Interval = 1000, Enabled = true };
            timer.Tick += (s, e) =>
            {
                if (statusBar.Panels.Count > 2)
                    statusBar.Panels[2].Text = DateTime.Now.ToLongTimeString();
            };
        }
        #endregion

        #region Menu
        private void SetupMenu()
        {
            var mainMenu = new MainMenu();

            var fileMenu = new MenuItem("&File");
            fileMenu.MenuItems.Add(new MenuItem("&New", (s, e) => UpdateStatus("New file created")));
            fileMenu.MenuItems.Add(new MenuItem("&Open", (s, e) =>
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                    UpdateStatus($"File selected: {ofd.FileName}");
            }));
            fileMenu.MenuItems.Add(new MenuItem("-"));
            fileMenu.MenuItems.Add(new MenuItem("&Save", (s, e) => UpdateStatus("File saved")));
            fileMenu.MenuItems.Add(new MenuItem("-"));
            fileMenu.MenuItems.Add(new MenuItem("E&xit", (s, e) => this.Close()));

            var editMenu = new MenuItem("&Edit");
            editMenu.MenuItems.Add(new MenuItem("&Cut", (s, e) => UpdateStatus("Cut")));
            editMenu.MenuItems.Add(new MenuItem("&Copy", (s, e) => UpdateStatus("Copy")));
            editMenu.MenuItems.Add(new MenuItem("&Paste", (s, e) => UpdateStatus("Paste")));

            var helpMenu = new MenuItem("&Help");
            helpMenu.MenuItems.Add(new MenuItem("&About", (s, e) =>
                MessageBox.Show(
                    "ThemeModernFlat Demo v2.0\n\n" +
                    "Modern design for Mono WinForms\n" +
                    "Cross-platform style with animations\n" +
                    "Flat design with shadows and rounded corners\n\n" +
                    $"Platform: {Environment.OSVersion}\n" +
                    $".NET: {Environment.Version}",
                    "About",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )
            ));

            mainMenu.MenuItems.Add(fileMenu);
            mainMenu.MenuItems.Add(editMenu);
            mainMenu.MenuItems.Add(helpMenu);

            this.Menu = mainMenu;
        }
        #endregion

        #region Helper Methods
        private Label CreateHeader(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 15, 0, 5)
            };
        }

        private Panel CreateSpacer()
        {
            return new Panel { Height = 8, Width = 10, BackColor = Color.Transparent };
        }

        private Image CreateSampleIcon()
        {
            var bmp = new Bitmap(20, 20);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawLine(pen, 3, 10, 8, 15);
                    g.DrawLine(pen, 8, 15, 17, 5);
                }
            }
            return bmp;
        }

        private Image CreateColoredIcon(Color color, string text)
        {
            var bmp = new Bitmap(32, 32);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, 2, 2, 28, 28);
                }

                using (var font = new Font("Segoe UI", 14F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(text, font, brush, new Rectangle(0, 0, 32, 32), sf);
                }
            }
            return bmp;
        }

        private void UpdateStatus(string message)
        {
            if (statusBar != null && statusBar.Panels.Count > 0)
            {
                statusBar.Panels[0].Text = message.Length > 50 ? message.Substring(0, 47) + "..." : message;
                statusBar.Refresh();
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer?.Dispose();
                tabControl?.Dispose();
                statusBar?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}