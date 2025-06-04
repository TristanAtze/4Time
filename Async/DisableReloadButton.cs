using _4Time.DataCore;
using _4Time.DataCore.Models;

namespace _4Time.Async;

public class DisableReloadButton
{
    public static async Task PerformDataReloadAsync(Time4SellersApp.UserView userViewInstance)
    {
        List<User> users = await Task.Run(() => Reader.Read<User>("User",
                new string[] { "[UserID]" },
                new string[] {
                    $"[FirstName] = '{Connector.FirstName}'",
                    $"[LastName] = '{Connector.LastName}'"
                }));

        if (users == null || !users.Any())
        {
            throw new InvalidOperationException("No user found with the given first and last name.");
        }
        int userId = users.First().UserID;

        userViewInstance.AllEntrys = await Task.Run(() => Reader.Read<Entry>("Entries", null,
            new string[] {
                     $"[UserID] = {userId}"
            }));
    }
}
