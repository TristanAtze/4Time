using _4Time;
using _4Time.Async;
using _4Time.DataCore;
using _4Time.DataCore.Models;
using _4Time.FrontEnd;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using Windows.ApplicationModel.Core;

namespace Time4SellersApp
{
    public partial class UserView : Form
    {
        private readonly IntPtr IconPointer;
        private List<(string Key, object Value)> _settingsToSave = [];
        private List<Category> _allCategorys = Reader.Read<Category>("Categories").Result;
        private bool isDataLoaded = false;

        public List<Entry> AllEntrys;

        public UserView()
        {
            InitializeComponent();

            tabAuslesen.Text = "Lädt...";
            tabEintragen.Text = "Lädt...";
            tabSettings.Text = "Lädt...";

            btnNeuladenAuslesen.Enabled = false;
            Neuladen.Enabled = false;

            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            MaximumSize = Size;
            MinimumSize = Size;

            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Res\\Icon.png", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\..\")));

            Bitmap bm = new Bitmap(FileName);
            IconPointer = bm.GetHicon();
            this.Icon = Icon.FromHandle(IconPointer);

            

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

            _ = FillValues();

            LogginName.Text = Connector.FirstName + " " + Connector.LastName;

            LoadSettings();

            TrackLockedTime.InitializeAndStartTracking(this);
        }

        public static async Task<List<Entry>> GetAllEntriesAsync()
        {
            List<User> users = await Task.Run(() => Reader.Read<User>("User",
                new string[] { "[UserID]" },
                new string[] {
                    $"[FirstName] = '{Connector.FirstName}'",
                    $"[LastName] = '{Connector.LastName}'"
                }));

            if (users == null || !users.Any())
            {
                throw new InvalidOperationException("No user found with the given first and last name.");
            }
            int userId = users.First().UserID;

            return await Task.Run(() => Reader.Read<Entry>("Entries", null,
                new string[] {
                     $"[UserID] = {userId}"
                }));
        }

        private async Task AwaitEntryTask()
        {
            if (!isDataLoaded)
                AllEntrys = await GetAllEntriesAsync();
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
            var lockPcTime = settings.FirstOrDefault(x => x.Key == "LockPcTime");
            var autostartCheckBoxValue = settings.FirstOrDefault(x => x.Key == "Autostart");

            if (settings != null)
            {
                if (lockTimeMin.Key != null)
                    LockTimeMin.Value = Convert.ToInt64(lockTimeMin.Value);

                if (checkBox1Value.Key != null)
                    checkBox1.Checked = Convert.ToBoolean(checkBox1Value.Value);

                if (checkBox2Value.Key != null)
                    checkBox2.Checked = Convert.ToBoolean(checkBox2Value.Value);

                if (lockPcTime.Key != null)
                    LockPcTime.Value = Convert.ToInt64(lockPcTime.Value);

                if (autostartCheckBoxValue.Key != null)
                    autostartCheckBox.Checked = Convert.ToBoolean(autostartCheckBoxValue.Value);
            }
        }

        private async Task FillValues(bool reloadDataGrid = true, bool isDatetimePicker = false)
        {
            DateTime My4SellersDateTime;    

            if (!isDatetimePicker)
            {
                await AwaitEntryTask();
            }

            My4SellersDateTime = dateTimePicker1.Value.Date;

            if (!isDatetimePicker)
            {
                dateTimePicker1.Value = DateTime.Now.Date;
                dateTimePickerOverview.Value = DateTime.Now.Date;
            }
            else
            {
                My4SellersDateTime = DateTime.Now.Date;
            }

            

            //Vormittag
            List<Entry> WorktimeVormittag = [.. AllEntrys.Where(x => x.Start.Date == My4SellersDateTime.Date).Where(x => x.CategoryName == "Vormittag")];
            var FirstEntryVormittag = WorktimeVormittag.Where(x => x.Start.Date == My4SellersDateTime.Date).OrderBy(x => x.Start).FirstOrDefault();

            TimeSpan VormittagTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimeVormittag)
            {
                VormittagTimeSpan += l.End - l.Start;
            }
            var WorktimeVormittagStartEnd = $"{FirstEntryVormittag?.Start.ToShortTimeString()} - {FirstEntryVormittag?.Start.Add(VormittagTimeSpan).ToShortTimeString()}";

