using Newtonsoft.Json;
using NewYearGift.App.Data;
using System;
using System.IO;

namespace NewYearGift.Core.Services
{
    public static class JsonDataService
    {   
        public static JsonDataModel GetLocalData()
        {
            var result = DataSeeder.GetData();

            return result;
        }

        public static string GetDataPath()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            path = path.Remove(path.IndexOf("bin")) + "App\\Data\\data.json";

            return path;
        }

        public static void SaveData(JsonDataModel data)
        {
            string dataPath = GetDataPath();

            string serializedObject = JsonConvert.SerializeObject(data);

            File.WriteAllText(dataPath, serializedObject);

            Console.WriteLine("\nData has been saved to file\n");
            
        }

        public static JsonDataModel ReadData()
        {

            string dataPath = GetDataPath();

            string jsonText = File.ReadAllText(dataPath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            var deserializedData = JsonConvert.DeserializeObject<JsonDataModel>(jsonText, settings);

            return deserializedData;
        }


    }
}
