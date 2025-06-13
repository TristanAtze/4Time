using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;

public class YouTubeShortsBlocker
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    private static bool _isRunning = false;
    private static CancellationTokenSource _cancellationTokenSource;

    public static async Task StartMonitoringAsync()
    {
        if (_isRunning)
        {
            Console.WriteLine("Überwachung läuft bereits.");
            return;
        }

        _isRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();
        Console.WriteLine("Überwachung gestartet. Drücke eine beliebige Taste zum Stoppen.");

        try
        {
            await Task.Run(() => MonitorBrowser(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Überwachung abgebrochen.");
        }
        finally
        {
            _isRunning = false;
        }
    }

    public static void StopMonitoring()
    {
        if (_isRunning && _cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
        }
    }

    private static void MonitorBrowser(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            IntPtr foregroundWindowHandle = GetForegroundWindow();
            if (foregroundWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(1000);
                continue;
            }

            uint processId;
            GetWindowThreadProcessId(foregroundWindowHandle, out processId);
            Process process = null;
            try
            {
                process = Process.GetProcessById((int)processId);
            }
            catch (ArgumentException)
            {
                Thread.Sleep(1000);
                continue;
            }

            string processName = process.ProcessName.ToLower();
            string windowTitle = GetActiveWindowTitle(foregroundWindowHandle);

            if (processName.Contains("chrome") || processName.Contains("firefox") || processName.Contains("msedge"))
            {
                Console.WriteLine($"Aktives Browser-Fenster erkannt: {processName} (PID: {processId}), Titel: {windowTitle}");
                string url = GetBrowserUrl(foregroundWindowHandle, processName) ?? string.Empty;

                if (!string.IsNullOrEmpty(url))
                {
                    Console.WriteLine($"Extrahierte URL: {url}");
                    if (url.Contains("youtube.com/shorts/.../") || url.Contains("youtube.com/shorts/"))
                    {
                        Console.WriteLine($"YouTube Shorts erkannt in {processName} (PID: {processId}). Beende Prozess...");
                        try
                        {
                            process.Kill();
                            Console.WriteLine($"Prozess {processName} (PID: {processId}) wurde beendet.");
                            Thread.Sleep(5000);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fehler beim Beenden des Prozesses {processName} (PID: {processId}): {ex.Message}");
                        }
                    }
                }
            }

            Thread.Sleep(2000);
        }
    }

    private static string GetActiveWindowTitle(IntPtr hWnd)
    {
        const int nChars = 256;
        StringBuilder buff = new StringBuilder(nChars);
        return GetWindowText(hWnd, buff, nChars) > 0 ? buff.ToString() : string.Empty;
    }

    private static string? GetBrowserUrl(IntPtr windowHandle, string processName)
    {
        try
        {
            AutomationElement rootElement = AutomationElement.FromHandle(windowHandle);
            if (rootElement == null) return null;

            Condition conditions = null;
            if (processName.Contains("chrome") || processName.Contains("msedge"))
            {
                // Für Chrome/Edge: Oft ein Element mit dem Namen "Address and search bar" oder eine bestimmte ControlType.Edit
                // Es kann auch notwendig sein, tiefer im UI-Baum zu suchen.
                // AutomationID kann sich auch ändern. Manchmal ist es "view_id:omnibox_edit_view" oder ähnlich.
                // Diese Implementierung ist stark vereinfacht.
                conditions = new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                    new OrCondition( // Versuche verschiedene gängige Namen
                        new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"),
                        new PropertyCondition(AutomationElement.NameProperty, "Adress- und Suchleiste") // Deutsch
                                                                                                        // Weitere NameProperties oder AutomationIDs könnten hier hinzugefügt werden
                    )
                );
                // Alternative, falls Name nicht zuverlässig ist: Nach der ersten fokussierbaren Edit-Box in einer Toolbar suchen.
                // var toolBar = rootElement.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar));
                // if (toolBar != null) {
                //    var addressBar = toolBar.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                //    if (addressBar != null && addressBar.TryGetCurrentPattern(ValuePattern.Pattern, out object valuePatternObj)) {
                //        return ((ValuePattern)valuePatternObj).Current.Value;
                //    }
                // }
            }
            else if (processName.Contains("firefox"))
            {
                // Für Firefox: Ähnlich, oft eine Edit-Box innerhalb einer Toolbar.
                // Die genauen Properties können mit Tools wie "Inspect.exe" (Teil des Windows SDK) ermittelt werden.
                conditions = new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                     new OrCondition(
                        new PropertyCondition(AutomationElement.AutomationIdProperty, "urlbar-input"), // Häufige AutomationID
                        new PropertyCondition(AutomationElement.NameProperty, "Search or enter address") // Englischer Name
                                                                                                         // new PropertyCondition(AutomationElement.NameProperty, "Suchen oder Adresse eingeben") // Deutscher Name
                     )
                );
            }

            if (conditions == null) return null;

            AutomationElement addressBarElement = rootElement.FindFirst(TreeScope.Descendants, conditions);

            if (addressBarElement != null)
            {
                if (addressBarElement.TryGetCurrentPattern(ValuePattern.Pattern, out object valuePatternObj))
                {
                    return ((ValuePattern)valuePatternObj).Current.Value;
                }
                // Manchmal ist der Wert auch im LegacyIAccessiblePattern
                //if (addressBarElement.TryGetCurrentPattern(LegacyIAccessiblePattern.Pattern, out object legacyPatternObj))
                //{
                //    return ((LegacyIAccessiblePattern)legacyPatternObj).Current.Value;
                //}
            }
            else
            {
                // Fallback: Versuche, alle Edit-Felder zu finden und zu prüfen, ob der Inhalt wie eine URL aussieht.
                // Dies ist noch unzuverlässiger.
                var editBoxes = rootElement.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                foreach (AutomationElement editBox in editBoxes)
                {
                    if (editBox.TryGetCurrentPattern(ValuePattern.Pattern, out object valuePatternObj))
                    {
                        string potentialUrl = ((ValuePattern)valuePatternObj).Current.Value;
                        if (!string.IsNullOrWhiteSpace(potentialUrl) && (potentialUrl.StartsWith("http") || potentialUrl.StartsWith("www")))
                        {
                            // Prüfe, ob das Element sichtbar ist, um versteckte Felder zu ignorieren
                            if (editBox.Current.IsOffscreen == false)
                            {
                                Console.WriteLine($"Fallback URL gefunden: {potentialUrl}");
                                return potentialUrl;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Extrahieren der URL via UI Automation: {ex.Message}");
            // UI Automation kann fehlschlagen, wenn das Fenster nicht bereit ist oder die Struktur unerwartet ist.
        }
        return null;
    }

    // Hauptmethode zum Testen
    // public static void Main(string[] args)
    // {
    //     _ = StartMonitoringAsync(); // Starte die Überwachung ohne auf den Abschluss zu warten
    //     Console.ReadKey(); // Warte auf Tastendruck zum Beenden
    //     StopMonitoring();
    //     Console.WriteLine("Programm beendet.");
    // }
}