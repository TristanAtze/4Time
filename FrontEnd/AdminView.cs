using _4Time.DataCore;
using _4Time.DataCore.Models;
using System.Data;
using System.Runtime.CompilerServices;

namespace Time4SellersApp
{
    public partial class AdminView : Form
    {
        private static string simulatedUser = "";
        private readonly List<Category> _allCategorys = Reader.Read<Category>("Categories");
        private List<Entry> _allEntrys = Reader.Read<Entry>("Entries", null,
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

        private readonly List<User> _allUsers = [.. Reader.Read<User>("User").Where(x => x.FirstName != "gerd" && x.LastName != "kaufmann")];

        public AdminView()
        {
            InitializeComponent();
            MaximumSize = Size;
            MinimumSize = Size;

            dateTimePickerOverview.Value = DateTime.Now.Date;

            foreach (var e in _allUsers)
            {
                comboBox1.Items.Add(e.FirstName + " " + e.LastName);
            }

            FillDataGridView();
            FillValues();
        }

        public static string ReturnSimulatedUser()
        {
            if (simulatedUser == "")
            {
                return "olkjhdsaoivufaewsutrzgAOSIUVFUESAOITRa";
            }
            return simulatedUser;
        }

        private void FillValues()
        {
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
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            foreach (var entry in _allEntrys)
            {
                if (entry.CategoryName == "")
                {
                    entry.CategoryName = _allCategorys
                        .First(x => x.CategoryID == entry.CategoryID)
                        .Description;
                }

                var dauer = (entry.End - entry.Start).ToString(@"hh\:mm");

                dataGridView1.Rows.Add(
                    entry.Start.ToString("g"),    // Start
                    entry.End.ToString("g"),      // Ende
                    entry.CategoryName,           // Art
                    entry.Comment ?? "",          // Kommentar
                    dauer                         // Dauer
                );
            }
        }

        private void Neuladen_Click(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void UebersichtDTP_ValueChanged(object sender, EventArgs e)
        {
            FillValues();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Settings.Select();
            Settings.Show();
            Settings.BringToFront();
            Settings.Focus();
        }

        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            simulatedUser = comboBox1.Text;

            _allEntrys = Reader.Read<Entry>("Entries", null,
            [
                $"[UserID] = {Reader.Read<User>("User",
                [
                    "[UserID]"
                ],
                [
                    $"[FirstName] = '{comboBox1.Text.Split(" ")[0]}'",
                    $"[LastName] = '{comboBox1.Text.Split(" ")[1]}'"
                ]).First().UserID}",
            ], Crypto.GetUserKeys());

            FillDataGridView();
            FillValues();
        }
    }
}
