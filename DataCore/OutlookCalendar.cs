using _4Time.DataCore.Models;

public class OutlookCalendar
{
    public static List<Entry> DoOutlookIntegration()
    {
        List<Entry> entries = new List<Entry>();
        Microsoft.Office.Interop.Outlook.Application outlook = new Microsoft.Office.Interop.Outlook.Application();
        Microsoft.Office.Interop.Outlook.NameSpace nameSpace = outlook.GetNamespace("MAPI");

        Microsoft.Office.Interop.Outlook.MAPIFolder calendarFolder = nameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderCalendar);

        Microsoft.Office.Interop.Outlook.Items calendarItems = calendarFolder.Items;
        calendarItems.IncludeRecurrences = true;
        calendarItems.Sort("[Start]");

        foreach (object item in calendarItems)
        {
            if (item is Microsoft.Office.Interop.Outlook.AppointmentItem appointment)
            {
                if (appointment.Subject == "[Urlaub]" || appointment.Subject == "Jahresurlaub" || appointment.Subject == "Berufsschule")
                {
                    Console.WriteLine($"Termin: {appointment.Subject} am {appointment.Start}");
                }
            }
            if (item != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(item);
        }

        if (calendarItems != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(calendarItems);
        if (calendarFolder != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(calendarFolder);
        if (nameSpace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(nameSpace);
        if (outlook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(outlook);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        return new List<Entry>();
    }
}