            //Pause
            List<Entry> WorktimePause = [.. AllEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CategoryName.Contains("ause"))];
            var FirstEntryPause = WorktimePause.Where(x => x.Start.Date == My4SellersDateTime).OrderBy(x => x.Start).FirstOrDefault();
            TimeSpan PauseTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimePause)
            {
                PauseTimeSpan += l.End - l.Start;
            }
            var WorktimePauseStartEnd = $"{FirstEntryPause?.Start.ToShortTimeString()} - {FirstEntryPause?.Start.Add(PauseTimeSpan).ToShortTimeString()}";

            //Nachmittag
            List<Entry> WorktimeNachmittag = [.. AllEntrys.Where(x => x.Start.Date == My4SellersDateTime).Where(x => x.CategoryName == "Nachmittag")];
            var FirstEntryNachmittag = WorktimeNachmittag.Where(x => x.Start.Date == My4SellersDateTime).OrderBy(x => x.Start).FirstOrDefault();
            TimeSpan NachmittagTimeSpan = TimeSpan.Zero;
            foreach (var l in WorktimeNachmittag)
            {
                NachmittagTimeSpan += l.End - l.Start;
            }
            var WorktimeNachmittagStartEnd = $"{FirstEntryPause?.Start.Add(PauseTimeSpan).ToShortTimeString()} - {FirstEntryPause?.Start.Add(PauseTimeSpan + NachmittagTimeSpan).ToShortTimeString()}";

            VormittagLabel.Text = $"Vormittag:    {WorktimeVormittagStartEnd} (Interne Buchung)" ?? $"Vormittag: 00:00";
            NachmittagLabel.Text = $"Nachmittag: {WorktimeNachmittagStartEnd} (Interne Buchung)" ?? $"Nachmittag: 00:00";
            PauseLabel.Text = $"Pause:          {WorktimePauseStartEnd} (gesetzl. Pausenzeiten für Auszubildende)" ?? $"Pause: 00:00";

            btnSpeichern.Enabled = false;

            var today = dateTimePickerOverview.Value.Date; // Dies ist der ausgewählte Tag im dateTimePickerOverview
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            var weekStart = today.AddDays(-diff);
            var weekEnd = weekStart.AddDays(4); // Annahme: Woche geht von Montag bis Freitag (5 Arbeitstage)

            var entriesToday = AllEntrys.Where(e => e.Start.Date == today);
            var entriesWeek = AllEntrys.Where(e => e.Start.Date >= weekStart && e.Start.Date <= weekEnd.Date);

            var isWorkLookup = _allCategorys.ToDictionary(c => c.CategoryID, c => c.IsWorkTime);

            TimeSpan pauseToday = TimeSpan.Zero, workToday = TimeSpan.Zero;
            TimeSpan pauseWeek = TimeSpan.Zero, workWeek = TimeSpan.Zero;

            // Berechnung für den ausgewählten Tag (today)
            foreach (var e in entriesToday)
            {
                var dur = e.End - e.Start;
                if (isWorkLookup.TryGetValue(e.CategoryID, out var isWork) && isWork)
                    workToday += dur;
                else
                    pauseToday += dur;
            }

            // Berechnung für die gesamte Woche
            foreach (var e in entriesWeek)
            {
                var dur = e.End - e.Start;
                if (isWorkLookup.TryGetValue(e.CategoryID, out var isWork) && isWork)
                    workWeek += dur;
                else
                    pauseWeek += dur;
            }

            TimeSpan overtimeToday = workToday - TimeSpan.FromHours(8); // Annahme: 8 Stunden Sollarbeitszeit pro Tag
            TimeSpan overtimeWeek = workWeek - TimeSpan.FromHours(40); // Annahme: 40 Stunden Sollarbeitszeit pro Woche

            TimeSpan summedDailyOvertime = TimeSpan.Zero;

            for (DateTime date = weekStart; date <= weekEnd; date = date.AddDays(1))
            {
                TimeSpan workTimeForDay = TimeSpan.Zero;
                var entriesForSpecificDayInWeek = AllEntrys.Where(e => e.Start.Date == date);

                foreach (var e in entriesForSpecificDayInWeek)
                {
                    var dur = e.End - e.Start;
                    if (isWorkLookup.TryGetValue(e.CategoryID, out var isWork) && isWork)
                    {
                        workTimeForDay += dur;
                    }
                }

                TimeSpan dailyOvertime = workTimeForDay - TimeSpan.FromHours(8);

                if (dailyOvertime > TimeSpan.Zero)
                {
                    summedDailyOvertime += dailyOvertime;
                }
            }
            OTgesamt.Text = $"{summedDailyOvertime:hh\\:mm} std";

            var workWeekHours = workWeek.Hours + workWeek.Days * 24;

            pauseWeek = TimeSpan.FromHours(pauseWeek.TotalDays * 24);
            var pauseWeekHours = pauseWeek.Hours + pauseWeek.Days * 24;

            string pauseWeekHoursStr = pauseWeekHours.ToString();
            string pauseWeekMinutesStr = pauseWeek.Minutes.ToString();
            string workWeekHoursStr = workWeekHours.ToString();
            string WorkWeekMinutesStr = workWeek.Minutes.ToString();

            if (pauseWeekHours < 10)
                pauseWeekHoursStr = "0" + pauseWeekHours;

            if (pauseWeek.Minutes < 10)
                pauseWeekMinutesStr = "0" + pauseWeek.Minutes;

            if (workWeekHours < 10)
                workWeekHoursStr = "0" + workWeekHours;

            if (workWeek.Minutes < 10)
                WorkWeekMinutesStr = "0" + workWeek.Minutes;

            PTToday.Text = $"{pauseToday:hh\\:mm} std";
            PTWeek.Text = $"{pauseWeekHoursStr}:{pauseWeekMinutesStr} std";
            WTToday.Text = $"{workToday:hh\\:mm} std";
            WTWeek.Text = $"{workWeekHoursStr}:{WorkWeekMinutesStr} std";
            OTToday.Text = $"{(overtimeToday > TimeSpan.Zero ? overtimeToday : TimeSpan.Zero):hh\\:mm} std";
            OTWeek.Text = $"{(overtimeWeek > TimeSpan.Zero ? overtimeWeek : TimeSpan.Zero):hh\\:mm} std";

            if (reloadDataGrid)
                await FillDataGridView();

            if (!isDataLoaded && !isDatetimePicker)
            {
                NotificationManager notificationManager = new(dgvEntries, allCategorys, checkBox1, checkBox2);
            }


            PTMin.Text = NotificationManager.startPauseAt.ToString(@"t");
            label11.Show();
            btnNeuladenAuslesen.Enabled = true;
            Neuladen.Enabled = true;

            
           
            tabAuslesen.Text = "Auslesen";
            tabEintragen.Text = "Eintragen";
            tabSettings.Text = "Settings";
            isDataLoaded = true;
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

            foreach (var entry in AllEntrys)
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

            var readerTask = Task.Run(async () => await Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]));

            Entry entry = new()
            {
                EntryID = 0,
                Start = startzeit,
                End = endzeit,
                CategoryName = art,
                Comment = bemerkung,
                UserID = readerTask.Result.First().UserID,
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

            var dailyEntries = AllEntrys.Where(x => x.Start.Date == DateTime.Now.Date)
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
            _settingsToSave.Add(("LockPcTime", LockPcTime.Value));
            _settingsToSave.Add(("Autostart", autostartCheckBox.Checked));
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

        private void autostartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autostartCheckBox.Checked)
            {
                if (!AutostartHelper.IsApplicationInCurrentUserStartup())
                {
                    AutostartHelper.AddApplicationToCurrentUserStartup();
                    MessageBox.Show("Autostart wurde aktiviert. Die Anwendung wird nun automatisch gestartet.", "Autostart aktiviert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                AutostartHelper.RemoveApplicationFromCurrentUserStartup();
                MessageBox.Show("Autostart wurde deaktiviert. Die Anwendung wird nicht mehr automatisch gestartet.", "Autostart deaktiviert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
