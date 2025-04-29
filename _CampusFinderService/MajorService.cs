using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using _CampusFinderCore.Specifications.University_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class MajorService : IMajorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MajorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Major?>> GetMajorsAsync(MajorsSpecParams specParams)
        {
            var spec = new MajorSpecifications(specParams);
            var majors = await _unitOfWork.Repository<Major>().GetAllWithSpecAsync(spec);
            return majors;
        }

        public Task<Major> GetMajorAsync(int majorId)
        {
            var spec = new MajorSpecifications(majorId);
            var major = _unitOfWork.Repository<Major>().GetEntityWithSpecAsync(spec);

            return major;
        }

        public async Task<IReadOnlyList<Major>> GetMajorsByCollegeIdAsync(int collegeId)
        {
            var spec = new RelatedEntitiesByForeignKeySpec<Major>
                (m => m.CollegeID == collegeId,
                 m => m.College //We Can Add Many Includes because it takes list of includes
                );
            var majors = await _unitOfWork.Repository<Major>().GetAllWithSpecAsync(spec);

            return majors;
        }

        public async Task<Major> CreateMajorAsync(Major major)
        {
            await _unitOfWork.Repository<Major>().AddAsync(major);
            await _unitOfWork.CompleteAsync();

            return major;
        }

    
        public async Task DeleteMajorAsync(int majorId)
        {
            var major =await _unitOfWork.Repository<Major>().GetByIdAsync(majorId);
            _unitOfWork.Repository<Major>().DeleteAsync(major);
             await _unitOfWork.CompleteAsync();

        }
    }
}
