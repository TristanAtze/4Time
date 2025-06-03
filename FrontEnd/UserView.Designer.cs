using _4Time.DataCore.Models;

namespace Time4SellersApp
{
    partial class UserView
    {
        /// <summary>
        /// Index des in dgvEntries zuletzt ausgewählten Eintrags.
        /// null = es wurde kein bestehender Eintrag zum Bearbeiten geöffnet.
        /// </summary>
        private int? selectedBookingIndex = null;

        private TabControl tabControl;
        private TabPage tabUebersicht;
        private TabPage tabEintragen;
        private TabPage tabAuslesen;

        // Übersicht-Controls
        private PictureBox pictureLogoUebersicht;
        private Label lblArbeitszeitHeute;
        private Label lblPausenzeitHeute;
        private Label lblUeberstundenHeute;
        private Label lblArbeitszeitWoche;
        private Label lblPausenzeitWoche;
        private Label lblUeberstundenWoche;
        private Label lblMy4SellersAusgabe;
        private RadioButton rbStartzeitEndzeit;
        private RadioButton rbStartzeitDauer;
        private RadioButton rbEndzeitDauer;
        private Label lblInfoEintragen;
        private TextBox txtBemerkung;
        private Label lblBemerkung;
        private Button btnSpeichern;
        private Button btnSettingsAuslesen;
        public Button btnNeuladenAuslesen;
        public DataGridView dgvEntries;
        private List<Entry> allEntrys = [];
        private List<Category> allCategorys = [];

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private DateTimePicker StartzeitEndzeitEnde;
        private DateTimePicker StartzeitEndzeitStart;
        private NumericUpDown StartzeitDauerMinuten;
        private NumericUpDown StartzeitDauerStunden;
        private DateTimePicker StartzeitDauerStart;
        private Label label1;
        private Label label3;
        private Label label4;
        private NumericUpDown EndzeitDauerMinuten;
        private NumericUpDown EndzeitDauerStunden;
        private DateTimePicker EndzeitDauerStart;
        private Label label2;
        private Label label5;
        private ComboBox BookingType;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Label OTWeek;
        private Label PTWeek;
        private Label OTToday;
        private Label PTToday;
        private Label WTWeek;
        private Label WTToday;
        private Label LogginName;
        private Label loggedInAs;
        private DataGridViewTextBoxColumn colStart;
        private DataGridViewTextBoxColumn colEnd;
        private DataGridViewTextBoxColumn colArt;
        private DataGridViewTextBoxColumn colKommentar;
        private DataGridViewTextBoxColumn colDauer;
        private Button Löschen;
        public Button Neuladen;
        private Label PauseLabel;
        private Label NachmittagLabel;
        private Label VormittagLabel;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePickerOverview;
        private TabPage tabSettings;


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
            label11 = new Label();
            PTMin = new Label();
            label10 = new Label();
            dateTimePickerOverview = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            PauseLabel = new Label();
            NachmittagLabel = new Label();
            VormittagLabel = new Label();
            Neuladen = new Button();
            LogginName = new Label();
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
            lblMy4SellersAusgabe = new Label();
            tabEintragen = new TabPage();
            button3 = new Button();
            button2 = new Button();
            pictureBox2 = new PictureBox();
            label5 = new Label();
            BookingType = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            EndzeitDauerMinuten = new NumericUpDown();
            EndzeitDauerStunden = new NumericUpDown();
            EndzeitDauerStart = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            StartzeitDauerMinuten = new NumericUpDown();
            StartzeitDauerStunden = new NumericUpDown();
            StartzeitDauerStart = new DateTimePicker();
            StartzeitEndzeitEnde = new DateTimePicker();
            StartzeitEndzeitStart = new DateTimePicker();
            rbStartzeitEndzeit = new RadioButton();
            rbStartzeitDauer = new RadioButton();
            rbEndzeitDauer = new RadioButton();
            lblInfoEintragen = new Label();
            lblBemerkung = new Label();
            txtBemerkung = new TextBox();
            btnSpeichern = new Button();
            tabAuslesen = new TabPage();
            Löschen = new Button();
            pictureBox1 = new PictureBox();
            btnSettingsAuslesen = new Button();
            btnNeuladenAuslesen = new Button();
            dgvEntries = new DataGridView();
            colStart = new DataGridViewTextBoxColumn();
            colEnd = new DataGridViewTextBoxColumn();
            colArt = new DataGridViewTextBoxColumn();
            colKommentar = new DataGridViewTextBoxColumn();
            colDauer = new DataGridViewTextBoxColumn();
            tabSettings = new TabPage();
            LockPcTime = new NumericUpDown();
            label13 = new Label();
            label12 = new Label();
            button1 = new Button();
            u18Description = new Label();
            checkBox2 = new CheckBox();
            u18 = new Label();
            label9 = new Label();
            label6 = new Label();
            checkBox1 = new CheckBox();
            Benach = new Label();
            label8 = new Label();
            LockTimeMin = new NumericUpDown();
            label7 = new Label();
            LockedTimeMin = new Label();
            tabControl.SuspendLayout();
            tabUebersicht.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogoUebersicht).BeginInit();
            tabEintragen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerMinuten).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerStunden).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerMinuten).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerStunden).BeginInit();
            tabAuslesen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEntries).BeginInit();
            tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LockPcTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LockTimeMin).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabUebersicht);
            tabControl.Controls.Add(tabEintragen);
            tabControl.Controls.Add(tabAuslesen);
            tabControl.Controls.Add(tabSettings);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(474, 561);
            tabControl.TabIndex = 0;
            tabControl.Selecting += tabControl_Selecting;
            // 
            // tabUebersicht
            // 
            tabUebersicht.Controls.Add(label11);
            tabUebersicht.Controls.Add(PTMin);
            tabUebersicht.Controls.Add(label10);
            tabUebersicht.Controls.Add(dateTimePickerOverview);
            tabUebersicht.Controls.Add(dateTimePicker1);
            tabUebersicht.Controls.Add(PauseLabel);
            tabUebersicht.Controls.Add(NachmittagLabel);
            tabUebersicht.Controls.Add(VormittagLabel);
            tabUebersicht.Controls.Add(Neuladen);
            tabUebersicht.Controls.Add(LogginName);
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
            tabUebersicht.Controls.Add(lblMy4SellersAusgabe);
            tabUebersicht.Location = new Point(4, 24);
            tabUebersicht.Name = "tabUebersicht";
            tabUebersicht.Size = new Size(466, 533);
            tabUebersicht.TabIndex = 0;
            tabUebersicht.Text = "Übersicht";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(399, 274);
            label11.Name = "label11";
            label11.Size = new Size(26, 15);
            label11.TabIndex = 28;
            label11.Text = "Uhr";
            // 
            // PTMin
            // 
            PTMin.AutoSize = true;
            PTMin.Location = new Point(352, 274);
            PTMin.Name = "PTMin";
            PTMin.Size = new Size(65, 15);
            PTMin.TabIndex = 27;
            PTMin.Text = "Berechne...";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(266, 274);
            label10.Name = "label10";
            label10.Size = new Size(90, 15);
            label10.TabIndex = 26;
            label10.Text = "Pausenzeit um: ";
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
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(338, 371);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(101, 23);
            dateTimePicker1.TabIndex = 24;
            dateTimePicker1.Value = new DateTime(2025, 4, 28, 0, 0, 0, 0);
            dateTimePicker1.ValueChanged += UebersichtDTP_ValueChanged;
            // 
            // PauseLabel
            // 
            PauseLabel.AutoSize = true;
            PauseLabel.Location = new Point(18, 433);
            PauseLabel.Name = "PauseLabel";
            PauseLabel.Size = new Size(76, 15);
            PauseLabel.TabIndex = 23;
            PauseLabel.Text = "Pause: Lädt...";
            // 
            // NachmittagLabel
            // 
            NachmittagLabel.AutoSize = true;
            NachmittagLabel.Location = new Point(18, 463);
            NachmittagLabel.Name = "NachmittagLabel";
            NachmittagLabel.Size = new Size(108, 15);
            NachmittagLabel.TabIndex = 22;
            NachmittagLabel.Text = "Nachmittag: Lädt...";
            // 
            // VormittagLabel
            // 
            VormittagLabel.AutoSize = true;
            VormittagLabel.Location = new Point(18, 405);
            VormittagLabel.Name = "VormittagLabel";
            VormittagLabel.Size = new Size(97, 15);
            VormittagLabel.TabIndex = 21;
            VormittagLabel.Text = "Vormittag: Lädt...";
            // 
            // Neuladen
            // 
            Neuladen.Location = new Point(363, 500);
            Neuladen.Name = "Neuladen";
            Neuladen.Size = new Size(100, 30);
            Neuladen.TabIndex = 20;
            Neuladen.Text = "Neuladen";
            Neuladen.Click += Neuladen_Click;
            // 
            // LogginName
            // 
            LogginName.AutoSize = true;
            LogginName.Location = new Point(108, 274);
            LogginName.Name = "LogginName";
            LogginName.Size = new Size(76, 15);
            LogginName.TabIndex = 19;
            LogginName.Text = "LogginName";
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
            OTWeek.Size = new Size(39, 15);
            OTWeek.TabIndex = 17;
            OTWeek.Text = "Lädt...";
            // 
            // PTWeek
            // 
            PTWeek.AutoSize = true;
            PTWeek.Location = new Point(390, 219);
            PTWeek.Name = "PTWeek";
            PTWeek.Size = new Size(39, 15);
            PTWeek.TabIndex = 16;
            PTWeek.Text = "Lädt...";
            // 
            // OTToday
            // 
            OTToday.AutoSize = true;
            OTToday.Location = new Point(129, 249);
            OTToday.Name = "OTToday";
            OTToday.Size = new Size(39, 15);
            OTToday.TabIndex = 15;
            OTToday.Text = "Lädt...";
            // 
            // PTToday
            // 
            PTToday.AutoSize = true;
            PTToday.Location = new Point(129, 219);
            PTToday.Name = "PTToday";
            PTToday.Size = new Size(39, 15);
            PTToday.TabIndex = 14;
            PTToday.Text = "Lädt...";
            // 
            // WTWeek
            // 
            WTWeek.AutoSize = true;
            WTWeek.Location = new Point(390, 191);
            WTWeek.Name = "WTWeek";
            WTWeek.Size = new Size(39, 15);
            WTWeek.TabIndex = 13;
            WTWeek.Text = "Lädt...";
            // 
            // WTToday
            // 
            WTToday.AutoSize = true;
            WTToday.Location = new Point(129, 191);
            WTToday.Name = "WTToday";
            WTToday.Size = new Size(39, 15);
            WTToday.TabIndex = 12;
            WTToday.Text = "Lädt...";
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
            // lblMy4SellersAusgabe
            // 
            lblMy4SellersAusgabe.AutoSize = true;
            lblMy4SellersAusgabe.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblMy4SellersAusgabe.Location = new Point(18, 373);
            lblMy4SellersAusgabe.Name = "lblMy4SellersAusgabe";
            lblMy4SellersAusgabe.Size = new Size(177, 20);
            lblMy4SellersAusgabe.TabIndex = 7;
            lblMy4SellersAusgabe.Text = "My 4Sellers Ausgabe";
            // 
            // tabEintragen
            // 
            tabEintragen.Controls.Add(button3);
            tabEintragen.Controls.Add(button2);
            tabEintragen.Controls.Add(pictureBox2);
            tabEintragen.Controls.Add(label5);
            tabEintragen.Controls.Add(BookingType);
            tabEintragen.Controls.Add(label3);
            tabEintragen.Controls.Add(label4);
            tabEintragen.Controls.Add(EndzeitDauerMinuten);
            tabEintragen.Controls.Add(EndzeitDauerStunden);
            tabEintragen.Controls.Add(EndzeitDauerStart);
            tabEintragen.Controls.Add(label2);
            tabEintragen.Controls.Add(label1);
            tabEintragen.Controls.Add(StartzeitDauerMinuten);
            tabEintragen.Controls.Add(StartzeitDauerStunden);
            tabEintragen.Controls.Add(StartzeitDauerStart);
            tabEintragen.Controls.Add(StartzeitEndzeitEnde);
            tabEintragen.Controls.Add(StartzeitEndzeitStart);
            tabEintragen.Controls.Add(rbStartzeitEndzeit);
            tabEintragen.Controls.Add(rbStartzeitDauer);
            tabEintragen.Controls.Add(rbEndzeitDauer);
            tabEintragen.Controls.Add(lblInfoEintragen);
            tabEintragen.Controls.Add(lblBemerkung);
            tabEintragen.Controls.Add(txtBemerkung);
            tabEintragen.Controls.Add(btnSpeichern);
            tabEintragen.Location = new Point(4, 24);
            tabEintragen.Name = "tabEintragen";
            tabEintragen.Size = new Size(466, 533);
            tabEintragen.TabIndex = 1;
            tabEintragen.Text = "Eintragen";
            // 
            // button3
            // 
            button3.Location = new Point(3, 502);
            button3.Name = "button3";
            button3.Size = new Size(133, 28);
            button3.TabIndex = 29;
            button3.Text = "Urlaub/Berufsschule";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(246, 185);
            button2.Name = "button2";
            button2.Size = new Size(200, 23);
            button2.TabIndex = 28;
            button2.Text = "Ende des letzten Eintrags";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.ImageLocation = "res/4TIMELogo.gif";
            pictureBox2.Location = new Point(20, 14);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(424, 122);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 27;
            pictureBox2.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 167);
            label5.Name = "label5";
            label5.Size = new Size(94, 15);
            label5.TabIndex = 26;
            label5.Text = "Art der Buchung";
            // 
            // BookingType
            // 
            BookingType.DropDownStyle = ComboBoxStyle.DropDownList;
            BookingType.FormattingEnabled = true;
            BookingType.Location = new Point(3, 185);
            BookingType.Name = "BookingType";
            BookingType.Size = new Size(207, 23);
            BookingType.TabIndex = 25;
            BookingType.SelectionChangeCommitted += BookingType_SelectionChangeCommitted;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(221, 371);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 24;
            label3.Text = "Stunden:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(346, 372);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 23;
            label4.Text = "Minuten:";
            // 
            // EndzeitDauerMinuten
            // 
            EndzeitDauerMinuten.Location = new Point(408, 370);
            EndzeitDauerMinuten.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            EndzeitDauerMinuten.Name = "EndzeitDauerMinuten";
            EndzeitDauerMinuten.Size = new Size(38, 23);
            EndzeitDauerMinuten.TabIndex = 22;
            // 
            // EndzeitDauerStunden
            // 
            EndzeitDauerStunden.Location = new Point(282, 369);
            EndzeitDauerStunden.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            EndzeitDauerStunden.Name = "EndzeitDauerStunden";
            EndzeitDauerStunden.Size = new Size(38, 23);
            EndzeitDauerStunden.TabIndex = 21;
            // 
            // EndzeitDauerStart
            // 
            EndzeitDauerStart.AllowDrop = true;
            EndzeitDauerStart.CustomFormat = "HH:mm";
            EndzeitDauerStart.Format = DateTimePickerFormat.Custom;
            EndzeitDauerStart.Location = new Point(3, 369);
            EndzeitDauerStart.Name = "EndzeitDauerStart";
            EndzeitDauerStart.Size = new Size(200, 23);
            EndzeitDauerStart.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(221, 309);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 19;
            label2.Text = "Stunden:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(346, 307);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 18;
            label1.Text = "Minuten:";
            // 
            // StartzeitDauerMinuten
            // 
            StartzeitDauerMinuten.Location = new Point(407, 307);
            StartzeitDauerMinuten.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            StartzeitDauerMinuten.Name = "StartzeitDauerMinuten";
            StartzeitDauerMinuten.Size = new Size(38, 23);
            StartzeitDauerMinuten.TabIndex = 17;
            // 
            // StartzeitDauerStunden
            // 
            StartzeitDauerStunden.Location = new Point(282, 307);
            StartzeitDauerStunden.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            StartzeitDauerStunden.Name = "StartzeitDauerStunden";
            StartzeitDauerStunden.Size = new Size(38, 23);
            StartzeitDauerStunden.TabIndex = 16;
            // 
            // StartzeitDauerStart
            // 
            StartzeitDauerStart.AllowDrop = true;
            StartzeitDauerStart.CustomFormat = "HH:mm";
            StartzeitDauerStart.Format = DateTimePickerFormat.Custom;
            StartzeitDauerStart.Location = new Point(3, 303);
            StartzeitDauerStart.Name = "StartzeitDauerStart";
            StartzeitDauerStart.Size = new Size(200, 23);
            StartzeitDauerStart.TabIndex = 15;
            // 
            // StartzeitEndzeitEnde
            // 
            StartzeitEndzeitEnde.AllowDrop = true;
            StartzeitEndzeitEnde.CustomFormat = "HH:mm";
            StartzeitEndzeitEnde.Format = DateTimePickerFormat.Custom;
            StartzeitEndzeitEnde.Location = new Point(246, 239);
            StartzeitEndzeitEnde.Name = "StartzeitEndzeitEnde";
            StartzeitEndzeitEnde.Size = new Size(200, 23);
            StartzeitEndzeitEnde.TabIndex = 14;
            // 
            // StartzeitEndzeitStart
            // 
            StartzeitEndzeitStart.AllowDrop = true;
            StartzeitEndzeitStart.CustomFormat = "HH:mm";
            StartzeitEndzeitStart.Format = DateTimePickerFormat.Custom;
            StartzeitEndzeitStart.Location = new Point(3, 239);
            StartzeitEndzeitStart.Name = "StartzeitEndzeitStart";
            StartzeitEndzeitStart.Size = new Size(200, 23);
            StartzeitEndzeitStart.TabIndex = 12;
            // 
            // rbStartzeitEndzeit
            // 
            rbStartzeitEndzeit.AutoSize = true;
            rbStartzeitEndzeit.Location = new Point(10, 214);
            rbStartzeitEndzeit.Name = "rbStartzeitEndzeit";
            rbStartzeitEndzeit.Size = new Size(116, 19);
            rbStartzeitEndzeit.TabIndex = 1;
            rbStartzeitEndzeit.Text = "Startzeit - Endzeit";
            rbStartzeitEndzeit.CheckedChanged += RbStartzeitEndzeit_CheckedChanged;
            // 
            // rbStartzeitDauer
            // 
            rbStartzeitDauer.AutoSize = true;
            rbStartzeitDauer.Location = new Point(10, 278);
            rbStartzeitDauer.Name = "rbStartzeitDauer";
            rbStartzeitDauer.Size = new Size(109, 19);
            rbStartzeitDauer.TabIndex = 2;
            rbStartzeitDauer.Text = "Startzeit - Dauer";
            rbStartzeitDauer.CheckedChanged += RbStartzeitDauer_CheckedChanged;
            // 
            // rbEndzeitDauer
            // 
            rbEndzeitDauer.AutoSize = true;
            rbEndzeitDauer.Location = new Point(10, 344);
            rbEndzeitDauer.Name = "rbEndzeitDauer";
            rbEndzeitDauer.Size = new Size(105, 19);
            rbEndzeitDauer.TabIndex = 3;
            rbEndzeitDauer.Text = "Endzeit - Dauer";
            rbEndzeitDauer.CheckedChanged += RbEndzeitDauer_CheckedChanged;
            // 
            // lblInfoEintragen
            // 
            lblInfoEintragen.AutoSize = true;
            lblInfoEintragen.Location = new Point(3, 402);
            lblInfoEintragen.Name = "lblInfoEintragen";
            lblInfoEintragen.Size = new Size(303, 15);
            lblInfoEintragen.TabIndex = 4;
            lblInfoEintragen.Text = "Info: Es kann nur eine der drei Optionen gewählt werden";
            // 
            // lblBemerkung
            // 
            lblBemerkung.AutoSize = true;
            lblBemerkung.Location = new Point(3, 453);
            lblBemerkung.Name = "lblBemerkung";
            lblBemerkung.Size = new Size(71, 15);
            lblBemerkung.TabIndex = 5;
            lblBemerkung.Text = "Bemerkung:";
            // 
            // txtBemerkung
            // 
            txtBemerkung.Location = new Point(19, 471);
            txtBemerkung.Name = "txtBemerkung";
            txtBemerkung.Size = new Size(425, 23);
            txtBemerkung.TabIndex = 6;
            // 
            // btnSpeichern
            // 
            btnSpeichern.Location = new Point(363, 500);
            btnSpeichern.Name = "btnSpeichern";
            btnSpeichern.Size = new Size(100, 30);
            btnSpeichern.TabIndex = 7;
            btnSpeichern.Text = "Speichern";
            btnSpeichern.Click += BtnSpeichern_Click;
            // 
            // tabAuslesen
            // 
            tabAuslesen.Controls.Add(Löschen);
            tabAuslesen.Controls.Add(pictureBox1);
            tabAuslesen.Controls.Add(btnSettingsAuslesen);
            tabAuslesen.Controls.Add(btnNeuladenAuslesen);
            tabAuslesen.Controls.Add(dgvEntries);
            tabAuslesen.Location = new Point(4, 24);
            tabAuslesen.Name = "tabAuslesen";
            tabAuslesen.Size = new Size(466, 533);
            tabAuslesen.TabIndex = 2;
            tabAuslesen.Text = "Auslesen";
            // 
            // Löschen
            // 
            Löschen.Location = new Point(190, 500);
            Löschen.Name = "Löschen";
            Löschen.Size = new Size(100, 30);
            Löschen.TabIndex = 5;
            Löschen.Text = "Löschen";
            Löschen.Click += Löschen_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.ImageLocation = "res/4TIMELogo.gif";
            pictureBox1.Location = new Point(20, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(424, 122);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnSettingsAuslesen
            // 
            btnSettingsAuslesen.Location = new Point(3, 500);
            btnSettingsAuslesen.Name = "btnSettingsAuslesen";
            btnSettingsAuslesen.Size = new Size(100, 30);
            btnSettingsAuslesen.TabIndex = 1;
            btnSettingsAuslesen.Text = "Settings";
            btnSettingsAuslesen.Click += SettingsButton_Click;
            // 
            // btnNeuladenAuslesen
            // 
            btnNeuladenAuslesen.Location = new Point(363, 500);
            btnNeuladenAuslesen.Name = "btnNeuladenAuslesen";
            btnNeuladenAuslesen.Size = new Size(100, 30);
            btnNeuladenAuslesen.TabIndex = 2;
            btnNeuladenAuslesen.Text = "Neuladen";
            btnNeuladenAuslesen.Click += Neuladen_Click;
            // 
            // dgvEntries
            // 
            dgvEntries.AllowUserToAddRows = false;
            dgvEntries.AllowUserToDeleteRows = false;
            dgvEntries.AllowUserToResizeColumns = false;
            dgvEntries.AllowUserToResizeRows = false;
            dgvEntries.Columns.AddRange(new DataGridViewColumn[] { colStart, colEnd, colArt, colKommentar, colDauer });
            dgvEntries.Location = new Point(20, 142);
            dgvEntries.Name = "dgvEntries";
            dgvEntries.ReadOnly = true;
            dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntries.Size = new Size(424, 300);
            dgvEntries.TabIndex = 4;
            dgvEntries.CellDoubleClick += DgvEntries_CellDoubleClick;
            // 
            // colStart
            // 
            colStart.Name = "colStart";
            colStart.ReadOnly = true;
            // 
            // colEnd
            // 
            colEnd.Name = "colEnd";
            colEnd.ReadOnly = true;
            // 
            // colArt
            // 
            colArt.Name = "colArt";
            colArt.ReadOnly = true;
            // 
            // colKommentar
            // 
            colKommentar.Name = "colKommentar";
            colKommentar.ReadOnly = true;
            // 
            // colDauer
            // 
            colDauer.Name = "colDauer";
            colDauer.ReadOnly = true;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(LockPcTime);
            tabSettings.Controls.Add(label13);
            tabSettings.Controls.Add(label12);
            tabSettings.Controls.Add(button1);
            tabSettings.Controls.Add(u18Description);
            tabSettings.Controls.Add(checkBox2);
            tabSettings.Controls.Add(u18);
            tabSettings.Controls.Add(label9);
            tabSettings.Controls.Add(label6);
            tabSettings.Controls.Add(checkBox1);
            tabSettings.Controls.Add(Benach);
            tabSettings.Controls.Add(label8);
            tabSettings.Controls.Add(LockTimeMin);
            tabSettings.Controls.Add(label7);
            tabSettings.Controls.Add(LockedTimeMin);
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(466, 533);
            tabSettings.TabIndex = 3;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // LockPcTime
            // 
            LockPcTime.Font = new Font("Segoe UI", 10F);
            LockPcTime.Location = new Point(196, 221);
            LockPcTime.Name = "LockPcTime";
            LockPcTime.Size = new Size(41, 25);
            LockPcTime.TabIndex = 15;
            LockPcTime.ValueChanged += LockPcTime_ValueChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(8, 249);
            label13.Name = "label13";
            label13.Size = new Size(391, 15);
            label13.TabIndex = 14;
            label13.Text = "Bestimmt nach welcher inaktivitäts Zeit der Pc automatisch gesperrt wird";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F);
            label12.Location = new Point(8, 217);
            label12.Name = "label12";
            label12.Size = new Size(190, 32);
            label12.TabIndex = 13;
            label12.Text = "Pc Sperren nach:";
            // 
            // button1
            // 
            button1.Location = new Point(5, 489);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 12;
            button1.Text = "Dad joke";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // u18Description
            // 
            u18Description.AutoSize = true;
            u18Description.Font = new Font("Segoe UI", 9F);
            u18Description.Location = new Point(8, 186);
            u18Description.Name = "u18Description";
            u18Description.Size = new Size(356, 15);
            u18Description.TabIndex = 11;
            u18Description.Text = "Dies bestimmt gesetzliche Regelungen bezüglich der Pausenzeiten";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 18F);
            checkBox2.Location = new Point(214, 169);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 10;
            checkBox2.Tag = "";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // u18
            // 
            u18.AutoSize = true;
            u18.Font = new Font("Segoe UI", 18F);
            u18.Location = new Point(8, 154);
            u18.Name = "u18";
            u18.Size = new Size(200, 32);
            u18.TabIndex = 9;
            u18.Text = "Älter als 18 Jahre:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(5, 515);
            label9.Name = "label9";
            label9.Size = new Size(453, 15);
            label9.TabIndex = 8;
            label9.Text = "Manche Einstellungen werden erst nach einem Neustart aktiv. Klicke zum Neustarten";
            label9.Click += label9_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(8, 112);
            label6.Name = "label6";
            label6.Size = new Size(421, 15);
            label6.TabIndex = 7;
            label6.Text = "10 min vor bevor du zu lange garbeitet hast eine Benachrichtigung bekommen";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 18F);
            checkBox1.Location = new Point(347, 95);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 6;
            checkBox1.Tag = "";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Benach
            // 
            Benach.AutoSize = true;
            Benach.Font = new Font("Segoe UI", 18F);
            Benach.Location = new Point(8, 80);
            Benach.Name = "Benach";
            Benach.Size = new Size(344, 32);
            Benach.TabIndex = 5;
            Benach.Text = "10 min Reminder (Plichtpause):";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 18F);
            label8.Location = new Point(294, 12);
            label8.Name = "label8";
            label8.Size = new Size(56, 32);
            label8.TabIndex = 4;
            label8.Text = "Min";
            // 
            // LockTimeMin
            // 
            LockTimeMin.Font = new Font("Segoe UI", 10F);
            LockTimeMin.Location = new Point(247, 19);
            LockTimeMin.Name = "LockTimeMin";
            LockTimeMin.Size = new Size(41, 25);
            LockTimeMin.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(8, 44);
            label7.Name = "label7";
            label7.Size = new Size(327, 15);
            label7.TabIndex = 2;
            label7.Text = "Die Sperrzeit ab der eine Desktop Benachrichtigung erscheint";
            // 
            // LockedTimeMin
            // 
            LockedTimeMin.AutoSize = true;
            LockedTimeMin.Font = new Font("Segoe UI", 18F);
            LockedTimeMin.Location = new Point(8, 12);
            LockedTimeMin.Name = "LockedTimeMin";
            LockedTimeMin.Size = new Size(243, 32);
            LockedTimeMin.TabIndex = 1;
            LockedTimeMin.Text = "Pc Sperrzeit Schwelle:";
            // 
            // UserView
            // 
            ClientSize = new Size(474, 561);
            Controls.Add(tabControl);
            MaximizeBox = false;
            Name = "UserView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "4TIME";
            FormClosed += UserView_FormClosed;
            tabControl.ResumeLayout(false);
            tabUebersicht.ResumeLayout(false);
            tabUebersicht.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogoUebersicht).EndInit();
            tabEintragen.ResumeLayout(false);
            tabEintragen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerMinuten).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerStunden).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerMinuten).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerStunden).EndInit();
            tabAuslesen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEntries).EndInit();
            tabSettings.ResumeLayout(false);
            tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LockPcTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)LockTimeMin).EndInit();
            ResumeLayout(false);
        }
        private Label label7;
        private Label LockedTimeMin;
        private Label label8;
        private NumericUpDown LockTimeMin;
        private Label Benach;
        private CheckBox checkBox1;
        private Label label6;
        private Label label9;
        private Label u18Description;
        private CheckBox checkBox2;
        private Label u18;
        private Button button1;
        private Label label10;
        private Label PTMin;
        private Button button2;
        private Label label11;
        private Button button3;
        private NumericUpDown LockPcTime;
        private Label label13;
        private Label label12;
    }
}
