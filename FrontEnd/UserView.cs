using _4Time;
using _4Time.Async;
using _4Time.DataCore;
using _4Time.DataCore.Models;
using _4Time.FrontEnd;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace Time4SellersApp
{
    public partial class UserView : Form
    {
        private readonly IntPtr IconPointer;
        private List<(string Key, object Value)> _settingsToSave = [];
        private List<Category> _allCategorys = Reader.Read<Category>("Categories").Result;

        private Task<List<Entry>> getAllEntrys = Task.Run(() => Reader.Read<Entry>("Entries", null,
        [
            $"[UserID] = {Reader.Read<User>("User",
            [
                "[UserID]"
            ],
            [
                $"[FirstName] = '{Connector.FirstName}'",
                $"[LastName] = '{Connector.LastName}'"
            ]).Result.First().UserID}",
        ]));
        public List<Entry> _allEntrys;

        public UserView()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            MaximumSize = Size;
            MinimumSize = Size;

            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Res\\Icon.png", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\..\")));

            Bitmap bm = new Bitmap(FileName);
            IconPointer = bm.GetHicon();
            this.Icon = Icon.FromHandle(IconPointer);

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

            rbStartzeitEndzeit.Checked = true;


            FillValues();
            FillDataGridView();

            LogginName.Text = Connector.FirstName + " " + Connector.LastName;

            LoadSettings();

            NotificationManager notificationManager = new(dgvEntries, allCategorys, checkBox1, checkBox2);

            PTMin.Text = NotificationManager.startPauseAt.ToString(@"t");

            TrackLockedTime.InitializeAndStartTracking(this);
        }

        private async Task AwaitEntryTask()
        {
            await getAllEntrys;
            _allEntrys = getAllEntrys.Result;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                var x = MessageBox.Show("Sie sollten dieses Fenster nicht schließen! Trozdem schließen?", "Hinweis", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (x.Equals(DialogResult.Yes))
                {
                    SetSettingsList();
                    SettingsController.SetSettings(_settingsToSave);
                    e.Cancel = false;
                }
            }
        }



        private void LoadSettings()
        {
            var settings = SettingsController.GetSettings();
            var lockTimeMin = settings.FirstOrDefault(x => x.Key == "LockTimeMin");
            var checkBox1Value = settings.FirstOrDefault(x => x.Key == "checkBox1");
            var checkBox2Value = settings.FirstOrDefault(x => x.Key == "checkBox2");

            if (settings != null)
            {
                if (lockTimeMin.Key != null)
                {
                    LockTimeMin.Value = Convert.ToInt64(lockTimeMin.Value);
                }
                if (checkBox1Value.Key != null)
                {
                    checkBox1.Checked = Convert.ToBoolean(checkBox1Value.Value);
                }
                if (checkBox2Value.Key != null)
                {
                    checkBox2.Checked = Convert.ToBoolean(checkBox2Value.Value);
                }
            }
        }

        private async Task FillValues()
        {
            await AwaitEntryTask();

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

            var workWeekHours = workWeek.Hours + workWeek.Days * 24;

            pauseWeek = TimeSpan.FromHours(pauseWeek.TotalDays * 24);
            var pauseWeekHours = pauseWeek.Hours + pauseWeek.Days * 24;

            PTToday.Text = $"{pauseToday:hh\\:mm} std";
            PTWeek.Text = $"{pauseWeekHours}:{pauseWeek.Minutes} std";
            WTToday.Text = $"{workToday:hh\\:mm} std";
            WTWeek.Text = $"{workWeekHours}:{workWeek.Minutes} std";
            OTToday.Text = $"{(overtimeToday > TimeSpan.Zero ? overtimeToday : TimeSpan.Zero):hh\\:mm} std";
            OTWeek.Text = $"{(overtimeWeek > TimeSpan.Zero ? overtimeWeek : TimeSpan.Zero):hh\\:mm} std";
        }

        /// <summary>
        /// Gibt die Zeit zurück, die der PC gesperrt war.
        /// </summary>
        /// <returns>Gibt einen <see cref="decimal"/> zurück</returns>
        public decimal GetMinLockedTime()
        {
            return LockTimeMin.Value;
        }

        public async Task FillDataGridView()
        {
            await AwaitEntryTask();
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
                UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).Result.First().UserID,
                CategoryID = _allCategorys.Where(x => x.Description == art)
                    .Select(x => x.CategoryID)
                    .First()
            };

            return entry;
        }
        /// <summary>
        /// Validiert die Werte des Eintrags.
        /// </summary>
        /// <param name="obj"> ist der aktuelle entry</param>
        /// <returns>Gibt einen <see cref="bool"/> zurück ob der eintrag Gültig ist</returns>
        private bool ValidateValues(Entry obj)
        {
            bool result = true;

            var dailyEntries = _allEntrys.Where(x => x.Start.Date == DateTime.Now.Date)
                .ToList();

            TimeSpan workDur = TimeSpan.Zero;
            for (int i = dailyEntries.Count - 1; i >= 0; i--)
            {
                if (_allCategorys.Where(x => x.CategoryID == dailyEntries[i].CategoryID).First().IsWorkTime)
                    workDur += dailyEntries[i].End - dailyEntries[i].Start;
            }

            TimeSpan entryDur = obj.End - obj.Start;
            if (workDur >= TimeSpan.FromHours(4.5) && entryDur < TimeSpan.FromMinutes(30))
            {
                result = false;
            }
            else if (workDur < TimeSpan.FromHours(1))
            {
                result = false;
            }

            return result;
        }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        private void UserView_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetSettingsList();
            SettingsController.SetSettings(_settingsToSave);
            DestroyIcon(IconPointer);
        }

        /// <summary>
        /// Setzt die Liste der Einstellungen, die gespeichert werden sollen.
        /// </summary>
        private void SetSettingsList()
        {
            _settingsToSave.Clear();

            _settingsToSave.Add(("LockTimeMin", LockTimeMin.Value));
            _settingsToSave.Add(("checkBox1", checkBox1.Checked));
            _settingsToSave.Add(("checkBox2", checkBox2.Checked));
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Restart();
        }



        /// <summary>
        /// Restartet die Anwendung, nachdem die Einstellungen gespeichert wurden.
        /// </summary>
        private void Restart()
        {
            SetSettingsList();
            SettingsController.SetSettings(_settingsToSave);
            this.Dispose();
            this.Close();

            var process = Process.GetProcessesByName("4Time").FirstOrDefault();
            string localExePath = process.MainModule.FileName;
            string localDir = Path.GetDirectoryName(localExePath);
            Process.Start(new ProcessStartInfo { FileName = localExePath, WorkingDirectory = localDir });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{DadJokes.GetRandomJoke()}", "Dad jokes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime endzeit = _allEntrys.Where(x => x.Start.Date == DateTime.Now.Date).OrderByDescending(x => x.End).FirstOrDefault()?.End ?? DateTime.Now;

            StartzeitEndzeitStart.Text = endzeit.ToString();
            StartzeitDauerStart.Text = endzeit.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OutlookCalendar.DoOutlookIntegration();
        }
    }
}
