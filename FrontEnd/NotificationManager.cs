using _4Time.DataCore.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System.Timers;

namespace _4Time.FrontEnd;

internal class NotificationManager
{
    private readonly List<Entry> _allEntrys;
    private readonly List<Category> _allCategorys;
    private readonly List<Entry> breakTimeToday;
    private static bool _preNotify = false;
    private static bool _preNotifyActive = false;

    public NotificationManager(List<Entry> allEntrys, List<Category> allCategorys, CheckBox preNotify)
    {
        _preNotifyActive = preNotify.Checked;
        _preNotify = preNotify.Checked;
        _allEntrys = allEntrys;
        _allCategorys = allCategorys;
        breakTimeToday = [.. _allEntrys
            .Where(x => _allCategorys.Where(y => y.CategoryID == x.CategoryID).First().IsWorkTime == false)
            .Where(x => x.Start.Date == DateTime.Now.Date)];

        TimerSetup();
        if (preNotify.Checked)
        {
            PreNotify();
        }
    }

    private static void SendNotification(object? sender, ElapsedEventArgs e)
    {
        string notifyText = "";

        if (_preNotify)
        {
            notifyText = "Bitte mache spätestens in 10 min Pause.";
            _preNotify = false;
        }
        else
        {
            notifyText = "Bitte mache eine Pause.";

            if (!_preNotifyActive)
            {
                _preNotify = true;
            }
        }

        new ToastContentBuilder()
            .AddText(notifyText)
            .Show(toast =>
            {
                toast.ExpirationTime = DateTime.Now.AddMinutes(15);
            });
    }

    private void PreNotify()
    {
        System.Timers.Timer timer = new();
        timer.Elapsed += SendNotification;
        timer.AutoReset = true;
        timer.Enabled = true;

        int interval = 0;
        
        if (breakTimeToday.Any(x => x.End.Date == DateTime.Now.Date))
        {
            DateTime endOfBreak = breakTimeToday.Select(x => x.End).Max();

            TimeSpan timeSpan = DateTime.Now - endOfBreak.AddMinutes(-10);

            interval = (int)timeSpan.TotalMilliseconds;
        }
        else
            interval = (int)TimeSpan.FromHours(4.5).TotalMilliseconds - Environment.TickCount;

        timer.Interval = interval > 0 ? interval : 1;
        timer.Start();
    }

    private void TimerSetup()
    {
        System.Timers.Timer timer = new();
        timer.Elapsed += SendNotification;
        timer.AutoReset = true;
        timer.Enabled = true;

        int interval = 0;

        if (breakTimeToday.Any(x => x.End.Date == DateTime.Now.Date))
        {
            DateTime endOfBreak  = breakTimeToday.Select(x => x.End).Max();

            TimeSpan timeSpan = DateTime.Now - endOfBreak;

            interval = (int)timeSpan.TotalMilliseconds;
        }
        else
            interval = (int)(TimeSpan.FromHours(4.5).TotalMilliseconds - Environment.TickCount);

        timer.Interval = interval > 0 ? interval : 1;
        timer.Start();
    }
}