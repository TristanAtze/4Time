using System;
using System.Text;
using System.Runtime.InteropServices; // Für P/Invoke und Marshal
using System.Security; // Für SecureString
using System.Security.Cryptography; // Für RandomNumberGenerator, etc.
using System.IO; // Für MemoryStream (falls nötig)
using System.Threading; // Für Thread.Sleep (kann manchmal bei Löschen helfen, aber Span.Clear ist Standard)

// Wichtiger Hinweis: SecureString und das sichere Löschen von Speicher sind komplexe Themen.
// SecureString hat Einschränkungen, und 100% garantierte sichere Speicherlöschung kann schwierig sein.
// Die hier gezeigten Methoden folgen gängigen Praktiken, aber es ist kein absoluter Schutz gegen alle Arten von Angriffen.

/// <summary>
/// Bietet ECHTE sichere Ver- und Entschlüsselung mit Authentifizierung und Integrität
/// mittels AES-256 im GCM-Modus. Nutzt standardmäßige, kryptographisch geprüfte .NET Klassen.
/// Erweitert um Versionierung, sehr hohe PBKDF2-Iterationen und Unterstützung für Associated Data (AAD).
/// Enthält die gemeinsame Hilfsmethode zum sicheren Löschen von Bytes.
/// </summary>
/// <remarks>
/// Implementiert Versionierung des Chiffretext-Formats.
/// Nutzt PBKDF2 für sichere Schlüsselableitung mit SEHR hohem Iterations-Count.
/// Speichert Version, Salt, Nonce (IV) und Authentifizierungs-Tag zusammen mit dem Chiffretext.
/// Ermöglicht die Einbindung zusätzlicher, nicht-verschlüsselter, aber authentifizierter Daten (AAD).
/// Bietet robusten Schutz vor Manipulation.
/// DIE SICHERE VERWALTUNG DES SCHLÜSSELS/PASSWORTS IST ENTSCHEIDEND UND LIEGT AUSSERHALB DIESES CODES!
/// </remarks>
public static class HighlySecureAuthenticatedVersionedCipher
{
    // Versionskontrolle für das Chiffretext-Format
    private const byte CurrentVersion = 0x01; // Erste Version des Formats: Version | Salt | Nonce | Ciphertext | Tag

    // AES-GCM benötigt einen 32-Byte (256 Bit) Schlüssel für AES-256.
    private const int AesKeySizeBytes = 32;

    // AES-GCM benötigt eine Nonce (ähnlich IV) der Grösse 12 Bytes.
    private const int AesGcmNonceSizeBytes = 12;

    // AES-GCM erzeugt einen Authentifizierungs-Tag der Grösse 16 Bytes (Standard).
    private const int AesGcmTagSizeBytes = 16;

    // Die Grösse des Salts für PBKDF2.
    private const int Pbkdf2SaltSizeBytes = 16;

    // SEHR hohe Iterationen für PBKDF2.
    private const int Pbkdf2Iterations =100000; // Oder mehr, je nach Performance-Budget

    /// <summary>
    /// Löscht den Inhalt eines Byte-Arrays sicher, indem er mit Nullen überschrieben wird.
    /// Eine defensive Massnahme. Gemacht internal static, damit andere Klassen (z.B. Credential Manager)
    /// in derselben Assembly sie nutzen können.
    /// </summary>
    /// <param name="data">Das zu löschende Byte-Array.</param>
    internal static void ClearBytes(byte[] data)
    {
        if (data == null) return;
        // Überschreibt den Speicher mit Nullen.
        new Span<byte>(data).Clear();
        // Optional: Füge eine kleine Verzögerung hinzu, um JIT/Compiler-Optimierungen zu erschweren,
        // die das Löschen überspringen könnten, aber das ist umstritten und kann Performance beeinträchtigen.
        // Thread.Sleep(1); // Beispiel für Verzögerung (meist unnötig)
    }


