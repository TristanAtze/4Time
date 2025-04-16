using _4Time.DataCore;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Time4SellersApp
{
    public partial class MainForm : Form
    {
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
        private Button btnNeuladenUebersicht;

        // Eintragen-Controls
        private PictureBox pictureLogoEintragen;
        private RadioButton rbStartzeitEndzeit;
        private RadioButton rbStartzeitDauer;
        private RadioButton rbEndzeitDauer;
        private Label lblInfoEintragen;
        private TextBox txtBemerkung;
        private Label lblBemerkung;
        private Button btnSpeichern;
        private Button btnSettingsEintragen;
        private Button btnNeuladenEintragen;

        // Auslesen-Controls (leer lassen, nur vorbereiten)
        private PictureBox pictureLogoAuslesen;
        private Button btnSettingsAuslesen;
        private Button btnNeuladenAuslesen;

        public MainForm()
        {
            InitializeComponent();
            rbStartzeitEndzeit.Checked = true;
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabUebersicht = new TabPage();
            pictureLogoUebersicht = new PictureBox();
            lblArbeitszeitHeute = new Label();
            lblPausenzeitHeute = new Label();
            lblUeberstundenHeute = new Label();
            lblArbeitszeitWoche = new Label();
            lblPausenzeitWoche = new Label();
            lblUeberstundenWoche = new Label();
            lblMy4SellersAusgabe = new Label();
            btnSettingsUebersicht = new Button();
            btnNeuladenUebersicht = new Button();
            tabEintragen = new TabPage();
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
            pictureLogoEintragen = new PictureBox();
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
            pictureLogoAuslesen = new PictureBox();
            btnSettingsAuslesen = new Button();
            btnNeuladenAuslesen = new Button();
            tabControl.SuspendLayout();
            tabUebersicht.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogoUebersicht).BeginInit();
            tabEintragen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerMinuten).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerStunden).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerMinuten).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerStunden).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogoEintragen).BeginInit();
            tabAuslesen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogoAuslesen).BeginInit();
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
            tabUebersicht.Controls.Add(pictureLogoUebersicht);
            tabUebersicht.Controls.Add(lblArbeitszeitHeute);
            tabUebersicht.Controls.Add(lblPausenzeitHeute);
            tabUebersicht.Controls.Add(lblUeberstundenHeute);
            tabUebersicht.Controls.Add(lblArbeitszeitWoche);
            tabUebersicht.Controls.Add(lblPausenzeitWoche);
            tabUebersicht.Controls.Add(lblUeberstundenWoche);
            tabUebersicht.Controls.Add(lblMy4SellersAusgabe);
            tabUebersicht.Controls.Add(btnSettingsUebersicht);
            tabUebersicht.Controls.Add(btnNeuladenUebersicht);
            tabUebersicht.Location = new Point(4, 24);
            tabUebersicht.Name = "tabUebersicht";
            tabUebersicht.Size = new Size(466, 533);
            tabUebersicht.TabIndex = 0;
            tabUebersicht.Text = "Übersicht";
            // 
            // pictureLogoUebersicht
            // 
            pictureLogoUebersicht.ImageLocation = "Logo.png";
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
            lblArbeitszeitWoche.Location = new Point(299, 157);
            lblArbeitszeitWoche.Name = "lblArbeitszeitWoche";
            lblArbeitszeitWoche.Size = new Size(105, 15);
            lblArbeitszeitWoche.TabIndex = 4;
            lblArbeitszeitWoche.Text = "Arbeitszeit Woche:";
            // 
            // lblPausenzeitWoche
            // 
            lblPausenzeitWoche.AutoSize = true;
            lblPausenzeitWoche.Location = new Point(299, 185);
            lblPausenzeitWoche.Name = "lblPausenzeitWoche";
            lblPausenzeitWoche.Size = new Size(106, 15);
            lblPausenzeitWoche.TabIndex = 5;
            lblPausenzeitWoche.Text = "Pausenzeit Woche:";
            // 
            // lblUeberstundenWoche
            // 
            lblUeberstundenWoche.AutoSize = true;
            lblUeberstundenWoche.Location = new Point(300, 215);
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
            // btnNeuladenUebersicht
            // 
            btnNeuladenUebersicht.Location = new Point(358, 495);
            btnNeuladenUebersicht.Name = "btnNeuladenUebersicht";
            btnNeuladenUebersicht.Size = new Size(100, 30);
            btnNeuladenUebersicht.TabIndex = 9;
            btnNeuladenUebersicht.Text = "Neuladen";
            // 
            // tabEintragen
            // 
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
            tabEintragen.Controls.Add(pictureLogoEintragen);
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(233, 270);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 24;
            label3.Text = "Stunden:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(358, 271);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 23;
            label4.Text = "Minuten:";
            // 
            // EndzeitDauerMinuten
            // 
            EndzeitDauerMinuten.Location = new Point(420, 269);
            EndzeitDauerMinuten.Name = "EndzeitDauerMinuten";
            EndzeitDauerMinuten.Size = new Size(38, 23);
            EndzeitDauerMinuten.TabIndex = 22;
            // 
            // EndzeitDauerStunden
            // 
            EndzeitDauerStunden.Location = new Point(294, 268);
            EndzeitDauerStunden.Name = "EndzeitDauerStunden";
            EndzeitDauerStunden.Size = new Size(38, 23);
            EndzeitDauerStunden.TabIndex = 21;
            // 
            // EndzeitDauerStart
            // 
            EndzeitDauerStart.AllowDrop = true;
            EndzeitDauerStart.Format = DateTimePickerFormat.Time;
            EndzeitDauerStart.Location = new Point(15, 268);
            EndzeitDauerStart.Name = "EndzeitDauerStart";
            EndzeitDauerStart.Size = new Size(200, 23);
            EndzeitDauerStart.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(233, 200);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 19;
            label2.Text = "Stunden:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(358, 198);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 18;
            label1.Text = "Minuten:";
            // 
            // StartzeitDauerMinuten
            // 
            StartzeitDauerMinuten.Location = new Point(419, 198);
            StartzeitDauerMinuten.Name = "StartzeitDauerMinuten";
            StartzeitDauerMinuten.Size = new Size(38, 23);
            StartzeitDauerMinuten.TabIndex = 17;
            // 
            // StartzeitDauerStunden
            // 
            StartzeitDauerStunden.Location = new Point(294, 198);
            StartzeitDauerStunden.Name = "StartzeitDauerStunden";
            StartzeitDauerStunden.Size = new Size(38, 23);
            StartzeitDauerStunden.TabIndex = 16;
            // 
            // StartzeitDauerStart
            // 
            StartzeitDauerStart.AllowDrop = true;
            StartzeitDauerStart.Format = DateTimePickerFormat.Time;
            StartzeitDauerStart.Location = new Point(15, 194);
            StartzeitDauerStart.Name = "StartzeitDauerStart";
            StartzeitDauerStart.Size = new Size(200, 23);
            StartzeitDauerStart.TabIndex = 15;
            // 
            // StartzeitEndzeitEnde
            // 
            StartzeitEndzeitEnde.AllowDrop = true;
            StartzeitEndzeitEnde.Format = DateTimePickerFormat.Time;
            StartzeitEndzeitEnde.Location = new Point(258, 129);
            StartzeitEndzeitEnde.Name = "StartzeitEndzeitEnde";
            StartzeitEndzeitEnde.Size = new Size(200, 23);
            StartzeitEndzeitEnde.TabIndex = 14;
            // 
            // StartzeitEndzeitStart
            // 
            StartzeitEndzeitStart.AllowDrop = true;
            StartzeitEndzeitStart.Format = DateTimePickerFormat.Time;
            StartzeitEndzeitStart.Location = new Point(15, 129);
            StartzeitEndzeitStart.Name = "StartzeitEndzeitStart";
            StartzeitEndzeitStart.Size = new Size(200, 23);
            StartzeitEndzeitStart.TabIndex = 12;
            // 
            // pictureLogoEintragen
            // 
            pictureLogoEintragen.ImageLocation = "Logo.png";
            pictureLogoEintragen.Location = new Point(64, 13);
            pictureLogoEintragen.Name = "pictureLogoEintragen";
            pictureLogoEintragen.Size = new Size(323, 75);
            pictureLogoEintragen.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogoEintragen.TabIndex = 0;
            pictureLogoEintragen.TabStop = false;
            // 
            // rbStartzeitEndzeit
            // 
            rbStartzeitEndzeit.AutoSize = true;
            rbStartzeitEndzeit.Location = new Point(15, 104);
            rbStartzeitEndzeit.Name = "rbStartzeitEndzeit";
            rbStartzeitEndzeit.Size = new Size(116, 19);
            rbStartzeitEndzeit.TabIndex = 1;
            rbStartzeitEndzeit.Text = "Startzeit - Endzeit";
            rbStartzeitEndzeit.CheckedChanged += rbStartzeitEndzeit_CheckedChanged;
            // 
            // rbStartzeitDauer
            // 
            rbStartzeitDauer.AutoSize = true;
            rbStartzeitDauer.Location = new Point(15, 169);
            rbStartzeitDauer.Name = "rbStartzeitDauer";
            rbStartzeitDauer.Size = new Size(109, 19);
            rbStartzeitDauer.TabIndex = 2;
            rbStartzeitDauer.Text = "Startzeit - Dauer";
            rbStartzeitDauer.CheckedChanged += rbStartzeitDauer_CheckedChanged;
            // 
            // rbEndzeitDauer
            // 
            rbEndzeitDauer.AutoSize = true;
            rbEndzeitDauer.Location = new Point(15, 243);
            rbEndzeitDauer.Name = "rbEndzeitDauer";
            rbEndzeitDauer.Size = new Size(105, 19);
            rbEndzeitDauer.TabIndex = 3;
            rbEndzeitDauer.Text = "Endzeit - Dauer";
            rbEndzeitDauer.CheckedChanged += rbEndzeitDauer_CheckedChanged;
            // 
            // lblInfoEintragen
            // 
            lblInfoEintragen.AutoSize = true;
            lblInfoEintragen.Location = new Point(15, 307);
            lblInfoEintragen.Name = "lblInfoEintragen";
            lblInfoEintragen.Size = new Size(329, 15);
            lblInfoEintragen.TabIndex = 4;
            lblInfoEintragen.Text = "Info: Es kann nur eine von den drei Optionen gewählt werden";
            // 
            // lblBemerkung
            // 
            lblBemerkung.AutoSize = true;
            lblBemerkung.Location = new Point(3, 333);
            lblBemerkung.Name = "lblBemerkung";
            lblBemerkung.Size = new Size(71, 15);
            lblBemerkung.TabIndex = 5;
            lblBemerkung.Text = "Bemerkung:";
            // 
            // txtBemerkung
            // 
            txtBemerkung.Location = new Point(0, 351);
            txtBemerkung.Name = "txtBemerkung";
            txtBemerkung.Size = new Size(425, 23);
            txtBemerkung.TabIndex = 6;
            // 
            // btnSpeichern
            // 
            btnSpeichern.Location = new Point(0, 380);
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
            tabAuslesen.Controls.Add(pictureLogoAuslesen);
            tabAuslesen.Controls.Add(btnSettingsAuslesen);
            tabAuslesen.Controls.Add(btnNeuladenAuslesen);
            tabAuslesen.Location = new Point(4, 24);
            tabAuslesen.Name = "tabAuslesen";
            tabAuslesen.Size = new Size(466, 533);
            tabAuslesen.TabIndex = 2;
            tabAuslesen.Text = "Auslesen";
            // 
            // pictureLogoAuslesen
            // 
            pictureLogoAuslesen.Location = new Point(20, 10);
            pictureLogoAuslesen.Name = "pictureLogoAuslesen";
            pictureLogoAuslesen.Size = new Size(200, 60);
            pictureLogoAuslesen.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogoAuslesen.TabIndex = 0;
            pictureLogoAuslesen.TabStop = false;
            // 
            // btnSettingsAuslesen
            // 
            btnSettingsAuslesen.Location = new Point(20, 400);
            btnSettingsAuslesen.Name = "btnSettingsAuslesen";
            btnSettingsAuslesen.Size = new Size(100, 30);
            btnSettingsAuslesen.TabIndex = 1;
            btnSettingsAuslesen.Text = "Settings";
            // 
            // btnNeuladenAuslesen
            // 
            btnNeuladenAuslesen.Location = new Point(130, 400);
            btnNeuladenAuslesen.Name = "btnNeuladenAuslesen";
            btnNeuladenAuslesen.Size = new Size(100, 30);
            btnNeuladenAuslesen.TabIndex = 2;
            btnNeuladenAuslesen.Text = "Neuladen";
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
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerMinuten).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndzeitDauerStunden).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerMinuten).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartzeitDauerStunden).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogoEintragen).EndInit();
            tabAuslesen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureLogoAuslesen).EndInit();
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
            DateTime Startzeit = DateTime.Now;
            DateTime Endzeit = DateTime.Now;
            if (StartzeitDauerStart.Enabled && StartzeitDauerStunden.Enabled && StartzeitDauerMinuten.Enabled)
            {
                Startzeit = StartzeitDauerStart.Value;
                Endzeit = Startzeit.AddHours((double)StartzeitDauerStunden.Value).AddMinutes((double)StartzeitDauerMinuten.Value);
            }
            else if (EndzeitDauerStart.Enabled && EndzeitDauerStunden.Enabled && EndzeitDauerMinuten.Enabled)
            {
                Endzeit = EndzeitDauerStart.Value;
                Startzeit = Endzeit.AddHours(-(double)EndzeitDauerStunden.Value).AddMinutes(-(double)EndzeitDauerMinuten.Value);
            }
            else if (StartzeitEndzeitStart.Enabled && StartzeitEndzeitEnde.Enabled)
            {
                Startzeit = StartzeitEndzeitStart.Value;
                Endzeit = StartzeitEndzeitEnde.Value;
            }
            Writer.WriteData(Startzeit, Endzeit);
        }
    }
}
