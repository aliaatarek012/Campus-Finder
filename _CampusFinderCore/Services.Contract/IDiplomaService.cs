using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Specifications.University_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IDiplomaService
    {
        //IReadOnlyList : Represents a read-only collection of Major  
        Task<IReadOnlyList<Diploma>> GetDiplomasAsync();

        Task<Diploma> GetDiplomaAsync(int diplomaId);

       
        Task<Diploma> CreateDiplomaAsync(Diploma diploma);


        Task DeleteDiplomaAsync(int diplomaId);
    }
}