    /// <summary>
    /// Leitet einen kryptographisch sicheren Schlüssel (AES-256) aus einem Passwort und einem Salt ab.
    /// Nutzt PBKDF2 mit hoher Iterationszahl.
    /// </summary>
    /// <param name="password">Das Benutzerpasswort.</param>
    /// <param name="salt">Ein zufälliges Salt.</param>
    /// <returns>Ein Byte-Array, das als AES-Schlüssel verwendet werden kann.</returns>
    private static byte[] DeriveKeyFromPassword(string password, byte[] salt)
    {
        // Wir nutzen hier SHA256 als Pseudorandom Function (PRF) für PBKDF2.
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256))
        {
            byte[] key = pbkdf2.GetBytes(AesKeySizeBytes);
            return key; // Aufrufer muss diesen Schlüssel löschen!
        }
    }

    /// <summary>
    /// Verschlüsselt Klartext sicher mittels AES-256 im GCM-Modus mit Authentifizierung.
    /// Generiert Salt und Nonce zufällig. Enthält Version, Salt, Nonce, Chiffretext und Tag im Ergebnis.
    /// Erlaubt die Einbindung zusätzlicher, nicht-verschlüsselter, aber authentifizierter Daten (AAD).
    /// </summary>
    /// <param name="plainText">Der zu verschlüsselnde Klartext.</param>
    /// <param name="password">Das Passwort, aus dem der Schlüssel abgeleitet wird.</param>
    /// <param name="associatedData">Zusätzliche Daten (z.B. Header), die authentifiziert, aber nicht verschlüsselt werden. Kann null sein.</param>
    /// <returns>Den Base64-kodierten Chiffretext inklusive Version, Salt, Nonce und Tag, oder null bei Fehler.</returns>
    public static string Encrypt(string plainText, string password, byte[] associatedData = null)
    {
        if (string.IsNullOrEmpty(password)) { Console.WriteLine("Error: Password cannot be empty for secure encryption."); return null; }
        byte[] plainBytes = (string.IsNullOrEmpty(plainText)) ? new byte[0] : Encoding.UTF8.GetBytes(plainText);

        byte[] keyBytes = null;
        byte[] salt = null;
        byte[] nonceBytes = null;
        byte[] resultBytes = null;

        try
        {
            salt = new byte[Pbkdf2SaltSizeBytes];
            using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(salt); }
            keyBytes = DeriveKeyFromPassword(password, salt);

            nonceBytes = new byte[AesGcmNonceSizeBytes];
            using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(nonceBytes); }

            byte[] cipherBytes = new byte[plainBytes.Length];
            byte[] tagBytes = new byte[AesGcmTagSizeBytes];

            using (var aesGcm = new AesGcm(keyBytes))
            {
                aesGcm.Encrypt(nonceBytes, plainBytes, cipherBytes, tagBytes, associatedData);
            }

            int headerLength = 1 + salt.Length + nonceBytes.Length;
            resultBytes = new byte[headerLength + cipherBytes.Length + tagBytes.Length];

            resultBytes[0] = CurrentVersion;
            Buffer.BlockCopy(salt, 0, resultBytes, 1, salt.Length);
            Buffer.BlockCopy(nonceBytes, 0, resultBytes, 1 + salt.Length, nonceBytes.Length);
            Buffer.BlockCopy(cipherBytes, 0, resultBytes, headerLength, cipherBytes.Length);
            Buffer.BlockCopy(tagBytes, 0, resultBytes, headerLength + cipherBytes.Length, tagBytes.Length);

            return Convert.ToBase64String(resultBytes);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during encryption: {ex.Message}");
            return null;
        }
        finally
        {
            // Defensive Massnahme: Lösche sensible Daten im Speicher
            ClearBytes(plainBytes);
            ClearBytes(keyBytes);
            ClearBytes(salt);
            ClearBytes(nonceBytes);
        }
    }

    /// <summary>
    /// Entschlüsselt und authentifiziert einen Base64-kodierten Chiffretext, der mit
    /// der Encrypt-Methode dieser Klasse erstellt wurde. Prüft die Integrität der Daten.
    /// Nutzt optional zusätzliche, nicht-verschlüsselte, aber authentifizierte Daten (AAD).
    /// </summary>
    /// <param name="base64CipherTextWithHeader">Der Base64-kodierte Chiffretext (inklusive Version, Salt, Nonce und Tag).</param>
    /// <param name="password">Das Passwort, aus dem der Schlüssel abgeleitet wird.</param>
    /// <param name="associatedData">Dieselbe zusätzliche Daten (z.B. Header) wie bei der Verschlüsselung. Kann null sein.</param>
    /// <returns>Der entschlüsselte Klartext, oder null bei Fehler oder Manipulationsversuch (inkl. falscher Version).</returns>
    public static string Decrypt(string base64CipherTextWithHeader, string password, byte[] associatedData = null)
    {
        if (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Error: Password cannot be empty for secure decryption.");
            return null;
        }
        if (string.IsNullOrEmpty(base64CipherTextWithHeader))
        {
            return "";
        }

        byte[] inputBytes = null;
        byte[] salt = null;
        byte[] nonceBytes = null;
        byte[] tagBytes = null;
        byte[] cipherBytes = null;
        byte[] keyBytes = null;
        byte[] plainBytes = null;

        try
        {
            inputBytes = Convert.FromBase64String(base64CipherTextWithHeader);

            int headerLength = 1 + Pbkdf2SaltSizeBytes + AesGcmNonceSizeBytes;
            int minLength = headerLength + AesGcmTagSizeBytes;

            if (inputBytes.Length < minLength)
            {
                Console.WriteLine($"Error: Invalid ciphertext (too short, minimum {minLength} bytes required).");
                return null;
            }

            byte version = inputBytes[0];
            if (version != CurrentVersion)
            {
                Console.WriteLine($"Error: Unsupported ciphertext version. Expected {CurrentVersion}, found {version}.");
                return null;
            }

            salt = new byte[Pbkdf2SaltSizeBytes];
            Buffer.BlockCopy(inputBytes, 1, salt, 0, salt.Length);

            nonceBytes = new byte[AesGcmNonceSizeBytes];
            Buffer.BlockCopy(inputBytes, 1 + salt.Length, nonceBytes, 0, nonceBytes.Length);

            tagBytes = new byte[AesGcmTagSizeBytes];
            Buffer.BlockCopy(inputBytes, inputBytes.Length - tagBytes.Length, tagBytes, 0, tagBytes.Length);

            int cipherLength = inputBytes.Length - headerLength - tagBytes.Length;

            if (cipherLength < 0)
            {
                Console.WriteLine("Error: Invalid ciphertext length after extracting components.");
                return null;
            }

            if (cipherLength == 0)
            {
                return "";
            }

            cipherBytes = new byte[cipherLength];
            Buffer.BlockCopy(inputBytes, headerLength, cipherBytes, 0, cipherBytes.Length);

            keyBytes = DeriveKeyFromPassword(password, salt);

            plainBytes = new byte[cipherBytes.Length];

            using (var aesGcm = new AesGcm(keyBytes))
            {
                aesGcm.Decrypt(nonceBytes, cipherBytes, tagBytes, plainBytes, associatedData);
            }

            return Encoding.UTF8.GetString(plainBytes);
        }
        catch (FormatException) { Console.WriteLine("Error: Invalid Base64 input during decryption."); return null; }
        catch (CryptographicException ex) { Console.WriteLine($"Error: Decryption or authentication failed. Data/AAD may have been tampered with, or password/key is incorrect. ({ex.Message})"); return null; }
        catch (Exception ex) { Console.WriteLine($"Error during decryption process: {ex.Message}"); return null; }
        finally
        {
            // Defensive Massnahme: Lösche sensible Daten im Speicher nach Gebrauch
            ClearBytes(inputBytes);
            ClearBytes(salt);
            ClearBytes(nonceBytes);
            ClearBytes(tagBytes);
            ClearBytes(cipherBytes);
            ClearBytes(keyBytes);
            ClearBytes(plainBytes);
        }
    }
}


