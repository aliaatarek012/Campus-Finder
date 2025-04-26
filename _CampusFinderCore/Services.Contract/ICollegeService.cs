using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface ICollegeService
    {
        // Retrieve all colleges
        Task<IReadOnlyList<College>> GetCollegesWithSpecAsync(CollegeSpecParams collegeSpec);

        // Retrieve all colleges by its University ID
        Task<IReadOnlyList<College>> GetCollegesByUniversityIdAsync(int universityId);

        // Retrieve a single college based on its collegeId
        Task<College> GetCollegeByIdAsync(int collegeId);

        //Update college
        Task<College> CreateCollegeAsync(College college);

        // Delete College
        Task DeleteCollegeAsync(int collegeId);

    }
}
