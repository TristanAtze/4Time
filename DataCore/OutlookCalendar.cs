using _4Time.DataCore.Models;
using Microsoft.Office.Interop.Outlook;

public class OutlookCalendar
{
    public static List<Entry> DoOutlookIntegration()
    {
        List<Entry> entries = new List<Entry>();
        Microsoft.Office.Interop.Outlook.Application outlook = new Microsoft.Office.Interop.Outlook.Application();
        NameSpace nameSpace = outlook.GetNamespace("MAPI");

        MAPIFolder calendarFolder = nameSpace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

        Items calendarItems = calendarFolder.Items;
        calendarItems.IncludeRecurrences = true;
        calendarItems.Sort("[Start]");

        List<AppointmentItem> appointments = new List<AppointmentItem>();

        foreach (object item in calendarItems)
        {
            if (item is AppointmentItem appointment)
            {
                //Überprüfung, ob der Termin länger als 1 Tag ist
                if (!appointment.AllDayEvent)
                    continue;

                // Überprüfung, ob der Termin ein Arbeitszeit-Termin ist
                int dayOfWeek = (int)appointment.Start.DayOfWeek;
                if (dayOfWeek == 0 || dayOfWeek == 6)
                    continue;

                appointments.Add(appointment);
            }
        }

        //Filtert alle doppelten Termine heraus
        appointments = [.. appointments.Distinct()];

        appointments.Sort((a, b) => a.Start.CompareTo(b.Start));

        foreach (AppointmentItem appointment in appointments)
        {

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