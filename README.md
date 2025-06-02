# ------------------------------EN------------------------------
# 🕒 4Time - Your Ultimate Time Tracking Companion! 🚀

Welcome to **4Time**! This isn't just another time tracker; it's a smart, secure, and fun way to manage your work hours, breaks, and productivity. Built with C# and .NET 8, 4Time is designed to keep you on top of your schedule with a sprinkle of humor and some neat automation tricks!

## ✨ Features That Make 4Time Shine ✨

* **👨‍💼 Dual Views:**
    * **User View:** For everyday time tracking, viewing personal stats, and managing entries.
    * **Admin View:** Special access for administrators (Hello, Gerd Kaufmann! 👋) to oversee other users' time data.
* **⏱️ Comprehensive Time Tracking:**
    * Log work hours, breaks (lunch, smoke, general), and other activities like vacation, sick leave, or vocational school.
    * Flexible entry methods: Start Time - End Time, Start Time + Duration, or End Time - Duration.
    * Automatic suggestions for booking time when your PC unlocks after being locked. Never lose track of those impromptu breaks!
* **🔒 Top-Notch Security & Encryption:**
    * Your time entries (start, end, comments) are encrypted using **AES-256 GCM** for maximum security.
    * Master encryption keys are securely stored using the **Windows Credential Manager**.
    * A versioned ciphertext format ensures robustness.
* **📊 Insightful Overview:**
    * Dashboard displaying daily and weekly summaries:
        * Total Work Time  трудовое время
        * Total Pause Time ⏸️
        * Overtime Calculated 📈
    * Special "My 4Sellers" section showing Vormittag (morning), Nachmittag (afternoon), and Pause times based on your entries.
* **🚀 Autostart & Updates:**
    * 4Time conveniently starts with Windows, so it's always ready when you are.
    * Built-in updater to keep you on the latest version. (Checks `update.xml`)
* **🔔 Smart Notifications:**
    * Get timely reminders to take breaks, helping you comply with work regulations (especially for U18 users!).
    * Configurable pre-notifications (10 minutes before a mandatory pause).
* **🚫 YouTube Shorts Blocker:**
    * Stay focused! 4Time includes an experimental feature to detect and close YouTube Shorts playing in your browser (Chrome, Firefox, Edge).
* **⚙️ Customizable Settings:**
    * Adjust settings like the minimum PC lock time to trigger auto-booking suggestions.
    * Toggle notification preferences.
    * All settings are saved locally in `settings.json`.
* **🤣 Dad Joke Dispenser:**
    * Need a chuckle? Click the "Dad Joke" button for a randomly selected, quality groan-inducer!
* **📅 Outlook Calendar Integration (Basic):**
    * Ability to read Outlook calendar for entries like "Urlaub" (Vacation) or "Berufsschule" (Vocational School).
* **🗃️ Robust Data Management:**
    * All data is stored in a SQL Server database.
    * Database schema includes tables for Users, Categories, Entries, Automatics, and a special Shutdown log.
    * Automatic database and user setup on first run.

## 🛠️ How It Works - A Glimpse Under the Hood

1.  **Startup:** `Program.cs` kicks things off!
    * Initializes YouTube Shorts Blocker & Autostart.
    * Sets up the database schema (`Res/Setup.txt`) and user profile if needed.
    * Checks for updates via an external `Updater.exe`.
    * Loads the appropriate view (`UserView` or `AdminView`) based on the Windows username.
2.  **User Identification:** Your Windows username (e.g., `firstname.lastname`) is used to identify you in the system.
3.  **Encryption is Key:**
    * A unique encryption key for the application is generated on first use (if not already present) and stored securely in the Windows Credential Manager under the name "4Time/DatenVerschluesselung".
    * For admin functionalities involving other users' data, a system involving a shared (but encrypted) key file (`AllKeysEncrypted.4Time`) is used.
    * Sensitive time entry data (Start, End, Comment) is encrypted before being written to the database and decrypted when read.
4.  **Time Entries:**
    * When you save an entry through `UserView`, the data goes through `Writer.cs` to be stored.
    * When you view entries, `Reader.cs` fetches and decrypts them.
5.  **PC Lock Tracking (`TrackLockedTime.cs`):**
    * Monitors session lock/unlock events.
    * On unlock, it calculates the locked duration and time since the last entry or PC start.
    * Prompts you to auto-book this time as work or a break.
6.  **Pause Notifications (`NotificationManager.cs`):**
    * Calculates your work duration since the last significant break or system start.
    * Sends toast notifications to remind you to take a pause, configurable based on whether you're under 18.
7.  **Settings (`SettingsController.cs`):**
    * Your preferences (like notification settings or lock time threshold) are saved in a local `settings.json` file.
