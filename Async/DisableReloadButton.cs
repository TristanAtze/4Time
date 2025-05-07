using _4Time.DataCore.Models;
using _4Time.DataCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time4SellersApp;

namespace _4Time.Async
{
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
                ]).First().UserID}",                    
            ]);
            await Task.Delay(5000);
        }
    }
}
