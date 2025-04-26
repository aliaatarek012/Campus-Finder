using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Specifications.University_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IMajorService
    {
        //IReadOnlyList : Represents a read-only collection of Major 
        Task<IReadOnlyList<Major?>> GetMajorsAsync(MajorsSpecParams specParams);

        // return single Major by its Id
        Task<Major> GetMajorAsync(int majorId);

        //Return all Majors by its College Id
        Task<IReadOnlyList<Major>> GetMajorsByCollegeIdAsync(int collegeId);

        //Create a new Major
        Task<Major> CreateMajorAsync(Major major);
        

        //Delete a Major
        Task DeleteMajorAsync(int majorId);
    }
}