8.  **Admin Power (`AdminView.cs`):**
    * If you're "gerd.kaufmann", you get the admin view.
    * This view allows selecting another user and viewing their decrypted time entries, leveraging the `Crypto.GetUserKeys()` mechanism.

## ⚙️ Technical Details

* **Framework:** .NET 8
* **Language:** C#
* **Database:** SQL Server (Connection string in `Connector.cs`, schema in `Res/Setup.txt`)
* **Encryption:** AES-256 GCM for data, Windows Credential Manager for master key storage.
* **UI:** Windows Forms
* **Current App Version (as per internal version file):** 2.1.0.01

## 🚀 Getting Started

4Time is designed to be plug-and-play!
1.  On first launch, it will attempt to set up necessary database tables and register the current user.
2.  It will also try to add itself to Windows Autostart for your convenience.
3.  Make sure the database server specified in `Connector.cs` is accessible. (For developers: you might need to adjust this!).

---

Enjoy tracking your time with **4Time**! May your work be productive and your breaks filled with excellent dad jokes! 😄


# ------------------------------DE------------------------------
# 🕒 4Time - Dein ultimativer Zeiterfassungs-Begleiter! 🚀

Willkommen bei **4Time**! Das ist nicht nur ein weiterer Zeit-Tracker; es ist eine intelligente, sichere und unterhaltsame Methode, um deine Arbeitsstunden, Pausen und Produktivität zu managen. Entwickelt mit C# und .NET 8, ist 4Time darauf ausgelegt, dich mit einem Augenzwinkern und einigen cleveren Automatisierungstricks auf dem Laufenden zu halten!

## ✨ Funktionen, die 4Time zum Strahlen bringen ✨

* **👨‍💼 Zwei Ansichten:**
    * **Benutzeransicht:** Für die tägliche Zeiterfassung, das Anzeigen persönlicher Statistiken und die Verwaltung von Einträgen.
    * **Admin-Ansicht:** Spezieller Zugriff für Administratoren (Hallo, Gerd Kaufmann! 👋), um die Zeitdaten anderer Benutzer einzusehen.
* **⏱️ Umfassende Zeiterfassung:**
    * Erfasse Arbeitsstunden, Pausen (Mittag, Rauchen, Allgemein) und andere Aktivitäten wie Urlaub, Krankheit oder Berufsschule.
    * Flexible Eingabemethoden: Startzeit - Endzeit, Startzeit + Dauer oder Endzeit - Dauer.
    * Automatische Vorschläge zur Zeitbuchung, wenn dein PC nach einer Sperre entsperrt wird. Verliere nie wieder den Überblick über spontane Unterbrechungen!
* **🔒 Erstklassige Sicherheit & Verschlüsselung:**
    * Deine Zeiteinträge (Start, Ende, Kommentare) werden mit **AES-256 GCM** für maximale Sicherheit verschlüsselt.
    * Hauptverschlüsselungsschlüssel werden sicher über die **Windows-Anmeldeinformationsverwaltung** gespeichert.
    * Ein versioniertes Chiffretextformat sorgt für Robustheit.
* **📊 Aufschlussreiche Übersicht:**
    * Dashboard mit täglichen und wöchentlichen Zusammenfassungen:
        * Gesamte Arbeitszeit 💼
        * Gesamte Pausenzeit ⏸️
        * Berechnete Überstunden 📈
    * Spezieller "My 4Sellers"-Bereich, der Vormittags-, Nachmittags- und Pausenzeiten basierend auf deinen Einträgen anzeigt.
* **🚀 Autostart & Updates:**
    * 4Time startet bequem mit Windows und ist somit immer einsatzbereit, wenn du es bist.
    * Integrierter Updater, um dich auf der neuesten Version zu halten (prüft `update.xml`).
* **🔔 Intelligente Benachrichtigungen:**
    * Erhalte rechtzeitige Erinnerungen für Pausen, die dir helfen, Arbeitsvorschriften einzuhalten (besonders für U18-Nutzer!).
    * Konfigurierbare Vorab-Benachrichtigungen (10 Minuten vor einer Pflichtpause).
* **🚫 YouTube Shorts Blocker:**
    * Bleib fokussiert! 4Time enthält eine experimentelle Funktion, um YouTube Shorts in deinem Browser (Chrome, Firefox, Edge) zu erkennen und zu schließen.
* **⚙️ Anpassbare Einstellungen:**
    * Passe Einstellungen an, wie z.B. die minimale PC-Sperrzeit, die Auto-Buchungsvorschläge auslöst.
    * Schalte Benachrichtigungspräferenzen um.
    * Alle Einstellungen werden lokal in `settings.json` gespeichert.
* **🤣 Papa-Witz-Spender:**
    * Brauchst du was zum Schmunzeln? Klicke auf den "Dad Joke"-Button für einen zufällig ausgewählten Witz, der garantiert für ein Augenrollen sorgt!
