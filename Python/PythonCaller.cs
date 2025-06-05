using System.Diagnostics;

namespace _4Time.Python
{
    class PythonCaller
    {
        private static Process pythonProcess;
        private static readonly string pythonInterpreterPath = "python";
        private static readonly string scriptPath = @"Python\SpeechToText.py";

        /// <summary>
        /// Wird ausgelöst, wenn das Python-Skript erkannten Text sendet.
        /// Der String-Parameter enthält den erkannten Text.
        /// </summary>
        public static event Action<string> OnTextErkannt;

        /// <summary>
        /// Wird ausgelöst, wenn das Python-Skript eine Fehlermeldung sendet oder ein Fehler im Caller auftritt.
        /// </summary>
        public static event Action<string> OnErrorOccurred;

        public static void SpeechToTextCaller()
        {
            if (pythonProcess != null && !pythonProcess.HasExited)
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

            pythonProcess = new Process { StartInfo = startInfo };

            pythonProcess.OutputDataReceived += (sender, e) =>
            {
                Debug.WriteLine($"DEBUG PythonCaller.OutputDataReceived: Empfangene Daten = '{e.Data}'"); 
                if (e.Data != null)
                {
                    Debug.WriteLine($"DEBUG PythonCaller: OnTextErkannt wird ausgelöst mit '{e.Data}'");
                    OnTextErkannt?.Invoke(e.Data);
                }
            };

            pythonProcess.ErrorDataReceived += (sender, e) =>
            {
                Debug.WriteLine($"DEBUG PythonCaller.ErrorDataReceived: Empfangene Fehlerdaten = '{e.Data}'");
                if (e.Data != null)
                {
                    OnErrorOccurred?.Invoke($"Python Fehler: {e.Data}");
                }
            };

            try
            {
                pythonProcess.Start();
                Debug.WriteLine("DEBUG PythonCaller: Prozess gestartet.");
                pythonProcess.BeginOutputReadLine();
                Debug.WriteLine("DEBUG PythonCaller: BeginOutputReadLine aufgerufen."); 
                pythonProcess.BeginErrorReadLine();
                Debug.WriteLine("DEBUG PythonCaller: BeginErrorReadLine aufgerufen."); 

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DEBUG PythonCaller: FEHLER beim Starten des Prozesses - {ex.Message}"); 
                OnErrorOccurred?.Invoke($"Fehler beim Starten des Python-Skripts: {ex.Message}");
                pythonProcess?.Dispose();
                pythonProcess = null;
            }
        }

        public static void StopSpeechToText()
        {
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                try
                {
                    pythonProcess.Kill();
                    pythonProcess.WaitForExit(2000); 

                    if (!pythonProcess.HasExited)
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
                    pythonProcess?.Dispose();
                    pythonProcess = null;
                }
            }
        }
    }
}