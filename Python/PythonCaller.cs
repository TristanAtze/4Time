using System;
using System.Diagnostics;
using System.IO; // Für Path.Combine (optional aber empfohlen)
using System.Threading.Tasks; // Für asynchrones Lesen

namespace _4Time.Python;
class PythonCaller
{
    public static async void SpeechToTextCaller()
    {
        string pythonInterpreterPath = "python";
        string scriptPath = "SpeechToText.py"; 
                                                                      
        if (!File.Exists(scriptPath))
        {
            MessageBox.Show($"Fehler: Python-Skript nicht gefunden unter: {scriptPath}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 2. Prozess-Starteinstellungen konfigurieren
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = pythonInterpreterPath,
            Arguments = $"\"{scriptPath}\"",
            UseShellExecute = false,          
            RedirectStandardOutput = true,    
            RedirectStandardError = true,     
            CreateNoWindow = false            
        };

        // 3. Prozess starten
        using (Process process = new Process { StartInfo = startInfo })
        {
            // Asynchrone Event-Handler für die Ausgabe
            // Dies verhindert, dass die UI (falls vorhanden) blockiert oder Deadlocks entstehen
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    MessageBox.Show(e.Data, "Python Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    MessageBox.Show(e.Data, "Python Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            try
            {
                process.Start();

                // Beginne mit dem asynchronen Lesen der Ausgaben
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                Console.WriteLine("Python-Skript gestartet. Es reagiert auf Tastendrücke (Leertaste zum Aufnehmen, Esc zum Beenden des Python-Skripts).");
                Console.WriteLine("Das C#-Programm wartet, bis das Python-Skript beendet wird (z.B. durch Drücken von 'Esc' im Python-Kontext).");

                // Warte, bis der Python-Prozess beendet wird.
                // Du könntest hier auch eine maximale Wartezeit einbauen oder den Prozess
                // programmatisch von C# aus beenden, wenn nötig (process.Kill()).
                await process.WaitForExitAsync(); // Asynchrones Warten

                MessageBox.Show("Python-Skript wurde beendet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox. Show($"Fehler beim Starten des Python-Skripts: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}