// === Klasse zur Interaktion mit dem Windows Credential Manager ===

/// <summary>
/// Kapselt P/Invoke-Aufrufe an das Windows Credential Manager API für sichere Passwortspeicherung.
/// Diese Klasse ist WINDOWS-SPEZIFISCH.
/// </summary>
public static class WindowsCredentialManager
{
    // --- P/Invoke Deklarationen für Credential Manager API ---

    private const string CredmgrDll = "advapi32.dll";

    // Gemacht internal, da nur intern von dieser Klasse oder anderen in derselben Assembly benötigt
    internal enum CREDENTIAL_TYPE : uint
    {
        GENERIC = 1,
        // ... andere Typen
    }

    // Fix for CS0051: Make the CRED_PERSIST enum public to match the accessibility of the SavePassword method.
    public enum CRED_PERSIST : uint
    {
        SESSION = 1,
        LOCAL_MACHINE = 2,
        ENTERPRISE = 3,
    }

    // Gemacht internal
    [StructLayout(LayoutKind.Sequential)]
    internal struct FILETIME
    {
        public uint dwLowDateTime;
        public uint dwHighDateTime;
    }

    // Gemacht internal
    [StructLayout(LayoutKind.Sequential)]
    internal struct CREDENTIAL
    {
        public uint Flags;
        public uint Type;
        public IntPtr TargetName; // LPWSTR
        public IntPtr Comment; // LPWSTR
        public FILETIME LastWritten;
        public uint CredentialBlobSize;
        public IntPtr CredentialBlob; // BYTE *
        public uint Persist; // CRED_PERSIST
        public uint AttributeCount;
        public IntPtr Attributes; // PCREDENTIAL_ATTRIBUTE
        public IntPtr AcquireCredentialsHandle;
        public IntPtr AcquireCredentialsHandleArgs;
    }

