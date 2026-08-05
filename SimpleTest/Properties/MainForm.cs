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
        private TabPage tabData;
        private TabPage tabDialogs;
        private StatusBar statusBar;
        private Timer timer;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStrip toolStrip;
        private MenuStrip menuStrip;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "ThemeModernFlat Demo - All Controls";
            this.Size = new Size(1200, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(248, 249, 250);

            InitializeNotifyIcon();

            // Create MenuStrip
            SetupMenuStrip();

            // Create ToolStrip
            SetupToolStrip();

            // Create StatusBar
            SetupStatusBar();

            // Create ContextMenuStrip
            SetupContextMenuStrip();

            // Main layout using TableLayoutPanel
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };

            // Row styles
            mainLayout.RowStyles.Clear();
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28)); // MenuStrip height
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28)); // ToolStrip height
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // TabControl
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24)); // StatusBar height

            // Create TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F),
                Padding = new Point(8, 5),
                ItemSize = new Size(100, 32),
                SizeMode = TabSizeMode.Fixed,
                HotTrack = true
            };

            // Create TabPages
            tabButtons = new TabPage("Buttons");
            tabInputs = new TabPage("Inputs");
            tabContainers = new TabPage("Containers");
            tabProgress = new TabPage("Progress");
            tabLists = new TabPage("Lists");
            tabMenus = new TabPage("Menu & Status");
            tabAdvanced = new TabPage("Advanced");
            tabData = new TabPage("Data");
            tabDialogs = new TabPage("Dialogs");

            tabControl.TabPages.AddRange(new TabPage[]
            {
                tabButtons, tabInputs, tabContainers, tabProgress,
                tabLists, tabMenus, tabAdvanced, tabData, tabDialogs
            });

            // Setup tab contents
            SetupButtonsTab();
            SetupInputsTab();
            SetupContainersTab();
            SetupProgressTab();
            SetupListsTab();
            SetupMenusTab();
            SetupAdvancedTab();
            SetupDataTab();
            SetupDialogsTab();

            // Add controls to layout
            mainLayout.Controls.Add(menuStrip, 0, 0);
            mainLayout.Controls.Add(toolStrip, 0, 1);
            mainLayout.Controls.Add(tabControl, 0, 2);
            mainLayout.Controls.Add(statusBar, 0, 3);

            this.Controls.Add(mainLayout);
            this.MainMenuStrip = menuStrip;
        }

        #region NotifyIcon

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "ThemeModernFlat Demo",
                Visible = true
            };

            var notifyMenu = new ContextMenuStrip();
            notifyMenu.Items.Add("Show", null, (s, e) =>
            {
                this.WindowState = FormWindowState.Normal;
                this.Show();
            });
            notifyMenu.Items.Add("Hide", null, (s, e) => { this.Hide(); });
            notifyMenu.Items.Add("-");
            notifyMenu.Items.Add("Exit", null, (s, e) =>
            {
                notifyIcon.Visible = false;
                Application.Exit();
            });

            notifyIcon.ContextMenuStrip = notifyMenu;
            notifyIcon.DoubleClick += (s, e) =>
            {
                this.WindowState = FormWindowState.Normal;
                this.Show();
            };

            notifyIcon.ShowBalloonTip(3000, "ThemeModernFlat", "Application is running in system tray",
                ToolTipIcon.Info);
        }

        #endregion

        #region MenuStrip

        private void SetupMenuStrip()
        {
            menuStrip = new MenuStrip
            {
                Font = new Font("Segoe UI", 9.5F),
                BackColor = Color.White,
                GripStyle = ToolStripGripStyle.Visible
            };

            var fileMenu = new ToolStripMenuItem("&File");
            fileMenu.DropDownItems.Add("&New", null, (s, e) => UpdateStatus("New file created"));
            fileMenu.DropDownItems.Add("&Open", null, (s, e) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        UpdateStatus($"File selected: {ofd.FileName}");
                }
            });
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("&Save", null, (s, e) => UpdateStatus("File saved"));
            fileMenu.DropDownItems.Add("Save &As", null, (s, e) =>
            {
                using (var sfd = new SaveFileDialog())
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                        UpdateStatus($"File saved as: {sfd.FileName}");
                }
            });
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("E&xit", null, (s, e) => this.Close());

            var editMenu = new ToolStripMenuItem("&Edit");
            editMenu.DropDownItems.Add("&Undo", null, (s, e) => UpdateStatus("Undo"));
            editMenu.DropDownItems.Add("&Redo", null, (s, e) => UpdateStatus("Redo"));
            editMenu.DropDownItems.Add(new ToolStripSeparator());
            editMenu.DropDownItems.Add("&Cut", null, (s, e) => UpdateStatus("Cut"));
            editMenu.DropDownItems.Add("&Copy", null, (s, e) => UpdateStatus("Copy"));
            editMenu.DropDownItems.Add("&Paste", null, (s, e) => UpdateStatus("Paste"));

            var viewMenu = new ToolStripMenuItem("&View");
            viewMenu.DropDownItems.Add("&Toolbar", null, (s, e) =>
            {
                toolStrip.Visible = !toolStrip.Visible;
                UpdateStatus($"Toolbar: {(toolStrip.Visible ? "Visible" : "Hidden")}");
            });
            viewMenu.DropDownItems.Add("&Status Bar", null, (s, e) =>
            {
                statusBar.Visible = !statusBar.Visible;
                UpdateStatus($"Status Bar: {(statusBar.Visible ? "Visible" : "Hidden")}");
            });

            var toolsMenu = new ToolStripMenuItem("&Tools");
            toolsMenu.DropDownItems.Add("&Options", null, (s, e) =>
            {
                using (var dlg = new Form())
                {
                    dlg.Text = "Options Dialog";
                    dlg.Size = new Size(400, 300);
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                    dlg.MaximizeBox = false;
                    dlg.MinimizeBox = false;

                    var panel = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        Padding = new Padding(20),
                        FlowDirection = FlowDirection.TopDown
                    };

                    panel.Controls.Add(new Label
                    {
                        Text = "Application Settings:", Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        AutoSize = true
                    });
                    panel.Controls.Add(new CheckBox { Text = "Enable animations", AutoSize = true, Checked = true });
                    panel.Controls.Add(new CheckBox { Text = "Show tooltips", AutoSize = true, Checked = true });
                    panel.Controls.Add(new CheckBox { Text = "Auto-save", AutoSize = true });

                    var btnPanel = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 50,
                        FlowDirection = FlowDirection.RightToLeft,
                        Padding = new Padding(10)
                    };

                    var okBtn = new Button { Text = "OK", Width = 90, Height = 32, DialogResult = DialogResult.OK };
                    var cancelBtn = new Button
                        { Text = "Cancel", Width = 90, Height = 32, DialogResult = DialogResult.Cancel };

                    btnPanel.Controls.Add(okBtn);
                    btnPanel.Controls.Add(cancelBtn);

                    dlg.Controls.Add(panel);
                    dlg.Controls.Add(btnPanel);
                    dlg.AcceptButton = okBtn;
                    dlg.CancelButton = cancelBtn;

                    if (dlg.ShowDialog() == DialogResult.OK)
                        UpdateStatus("Settings saved");
                }
            });
            toolsMenu.DropDownItems.Add(new ToolStripSeparator());
            toolsMenu.DropDownItems.Add("&Notify Icon", null, (s, e) =>
            {
                notifyIcon.Visible = !notifyIcon.Visible;
                UpdateStatus($"Notify Icon: {(notifyIcon.Visible ? "Visible" : "Hidden")}");
            });

            var helpMenu = new ToolStripMenuItem("&Help");
            helpMenu.DropDownItems.Add("&Help", null, (s, e) => UpdateStatus("Help opened"));
            helpMenu.DropDownItems.Add(new ToolStripSeparator());
            helpMenu.DropDownItems.Add("&About", null, (s, e) =>
                MessageBox.Show(
                    "ThemeModernFlat Demo v3.0\n\n" +
                    "Complete Windows Forms controls demo\n" +
                    "Modern flat design with Material Design inspiration\n\n" +
                    $"Platform: {Environment.OSVersion}\n" +
                    $".NET: {Environment.Version}",
                    "About",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                ));

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(viewMenu);
            menuStrip.Items.Add(toolsMenu);
            menuStrip.Items.Add(helpMenu);

            this.Controls.Add(menuStrip);
        }

        #endregion

        #region ToolStrip

        private void SetupToolStrip()
        {
            toolStrip = new ToolStrip
            {
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.White,
                GripStyle = ToolStripGripStyle.Visible,
                RenderMode = ToolStripRenderMode.Professional
            };

            toolStrip.Items.Add(new ToolStripButton("New", null, (s, e) => UpdateStatus("New")));
            toolStrip.Items.Add(new ToolStripButton("Open", null, (s, e) => UpdateStatus("Open")));
            toolStrip.Items.Add(new ToolStripButton("Save", null, (s, e) => UpdateStatus("Save")));
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(new ToolStripButton("Cut", null, (s, e) => UpdateStatus("Cut")));
            toolStrip.Items.Add(new ToolStripButton("Copy", null, (s, e) => UpdateStatus("Copy")));
            toolStrip.Items.Add(new ToolStripButton("Paste", null, (s, e) => UpdateStatus("Paste")));
            toolStrip.Items.Add(new ToolStripSeparator());

            var dropdownBtn = new ToolStripDropDownButton("More");
            dropdownBtn.DropDownItems.Add("Action 1", null, (s, e) => UpdateStatus("Action 1"));
            dropdownBtn.DropDownItems.Add("Action 2", null, (s, e) => UpdateStatus("Action 2"));
            dropdownBtn.DropDownItems.Add(new ToolStripSeparator());
            dropdownBtn.DropDownItems.Add("Exit", null, (s, e) => this.Close());
            toolStrip.Items.Add(dropdownBtn);

            var comboBox = new ToolStripComboBox
            {
                Items = { "Option 1", "Option 2", "Option 3" },
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 120
            };
            comboBox.SelectedIndexChanged += (s, e) => UpdateStatus($"Selected: {comboBox.SelectedItem}");
            toolStrip.Items.Add(comboBox);

            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(new ToolStripLabel(" | Status: Ready"));

            this.Controls.Add(toolStrip);
        }

        #endregion

        #region ContextMenuStrip

        private void SetupContextMenuStrip()
        {
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Action 1", null, (s, e) => UpdateStatus("Context: Action 1"));
            contextMenuStrip.Items.Add("Action 2", null, (s, e) => UpdateStatus("Context: Action 2"));
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Properties", null, (s, e) => UpdateStatus("Context: Properties"));

            this.ContextMenuStrip = contextMenuStrip;
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

            var panel1 = new StatusBarPanel
            {
                Text = "Ready",
                Width = 180,
                BorderStyle = StatusBarPanelBorderStyle.Sunken,
                Alignment = HorizontalAlignment.Left
            };
            var panel2 = new StatusBarPanel
            {
                Text = "Status: OK",
                Width = 220,
                BorderStyle = StatusBarPanelBorderStyle.Raised,
                Alignment = HorizontalAlignment.Left
            };
            var panel3 = new StatusBarPanel
            {
                Text = DateTime.Now.ToLongTimeString(),
                Width = 150,
                BorderStyle = StatusBarPanelBorderStyle.None,
                Alignment = HorizontalAlignment.Right
            };

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

            // ===== STANDARD BUTTONS =====
            leftPanel.Controls.Add(CreateHeader("Standard Buttons"));
            leftPanel.Controls.Add(CreateSpacer());

            var btn1 = new Button
                { Text = "Normal Button", Size = new Size(160, 38), Font = new Font("Segoe UI", 10F) };
            btn1.Click += (s, e) => UpdateStatus("Normal button clicked");
            leftPanel.Controls.Add(btn1);

            var btn2 = new Button
                { Text = "Default Button", Size = new Size(160, 38), Font = new Font("Segoe UI", 10F) };
            btn2.Click += (s, e) => UpdateStatus("Default button clicked");
            leftPanel.Controls.Add(btn2);

            // ===== DIALOG BUTTONS =====
            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Dialog Buttons"));

            var dialogPanel = new FlowLayoutPanel
            {
                Width = 400,
                Height = 45,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0)
            };

            var okButton = new Button
            {
                Text = "OK",
                Size = new Size(90, 34),
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 10F)
            };
            okButton.Click += (s, e) => { UpdateStatus("OK clicked"); };

            var applyButton = new Button
            {
                Text = "Apply",
                Size = new Size(90, 34),
                Font = new Font("Segoe UI", 10F)
            };
            applyButton.Click += (s, e) => { UpdateStatus("Apply clicked"); };

            var cancelButton = new Button
            {
                Text = "Cancel",
                Size = new Size(90, 34),
                DialogResult = DialogResult.Cancel,
                Font = new Font("Segoe UI", 10F)
            };
            cancelButton.Click += (s, e) => { UpdateStatus("Cancel clicked"); };

            dialogPanel.Controls.Add(okButton);
            dialogPanel.Controls.Add(applyButton);
            dialogPanel.Controls.Add(cancelButton);
            leftPanel.Controls.Add(dialogPanel);

            // ===== FLAT BUTTONS =====
            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Flat Buttons"));

            var btn3 = new Button
            {
                Text = "Flat Button",
                Size = new Size(160, 38),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F)
            };
            btn3.Click += (s, e) => UpdateStatus("Flat button clicked");
            leftPanel.Controls.Add(btn3);

            var btn4 = new Button
            {
                Text = "Popup Button",
                Size = new Size(160, 38),
                FlatStyle = FlatStyle.Popup,
                Font = new Font("Segoe UI", 10F)
            };
            btn4.Click += (s, e) => UpdateStatus("Popup button clicked");
            leftPanel.Controls.Add(btn4);

            // ===== BUTTONS WITH IMAGES =====
            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Buttons with Images"));

            var btn5 = new Button
            {
                Text = "Open",
                Size = new Size(160, 38),
                Image = CreateSampleIcon(),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10F)
            };
            btn5.Click += (s, e) => UpdateStatus("Open button clicked");
            leftPanel.Controls.Add(btn5);

            var btnImageOnly = new Button
            {
                Size = new Size(50, 50),
                Image = CreateSampleIcon(),
                ImageAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(5, 5, 5, 5)
            };
            btnImageOnly.Click += (s, e) => UpdateStatus("Image only button clicked");
            leftPanel.Controls.Add(btnImageOnly);

            // ===== BLINKING BUTTONS =====
            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("Blinking Buttons"));

            var blinkTimer = new Timer { Interval = 500, Enabled = true };
            bool blinkState = false;

