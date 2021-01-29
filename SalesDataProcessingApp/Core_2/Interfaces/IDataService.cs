using System.Threading.Tasks;

namespace Core_2.Interfaces
{
    public interface IDataService
    {
        Task ProcessFile(object filePath);
    }
}
