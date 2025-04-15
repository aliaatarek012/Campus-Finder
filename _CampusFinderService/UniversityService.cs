using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class UniversityService : IUniversityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UniversityService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IReadOnlyList<University?>> GetUniversitiesAsync(UniversitySpecParams specParams)
        {
            var spec = new UniversityWithCollegesSpecifications(specParams);
            var universities = await _unitOfWork.Repository<University>().GetAllWithSpecAsync(spec);

            // Apply college filtering if needed
            if (!string.IsNullOrEmpty(specParams.CollegeName) || specParams.MinFees.HasValue || specParams.MaxFees.HasValue)
            {
                foreach (var university in universities)
                {
                    // Create the college specification
                    var collegeSpecParams = new CollegeSpecParams
                    {
                        CollegeName = specParams.CollegeName,
                        MinFees = specParams.MinFees,
                        MaxFees = specParams.MaxFees
                    };


                    // Create the college specification
                    var collegeSpec = new CollegeWithMajorsSpecifications(collegeSpecParams);

                    // Fetch filtered colleges for the current university
                    var filteredColleges = await _unitOfWork.Repository<College>().GetAllWithSpecAsync(collegeSpec);

                    // Replace the university's colleges with the filtered list
                    university.Colleges = filteredColleges.Where(c => c.UniversityID == university.UniversityID).ToList();
                }

                // Step 3: Exclude universities with no matching colleges (optional)
                universities = universities.Where(u => u.Colleges.Any()).ToList();
            }

            return universities;
        }

        public Task<University> GetUniversityAsync(int universityId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<College>> GetCollegesAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}
