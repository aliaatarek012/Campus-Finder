using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class EnglishTestService : IEnglishTestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnglishTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<English_Test>> GetAllTests()
        {
            var tests = await _unitOfWork.Repository<English_Test>().GetAllAsync();

            return tests;
        }

        public async Task<English_Test> GetEnglishTest(int id)
        {
            var test = await _unitOfWork.Repository<English_Test>().GetByIdAsync(id);

            return test;
        }
        public Task<English_Test> CreateEnglishTestAsync(English_Test english_Test)
        {
            _unitOfWork.Repository<English_Test>().AddAsync(english_Test);
            _unitOfWork.CompleteAsync();

            return Task.FromResult(english_Test);
            
        }
        public async Task DeleteEnglishTest(int id)
        {
            var test = await _unitOfWork.Repository<English_Test>().GetByIdAsync(id);
            _unitOfWork.Repository<English_Test>().DeleteAsync(test);
            _unitOfWork.CompleteAsync();

        
        }

       
    }
}