* **📅 Outlook Kalender Integration (Basis):**
    * Möglichkeit, Outlook-Kalendereinträge wie "Urlaub" oder "Berufsschule" auszulesen.
* **🗃️ Robuste Datenverwaltung:**
    * Alle Daten werden in einer SQL Server-Datenbank gespeichert.
    * Das Datenbankschema umfasst Tabellen für Benutzer, Kategorien, Einträge, Automatisierungen und ein spezielles Shutdown-Protokoll.
    * Automatische Datenbank- und Benutzer-Einrichtung beim ersten Start.

## 🛠️ Wie es funktioniert - Ein Blick unter die Haube

1.  **Start:** `Program.cs` legt los!
    * Initialisiert den YouTube Shorts Blocker & Autostart.
    * Richtet bei Bedarf das Datenbankschema (`Res/Setup.txt`) und das Benutzerprofil ein.
    * Sucht nach Updates über eine externe `Updater.exe`.
    * Lädt die passende Ansicht (`UserView` oder `AdminView`) basierend auf dem Windows-Benutzernamen.
2.  **Benutzeridentifikation:** Dein Windows-Benutzername (z.B. `vorname.nachname`) wird verwendet, um dich im System zu identifizieren.
3.  **Verschlüsselung ist der Schlüssel:**
    * Ein einzigartiger Verschlüsselungsschlüssel für die Anwendung wird bei der ersten Verwendung generiert (falls nicht vorhanden) und sicher in der Windows-Anmeldeinformationsverwaltung unter dem Namen "4Time/DatenVerschluesselung" gespeichert.
    * Für Admin-Funktionen, die Daten anderer Benutzer betreffen, wird ein System mit einer gemeinsamen (aber verschlüsselten) Schlüsseldatei (`AllKeysEncrypted.4Time`) verwendet.
    * Sensible Zeiterfassungsdaten (Start, Ende, Kommentar) werden vor dem Schreiben in die Datenbank verschlüsselt und beim Lesen entschlüsselt.
4.  **Zeiteinträge:**
    * Wenn du einen Eintrag über `UserView` speicherst, gehen die Daten durch `Writer.cs` zur Speicherung.
    * Wenn du Einträge ansiehst, holt und entschlüsselt `Reader.cs` diese.
5.  **PC-Sperrverfolgung (`TrackLockedTime.cs`):**
    * Überwacht Sitzungssperr-/-entsperrereignisse.
    * Beim Entsperren berechnet es die Sperrdauer und die Zeit seit dem letzten Eintrag oder PC-Start.
    * Fordert dich auf, diese Zeit automatisch als Arbeit oder Pause zu buchen.
6.  **Pausenbenachrichtigungen (`NotificationManager.cs`):**
    * Berechnet deine Arbeitsdauer seit der letzten signifikanten Pause oder dem Systemstart.
    * Sendet Toast-Benachrichtigungen, um dich an Pausen zu erinnern, konfigurierbar je nachdem, ob du unter 18 bist.
7.  **Einstellungen (`SettingsController.cs`):**
    * Deine Präferenzen (wie Benachrichtigungseinstellungen oder Sperrzeit-Schwellenwert) werden in einer lokalen `settings.json`-Datei gespeichert.
8.  **Admin-Macht (`AdminView.cs`):**
    * Wenn du "gerd.kaufmann" bist, erhältst du die Admin-Ansicht.
    * Diese Ansicht ermöglicht die Auswahl eines anderen Benutzers und die Anzeige seiner entschlüsselten Zeiteinträge unter Nutzung des `Crypto.GetUserKeys()`-Mechanismus.

## ⚙️ Technische Details

* **Framework:** .NET 8
* **Sprache:** C#
* **Datenbank:** SQL Server (Verbindungszeichenfolge in `Connector.cs`, Schema in `Res/Setup.txt`)
* **Verschlüsselung:** AES-256 GCM für Daten, Windows-Anmeldeinformationsverwaltung für die Speicherung des Hauptschlüssels.
* **UI:** Windows Forms
* **Aktuelle App-Version (laut interner Versionsdatei):** 2.1.0.01

## 🚀 Erste Schritte

4Time ist darauf ausgelegt, sofort einsatzbereit zu sein!
1.  Beim ersten Start versucht es, die notwendigen Datenbanktabellen einzurichten und den aktuellen Benutzer zu registrieren.
2.  Es wird auch versuchen, sich selbst zum Windows-Autostart hinzuzufügen.
3.  Stelle sicher, dass der in `Connector.cs` angegebene Datenbankserver erreichbar ist. (Für Entwickler: Dies muss möglicherweise angepasst werden!).

---

Viel Spaß beim Erfassen deiner Zeit mit **4Time**! Möge deine Arbeit produktiv sein und deine Pausen gefüllt mit exzellenten Papa-Witzen! 😄
