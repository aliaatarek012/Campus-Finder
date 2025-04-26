using _CampusFinderCore.Dto.University_Dtos;
using _CampusFinderCore.Entities.UniversityEntities;
using CampusFinder.Dto.University_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IAdmissionRequirementService
    {
        Task<List<DiplomaRequirementDto>> GetDiplomaRequirementsAsync(int collegeId);
        Task<List<EnglishTestRequirementDto>> GetEnglishTestRequirementsAsync(int collegeId);
        Task<AdmissionRequirementDto> GetAllAdmissionsRequirementByCollegeIdAsync(int collegeId);

    }
}
