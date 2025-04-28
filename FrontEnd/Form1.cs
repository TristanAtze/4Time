using _4Time.DataCore;
using _4Time.DataCore.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Time4SellersApp
{
    public partial class MainForm : Form
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
        private Button btnSettingsUebersicht;
        private RadioButton rbStartzeitEndzeit;
        private RadioButton rbStartzeitDauer;
        private RadioButton rbEndzeitDauer;
        private Label lblInfoEintragen;
        private TextBox txtBemerkung;
        private Label lblBemerkung;
        private Button btnSpeichern;
        private Button btnSettingsEintragen;
        private Button btnSettingsAuslesen;
        private Button btnNeuladenAuslesen;
        private System.Windows.Forms.DataGridView dgvEntries;
        private List<Entry> allEntrys = [];
        private List<Category> allCategorys = [];


        public MainForm()
        {
            Connector.OpenConnection();
            if (Connector.isConnected)
            {
                Thread.Sleep(222);
            }
            allCategorys = Reader.GetAllCategorysDetails();
            allEntrys = Reader.GetAllEntrysOfUser();
            InitializeComponent();
            MaximumSize = Size;
            MinimumSize = Size;

            foreach (var e in allCategorys)
            {
                BookingType.Items.Add(e.Description);
            }

            colArt.HeaderText = "Art";
            colStart.HeaderText = "Start";
            colEnd.HeaderText = "Ende";
            colKommentar.HeaderText = "Kommentar";
            colDauer.HeaderText = "Dauer";
            fillDataGridView();
            rbStartzeitEndzeit.Checked = true;
            fillValues();

            LogginName.Text = Connector.FirstName + " " + Connector.LastName;

            // Testen der Verschlüsselung
            //string encrypted = Crypto.Encrypt("Test Satz");
            //string decrypted = Crypto.Decrypt(encrypted);
        }
        private void fillValues()
        {
            DateTime My4SellersDateTime = dateTimePicker1.Value.Date;
            VormittagLabel.Text = $"Vormittag: {allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CatergoryName == "Vormittag").Select(x => x.Duration).FirstOrDefault()}" ?? $"Vormittag: 00:00";
            NachmittagLabel.Text = $"Nachmittag: {allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CatergoryName == "Nachmittag").Select(x => x.Duration).FirstOrDefault()}" ?? $"Nachmittag: 00:00";
            PauseLabel.Text = $"Pause: {allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CatergoryName.Contains("ause")).Select(x => x.Duration).FirstOrDefault()}" ?? $"Pause: 00:00";

            btnSpeichern.Enabled = false;
            var today = DateTime.Today;

            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            var weekStart = today.AddDays(-diff);

            var entriesToday = allEntrys.Where(e => e.Start.Date == today);
            var entriesWeek = allEntrys.Where(e => e.Start.Date >= weekStart && e.Start.Date <= today);

            var isWorkLookup = allCategorys.ToDictionary(c => c.CategoryID, c => c.IsWorkTime);

            TimeSpan pauseToday = TimeSpan.Zero, workToday = TimeSpan.Zero;
            TimeSpan pauseWeek = TimeSpan.Zero, workWeek = TimeSpan.Zero;

            foreach (var e in entriesToday)
            {
                var dur = e.End - e.Start;
                if (isWorkLookup.TryGetValue(e.CategoryID, out var isWork) && isWork)
                    workToday += dur;
                else
                    pauseToday += dur;
            }

            foreach (var e in entriesWeek)
            {
                var dur = e.End - e.Start;
                if (isWorkLookup.TryGetValue(e.CategoryID, out var isWork) && isWork)
                    workWeek += dur;
                else
                    pauseWeek += dur;
            }

            TimeSpan overtimeToday = workToday - TimeSpan.FromHours(8);
            TimeSpan overtimeWeek = workWeek - TimeSpan.FromHours(40);

            PTToday.Text = $"{pauseToday:hh\\:mm} std";
            PTWeek.Text = $"{pauseWeek:hh\\:mm} std";
            WTToday.Text = $"{workToday:hh\\:mm} std";
            WTWeek.Text = $"{workWeek:hh\\:mm} std";
            OTToday.Text = $"{(overtimeToday > TimeSpan.Zero ? overtimeToday : TimeSpan.Zero):hh\\:mm} std";
            OTWeek.Text = $"{(overtimeWeek > TimeSpan.Zero ? overtimeWeek : TimeSpan.Zero):hh\\:mm} std";
        }


        private void fillDataGridView()
        {
            dgvEntries.DataSource = null;

            dgvEntries.Rows.Clear();

            foreach (var entry in allEntrys)
            {
                if (entry.CatergoryName == "")
                {
                    entry.CatergoryName = allCategorys
                        .First(x => x.CategoryID == entry.CategoryID)
                        ?.Description;
                }

                var dauer = (entry.End - entry.Start).ToString(@"hh\:mm");

                dgvEntries.Rows.Add(
                    entry.Start.ToString("g"),   // Start
                    entry.End.ToString("g"),   // Ende
                    entry.CatergoryName,                          // Art
                    entry.Comment,                // Kommentar
                    dauer                         // Dauer
                );
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl = new TabControl();
            tabUebersicht = new TabPage();
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
            btnSettingsUebersicht = new Button();
            tabEintragen = new TabPage();
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
            btnSettingsEintragen = new Button();
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
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabUebersicht);
            tabControl.Controls.Add(tabEintragen);
            tabControl.Controls.Add(tabAuslesen);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(474, 561);
            tabControl.TabIndex = 0;
            // 
            // tabUebersicht
            // 
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
            tabUebersicht.Controls.Add(btnSettingsUebersicht);
            tabUebersicht.Location = new Point(4, 24);
            tabUebersicht.Name = "tabUebersicht";
            tabUebersicht.Size = new Size(466, 533);
            tabUebersicht.TabIndex = 0;
            tabUebersicht.Text = "Übersicht";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(340, 301);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(101, 23);
            dateTimePicker1.TabIndex = 24;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // PauseLabel
            // 
            PauseLabel.AutoSize = true;
            PauseLabel.Location = new Point(20, 350);
            PauseLabel.Name = "PauseLabel";
            PauseLabel.Size = new Size(41, 15);
            PauseLabel.TabIndex = 23;
            PauseLabel.Text = "Pause:";
            // 
            // NachmittagLabel
            // 
            NachmittagLabel.AutoSize = true;
            NachmittagLabel.Location = new Point(20, 365);
            NachmittagLabel.Name = "NachmittagLabel";
            NachmittagLabel.Size = new Size(73, 15);
            NachmittagLabel.TabIndex = 22;
            NachmittagLabel.Text = "Nachmittag:";
            // 
            // VormittagLabel
            // 
            VormittagLabel.AutoSize = true;
            VormittagLabel.Location = new Point(20, 335);
            VormittagLabel.Name = "VormittagLabel";
            VormittagLabel.Size = new Size(62, 15);
            VormittagLabel.TabIndex = 21;
            VormittagLabel.Text = "Vormittag:";
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
            LogginName.Location = new Point(110, 240);
            LogginName.Name = "LogginName";
            LogginName.Size = new Size(76, 15);
            LogginName.TabIndex = 19;
            LogginName.Text = "LogginName";
            // 
            // loggedInAs
            // 
            loggedInAs.AutoSize = true;
            loggedInAs.Location = new Point(20, 240);
            loggedInAs.Name = "loggedInAs";
            loggedInAs.Size = new Size(84, 15);
            loggedInAs.TabIndex = 18;
            loggedInAs.Text = "Eingeloggt als:";
            // 
            // OTWeek
            // 
            OTWeek.AutoSize = true;
            OTWeek.Location = new Point(392, 215);
            OTWeek.Name = "OTWeek";
            OTWeek.Size = new Size(50, 15);
            OTWeek.TabIndex = 17;
            OTWeek.Text = "OTWeek";
            // 
            // PTWeek
            // 
            PTWeek.AutoSize = true;
            PTWeek.Location = new Point(392, 185);
            PTWeek.Name = "PTWeek";
            PTWeek.Size = new Size(49, 15);
            PTWeek.TabIndex = 16;
            PTWeek.Text = "PTWeek";
            // 
            // OTToday
            // 
            OTToday.AutoSize = true;
            OTToday.Location = new Point(131, 215);
            OTToday.Name = "OTToday";
            OTToday.Size = new Size(52, 15);
            OTToday.TabIndex = 15;
            OTToday.Text = "OTToday";
            // 
            // PTToday
            // 
            PTToday.AutoSize = true;
            PTToday.Location = new Point(131, 185);
            PTToday.Name = "PTToday";
            PTToday.Size = new Size(51, 15);
            PTToday.TabIndex = 14;
            PTToday.Text = "PTToday";
            // 
            // WTWeek
            // 
            WTWeek.AutoSize = true;
            WTWeek.Location = new Point(392, 157);
            WTWeek.Name = "WTWeek";
            WTWeek.Size = new Size(53, 15);
            WTWeek.TabIndex = 13;
            WTWeek.Text = "WTWeek";
            // 
            // WTToday
            // 
            WTToday.AutoSize = true;
            WTToday.Location = new Point(131, 157);
            WTToday.Name = "WTToday";
            WTToday.Size = new Size(55, 15);
            WTToday.TabIndex = 12;
            WTToday.Text = "WTToday";
            // 
            // pictureLogoUebersicht
            // 
            pictureLogoUebersicht.ErrorImage = (Image)resources.GetObject("pictureLogoUebersicht.ErrorImage");
            pictureLogoUebersicht.ImageLocation = "4TIMELogo.gif";
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
            lblArbeitszeitHeute.Location = new Point(20, 157);
            lblArbeitszeitHeute.Name = "lblArbeitszeitHeute";
            lblArbeitszeitHeute.Size = new Size(98, 15);
            lblArbeitszeitHeute.TabIndex = 1;
            lblArbeitszeitHeute.Text = "Arbeitszeit heute:";
            // 
            // lblPausenzeitHeute
            // 
            lblPausenzeitHeute.AutoSize = true;
            lblPausenzeitHeute.Location = new Point(20, 185);
            lblPausenzeitHeute.Name = "lblPausenzeitHeute";
            lblPausenzeitHeute.Size = new Size(99, 15);
            lblPausenzeitHeute.TabIndex = 2;
            lblPausenzeitHeute.Text = "Pausenzeit heute:";
            // 
            // lblUeberstundenHeute
            // 
            lblUeberstundenHeute.AutoSize = true;
            lblUeberstundenHeute.Location = new Point(20, 215);
            lblUeberstundenHeute.Name = "lblUeberstundenHeute";
            lblUeberstundenHeute.Size = new Size(111, 15);
            lblUeberstundenHeute.TabIndex = 3;
            lblUeberstundenHeute.Text = "Überstunden heute:";
            // 
            // lblArbeitszeitWoche
            // 
            lblArbeitszeitWoche.AutoSize = true;
            lblArbeitszeitWoche.Location = new Point(268, 157);
            lblArbeitszeitWoche.Name = "lblArbeitszeitWoche";
            lblArbeitszeitWoche.Size = new Size(105, 15);
            lblArbeitszeitWoche.TabIndex = 4;
            lblArbeitszeitWoche.Text = "Arbeitszeit Woche:";
            // 
            // lblPausenzeitWoche
            // 
            lblPausenzeitWoche.AutoSize = true;
            lblPausenzeitWoche.Location = new Point(268, 185);
            lblPausenzeitWoche.Name = "lblPausenzeitWoche";
            lblPausenzeitWoche.Size = new Size(106, 15);
            lblPausenzeitWoche.TabIndex = 5;
            lblPausenzeitWoche.Text = "Pausenzeit Woche:";
            // 
            // lblUeberstundenWoche
            // 
            lblUeberstundenWoche.AutoSize = true;
            lblUeberstundenWoche.Location = new Point(268, 215);
            lblUeberstundenWoche.Name = "lblUeberstundenWoche";
            lblUeberstundenWoche.Size = new Size(118, 15);
            lblUeberstundenWoche.TabIndex = 6;
            lblUeberstundenWoche.Text = "Überstunden Woche:";
            // 
            // lblMy4SellersAusgabe
            // 
            lblMy4SellersAusgabe.AutoSize = true;
            lblMy4SellersAusgabe.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblMy4SellersAusgabe.Location = new Point(20, 303);
            lblMy4SellersAusgabe.Name = "lblMy4SellersAusgabe";
            lblMy4SellersAusgabe.Size = new Size(177, 20);
            lblMy4SellersAusgabe.TabIndex = 7;
            lblMy4SellersAusgabe.Text = "My 4Sellers Ausgabe";
            // 
            // btnSettingsUebersicht
            // 
            btnSettingsUebersicht.Location = new Point(3, 500);
            btnSettingsUebersicht.Name = "btnSettingsUebersicht";
            btnSettingsUebersicht.Size = new Size(100, 30);
            btnSettingsUebersicht.TabIndex = 8;
            btnSettingsUebersicht.Text = "Settings";
            // 
            // tabEintragen
            // 
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
            tabEintragen.Controls.Add(btnSettingsEintragen);
            tabEintragen.Location = new Point(4, 24);
            tabEintragen.Name = "tabEintragen";
            tabEintragen.Size = new Size(466, 533);
            tabEintragen.TabIndex = 1;
            tabEintragen.Text = "Eintragen";
            // 
            // pictureBox2
            // 
            pictureBox2.ImageLocation = "4TIMELogo.gif";
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
            EndzeitDauerMinuten.Name = "EndzeitDauerMinuten";
            EndzeitDauerMinuten.Size = new Size(38, 23);
            EndzeitDauerMinuten.TabIndex = 22;
            // 
            // EndzeitDauerStunden
            // 
            EndzeitDauerStunden.Location = new Point(282, 369);
            EndzeitDauerStunden.Name = "EndzeitDauerStunden";
            EndzeitDauerStunden.Size = new Size(38, 23);
            EndzeitDauerStunden.TabIndex = 21;
            // 
            // EndzeitDauerStart
            // 
            EndzeitDauerStart.AllowDrop = true;
            EndzeitDauerStart.Format = DateTimePickerFormat.Time;
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
            StartzeitDauerMinuten.Name = "StartzeitDauerMinuten";
            StartzeitDauerMinuten.Size = new Size(38, 23);
            StartzeitDauerMinuten.TabIndex = 17;
            // 
            // StartzeitDauerStunden
            // 
            StartzeitDauerStunden.Location = new Point(282, 307);
            StartzeitDauerStunden.Name = "StartzeitDauerStunden";
            StartzeitDauerStunden.Size = new Size(38, 23);
            StartzeitDauerStunden.TabIndex = 16;
            // 
            // StartzeitDauerStart
            // 
            StartzeitDauerStart.AllowDrop = true;
            StartzeitDauerStart.Format = DateTimePickerFormat.Time;
            StartzeitDauerStart.Location = new Point(3, 303);
            StartzeitDauerStart.Name = "StartzeitDauerStart";
            StartzeitDauerStart.Size = new Size(200, 23);
            StartzeitDauerStart.TabIndex = 15;
            // 
            // StartzeitEndzeitEnde
            // 
            StartzeitEndzeitEnde.AllowDrop = true;
            StartzeitEndzeitEnde.Format = DateTimePickerFormat.Time;
            StartzeitEndzeitEnde.Location = new Point(246, 239);
            StartzeitEndzeitEnde.Name = "StartzeitEndzeitEnde";
            StartzeitEndzeitEnde.Size = new Size(200, 23);
            StartzeitEndzeitEnde.TabIndex = 14;
            // 
            // StartzeitEndzeitStart
            // 
            StartzeitEndzeitStart.AllowDrop = true;
            StartzeitEndzeitStart.Format = DateTimePickerFormat.Time;
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
            rbStartzeitEndzeit.CheckedChanged += rbStartzeitEndzeit_CheckedChanged;
            // 
            // rbStartzeitDauer
            // 
            rbStartzeitDauer.AutoSize = true;
            rbStartzeitDauer.Location = new Point(10, 278);
            rbStartzeitDauer.Name = "rbStartzeitDauer";
            rbStartzeitDauer.Size = new Size(109, 19);
            rbStartzeitDauer.TabIndex = 2;
            rbStartzeitDauer.Text = "Startzeit - Dauer";
            rbStartzeitDauer.CheckedChanged += rbStartzeitDauer_CheckedChanged;
            // 
            // rbEndzeitDauer
            // 
            rbEndzeitDauer.AutoSize = true;
            rbEndzeitDauer.Location = new Point(10, 344);
            rbEndzeitDauer.Name = "rbEndzeitDauer";
            rbEndzeitDauer.Size = new Size(105, 19);
            rbEndzeitDauer.TabIndex = 3;
            rbEndzeitDauer.Text = "Endzeit - Dauer";
            rbEndzeitDauer.CheckedChanged += rbEndzeitDauer_CheckedChanged;
            // 
            // lblInfoEintragen
            // 
            lblInfoEintragen.AutoSize = true;
            lblInfoEintragen.Location = new Point(3, 402);
            lblInfoEintragen.Name = "lblInfoEintragen";
            lblInfoEintragen.Size = new Size(329, 15);
            lblInfoEintragen.TabIndex = 4;
            lblInfoEintragen.Text = "Info: Es kann nur eine von den drei Optionen gewählt werden";
            // 
            // lblBemerkung
            // 
            lblBemerkung.AutoSize = true;
            lblBemerkung.Location = new Point(3, 440);
            lblBemerkung.Name = "lblBemerkung";
            lblBemerkung.Size = new Size(71, 15);
            lblBemerkung.TabIndex = 5;
            lblBemerkung.Text = "Bemerkung:";
            // 
            // txtBemerkung
            // 
            txtBemerkung.Location = new Point(3, 458);
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
            btnSpeichern.Click += btnSpeichern_Click;
            // 
            // btnSettingsEintragen
            // 
            btnSettingsEintragen.Location = new Point(3, 500);
            btnSettingsEintragen.Name = "btnSettingsEintragen";
            btnSettingsEintragen.Size = new Size(100, 30);
            btnSettingsEintragen.TabIndex = 9;
            btnSettingsEintragen.Text = "Settings";
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
            pictureBox1.ImageLocation = "4TIMELogo.gif";
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
            // 
            // btnNeuladenAuslesen
            // 
            btnNeuladenAuslesen.Location = new Point(363, 500);
            btnNeuladenAuslesen.Name = "btnNeuladenAuslesen";
            btnNeuladenAuslesen.Size = new Size(100, 30);
            btnNeuladenAuslesen.TabIndex = 2;
            btnNeuladenAuslesen.Text = "Neuladen";
            btnNeuladenAuslesen.Click += btnNeuladenAuslesen_Click;
            // 
            // dgvEntries
            // 
            dgvEntries.AllowUserToAddRows = false;
            dgvEntries.Columns.AddRange(new DataGridViewColumn[] { colStart, colEnd, colArt, colKommentar, colDauer });
            dgvEntries.Location = new Point(20, 142);
            dgvEntries.Name = "dgvEntries";
            dgvEntries.ReadOnly = true;
            dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntries.Size = new Size(424, 300);
            dgvEntries.TabIndex = 4;
            dgvEntries.CellDoubleClick += dgvEntries_CellDoubleClick;
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
            // MainForm
            // 
            ClientSize = new Size(474, 561);
            Controls.Add(tabControl);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TIME 4Sellers";
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
            ResumeLayout(false);
        }

        private void rbStartzeitEndzeit_CheckedChanged(object sender, EventArgs e)
        {
            StartzeitDauerStart.Enabled = false;
            StartzeitDauerStunden.Enabled = false;
            StartzeitDauerMinuten.Enabled = false;
            EndzeitDauerStart.Enabled = false;
            EndzeitDauerStunden.Enabled = false;
            EndzeitDauerMinuten.Enabled = false;
            StartzeitEndzeitStart.Enabled = true;
            StartzeitEndzeitEnde.Enabled = true;
        }

        private void rbStartzeitDauer_CheckedChanged(object sender, EventArgs e)
        {
            StartzeitDauerStart.Enabled = true;
            StartzeitDauerStunden.Enabled = true;
            StartzeitDauerMinuten.Enabled = true;
            EndzeitDauerStart.Enabled = false;
            EndzeitDauerStunden.Enabled = false;
            EndzeitDauerMinuten.Enabled = false;
            StartzeitEndzeitStart.Enabled = false;
            StartzeitEndzeitEnde.Enabled = false;
        }

        private void rbEndzeitDauer_CheckedChanged(object sender, EventArgs e)
        {
            StartzeitDauerStart.Enabled = false;
            StartzeitDauerStunden.Enabled = false;
            StartzeitDauerMinuten.Enabled = false;
            EndzeitDauerStart.Enabled = true;
            EndzeitDauerStunden.Enabled = true;
            EndzeitDauerMinuten.Enabled = true;
            StartzeitEndzeitStart.Enabled = false;
            StartzeitEndzeitEnde.Enabled = false;
        }



        private void btnSpeichern_Click(object sender, EventArgs e)
        {
            DateTime startzeit = DateTime.Now, endzeit = DateTime.Now;
            if (StartzeitEndzeitStart.Enabled && StartzeitEndzeitEnde.Enabled)
            {
                startzeit = StartzeitEndzeitStart.Value;
                endzeit = StartzeitEndzeitEnde.Value;
            }
            else if (StartzeitDauerStart.Enabled)
            {
                startzeit = StartzeitDauerStart.Value;
                endzeit = startzeit
                           .AddHours((double)StartzeitDauerStunden.Value)
                           .AddMinutes((double)StartzeitDauerMinuten.Value);
            }
            else if (EndzeitDauerStart.Enabled)
            {
                endzeit = EndzeitDauerStart.Value;
                startzeit = endzeit
                           .AddHours(-(double)EndzeitDauerStunden.Value)
                           .AddMinutes(-(double)EndzeitDauerMinuten.Value);
            }

            string art = BookingType.SelectedItem?.ToString();

            string bemerkung = txtBemerkung.Text;

            int? oldId = selectedBookingIndex.HasValue
                         ? allEntrys[selectedBookingIndex.Value].EntryID
                         : (int?)null;

            Writer.CreateOrUpdateEntry(
                oldId,
                startzeit,
                endzeit,
                art,
                bemerkung
            );

            if (selectedBookingIndex.HasValue)
            {
                var idx = selectedBookingIndex.Value;
                var k = allEntrys[idx];
                k.CategoryID = allCategorys.Where(x => x.Description == art).Select(x => x.CategoryID).First();
                k.Start = startzeit;
                k.End = endzeit;
                k.CatergoryName = art;
                k.Comment = bemerkung;
            }
            else
            {
                allEntrys.Add(new Entry
                {
                    CategoryID = allCategorys.Where(x => x.Description == art).Select(x => x.CategoryID).First(),
                    Start = startzeit,
                    End = endzeit,
                    CatergoryName = art,
                    Comment = bemerkung
                });
            }

            dgvEntries.Refresh();

            selectedBookingIndex = null;
            MessageBox.Show("Daten gespeichert!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            fillDataGridView();
            btnSpeichern.Enabled = false;
            fillValues();
        }


        private void dgvEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selectedBookingIndex = e.RowIndex;
            var entry = allEntrys[e.RowIndex];

            BookingType.SelectedItem = entry.CatergoryName;
            txtBemerkung.Text = entry.Comment;
            rbStartzeitEndzeit.Checked = true;
            StartzeitEndzeitStart.Value = entry.Start;
            StartzeitEndzeitEnde.Value = entry.End;

            tabControl.SelectedTab = tabEintragen;
        }

        private void btnNeuladenAuslesen_Click(object sender, EventArgs e)
        {
            fillDataGridView();
        }

        private void Löschen_Click(object sender, EventArgs e)
        {
            if (dgvEntries.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Bitte wählen Sie mindestens einen Eintrag aus.",
                    "Löschen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var result = MessageBox.Show(
                "Möchten Sie die ausgewählten Einträge wirklich löschen?",
                "Einträge löschen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result != DialogResult.Yes)
                return;

            var indices = dgvEntries.SelectedRows
               .Cast<DataGridViewRow>()
               .Select(r => r.Index)
               .OrderByDescending(i => i)
               .ToList();

            foreach (int rowIndex in indices)
            {
                var entry = allEntrys[rowIndex];
                Writer.DeleteEntry(entry.EntryID);

                allEntrys.RemoveAt(rowIndex);
            }

            dgvEntries.Refresh();
            fillValues();
            fillDataGridView();
        }

        private void Neuladen_Click(object sender, EventArgs e)
        {
            fillValues();
        }

        private void BookingType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnSpeichern.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            fillValues();
        }
    }
}
