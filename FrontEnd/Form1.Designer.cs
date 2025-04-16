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

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
    }
}
