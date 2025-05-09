using _4Time.DataCore;
using _4Time.DataCore.Models;
using Microsoft.Win32;
using Time4SellersApp;
using System.Diagnostics;


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

        if (mainView == null)
        {
            throw new ArgumentNullException(nameof(mainView), "UserView-Instanz darf nicht null sein.");
        }

        _mainUserViewInstance = mainView;
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
                AutoBookCaller(); // Verwendet jetzt _mainUserViewInstance
                ShowPauseMessageBox(); // Verwendet jetzt _mainUserViewInstance
                break;
        }
    }

    private static void AutoBookCaller()
    {
        if (_mainUserViewInstance == null || _mainUserViewInstance._allEntrys == null)
        {
            Debug.WriteLine("AutoBookCaller: _mainUserViewInstance oder _allEntrys ist null.");
            return;
        }

        Entry? todaysFirstEntry = _mainUserViewInstance._allEntrys
            .Where(x => x.Start.Date == DateTime.Now.Date)
            .Where(x => x.Start.TimeOfDay < DateTime.Now.TimeOfDay)
            .OrderBy(x => x.Start).FirstOrDefault();

        Entry? lastEntry = _mainUserViewInstance._allEntrys
            .Where(x => x.End.Date == DateTime.Now.Date) // Beachte: Nur Einträge, die heute enden
            .Where(x => x.End.TimeOfDay < DateTime.Now.TimeOfDay)
            .OrderByDescending(x => x.End).FirstOrDefault();

        if (DateTime.Now.Hour < 12 && todaysFirstEntry == null)
            ShowFirstEntryMessageBox(null); // Explizit null übergeben, da todaysFirstEntry hier null ist
        else
            ShowLatestEntryMessageBox(lastEntry);
    }

    private static void ShowLatestEntryMessageBox(Entry? entry)
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

        var latestEntryEnd = entry.End; // Jetzt sicher, da entry nicht null ist
        var category = 1;
        string bookTypeString = "";

        if (DateTime.Now.Hour >= 12) // Korrektur: >= 12 für Nachmittag
        {
            bookTypeString = "Nachmittag"; category = 10;
        }
        else
        {
            bookTypeString = "Vormittag"; category = 9;
        }

        Thread.Sleep(500); // Überlege, ob Thread.Sleep hier wirklich nötig ist (blockiert den Event-Thread)
        if (_pcLockedTime.HasValue)
        {
            var ursprünglicheMinSize = _mainUserViewInstance.MinimumSize; // Verwende _mainUserViewInstance
            _mainUserViewInstance.MinimumSize = new System.Drawing.Size(0, 0);

            DialogResult result = MessageBox.Show(
                _mainUserViewInstance, // Wichtig: Owner für das MessageBox setzen, falls UserView ein Fenster ist
                $"Der PC wurde Entsperrt. Willst du folgende Zeit als '{bookTypeString}' buchen?\nStart: {latestEntryEnd}\nEnde: {_pcLockedTime.Value}",
                "4Time Autobook",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification // Kann UI blockieren, wenn aus einem Nicht-UI-Thread aufgerufen ohne Invoke
            );

            if (result == DialogResult.Yes)
            {
                _mainUserViewInstance.Neuladen.PerformClick(); // Verwende die übergebene Instanz

                Writer.Insert("Entries", new Entry
                {
                    UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                    Start = latestEntryEnd,
                    End = _pcLockedTime.Value,
                    CategoryID = category,
                    Comment = "[AUTO]Letzer Eintrag - Lock"
                });
                _mainUserViewInstance.Neuladen.PerformClick(); // Eventuell nochmal laden nach dem Insert
            }
            _mainUserViewInstance.MinimumSize = ursprünglicheMinSize; // Zurücksetzen
        }
    }

    private static void ShowFirstEntryMessageBox(Entry? todaysFirstEntry) // todaysFirstEntry ist hier immer null basierend auf AutoBookCaller
    {
        if (_mainUserViewInstance == null)
        {
            Debug.WriteLine("ShowFirstEntryMessageBox: _mainUserViewInstance ist null.");
            return;
        }

        if (DateTime.Now.Hour >= 12) // Korrektur: >= 12
            return;

        string bookTypeString = "Vormittag";
        TimeSpan pcUptime = TimeSpan.FromMilliseconds(Environment.TickCount); // Uptime seit Systemstart
        DateTime pcStartedAt = DateTime.Now - pcUptime; // Berechnet den Zeitpunkt des Systemstarts

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
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification
            );

            if (result == DialogResult.Yes)
            {
                _mainUserViewInstance.Neuladen.PerformClick(); // Verwende die übergebene Instanz

                Writer.Insert("Entries", new Entry
                {
                    UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                    Start = pcStartedAt,
                    End = _pcLockedTime.Value,
                    CategoryID = 9,
                    Comment = "[AUTO]PC Start - Lock"
                });
                _mainUserViewInstance.Neuladen.PerformClick();
            }
            _mainUserViewInstance.MinimumSize = ursprünglicheMinSize;
        }
    }

    private static void ShowPauseMessageBox()
    {
        if (_mainUserViewInstance == null)
        {
            Debug.WriteLine("ShowPauseMessageBox: _mainUserViewInstance ist null.");
            return;
        }

        Thread.Sleep(500);
        if (_pcLockedTime.HasValue && _pcUnlockedTime.HasValue &&
            (_pcUnlockedTime.Value - _pcLockedTime.Value > TimeSpan.FromMinutes((double)_mainUserViewInstance.GetMinLockedTime()))) // Zugriff auf Methode der Instanz
        {
            var ursprünglicheMinSize = _mainUserViewInstance.Size;
            _mainUserViewInstance.MinimumSize = new System.Drawing.Size(0, 0);
            DialogResult result = MessageBox.Show(
                _mainUserViewInstance,
                $"Der PC wurde Entsperrt. Willst du folgende Pause buchen?\nStart: {_pcLockedTime.Value}\nEnde: {_pcUnlockedTime.Value}",
                "4Time Autobook",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification
            );

            if (result == DialogResult.Yes)
            {
                _mainUserViewInstance.Neuladen.PerformClick(); // Verwende die übergebene Instanz

                Writer.Insert("Entries", new Entry
                {
                    UserID = Reader.Read<User>("User", ["[UserID]"], [$"[FirstName] = '{Connector.FirstName}'", $"[LastName] = '{Connector.LastName}'"]).First().UserID,
                    Start = _pcLockedTime.Value,
                    End = _pcUnlockedTime.Value,
                    CategoryID = 3,
                    Comment = "[AUTO]PC Lock - Unlock"
                });
                _mainUserViewInstance.Neuladen.PerformClick();
            }
            _mainUserViewInstance.MinimumSize = ursprünglicheMinSize;
        }
    }
}