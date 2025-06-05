# 🕒 4Time - Dein Produktivitäts-Booster! 🚀

Willkommen bei **4Time**! Tauche ein in eine intelligente und sichere Welt der Zeiterfassung, die dich dabei unterstützt, den Überblick über deine Arbeitsstunden und Pausen zu behalten. Entwickelt in C#, Python und .NET 8, verbindet 4Time Professionalität mit cleveren Automatisierungsfeatures – und einer Prise Humor!

## ✨ Kernfunktionen, die 4Time einzigartig machen ✨

* **👨‍💼 Zwei maßgeschneiderte Ansichten:**
    * **Benutzeransicht:** Dein täglicher Begleiter für die persönliche Zeiterfassung, das Anzeigen von Statistiken und die Verwaltung deiner Einträge.
    * **Admin-Ansicht:** Speziell für Administratoren (ja, Gerd Kaufmann, wir sprechen von dir! 👋) – hier können die Zeitdaten anderer Benutzer eingesehen und verwaltet werden.
* **⏱️ Umfassende und flexible Zeiterfassung:**
    * Erfasse präzise Arbeitsstunden, verschiedene Pausen (Mittag, Raucherpause, allgemeine Pause) und andere Aktivitäten wie Urlaub, Krankheit oder Berufsschule.
    * Wähle die für dich passende Eingabemethode: Gib eine Start- und Endzeit an, eine Startzeit plus Dauer oder eine Endzeit minus Dauer.
    * Profitiere von automatischen Vorschlägen zur Zeitbuchung, sobald dein PC nach einer Inaktivität entsperrt wird. So gehen keine spontanen Pausen oder Arbeitsbeginne verloren!
* **🔒 Maximale Sicherheit durch fortschrittliche Verschlüsselung:**
    * Deine sensiblen Zeiteinträge (Startzeiten, Endzeiten, Kommentare) werden mit dem robusten **AES-256 GCM**-Algorithmus verschlüsselt, um höchste Datensicherheit zu gewährleisten.
    * Die Hauptverschlüsselungsschlüssel werden sicher über die **Windows-Anmeldeinformationsverwaltung** gespeichert, um den Zugriff unberechtigter Dritter zu verhindern.
    * Ein integriertes versioniertes Chiffretextformat sorgt für zusätzliche Robustheit und Zukunftssicherheit.
* **📊 Aussagekräftige Übersichten und Statistiken:**
    * Ein übersichtliches Dashboard zeigt dir tägliche und wöchentliche Zusammenfassungen deiner Arbeits- und Pausenzeiten.
        * **Gesamte Arbeitszeit** 💼
        * **Gesamte Pausenzeit** ⏸️
        * **Berechnete Überstunden** 📈
    * Der spezielle "My 4Sellers"-Bereich bietet detaillierte Aufschlüsselungen der Vormittags-, Nachmittags- und Pausenzeiten basierend auf deinen erfassten Einträgen.
* **🚀 Reibungsloser Autostart und automatische Updates:**
    * 4Time startet bequem mit Windows, sodass die Anwendung immer sofort einsatzbereit ist.
    * Ein integrierter Updater sorgt dafür, dass du stets die neueste Version nutzt und von Verbesserungen profitierst.
* **🔔 Intelligente Benachrichtigungen für Pausen:**
    * Erhalte rechtzeitige Erinnerungen, um deine Pausen einzuhalten und Arbeitsvorschriften zu befolgen – besonders wichtig für Benutzer unter 18 Jahren!
    * Konfiguriere Vorab-Benachrichtigungen, die dich 10 Minuten vor einer verpflichtenden Pause informieren.
* **🚫 YouTube Shorts Blocker (Experimentell):**
    * Bleib fokussiert! 4Time enthält eine experimentelle Funktion, die YouTube Shorts in gängigen Browsern (Chrome, Firefox, Edge) erkennt und das entsprechende Browser-Fenster schließt, um Ablenkungen zu minimieren.
* **⚙️ Anpassbare Einstellungen:**
    * Lege fest, nach welcher Inaktivitätszeit der PC automatisch gesperrt werden soll, um eine nahtlose Auto-Buchung zu ermöglichen.
    * Passe deine Benachrichtigungspräferenzen an.
    * Alle Einstellungen werden lokal in einer `settings.json`-Datei gespeichert.
