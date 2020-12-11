using Newtonsoft.Json;
using NewYearGift.App.Models;
using System.IO;

namespace NewYearGift.DAL.Repositories.Interfaces
{
    public class JsonDataRepository : IJsonDataRepository
    {
        public string GetDataPath()
        {
            string path = (@".\App\Data\data.json");

            return path;
        }

        public void SaveData(JsonDataModel data, string dataPath)
        {     
            string serializedObject = JsonConvert.SerializeObject(data);

            File.WriteAllText(dataPath, serializedObject);
        }

        public JsonDataModel ReadData(string dataPath)
        {
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
