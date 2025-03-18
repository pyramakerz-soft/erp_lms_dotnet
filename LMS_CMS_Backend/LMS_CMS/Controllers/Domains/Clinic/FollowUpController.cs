using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class FollowUpController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public FollowUpController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Follow Up" }
        )]
        public async Task<IActionResult> Get()
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

            List<FollowUp> followUps = await Unit_Of_Work.followUp_Repository.Select_All_With_IncludesById<FollowUp>(
                d => d.IsDeleted != true,
                query => query.Include(f => f.FollowUpDrugs),
                query => query.Include(f => f.School),
                query => query.Include(f => f.Grade),
                query => query.Include(f => f.Classroom),
                query => query.Include(f => f.Student),
                query => query.Include(f => f.Diagnosis)
            );

            if (followUps == null || followUps.Count == 0)
            {
                return NotFound();
            }

            List<FollowUpGetDTO> followUpDto = _mapper.Map<List<FollowUpGetDTO>>(followUps);

            foreach (var follow in followUpDto)
            {
                foreach (var drug in follow.FollowUpDrugs)
                {
                    Drug Drug = Unit_Of_Work.drug_Repository.Select_By_Id(drug.DrugId);
                    drug.Drug = Drug.Name;

                    Dose dose = Unit_Of_Work.dose_Repository.Select_By_Id(drug.DoseId);
                    drug.Dose = dose.DoseTimes;
                }
            }

            return Ok(followUpDto);
        }
        #endregion

        #region GetByID
        [HttpGet("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Follow Up" }
        )]
        public async Task<IActionResult> GetByID(long id)
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

            FollowUp followUp = await Unit_Of_Work.followUp_Repository
                .FindByIncludesAsync(
                    d => d.Id == id && d.IsDeleted != true, 
                    query => query.Include(f => f.FollowUpDrugs),
                    query => query.Include(f => f.School),
                    query => query.Include(f => f.Grade),
                    query => query.Include(f => f.Classroom),
                    query => query.Include(f => f.Student),
                    query => query.Include(f => f.Diagnosis)
                );

            if (followUp == null)
            {
                return NotFound();
            }

            FollowUpGetDTO followUpDto = _mapper.Map<FollowUpGetDTO>(followUp);

            foreach (var follow in followUpDto.FollowUpDrugs)
            {
                Drug Drug = Unit_Of_Work.drug_Repository.Select_By_Id(follow.DrugId);
                follow.Drug = Drug.Name;

                Dose dose = Unit_Of_Work.dose_Repository.Select_By_Id(follow.DoseId);
                follow.Dose = dose.DoseTimes;
            }

            return Ok(followUpDto);
        }
        #endregion

        #region Add Follow Up
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Add(FollowUpAddDTO followUpDto)
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

            School school = Unit_Of_Work.school_Repository.First_Or_Default(d => d.ID == followUpDto.SchoolId && d.IsDeleted != true);

            if (school == null)
            {
                return NotFound("School not found.");
            }

            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(d => d.ID == followUpDto.GradeId && d.IsDeleted != true);

            if (grade == null)
            {
                return NotFound("Grade not found.");
            }

            Classroom classroom = Unit_Of_Work.classroom_Repository.First_Or_Default(d => d.ID == followUpDto.ClassroomId && d.IsDeleted != true);

            if (classroom == null)
            {
                return NotFound("Classroom not found.");
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(d => d.ID == followUpDto.StudentId && d.IsDeleted != true);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            Diagnosis diagnosis = Unit_Of_Work.diagnosis_Repository.First_Or_Default(d => d.Id == followUpDto.DiagnosisId && d.IsDeleted != true);

            if (diagnosis == null)
            {
                return NotFound("Diagnosis not found.");
            }

            foreach (var followUpDrug in followUpDto.FollowUpDrugs)
            {
                Drug drug = Unit_Of_Work.drug_Repository.First_Or_Default(d => d.Id == followUpDrug.DrugId && d.IsDeleted != true);
                if (drug == null)
                {
                    return NotFound($"Drug with ID: {followUpDrug.DrugId} not found.");
                }

                Dose dose = Unit_Of_Work.dose_Repository.First_Or_Default(d => d.Id == followUpDrug.DoseId && d.IsDeleted != true);
                if (drug == null)
                {
                    return NotFound($"Dose with ID: {followUpDrug.DoseId} not found.");
                }
            }

            FollowUp followUp = _mapper.Map<FollowUp>(followUpDto);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            followUp.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                followUp.InsertedByOctaId = userId;
            }

            else if (userTypeClaim == "employee")
            {
                followUp.InsertedByUserId = userId;
            }

            Unit_Of_Work.followUp_Repository.Add(followUp);
            Unit_Of_Work.SaveChanges();

            foreach (var followUpDrug in followUpDto.FollowUpDrugs)
            {
                FollowUpDrug fud = _mapper.Map<FollowUpDrug>(followUpDrug);
                
                fud.FollowUpId = followUp.Id;

                Unit_Of_Work.followUpDrug_Repository.Add(fud);
            }

            return Ok(followUpDto);
        }
        #endregion

        #region Update Follow Up
        [HttpPut]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Update(FollowUpPutDTO followUpDto)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            FollowUp followUp = Unit_Of_Work.followUp_Repository.First_Or_Default(d => d.Id == followUpDto.ID && d.IsDeleted != true);

            if (followUp == null)
            {
                return NotFound();
            }

            foreach (var followUpDrug in followUpDto.FollowUpDrugs)
            {
                Drug drug = Unit_Of_Work.drug_Repository.First_Or_Default(d => d.Id == followUpDrug.DrugId && d.IsDeleted != true);
                if (drug == null)
                {
                    return NotFound($"Drug with ID: {followUpDrug.DrugId} not found.");
                }

                Dose dose = Unit_Of_Work.dose_Repository.First_Or_Default(d => d.Id == followUpDrug.DoseId && d.IsDeleted != true);
                if (drug == null)
                {
                    return NotFound($"Dose with ID: {followUpDrug.DoseId} not found.");
                }
            }

            _mapper.Map(followUpDto, followUp);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            followUp.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                followUp.UpdatedByOctaId = userId;
                if (followUp.UpdatedByUserId != null)
                {
                    followUp.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                followUp.UpdatedByUserId = userId;
                if (followUp.UpdatedByOctaId != null)
                {
                    followUp.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.followUp_Repository.Update(followUp);
            Unit_Of_Work.SaveChanges();

            foreach (var followUpDrugDto in followUpDto.FollowUpDrugs)
            {
                var existingFollowUpDrug = Unit_Of_Work.followUpDrug_Repository
                    .First_Or_Default(f => f.Id == followUpDrugDto.ID);

                if (existingFollowUpDrug != null)
                {
                    _mapper.Map(followUpDrugDto, existingFollowUpDrug);

                    existingFollowUpDrug.FollowUpId = followUp.Id; 

                    Unit_Of_Work.followUpDrug_Repository.Update(existingFollowUpDrug);
                }
                else
                {
                    FollowUpDrug newFollowUpDrug = _mapper.Map<FollowUpDrug>(followUpDrugDto);
                    newFollowUpDrug.FollowUpId = followUp.Id;

                    Unit_Of_Work.followUpDrug_Repository.Add(newFollowUpDrug);
                }
            }

            return Ok(followUpDto);
        }
        #endregion

        #region Delete Follow Up
        [HttpDelete]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Follow Up" }
        )]
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return BadRequest("Follow Up ID cannot be null.");
            }

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            FollowUp followUp = Unit_Of_Work.followUp_Repository.First_Or_Default(d => d.IsDeleted != true && d.Id == id);

            if (followUp == null)
            {
                return NotFound();
            }

            followUp.IsDeleted = true;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            followUp.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                followUp.DeletedByOctaId = userId;

                if (followUp.DeletedByUserId != null)
                {
                    followUp.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                followUp.DeletedByUserId = userId;
                if (followUp.DeletedByOctaId != null)
                {
                    followUp.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.followUp_Repository.Update(followUp);
            Unit_Of_Work.SaveChanges();

            return Ok("Follow Up deleted successfully");
        }
        #endregion
    }
}
