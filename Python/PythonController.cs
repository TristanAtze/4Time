using System.Diagnostics;

namespace _4Time.Python
{
    class SpeechToTextController
    {
        private static Process SpeechToTextythonProcess;
        private static readonly string pythonInterpreterPath = "python";
        private static readonly string scriptPath = @"Python\SpeechToText.py";

        /// <summary>
        /// Wird ausgelöst, wenn das Python-Skript erkannten Text sendet.
        /// Der String-Parameter enthält den erkannten Text.
        /// </summary>
        public static event Action<string> OnTextReceived;

        /// <summary>
        /// Wird ausgelöst, wenn das Python-Skript eine Fehlermeldung sendet oder ein Fehler im Caller auftritt.
        /// </summary>
        public static event Action<string> OnErrorOccurred;

        public static void SpeechToTextCaller()
        {
            if (SpeechToTextythonProcess != null && !SpeechToTextythonProcess.HasExited)
            {
                OnErrorOccurred?.Invoke("Der Spracherkennungsprozess läuft bereits.");
                return;
            }

            if (!File.Exists(scriptPath))
            {
                OnErrorOccurred?.Invoke($"Fehler: Python-Skript nicht gefunden unter: {Path.GetFullPath(scriptPath)}");
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonInterpreterPath,
                Arguments = $"\"{Path.GetFullPath(scriptPath)}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            SpeechToTextythonProcess = new Process { StartInfo = startInfo };

            SpeechToTextythonProcess.OutputDataReceived += (sender, e) =>
            {
                Debug.WriteLine($"DEBUG PythonCaller.OutputDataReceived: Empfangene Daten = '{e.Data}'"); 
                if (e.Data != null)
                {
                    Debug.WriteLine($"DEBUG PythonCaller: OnTextErkannt wird ausgelöst mit '{e.Data}'");
                    OnTextReceived?.Invoke(e.Data);
                }
            };

            SpeechToTextythonProcess.ErrorDataReceived += (sender, e) =>
            {
                Debug.WriteLine($"DEBUG PythonCaller.ErrorDataReceived: Empfangene Fehlerdaten = '{e.Data}'");
                if (e.Data != null)
                {
                    OnErrorOccurred?.Invoke($"{e.Data}");
                }
            };

            try
            {
                SpeechToTextythonProcess.Start();
                Debug.WriteLine("DEBUG PythonCaller: Prozess gestartet.");
                SpeechToTextythonProcess.BeginOutputReadLine();
                Debug.WriteLine("DEBUG PythonCaller: BeginOutputReadLine aufgerufen."); 
                SpeechToTextythonProcess.BeginErrorReadLine();
                Debug.WriteLine("DEBUG PythonCaller: BeginErrorReadLine aufgerufen."); 

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DEBUG PythonCaller: FEHLER beim Starten des Prozesses - {ex.Message}"); 
                OnErrorOccurred?.Invoke($"Fehler beim Starten des Python-Skripts: {ex.Message}");
                SpeechToTextythonProcess?.Dispose();
                SpeechToTextythonProcess = null;
            }
        }

        public static void StopSpeechToText()
        {
            if (SpeechToTextythonProcess != null && !SpeechToTextythonProcess.HasExited)
            {
                try
                {
                    SpeechToTextythonProcess.Kill();
                    SpeechToTextythonProcess.WaitForExit(3500); 

                    if (!SpeechToTextythonProcess.HasExited)
                    {
                        OnErrorOccurred?.Invoke("Python-Prozess konnte nicht sofort beendet werden.");
                    }
                }
                catch (InvalidOperationException)
                {
                    OnErrorOccurred?.Invoke("Spracherkennung war bereits gestoppt oder konnte nicht adressiert werden.");
                }
                catch (Exception ex)
                {
                    OnErrorOccurred?.Invoke($"Fehler beim Beenden des Python-Skripts: {ex.Message}");
                }
                finally
                {
                    SpeechToTextythonProcess?.Dispose();
                    SpeechToTextythonProcess = null;
                }
            }
        }
    }

    public class WebcamLockController
    {
        private static Process pythonProcess;
        private static readonly string pythonInterpreterPath = "python"; // Oder der volle Pfad zu python.exe
        private static readonly string scriptName = "WebcamPresenceLock.py"; // Name des Python-Skripts
        private static readonly string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Python", scriptName);