    [DllImport(CredmgrDll, EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)] // Gibt Bool zurück
    internal static extern bool CredRead(
        string TargetName,
        CREDENTIAL_TYPE Type,
        int Flags,
        out IntPtr Credential // Wird von API zugewiesen
    );

    [DllImport(CredmgrDll, EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CredWrite(
        ref CREDENTIAL Credential,
        int Flags
    );

    [DllImport(CredmgrDll, EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CredDelete(
        string TargetName,
        CREDENTIAL_TYPE Type,
        int Flags
    );

    [DllImport(CredmgrDll, EntryPoint = "CredFree", SetLastError = true)]
    internal static extern void CredFree(IntPtr Credential);

    // --- Hilfsfunktion zum Konvertieren und Löschen von SecureString ---
    // Nutzt die ClearBytes Methode aus HighlySecureAuthenticatedVersionedCipher
    private static byte[] SecureStringToBytes(SecureString secureString)
    {
        if (secureString == null) return null;

        IntPtr unmanagedBytes = IntPtr.Zero;
        IntPtr managedBytesPtr = IntPtr.Zero; // Für das Byte-Array, das wir manuell erstellen

        try
        {
            // Marshal.SecureStringToGlobalAllocUnicode kopiert SecureString sicher
            // in unmanaged Speicher und gibt einen Pointer zurück.
            unmanagedBytes = Marshal.SecureStringToGlobalAllocUnicode(secureString);

            // Die Länge ist die Anzahl der Zeichen * 2 (für UTF16/Unicode).
            int byteLength = secureString.Length * 2;

            // Erstelle ein managed Byte-Array und kopiere die Daten hinein.
            byte[] bytes = new byte[byteLength];
            Marshal.Copy(unmanagedBytes, bytes, 0, byteLength);

            // Gib das managed Byte-Array zurück. Der AUFRUFER muss es löschen!
            return bytes;
        }
        finally
        {
            // Lösche den unmanaged Speicher sicher
            if (unmanagedBytes != IntPtr.Zero)
            {
                Marshal.ZeroFreeGlobalAllocAnsi(unmanagedBytes);
            }
        }
    }

    public static bool SavePassword(string targetName, SecureString password, CRED_PERSIST persistType = CRED_PERSIST.LOCAL_MACHINE, string? comment = null)
    {
        if (string.IsNullOrEmpty(targetName)) throw new ArgumentNullException(nameof(targetName));
        if (password == null) throw new ArgumentNullException(nameof(password));

        byte[] passwordBytes = null; // Managed copy
        IntPtr targetNamePtr = IntPtr.Zero;
        IntPtr commentPtr = IntPtr.Zero;
        GCHandle passwordBytesHandle = default;

        try
        {
            passwordBytes = SecureStringToBytes(password);
            if (passwordBytes == null) return false;

            passwordBytesHandle = GCHandle.Alloc(passwordBytes, GCHandleType.Pinned);
            IntPtr credentialBlobPtr = passwordBytesHandle.AddrOfPinnedObject();

            targetNamePtr = Marshal.StringToHGlobalUni(targetName);
            if (comment != null)
            {
                commentPtr = Marshal.StringToHGlobalUni(comment);
            }

            CREDENTIAL cred = new CREDENTIAL
            {
                Flags = 0,
                Type = (uint)CREDENTIAL_TYPE.GENERIC,
                TargetName = targetNamePtr,
                Comment = commentPtr,
                CredentialBlobSize = (uint)passwordBytes.Length,
                CredentialBlob = credentialBlobPtr,
                Persist = (uint)persistType,
                AttributeCount = 0,
                Attributes = IntPtr.Zero,
                AcquireCredentialsHandle = IntPtr.Zero,
                AcquireCredentialsHandleArgs = IntPtr.Zero
            };

            bool success = CredWrite(ref cred, 0);

            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                Console.WriteLine($"Error writing credential: {error} (Win32 Error)");
            }

            return success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception saving credential: {ex.Message}");
            return false;
        }
        finally
        {
            HighlySecureAuthenticatedVersionedCipher.ClearBytes(passwordBytes);
            if (passwordBytesHandle.IsAllocated) passwordBytesHandle.Free();

            if (targetNamePtr != IntPtr.Zero) Marshal.FreeHGlobal(targetNamePtr);
            if (commentPtr != IntPtr.Zero) Marshal.FreeHGlobal(commentPtr);
        }
    }

    /// <summary>
    /// Lädt ein Passwort aus dem Windows Credential Manager.
    /// </summary>
    /// <param name="targetName">Der eindeutige Name des Credentials.</param>
    /// <returns>Das geladene Passwort als SecureString, oder null, wenn nicht gefunden oder Fehler.</returns>
    public static SecureString LoadPassword(string targetName)
    {
        if (string.IsNullOrEmpty(targetName)) throw new ArgumentNullException(nameof(targetName));

        IntPtr credPtr = IntPtr.Zero; // Pointer, auf den API die Struktur schreibt
        SecureString password = null;

        try
        {
            // 1. Credential lesen
            bool success = CredRead(targetName, CREDENTIAL_TYPE.GENERIC, 0, out credPtr);

            if (!success)
            {
                return null; // Credential nicht gefunden oder anderer Fehler
            }

            // 2. Passwortbytes aus der zurückgegebenen Struktur extrahieren
            // credPtr zeigt auf die CREDENTIAL Struktur, die von CredRead zugewiesen wurde
            CREDENTIAL cred = (CREDENTIAL)Marshal.PtrToStructure(credPtr, typeof(CREDENTIAL));

            if (cred.CredentialBlob != IntPtr.Zero && cred.CredentialBlobSize > 0)
            {
                // Kopiere die Bytes aus dem unmanaged Speicher, den die API zurückgegeben hat
                byte[] passwordBytes = new byte[cred.CredentialBlobSize];
                Marshal.Copy(cred.CredentialBlob, passwordBytes, 0, (int)cred.CredentialBlobSize);

                // 3. Erstelle SecureString aus den Bytes (UTF16 erwartet)
                // Wir erwarten, dass die Bytes UTF16 sind, da wir UTF16 bei CredWriteW geschrieben haben
                // Konvertiere Bytes (UTF16) zurück zu char[] und dann zu SecureString
                // Stelle sicher, dass die Bytezahl gerade ist, sonst ist es kein gültiges UTF16
                if (passwordBytes.Length % 2 != 0)
                {
                    Console.WriteLine("Warning: Credential Blob size is not a multiple of 2, cannot convert to UTF16 characters.");
                    HighlySecureAuthenticatedVersionedCipher.ClearBytes(passwordBytes); // Lösche die Bytes trotzdem
                    return null; // Fehler: Ungültiges Format
                }
                char[] passwordChars = new char[passwordBytes.Length / 2];
                Buffer.BlockCopy(passwordBytes, 0, passwordChars, 0, passwordBytes.Length);

                password = new SecureString();
                foreach (char c in passwordChars)
                {
                    password.AppendChar(c);
                }
                password.MakeReadOnly(); // Wichtig! SecureString abschließen

                // 4. Lösche die temporäre managed Kopie der Passwort-Bytes
                HighlySecureAuthenticatedVersionedCipher.ClearBytes(passwordBytes);
            }
            else
            {
                // Credential gefunden, aber kein PasswortBlob vorhanden (sollte nicht vorkommen für Passwörter)
                Console.WriteLine("Warning: Credential found, but no password data in blob.");
            }

            return password; // Gib SecureString zurück
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception loading credential: {ex.Message}");
            return null;
        }
        finally
        {
            // 5. Speicher, den die API zugewiesen hat (credPtr), MUSS freigegeben werden!
            if (credPtr != IntPtr.Zero)
            {
                CredFree(credPtr);
            }
        }
    }

    /// <summary>
    /// Löscht ein Passwort aus dem Windows Credential Manager.
    /// </summary>
    /// <param name="targetName">Der eindeutige Name des Credentials.</param>
    /// <returns>True, wenn erfolgreich oder nicht gefunden, False bei anderem Fehler.</returns>
    public static bool DeletePassword(string targetName)
    {
        if (string.IsNullOrEmpty(targetName)) throw new ArgumentNullException(nameof(targetName));

        // Credential löschen
        bool success = CredDelete(targetName, CREDENTIAL_TYPE.GENERIC, 0);

        if (!success)
        {
            int error = Marshal.GetLastWin32Error();
            // ERROR_NOT_FOUND (1168) bedeutet, es gab nichts zu löschen, was in Ordnung ist.
            if (error != 1168)
            {
                Console.WriteLine($"Error deleting credential: {error} (Win32 Error)");
                return false; // Fehler
            }
        }

        return true; // Erfolgreich gelöscht oder war nicht vorhanden
    }
}

// === Hauptprogrammlogik der Crypto ===
public static class Crypto
{
    private const string AppCredentialName = "4Time/DatenVerschluesselung";
    private static byte[] myAssociatedData = Encoding.UTF8.GetBytes("System Configuration");
    private static string allKeysFilePath = "K:\\Team Academy\\Azubi_Jahrgang_2024\\Lorenz_Kupfer\\Konsolen Programme\\AllKeysEncrypted.4Time";
    private static FileSystemWatcher watcher = new FileSystemWatcher();

    public static void WriteKey()
    {
        SecureString userKeySecureString = WindowsCredentialManager.LoadPassword(AppCredentialName);
        string userKey = userKeySecureString.ToString();

        IntPtr unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(userKeySecureString);
        try
        {
            userKey = Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString); 
        }

        File.AppendAllLines(allKeysFilePath, [userKey ?? "ERROR"]);
    }

    public static void FileListenerStart()
    {      
        watcher.Path = Path.GetDirectoryName(allKeysFilePath);
        watcher.Filter = Path.GetFileName(allKeysFilePath);
        watcher.Changed += (sender, e) => { GetAllKeys(); };
        watcher.EnableRaisingEvents = true;
    }


    public static string[] GetAllKeys()
    {
        string[] _allKeys = File.ReadAllLines(allKeysFilePath);
        File.Delete(allKeysFilePath);
        File.Create(allKeysFilePath).Close();

        foreach (string key in _allKeys)
        {
            if (key[0] != 'A')
            {
                File.AppendAllLines(allKeysFilePath, [Crypto.Encryption(key)]);
            }
            else if (key[0] == 'A')
            {
                File.AppendAllLines(allKeysFilePath, [key]);
            }
            else if (key == "ERROR" || key == "")
            {
                MessageBox.Show("Error: Fatal Key Error. Please return to the devs immediately");
            }
            else
            {
                MessageBox.Show("Error: Fatal Error. How did u get here?");
            }
        }

        return _allKeys;
    }

    public static string Encryption(string plainText)
    {
        FirstCallOnly();
        string encryptedData = "";
        SecureString loadedSecurePassword = WindowsCredentialManager.LoadPassword(AppCredentialName);

        if (loadedSecurePassword != null)
        {
            // WICHTIG: Das geladene SecureString SOFORT für die Schlüsselableitung nutzen
            // und dann das SecureString Objekt und alle temporären Klartextkopien löschen.
            string passwordStringForDerivation = null;

            try
            {
                // Umwandlung SecureString -> String. Dieser String ist eine temporäre Klartext-Kopie!
                passwordStringForDerivation = new System.Net.NetworkCredential("", loadedSecurePassword).Password;

                // SecureString Objekt sofort nach der Umwandlung löschen
                loadedSecurePassword.Dispose();

                // Schlüssel aus dem temporären String ableiten
                // (Die DeriveKeyFromPassword Methode ist jetzt in HighlySecureAuthenticatedVersionedCipher,
                // aber wir brauchen hier das Salt vom Encrypt/Decrypt Ergebnis, um sie direkt aufzurufen.
                // Einfacher ist, den String an Encrypt/Decrypt zu übergeben.)

                encryptedData = HighlySecureAuthenticatedVersionedCipher.Encrypt(plainText, passwordStringForDerivation, myAssociatedData);
            }
            finally
            {
                // Sorge dafür, dass der temporäre Klartext-String aus der ersten Ableitung gelöscht wird
                // (Obwohl String.Clear nicht existiert, kann man die Referenz null setzen und auf GC hoffen)
                passwordStringForDerivation = null;
                // Abgeleiteten Schlüssel löschen, falls er hier direkt abgeleitet worden wäre
                // (In dieser Demo leitet der Cipher ihn intern ab)
                // ClearBytes(keyBytes); // Nicht notwendig in dieser Demo, da Cipher es intern tut
            }
        }

        return encryptedData;
    }

    public static Task<string> Decryption(string encryptedData)
    {
        FirstCallOnly();
        SecureString loadedSecurePassword = WindowsCredentialManager.LoadPassword(AppCredentialName);
        string decryptedData = "";

        if (loadedSecurePassword != null)
        {
            // --- Schritt 3: Geladenes Passwort für Entschlüsselung nutzen ---
            string passwordStringForDecryption = null;
            try
            {
                passwordStringForDecryption = new System.Net.NetworkCredential("", loadedSecurePassword).Password;

                decryptedData = HighlySecureAuthenticatedVersionedCipher.Decrypt(encryptedData, passwordStringForDecryption, myAssociatedData);
            }
            finally
            {
                // Lösche die SecureString Kopie und den temporären String
                loadedSecurePassword.Dispose();
                // Der string passwordStringForDecryption wird vom GC aufgeräumt
            }
        }
        return Task.FromResult(decryptedData);
    }

    public static void FirstCallOnly()
    {
        string myPasswordString = $"{Environment.OSVersion}{Environment.ProcessId}{Environment.CpuUsage}{Environment.TickCount64}";
        SecureString loadedSecurePassword = WindowsCredentialManager.LoadPassword(AppCredentialName);
        if (loadedSecurePassword == null)
        {
            SecureString userSecurePassword = new SecureString();
            foreach (char c in myPasswordString)
            {
                userSecurePassword.AppendChar(c);
            }
            userSecurePassword.MakeReadOnly();

            bool saveSuccess = WindowsCredentialManager.SavePassword(AppCredentialName, userSecurePassword, WindowsCredentialManager.CRED_PERSIST.LOCAL_MACHINE);
            userSecurePassword.Dispose();
        }
    }
}