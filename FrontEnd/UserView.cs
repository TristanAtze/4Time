using _4Time.DataCore;
using _4Time.DataCore.Models;
using System.Data;

namespace Time4SellersApp
{
    public partial class UserView : Form
    {
        private readonly List<Category> _allCategorys = Reader.Read<Category>("Categories");
        private readonly List<Entry> _allEntrys = Reader.Read<Entry>("Entries", null,
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

        public UserView()
        {
            InitializeComponent();
            MaximumSize = Size;
            MinimumSize = Size;

            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePickerOverview.Value = DateTime.Now.Date;

            foreach (var e in _allCategorys)
            {
                BookingType.Items.Add(e.Description);
            }

            colArt.HeaderText = "Art";
            colStart.HeaderText = "Start";
            colEnd.HeaderText = "Ende";
            colKommentar.HeaderText = "Kommentar";
            colDauer.HeaderText = "Dauer";

            FillDataGridView();
            rbStartzeitEndzeit.Checked = true;
            FillValues();

            LogginName.Text = Connector.FirstName + " " + Connector.LastName;
        }

        private void FillValues()
        {
            Settings.Hide();
            DateTime My4SellersDateTime = dateTimePicker1.Value.Date;


            //Vormittag
            List<Entry> WorktimeVormittag = [.. _allEntrys.Where(x => x.Start.Date == My4SellersDateTime.Date).Where(x => x.CategoryName == "Vormittag")];
            var FirstEntryVormittag = WorktimeVormittag.Where(x => x.Start.Date == My4SellersDateTime.Date).OrderBy(x => x.Start).FirstOrDefault();

            TimeSpan VormittagTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimeVormittag)
            {
                VormittagTimeSpan += l.End - l.Start;
            }
            var WorktimeVormittagStartEnd = $"{FirstEntryVormittag?.Start.ToShortTimeString()} - {FirstEntryVormittag?.Start.Add(VormittagTimeSpan).ToShortTimeString()}";

            //Pause
            List<Entry> WorktimePause = [.. _allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CategoryName.Contains("ause"))];
            var FirstEntryPause = WorktimePause.Where(x => x.Start.Date == My4SellersDateTime).OrderBy(x => x.Start).FirstOrDefault();
            TimeSpan PauseTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimePause)
            {
                PauseTimeSpan += l.End - l.Start;
            }
            var WorktimePauseStartEnd = $"{FirstEntryPause?.Start.ToShortTimeString()} - {FirstEntryPause?.Start.Add(PauseTimeSpan).ToShortTimeString()}";

            //Nachmittag
            List<Entry> WorktimeNachmittag = [.. _allEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CategoryName == "Nachmittag")];
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

            var entriesToday = _allEntrys.Where(e => e.Start.Date == today);
            var entriesWeek = _allEntrys.Where(e => e.Start.Date >= weekStart && e.Start.Date <= today.Date);

            var isWorkLookup = _allCategorys.ToDictionary(c => c.CategoryID, c => c.IsWorkTime);

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

        private void FillDataGridView()
        {
            dgvEntries.DataSource = null;
            dgvEntries.Rows.Clear();

            foreach (var entry in _allEntrys)
            {
                if (entry.CategoryName == "")
                {
                    entry.CategoryName = _allCategorys
                        .First(x => x.CategoryID == entry.CategoryID)
                        .Description;
                }

                var dauer = (entry.End - entry.Start).ToString(@"hh\:mm");

                dgvEntries.Rows.Add(
                    entry.Start.ToString("g"),    // Start
                    entry.End.ToString("g"),      // Ende
                    entry.CategoryName,           // Art
                    entry.Comment ?? "",          // Kommentar
                    dauer                         // Dauer
                );
            }
        }

        private Entry ProcessValues()
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

            string art = BookingType.SelectedItem?.ToString() ?? "";

            string bemerkung = txtBemerkung.Text;

            Entry entry = new()
            {
                EntryID = 0,
                Start = startzeit,
                End = endzeit,
                CategoryName = art,
                Comment = bemerkung,
                //TODO GetUser sollte u.a. einen User zurückgeben, welcher alle nötigen Werte (Vor- und Nachname + ID) enthält
                UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                CategoryID = _allCategorys.Where(x => x.Description == art)
                    .Select(x => x.CategoryID)
                    .First()
            };

            return entry;
        }
    }
}
