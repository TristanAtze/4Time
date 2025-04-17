using _4Time.DataCore;
using System;
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
        private Button btnNeuladenEintragen;
        private Button btnSettingsAuslesen;
        private Button btnNeuladenAuslesen;
        private System.Windows.Forms.DataGridView dgvEntries;
        private List<BookingEntry> bookings;


        public MainForm()
        {
            Connector.OpenConnection();
            if (Connector.isConnected)
            {
                Thread.Sleep(222);
            }

            InitializeComponent();
            // Beispiel-Daten befüllen und an das Grid binden
            bookings = new List<BookingEntry>()
{
            new BookingEntry {
                Startzeit = DateTime.Today.AddHours( 8).AddMinutes(  0),
                Endzeit   = DateTime.Today.AddHours(12).AddMinutes(30),
                Art       = "Arbeitszeit",
                Kommentar = "Morgendliches Meeting"
            },
            new BookingEntry {
                Startzeit = DateTime.Today.AddHours(13).AddMinutes(  0),
                Endzeit   = DateTime.Today.AddHours(17).AddMinutes(15),
                Art       = "Arbeitszeit",
                Kommentar = "Projektarbeit"
            },
            new BookingEntry {
                Startzeit = DateTime.Today.AddHours(12).AddMinutes(30),
                Endzeit   = DateTime.Today.AddHours(13).AddMinutes(  0),
                Art       = "Pause",
                Kommentar = "Mittagessen"
            }
};

            dgvEntries.DataSource = bookings;

            rbStartzeitEndzeit.Checked = true;
            BookingType.Text = "Arbeitszeit";
            PTToday.Text = "01:00 std";
            PTWeek.Text = "05:00 std";
            WTToday.Text = "08:40 std";
            WTWeek.Text = "42:00 std";
            OTToday.Text = "00:40 std";
            OTWeek.Text = "02:00 std";
            LogginName.Text = Connector.FirstName + " " + Connector.LastName;

            // Testen der Verschlüsselung
            //string encrypted = Crypto.Encrypt("Test Satz");
            //string decrypted = Crypto.Decrypt(encrypted);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabUebersicht = new TabPage();
            OTWeek = new Label();
            PTWeek = new Label();
            OTToday = new Label();
            PTToday = new Label();
            WTWeek = new Label();
            WTToday = new Label();
            button1 = new Button();
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
            btnNeuladenEintragen = new Button();
            tabAuslesen = new TabPage();
            pictureBox1 = new PictureBox();
            btnSettingsAuslesen = new Button();
            btnNeuladenAuslesen = new Button();
            dgvEntries = new DataGridView();
            this.colStart = new DataGridViewTextBoxColumn();
            this.colEnd = new DataGridViewTextBoxColumn();
            this.colArt = new DataGridViewTextBoxColumn();
            this.colKommentar = new DataGridViewTextBoxColumn();
            this.colDauer = new DataGridViewTextBoxColumn();
            this.loggedInAs = new Label();
            this.LogginName = new Label();
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
            tabUebersicht.Controls.Add(this.LogginName);
            tabUebersicht.Controls.Add(this.loggedInAs);
            tabUebersicht.Controls.Add(OTWeek);
            tabUebersicht.Controls.Add(PTWeek);
            tabUebersicht.Controls.Add(OTToday);
            tabUebersicht.Controls.Add(PTToday);
            tabUebersicht.Controls.Add(WTWeek);
            tabUebersicht.Controls.Add(WTToday);
            tabUebersicht.Controls.Add(button1);
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
            // button1
            // 
            button1.Location = new Point(363, 500);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 11;
            button1.Text = "Neuladen";
            // 
            // pictureLogoUebersicht
            // 
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
            lblMy4SellersAusgabe.Location = new Point(131, 345);
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
            tabEintragen.Controls.Add(btnNeuladenEintragen);
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
            BookingType.FormattingEnabled = true;
            BookingType.Items.AddRange(new object[] { "Pausenzeit", "Arbeitszeit", "Abwesendheit" });
            BookingType.Location = new Point(3, 185);
            BookingType.Name = "BookingType";
            BookingType.Size = new Size(207, 23);
            BookingType.TabIndex = 25;
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
            btnSpeichern.Location = new Point(187, 500);
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
            // btnNeuladenEintragen
            // 
            btnNeuladenEintragen.Location = new Point(363, 500);
            btnNeuladenEintragen.Name = "btnNeuladenEintragen";
            btnNeuladenEintragen.Size = new Size(100, 30);
            btnNeuladenEintragen.TabIndex = 10;
            btnNeuladenEintragen.Text = "Neuladen";
            // 
            // tabAuslesen
            // 
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
            // 
            // dgvEntries
            // 
            dgvEntries.AllowUserToAddRows = false;
            dgvEntries.Columns.AddRange(new DataGridViewColumn[] { this.colStart, this.colEnd, this.colArt, this.colKommentar, this.colDauer });
            dgvEntries.Location = new Point(20, 150);
            dgvEntries.Name = "dgvEntries";
            dgvEntries.ReadOnly = true;
            dgvEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntries.Size = new Size(424, 300);
            dgvEntries.TabIndex = 4;
            dgvEntries.CellDoubleClick += dgvEntries_CellDoubleClick;
            // 
            // colStart
            // 
            this.colStart.Name = "colStart";
            this.colStart.ReadOnly = true;
            // 
            // colEnd
            // 
            this.colEnd.Name = "colEnd";
            this.colEnd.ReadOnly = true;
            // 
            // colArt
            // 
            this.colArt.Name = "colArt";
            this.colArt.ReadOnly = true;
            // 
            // colKommentar
            // 
            this.colKommentar.Name = "colKommentar";
            this.colKommentar.ReadOnly = true;
            // 
            // colDauer
            // 
            this.colDauer.Name = "colDauer";
            this.colDauer.ReadOnly = true;
            // 
            // loggedInAs
            // 
            this.loggedInAs.AutoSize = true;
            this.loggedInAs.Location = new Point(20, 240);
            this.loggedInAs.Name = "loggedInAs";
            this.loggedInAs.Size = new Size(84, 15);
            this.loggedInAs.TabIndex = 18;
            this.loggedInAs.Text = "Eingeloggt als:";
            // 
            // LogginName
            // 
            this.LogginName.AutoSize = true;
            this.LogginName.Location = new Point(110, 240);
            this.LogginName.Name = "LogginName";
            this.LogginName.Size = new Size(76, 15);
            this.LogginName.TabIndex = 19;
            this.LogginName.Text = "LogginName";
            // 
            // MainForm
            // 
            ClientSize = new Size(474, 561);
            Controls.Add(tabControl);
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
            // 1) ermittle Start- & Endzeit aus den Controls
            DateTime startzeit = DateTime.Now;
            DateTime endzeit = DateTime.Now;

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

            string art = BookingType.SelectedItem?.ToString() ?? "";
            string bemerkung = txtBemerkung.Text;

            Writer.CreateEntry(startzeit, endzeit, art, bemerkung);
           
            if (selectedBookingIndex.HasValue)
            {
                var idx = selectedBookingIndex.Value;
                bookings[idx].Startzeit = startzeit;
                bookings[idx].Endzeit = endzeit;
                bookings[idx].Art = art;
                bookings[idx].Kommentar = bemerkung;
            }
            else
            {
                bookings.Add(new BookingEntry
                {
                    Startzeit = startzeit,
                    Endzeit = endzeit,
                    Art = art,
                    Kommentar = bemerkung
                });
            }

            dgvEntries.Refresh();            // bei List<T> reicht das meistens
                                             
            selectedBookingIndex = null;

            MessageBox.Show("Daten gespeichert!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selectedBookingIndex = e.RowIndex;
            var entry = bookings[e.RowIndex];

            BookingType.SelectedItem = entry.Art;
            txtBemerkung.Text = entry.Kommentar;
            rbStartzeitEndzeit.Checked = true;
            StartzeitEndzeitStart.Value = entry.Startzeit;
            StartzeitEndzeitEnde.Value = entry.Endzeit;

            tabControl.SelectedTab = tabEintragen;
        }
    }


    public class BookingEntry
    {
        public DateTime Startzeit { get; set; }
        public DateTime Endzeit { get; set; }
        public string Art { get; set; }      // z.B. "Arbeitszeit" oder "Pause"
        public string Kommentar { get; set; }

        // berechnete Eigenschaft, Format z.B. "01:30"
        public string Dauer => (Endzeit - Startzeit).ToString(@"hh\:mm");
    }

}