* **🤣 Papa-Witz-Spender:**
    * Manchmal braucht man einfach einen guten Witz! Klicke auf den "Dad Joke"-Button und lass dich von einem zufällig ausgewählten, garantiert augenrollwürdigen Papa-Witz unterhalten!
* **📅 Outlook Kalender Integration (Basis):**
    * Möglichkeit, Kalendereinträge aus Outlook zu lesen, um Aktivitäten wie "Urlaub" oder "Berufsschule" direkt zu übernehmen.
* **🗃️ Robuste Datenverwaltung mit SQL Server:**
    * Alle Zeiterfassungsdaten werden sicher in einer SQL Server-Datenbank gespeichert.
    * Das Datenbankschema umfasst klar strukturierte Tabellen für Benutzer, Kategorien, Einträge, Automatisierungen und ein spezielles Shutdown-Protokoll.
    * Die automatische Datenbank- und Benutzer-Einrichtung erfolgt beim ersten Start der Anwendung.

## ✨ Update Logs (Neue Funktionen seit letzter Dokumentation) ✨

* **📊 PerformanceV4 Update:**
    * Das neue Performance Update (PerformanceV4) senkt druch umfassende anpassung der start- und laufzeit-Logik die Anfangsladezeit von ca. 12 Sekunden auf unter 800 Millisekunden.
* **🗣️ Sprachsteuerung (Speech-to-Text):**
    * Integriert eine Spracherkennungsfunktion, die über ein Python-Skript (`SpeechToText.py`) und eine C#-Schnittstelle (`PythonCaller.cs`) realisiert wird.
    * Ermöglicht die Navigation zwischen den Haupt-Tabs der Anwendung (Übersicht, Eintragen, Auslesen, Settings) mittels Sprachbefehlen wie "eintragen" oder "übersicht".
    * Bietet die Möglichkeit, das CD-Laufwerk per Sprachbefehl zu steuern (z.B. "cd öffnen", "cd schließen").
    * Die Benutzeroberfläche in den Einstellungen (`tabSettings`) zeigt nun den Status der Spracherkennung (Online/Offline) und ein Log der erkannten Wörter an. Es gibt auch ein Textfeld mit Tipps zur besseren Nutzung der Spracherkennung.
    * Die Spracherkennung kann in den Einstellungen über eine Checkbox (`SpeechToTextCheck`) aktiviert bzw. deaktiviert werden.
* **💿 CD-Laufwerk-Steuerung:**
    * Es wurde eine neue Funktionalität zum Öffnen und Schließen des CD-Laufwerks implementiert (`OpenCd.cs`).
    * Diese Funktion ist, wie oben erwähnt, auch über die neue Sprachsteuerung zugänglich.
* **💻 Programmierer-Witze:**
    * Zusätzlich zum bekannten "Dad-Joke"-Button gibt es nun eine neue Quelle der Erheiterung: Programmierer-Witze!
    * Ein spezieller Button (`button4`) in den Einstellungen ruft einen zufälligen Witz aus der Sammlung in `ProgrammingJoke.cs` ab.
* **⚙️ Erweiterte Autostart-Kontrolle in der UI:**
    * Während die Autostart-Funktionalität bereits existierte, können Benutzer diese nun direkt über eine Checkbox (`autostartCheckBox`) in den Einstellungen der `UserView` bequem aktivieren oder deaktivieren.
* **🎨 Benutzerdefiniertes Anwendungssymbol:**
    * Die Anwendung verfügt nun über ein eigenes Icon (`Res/Icon.png`), welches im Fenster und in der Taskleiste angezeigt wird, um die Wiedererkennbarkeit zu verbessern (`UserView.cs`).
* **🛡️ Detailliertere Datenbank-Sicherheitsmaßnahmen (`Setup.txt`):**
    * Die Datenbanktabelle `dbo.Shutdown` wurde durch spezifische Trigger (`TRG_Shutdown_PreventInsert`, `TRG_Shutdown_AutoUpdate`) erweitert. `TRG_Shutdown_PreventInsert` blockiert das direkte Einfügen von Datensätzen, um die Integrität der Tabelle zu wahren. `TRG_Shutdown_AutoUpdate` startet einen SQL Server Agent Job, wenn der Shutdown-Status geändert wird.
    * Ein SQL Server Agent Job namens `4TIME_ResetShutdown` wurde implementiert. Dieser Job wird nach einer Verzögerung von 10 Minuten aktiv und setzt den Wert in der `dbo.Shutdown`-Tabelle zurück, falls dieser auf '1' (true) steht.

