using _4Time.DataCore.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System.Timers;

namespace _4Time.FrontEnd;

internal class NotificationManager
{
    private readonly List<Entry> _allEntrys;
    private readonly List<Category> _allCategorys;

    public NotificationManager(List<Entry> allEntrys, List<Category> allCategorys)
    {
        _allEntrys = allEntrys;
        _allCategorys = allCategorys;
        TimerSetup();
    }

    private static void SendNotification(object? sender, ElapsedEventArgs e)
    {
        new ToastContentBuilder()
            .AddText("Es ist Zeit für eine Pause")
            .Show(toast =>
            {
                toast.ExpirationTime = DateTime.Now.AddMinutes(15);
            });
    }

    private void TimerSetup()
    {
        System.Timers.Timer timer = new();
        timer.Elapsed += SendNotification;
        timer.AutoReset = false;
        timer.Enabled = true;

        int interval = 0;
        List<Entry> breakTimeToday = [.._allEntrys
            .Where(x => _allCategorys.Where(y => y.CategoryID == x.CategoryID).First().IsWorkTime == false)
            .Where(x => x.Start.Date == DateTime.Now.Date)];

        if (breakTimeToday.Any(x => x.End.Date == DateTime.Now.Date))
        {
            DateTime endOfBreak  = breakTimeToday.Select(x => x.End).Max();

            TimeSpan timeSpan = DateTime.Now - endOfBreak;

            interval = timeSpan.Milliseconds;
        }
        else
            interval = TimeSpan.FromHours(4.5).Milliseconds - Environment.TickCount;

        timer.Interval = interval > 0 ? interval : 1;
        timer.Start();
    }
}