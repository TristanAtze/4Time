using System;
using System.Runtime.InteropServices;

public static class LockPcWhenInaktive
{
    // Struktur für die letzte Eingabeinformation
    [StructLayout(LayoutKind.Sequential)]
    private struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    // Importiert die GetLastInputInfo-Funktion von user32.dll
    [DllImport("user32.dll")]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    // Importiert die LockWorkStation-Funktion von user32.dll
    [DllImport("user32.dll")]
    private static extern bool LockWorkStation();

    private static System.Windows.Forms.Timer _inactivityTimer;
    private static uint _currentInactivityThresholdMilliseconds;
    private static bool _isMonitoring = false;

    static LockPcWhenInaktive()
    {
        _inactivityTimer = new System.Windows.Forms.Timer();
        _inactivityTimer.Interval = 1000;
        _inactivityTimer.Tick += _inactivityTimer_Tick;
    }

    /// <summary>
    /// Legt die Zeitspanne fest, nach der der PC bei Inaktivität gesperrt werden soll.
    /// Das Aufrufen dieser Methode beendet jede vorherige Inaktivitätsüberwachung und startet
    /// eine neue mit der angegebenen Dauer.
    /// </summary>
    /// <param name="minutesToLock">Die Dauer der Inaktivität in Minuten.
    /// Wenn 0 oder ein negativer Wert angegeben wird, wird die Überwachung gestoppt.</param>
    public static void SetLockPcTime(decimal minutesToLock)
    {
        // Stoppe immer den aktuellen Timer, bevor neue Einstellungen vorgenommen werden.
        _inactivityTimer.Stop();
        _isMonitoring = false; 

        if (minutesToLock <= 0)
        {
            _currentInactivityThresholdMilliseconds = 0;
            MessageBox.Show("Inaktivitätsüberwachung wurde gestoppt.", "Inaktivitätsüberwachung", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return; 
        }

        _currentInactivityThresholdMilliseconds = (uint)(minutesToLock * 60 * 1000);
        _inactivityTimer.Start();
        _isMonitoring = true;
    }

    /// <summary>
    /// Stoppt die Überwachung der Benutzerinaktivität explizit.
    /// </summary>
    public static void StopMonitoring()
    {
        _inactivityTimer.Stop();
        _isMonitoring = false;
        _currentInactivityThresholdMilliseconds = 0;
        MessageBox.Show("Inaktivitätsüberwachung wurde gestoppt.", "Inaktivitätsüberwachung", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    /// <summary>
    /// Ruft die Zeitspanne in Millisekunden ab, seit der letzten Benutzereingabe.
    /// </summary>
    private static uint GetLastInputTimeMilliseconds()
    {
        LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
        lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

        if (!GetLastInputInfo(ref lastInputInfo))
        {
            // Simuliert im Fehlerfall eine Aktivität
            return 0;
        }
        return (uint)Environment.TickCount - lastInputInfo.dwTime;
    }

    /// <summary>
    /// Wird bei jedem Tick des Timers aufgerufen, um die Inaktivität zu überprüfen.
    /// </summary>
    private static void _inactivityTimer_Tick(object sender, EventArgs e)
    {
        if (!_isMonitoring || _currentInactivityThresholdMilliseconds == 0)
        {
            return;
        }

        uint idleTimeMilliseconds = GetLastInputTimeMilliseconds();

        if (idleTimeMilliseconds >= _currentInactivityThresholdMilliseconds)
        {
            LockPC();
            _inactivityTimer.Stop(); 
            _isMonitoring = false;
        }
    }

    /// <summary>
    /// Sperrt den Computer.
    /// </summary>
    private static void LockPC()
    {
        try
        {
            if (!LockWorkStation())
            {
                MessageBox.Show("Fehler beim Sperren des PCs. Möglicherweise haben Sie nicht die erforderlichen Berechtigungen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Console.WriteLine("PC erfolgreich gesperrt.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Fehler beim Sperren des PCs: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}