## 🛠️ Ein Blick unter die Haube – Wie 4Time funktioniert

1.  **Start der Anwendung:** Die Ausführung beginnt in `Program.cs`.
    * Der YouTube Shorts Blocker (`CloseYTShorts.cs`) und der Autostart-Mechanismus (`AutostartHelper` in `Program.cs`) werden asynchron initialisiert, um eine reaktionsschnelle Startphase zu gewährleisten.
    * Das Datenbankschema (`Res/Setup.txt`) und das Benutzerprofil werden bei Bedarf über `Writer.DatabaseSetupAsync()` und `Writer.UserSetupAsync()` eingerichtet. Diese Vorgänge sind robust gegenüber wiederholten Aufrufen.
    * Ein externer `Updater.exe` wird gestartet, um die Anwendung auf dem neuesten Stand zu halten, was eine Entkopplung des Update-Prozesses von der Hauptanwendung ermöglicht.
    * Basierend auf dem Windows-Benutzernamen (`vorname.nachname`) wird die entsprechende Benutzeroberfläche (`UserView` oder `AdminView`) dynamisch geladen.
2.  **Benutzeridentifikation:** Dein Windows-Benutzername wird über `Connector.GetCurrentUser()` extrahiert und zur eindeutigen Identifizierung und Personalisierung im System verwendet.
3.  **Verschlüsselung – Dein Schutzschild (`Crypto.cs`):**
    * Beim ersten Start wird ein kryptographisch starker, einzigartiger Verschlüsselungsschlüssel generiert und sicher über die `WindowsCredentialManager` API in der Windows-Anmeldeinformationsverwaltung persistiert. Dies verhindert die Speicherung sensibler Schlüssel im Dateisystem.
    * Sensible Zeiteinträge (Start, Ende, Kommentar) werden mittels `HighlySecureAuthenticatedVersionedCipher` unter Verwendung von **AES-256 GCM** ver- und entschlüsselt. Dieser Modus bietet nicht nur Vertraulichkeit, sondern auch Authentifizierung und Integrität der Daten, um Manipulationen zu erkennen.
    * Für Admin-Funktionen, die den Zugriff auf Daten anderer Benutzer erfordern, wird ein komplexes System mit einer gemeinsam genutzten, jedoch ebenfalls verschlüsselten Schlüsseldatei (`AllKeysEncrypted.4Time`) eingesetzt, deren Zugriff sorgfältig über `Crypto.GetUserKeys()` verwaltet wird.
4.  **Verwaltung der Zeiteinträge:**
    * Die `Writer.cs`-Klasse ist für die persistente Speicherung und Aktualisierung von Daten in der SQL Server-Datenbank zuständig. Sie nutzt Reflection, um Objekte dynamisch in Datenbankspalten zu mappen, und berücksichtigt dabei nicht setzbare Spalten zur Wahrung der Datenintegrität.
    * `Reader.cs` implementiert eine asynchrone und parallele Datenleselogik mit `SemaphoreSlim` zur Steuerung des Grades der Parallelität. Dies ermöglicht effizientes Abrufen und Entschlüsseln von Zeiteinträgen unter Verwendung von `Task.WhenAll` für simultane Entschlüsselungsvorgänge.
5.  **Tracking der PC-Sperrzeit (`TrackLockedTime.cs`):**
    * Die Implementierung überwacht `SystemEvents.SessionSwitch`-Ereignisse, um präzise den Zeitpunkt des Sperrens und Entsperrens des PCs zu erfassen.
    * Anhand dieser Zeitpunkte werden die Leerlaufzeiten berechnet und dem Benutzer proaktiv als Vorschlag zur Buchung präsentiert, wobei komplexe Zeitlogiken (z.B. Beginn des Arbeitstages vs. letzte Buchung) berücksichtigt werden.
6.  **Pausen-Benachrichtigungen (`NotificationManager.cs`):**
    * Der `NotificationManager` analysiert die erfassten Arbeitszeiten und sendet Windows-Toast-Benachrichtigungen. Die Logik berücksichtigt dabei Arbeitszeitgesetze (insbesondere für U18-Regelungen) und plant Vorab-Benachrichtigungen dynamisch basierend auf der bisherigen Arbeitsdauer und dem Zeitpunkt der letzten Pause.
