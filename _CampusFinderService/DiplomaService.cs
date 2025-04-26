using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using _CampusFinderCore.Specifications.University_Specs;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class DiplomaService : IDiplomaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiplomaService(IUnitOfWork unitOfWork)
        {
            unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Diploma>> GetDiplomasAsync()
        {
            var diplomas = await _unitOfWork.Repository<Diploma>().GetAllAsync();

            return diplomas;
        }
        


        public async Task<Diploma> GetDiplomaAsync(int diplomaId)
        {
            var diploma = await _unitOfWork.Repository<Diploma>().GetByIdAsync(diplomaId);
            return diploma;
        }

        public async Task<Diploma> CreateDiplomaAsync(Diploma diploma)
        {
           await _unitOfWork.Repository<Diploma>().AddAsync(diploma);
           await _unitOfWork.CompleteAsync();

           return diploma;
        }

        public async Task DeleteDiplomaAsync(int diplomaId)
        {
            var diploma = await  _unitOfWork.Repository<Diploma>().GetByIdAsync(diplomaId);
            _unitOfWork.Repository<Diploma>().DeleteAsync(diploma);
            await _unitOfWork.CompleteAsync();

        }

        

       
    }
}
