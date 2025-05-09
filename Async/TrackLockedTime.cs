using _4Time.DataCore;
using _4Time.DataCore.Models;
using Microsoft.Win32;
using Time4SellersApp;

namespace _4Time.Async;

internal class TrackLockedTime
{
    public static DateTime? _pcLockedTime;
    public static DateTime? _pcUnlockedTime;

    private static readonly UserView _userViewInstance = new(); 

    public static void TrackLockedTimeStartAsync()
    {
        SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

        while (true){ }
    }

    private static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
    {
        switch (e.Reason)
        {
            case SessionSwitchReason.SessionLock:
                _pcLockedTime = DateTime.Now;
                break;

            case SessionSwitchReason.SessionUnlock:
                _pcUnlockedTime = DateTime.Now;
                AutoBookCaller();
                ShowPauseMessageBox();
                break;
        }
    }

    public static void AutoBookCaller()
    {
        Entry? todaysFirstEntry = _userViewInstance._allEntrys
            .Where(x => x.Start.Date == DateTime.Now.Date)
            .Where(x => x.Start.TimeOfDay < DateTime.Now.TimeOfDay)
            .OrderBy(x => x.Start).FirstOrDefault();

        Entry? lastEntry = _userViewInstance._allEntrys.Where(x => x.End.Date == DateTime.Now.Date)
            .Where(x => x.End.TimeOfDay < DateTime.Now.TimeOfDay)
            .OrderByDescending(x => x.End).FirstOrDefault();

        if (DateTime.Now.Hour < 12 && todaysFirstEntry == null)
            ShowFirstEntryMessageBox(todaysFirstEntry);
        else
            ShowLatestEntryMessageBox(lastEntry);
    }

    private static void ShowLatestEntryMessageBox(Entry? entry)
    {
        var latestEntryEnd = entry.End;
        var category = 1;
        string bookTypeString = "";

        if (DateTime.Now.Hour > 12) {bookTypeString = "Nachmittag"; category = 10;}
        else bookTypeString = "Vormittag"; category = 9;

        Thread.Sleep(500);
        if (_pcLockedTime.HasValue)
        {
            var size = _userViewInstance.Size;
            _userViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                 $"Der Pc wurde Entsperrt. Willst du folgende Zeit als '{bookTypeString}' buchen?\nStart: {latestEntryEnd}\nEnde: {_pcLockedTime}",
                 "4Time Autobook",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.ServiceNotification
             );

            if (result == DialogResult.Yes)
            {
                UserView userView = new();
                userView.Neuladen.PerformClick();
                Writer.Insert("Entries", new Entry
                {
                    UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                    Start = latestEntryEnd,
                    End = _pcLockedTime.Value,
                    CategoryID = category,
                    Comment = "[AUTO]Letzer Eintrag - Lock"
                });
            }
            _userViewInstance.MinimumSize = size;
        }

    }

    private static void ShowFirstEntryMessageBox(Entry? todaysFirstEntry)
    {
        string bookTypeString = "Vormittag";

        if(DateTime.Now.Hour > 12 || todaysFirstEntry != null)
            return;


        TimeSpan pcStartTime = TimeSpan.FromMilliseconds(Environment.TickCount);

        DateTime pcStartedAt = DateTime.Now - pcStartTime;

        Thread.Sleep(500);
        if (_pcLockedTime.HasValue)
        {
            var size = _userViewInstance.Size;
            _userViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                 $"Der Pc wurde Entsperrt. Willst du folgende Zeit als '{bookTypeString}' buchen?\nStart: {pcStartedAt}\nEnde: {_pcLockedTime}",
                 "4Time Autobook",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.ServiceNotification
             );

            if (result == DialogResult.Yes)
            {
                UserView userView = new();
                userView.Neuladen.PerformClick();
                Writer.Insert("Entries", new Entry
                {
                    UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                    Start = pcStartedAt,
                    End = _pcLockedTime.Value,
                    CategoryID = 9,
                    Comment = "[AUTO]PC Start - Lock"
                });
            }
            _userViewInstance.MinimumSize = size;
        }
    }

    private static void ShowPauseMessageBox()
    {
        Thread.Sleep(500);
        if (_pcLockedTime.HasValue && _pcUnlockedTime.HasValue &&
            (_pcUnlockedTime - _pcLockedTime > TimeSpan.FromMinutes((double)_userViewInstance.GetMinLockedTime())))
        {
            var size = _userViewInstance.Size;
            _userViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                 $"Der Pc wurde Entsperrt. Willst du folgende Pause buchen?\nStart: {_pcLockedTime}\nEnde: {_pcUnlockedTime}",
                 "4Time Autobook",                                                                                         
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.ServiceNotification
             );

            if (result == DialogResult.Yes)
            {
                UserView userView = new();
                userView.Neuladen.PerformClick();
                Writer.Insert("Entries", new Entry
                {
                    UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                    Start = _pcLockedTime.Value,
                    End = _pcUnlockedTime.Value,
                    CategoryID = 3, 
                    Comment = "[AUTO]PC Lock - Unlock"
                });
            }
            _userViewInstance.MinimumSize = size;
        }
    }
}