7.  **Deine Einstellungen (`SettingsController.cs`):**
    * Die Anwendungseinstellungen werden über `SettingsController.cs` in einer `settings.json`-Datei im JSON-Format verwaltet. Dies ermöglicht eine flexible Speicherung verschiedener Datentypen und deren Typkonvertierung beim Laden.
8.  **Admin-Funktionen (`AdminView.cs`):**
    * Die `AdminView` bietet erweiterte Funktionalitäten, einschließlich der Simulation von Benutzerkonten für die Datenanalyse und der dynamischen Aktualisierung der angezeigten Zeitdaten basierend auf der Benutzerauswahl.

## ⚙️ Technische Details & Projektstruktur

* **Framework:** .NET 8
* **Sprache:** C#
* **Datenbank:** SQL Server
    * Der Connection String ist in `Connector.cs` definiert und wird durch `_C1x2y3.cs` obfuscated generiert.
    * Das Datenbankschema (`dbo.Automatics`, `dbo.User`, `dbo.Categories`, `dbo.Shutdown`, `dbo.Entries`) wird über das SQL-Skript in `Res/Setup.txt` beim ersten Start erstellt und mit initialen Daten (`dbo.Categories`) befüllt.
    * Die Datenbankinteraktion erfolgt über `Microsoft.Data.SqlClient`, wobei asynchrone Operationen (`OpenAsync`, `ReadAsync`) für eine nicht-blockierende Ausführung eingesetzt werden.
* **Verschlüsselung:**
    * **AES-256 GCM:** Implementiert in `HighlySecureAuthenticatedVersionedCipher` innerhalb von `Crypto.cs`. Dies beinhaltet die Ableitung kryptographisch sicherer Schlüssel mittels PBKDF2 mit hoher Iterationszahl, die Generierung von Zufalls-Nonces und die Nutzung von Authentifizierungs-Tags zur Sicherstellung der Datenintegrität und -authentizität.
    * **Windows Credential Manager:** Genutzt über P/Invoke-Aufrufe (`DllImport` und `LibraryImport` in `Crypto.cs` für `CredReadW`, `CredWriteW`, `CredDeleteW`) zur sicheren Speicherung sensibler Anmeldeinformationen im Betriebssystem. Die sichere Umwandlung von `SecureString` zu Bytes und deren Löschung (`ClearBytes`) ist ebenfalls integriert.
* **Benutzeroberfläche:** Windows Forms. Die Trennung von Designer-Code (`.Designer.cs`) und Logik (`.cs`) fördert eine saubere Codebasis und Wartbarkeit.
* **Asynchrone Programmierung:** Umfangreicher Einsatz von `async`/`await` und `Task`-basierten Operationen (`Task.Run`, `Task.WhenAll`) zur Verbesserung der Responsivität der Anwendung, insbesondere bei datenbankintensiven oder langlaufenden Prozessen.
* **System-Interaktionen:** Direkte Interaktionen mit dem Betriebssystem, z.B. über `DllImport` für `user32.dll` (`GetForegroundWindow`, `GetWindowText`, `LockWorkStation`) und `advapi32.dll` (Credential Manager), sowie die Nutzung von `Microsoft.Win32.SystemEvents` für die Überwachung von Session-Statusänderungen.
* **Python-Integration:** Für die Spracherkennung wird ein Python-Skript (`SpeechToText.py`) über `PythonCaller.cs` gestartet und die Kommunikation erfolgt über Standard-Output/Error-Streams.
* **Aktuelle App-Version (gemäß interner Versionsdatei):** 3.1.0.02

### 📁 Projektstruktur (Auszug)

