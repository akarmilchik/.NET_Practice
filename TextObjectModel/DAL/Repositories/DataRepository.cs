using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.DAL.Repositories
{
    public class DataRepository : IDataRepository
    {
        public string GetDataPath()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            path = path.Remove(path.LastIndexOf("\\")) + "\\App\\Data\\text.txt";

            return path;
        }

        public string GetModelPath()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            path = path.Remove(path.LastIndexOf("\\")) + "\\App\\Data\\dataObjectModel.json";

            return path;
        }

        public DataObjectModel UpdateObjectModel(Text data) => new DataObjectModel { Text = data };

        public void SaveData(DataObjectModel data)
        {
            string modelPath = GetModelPath();

            string serializedObject = JsonConvert.SerializeObject(data);

            File.WriteAllText(modelPath, serializedObject);

            JObject dataObject = new JObject(data);

            using StreamWriter file = File.CreateText(modelPath);

            using JsonTextWriter writer = new JsonTextWriter(file);
            
            dataObject.WriteTo(writer);
            
        }

        public DataObjectModel ReadData()
        {
            string data = File.ReadAllText(GetDataPath());

            using StreamReader file = File.OpenText(GetModelPath());

            using JsonTextReader reader = new JsonTextReader(file);
            
            JObject jsonTextObject = (JObject)JToken.ReadFrom(reader);

            Text textObject = jsonTextObject.ToObject<Text>();

            DataObjectModel textObjectModel = new DataObjectModel();

            textObjectModel.Text = textObject;

            return textObjectModel;
        }
    }
}
