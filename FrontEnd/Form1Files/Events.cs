using _4Time.Async;
using _4Time.DataCore;
using _4Time.DataCore.Models;
using _4Time.FrontEnd;
using Microsoft.IdentityModel.Tokens;

namespace Time4SellersApp;

partial class UserView
{
    private void RbStartzeitEndzeit_CheckedChanged(object sender, EventArgs e)
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

    private void RbStartzeitDauer_CheckedChanged(object sender, EventArgs e)
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

    private void RbEndzeitDauer_CheckedChanged(object sender, EventArgs e)
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

    private async void BtnSpeichern_Click(object sender, EventArgs e)
    {
        int? oldId = selectedBookingIndex.HasValue
                         ? AllEntrys[selectedBookingIndex.Value].EntryID
                         : (int?)null;

        var entry = ProcessValues();

        if(entry.End < entry.Start || entry.End == entry.Start)
        {
            MessageBox.Show("Ungültige Zeiten!", "4TIME", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        //Überprüfen der Eingaben hinsichtlich des Jugendarbeitschutzes
        if (entry.End.Date == DateTime.Now.Date 
            && entry.Start.Date == DateTime.Now.Date
            && _allCategorys.Where(x => x.CategoryID == entry.CategoryID).First().IsWorkTime == false
            && !ValidateValues(entry))
        {
            DialogResult result = MessageBox.Show("Ungültiger Eintrag!\nTrotzdem buchen?", "4TIME", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.No)
                return;
        }

        if (oldId.HasValue)
        {
            entry.EntryID = oldId.Value;
            Writer.Update("Entries", entry, [$"[EntryID] = {entry.EntryID}"]);
        }
        else
            Writer.Insert("Entries", entry);

        if (selectedBookingIndex.HasValue)
        {
            //Ändert die Werte des gewählten Objekts auf der "Auslesen"-Seite
            var idx = selectedBookingIndex.Value;
            var k = AllEntrys[idx];
            k.CategoryID = entry.CategoryID;
            k.Start = entry.Start;
            k.End = entry.End;
            k.CategoryName = entry.CategoryName;
            k.Comment = entry.Comment;
        }
        else
        {
            AllEntrys.Add(new Entry
            {
                CategoryID = entry.CategoryID,
                Start = entry.Start,
                End = entry.End,
                CategoryName = entry.CategoryName,
                Comment = entry.Comment
            });
        }

        dgvEntries.Refresh();
        selectedBookingIndex = null;
        btnSpeichern.Enabled = false;

        MessageBox.Show("Daten gespeichert!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

        if (entry.CategoryName == "Pause" || entry.CategoryName == "MittagsPause" || entry.CategoryName == "RaucherPause")
        { 
            NotificationManager notificationManager = new(dgvEntries, allCategorys, checkBox1, checkBox2); 
        }
        await FillValues();
    }

    private void DgvEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        selectedBookingIndex = e.RowIndex;
        var entry = AllEntrys[e.RowIndex];

        BookingType.SelectedItem = entry.CategoryName;
        txtBemerkung.Text = entry.Comment;
        rbStartzeitEndzeit.Checked = true;
        StartzeitEndzeitStart.Value = entry.Start;
        StartzeitEndzeitEnde.Value = entry.End;

        tabControl.SelectedTab = tabEintragen;
        btnSpeichern.Enabled = true;
    }

    private async void Löschen_Click(object sender, EventArgs e)
    {
        btnNeuladenAuslesen.Enabled = false;
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
            "Möchten Sie den ausgewählten Eintrag wirklich löschen?",
            "Löschen",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Yes)
        {
            var indices = dgvEntries.SelectedRows
           .Cast<DataGridViewRow>()
           .Select(r => r.Index)
           .OrderByDescending(i => i)
           .ToList();

            foreach (int rowIndex in indices)
            {
                var entry = AllEntrys[rowIndex];
                Writer.Delete("Entries", [$"[EntryID] = {entry.EntryID}"]);
                AllEntrys.Where(x => x.EntryID == entry.EntryID).ToList().ForEach(x => AllEntrys.Remove(x));
            }

            dgvEntries.Refresh();
            await FillValues();
            btnNeuladenAuslesen.Enabled = true;
        }
    }

    private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
    {
        e.Cancel = !isDataLoaded;
    }

    public async void Neuladen_Click(object sender, EventArgs e)
    {
        PTToday.Text = "Lädt...";
        PTWeek.Text = "Lädt...";
        WTToday.Text = "Lädt...";
        WTWeek.Text = "Lädt...";
        OTToday.Text = "Lädt...";
        OTWeek.Text = "Lädt...";
        PTMin.Text = "Berechne...";
        OTgesamt.Text = "Lädt...";
        label11.Visible = false;

        this.Neuladen.Enabled = false;
        this.btnNeuladenAuslesen.Enabled = false;

        try
        {
            AllEntrys.Clear();
            await Task.Run(() => DisableReloadButton.PerformDataReloadAsync(this));
            await FillValues();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ein Fehler ist beim Neuladen aufgetreten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            this.Neuladen.Enabled = true;
            this.btnNeuladenAuslesen.Enabled = true;
            label11.Visible = true;
        }
    }

    private void BookingType_SelectionChangeCommitted(object sender, EventArgs e)
    {
        btnSpeichern.Enabled = true;

        //Überprüfen ob es sich um Abwesenheit handelt
        string[] absence = ["Urlaub", "Krankheit", "Berufsschule"];

        if (absence.Contains(BookingType.Text))
        {
            rbEndzeitDauer.Enabled = false;
            rbStartzeitDauer.Enabled = false;

            rbStartzeitEndzeit.Checked = true;
            StartzeitEndzeitStart.Value = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                7, 00, 00
            );
            StartzeitEndzeitStart.Text = "07:00";

            StartzeitEndzeitEnde.Value = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                15, 00, 00
            );
            StartzeitEndzeitEnde.Text = "15:00";
        }
        else
        {
            rbEndzeitDauer.Enabled = true;
            rbStartzeitDauer.Enabled = true;
        }
    }

    private async void UebersichtDTP_ValueChanged(object sender, EventArgs e)
    {
       await FillValues(false, true);
    }

    private void SettingsButton_Click(object sender, EventArgs e)
    {
        tabSettings.Select();
        tabSettings.Show();
        tabSettings.BringToFront();
        tabSettings.Focus();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        MessageBox.Show($"{DadJokes.GetRandomJoke()}", "Dad jokes", MessageBoxButtons.OK, MessageBoxIcon.Information);

    }

    private void button2_Click(object sender, EventArgs e)
    {
        DateTime endzeit = AllEntrys.Where(x => x.Start.Date == DateTime.Now.Date).OrderByDescending(x => x.End).FirstOrDefault()?.End ?? DateTime.Now;

        StartzeitEndzeitStart.Text = endzeit.ToString();
        StartzeitDauerStart.Text = endzeit.ToString();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        List<Entry> OutlookEntries = OutlookCalendar.DoOutlookIntegration();
        if (OutlookEntries.IsNullOrEmpty())
        {
            MessageBox.Show("Keine Einträge aus Outlook gefunden.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        foreach (var Oe in OutlookEntries)
        {
            //Writer.Insert("Entries", Oe);
        }
    }

    private void LockPcTime_ValueChanged(object sender, EventArgs e)
    {
        NumericUpDown numericUpDownControl = sender as NumericUpDown;
        if (numericUpDownControl != null)
        {
            decimal newLockMinutes = numericUpDownControl.Value;
            Task.Run(() => LockPcWhenInaktive.SetLockPcTime(newLockMinutes));
        }
    }
}
