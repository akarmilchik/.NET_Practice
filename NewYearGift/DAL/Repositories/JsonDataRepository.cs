using Newtonsoft.Json;
using NewYearGift.App.Models;
using NewYearGift.DAL.Repositories.Interfaces;
using System.IO;

namespace NewYearGift.Core.Services
{
    public class JsonDataRepository : IJsonDataRepository
    {
        public string GetDataPath()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            path = path.Remove(path.LastIndexOf("\\")) + "\\App\\Data\\data.json";

            return path;
        }

        public void SaveData(JsonDataModel data, string dataPath)
        {     
            string serializedObject = JsonConvert.SerializeObject(data);

            File.WriteAllText(dataPath, serializedObject);
        }

        public JsonDataModel ReadData()
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
