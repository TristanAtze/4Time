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
        private Label OTWeek;
        private Label PTWeek;
        private Label OTToday;
        private Label PTToday;
        private Label WTWeek;
        private Label WTToday;
        private Label LogginName;
        private Label loggedInAs;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserView));
            tabControl = new TabControl();
            tabUebersicht = new TabPage();
            pictureBox3 = new PictureBox();
            OTgesamt = new Label();
            label16 = new Label();
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
            lblArbeitszeitHeute = new Label();
            lblPausenzeitHeute = new Label();
            lblUeberstundenHeute = new Label();
            lblArbeitszeitWoche = new Label();
            lblPausenzeitWoche = new Label();
            lblUeberstundenWoche = new Label();
            lblMy4SellersAusgabe = new Label();
            tabEintragen = new TabPage();
            pictureBox2 = new PictureBox();
            button3 = new Button();
            button2 = new Button();
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
            lblBemerkung = new Label();
            txtBemerkung = new TextBox();
            btnSpeichern = new Button();
            tabAuslesen = new TabPage();
            pictureBox1 = new PictureBox();
            Löschen = new Button();
            btnSettingsAuslesen = new Button();
            btnNeuladenAuslesen = new Button();
            dgvEntries = new DataGridView();
            colStart = new DataGridViewTextBoxColumn();
            colEnd = new DataGridViewTextBoxColumn();
            colArt = new DataGridViewTextBoxColumn();
            colKommentar = new DataGridViewTextBoxColumn();
            colDauer = new DataGridViewTextBoxColumn();
            tabSettings = new TabPage();
            label28 = new Label();
            label27 = new Label();
            numericUpDownSecondsToLock = new NumericUpDown();
            button5 = new Button();
            label19 = new Label();
            label26 = new Label();
            FaceRegocnitionCheck = new CheckBox();
            label20 = new Label();
            label11 = new Label();
            textBox1 = new TextBox();
            txtOutputLog = new TextBox();
            label18 = new Label();
            SpeechToTextCheck = new CheckBox();
            button4 = new Button();
            label15 = new Label();
            autostartCheckBox = new CheckBox();
            label14 = new Label();
            LockPcTime = new NumericUpDown();
            label13 = new Label();
            label12 = new Label();
            button1 = new Button();
            u18Description = new Label();
            checkBox2 = new CheckBox();
            label9 = new Label();
            label6 = new Label();
            checkBox1 = new CheckBox();
            Benach = new Label();
            label8 = new Label();
            LockTimeMin = new NumericUpDown();
            label7 = new Label();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            u18 = new Label();
            label25 = new Label();
            label24 = new Label();
            label17 = new Label();
            LockedTimeMin = new Label();
            textBox2 = new TextBox();
            tabControl.SuspendLayout();
            tabUebersicht.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)numericUpDownSecondsToLock).BeginInit();
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
            tabControl.Size = new Size(890, 561);
            tabControl.TabIndex = 0;
            tabControl.Selecting += tabControl_Selecting;
            // 
            // tabUebersicht
            // 
            tabUebersicht.Controls.Add(pictureBox3);
            tabUebersicht.Controls.Add(OTgesamt);
            tabUebersicht.Controls.Add(label16);
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
            tabUebersicht.Controls.Add(lblArbeitszeitHeute);
            tabUebersicht.Controls.Add(lblPausenzeitHeute);
            tabUebersicht.Controls.Add(lblUeberstundenHeute);
            tabUebersicht.Controls.Add(lblArbeitszeitWoche);
            tabUebersicht.Controls.Add(lblPausenzeitWoche);
            tabUebersicht.Controls.Add(lblUeberstundenWoche);
            tabUebersicht.Controls.Add(lblMy4SellersAusgabe);
            tabUebersicht.Font = new Font("Segoe UI", 14F);
            tabUebersicht.Location = new Point(4, 24);
            tabUebersicht.Name = "tabUebersicht";
            tabUebersicht.Size = new Size(882, 533);
            tabUebersicht.TabIndex = 0;
            tabUebersicht.Text = "Übersicht";
            // 
            // pictureBox3
            // 
            pictureBox3.ImageLocation = "res/4TIMELogo.gif";
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(882, 150);
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.TabIndex = 31;
            pictureBox3.TabStop = false;
            // 
            // OTgesamt
            // 
            OTgesamt.AutoSize = true;
            OTgesamt.Font = new Font("Segoe UI", 14F);
            OTgesamt.Location = new Point(245, 287);
            OTgesamt.Name = "OTgesamt";
            OTgesamt.Size = new Size(60, 25);
            OTgesamt.TabIndex = 30;
            OTgesamt.Text = "Lädt...";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14F);
            label16.Location = new Point(22, 287);
            label16.Name = "label16";
            label16.Size = new Size(191, 25);
            label16.TabIndex = 29;
            label16.Text = "Überstunden gesamt:";
            // 
            // PTMin
            // 
            PTMin.AutoSize = true;
            PTMin.Font = new Font("Segoe UI", 14F);
            PTMin.Location = new Point(675, 274);
            PTMin.Name = "PTMin";
            PTMin.Size = new Size(103, 25);
            PTMin.TabIndex = 27;
            PTMin.Text = "Berechne...";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14F);
            label10.Location = new Point(445, 274);
            label10.Name = "label10";
            label10.Size = new Size(143, 25);
            label10.TabIndex = 26;
            label10.Text = "Pausenzeit um: ";
            // 
            // dateTimePickerOverview
            // 
            dateTimePickerOverview.Format = DateTimePickerFormat.Short;
            dateTimePickerOverview.Location = new Point(24, 156);
            dateTimePickerOverview.Name = "dateTimePickerOverview";
            dateTimePickerOverview.Size = new Size(148, 32);
            dateTimePickerOverview.TabIndex = 25;
            dateTimePickerOverview.Value = new DateTime(2025, 4, 28, 0, 0, 0, 0);
            dateTimePickerOverview.ValueChanged += UebersichtDTP_ValueChanged;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(445, 377);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(156, 32);
            dateTimePicker1.TabIndex = 24;
            dateTimePicker1.Value = new DateTime(2025, 4, 28, 0, 0, 0, 0);
            dateTimePicker1.ValueChanged += UebersichtDTP_ValueChanged;
            // 
            // PauseLabel
            // 
            PauseLabel.AutoSize = true;
            PauseLabel.Font = new Font("Segoe UI", 14F);
            PauseLabel.Location = new Point(142, 443);
            PauseLabel.Name = "PauseLabel";
            PauseLabel.Size = new Size(118, 25);
            PauseLabel.TabIndex = 23;
            PauseLabel.Text = "Pause: Lädt...";
            // 
            // NachmittagLabel
            // 
            NachmittagLabel.AutoSize = true;
            NachmittagLabel.Font = new Font("Segoe UI", 14F);
            NachmittagLabel.Location = new Point(140, 479);
            NachmittagLabel.Name = "NachmittagLabel";
            NachmittagLabel.Size = new Size(167, 25);
            NachmittagLabel.TabIndex = 22;
            NachmittagLabel.Text = "Nachmittag: Lädt...";
            // 
            // VormittagLabel
            // 
            VormittagLabel.AutoSize = true;
            VormittagLabel.Font = new Font("Segoe UI", 14F);
            VormittagLabel.Location = new Point(138, 408);
            VormittagLabel.Name = "VormittagLabel";
            VormittagLabel.Size = new Size(152, 25);
            VormittagLabel.TabIndex = 21;
            VormittagLabel.Text = "Vormittag: Lädt...";
            // 
            // Neuladen
            // 
            Neuladen.Location = new Point(710, 463);
            Neuladen.Name = "Neuladen";
            Neuladen.Size = new Size(164, 62);
            Neuladen.TabIndex = 20;
            Neuladen.Text = "Neuladen";
            Neuladen.Click += Neuladen_Click;
            // 
            // LogginName
            // 
            LogginName.AutoSize = true;
            LogginName.Font = new Font("Segoe UI", 14F);
            LogginName.Location = new Point(185, 333);
            LogginName.Name = "LogginName";
            LogginName.Size = new Size(120, 25);
            LogginName.TabIndex = 19;
            LogginName.Text = "LogginName";
            // 
            // loggedInAs
            // 
            loggedInAs.AutoSize = true;
            loggedInAs.Font = new Font("Segoe UI", 14F);
            loggedInAs.Location = new Point(22, 333);
            loggedInAs.Name = "loggedInAs";
            loggedInAs.Size = new Size(135, 25);
            loggedInAs.TabIndex = 18;
            loggedInAs.Text = "Eingeloggt als:";
            // 
            // OTWeek
            // 
            OTWeek.AutoSize = true;
            OTWeek.Font = new Font("Segoe UI", 14F);
            OTWeek.Location = new Point(675, 245);
            OTWeek.Name = "OTWeek";
            OTWeek.Size = new Size(60, 25);
            OTWeek.TabIndex = 17;
            OTWeek.Text = "Lädt...";
            // 
            // PTWeek
            // 
            PTWeek.AutoSize = true;
            PTWeek.Font = new Font("Segoe UI", 14F);
            PTWeek.Location = new Point(675, 216);
            PTWeek.Name = "PTWeek";
            PTWeek.Size = new Size(60, 25);
            PTWeek.TabIndex = 16;
            PTWeek.Text = "Lädt...";
            // 
            // OTToday
            // 
            OTToday.AutoSize = true;
            OTToday.Font = new Font("Segoe UI", 14F);
            OTToday.Location = new Point(245, 255);
            OTToday.Name = "OTToday";
            OTToday.Size = new Size(60, 25);
            OTToday.TabIndex = 15;
            OTToday.Text = "Lädt...";
            // 
            // PTToday
            // 
            PTToday.AutoSize = true;
            PTToday.Font = new Font("Segoe UI", 14F);
            PTToday.Location = new Point(245, 224);
            PTToday.Name = "PTToday";
            PTToday.Size = new Size(60, 25);
            PTToday.TabIndex = 14;
            PTToday.Text = "Lädt...";
            // 
            // WTWeek
            // 
            WTWeek.AutoSize = true;
            WTWeek.Font = new Font("Segoe UI", 14F);
            WTWeek.Location = new Point(675, 191);
            WTWeek.Name = "WTWeek";
            WTWeek.Size = new Size(60, 25);
            WTWeek.TabIndex = 13;
            WTWeek.Text = "Lädt...";
            // 
            // WTToday
            // 
            WTToday.AutoSize = true;
            WTToday.Font = new Font("Segoe UI", 14F);
            WTToday.Location = new Point(245, 191);
            WTToday.Name = "WTToday";
            WTToday.Size = new Size(60, 25);
            WTToday.TabIndex = 12;
            WTToday.Text = "Lädt...";
            // 
            // lblArbeitszeitHeute
            // 
            lblArbeitszeitHeute.AutoSize = true;
            lblArbeitszeitHeute.Font = new Font("Segoe UI", 14F);
            lblArbeitszeitHeute.Location = new Point(24, 191);
            lblArbeitszeitHeute.Name = "lblArbeitszeitHeute";
            lblArbeitszeitHeute.Size = new Size(160, 25);
            lblArbeitszeitHeute.TabIndex = 1;
            lblArbeitszeitHeute.Text = "Arbeitszeit Heute:";
            // 
            // lblPausenzeitHeute
            // 
            lblPausenzeitHeute.AutoSize = true;
            lblPausenzeitHeute.Font = new Font("Segoe UI", 14F);
            lblPausenzeitHeute.Location = new Point(24, 224);
            lblPausenzeitHeute.Name = "lblPausenzeitHeute";
            lblPausenzeitHeute.Size = new Size(161, 25);
            lblPausenzeitHeute.TabIndex = 2;
            lblPausenzeitHeute.Text = "Pausenzeit Heute:";
            // 
            // lblUeberstundenHeute
            // 
            lblUeberstundenHeute.AutoSize = true;
            lblUeberstundenHeute.Font = new Font("Segoe UI", 14F);
            lblUeberstundenHeute.Location = new Point(22, 255);
            lblUeberstundenHeute.Name = "lblUeberstundenHeute";
            lblUeberstundenHeute.Size = new Size(180, 25);
            lblUeberstundenHeute.TabIndex = 3;
            lblUeberstundenHeute.Text = "Überstunden Heute:";
            // 
            // lblArbeitszeitWoche
            // 
            lblArbeitszeitWoche.AutoSize = true;
            lblArbeitszeitWoche.Font = new Font("Segoe UI", 14F);
            lblArbeitszeitWoche.Location = new Point(445, 191);
            lblArbeitszeitWoche.Name = "lblArbeitszeitWoche";
            lblArbeitszeitWoche.Size = new Size(168, 25);
            lblArbeitszeitWoche.TabIndex = 4;
            lblArbeitszeitWoche.Text = "Arbeitszeit Woche:";
            // 
            // lblPausenzeitWoche
            // 
            lblPausenzeitWoche.AutoSize = true;
            lblPausenzeitWoche.Font = new Font("Segoe UI", 14F);
            lblPausenzeitWoche.Location = new Point(445, 219);
            lblPausenzeitWoche.Name = "lblPausenzeitWoche";
            lblPausenzeitWoche.Size = new Size(169, 25);
            lblPausenzeitWoche.TabIndex = 5;
            lblPausenzeitWoche.Text = "Pausenzeit Woche:";
            // 
            // lblUeberstundenWoche
            // 
            lblUeberstundenWoche.AutoSize = true;
            lblUeberstundenWoche.Font = new Font("Segoe UI", 14F);
            lblUeberstundenWoche.Location = new Point(445, 246);
            lblUeberstundenWoche.Name = "lblUeberstundenWoche";
            lblUeberstundenWoche.Size = new Size(188, 25);
            lblUeberstundenWoche.TabIndex = 6;
            lblUeberstundenWoche.Text = "Überstunden Woche:";
            // 
            // lblMy4SellersAusgabe
            // 
            lblMy4SellersAusgabe.AutoSize = true;
            lblMy4SellersAusgabe.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblMy4SellersAusgabe.Location = new Point(22, 374);
            lblMy4SellersAusgabe.Name = "lblMy4SellersAusgabe";
            lblMy4SellersAusgabe.Size = new Size(237, 26);
            lblMy4SellersAusgabe.TabIndex = 7;
            lblMy4SellersAusgabe.Text = "My 4Sellers Ausgabe";
            // 
            // tabEintragen
            // 
            tabEintragen.Controls.Add(pictureBox2);
            tabEintragen.Controls.Add(button3);
            tabEintragen.Controls.Add(button2);
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
            tabEintragen.Controls.Add(lblBemerkung);
            tabEintragen.Controls.Add(txtBemerkung);
            tabEintragen.Controls.Add(btnSpeichern);
            tabEintragen.Font = new Font("Segoe UI", 14F);
            tabEintragen.Location = new Point(4, 24);
            tabEintragen.Name = "tabEintragen";
            tabEintragen.Size = new Size(882, 533);
            tabEintragen.TabIndex = 1;
            tabEintragen.Text = "Eintragen";
            // 
            // pictureBox2
            // 
            pictureBox2.ImageLocation = "res/4TIMELogo.gif";
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(882, 150);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 32;
            pictureBox2.TabStop = false;
            // 
            // button3
            // 
            button3.Location = new Point(627, 185);
            button3.Name = "button3";
            button3.Size = new Size(247, 33);
            button3.TabIndex = 29;
            button3.Text = "Outlook-Import";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(627, 256);
            button2.Name = "button2";
            button2.Size = new Size(247, 43);
            button2.TabIndex = 28;
            button2.Text = "Ende des letzten Eintrags";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 153);
            label5.Name = "label5";
            label5.Size = new Size(150, 25);
            label5.TabIndex = 26;
            label5.Text = "Art der Buchung";
            // 
            // BookingType
            // 
            BookingType.DropDownStyle = ComboBoxStyle.DropDownList;
            BookingType.FormattingEnabled = true;
            BookingType.Location = new Point(8, 185);
            BookingType.Name = "BookingType";
            BookingType.Size = new Size(207, 33);
            BookingType.TabIndex = 25;
            BookingType.SelectionChangeCommitted += BookingType_SelectionChangeCommitted;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(272, 419);
            label3.Name = "label3";
            label3.Size = new Size(85, 25);
            label3.TabIndex = 24;
            label3.Text = "Stunden:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(468, 424);
            label4.Name = "label4";
            label4.Size = new Size(87, 25);
            label4.TabIndex = 23;
            label4.Text = "Minuten:";
            // 
            // EndzeitDauerMinuten
            // 
            EndzeitDauerMinuten.Location = new Point(561, 419);
            EndzeitDauerMinuten.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            EndzeitDauerMinuten.Name = "EndzeitDauerMinuten";
            EndzeitDauerMinuten.Size = new Size(38, 32);
            EndzeitDauerMinuten.TabIndex = 22;
            // 
            // EndzeitDauerStunden
            // 
            EndzeitDauerStunden.Location = new Point(363, 417);
            EndzeitDauerStunden.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            EndzeitDauerStunden.Name = "EndzeitDauerStunden";
            EndzeitDauerStunden.Size = new Size(38, 32);
            EndzeitDauerStunden.TabIndex = 21;
            // 
            // EndzeitDauerStart
            // 
            EndzeitDauerStart.AllowDrop = true;
            EndzeitDauerStart.CustomFormat = "HH:mm";
            EndzeitDauerStart.Format = DateTimePickerFormat.Custom;
            EndzeitDauerStart.Location = new Point(8, 418);
            EndzeitDauerStart.Name = "EndzeitDauerStart";
            EndzeitDauerStart.Size = new Size(200, 32);
            EndzeitDauerStart.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(272, 345);
            label2.Name = "label2";
            label2.Size = new Size(85, 25);
            label2.TabIndex = 19;
            label2.Text = "Stunden:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(468, 343);
            label1.Name = "label1";
            label1.Size = new Size(87, 25);
            label1.TabIndex = 18;
            label1.Text = "Minuten:";
            // 
            // StartzeitDauerMinuten
            // 
            StartzeitDauerMinuten.Location = new Point(561, 343);
            StartzeitDauerMinuten.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            StartzeitDauerMinuten.Name = "StartzeitDauerMinuten";
            StartzeitDauerMinuten.Size = new Size(38, 32);
            StartzeitDauerMinuten.TabIndex = 17;
            // 
            // StartzeitDauerStunden
            // 
            StartzeitDauerStunden.Location = new Point(363, 343);
            StartzeitDauerStunden.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            StartzeitDauerStunden.Name = "StartzeitDauerStunden";
            StartzeitDauerStunden.Size = new Size(38, 32);
            StartzeitDauerStunden.TabIndex = 16;
            // 
            // StartzeitDauerStart
            // 
            StartzeitDauerStart.AllowDrop = true;
            StartzeitDauerStart.CustomFormat = "HH:mm";
            StartzeitDauerStart.Format = DateTimePickerFormat.Custom;
            StartzeitDauerStart.Location = new Point(9, 344);
            StartzeitDauerStart.Name = "StartzeitDauerStart";
            StartzeitDauerStart.Size = new Size(200, 32);
            StartzeitDauerStart.TabIndex = 15;
            // 
            // StartzeitEndzeitEnde
            // 
            StartzeitEndzeitEnde.AllowDrop = true;
            StartzeitEndzeitEnde.CustomFormat = "HH:mm";
            StartzeitEndzeitEnde.Format = DateTimePickerFormat.Custom;
            StartzeitEndzeitEnde.Location = new Point(272, 260);
            StartzeitEndzeitEnde.Name = "StartzeitEndzeitEnde";
            StartzeitEndzeitEnde.Size = new Size(200, 32);
            StartzeitEndzeitEnde.TabIndex = 14;
            // 
            // StartzeitEndzeitStart
            // 
            StartzeitEndzeitStart.AllowDrop = true;
            StartzeitEndzeitStart.CustomFormat = "HH:mm";
            StartzeitEndzeitStart.Format = DateTimePickerFormat.Custom;
            StartzeitEndzeitStart.Location = new Point(8, 259);
            StartzeitEndzeitStart.Name = "StartzeitEndzeitStart";
            StartzeitEndzeitStart.Size = new Size(200, 32);
            StartzeitEndzeitStart.TabIndex = 12;
            // 
            // rbStartzeitEndzeit
            // 
            rbStartzeitEndzeit.AutoSize = true;
            rbStartzeitEndzeit.Location = new Point(8, 224);
            rbStartzeitEndzeit.Name = "rbStartzeitEndzeit";
            rbStartzeitEndzeit.Size = new Size(178, 29);
            rbStartzeitEndzeit.TabIndex = 1;
            rbStartzeitEndzeit.Text = "Startzeit - Endzeit";
            rbStartzeitEndzeit.CheckedChanged += RbStartzeitEndzeit_CheckedChanged;
            // 
            // rbStartzeitDauer
            // 
            rbStartzeitDauer.AutoSize = true;
            rbStartzeitDauer.Location = new Point(9, 310);
            rbStartzeitDauer.Name = "rbStartzeitDauer";
            rbStartzeitDauer.Size = new Size(167, 29);
            rbStartzeitDauer.TabIndex = 2;
            rbStartzeitDauer.Text = "Startzeit - Dauer";
            rbStartzeitDauer.CheckedChanged += RbStartzeitDauer_CheckedChanged;
            // 
            // rbEndzeitDauer
            // 
            rbEndzeitDauer.AutoSize = true;
            rbEndzeitDauer.Location = new Point(9, 382);
            rbEndzeitDauer.Name = "rbEndzeitDauer";
            rbEndzeitDauer.Size = new Size(161, 29);
            rbEndzeitDauer.TabIndex = 3;
            rbEndzeitDauer.Text = "Endzeit - Dauer";
            rbEndzeitDauer.CheckedChanged += RbEndzeitDauer_CheckedChanged;
            // 
            // lblBemerkung
            // 
            lblBemerkung.AutoSize = true;
            lblBemerkung.Location = new Point(9, 462);
            lblBemerkung.Name = "lblBemerkung";
            lblBemerkung.Size = new Size(112, 25);
            lblBemerkung.TabIndex = 5;
            lblBemerkung.Text = "Bemerkung:";
            // 
            // txtBemerkung
            // 
            txtBemerkung.Location = new Point(9, 493);
            txtBemerkung.Name = "txtBemerkung";
            txtBemerkung.Size = new Size(545, 32);
            txtBemerkung.TabIndex = 6;
            // 
            // btnSpeichern
            // 
            btnSpeichern.Location = new Point(627, 486);
            btnSpeichern.Name = "btnSpeichern";
            btnSpeichern.Size = new Size(247, 44);
            btnSpeichern.TabIndex = 7;
            btnSpeichern.Text = "Speichern";
            btnSpeichern.Click += BtnSpeichern_Click;
            // 
            // tabAuslesen
            // 
            tabAuslesen.Controls.Add(pictureBox1);
            tabAuslesen.Controls.Add(Löschen);
            tabAuslesen.Controls.Add(btnSettingsAuslesen);
            tabAuslesen.Controls.Add(btnNeuladenAuslesen);
            tabAuslesen.Controls.Add(dgvEntries);
            tabAuslesen.Location = new Point(4, 24);
            tabAuslesen.Name = "tabAuslesen";
            tabAuslesen.Size = new Size(882, 533);
            tabAuslesen.TabIndex = 2;
            tabAuslesen.Text = "Auslesen";
            // 
            // pictureBox1
            // 
            pictureBox1.ImageLocation = "res/4TIMELogo.gif";
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(882, 150);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 33;
            pictureBox1.TabStop = false;
            // 
            // Löschen
            // 
            Löschen.Location = new Point(339, 471);
            Löschen.Name = "Löschen";
            Löschen.Size = new Size(156, 59);
            Löschen.TabIndex = 5;
            Löschen.Text = "Löschen";
            Löschen.Click += Löschen_Click;
            // 
            // btnSettingsAuslesen
            // 
            btnSettingsAuslesen.Location = new Point(135, 471);
            btnSettingsAuslesen.Name = "btnSettingsAuslesen";
            btnSettingsAuslesen.Size = new Size(147, 59);
            btnSettingsAuslesen.TabIndex = 1;
            btnSettingsAuslesen.Text = "Settings";
            btnSettingsAuslesen.Click += SettingsButton_Click;
            // 
            // btnNeuladenAuslesen
            // 
            btnNeuladenAuslesen.Location = new Point(542, 471);
            btnNeuladenAuslesen.Name = "btnNeuladenAuslesen";
            btnNeuladenAuslesen.Size = new Size(156, 59);
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
            dgvEntries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntries.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvEntries.Columns.AddRange(new DataGridViewColumn[] { colStart, colEnd, colArt, colKommentar, colDauer });
            dgvEntries.Location = new Point(8, 156);
            dgvEntries.Name = "dgvEntries";
            dgvEntries.ReadOnly = true;
            dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntries.Size = new Size(866, 300);
            dgvEntries.TabIndex = 4;
            dgvEntries.CellDoubleClick += DgvEntries_CellDoubleClick;
            // 
            // colStart
            // 
            colStart.FillWeight = 50F;
            colStart.HeaderText = "colStart";
            colStart.Name = "colStart";
            colStart.ReadOnly = true;
            // 
            // colEnd
            // 
            colEnd.FillWeight = 50F;
            colEnd.HeaderText = "colEnd";
            colEnd.Name = "colEnd";
            colEnd.ReadOnly = true;
            // 
            // colArt
            // 
            colArt.FillWeight = 75F;
            colArt.HeaderText = "colArt";
            colArt.Name = "colArt";
            colArt.ReadOnly = true;
            // 
            // colKommentar
            // 
            colKommentar.FillWeight = 150F;
            colKommentar.HeaderText = "colKommentar";
            colKommentar.Name = "colKommentar";
            colKommentar.ReadOnly = true;
            // 
            // colDauer
            // 
            colDauer.FillWeight = 50F;
            colDauer.HeaderText = "colDauer";
            colDauer.Name = "colDauer";
            colDauer.ReadOnly = true;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(textBox2);
            tabSettings.Controls.Add(label28);
            tabSettings.Controls.Add(label27);
            tabSettings.Controls.Add(numericUpDownSecondsToLock);
            tabSettings.Controls.Add(button5);
            tabSettings.Controls.Add(label19);
            tabSettings.Controls.Add(label26);
            tabSettings.Controls.Add(FaceRegocnitionCheck);
            tabSettings.Controls.Add(label20);
            tabSettings.Controls.Add(label11);
            tabSettings.Controls.Add(textBox1);
            tabSettings.Controls.Add(txtOutputLog);
            tabSettings.Controls.Add(label18);
            tabSettings.Controls.Add(SpeechToTextCheck);
            tabSettings.Controls.Add(button4);
            tabSettings.Controls.Add(label15);
            tabSettings.Controls.Add(autostartCheckBox);
            tabSettings.Controls.Add(label14);
            tabSettings.Controls.Add(LockPcTime);
            tabSettings.Controls.Add(label13);
            tabSettings.Controls.Add(label12);
            tabSettings.Controls.Add(button1);
            tabSettings.Controls.Add(u18Description);
            tabSettings.Controls.Add(checkBox2);
            tabSettings.Controls.Add(label9);
            tabSettings.Controls.Add(label6);
            tabSettings.Controls.Add(checkBox1);
            tabSettings.Controls.Add(Benach);
            tabSettings.Controls.Add(label8);
            tabSettings.Controls.Add(LockTimeMin);
            tabSettings.Controls.Add(label7);
            tabSettings.Controls.Add(label21);
            tabSettings.Controls.Add(label22);
            tabSettings.Controls.Add(label23);
            tabSettings.Controls.Add(u18);
            tabSettings.Controls.Add(label25);
            tabSettings.Controls.Add(label24);
            tabSettings.Controls.Add(label17);
            tabSettings.Controls.Add(LockedTimeMin);
            tabSettings.Font = new Font("Segoe UI", 12F);
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(882, 533);
            tabSettings.TabIndex = 3;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 9F);
            label28.Location = new Point(495, 386);
            label28.Name = "label28";
            label28.Size = new Size(208, 15);
            label28.TabIndex = 40;
            label28.Text = "Optimal ist ein Wert von mindestens 7";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI", 9F);
            label27.Location = new Point(495, 371);
            label27.Name = "label27";
            label27.Size = new Size(245, 15);
            label27.TabIndex = 39;
            label27.Text = "Zeitintervall bis zur PC-Sperrung in Sekunden";
            // 
            // numericUpDownSecondsToLock
            // 
            numericUpDownSecondsToLock.Font = new Font("Segoe UI", 10F);
            numericUpDownSecondsToLock.Location = new Point(815, 343);
            numericUpDownSecondsToLock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownSecondsToLock.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownSecondsToLock.Name = "numericUpDownSecondsToLock";
            numericUpDownSecondsToLock.Size = new Size(41, 25);
            numericUpDownSecondsToLock.TabIndex = 38;
            numericUpDownSecondsToLock.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // button5
            // 
            button5.Location = new Point(276, 494);
            button5.Name = "button5";
            button5.Size = new Size(91, 36);
            button5.TabIndex = 34;
            button5.Text = "Curry";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 18F);
            label19.Location = new Point(491, 339);
            label19.Name = "label19";
            label19.Size = new Size(172, 32);
            label19.TabIndex = 37;
            label19.Text = "Schwellenwert:";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 9F);
            label26.Location = new Point(495, 317);
            label26.Name = "label26";
            label26.Size = new Size(379, 15);
            label26.TabIndex = 36;
            label26.Text = "Dein PC sperrt sich automatisch, sobald du deinen Arbeitsplatz verlässt";
            // 
            // FaceRegocnitionCheck
            // 
            FaceRegocnitionCheck.AutoSize = true;
            FaceRegocnitionCheck.Font = new Font("Segoe UI", 18F);
            FaceRegocnitionCheck.Location = new Point(841, 295);
            FaceRegocnitionCheck.Name = "FaceRegocnitionCheck";
            FaceRegocnitionCheck.Size = new Size(15, 14);
            FaceRegocnitionCheck.TabIndex = 35;
            FaceRegocnitionCheck.Tag = "";
            FaceRegocnitionCheck.UseVisualStyleBackColor = true;
            FaceRegocnitionCheck.CheckedChanged += FaceRegocnitionCheck_CheckedChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 18F);
            label20.Location = new Point(491, 285);
            label20.Name = "label20";
            label20.Size = new Size(291, 32);
            label20.TabIndex = 34;
            label20.Text = "Anwesenheits-Erkennung:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 18F);
            label11.Location = new Point(543, 220);
            label11.Name = "label11";
            label11.Size = new Size(55, 32);
            label11.TabIndex = 27;
            label11.Text = "min";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.Location = new Point(6, 374);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(451, 114);
            textBox1.TabIndex = 25;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // txtOutputLog
            // 
            txtOutputLog.Enabled = false;
            txtOutputLog.Location = new Point(8, 339);
            txtOutputLog.Name = "txtOutputLog";
            txtOutputLog.PlaceholderText = "Status: Offline";
            txtOutputLog.ReadOnly = true;
            txtOutputLog.Size = new Size(449, 29);
            txtOutputLog.TabIndex = 24;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 9F);
            label18.Location = new Point(8, 321);
            label18.Name = "label18";
            label18.Size = new Size(372, 15);
            label18.TabIndex = 22;
            label18.Text = "Wenn aktiviert hört 4Time alles was du sagst und reagiert auch darauf";
            // 
            // SpeechToTextCheck
            // 
            SpeechToTextCheck.AutoSize = true;
            SpeechToTextCheck.Font = new Font("Segoe UI", 18F);
            SpeechToTextCheck.Location = new Point(213, 303);
            SpeechToTextCheck.Name = "SpeechToTextCheck";
            SpeechToTextCheck.Size = new Size(15, 14);
            SpeechToTextCheck.TabIndex = 21;
            SpeechToTextCheck.Tag = "";
            SpeechToTextCheck.UseVisualStyleBackColor = true;
            SpeechToTextCheck.CheckedChanged += checkBox_SpeechToText_CheckedChanged;
            // 
            // button4
            // 
            button4.Location = new Point(105, 494);
            button4.Name = "button4";
            button4.Size = new Size(165, 36);
            button4.TabIndex = 19;
            button4.Text = "Programming-Joke";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(12, 33);
            label15.Name = "label15";
            label15.Size = new Size(359, 15);
            label15.TabIndex = 18;
            label15.Text = "Bestimmt, ob die Anwendung bei Pc start mit gestartet werden soll";
            // 
            // autostartCheckBox
            // 
            autostartCheckBox.AutoSize = true;
            autostartCheckBox.Font = new Font("Segoe UI", 18F);
            autostartCheckBox.Location = new Point(496, 21);
            autostartCheckBox.Name = "autostartCheckBox";
            autostartCheckBox.Size = new Size(15, 14);
            autostartCheckBox.TabIndex = 17;
            autostartCheckBox.Tag = "";
            autostartCheckBox.UseVisualStyleBackColor = true;
            autostartCheckBox.CheckedChanged += autostartCheckBox_CheckedChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 18F);
            label14.Location = new Point(7, 3);
            label14.Name = "label14";
            label14.Size = new Size(116, 32);
            label14.TabIndex = 16;
            label14.Text = "Autostart:";
            // 
            // LockPcTime
            // 
            LockPcTime.Font = new Font("Segoe UI", 10F);
            LockPcTime.Location = new Point(496, 227);
            LockPcTime.Name = "LockPcTime";
            LockPcTime.Size = new Size(41, 25);
            LockPcTime.TabIndex = 15;
            LockPcTime.ValueChanged += LockPcTime_ValueChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(10, 252);
            label13.Name = "label13";
            label13.Size = new Size(391, 15);
            label13.TabIndex = 14;
            label13.Text = "Bestimmt nach welcher inaktivitäts Zeit der Pc automatisch gesperrt wird";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F);
            label12.Location = new Point(6, 217);
            label12.Name = "label12";
            label12.Size = new Size(213, 32);
            label12.TabIndex = 13;
            label12.Text = "PC-Sperrung nach:";
            // 
            // button1
            // 
            button1.Location = new Point(6, 494);
            button1.Name = "button1";
            button1.Size = new Size(93, 36);
            button1.TabIndex = 12;
            button1.Text = "Dad-Joke";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // u18Description
            // 
            u18Description.AutoSize = true;
            u18Description.Font = new Font("Segoe UI", 9F);
            u18Description.Location = new Point(10, 186);
            u18Description.Name = "u18Description";
            u18Description.Size = new Size(356, 15);
            u18Description.TabIndex = 11;
            u18Description.Text = "Dies bestimmt gesetzliche Regelungen bezüglich der Pausenzeiten";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 18F);
            checkBox2.Location = new Point(496, 155);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 10;
            checkBox2.Tag = "";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F);
            label9.Location = new Point(438, 520);
            label9.Name = "label9";
            label9.Size = new Size(441, 13);
            label9.TabIndex = 8;
            label9.Text = "Manche Einstellungen werden erst nach einem Neustart aktiv. Klicke zum Neustarten";
            label9.Click += label9_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(10, 128);
            label6.Name = "label6";
            label6.Size = new Size(356, 15);
            label6.TabIndex = 7;
            label6.Text = "10 min vor deiner Plichtpause eine benachrichtitigung bekommen";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 18F);
            checkBox1.Location = new Point(496, 111);
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
            Benach.Location = new Point(6, 96);
            Benach.Name = "Benach";
            Benach.Size = new Size(344, 32);
            Benach.TabIndex = 5;
            Benach.Text = "10 min Reminder (Plichtpause):";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 18F);
            label8.Location = new Point(543, 55);
            label8.Name = "label8";
            label8.Size = new Size(55, 32);
            label8.TabIndex = 4;
            label8.Text = "min";
            // 
            // LockTimeMin
            // 
            LockTimeMin.Font = new Font("Segoe UI", 10F);
            LockTimeMin.Location = new Point(496, 58);
            LockTimeMin.Name = "LockTimeMin";
            LockTimeMin.Size = new Size(41, 25);
            LockTimeMin.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(10, 77);
            label7.Name = "label7";
            label7.Size = new Size(327, 15);
            label7.TabIndex = 2;
            label7.Text = "Die Sperrzeit ab der eine Desktop Benachrichtigung erscheint";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(-79, 76);
            label21.Name = "label21";
            label21.Size = new Size(1060, 21);
            label21.TabIndex = 29;
            label21.Text = "______________________________________________________________________________________________________________________________________________________";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(-98, 30);
            label22.Name = "label22";
            label22.Size = new Size(1060, 21);
            label22.TabIndex = 30;
            label22.Text = "______________________________________________________________________________________________________________________________________________________";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(-98, 127);
            label23.Name = "label23";
            label23.Size = new Size(1060, 21);
            label23.TabIndex = 31;
            label23.Text = "______________________________________________________________________________________________________________________________________________________";
            // 
            // u18
            // 
            u18.AutoSize = true;
            u18.Font = new Font("Segoe UI", 18F);
            u18.Location = new Point(8, 144);
            u18.Name = "u18";
            u18.Size = new Size(200, 32);
            u18.TabIndex = 9;
            u18.Text = "Älter als 18 Jahre:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(-79, 194);
            label25.Name = "label25";
            label25.Size = new Size(1060, 21);
            label25.TabIndex = 33;
            label25.Text = "______________________________________________________________________________________________________________________________________________________";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(-89, 256);
            label24.Name = "label24";
            label24.Size = new Size(1060, 21);
            label24.TabIndex = 32;
            label24.Text = "______________________________________________________________________________________________________________________________________________________";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 18F);
            label17.Location = new Point(3, 285);
            label17.Name = "label17";
            label17.Size = new Size(193, 32);
            label17.TabIndex = 20;
            label17.Text = "Spracherkenung:";
            // 
            // LockedTimeMin
            // 
            LockedTimeMin.AutoSize = true;
            LockedTimeMin.Font = new Font("Segoe UI", 18F);
            LockedTimeMin.Location = new Point(8, 48);
            LockedTimeMin.Name = "LockedTimeMin";
            LockedTimeMin.Size = new Size(248, 32);
            LockedTimeMin.TabIndex = 1;
            LockedTimeMin.Text = "PC Sperrzeit Schwelle:";
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Font = new Font("Segoe UI", 12F);
            textBox2.Location = new Point(496, 404);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(361, 85);
            textBox2.TabIndex = 41;
            // 
            // UserView
            // 
            ClientSize = new Size(890, 561);
            Controls.Add(tabControl);
            MaximizeBox = false;
            Name = "UserView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "4TIME";
            FormClosed += UserView_FormClosed;
            tabControl.ResumeLayout(false);
            tabUebersicht.ResumeLayout(false);
            tabUebersicht.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)numericUpDownSecondsToLock).EndInit();
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
        private Button button3;
        private NumericUpDown LockPcTime;
        private Label label13;
        private Label label12;
        private Label label15;
        private CheckBox autostartCheckBox;
        private Label label14;
        private Label OTgesamt;
        private Label label16;
        private Button button4;
        private DataGridViewTextBoxColumn colStart;
        private DataGridViewTextBoxColumn colEnd;
        private DataGridViewTextBoxColumn colArt;
        private DataGridViewTextBoxColumn colKommentar;
        private DataGridViewTextBoxColumn colDauer;
        private PictureBox pictureBox3;
        private Label label17;
        private Label label18;
        private CheckBox SpeechToTextCheck;
        private TextBox txtOutputLog;
        private TextBox textBox1;
        private PictureBox pictureBox2;
        private Label label11;
        private Label label25;
        private Label label24;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label26;
        private CheckBox FaceRegocnitionCheck;
        private Label label20;
        private Label label27;
        private NumericUpDown numericUpDownSecondsToLock;
        private Label label19;
        private PictureBox pictureBox1;
        private Button button5;
        private Label label28;
        private TextBox textBox2;
    }
}
