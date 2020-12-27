using Newtonsoft.Json;
using System.IO;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Services;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.DAL.Repositories
{
    public class DataRepository : IDataRepository
    {
        private static readonly string dataFileName = MenuService.ReadSetting("DataPath");

        private static readonly string dataObjectModelTxtFileName = MenuService.ReadSetting("DataObjectModelJsonPath");

        public void SaveData(Text data)
        {
            var model = new DataObjectModel { Text = data };

            string modelPath = GetModelPath();

            string serializedObject = JsonConvert.SerializeObject(model);

            File.WriteAllText(modelPath, serializedObject);
        }

        public DataObjectModel ReadData()
        {
            var jsonText = File.ReadAllText(GetModelPath());

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            var deserializedData = JsonConvert.DeserializeObject<DataObjectModel>(jsonText, settings);

            return deserializedData;
        }

        public string GetDataPath() => GetPath(dataFileName);

        private static string GetModelPath() => GetPath(dataObjectModelTxtFileName);

        private static string GetPath(string fileName)
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            return path.Remove(path.LastIndexOf("\\")) + $"{fileName}";
        }
    }
}
