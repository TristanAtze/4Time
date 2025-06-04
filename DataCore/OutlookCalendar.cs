using _4Time.DataCore.Models;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace _4Time.DataCore;

public static class OutlookCalendar
{
    public static async Task<List<Entry>> DoOutlookIntegrationAsync(List<DateTime> exemptDates)
    {
        List<Entry> entries = [];
        Outlook.Application outlook = new();
        Outlook.NameSpace nameSpace = outlook.GetNamespace("MAPI");

        Outlook.MAPIFolder calendarFolder = nameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);

        Outlook.Items calendarItems = calendarFolder.Items;
        calendarItems.IncludeRecurrences = false;
        calendarItems.Sort("[Start]");

        List<Outlook.AppointmentItem> appointments = [];

        foreach (object item in calendarItems)
        {
            if (item is Outlook.AppointmentItem appointment)
            {
                //Überprüfung, ob der Termin länger als 1 Tag ist
                if (!appointment.AllDayEvent)
                    continue;

                // Überprüfung, ob der Termin ein Arbeitszeit-Termin ist
                int dayOfWeek = (int)appointment.Start.DayOfWeek;
                if (dayOfWeek == 0 || dayOfWeek == 6)
                    continue;
                
                if (!exemptDates.Contains(appointment.Start))
                    appointments.Add(appointment);
            }
        }

        //Filtert alle doppelten Termine heraus
        appointments = [.. appointments.Distinct()];

        appointments.Sort((a, b) => a.Start.CompareTo(b.Start));

        int userId = (await Reader.Read<User>("User",
                ["[UserID]"],
                [
                    $"[FirstName] = '{Connector.FirstName}'",
                    $"[LastName] = '{Connector.LastName}'"
                ])).First().UserID;

        foreach (Outlook.AppointmentItem appointment in appointments)
        {
            foreach (int i in Enumerable.Range(0, (appointment.EndInEndTimeZone - appointment.StartInStartTimeZone).Days))
            {
                DateTime tmpDate = appointment.StartInStartTimeZone.AddDays(i);
                entries.Add(new() { Start = tmpDate, End = (int)tmpDate.DayOfWeek is > 0 and < 6 ? tmpDate.AddHours(8) : tmpDate, Comment = $"[OUTLOOK-AUTO]{appointment.Subject.Replace(" ", "").Replace("[", "").Replace("]", "")}", UserID = userId });
            }
        }

        List<Category> categories = await Reader.Read<Category>("Categories", ["[CategoryID]", "[Description]"]);

        entries.ForEach(x =>
        {
            if (x.Comment?.Contains("krank", StringComparison.CurrentCultureIgnoreCase) ?? false)
                x.CategoryID = categories.FirstOrDefault(c => c.Description == "Krankheit")?.CategoryID ?? 6;
            else if (x.Comment?.Contains("urlaub", StringComparison.CurrentCultureIgnoreCase) ?? false)
                x.CategoryID = categories.FirstOrDefault(c => c.Description == "Urlaub")?.CategoryID ?? 5;
            else if (x.Comment?.Contains("berufsschule", StringComparison.CurrentCultureIgnoreCase) ?? false)
                x.CategoryID = categories.FirstOrDefault(c => c.Description == "Berufsschule")?.CategoryID ?? 8;
            else
                x.CategoryID = x.CategoryID = categories.FirstOrDefault(c => c.Description == "Planmäßige Abwesenheit")?.CategoryID ?? 7;
        });

        if (calendarItems != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(calendarItems);
        if (calendarFolder != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(calendarFolder);
        if (nameSpace != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(nameSpace);
        if (outlook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(outlook);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        return entries;
    }
}