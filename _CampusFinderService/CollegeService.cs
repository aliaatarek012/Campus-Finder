using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using _CampusFinderCore.Specifications.University_Specs;
using _CampusFinderInfrastructure.Specifications.University_Specs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class CollegeService : ICollegeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CollegeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Retrieve all colleges with specifications
        public Task<IReadOnlyList<College>> GetCollegesWithSpecAsync(CollegeSpecParams collegeSpec)
        {
            var spec = new CollegeWithMajorsSpecifications(collegeSpec);
            var colleges = _unitOfWork.Repository<College>().GetAllWithSpecAsync(spec);
           

            return colleges;
        }
        // Retrieve a college by its ID
        public Task<College> GetCollegeByIdAsync(int collegeId)
        {
            var spec = new CollegeWithMajorsSpecifications(collegeId);
            var college = _unitOfWork.Repository<College>().GetEntityWithSpecAsync(spec);

            return college;
        }
        // Create a new college
        public async Task<College> CreateCollegeAsync(College college)
        {
           await _unitOfWork.Repository<College>().AddAsync(college);
            await _unitOfWork.CompleteAsync();

            return college;

        }
        // Delete a college by its ID
        public async Task DeleteCollegeAsync(int collegeId)
        {
            var college = await _unitOfWork.Repository<College>().GetByIdAsync(collegeId);
             _unitOfWork.Repository<College>().DeleteAsync(college);
            await _unitOfWork.CompleteAsync();
        }
        // Retrieve all colleges by its University ID
        public async Task<IReadOnlyList<College>> GetCollegesByUniversityIdAsync(int universityId)
        {
            var spec = new RelatedEntitiesByForeignKeySpec<College>(c => c.UniversityID==universityId);
            var colleges = await _unitOfWork.Repository<College>().GetAllWithSpecAsync(spec);

            return colleges;
        }   
       
    }
}
