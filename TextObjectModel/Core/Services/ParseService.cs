using TextObjectModel.App.Models;
using TextObjectModel.Core.Interfaces;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Services
{
    class ParseService : IParseService
    {
        public Text ParseData(Parser parser, IDataRepository dataRepository)
        {
            var path = dataRepository.GetDataPath();
 
            return parser.Parse(path);
        }
    }
}
