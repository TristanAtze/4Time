using _4Time.DataCore;
using _4Time.DataCore.Models;

namespace _4Time.Async;

public class DisableReloadButton
{
    public static async Task PerformDataReloadAsync(Time4SellersApp.UserView userViewInstance)
    {
        userViewInstance._allEntrys = Reader.Read<Entry>("Entries", null,
        [
        $"[UserID] = {Reader.Read<User>("User",
            [
                "[UserID]"
            ],
            [
                $"[FirstName] = '{Connector.FirstName}'",
                $"[LastName] = '{Connector.LastName}'"
            ]).Result.First().UserID}",
        ]).Result;
    }
}
