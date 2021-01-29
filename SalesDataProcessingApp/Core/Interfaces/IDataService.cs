using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataService
    {
        Task ProcessFile(object filePath);
    }
}
