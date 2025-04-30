using _CampusFinderCore.Dto.University_Dtos;
using _CampusFinderCore.Entities.UniversityEntities;
using _CampusFinderCore.Services.Contract;
using _CampusFinderCore.Specifications;
using _CampusFinderInfrastructure.Data.AppDbContext;
using CampusFinder.Dto.University_Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class AdmissionRequirementService : IAdmissionRequirementService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdmissionRequirementService(ApplicationDbContext dbContext)
        {
          _dbContext = dbContext;
        }
        public async Task<List<DiplomaRequirementDto>> GetDiplomaRequirementsAsync(int collegeId)
        {
            var diplomas = await _dbContext.CollegeDiplomas
                .Where(cd => cd.CollegeID == collegeId)
                .Select(cd => new DiplomaRequirementDto
                {
                    DiplomaName = cd.Diploma.Name,
                    MinGrade = cd.Min_Grade,
                    Requirements = cd.Requirments
                })
                .ToListAsync();

            return diplomas;
        }
        public async Task<List<EnglishTestRequirementDto>> GetEnglishTestRequirementsAsync(int collegeId)
        {
            var englishTests = await _dbContext.CollegeEnglishTests
                .Where(ct => ct.CollegeID == collegeId)
                .Select(ct => new EnglishTestRequirementDto
                {
                    TestName = ct.English_Test.Name,
                    MinScore = ct.Min_Score
                })
                .ToListAsync();

            return englishTests;
        }


        public async Task<AdmissionRequirementDto> GetAllAdmissionsRequirementByCollegeIdAsync(int collegeId)
        {
            var college = await _dbContext.Colleges
                .Where(c => c.CollegeID == collegeId)
                .Select(c => new AdmissionRequirementDto
                {
                    CollegeID = c.CollegeID,
                    Name = c.Name,
                    Description = c.Description,
                    StandardFees = c.StandardFees,
                    YearsOfDration = c.YearsOfDration,

                    Diplomas = _dbContext.CollegeDiplomas
                        .Where(cd => cd.CollegeID == c.CollegeID)
                        .Select(cd => new DiplomaRequirementDto
                        {
                            DiplomaName = cd.Diploma.Name,
                            MinGrade = cd.Min_Grade,
                            Requirements = cd.Requirments
                        }).ToList(),

                    EnglishTests = _dbContext.CollegeEnglishTests
                        .Where(ct => ct.CollegeID == c.CollegeID)
                        .Join(_dbContext.EnglishTests,
                              ct => ct.TestID,
                              et => et.TestID,
                              (ct, et) => new EnglishTestRequirementDto
                              {
                                  TestName = et.Name,
                                  MinScore = ct.Min_Score
                              }).ToList(),

                    // retrieve all majors inside the college 
                    Majors = _dbContext.Majors
                        .Where(m => m.CollegeID == c.CollegeID)
                        .Select(m => new MajorsDto
                        {
                            MajorId = m.MajorID,
                            Name = m.Name,
                            Description = m.Description,
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return college;
        }



    }
}
