using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Verwaltet das Speichern und Laden von Anwendungseinstellungen in/aus einer JSON-Datei
/// unter Verwendung von Newtonsoft.Json.
/// </summary>
public class SettinngsController
{
    private readonly string _filePath;
    private Dictionary<string, object> _currentSettings;

    public SettinngsController(string filePath = "settings.json")
    {
        _filePath = filePath;
        _currentSettings = LoadSettingsFromFile();
    }

    /// <summary>
    /// Lädt die Einstellungen aus der JSON-Datei.
    /// </summary>
    /// <returns>Ein Dictionary mit den geladenen Einstellungen.</returns>
    private Dictionary<string, object> LoadSettingsFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
               var settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                return settings ?? [];
            }
        }
        catch (JsonException jsonEx)
        { 
            Console.WriteLine($"Fehler beim Deserialisieren der Einstellungsdatei '{_filePath}': {jsonEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Laden der Einstellungsdatei '{_filePath}': {ex.Message}");
        }
        return []; }

    /// <summary>
    /// Speichert die aktuellen Einstellungen in der JSON-Datei.
    /// </summary>
    private void SaveChangesToFile()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_currentSettings, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern der Einstellungen in '{_filePath}': {ex.Message}");
        }
    }

    /// <summary>
    /// Speichert eine Liste von Einstellungen. Vorhandene Einstellungen werden überschrieben oder neue hinzugefügt.
    /// Wenn die übergebene Liste doppelte Schlüssel enthält, wird eine ArgumentException ausgelöst.
    /// </summary>
    /// <param name="settingsToSave">Eine Liste von Tupeln (Schlüssel, Wert), die die zu speichernden Einstellungen repräsentieren.</param>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn settingsToSave null ist.</exception>
    /// <exception cref="ArgumentException">Wird ausgelöst, wenn settingsToSave doppelte Schlüssel enthält.</exception>
    public void SetSettings(List<(string Key, object Value)> settingsToSave)
    {
        if (settingsToSave == null)
        {
            throw new ArgumentNullException(nameof(settingsToSave));
        }

        try
        {
            _currentSettings = settingsToSave.ToDictionary(item => item.Key, item => item.Value);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fehler beim Setzen der Einstellungen: Doppelte Schlüssel in der Eingabeliste. {ex.Message}");
            throw; 
        }

        SaveChangesToFile(); 
    }

    /// <summary>
    /// Ruft alle aktuellen Einstellungen als eine Liste von Schlüssel-Wert-Paaren ab.
    /// </summary>
    /// <returns>Eine Liste von Tupeln (Schlüssel, Wert), die alle aktuellen Einstellungen repräsentieren.</returns>
    public List<(string Key, object Value)> GetSettings()
    {
        return _currentSettings.Select(kvp => (kvp.Key, kvp.Value)).ToList();
    }

    /// <summary>
    /// Ruft den Wert einer einzelnen Einstellung ab und konvertiert ihn in den angegebenen Typ.
    /// </summary>
    /// <typeparam name="T">Der Typ, in den der Einstellungswert konvertiert werden soll.</typeparam>
    /// <param name="key">Der Schlüssel der Einstellung.</param>
    /// <param name="defaultValue">Der Standardwert, der zurückgegeben wird, wenn der Schlüssel nicht existiert oder die Konvertierung fehlschlägt.</param>
    /// <returns>Der konvertierte Einstellungswert oder der Standardwert.</returns>
    public T GetSetting<T>(string key, T defaultValue = default)
    {
        if (_currentSettings.TryGetValue(key, out object value))
        {
            if (value == null) return defaultValue;
            if (value is T typedValue)
            {
                return typedValue;
            }

            try
            {
                if (value is JToken token)
                {
                    return token.ToObject<T>();
                }
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Konvertieren der Einstellung '{key}' in den Typ {typeof(T).Name}: {ex.Message}. Standardwert wird zurückgegeben.");
                return defaultValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Fügt eine neue Einstellung hinzu oder aktualisiert eine vorhandene und speichert die Änderungen.
    /// </summary>
    /// <param name="key">Der Schlüssel der Einstellung.</param>
    /// <param name="value">Der Wert der Einstellung.</param>
    public void SetSetting(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Der Schlüssel darf nicht null oder leer sein.", nameof(key));
        }
        _currentSettings[key] = value;
        SaveChangesToFile();
    }

    /// <summary>
    /// Entfernt eine Einstellung anhand ihres Schlüssels und speichert die Änderungen.
    /// </summary>
    /// <param name="key">Der Schlüssel der zu entfernenden Einstellung.</param>
    /// <returns>True, wenn die Einstellung erfolgreich entfernt wurde, andernfalls false.</returns>
    public bool RemoveSetting(string key)
    {
        if (_currentSettings.Remove(key))
        {
            SaveChangesToFile();
            return true;
        }
        return false;
    }
}