        /// <summary>
        /// Wird ausgelöst, wenn das Python-Skript eine Statusmeldung sendet.
        /// </summary>
        public static event Action<string> OnPythonOutput;

        /// <summary>
        /// Wird ausgelöst, wenn das Python-Skript eine Fehlermeldung sendet oder ein Fehler im Controller auftritt.
        /// </summary>
        public static event Action<string> OnPythonError;

        public static bool IsRunning => pythonProcess != null && !pythonProcess.HasExited;

        public static void StartWebcamDetection(int secondsToLock)
        {
            if (IsRunning)
            {
                OnPythonError?.Invoke("INFO: Webcam-Überwachung läuft bereits.");
                return;
            }

            if (!File.Exists(scriptPath))
            {
                OnPythonError?.Invoke($"ERROR: Python-Skript nicht gefunden unter: {Path.GetFullPath(scriptPath)}");
                return;
            }
            if (secondsToLock <= 0)
            {
                secondsToLock = 10;
                OnPythonError?.Invoke($"WARNING: Ungültiger Wert für secondsToLock ({secondsToLock}). Muss größer 0 sein.");
                // Hier könnte man einen Standardwert setzen oder abbrechen.
                // Fürs Erste brechen wir ab.
                return;
            }


            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonInterpreterPath,
                Arguments = $"\"{Path.GetFullPath(scriptPath)}\" {secondsToLock}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true, 
                WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(scriptPath)) ?? AppDomain.CurrentDomain.BaseDirectory
            };

            pythonProcess = new Process { StartInfo = startInfo };

            pythonProcess.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Debug.WriteLine($"Python Output: {e.Data}"); 
                    OnPythonOutput?.Invoke(e.Data);
                }
            };

            pythonProcess.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Debug.WriteLine($"Python Error: {e.Data}");
                    OnPythonError?.Invoke(e.Data);
                }
            };

            try
            {
                pythonProcess.Start();
                OnPythonOutput?.Invoke("INFO: Webcam-Überwachungsprozess gestartet.");
                pythonProcess.BeginOutputReadLine();
                pythonProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                OnPythonError?.Invoke($"ERROR: Fehler beim Starten des Python-Skripts: {ex.Message}");
                pythonProcess?.Dispose();
                pythonProcess = null;
            }
        }

        public static void StopWebcamDetection()
        {
            if (!IsRunning)
            {
                OnPythonOutput?.Invoke("INFO: Webcam-Überwachung lief nicht.");
                return;
            }

            try
            {
                // Sanfter Versuch, das Fenster zu schließen (wenn das Python-Skript darauf reagieren würde)
                // if (pythonProcess.MainWindowHandle != IntPtr.Zero)
                // {
                //     pythonProcess.CloseMainWindow(); // Sendet WM_CLOSE
                //     if (pythonProcess.WaitForExit(1000)) // Warte kurz
                //     {
                //         OnPythonOutput?.Invoke("INFO: Webcam-Überwachungsprozess ordnungsgemäß beendet.");
                //         CleanUpProcess();
                //         return;
                //     }
                // }

                // Harter Stopp, wenn nötig
                pythonProcess.Kill(true); // Gesamten Prozessbaum killen
                pythonProcess.WaitForExit(3500); 

                OnPythonOutput?.Invoke("INFO: Webcam-Überwachungsprozess gestoppt (Kill).");
            }
            catch (InvalidOperationException)
            {
                // Prozess ist möglicherweise bereits beendet
                OnPythonOutput?.Invoke("INFO: Webcam-Überwachung war bereits gestoppt oder konnte nicht adressiert werden.");
            }
            catch (Exception ex)
            {
                OnPythonError?.Invoke($"ERROR: Fehler beim Beenden des Python-Skripts: {ex.Message}");
            }
            finally
            {
                CleanUpProcess();
            }
        }

        private static void CleanUpProcess()
        {
            if (pythonProcess != null)
            {
                pythonProcess.OutputDataReceived -= (sender, e) => { /* Handler entfernen oder Logik hier */ };
                pythonProcess.ErrorDataReceived -= (sender, e) => { /* Handler entfernen oder Logik hier */ };
                pythonProcess.Dispose();
                pythonProcess = null;
            }
        }
    }
}