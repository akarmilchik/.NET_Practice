using Newtonsoft.Json;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;
using System.IO;

namespace NewYearGift.Core.Services
{
    public static class DataService
    {
        public static string GetDataPath()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

                path = path.Remove(path.IndexOf("bin")) + "App\\Data\\data.json";


            return path;
        }

        public static void SaveData(Gift gift)
        {
            string dataPath = GetDataPath();

            dataPath = dataPath.Remove(dataPath.IndexOf("data"));

            string res = JsonConvert.SerializeObject(gift);

            File.WriteAllText(dataPath, res);

            Console.WriteLine("Data has been saved to file");
            
        }

        public static Gift ReadData()
        {
            string dataPath = GetDataPath();

            Gift restoredGift;

            string json = File.ReadAllText(dataPath);

            restoredGift = JsonConvert.DeserializeObject<Gift>(json);

            return restoredGift;
        }


    }
}
