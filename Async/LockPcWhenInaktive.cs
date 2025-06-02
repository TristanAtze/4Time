using System;
using System.Runtime.InteropServices;

public static class LockPcWhenInaktive
{
    [StructLayout(LayoutKind.Sequential)]
    private struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    [DllImport("user32.dll")]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [DllImport("user32.dll")]
    private static extern bool LockWorkStation();

    private static System.Threading.Timer _inactivityTimer;
    private static uint _currentInactivityThresholdMilliseconds;
    private static bool _isMonitoring = false;
    private static readonly int _checkIntervalMilliseconds = 1000; 
    /// <summary>
    /// Legt die Zeitspanne fest, nach der der PC bei Inaktivität gesperrt werden soll.
    /// Das Aufrufen dieser Methode beendet jede vorherige Inaktivitätsüberwachung und startet
    /// eine neue mit der angegebenen Dauer.
    /// </summary>
    /// <param name="minutesToLock">Die Dauer der Inaktivität in Minuten.
    /// Wenn 0 oder ein negativer Wert angegeben wird, wird die Überwachung gestoppt.</param>
    public static void SetLockPcTime(decimal minutesToLock)
    {
        _inactivityTimer?.Dispose();
        _inactivityTimer = null;
        _isMonitoring = false;

        if (minutesToLock <= 0)
        {
            MessageBox.Show("Inactivity monitoring gestoppt.", "LockPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _currentInactivityThresholdMilliseconds = 0;
            return;
        }

        _currentInactivityThresholdMilliseconds = (uint)(minutesToLock * 60 * 1000);

        _inactivityTimer = new System.Threading.Timer(TimerCallbackMethod, null, _checkIntervalMilliseconds, _checkIntervalMilliseconds);
        _isMonitoring = true;
    }

    /// <summary>
    /// Stoppt die Überwachung der Benutzerinaktivität explizit.
    /// </summary>
    public static void StopMonitoring()
    {
        _inactivityTimer?.Dispose();
        _inactivityTimer = null;
        _isMonitoring = false;
        _currentInactivityThresholdMilliseconds = 0;
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
            return 0;
        }
        return (uint)Environment.TickCount - lastInputInfo.dwTime;
    }

    /// <summary>
    /// Callback-Methode für den System.Threading.Timer.
    /// Wird in einem ThreadPool-Thread ausgeführt.
    /// </summary>
    private static void TimerCallbackMethod(object stateInfo)
    {
        if (!_isMonitoring || _currentInactivityThresholdMilliseconds == 0 || _inactivityTimer == null)
        {
            return;
        }

        uint idleTimeMilliseconds = GetLastInputTimeMilliseconds();

        if (idleTimeMilliseconds >= _currentInactivityThresholdMilliseconds)
        {
            LockPC();

            _isMonitoring = false;
            _inactivityTimer?.Dispose();
            _inactivityTimer = null;    
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
                MessageBox. Show("Fehler beim sperren der Workstation.", "LockPC", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Sperren des PCs: {ex.Message}", "LockPC", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}