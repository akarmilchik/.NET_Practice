using NewYearGift.Models.Gifts;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewYearGift.Core.Services
{
    public class DataService
    {

        static async Task SaveData(Gift gift)
        {
            using (FileStream stream = new FileStream("../ user.json", FileMode.OpenOrCreate))
            {
                
                await JsonSerializer.SerializeAsync<Gift>(stream, gift);
                Console.WriteLine("Data has been saved to file");
            }
        }

        static async Task<Gift> ReadData()
        {
            Gift restoredGift;

            using (FileStream stream = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                restoredGift = await JsonSerializer.DeserializeAsync<Gift>(stream);
            }

            return restoredGift;
        }
    }
}
