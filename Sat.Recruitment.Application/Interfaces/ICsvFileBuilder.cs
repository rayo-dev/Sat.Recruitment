using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface ICsvFileBuilder
    {
        Task<IList<string>> GetDataFile(string path);
        Task<int> WriteFile(string filePath, IEnumerable<string> lines);
    }
}
