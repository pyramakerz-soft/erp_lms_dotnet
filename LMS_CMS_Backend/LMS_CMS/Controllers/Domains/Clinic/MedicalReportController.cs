using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class MedicalReportController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public MedicalReportController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region get All Medical History By Parent
        [HttpGet("GetAllMHByParent")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical Report" }
        )]
        public IActionResult GetAllMHByParent()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
           
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            
            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            
            List<MedicalHistory> medicalHistory = Unit_Of_Work.medicalHistory_Repository.FindBy(t => t.IsDeleted != true && t.InsertedByUserId != null);
            
            if (medicalHistory == null || medicalHistory.Count == 0)
            {
                return NotFound();
            }
            
            List<MedicalHistoryGetByParentDTO> MedicalHistoryDto = _mapper.Map<List<MedicalHistoryGetByParentDTO>>(medicalHistory);

            return Ok(MedicalHistoryDto);
        }
        #endregion

        #region get All Medical History By Doctor
        [HttpGet("GetAllMHByDoctor")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical Report" }
        )]
        public IActionResult GetAllMHByDoctor()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            List<MedicalHistory> medicalHistory = Unit_Of_Work.medicalHistory_Repository.FindBy(t => t.IsDeleted != true && t.InsertedByUserId == null);

            if (medicalHistory == null || medicalHistory.Count == 0)
            {
                return NotFound();
            }

            List<MedicalHistoryGetByDoctorDTO> MedicalHistoryDto = _mapper.Map<List<MedicalHistoryGetByDoctorDTO>>(medicalHistory);
            return Ok(MedicalHistoryDto);
        }
        #endregion

        #region get All Hygienes Forms
        [HttpGet("GetAllHygienesForms")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical Report" }
        )]
        public async Task<IActionResult> GetAllHygienesForms()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            long.TryParse(userIdClaim, out long userId);
            
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            
            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            
            List<HygieneForm> hygieneForms = await Unit_Of_Work.hygieneForm_Repository
                .Select_All_With_IncludesById<HygieneForm>(
                    d => d.IsDeleted != true,
                    query => query.Include(h => h.Classroom),
                    query => query.Include(h => h.School),
                    query => query.Include(h => h.Grade)
                );

            if (hygieneForms == null || hygieneForms.Count == 0)
            {
                return NotFound();
            }
            
            List<HygieneFormGetDTO> hygieneFormsDto = _mapper.Map<List<HygieneFormGetDTO>>(hygieneForms);

            foreach (var item in hygieneFormsDto)
            {
                List<StudentHygieneTypes> stuHyTy = await Unit_Of_Work.studentHygieneTypes_Repository.Select_All_With_IncludesById<StudentHygieneTypes>(
                    d => d.IsDeleted != true,
                    query => query.Include(h => h.HygieneForm),
                    query => query.Include(h => h.Student),
                    query => query.Include(h => h.HygieneTypes)
                );
                if (stuHyTy != null)
                {
                    List<StudentHygieneTypesGetDTO> stuHyTyDTO = _mapper.Map<List<StudentHygieneTypesGetDTO>>(stuHyTy);
                    item.StudentHygieneTypes = stuHyTyDTO;
                }
            }

            return Ok(hygieneFormsDto);
        }
        #endregion

        #region get All FollowUps
        [HttpGet("GetAllFollowUps")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical Report" }
        )]
        public async Task<IActionResult> GetAllFollowUps()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            List<FollowUp> followUps = await Unit_Of_Work.followUp_Repository
                .Select_All_With_IncludesById<FollowUp>(
                    d => d.IsDeleted != true,
                    query => query.Include(h => h.Classroom),
                    query => query.Include(h => h.School),
                    query => query.Include(h => h.Grade),
                    query => query.Include(h => h.FollowUpDrugs)
                );
            
            if (followUps == null || followUps.Count == 0)
            {
                return NotFound();
            }

            List<FollowUpGetDTO> followUpsDto = _mapper.Map<List<FollowUpGetDTO>>(followUps);

            return Ok(followUpsDto);
        }
        #endregion
    }
}
