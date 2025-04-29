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
        public MainForm()
        {
            Connector.OpenConnection();
            if (Connector.isConnected)
            {
                Thread.Sleep(222);
            }
            allCategorys = Reader.Read<Category>("Categories");
            allEntrys = Reader.Read<Entry>("Entries", null,
            [
                $"[UserID] = {Reader.Read<User>("User",
                [
                    "[UserID]"
                ],
                [
                    $"[FirstName] = '{Connector.FirstName}'",
                    $"[LastName] = '{Connector.LastName}'"    
                ]).First().UserID}",
            ]);
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePickerOverview.Value = DateTime.Now.Date;
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
        }

        private void fillValues()
        {
            Settings.Hide();
            DateTime My4SellersDateTime = dateTimePicker1.Value.Date;


            //Vormittag
            List<Entry> WorktimeVormittag = [.. allEntrys.Where(x => x.Start.Date == My4SellersDateTime.Date).Where(x => x.CategoryName == "Vormittag")];
            var FirstEntryVormittag = WorktimeVormittag.Where(x => x.Start.Date == My4SellersDateTime.Date).OrderBy(x => x.Start).FirstOrDefault();

            TimeSpan VormittagTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimeVormittag)
            {
                VormittagTimeSpan += l.End - l.Start;
            }
            var WorktimeVormittagStartEnd = $"{FirstEntryVormittag?.Start.ToShortTimeString()} - {FirstEntryVormittag?.Start.Add(VormittagTimeSpan).ToShortTimeString()}";

            //Pause
            List<Entry> WorktimePause = [.. allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CategoryName.Contains("ause"))];
            var FirstEntryPause = WorktimePause.Where(x => x.Start.Date == My4SellersDateTime).OrderBy(x => x.Start).FirstOrDefault();
            TimeSpan PauseTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimePause)
            {
                PauseTimeSpan += l.End - l.Start;
            }
            var WorktimePauseStartEnd = $"{FirstEntryPause?.Start.ToShortTimeString()} - {FirstEntryPause?.Start.Add(PauseTimeSpan).ToShortTimeString()}";

            //Nachmittag
            List<Entry> WorktimeNachmittag = [.. allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CategoryName == "Nachmittag")];
            var FirstEntryNachmittag = WorktimeNachmittag.Where(x => x.Start.Date == My4SellersDateTime).OrderBy(x => x.Start).FirstOrDefault();
            TimeSpan NachmittagTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimeNachmittag)
            {
                NachmittagTimeSpan += l.End - l.Start;
            }
            var WorktimeNachmittagStartEnd = $"{FirstEntryPause?.Start.Add(PauseTimeSpan).ToShortTimeString()} - {FirstEntryPause?.Start.Add(PauseTimeSpan + NachmittagTimeSpan).ToShortTimeString()}";

            VormittagLabel.Text = $"Vormittag:    {WorktimeVormittagStartEnd} (Interne Buchung)" ?? $"Vormittag: 00:00";
            NachmittagLabel.Text = $"Nachmittag: {WorktimeNachmittagStartEnd} (Interne Buchung)" ?? $"Nachmittag: 00:00";
            PauseLabel.Text = $"Pause:           {WorktimePauseStartEnd} (gesetzl. Pausenzeiten für Auszubildende)" ?? $"Pause: 00:00";

            btnSpeichern.Enabled = false;

            var today = dateTimePickerOverview.Value.Date;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            var weekStart = today.AddDays(-diff);

            var entriesToday = allEntrys.Where(e => e.Start.Date == today);
            var entriesWeek = allEntrys.Where(e => e.Start.Date >= weekStart && e.Start.Date <= today.Date);

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
                if (entry.CategoryName == "")
                {
                    entry.CategoryName = allCategorys
                        .First(x => x.CategoryID == entry.CategoryID)
                        ?.Description;
                }

                var dauer = (entry.End - entry.Start).ToString(@"hh\:mm");

                dgvEntries.Rows.Add(
                    entry.Start.ToString("g"),    // Start
                    entry.End.ToString("g"),      // Ende
                    entry.CategoryName,          // Art
                    entry.Comment,                // Kommentar
                    dauer                         // Dauer
                );
            }
        }
    }
}
