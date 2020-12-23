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
    }
}
