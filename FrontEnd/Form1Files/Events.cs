using _4Time.DataCore.Models;
using _4Time.DataCore;

namespace Time4SellersApp;

partial class MainForm
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

    private void BtnSpeichern_Click(object sender, EventArgs e)
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
        StartzeitEndzeitStart.Text = endzeit.ToString();
        StartzeitDauerStart.Text = endzeit.ToString();
        EndzeitDauerStart.Text = endzeit.ToString();

        string art = BookingType.SelectedItem?.ToString();

        string bemerkung = txtBemerkung.Text;

        int? oldId = selectedBookingIndex.HasValue
                     ? allEntrys[selectedBookingIndex.Value].EntryID
                     : (int?)null;

        Entry entry = new()
        {
            EntryID = 0,
            Start = startzeit,
            End = endzeit,
            CategoryName = art,
            Comment = bemerkung,
            UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
        };
        entry.CategoryID = Reader.Read<Category>("Categories", ["[CategoryID]"], [$"[Description] = '{entry.CategoryName}'"]).First().CategoryID;

        if (oldId.HasValue)
        {
            entry.EntryID = oldId.Value;
            Writer.Update("Entries", entry, [$"[EntryID] = {entry.EntryID}"]);
        }
        else
            Writer.Insert("Entries", entry);

        if (selectedBookingIndex.HasValue)
        {
            var idx = selectedBookingIndex.Value;
            var k = allEntrys[idx];
            k.CategoryID = allCategorys.Where(x => x.Description == art).Select(x => x.CategoryID).First();
            k.Start = startzeit;
            k.End = endzeit;
            k.CategoryName = art;
            k.Comment = bemerkung;
        }
        else
        {
            allEntrys.Add(new Entry
            {
                CategoryID = allCategorys.Where(x => x.Description == art).Select(x => x.CategoryID).First(),
                Start = startzeit,
                End = endzeit,
                CategoryName = art,
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

    private void DgvEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        selectedBookingIndex = e.RowIndex;
        var entry = allEntrys[e.RowIndex];

        BookingType.SelectedItem = entry.CategoryName;
        txtBemerkung.Text = entry.Comment;
        rbStartzeitEndzeit.Checked = true;
        StartzeitEndzeitStart.Value = entry.Start;
        StartzeitEndzeitEnde.Value = entry.End;

        tabControl.SelectedTab = tabEintragen;
    }

    private void BtnNeuladenAuslesen_Click(object sender, EventArgs e)
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
            "Möchten Sie den ausgewählten Eintrag wirklich löschen?",
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
            Writer.Delete("Entries", [$"[EntryID] = {entry.EntryID}"]);

            allEntrys.RemoveAt(rowIndex);
        }

        dgvEntries.Refresh();
        fillValues();
        fillDataGridView();
    }

    private void Neuladen_Click(object sender, EventArgs e)
    {
        fillDataGridView();
    }

    private void BookingType_SelectionChangeCommitted(object sender, EventArgs e)
    {
        btnSpeichern.Enabled = true;
    }

    private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
    {
        fillValues();
    }

    private void DateTimePickerOverview_ValueChanged(object sender, EventArgs e)
    {
        fillValues();
    }

    private void SettingsButton_Click(object sender, EventArgs e)
    {
        Settings.Select();
        Settings.Show();
        Settings.BringToFront();
        Settings.Focus();
    }
}
