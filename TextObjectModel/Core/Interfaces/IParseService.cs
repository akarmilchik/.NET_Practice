using TextObjectModel.App.Models;
using TextObjectModel.Core.Services;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Interfaces
{
    public interface IParseService
    {
        Text ParseData(Parser parser, IDataRepository dataRepository);
    }
}
