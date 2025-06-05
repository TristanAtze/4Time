using System.Runtime.InteropServices;
using System.Text;

public class OpenOrCloseCDDrive
{
    [DllImport("winmm.dll", CharSet = CharSet.Auto, EntryPoint = "mciSendString")]
    public static extern int MciSendString(string command,
       StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

    protected const int IntMciSuccess = 0;
    protected const int IntBufferSize = 127;

    protected List<DriveInfo> listCDDrives = new List<DriveInfo>();

    public List<DriveInfo> GetCDDrives
    {
        get
        {
            return listCDDrives;
        }
    }

    public OpenOrCloseCDDrive()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
            if (drive.DriveType == DriveType.CDRom)
            {
                listCDDrives.Add(drive);
            }
        }
    }

    public void Open(DriveInfo cdDrive)
    {
        if (cdDrive.DriveType != DriveType.CDRom)
        {
            throw new InvalidOperationException
                ("Der übergebene Parameter enthält kein gültiges CD/DVD-Laufwerk");
        }

        StringBuilder buffer = new StringBuilder();

        int errorCode = MciSendString
            (
            (
            String.Format
            ("set CDAudio!{0} door open", cdDrive.Name)
            ),
            buffer,
            IntBufferSize,
            IntPtr.Zero
            );
    }

    public void Close(DriveInfo cdDrive)
    {
        if (cdDrive.DriveType != DriveType.CDRom)
        {
            throw new InvalidOperationException
                ("Der übergebene Parameter enthält kein gültiges CD/DVD-Laufwerk");
        }

        StringBuilder buffer = new StringBuilder();

        int errorCode = MciSendString
            (
            (
            String.Format
            ("set CDAudio!{0} door closed", cdDrive.Name)
            ),
            buffer,
            IntBufferSize,
            IntPtr.Zero
            );
    }
}