```
4Time/
├── Async/
│   ├── CloseYTShorts.cs                # Implementiert die Logik zum Erkennen und Schließen von YouTube Shorts in Browsern unter Verwendung von UI Automation und P/Invoke.
│   ├── DisableReloadButton.cs          # Kapselt die asynchrone Datenneuladung für die Benutzeroberfläche.
│   ├── LockPcWhenInaktive.cs           # Enthält die P/Invoke-Definitionen und Logik zur automatischen PC-Sperrung nach definierter Inaktivität.
│   └── TrackLockedTime.cs              # Abonniert Systemereignisse für Session-Sperrung/-Entsperrung und initialisiert automatische Buchungsvorschläge.
├── DataCore/
│   ├── Connector.cs                    # Verwaltet die SQL Server-Datenbankverbindung und die Extraktion des aktuellen Windows-Benutzernamens.
│   ├── Crypto.cs                       # Kern der Sicherheitsarchitektur, umfasst AES-256 GCM Verschlüsselung und sichere Interaktion mit dem Windows Credential Manager.
│   ├── OutlookCalendar.cs              # Bietet eine grundlegende Schnittstelle zur Interaktion mit Outlook-Kalenderdaten.
│   ├── Settings.cs                     # Steuert das Laden, Speichern und Verwalten von Anwendungseinstellungen als JSON-Datei.
│   ├── Writer.cs                       # Verantwortlich für das Einfügen, Aktualisieren und Löschen von Daten in der Datenbank, inklusive Verschlüsselung von Einträgen.
│   ├── _C1x2y3.cs                      # Generiert den obfuscated Connection String.
│   └── Models/
│       ├── Automatics.cs               # Datenmodell für automatische Prozesse.
│       ├── Category.cs                 # Datenmodell für verschiedene Zeitkategorien (Arbeit, Pause, Urlaub).
│       ├── Entry.cs                    # Hauptdatenmodell für einzelne Zeiteinträge, inklusive berechneter Dauer.
│       └── User.cs                     # Datenmodell für Benutzerprofile.
│   └── Reader/
│       ├── EntrySpecificRowData.cs     # Hilfsstruktur zur Entkapselung von Rohdaten spezifischer Zeiteinträge vor der Entschlüsselung.
│       ├── Reader.cs                   # Implementiert eine parallele und asynchrone Datenleselogik für die Datenbank, inklusive Entschlüsselung und dynamischem Objekt-Mapping.
│       └── RowDataHolder.cs            # Universeller Container für Rohdaten aus Datenbankzeilen.
├── FrontEnd/
│   ├── AdminView.Designer.cs           # Automatisch generierter Code für das Design der Admin-Oberfläche.
│   ├── AdminView.cs                    # Geschäftslogik und Event-Handler für die Admin-Ansicht, inklusive Benutzer-Simulation.
│   ├── Form1Files/
│   │   └── Events.cs                   # Spezifische Event-Handler und Validierungslogik für die Benutzer-Ansicht (UserView).
│   ├── Jokes/
│   │   ├── DadJokes.cs                 # Kapselt eine Sammlung von Papa-Witzen.
│   │   └── ProgrammingJoke.cs          # Kapselt eine Sammlung von Programmierer-Witzen.
│   ├── NotificationManager.cs          # Verwaltet die Zeitplanung und das Senden von Desktop-Benachrichtigungen.
│   ├── UserView.Designer.cs            # Automatisch generierter Code für das Design der Benutzer-Oberfläche.
│   └── UserView.cs                     # Hauptlogik der Benutzer-Ansicht, Datenaggregation und Interaktion mit anderen Modulen.
├── Python/
│   ├── PythonCaller.cs                 # C#-Klasse zur Interaktion mit Python-Skripten.
│   └── SpeechToText.py                 # Python-Skript für die Spracherkennung.
├── Program.cs                          # Der Anwendungseinstiegspunkt, orchestriert den Start von Diensten und der Benutzeroberfläche.
└── Res/
    ├── Setup.txt                       # SQL-DDL-Skript für die Erstellung der Datenbanktabellen und Trigger, inklusive initialer Daten und SQL Server Agent Job Definitionen.
    └── Version.txt                     # Enthält die aktuelle Versionsnummer der Anwendung.
```

## 🚀 Erste Schritte

4Time ist darauf ausgelegt, schnell und unkompliziert einsatzbereit zu sein!
1.  Beim ersten Start versucht die Anwendung, die notwendigen Datenbanktabellen einzurichten und den aktuellen Benutzer zu registrieren.
2.  Zudem wird versucht, sich automatisch zum Windows-Autostart hinzuzufügen.
3.  Stelle sicher, dass der in `Connector.cs` (bzw. durch `_C1x2y3.cs` generierte) angegebene Datenbankserver erreichbar ist. (Entwickler-Hinweis: Dies muss eventuell angepasst werden!).

---

Viel Spaß beim effektiven Managen deiner Zeit mit **4Time**! Mögen deine Arbeitstage produktiv und deine Pausen von großartigen Witzen begleitet sein! 😄
