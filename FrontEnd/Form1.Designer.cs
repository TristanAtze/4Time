namespace Time4SellersApp
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
    }
}
