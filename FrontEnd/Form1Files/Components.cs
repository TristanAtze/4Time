using _4Time.DataCore.Models;

namespace Time4SellersApp;

partial class MainForm
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
	private DataGridView dgvEntries;
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
	private Button Neuladen;
	private Label PauseLabel;
	private Label NachmittagLabel;
	private Label VormittagLabel;
	private DateTimePicker dateTimePicker1;
	private DateTimePicker dateTimePickerOverview;
	private TabPage Settings;
	private Label label6;
}