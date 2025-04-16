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
        private Label lblStandartEintragung;
        private Button btnSettingsEintragen;
        private Button btnNeuladenEintragen;

        // Auslesen-Controls (leer lassen, nur vorbereiten)
        private PictureBox pictureLogoAuslesen;
        private Button btnSettingsAuslesen;
        private Button btnNeuladenAuslesen;

        public MainForm()
        {
            InitializeComponent();
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
            pictureLogoEintragen = new PictureBox();
            rbStartzeitEndzeit = new RadioButton();
            rbStartzeitDauer = new RadioButton();
            rbEndzeitDauer = new RadioButton();
            lblInfoEintragen = new Label();
            lblBemerkung = new Label();
            txtBemerkung = new TextBox();
            btnSpeichern = new Button();
            lblStandartEintragung = new Label();
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
            pictureLogoUebersicht.Location = new Point(20, 14);
            pictureLogoUebersicht.Name = "pictureLogoUebersicht";
            pictureLogoUebersicht.Size = new Size(424, 122);
            pictureLogoUebersicht.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogoUebersicht.TabIndex = 0;
            pictureLogoUebersicht.TabStop = false;
            pictureLogoUebersicht.Image = Image.FromFile("Logo.png");
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
            tabEintragen.Controls.Add(pictureLogoEintragen);
            tabEintragen.Controls.Add(rbStartzeitEndzeit);
            tabEintragen.Controls.Add(rbStartzeitDauer);
            tabEintragen.Controls.Add(rbEndzeitDauer);
            tabEintragen.Controls.Add(lblInfoEintragen);
            tabEintragen.Controls.Add(lblBemerkung);
            tabEintragen.Controls.Add(txtBemerkung);
            tabEintragen.Controls.Add(btnSpeichern);
            tabEintragen.Controls.Add(lblStandartEintragung);
            tabEintragen.Controls.Add(btnSettingsEintragen);
            tabEintragen.Controls.Add(btnNeuladenEintragen);
            tabEintragen.Location = new Point(4, 24);
            tabEintragen.Name = "tabEintragen";
            tabEintragen.Size = new Size(466, 533);
            tabEintragen.TabIndex = 1;
            tabEintragen.Text = "Eintragen";
            // 
            // pictureLogoEintragen
            // 
            pictureLogoEintragen.Location = new Point(54, 23);
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
            // 
            // rbStartzeitDauer
            // 
            rbStartzeitDauer.AutoSize = true;
            rbStartzeitDauer.Location = new Point(15, 138);
            rbStartzeitDauer.Name = "rbStartzeitDauer";
            rbStartzeitDauer.Size = new Size(109, 19);
            rbStartzeitDauer.TabIndex = 2;
            rbStartzeitDauer.Text = "Startzeit - Dauer";
            // 
            // rbEndzeitDauer
            // 
            rbEndzeitDauer.AutoSize = true;
            rbEndzeitDauer.Location = new Point(15, 163);
            rbEndzeitDauer.Name = "rbEndzeitDauer";
            rbEndzeitDauer.Size = new Size(105, 19);
            rbEndzeitDauer.TabIndex = 3;
            rbEndzeitDauer.Text = "Endzeit - Dauer";
            // 
            // lblInfoEintragen
            // 
            lblInfoEintragen.AutoSize = true;
            lblInfoEintragen.Location = new Point(20, 202);
            lblInfoEintragen.Name = "lblInfoEintragen";
            lblInfoEintragen.Size = new Size(329, 15);
            lblInfoEintragen.TabIndex = 4;
            lblInfoEintragen.Text = "Info: Es kann nur eine von den drei Optionen gewählt werden";
            // 
            // lblBemerkung
            // 
            lblBemerkung.AutoSize = true;
            lblBemerkung.Location = new Point(20, 248);
            lblBemerkung.Name = "lblBemerkung";
            lblBemerkung.Size = new Size(71, 15);
            lblBemerkung.TabIndex = 5;
            lblBemerkung.Text = "Bemerkung:";
            // 
            // txtBemerkung
            // 
            txtBemerkung.Location = new Point(20, 278);
            txtBemerkung.Name = "txtBemerkung";
            txtBemerkung.Size = new Size(425, 23);
            txtBemerkung.TabIndex = 6;
            // 
            // btnSpeichern
            // 
            btnSpeichern.Location = new Point(20, 318);
            btnSpeichern.Name = "btnSpeichern";
            btnSpeichern.Size = new Size(100, 30);
            btnSpeichern.TabIndex = 7;
            btnSpeichern.Text = "Speichern";
            // 
            // lblStandartEintragung
            // 
            lblStandartEintragung.AutoSize = true;
            lblStandartEintragung.Location = new Point(184, 422);
            lblStandartEintragung.Name = "lblStandartEintragung";
            lblStandartEintragung.Size = new Size(112, 15);
            lblStandartEintragung.TabIndex = 8;
            lblStandartEintragung.Text = "Standart Eintragung";
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
            tabAuslesen.Size = new Size(380, 533);
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
            ((System.ComponentModel.ISupportInitialize)pictureLogoEintragen).EndInit();
            tabAuslesen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureLogoAuslesen).EndInit();
            ResumeLayout(false);
        }
    }
}
