using _4Time.DataCore.Models;

namespace Time4SellersApp
{
    partial class AdminView
    {
        /// <summary>
        /// Index des in dgvEntries zuletzt ausgewählten Eintrags.
        /// null = es wurde kein bestehender Eintrag zum Bearbeiten geöffnet.
        /// </summary>
        private int? selectedBookingIndex = null;

        private TabControl tabControl;
        private TabPage tabUebersicht;

        // Übersicht-Controls
        private PictureBox pictureLogoUebersicht;
        private Label lblArbeitszeitHeute;
        private Label lblPausenzeitHeute;
        private Label lblUeberstundenHeute;
        private Label lblArbeitszeitWoche;
        private Label lblPausenzeitWoche;
        private Label lblUeberstundenWoche;
        private Button btnSettingsUebersicht;
        private List<Entry> allEntrys = [];
        private List<Category> allCategorys = [];

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label OTWeek;
        private Label PTWeek;
        private Label OTToday;
        private Label PTToday;
        private Label WTWeek;
        private Label WTToday;
        private Label loggedInAs;
        private Button Neuladen;
        private DateTimePicker dateTimePickerOverview;
        private TabPage Settings;
        private Label label6;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabUebersicht = new TabPage();
            dataGridView1 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            comboBox1 = new ComboBox();
            dateTimePickerOverview = new DateTimePicker();
            Neuladen = new Button();
            loggedInAs = new Label();
            OTWeek = new Label();
            PTWeek = new Label();
            OTToday = new Label();
            PTToday = new Label();
            WTWeek = new Label();
            WTToday = new Label();
            pictureLogoUebersicht = new PictureBox();
            lblArbeitszeitHeute = new Label();
            lblPausenzeitHeute = new Label();
            lblUeberstundenHeute = new Label();
            lblArbeitszeitWoche = new Label();
            lblPausenzeitWoche = new Label();
            lblUeberstundenWoche = new Label();
            btnSettingsUebersicht = new Button();
            Settings = new TabPage();
            label6 = new Label();
            tabControl.SuspendLayout();
            tabUebersicht.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogoUebersicht).BeginInit();
            Settings.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabUebersicht);
            tabControl.Controls.Add(Settings);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(474, 696);
            tabControl.TabIndex = 0;
            // 
            // tabUebersicht
            // 
            tabUebersicht.Controls.Add(dataGridView1);
            tabUebersicht.Controls.Add(comboBox1);
            tabUebersicht.Controls.Add(dateTimePickerOverview);
            tabUebersicht.Controls.Add(Neuladen);
            tabUebersicht.Controls.Add(loggedInAs);
            tabUebersicht.Controls.Add(OTWeek);
            tabUebersicht.Controls.Add(PTWeek);
            tabUebersicht.Controls.Add(OTToday);
            tabUebersicht.Controls.Add(PTToday);
            tabUebersicht.Controls.Add(WTWeek);
            tabUebersicht.Controls.Add(WTToday);
            tabUebersicht.Controls.Add(pictureLogoUebersicht);
            tabUebersicht.Controls.Add(lblArbeitszeitHeute);
            tabUebersicht.Controls.Add(lblPausenzeitHeute);
            tabUebersicht.Controls.Add(lblUeberstundenHeute);
            tabUebersicht.Controls.Add(lblArbeitszeitWoche);
            tabUebersicht.Controls.Add(lblPausenzeitWoche);
            tabUebersicht.Controls.Add(lblUeberstundenWoche);
            tabUebersicht.Controls.Add(btnSettingsUebersicht);
            tabUebersicht.Location = new Point(4, 24);
            tabUebersicht.Name = "tabUebersicht";
            tabUebersicht.Size = new Size(466, 668);
            tabUebersicht.TabIndex = 0;
            tabUebersicht.Text = "Übersicht";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dataGridView1.Location = new Point(18, 338);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(424, 291);
            dataGridView1.TabIndex = 27;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Start";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Ende";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Art";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Kommentar";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Dauer";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(108, 271);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 26;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectionChangeCommitted;
            // 
            // dateTimePickerOverview
            // 
            dateTimePickerOverview.Format = DateTimePickerFormat.Short;
            dateTimePickerOverview.Location = new Point(20, 152);
            dateTimePickerOverview.Name = "dateTimePickerOverview";
            dateTimePickerOverview.Size = new Size(101, 23);
            dateTimePickerOverview.TabIndex = 25;
            dateTimePickerOverview.Value = new DateTime(2025, 4, 28, 0, 0, 0, 0);
            dateTimePickerOverview.ValueChanged += UebersichtDTP_ValueChanged;
            // 
            // Neuladen
            // 
            Neuladen.Location = new Point(363, 635);
            Neuladen.Name = "Neuladen";
            Neuladen.Size = new Size(100, 30);
            Neuladen.TabIndex = 20;
            Neuladen.Text = "Neuladen";
            Neuladen.Click += Neuladen_Click;
            // 
            // loggedInAs
            // 
            loggedInAs.AutoSize = true;
            loggedInAs.Location = new Point(18, 274);
            loggedInAs.Name = "loggedInAs";
            loggedInAs.Size = new Size(84, 15);
            loggedInAs.TabIndex = 18;
            loggedInAs.Text = "Eingeloggt als:";
            // 
            // OTWeek
            // 
            OTWeek.AutoSize = true;
            OTWeek.Location = new Point(390, 249);
            OTWeek.Name = "OTWeek";
            OTWeek.Size = new Size(50, 15);
            OTWeek.TabIndex = 17;
            OTWeek.Text = "OTWeek";
            // 
            // PTWeek
            // 
            PTWeek.AutoSize = true;
            PTWeek.Location = new Point(390, 219);
            PTWeek.Name = "PTWeek";
            PTWeek.Size = new Size(49, 15);
            PTWeek.TabIndex = 16;
            PTWeek.Text = "PTWeek";
            // 
            // OTToday
            // 
            OTToday.AutoSize = true;
            OTToday.Location = new Point(129, 249);
            OTToday.Name = "OTToday";
            OTToday.Size = new Size(52, 15);
            OTToday.TabIndex = 15;
            OTToday.Text = "OTToday";
            // 
            // PTToday
            // 
            PTToday.AutoSize = true;
            PTToday.Location = new Point(129, 219);
            PTToday.Name = "PTToday";
            PTToday.Size = new Size(51, 15);
            PTToday.TabIndex = 14;
            PTToday.Text = "PTToday";
            // 
            // WTWeek
            // 
            WTWeek.AutoSize = true;
            WTWeek.Location = new Point(390, 191);
            WTWeek.Name = "WTWeek";
            WTWeek.Size = new Size(53, 15);
            WTWeek.TabIndex = 13;
            WTWeek.Text = "WTWeek";
            // 
            // WTToday
            // 
            WTToday.AutoSize = true;
            WTToday.Location = new Point(129, 191);
            WTToday.Name = "WTToday";
            WTToday.Size = new Size(55, 15);
            WTToday.TabIndex = 12;
            WTToday.Text = "WTToday";
            // 
            // pictureLogoUebersicht
            // 
            pictureLogoUebersicht.ImageLocation = "res/4TIMELogo.gif";
            pictureLogoUebersicht.Location = new Point(20, 14);
            pictureLogoUebersicht.Name = "pictureLogoUebersicht";
            pictureLogoUebersicht.Size = new Size(424, 122);
            pictureLogoUebersicht.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogoUebersicht.TabIndex = 0;
            pictureLogoUebersicht.TabStop = false;
            // 
            // lblArbeitszeitHeute
            // 
            lblArbeitszeitHeute.AutoSize = true;
            lblArbeitszeitHeute.Location = new Point(18, 191);
            lblArbeitszeitHeute.Name = "lblArbeitszeitHeute";
            lblArbeitszeitHeute.Size = new Size(100, 15);
            lblArbeitszeitHeute.TabIndex = 1;
            lblArbeitszeitHeute.Text = "Arbeitszeit Heute:";
            // 
            // lblPausenzeitHeute
            // 
            lblPausenzeitHeute.AutoSize = true;
            lblPausenzeitHeute.Location = new Point(18, 219);
            lblPausenzeitHeute.Name = "lblPausenzeitHeute";
            lblPausenzeitHeute.Size = new Size(101, 15);
            lblPausenzeitHeute.TabIndex = 2;
            lblPausenzeitHeute.Text = "Pausenzeit Heute:";
            // 
            // lblUeberstundenHeute
            // 
            lblUeberstundenHeute.AutoSize = true;
            lblUeberstundenHeute.Location = new Point(18, 249);
            lblUeberstundenHeute.Name = "lblUeberstundenHeute";
            lblUeberstundenHeute.Size = new Size(113, 15);
            lblUeberstundenHeute.TabIndex = 3;
            lblUeberstundenHeute.Text = "Überstunden Heute:";
            // 
            // lblArbeitszeitWoche
            // 
            lblArbeitszeitWoche.AutoSize = true;
            lblArbeitszeitWoche.Location = new Point(266, 191);
            lblArbeitszeitWoche.Name = "lblArbeitszeitWoche";
            lblArbeitszeitWoche.Size = new Size(105, 15);
            lblArbeitszeitWoche.TabIndex = 4;
            lblArbeitszeitWoche.Text = "Arbeitszeit Woche:";
            // 
            // lblPausenzeitWoche
            // 
            lblPausenzeitWoche.AutoSize = true;
            lblPausenzeitWoche.Location = new Point(266, 219);
            lblPausenzeitWoche.Name = "lblPausenzeitWoche";
            lblPausenzeitWoche.Size = new Size(106, 15);
            lblPausenzeitWoche.TabIndex = 5;
            lblPausenzeitWoche.Text = "Pausenzeit Woche:";
            // 
            // lblUeberstundenWoche
            // 
            lblUeberstundenWoche.AutoSize = true;
            lblUeberstundenWoche.Location = new Point(266, 249);
            lblUeberstundenWoche.Name = "lblUeberstundenWoche";
            lblUeberstundenWoche.Size = new Size(118, 15);
            lblUeberstundenWoche.TabIndex = 6;
            lblUeberstundenWoche.Text = "Überstunden Woche:";
            // 
            // btnSettingsUebersicht
            // 
            btnSettingsUebersicht.Location = new Point(3, 635);
            btnSettingsUebersicht.Name = "btnSettingsUebersicht";
            btnSettingsUebersicht.Size = new Size(100, 30);
            btnSettingsUebersicht.TabIndex = 8;
            btnSettingsUebersicht.Text = "Settings";
            btnSettingsUebersicht.Click += SettingsButton_Click;
            // 
            // Settings
            // 
            Settings.Controls.Add(label6);
            Settings.Location = new Point(4, 24);
            Settings.Name = "Settings";
            Settings.Padding = new Padding(3);
            Settings.Size = new Size(466, 668);
            Settings.TabIndex = 3;
            Settings.Text = "Settings";
            Settings.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 42F);
            label6.Location = new Point(-3, 226);
            label6.Name = "label6";
            label6.Size = new Size(473, 74);
            label6.TabIndex = 0;
            label6.Text = "COMMING SOON";
            // 
            // AdminView
            // 
            ClientSize = new Size(474, 696);
            Controls.Add(tabControl);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AdminView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "4TIME";
            tabControl.ResumeLayout(false);
            tabUebersicht.ResumeLayout(false);
            tabUebersicht.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogoUebersicht).EndInit();
            Settings.ResumeLayout(false);
            Settings.PerformLayout();
            ResumeLayout(false);
        }
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}
