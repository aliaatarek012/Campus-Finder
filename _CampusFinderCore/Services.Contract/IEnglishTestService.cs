using _CampusFinderCore.Entities.UniversityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IEnglishTestService
    {
        Task<IReadOnlyList<English_Test>> GetAllTests();
        Task<English_Test> GetEnglishTest(int id);
        Task<English_Test> CreateEnglishTestAsync(English_Test english_Test);
     
        Task DeleteEnglishTest(int id);
    }
}
