using _4Time.DataCore.Models; // Annahme: Diese using-Direktive ist weiterhin notwendig
using Microsoft.Toolkit.Uwp.Notifications;

// Behelfsklasse für CheckBox, falls der Originaltyp nicht bekannt ist.
// Ersetze dies durch deinen tatsächlichen CheckBox-Typ oder eine bool-Eigenschaft.
public class CheckBoxStub
{
    public bool Checked { get; set; }
}

internal class NotificationManager
{
    private readonly List<Entry> _allEntrys = [];
    private readonly List<Category> _allCategorys;
    public static DateTime startPauseAt; 

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="NotificationManager"/> Klasse.
    /// Richtet Benachrichtigungen basierend auf Arbeitszeiten und Pausen ein.
    /// </summary>
    /// <param name="allEntrys">Eine Liste aller Zeiteinträge. Es wird angenommen, dass diese Liste aktuell gehalten wird oder eine Referenz darauf ist.</param>
    /// <param name="allCategorys">Eine Liste aller Kategorien.</param>
    /// <param name="preNotifyCheckBox">Eine CheckBox, die angibt, ob eine Vorab-Benachrichtigung gesendet werden soll.</param>
    public NotificationManager(DataGridView dataGridView, List<Category> allCategorys, CheckBox preNotifyCheckBox, CheckBox is18)
    {
        for (int i = 0; i < dataGridView.Rows.Count; i++)
        {
            var row = dataGridView.Rows[i];
            var entry = new Entry
            {
                CategoryName = (string)row.Cells["colArt"].Value,
                Start = DateTime.Parse(row.Cells["colStart"].Value.ToString() ?? ""),
                End = DateTime.Parse(row.Cells["colEnd"].Value.ToString() ?? "")
            };
            _allEntrys.Add(entry);
        }
        _allCategorys = allCategorys ?? throw new ArgumentNullException(nameof(allCategorys));
        double maxWorkTime = 4.5;

        if (preNotifyCheckBox == null) throw new ArgumentNullException(nameof(preNotifyCheckBox));

        if (is18 != null && is18.Checked)
            maxWorkTime = 6.0;

        if (preNotifyCheckBox.Checked)
            _ = StartPreNotificationTaskAsync(maxWorkTime);

        _ = StartMainNotificationTaskAsync(maxWorkTime);
    }

    private static void SendSpecificToastNotification(string message, int expirationMinutes)
    {
        try
        {
            new ToastContentBuilder()
                .AddText(message)
                .Show(toast =>
                {
                    toast.ExpirationTime = DateTime.Now.AddMinutes(expirationMinutes);
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Senden der Benachrichtigung '{message}': {ex.Message}");
        }
    }

    /// <summary>
    /// Ermittelt die relevanten Pauseneinträge für den durch 'currentTime' definierten Tag.
    /// </summary>
    /// <param name="currentTime">Der aktuelle Zeitpunkt, der für Datumsvergleiche verwendet wird.</param>
    /// <returns>Eine Liste der relevanten beendeten Pauseneinträge.</returns>
    private Entry? GetRelevantBreaksToday(DateTime currentTime)
    {
        return _allEntrys?.Where(x => x.End.Date == DateTime.Now.Date).Where(x => x.End - x.Start > TimeSpan.FromMinutes(15)).OrderBy(e => Math.Abs((e.End - currentTime).Ticks))
            .FirstOrDefault();
    }

    /// <summary>
    /// Berechnet das Zeitintervall bis zur Vorab-Benachrichtigung.
    /// Die Basiszeit ist das Ende der letzten Pause heute oder der Systemstart.
    /// </summary>
    /// <returns>Die <see cref="TimeSpan"/> bis zur Vorab-Benachrichtigung.</returns>
    private TimeSpan CalculatePreNotifyInterval(double maxWorkTime)
    {
        DateTime now = DateTime.Now;
        DateTime systemStartTime = now - TimeSpan.FromMilliseconds(Environment.TickCount64);

        DateTime notificationBaseTime;
        Entry relevantBreaksToday = GetRelevantBreaksToday(now);

        if (relevantBreaksToday != null)
        {
            notificationBaseTime = relevantBreaksToday.End;
        }
        else
        {
            notificationBaseTime = systemStartTime;
        }

        DateTime preNotificationTargetTime = notificationBaseTime.AddHours(maxWorkTime).AddMinutes(-10);

        if (preNotificationTargetTime < now) return TimeSpan.MinValue;
        if (preNotificationTargetTime == now) return TimeSpan.Zero;
        return preNotificationTargetTime - now;
    }

    /// <summary>
    /// Startet die Vorab-Benachrichtigung, wenn die Zeit abgelaufen ist.
    /// </summary>
    /// <param name="maxWorkTime"></param>
    /// <returns></returns>
    private async Task StartPreNotificationTaskAsync(double maxWorkTime)
    {
        TimeSpan initialDelay = CalculatePreNotifyInterval(maxWorkTime);

        if (initialDelay < TimeSpan.Zero)
        {
            if (CalculatePreNotifyInterval(maxWorkTime) <= TimeSpan.Zero)
            {
                SendSpecificToastNotification("Bitte mache spätestens in 10 Minuten Pause.", 15);
            }
        }
        else if (initialDelay > TimeSpan.Zero)
        {
            await Task.Delay(initialDelay);
            if (CalculatePreNotifyInterval(maxWorkTime) <= TimeSpan.Zero)
            {
                SendSpecificToastNotification("Bitte mache spätestens in 10 Minuten Pause.", 15);
            }
        }
    }

    /// <summary>
    /// Berechnet das Zeitintervall bis zur Haupt-Benachrichtigung.
    /// Die Basiszeit ist das Ende der letzten Pause heute oder der Systemstart.
    /// </summary>
    /// <returns>Die <see cref="TimeSpan"/> bis zur Haupt-Benachrichtigung.</returns>
    private TimeSpan CalculateMainNotificationInterval(double maxWorkTime)
    {
        DateTime now = DateTime.Now;
        DateTime systemStartTime = now - TimeSpan.FromMilliseconds(Environment.TickCount64);

        DateTime notificationBaseTime;
        Entry? relevantBreaksToday = GetRelevantBreaksToday(now);

        if (relevantBreaksToday != null)
        {
            notificationBaseTime = relevantBreaksToday.End;
        }
        else
        {
            notificationBaseTime = systemStartTime;
        }

        DateTime mainNotificationTargetTime = notificationBaseTime.AddHours(maxWorkTime);

        startPauseAt = mainNotificationTargetTime;

        if (mainNotificationTargetTime < now) return TimeSpan.MinValue;
        if (mainNotificationTargetTime == now) return TimeSpan.Zero;
        return mainNotificationTargetTime - now;
    }

    private async Task StartMainNotificationTaskAsync(double maxWorkTime)
    {
        TimeSpan initialDelay = CalculateMainNotificationInterval(maxWorkTime);

        if (initialDelay < TimeSpan.Zero)
        {
            if (CalculateMainNotificationInterval(maxWorkTime) <= TimeSpan.Zero)
            {
                SendSpecificToastNotification("Bitte mache jetzt eine Pause.", 15);
            }
        }
        else if (initialDelay > TimeSpan.Zero)
        {
            await Task.Delay(initialDelay);
            if (CalculateMainNotificationInterval(maxWorkTime) <= TimeSpan.Zero)
            {
                SendSpecificToastNotification("Bitte mache jetzt eine Pause.", 15);
            }
        }
    }
}