using AutoMapper;
using LMS_CMS_BL.DTO.Clinic;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.ClinicModule;
using LMS_CMS_DAL.Models.Domains.Inventory;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Clinic
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;

        public MedicalHistoryController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        #region Get
        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public IActionResult Get()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            List<MedicalHistory> medicalHistories = Unit_Of_Work.medicalHistory_Repository.FindBy(m => m.IsDeleted != true);

            if (medicalHistories == null || medicalHistories.Count == 0)
            {
                return NotFound();
            }

            List<MedicalHistoryGetDTO> medicalHistoryGets = _mapper.Map<List<MedicalHistoryGetDTO>>(medicalHistories);

            return Ok(medicalHistoryGets);
        }
        #endregion

        #region Add Medical History
        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> AddAsync([FromForm] MedicalHistoryAddDTO historyAddDTO)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
           
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (historyAddDTO == null)
            {
                return BadRequest("Medical History cannot be null");
            }

            School school = await Unit_Of_Work.school_Repository.Select_By_IdAsync(historyAddDTO.SchoolId);

            if (school == null || school.IsDeleted == true)
            {
                return NotFound("No School With this ID");
            }

            Grade grade = await Unit_Of_Work.grade_Repository.Select_By_IdAsync(historyAddDTO.GradeId);

            if (grade == null || grade.IsDeleted == true)
            {
                return NotFound("No Grade With this ID");
            }

            Classroom classroom = await Unit_Of_Work.classroom_Repository.Select_By_IdAsync(historyAddDTO.ClassRoomID);

            if (classroom == null || classroom.IsDeleted == true)
            {
                return NotFound("No Classroom With this ID");
            }

            Student student = await Unit_Of_Work.student_Repository.Select_By_IdAsync(historyAddDTO.ClassRoomID);

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student With this ID");
            }

            MedicalHistory medicalHistory = _mapper.Map<MedicalHistory>(historyAddDTO);

            medicalHistory.Attached = historyAddDTO.Attachments.Count;

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medicalHistory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                medicalHistory.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                medicalHistory.InsertedByUserId = userId;
            }

            Unit_Of_Work.medicalHistory_Repository.Add(medicalHistory);
            Unit_Of_Work.SaveChanges();
                     

            if (historyAddDTO.Attachments is not null || historyAddDTO.Attachments.Any())
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");
                var medicalHistoryFolder = Path.Combine(baseFolder, medicalHistory.Student?.en_name + "_" + medicalHistory.Id);

                if (!Directory.Exists(medicalHistoryFolder))
                {
                    Directory.CreateDirectory(medicalHistoryFolder);
                }

                foreach (var file in historyAddDTO.Attachments)
                {
                    MedicalHistoryFiles medicalHistoryFiles = new MedicalHistoryFiles
                    {
                        FileName = file.FileName,
                        MedicalHistoryId = medicalHistory.Id,
                    };

                    var filePath = Path.Combine(medicalHistoryFolder, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    Path.Combine("Uploads", "MedicalHistories", file.FileName + "_" + medicalHistory.Id);

                    Unit_Of_Work.medicalHistoryFiles_Repository.Add(medicalHistoryFiles);
                }
            }

            Unit_Of_Work.SaveChanges();

            return Ok(historyAddDTO);
        }
        #endregion
    }
}
