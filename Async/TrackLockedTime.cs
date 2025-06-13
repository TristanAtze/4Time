using _4Time.DataCore;
using _4Time.DataCore.Models;
using Microsoft.Win32;
using System.Diagnostics;
using Time4SellersApp;


namespace _4Time.Async;

internal class TrackLockedTime
{
    public static DateTime? _pcLockedTime;
    public static DateTime? _pcUnlockedTime;

    private static UserView _mainUserViewInstance;

    public static void InitializeAndStartTracking(UserView mainView)
    {
        if (_mainUserViewInstance != null)
        {
            Debug.WriteLine("TrackLockedTime wurde bereits initialisiert.");
            return;
        }

        _mainUserViewInstance = mainView ?? throw new ArgumentNullException(nameof(mainView), "UserView-Instanz darf nicht null sein.");
        SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        Debug.WriteLine("TrackLockedTime initialisiert und SessionSwitch-Event abonniert.");
    }

    private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
    {
        if (_mainUserViewInstance == null)
        {
            Debug.WriteLine("FEHLER: TrackLockedTime.SystemEvents_SessionSwitch aufgerufen, aber _mainUserViewInstance ist null. InitializeAndStartTracking wurde nicht (korrekt) aufgerufen.");
            return;
        }

        switch (e.Reason)
        {
            case SessionSwitchReason.SessionLock:
                _pcLockedTime = DateTime.Now;
                Debug.WriteLine($"PC gesperrt um: {_pcLockedTime}");
                break;

            case SessionSwitchReason.SessionUnlock:
                _pcUnlockedTime = DateTime.Now;
                Debug.WriteLine($"PC entsperrt um: {_pcUnlockedTime}");
                AutoBookCaller();
                ShowPauseMessageBox();
                break;
            default:
                break;
        }
    }

    private static void AutoBookCaller()
    {
        if (_mainUserViewInstance?.AllEntrys == null)
        {
            Debug.WriteLine("AutoBookCaller: _mainUserViewInstance oder _allEntrys ist null.");
            return;
        }

        Entry? todaysFirstEntry = _mainUserViewInstance.AllEntrys
            .Where(x => x.Start.Date == DateTime.Now.Date)
            .Where(x => x.Start.TimeOfDay < DateTime.Now.TimeOfDay)
            .OrderBy(x => x.Start).FirstOrDefault();

        Entry? lastEntry = _mainUserViewInstance.AllEntrys
            .Where(x => x.End.Date == DateTime.Now.Date)
            .Where(x => x.End.TimeOfDay < DateTime.Now.TimeOfDay)
            .OrderByDescending(x => x.End).FirstOrDefault();

        if (todaysFirstEntry == null)
            ShowFirstEntryMessageBox(null);
        else
            ShowLatestEntryMessageBox(lastEntry);

        _mainUserViewInstance.Neuladen.PerformClick();
    }

    private async static void ShowLatestEntryMessageBox(Entry? entry)
    {
        if (_mainUserViewInstance == null)
        {
            Debug.WriteLine("ShowLatestEntryMessageBox: _mainUserViewInstance ist null.");
            return;
        }
        if (entry == null)
        {
            Debug.WriteLine("ShowLatestEntryMessageBox: 'entry' ist null, keine Aktion durchgeführt.");
            return;
        }

        var latestEntryEnd = entry.End;
        var category = 1;
        string bookTypeString = "";

        if (DateTime.Now.Hour >= 12)
        {
            bookTypeString = "Nachmittag"; category = 10;
        }
        else
        {
            bookTypeString = "Vormittag"; category = 9;
        }

        Thread.Sleep(500);
        if (!_pcLockedTime.HasValue) return;
        var ursprünglicheMinSize = _mainUserViewInstance.MinimumSize;
        _mainUserViewInstance.MinimumSize = new System.Drawing.Size(0, 0);

        DialogResult result = MessageBox.Show(
            _mainUserViewInstance,
            $"Der PC wurde Entsperrt. Willst du folgende Zeit als '{bookTypeString}' buchen?\nStart: {latestEntryEnd}\nEnde: {_pcLockedTime.Value}",
            "4Time Autobook",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1
        );

        if (result == DialogResult.Yes)
        {
            Writer.Insert("Entries", new Entry
            {
                UserID = await Task.Run(async () => Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).Result.First().UserID),
                Start = latestEntryEnd,
                End = _pcLockedTime.Value,
                CategoryID = category,
                Comment = "[AUTO]Letzer Eintrag - Lock"
            });
        }
        _mainUserViewInstance.MinimumSize = ursprünglicheMinSize;
    }

    private async static void ShowFirstEntryMessageBox(Entry? todaysFirstEntry)
    {
        if (_mainUserViewInstance == null)
        {
            Debug.WriteLine("ShowFirstEntryMessageBox: _mainUserViewInstance ist null.");
            return;
        }

        string bookTypeString = "Vormittag";
        TimeSpan pcUptime = TimeSpan.FromMilliseconds(Environment.TickCount);
        DateTime pcStartedAt = DateTime.Now - pcUptime;

        Thread.Sleep(500);
        if (_pcLockedTime.HasValue)
        {
            var ursprünglicheMinSize = _mainUserViewInstance.Size;
            _mainUserViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                _mainUserViewInstance,
                $"Der PC wurde Entsperrt. Willst du folgende Zeit als '{bookTypeString}' buchen?\nStart: {pcStartedAt}\nEnde: {_pcLockedTime.Value}",
                "4Time Autobook",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1
            );

            if (result == DialogResult.Yes)
            {
                Writer.Insert("Entries", new Entry
                {
                    UserID = await Task.Run(async () => Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).Result.First().UserID),
                    Start = pcStartedAt,
                    End = _pcLockedTime.Value,
                    CategoryID = 9,
                    Comment = "[AUTO]PC Start - Lock"
                });
            }
            _mainUserViewInstance.MinimumSize = ursprünglicheMinSize;
        }
    }

    private async static void ShowPauseMessageBox()
    {
        if (_mainUserViewInstance == null)
        {
            Debug.WriteLine("ShowPauseMessageBox: _mainUserViewInstance ist null.");
            return;
        }

        Thread.Sleep(500);
        if (_pcLockedTime.HasValue && _pcUnlockedTime.HasValue &&
            (_pcUnlockedTime.Value - _pcLockedTime.Value > TimeSpan.FromMinutes((double)_mainUserViewInstance.GetMinLockedTime())))
        {
            var ursprünglicheMinSize = _mainUserViewInstance.Size;
            _mainUserViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                _mainUserViewInstance,
                $"Der PC wurde Entsperrt. Willst du folgende Pause buchen?\nStart: {_pcLockedTime.Value}\nEnde: {_pcUnlockedTime.Value}",
                "4Time Autobook",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1
            );

            if (result == DialogResult.Yes)
            {
                Writer.Insert("Entries", new Entry
                {
                    UserID = await Task.Run(async () => Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).Result.First().UserID),
                    Start = _pcLockedTime.Value,
                    End = _pcUnlockedTime.Value,
                    CategoryID = 3,
                    Comment = "[AUTO]PC Lock - Unlock"
                });
            }
            _mainUserViewInstance.MinimumSize = ursprünglicheMinSize;
        }
    }
}