// Кнопка 1 - мигает красным/черным
            var blinkRedBtn = new Button
            {
                Text = "Blink Red",
                Size = new Size(160, 38),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Tag = "blink"
            };
            blinkRedBtn.Click += (s, e) => UpdateStatus("Blink Red clicked");
            leftPanel.Controls.Add(blinkRedBtn);

// Кнопка 2 - мигает синим/черным
            var blinkBlueBtn = new Button
            {
                Text = "Blink Blue",
                Size = new Size(160, 38),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Tag = "blink"
            };
            blinkBlueBtn.Click += (s, e) => UpdateStatus("Blink Blue clicked");
            leftPanel.Controls.Add(blinkBlueBtn);

// Кнопка 3 - мигает зеленым/черным
            var blinkGreenBtn = new Button
            {
                Text = "Blink Green",
                Size = new Size(160, 38),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Tag = "blink"
            };
            blinkGreenBtn.Click += (s, e) => UpdateStatus("Blink Green clicked");
            leftPanel.Controls.Add(blinkGreenBtn);

// Кнопка с иконкой - мигает красным/оранжевым
            var blinkIconBtn = new Button
            {
                Text = "   Alert",
                Size = new Size(160, 38),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Orange,
                Tag = "blink",
                Image = CreateWarningIcon(),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight
            };
            blinkIconBtn.Click += (s, e) => UpdateStatus("Blink Icon clicked");
            leftPanel.Controls.Add(blinkIconBtn);

