using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.DAL.Repositories
{
    public class DataRepository : IDataRepository
    {
        private static string dataFileName = "text.txt";
        private static string dataObjectModelFileName = "dataObjectModel.txt";

        public void SaveData(Text data)
        {
            var model = new DataObjectModel { Text = data };

            string modelPath = GetModelPath();

            string serializedObject = JsonConvert.SerializeObject(model);

            File.WriteAllText(modelPath, serializedObject);

            JObject dataObject = new JObject(model);

            using StreamWriter file = File.CreateText(modelPath);

            using JsonTextWriter writer = new JsonTextWriter(file);

            dataObject.WriteTo(writer);

        }

        public DataObjectModel ReadData()
        {
            using StreamReader file = File.OpenText(GetModelPath());

            using JsonTextReader reader = new JsonTextReader(file);

            JObject jsonTextObject = (JObject)JToken.ReadFrom(reader);

            Text textObject = jsonTextObject.ToObject<Text>();

            DataObjectModel textObjectModel = new DataObjectModel();

            textObjectModel.Text = textObject;

            return textObjectModel;
        }

        private static string GetPath(string fileName)
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            return path.Remove(path.LastIndexOf("\\")) + $"\\App\\Data\\{fileName}";
        }

        private static string GetModelPath() => GetPath(dataObjectModelFileName);

        public string GetDataPath() => GetPath(dataFileName);
    }
}
