using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IUniversityService
    {
        //IReadOnlyList : Represents a read-only collection of University objects(store multiple products) 
        Task<IReadOnlyList<University?>> GetUniversitiesAsync(UniversitySpecParams specParams);


        //Retrieve a single product based on its universityId,so not use there IReadOnlyList<University>
        Task<University> GetUniversityAsync(int universityId);

        //Create a new university
        Task<University> CreateUniversityAsync(University university);
        //Update university
        //Task<University> UpdateUniversity(int id);

        //Delete a university
        Task DeleteUniversityAsync(int universityId);

        //Retrieve all Colleges
        Task<IReadOnlyList<College>> GetCollegesAsync();


    }
}
