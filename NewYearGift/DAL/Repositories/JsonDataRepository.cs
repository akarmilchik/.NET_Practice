using Newtonsoft.Json;
using NewYearGift.App.Models;
using System.IO;

namespace NewYearGift.DAL.Repositories.Interfaces
{
    public class JsonDataRepository : IDataRepository
    {
 
        private string GetDataPath()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            path = path.Remove(path.LastIndexOf("\\")) + "\\App\\Data\\data.json";

            return path;
        }

        public void SaveData(JsonDataModel data)
        {     
            string serializedObject = JsonConvert.SerializeObject(data);

            File.WriteAllText(GetDataPath(), serializedObject);
        }

        public JsonDataModel ReadData()
        {
            string jsonText = File.ReadAllText(GetDataPath());

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