// Сохраняем оригинальные цвета
            var origRedColor = blinkRedBtn.ForeColor;
            var origBlueColor = blinkBlueBtn.ForeColor;
            var origGreenColor = blinkGreenBtn.ForeColor;
            var origIconColor = blinkIconBtn.ForeColor;

            blinkTimer.Tick += (s, e) =>
            {
                blinkState = !blinkState;
                // Каждая кнопка мигает своим цветом
                blinkRedBtn.ForeColor = blinkState ? Color.Red : origRedColor;
                blinkBlueBtn.ForeColor = blinkState ? Color.FromArgb(15, 100, 210) : origBlueColor;
                blinkGreenBtn.ForeColor = blinkState ? Color.Green : origGreenColor;
                blinkIconBtn.ForeColor = blinkState ? Color.Red : origIconColor;
            };

            // ===== CHECKBOXES =====
            rightPanel.Controls.Add(CreateHeader("CheckBox"));

            var cb1 = new CheckBox
            {
                Text = "Standard CheckBox",
                Font = new Font("Segoe UI", 10F),
                Checked = true,
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 10)
            };
            cb1.CheckedChanged += (s, e) => UpdateStatus($"CheckBox: {(cb1.Checked ? "Checked" : "Unchecked")}");
            rightPanel.Controls.Add(cb1);

            var cb2 = new CheckBox
            {
                Text = "Three State CheckBox",
                Font = new Font("Segoe UI", 10F),
                ThreeState = true,
                CheckState = CheckState.Indeterminate,
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 10)
            };
            cb2.CheckStateChanged += (s, e) => UpdateStatus($"Three State: {cb2.CheckState}");
            rightPanel.Controls.Add(cb2);

            var cb3 = new CheckBox
            {
                Text = "Disabled CheckBox",
                Font = new Font("Segoe UI", 10F),
                Enabled = false,
                Checked = true,
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 10)
            };
            rightPanel.Controls.Add(cb3);

            // ===== RADIOBUTTONS =====
            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("RadioButton"));

            var rb1 = new RadioButton
            {
                Text = "Radio 1 - Active",
                Font = new Font("Segoe UI", 10F),
                Checked = true,
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 10)
            };
            rb1.CheckedChanged += (s, e) => UpdateStatus($"Selected: {rb1.Text}");
            rightPanel.Controls.Add(rb1);

            var rb2 = new RadioButton
            {
                Text = "Radio 2 - Inactive",
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 10)
            };
            rb2.CheckedChanged += (s, e) => UpdateStatus($"Selected: {rb2.Text}");
            rightPanel.Controls.Add(rb2);

            var rb3 = new RadioButton
            {
                Text = "Disabled Radio",
                Font = new Font("Segoe UI", 10F),
                Enabled = false,
                AutoSize = true,
                Margin = new Padding(5, 5, 5, 10)
            };
            rightPanel.Controls.Add(rb3);

            // ===== ADD PANELS TO TAB =====
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

            leftPanel.Controls.Add(CreateHeader("TextBox"));

            var tb1 = new TextBox { Text = "Standard TextBox", Width = 300, Font = new Font("Segoe UI", 10F) };
            leftPanel.Controls.Add(tb1);

            leftPanel.Controls.Add(CreateSpacer());

            var tb2 = new TextBox
            {
                Text = "Multiline TextBox\nWith support\nMultiple lines", Width = 300, Height = 80, Multiline = true,
                Font = new Font("Segoe UI", 10F)
            };
            leftPanel.Controls.Add(tb2);

            leftPanel.Controls.Add(CreateSpacer());

            var tb3 = new TextBox
                { Text = "secret123", Width = 300, Font = new Font("Segoe UI", 10F), PasswordChar = '●' };
            leftPanel.Controls.Add(tb3);

            leftPanel.Controls.Add(CreateSpacer());

            var tb4 = new TextBox
                { Text = "Disabled Field", Width = 300, Font = new Font("Segoe UI", 10F), Enabled = false };
            leftPanel.Controls.Add(tb4);

            rightPanel.Controls.Add(CreateHeader("ComboBox"));

            var cb1 = new ComboBox
                { Width = 250, Font = new Font("Segoe UI", 10F), DropDownStyle = ComboBoxStyle.DropDownList };
            cb1.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" });
            cb1.SelectedIndex = 0;
            cb1.SelectedIndexChanged += (s, e) => UpdateStatus($"Selected: {cb1.SelectedItem}");
            rightPanel.Controls.Add(cb1);

            rightPanel.Controls.Add(CreateSpacer());

            var cb2 = new ComboBox
                { Width = 250, Font = new Font("Segoe UI", 10F), DropDownStyle = ComboBoxStyle.DropDown };
            cb2.Items.AddRange(new object[] { "Editable", "ComboBox", "With text input" });
            cb2.Text = "Enter text...";
            rightPanel.Controls.Add(cb2);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("DateTimePicker"));

            var dtp = new DateTimePicker
            {
                Width = 250, Font = new Font("Segoe UI", 10F), Value = DateTime.Now, Format = DateTimePickerFormat.Long
            };
            rightPanel.Controls.Add(dtp);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("NumericUpDown"));

            var nud = new NumericUpDown
            {
                Width = 150, Font = new Font("Segoe UI", 10F), Minimum = 0, Maximum = 100, Value = 50,
                TextAlign = HorizontalAlignment.Center
            };
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

            var gb = new GroupBox
            {
                Text = "GroupBox with Controls", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F),
                Padding = new Padding(8)
            };
            var gbPanel = new FlowLayoutPanel
                { Dock = DockStyle.Fill, Padding = new Padding(10), FlowDirection = FlowDirection.TopDown };
            gbPanel.Controls.Add(new RadioButton { Text = "Option 1", AutoSize = true, Checked = true });
            gbPanel.Controls.Add(new RadioButton { Text = "Option 2", AutoSize = true });
            gbPanel.Controls.Add(new CheckBox { Text = "CheckBox in Group", AutoSize = true });
            gbPanel.Controls.Add(new Button
                { Text = "Action", Size = new Size(100, 28), Font = new Font("Segoe UI", 9F) });
            gb.Controls.Add(gbPanel);
            panel.Controls.Add(gb, 0, 0);

            var pnl1 = new Panel { Dock = DockStyle.Fill, BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White };
            var lbl1 = new Label
            {
                Text = "Panel with 3D border\n(modern flat style)", Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            pnl1.Controls.Add(lbl1);
            panel.Controls.Add(pnl1, 1, 0);

            var pnl2 = new Panel
                { Dock = DockStyle.Fill, BackColor = Color.FromArgb(240, 248, 255), BorderStyle = BorderStyle.None };
            var lbl2 = new Label
            {
                Text = "Panel without border\nwith colored background", Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            pnl2.Controls.Add(lbl2);
            panel.Controls.Add(pnl2, 0, 1);

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill, Orientation = Orientation.Vertical, BorderStyle = BorderStyle.Fixed3D,
                SplitterDistance = 170, SplitterWidth = 3, BackColor = Color.White
            };
            split.Panel1.BackColor = Color.FromArgb(240, 248, 255);
            split.Panel2.BackColor = Color.FromArgb(255, 240, 245);
            var lblLeft = new Label
            {
                Text = "Left Panel", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F)
            };
            split.Panel1.Controls.Add(lblLeft);
            var lblRight = new Label
            {
                Text = "Right Panel", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F)
            };
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

            leftPanel.Controls.Add(CreateHeader("ProgressBar"));

            var pb1 = new ProgressBar { Width = 400, Height = 28, Minimum = 0, Maximum = 100, Value = 70 };
            leftPanel.Controls.Add(pb1);

            leftPanel.Controls.Add(CreateSpacer());

            var pb2 = new ProgressBar
                { Width = 400, Height = 28, Style = ProgressBarStyle.Marquee, MarqueeAnimationSpeed = 25 };
            leftPanel.Controls.Add(pb2);

            leftPanel.Controls.Add(CreateSpacer());

            var btnPanel = new FlowLayoutPanel { Width = 400, FlowDirection = FlowDirection.LeftToRight };
            var btnUp = new Button { Text = "Increase", Size = new Size(120, 30), Font = new Font("Segoe UI", 9F) };
            btnUp.Click += (s, e) =>
            {
                pb1.Value = Math.Min(100, pb1.Value + 10);
                UpdateStatus($"Progress: {pb1.Value}%");
            };
            var btnDown = new Button { Text = "Decrease", Size = new Size(120, 30), Font = new Font("Segoe UI", 9F) };
            btnDown.Click += (s, e) =>
            {
                pb1.Value = Math.Max(0, pb1.Value - 10);
                UpdateStatus($"Progress: {pb1.Value}%");
            };
            var btnReset = new Button { Text = "Reset", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnReset.Click += (s, e) =>
            {
                pb1.Value = 0;
                UpdateStatus("Progress reset");
            };
            btnPanel.Controls.Add(btnUp);
            btnPanel.Controls.Add(btnDown);
            btnPanel.Controls.Add(btnReset);
            leftPanel.Controls.Add(btnPanel);

            leftPanel.Controls.Add(CreateSpacer());
            leftPanel.Controls.Add(CreateHeader("TrackBar (Horizontal)"));

            var tb1 = new TrackBar
            {
                Width = 400, Minimum = 0, Maximum = 100, Value = 50, TickFrequency = 10,
                TickStyle = TickStyle.BottomRight, LargeChange = 10
            };
            var lblTb1 = new Label { Text = "Value: 50", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            tb1.ValueChanged += (s, e) =>
            {
                lblTb1.Text = $"Value: {tb1.Value}";
                UpdateStatus($"TrackBar: {tb1.Value}");
            };
            leftPanel.Controls.Add(tb1);
            leftPanel.Controls.Add(lblTb1);

            rightPanel.Controls.Add(CreateHeader("TrackBar (Vertical)"));

            var tbV = new TrackBar
            {
                Height = 150, Width = 45, Orientation = Orientation.Vertical, Minimum = 0, Maximum = 100, Value = 75,
                TickFrequency = 10, TickStyle = TickStyle.Both
            };
            var lblTbV = new Label { Text = "Value: 75", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            tbV.ValueChanged += (s, e) =>
            {
                lblTbV.Text = $"Value: {tbV.Value}";
                UpdateStatus($"Vertical TrackBar: {tbV.Value}");
            };
            rightPanel.Controls.Add(tbV);
            rightPanel.Controls.Add(lblTbV);

            rightPanel.Controls.Add(CreateSpacer());
            rightPanel.Controls.Add(CreateHeader("ScrollBar (Horizontal)"));

            var hsb = new HScrollBar
                { Width = 350, Minimum = 0, Maximum = 100, Value = 30, LargeChange = 10, SmallChange = 1 };
            var lblHs = new Label { Text = "Scroll: 30", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            hsb.ValueChanged += (s, e) =>
            {
                lblHs.Text = $"Scroll: {hsb.Value}";
                UpdateStatus($"ScrollBar: {hsb.Value}");
            };
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

            var lbGroup = new GroupBox
                { Text = "ListBox", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F) };
            var lb = new ListBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
            for (int i = 1; i <= 8; i++) lb.Items.Add($"Item {i}");
            lb.SelectedIndexChanged += (s, e) =>
            {
                if (lb.SelectedItem != null) UpdateStatus($"Selected: {lb.SelectedItem}");
            };
            lbGroup.Controls.Add(lb);
            panel.Controls.Add(lbGroup, 0, 0);

            var clbGroup = new GroupBox
            {
                Text = "CheckedListBox", Dock = DockStyle.Fill, Padding = new Padding(8),
                Font = new Font("Segoe UI", 10F)
            };
            var clb = new CheckedListBox
                { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), CheckOnClick = true };
            for (int i = 1; i <= 5; i++) clb.Items.Add($"Item {i}");
            clb.SetItemChecked(0, true);
            clb.SetItemChecked(2, true);
            clb.SetItemChecked(4, true);
            clb.ItemCheck += (s, e) =>
                UpdateStatus($"{(e.NewValue == CheckState.Checked ? "Checked" : "Unchecked")}: {clb.Items[e.Index]}");
            clbGroup.Controls.Add(clb);
            panel.Controls.Add(clbGroup, 1, 0);

            var tvGroup = new GroupBox
            {
                Text = "TreeView", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F)
            };
            var tv = new TreeView { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };
            var root1 = tv.Nodes.Add("Root 1");
            root1.Nodes.Add("Child 1.1");
            root1.Nodes.Add("Child 1.2");
            root1.Nodes.Add("Child 1.3");
            var root2 = tv.Nodes.Add("Root 2");
            root2.Nodes.Add("Child 2.1");
            root2.Nodes.Add("Child 2.2");
            root2.Nodes.Add("Child 2.3");
            tv.AfterSelect += (s, e) =>
            {
                if (e.Node != null) UpdateStatus($"Selected: {e.Node.Text}");
            };
            tvGroup.Controls.Add(tv);
            panel.Controls.Add(tvGroup, 0, 1);

            var lvGroup = new GroupBox
            {
                Text = "ListView", Dock = DockStyle.Fill, Padding = new Padding(8), Font = new Font("Segoe UI", 10F)
            };
            var lv = new ListView
            {
                Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), View = View.Details, FullRowSelect = true,
                GridLines = true
            };
            lv.Columns.Add("Name", 120);
            lv.Columns.Add("Value", 120);
            lv.Columns.Add("Status", 100);
            lv.Columns.Add("Date", 120);
            var date = DateTime.Now;
            lv.Items.Add(new ListViewItem(new[] { "Item 1", "Value 1", "Active", date.ToShortDateString() }));
            lv.Items.Add(
                new ListViewItem(new[] { "Item 2", "Value 2", "Pending", date.AddDays(1).ToShortDateString() }));
            lv.Items.Add(new ListViewItem(new[] { "Item 3", "Value 3", "Done", date.AddDays(-1).ToShortDateString() }));
            lv.Items.Add(new ListViewItem(new[]
                { "Item 4", "Value 4", "Active", date.AddDays(2).ToShortDateString() }));
            lv.SelectedIndexChanged += (s, e) =>
            {
                if (lv.SelectedItems.Count > 0) UpdateStatus($"Selected: {lv.SelectedItems[0].SubItems[0].Text}");
            };
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

            var infoGroup = new GroupBox
                { Text = "Theme Information", Width = 420, Height = 200, Font = new Font("Segoe UI", 10F) };
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

            var ctxPanel = new Panel
            {
                Size = new Size(420, 80), BorderStyle = BorderStyle.Fixed3D, BackColor = Color.White,
                ContextMenu = ctxMenu
            };
            var ctxLabel = new Label
            {
                Text = "Right-click here for context menu", Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            ctxPanel.Controls.Add(ctxLabel);
            leftPanel.Controls.Add(ctxPanel);

            rightPanel.Controls.Add(CreateHeader("Status Demo"));

            var statusGroup = new GroupBox
                { Text = "Status Bar Panels", Width = 420, Height = 150, Font = new Font("Segoe UI", 10F) };
            var statusPanel = new FlowLayoutPanel
                { Dock = DockStyle.Fill, Padding = new Padding(10), FlowDirection = FlowDirection.TopDown };

            var statLabel = new Label
                { Text = "Current Status: System running", AutoSize = true, Font = new Font("Segoe UI", 10F) };
            statusPanel.Controls.Add(statLabel);

            var btnStatus = new Button
                { Text = "Change Status", Size = new Size(150, 30), Font = new Font("Segoe UI", 9F) };
            int statusIndex = 0;
            string[] statuses =
                { "System running", "Warning! Check data", "Error! Intervention required", "Operation in progress..." };
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

            var timerGroup = new GroupBox
                { Text = "Time in Status Bar", Width = 420, Height = 80, Font = new Font("Segoe UI", 10F) };
            var timerLabel = new Label
            {
                Text = "Current Time: " + DateTime.Now.ToLongTimeString(), Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 11F)
            };
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

            panel.Controls.Add(CreateHeader("RichTextBox"));
            var rtb = new RichTextBox
            {
                Width = 500,
                Height = 120,
                Font = new Font("Segoe UI", 10F),
                Text =
                    "RichTextBox with formatting support.\nCan insert text with different styles.\nSupports multiline input."
            };
            panel.Controls.Add(rtb);

            panel.Controls.Add(CreateSpacer());

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

        #region Data Tab

        private void SetupDataTab()
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

            // DataGridView
            var dgvGroup = new GroupBox
            {
                Text = "DataGridView",
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 10F)
            };

            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                RowHeadersVisible = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Name", "Name");
            dgv.Columns.Add("Department", "Department");
            dgv.Columns.Add("Salary", "Salary");

            dgv.Rows.Add("1", "John Smith", "IT", "75000");
            dgv.Rows.Add("2", "Jane Doe", "HR", "65000");
            dgv.Rows.Add("3", "Bob Johnson", "Finance", "85000");
            dgv.Rows.Add("4", "Alice Brown", "Marketing", "70000");
            dgv.Rows.Add("5", "Charlie Wilson", "IT", "80000");

            dgv.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var val = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    UpdateStatus($"Cell clicked: {val}");
                }
            };

            dgvGroup.Controls.Add(dgv);
            panel.Controls.Add(dgvGroup, 0, 0);

            // MonthCalendar
            var mcGroup = new GroupBox
            {
                Text = "MonthCalendar",
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 10F)
            };

            var mc = new MonthCalendar
            {
                Dock = DockStyle.Fill,
                ShowToday = true,
                ShowTodayCircle = true,
                ShowWeekNumbers = true,
                CalendarDimensions = new Size(1, 1),
                MaxSelectionCount = 7
            };

            mc.DateChanged += (s, e) => { UpdateStatus($"Date selected: {mc.SelectionStart.ToShortDateString()}"); };

            mcGroup.Controls.Add(mc);
            panel.Controls.Add(mcGroup, 1, 0);

            // DataGridView with CheckBox column
            var dgvCheckGroup = new GroupBox
            {
                Text = "DataGridView with CheckBoxes",
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 10F)
            };

            var dgvCheck = new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false
            };

            var checkCol = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Select",
                Name = "Select",
                Width = 60
            };
            dgvCheck.Columns.Add(checkCol);
            dgvCheck.Columns.Add("Task", "Task");
            dgvCheck.Columns.Add("Priority", "Priority");

            dgvCheck.Rows.Add(true, "Complete report", "High");
            dgvCheck.Rows.Add(false, "Review code", "Medium");
            dgvCheck.Rows.Add(true, "Test application", "High");
            dgvCheck.Rows.Add(false, "Deploy to production", "Low");

            dgvCheck.CellValueChanged += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var val = dgvCheck.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    UpdateStatus($"Checkbox changed: {((bool?)val == true ? "Checked" : "Unchecked")}");
                }
            };

            dgvCheckGroup.Controls.Add(dgvCheck);
            panel.Controls.Add(dgvCheckGroup, 0, 1);

            // PropertyGrid
            var pgGroup = new GroupBox
            {
                Text = "PropertyGrid",
                Dock = DockStyle.Fill,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 10F)
            };

            var pg = new PropertyGrid
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                ToolbarVisible = true,
                PropertySort = PropertySort.Categorized
            };

            pg.SelectedObject = new DemoObject();
            pg.PropertyValueChanged += (s, e) => { UpdateStatus($"Property changed: {e.ChangedItem.Label}"); };

            pgGroup.Controls.Add(pg);
            panel.Controls.Add(pgGroup, 1, 1);

            tabData.Controls.Add(panel);
        }

        private class DemoObject
        {
            [System.ComponentModel.Category("General")]
            public string Name { get; set; } = "Demo Object";

            [System.ComponentModel.Category("General")]
            public int Value { get; set; } = 100;

            [System.ComponentModel.Category("General")]
            public bool Enabled { get; set; } = true;

            [System.ComponentModel.Category("Appearance")]
            public Color BackgroundColor { get; set; } = Color.White;

            [System.ComponentModel.Category("Appearance")]
            public int Size { get; set; } = 50;

            [System.ComponentModel.Category("Data")]
            public DateTime CreatedDate { get; set; } = DateTime.Now;

            [System.ComponentModel.Category("Data")]
            public string Description { get; set; } = "Sample description";
        }

        #endregion

        #region Dialogs Tab

        private void SetupDialogsTab()
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                AutoScroll = true
            };

            panel.Controls.Add(CreateHeader("Standard Dialogs"));

            var btnOpenFile = new Button
                { Text = "Open File Dialog", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnOpenFile.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Title = "Select a file";
                    ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    ofd.Multiselect = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateStatus($"Selected: {string.Join(", ", ofd.FileNames)}");
                    }
                }
            };
            panel.Controls.Add(btnOpenFile);

            var btnSaveFile = new Button
                { Text = "Save File Dialog", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnSaveFile.Click += (s, e) =>
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Save file";
                    sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    sfd.DefaultExt = "txt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateStatus($"Saved: {sfd.FileName}");
                    }
                }
            };
            panel.Controls.Add(btnSaveFile);

            var btnFolder = new Button
                { Text = "Browse Folder", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnFolder.Click += (s, e) =>
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Select a folder";
                    fbd.ShowNewFolderButton = true;
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateStatus($"Folder selected: {fbd.SelectedPath}");
                    }
                }
            };
            panel.Controls.Add(btnFolder);

            var btnColor = new Button
                { Text = "Color Dialog", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnColor.Click += (s, e) =>
            {
                using (var cd = new ColorDialog())
                {
                    cd.AllowFullOpen = true;
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateStatus($"Color selected: {cd.Color.Name}");
                    }
                }
            };
            panel.Controls.Add(btnColor);

            var btnFont = new Button
                { Text = "Font Dialog", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnFont.Click += (s, e) =>
            {
                using (var fd = new FontDialog())
                {
                    fd.Font = this.Font;
                    fd.ShowColor = true;
                    fd.ShowEffects = true;
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateStatus($"Font selected: {fd.Font.Name}, Color: {fd.Color.Name}");
                    }
                }
            };
            panel.Controls.Add(btnFont);

            var btnPrint = new Button
                { Text = "Print Dialog", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnPrint.Click += (s, e) =>
            {
                using (var pd = new PrintDialog())
                {
                    pd.AllowSomePages = true;
                    pd.AllowSelection = true;
                    pd.AllowPrintToFile = true;
                    if (pd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateStatus("Print dialog confirmed");
                    }
                }
            };
            panel.Controls.Add(btnPrint);

            panel.Controls.Add(CreateSpacer());
            panel.Controls.Add(CreateHeader("Message Boxes"));

            var msgPanel = new FlowLayoutPanel
            {
                Width = 600,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            var btnInfo = new Button { Text = "Info", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnInfo.Click += (s, e) =>
                MessageBox.Show("Information message", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var btnWarning = new Button { Text = "Warning", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnWarning.Click += (s, e) =>
                MessageBox.Show("Warning message", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var btnError = new Button { Text = "Error", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnError.Click += (s, e) =>
                MessageBox.Show("Error message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var btnQuestion = new Button
                { Text = "Question", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnQuestion.Click += (s, e) =>
            {
                var result = MessageBox.Show("Continue operation?", "Question", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                UpdateStatus($"Question result: {result}");
            };

            var btnCustom = new Button { Text = "Custom", Size = new Size(100, 30), Font = new Font("Segoe UI", 9F) };
            btnCustom.Click += (s, e) =>
            {
                var result = MessageBox.Show("Custom message box with multiple options", "Custom Dialog",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                UpdateStatus($"Custom result: {result}");
            };

            msgPanel.Controls.Add(btnInfo);
            msgPanel.Controls.Add(btnWarning);
            msgPanel.Controls.Add(btnError);
            msgPanel.Controls.Add(btnQuestion);
            msgPanel.Controls.Add(btnCustom);
            panel.Controls.Add(msgPanel);

            panel.Controls.Add(CreateSpacer());
            panel.Controls.Add(CreateHeader("Custom Dialog (Input)"));

            var btnInputDialog = new Button
                { Text = "Show Input Dialog", Size = new Size(200, 36), Font = new Font("Segoe UI", 10F) };
            btnInputDialog.Click += (s, e) => ShowInputDialog();
            panel.Controls.Add(btnInputDialog);

            tabDialogs.Controls.Add(panel);
        }

        private Image CreateWarningIcon()
        {
            var bmp = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Треугольник предупреждения
                using (var pen = new Pen(Color.Red, 2))
                {
                    Point[] points = new Point[]
                    {
                        new Point(12, 2),
                        new Point(22, 20),
                        new Point(2, 20)
                    };
                    g.DrawPolygon(pen, points);

                    // Восклицательный знак
                    g.DrawLine(pen, 12, 8, 12, 14);
                    g.DrawRectangle(pen, 11, 16, 2, 2);
                }
            }

            return bmp;
        }

        private Image CreateSampleIcon()
        {
            var bmp = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (var pen = new Pen(Color.FromArgb(15, 100, 210), 2))
                {
                    // Стрелка вниз (как иконка открытия)
                    g.DrawLine(pen, 12, 4, 12, 18);
                    g.DrawLine(pen, 6, 12, 12, 18);
                    g.DrawLine(pen, 18, 12, 12, 18);

                    // Скобка сверху
                    g.DrawLine(pen, 4, 4, 8, 4);
                    g.DrawLine(pen, 16, 4, 20, 4);
                }
            }

            return bmp;
        }

        private void ShowInputDialog()
        {
            using (var dlg = new Form())
            {
                dlg.Text = "Input Dialog";
                dlg.Size = new Size(400, 180);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                var panel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20),
                    FlowDirection = FlowDirection.TopDown
                };

                panel.Controls.Add(new Label
                {
                    Text = "Enter your name:",
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true
                });

                var textBox = new TextBox
                {
                    Width = 340,
                    Font = new Font("Segoe UI", 10F),
                    Text = "John Doe"
                };
                panel.Controls.Add(textBox);

                var btnPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 50,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(10)
                };

                var okBtn = new Button { Text = "OK", Width = 90, Height = 32, DialogResult = DialogResult.OK };
                var cancelBtn = new Button
                    { Text = "Cancel", Width = 90, Height = 32, DialogResult = DialogResult.Cancel };

                btnPanel.Controls.Add(okBtn);
                btnPanel.Controls.Add(cancelBtn);

                dlg.Controls.Add(panel);
                dlg.Controls.Add(btnPanel);
                dlg.AcceptButton = okBtn;
                dlg.CancelButton = cancelBtn;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatus($"Input: {textBox.Text}");
                }
            }
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
                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
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
                notifyIcon?.Dispose();
                menuStrip?.Dispose();
                toolStrip?.Dispose();
                contextMenuStrip?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}