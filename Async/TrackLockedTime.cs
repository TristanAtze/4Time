using _4Time.DataCore;
using _4Time.DataCore.Models;
using Microsoft.Win32;
using Time4SellersApp;

namespace _4Time.Async;

internal class TrackLockedTime
{
    public static DateTime? _pcLockedTime;
    public static DateTime? _pcUnlockedTime;

    private static readonly UserView _userViewInstance = new(); // Create an instance of UserView

    public static void TrackLockedTimeStartAsync()
    {
        SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

        while (true)
        {
            
        }
        //SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
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
                ShowMessageBox();
                break;
        }
    }

    private static void ShowMessageBox()
    {
        Thread.Sleep(500);
        if (_pcLockedTime.HasValue && _pcUnlockedTime.HasValue &&
            (_pcUnlockedTime - _pcLockedTime > TimeSpan.FromMinutes((double)_userViewInstance.GetMinLockedTime())))
        {
            var size = _userViewInstance.Size;
            _userViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                 $"Der Pc wurde Entsperrt. Willst du folgende Zeit buchen?\nStart: {_pcLockedTime}\nEnde: {_pcUnlockedTime}",
                 "Pc Sperr-Event",                                                                                         
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
                    Comment = "PC Lock/Unlock"
                });
            }
            _userViewInstance.MinimumSize = size;
        }
    }
}
