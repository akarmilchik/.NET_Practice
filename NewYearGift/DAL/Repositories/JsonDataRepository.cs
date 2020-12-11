using Newtonsoft.Json;
using NewYearGift.App.Models;
using System.IO;

namespace NewYearGift.DAL.Repositories.Interfaces
{
    public class JsonDataRepository : IDataRepository
    {
        private const string Path = @".\App\Data\data.json";

        public void SaveData(JsonDataModel data)
        {     

            string serializedObject = JsonConvert.SerializeObject(data);

            var res = Path;

            File.WriteAllText(Path, serializedObject);
        }

        public JsonDataModel ReadData()
        {
            string jsonText = File.ReadAllText(Path);

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
