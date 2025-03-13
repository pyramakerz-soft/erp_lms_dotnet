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
    public class MedicalHistoryController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly FileImageValidationService _fileImageValidationService;
        private readonly CheckPageAccessService _checkPageAccessService;

        public MedicalHistoryController(DbContextFactoryService dbContextFactory, IMapper mapper, FileImageValidationService fileImageValidationService, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _fileImageValidationService = fileImageValidationService;
            _checkPageAccessService = checkPageAccessService;
        }

        #region Get By Doctor
        [HttpGet("GetByDoctor")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> GetByDoctor()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            List<MedicalHistory> medicalHistories = await Unit_Of_Work.medicalHistory_Repository.Select_All_With_IncludesById<MedicalHistory>(
                    d => d.IsDeleted != true,
                    query => query.Include(h => h.Classroom),
                    query => query.Include(h => h.School),
                    query => query.Include(h => h.Grade),
                    query => query.Include(h => h.Student)
                );

            if (medicalHistories == null || medicalHistories.Count == 0)
            {
                return NotFound();
            }

            List<MedicalHistoryGetByDoctorDTO> medicalHistoryGetDTO = _mapper.Map<List<MedicalHistoryGetByDoctorDTO>>(medicalHistories);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            foreach (var medicalHistory in medicalHistoryGetDTO)
            {
                if (!string.IsNullOrEmpty(medicalHistory.FirstReport))
                {
                    medicalHistory.FirstReport = $"{serverUrl}{medicalHistory.FirstReport.Replace("\\", "/")}";
                }
                if (!string.IsNullOrEmpty(medicalHistory.SecReport))
                {
                    medicalHistory.SecReport = $"{serverUrl}{medicalHistory.SecReport.Replace("\\", "/")}";
                }
            }

            return Ok(medicalHistoryGetDTO);
        }
        #endregion

        #region Get By Parent
        [HttpGet("GetByParent")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Medical History" }
        )]
        public IActionResult GetByParent()
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

            List<MedicalHistoryGetByParentDTO> medicalHistoryGetDTO = _mapper.Map<List<MedicalHistoryGetByParentDTO>>(medicalHistories);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            foreach (var medicalHistory in medicalHistoryGetDTO)
            {
                if (!string.IsNullOrEmpty(medicalHistory.FirstReport))
                {
                    medicalHistory.FirstReport = $"{serverUrl}{medicalHistory.FirstReport.Replace("\\", "/")}";
                }
                if (!string.IsNullOrEmpty(medicalHistory.SecReport))
                {
                    medicalHistory.SecReport = $"{serverUrl}{medicalHistory.SecReport.Replace("\\", "/")}";
                }
            }

            return Ok(medicalHistoryGetDTO);
        }
        #endregion

        #region Get By ID By Doctor
        [HttpGet("GetByIdByDoctor/id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> GetByIdByDoctorAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            MedicalHistory medicalHistory = await Unit_Of_Work.medicalHistory_Repository.FindByIncludesAsync(
                    m => m.IsDeleted != true && m.Id == id, 
                    query => query.Include(m => m.School),
                    query => query.Include(m => m.Grade),
                    query => query.Include(m => m.Classroom),
                    query => query.Include(m => m.Student)
            );

            if (medicalHistory == null || medicalHistory.IsDeleted == true)
            {
                return NotFound("No Medical History With this ID");
            }

            MedicalHistoryGetByDoctorDTO medicalHistoryGetDTO = _mapper.Map<MedicalHistoryGetByDoctorDTO>(medicalHistory);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            if (!string.IsNullOrEmpty(medicalHistoryGetDTO.FirstReport))
            {
                medicalHistoryGetDTO.FirstReport = $"{serverUrl}{medicalHistoryGetDTO.FirstReport.Replace("\\", "/")}";
            }

            if (!string.IsNullOrEmpty(medicalHistoryGetDTO.SecReport))
            {
                medicalHistoryGetDTO.SecReport = $"{serverUrl}{medicalHistoryGetDTO.SecReport.Replace("\\", "/")}";
            }

            return Ok(medicalHistoryGetDTO);
        }
        #endregion

        #region Get By ID By Parent
        [HttpGet("GetByIdByParent/id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> GetByIdByParentAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            MedicalHistory medicalHistory = await Unit_Of_Work.medicalHistory_Repository.Select_By_IdAsync(id);

            if (medicalHistory == null || medicalHistory.IsDeleted == true)
            {
                return NotFound("No Medical History With this ID");
            }

            MedicalHistoryGetByParentDTO dto = _mapper.Map<MedicalHistoryGetByParentDTO>(medicalHistory);

            string serverUrl = $"{Request.Scheme}://{Request.Host}/";

            if (!string.IsNullOrEmpty(dto.FirstReport))
            {
                dto.FirstReport = $"{serverUrl}{dto.FirstReport.Replace("\\", "/")}";
            }

            if (!string.IsNullOrEmpty(dto.SecReport))
            {
                dto.SecReport = $"{serverUrl}{dto.SecReport.Replace("\\", "/")}";
            }

            return Ok(dto);
        }
        #endregion

        #region Add By Doctor
        [HttpPost("AddByDoctor")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> AddByDoctorAsync([FromForm] MedicalHistoryAddByDoctorDTO historyAddDTO)
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

            Student student = await Unit_Of_Work.student_Repository.Select_By_IdAsync(historyAddDTO.StudentId);

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student With this ID");
            }

            MedicalHistory medicalHistory = _mapper.Map<MedicalHistory>(historyAddDTO);

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

            var enNameExists = student.en_name;

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");
            var medicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
            var medicalHistoryFirstReportFolder = Path.Combine(medicalHistoryFolder, "FirstReport");
            var medicalHistorySecReportFolder = Path.Combine(medicalHistoryFolder, "SecReport");

            if (historyAddDTO.FirstReport != null | historyAddDTO.SecReport != null)
            {
                if (!Directory.Exists(medicalHistoryFirstReportFolder))
                {
                    Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                }
            }

            if (!Directory.Exists(medicalHistorySecReportFolder))
            {
                Directory.CreateDirectory(medicalHistorySecReportFolder);
            }

            if (historyAddDTO.FirstReport != null)
            {
                if (historyAddDTO.FirstReport.Length > 0)
                {
                    var filePath = Path.Combine(medicalHistoryFirstReportFolder, historyAddDTO.FirstReport.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await historyAddDTO.FirstReport.CopyToAsync(stream);
                    }
                }
            }

            if (historyAddDTO.SecReport != null)
            {
                if (historyAddDTO.SecReport.Length > 0)
                {
                    var filePath = Path.Combine(medicalHistorySecReportFolder, historyAddDTO.SecReport.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await historyAddDTO.SecReport.CopyToAsync(stream);
                    }
                }
            }

            if (historyAddDTO.FirstReport != null)
            {
                medicalHistory.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", historyAddDTO.FirstReport.FileName);
                medicalHistory.Attached += 1;
            }
            if (historyAddDTO.SecReport != null)
            {
                medicalHistory.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", historyAddDTO.SecReport.FileName);
                medicalHistory.Attached += 1;
            }

            Unit_Of_Work.medicalHistory_Repository.Update(medicalHistory);
            Unit_Of_Work.SaveChanges();

            return Ok(historyAddDTO);
        }
        #endregion

        #region Add By Parent
        [HttpPost("AddByParent")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> AddByParentAsync([FromForm] MedicalHistoryAddByParentDTO historyAddDTO)
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

            MedicalHistory medicalHistory = _mapper.Map<MedicalHistory>(historyAddDTO);
            string enNameExists = userId.ToString();
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medicalHistory.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                medicalHistory.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee" || userTypeClaim == "parent")
            {
                medicalHistory.InsertedByUserId = userId;
            }

            Unit_Of_Work.medicalHistory_Repository.Add(medicalHistory);
            Unit_Of_Work.SaveChanges();


            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");
            var medicalHistoryFolder = Path.Combine(baseFolder, Guid.NewGuid() + "_" + medicalHistory.Id);
            var medicalHistoryFirstReportFolder = Path.Combine(medicalHistoryFolder, "FirstReport");
            var medicalHistorySecReportFolder = Path.Combine(medicalHistoryFolder, "SecReport");

            if (historyAddDTO.FirstReport != null | historyAddDTO.SecReport != null)
            {
                if (!Directory.Exists(medicalHistoryFirstReportFolder))
                {
                    Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                }
            }

            if (!Directory.Exists(medicalHistorySecReportFolder))
            {
                Directory.CreateDirectory(medicalHistorySecReportFolder);
            }

            if (historyAddDTO.FirstReport != null)
            {
                if (historyAddDTO.FirstReport.Length > 0)
                {
                    medicalHistory.Attached += 1;
                    var filePath = Path.Combine(medicalHistoryFirstReportFolder, historyAddDTO.FirstReport.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await historyAddDTO.FirstReport.CopyToAsync(stream);
                    }
                }
            }

            if (historyAddDTO.SecReport != null)
            {
                if (historyAddDTO.SecReport.Length > 0)
                {
                    medicalHistory.Attached += 1;
                    var filePath = Path.Combine(medicalHistorySecReportFolder, historyAddDTO.SecReport.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await historyAddDTO.SecReport.CopyToAsync(stream);
                    }
                }
            }

            if (historyAddDTO.FirstReport != null)
                medicalHistory.FirstReport = Path.Combine("Uploads", "MedicalHistories", Guid.NewGuid() + "_" + medicalHistory.Id, "FirstReport", historyAddDTO.FirstReport.FileName);
            if (historyAddDTO.SecReport != null)
                medicalHistory.SecReport = Path.Combine("Uploads", "MedicalHistories", Guid.NewGuid() + "_" + medicalHistory.Id, "SecReport", historyAddDTO.SecReport.FileName);

            Unit_Of_Work.medicalHistory_Repository.Update(medicalHistory);
            Unit_Of_Work.SaveChanges();

            return Ok(historyAddDTO);
        }
        #endregion

        #region Update By Doctor
        [HttpPut("UpdateByDoctorAsync")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> UpdateByDoctorAsync([FromForm] MedicalHistoryPutByDoctorDTO historyPutDTO)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (historyPutDTO == null)
            {
                return BadRequest("Medical History cannot be null");
            }

            MedicalHistory medicalHistory = await Unit_Of_Work.medicalHistory_Repository.Select_By_IdAsync(historyPutDTO.Id);

            if (medicalHistory == null || medicalHistory.IsDeleted == true)
            {
                return NotFound("No Medical History with this ID");
            }

            School school = await Unit_Of_Work.school_Repository.Select_By_IdAsync(historyPutDTO.SchoolId);

            if (school == null || school.IsDeleted == true)
            {
                return NotFound("No School with this ID");
            }

            Grade grade = await Unit_Of_Work.grade_Repository.Select_By_IdAsync(historyPutDTO.GradeId);

            if (grade == null || grade.IsDeleted == true)
            {
                return NotFound("No Grade with this ID");
            }

            Classroom classroom = await Unit_Of_Work.classroom_Repository.Select_By_IdAsync(historyPutDTO.ClassRoomID);

            if (classroom == null || classroom.IsDeleted == true)
            {
                return NotFound("No Classroom with this ID");
            }

            Student student = await Unit_Of_Work.student_Repository.Select_By_IdAsync(historyPutDTO.StudentId);

            if (student == null || student.IsDeleted == true)
            {
                return NotFound("No Student with this ID");
            }

            string secReportExists = medicalHistory.FirstReport;
            string mainImageLinkExists = medicalHistory.SecReport;
            string enNameExists = student.en_name;

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Medical History", roleId, userId, medicalHistory);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (historyPutDTO.FirstReportFile != null || historyPutDTO.SecReportFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");

                var oldMedicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
                var oldMedicalHistoryFirstReportFolder = Path.Combine(oldMedicalHistoryFolder, "FirstReport");
                var oldMedicalHistorySecReportFolder = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                var medicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
                var medicalHistoryFirstReportFolder = Path.Combine(medicalHistoryFolder, "FirstReport");
                var medicalHistorySecReportFolder = Path.Combine(medicalHistoryFolder, "SecReport");

                if (historyPutDTO.FirstReportFile != null)
                {
                    string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "FirstReport");

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath); // Delete the old file
                    }
                }

                if (historyPutDTO.SecReportFile != null)
                {
                    string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath); // Delete the old file
                    }
                }

                if (historyPutDTO.FirstReportFile != null && historyPutDTO.SecReportFile != null)
                {
                    if (Directory.Exists(oldMedicalHistoryFolder))
                    {
                        Directory.Delete(oldMedicalHistoryFirstReportFolder, true);
                        Directory.Delete(oldMedicalHistorySecReportFolder, true);
                        Directory.Delete(oldMedicalHistoryFolder, true);
                    }

                    if (!Directory.Exists(medicalHistoryFirstReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                    }

                    if (!Directory.Exists(medicalHistorySecReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistorySecReportFolder);
                    }

                    historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", historyPutDTO.FirstReportFile.FileName);
                    historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", historyPutDTO.SecReportFile.FileName);

                    if (historyPutDTO.FirstReportFile.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistoryFirstReportFolder, historyPutDTO.FirstReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.FirstReportFile.CopyToAsync(stream);
                        }
                    }

                    if (historyPutDTO.SecReportFile.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistorySecReportFolder, historyPutDTO.SecReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.SecReportFile.CopyToAsync(stream);
                        }
                    }
                }
                else if (historyPutDTO.FirstReportFile != null)
                {
                    if (Directory.Exists(oldMedicalHistoryFirstReportFolder))
                    {
                        Directory.Delete(oldMedicalHistoryFirstReportFolder, true);
                    }

                    if (!Directory.Exists(medicalHistoryFirstReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                    }

                    if (!Directory.Exists(medicalHistorySecReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistorySecReportFolder);
                    }

                    historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", historyPutDTO.FirstReportFile.FileName);

                    if (historyPutDTO.FirstReport.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistoryFirstReportFolder, historyPutDTO.FirstReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.FirstReportFile.CopyToAsync(stream);
                        }
                    }

                    if (historyPutDTO.SecReport == null && (historyPutDTO.SecReport == null || medicalHistory.SecReport == null))
                    {
                        historyPutDTO.SecReport = null;
                        string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath); // Delete the old file
                        }
                    }
                    else if (historyPutDTO.SecReport == null && medicalHistory.SecReport != null)
                    {
                        historyPutDTO.SecReport = medicalHistory.SecReport;
                    }

                    if (enNameExists != enNameExists && medicalHistory.SecReport != null)
                    {
                        var filesOther = Directory.GetFiles(oldMedicalHistorySecReportFolder);

                        var fileName = "";

                        foreach (var file in filesOther)
                        {
                            fileName = Path.GetFileName(file);
                            var destFile = Path.Combine(medicalHistorySecReportFolder, fileName);
                            System.IO.File.Move(file, destFile);
                        }

                        Directory.Delete(oldMedicalHistorySecReportFolder);
                        Directory.Delete(oldMedicalHistoryFolder);
                        historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", fileName);
                    }
                }
                else if (historyPutDTO.SecReport != null)
                {
                    if (Directory.Exists(oldMedicalHistorySecReportFolder))
                    {
                        Directory.Delete(oldMedicalHistorySecReportFolder, true);
                    }

                    if (!Directory.Exists(medicalHistoryFirstReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                    }

                    if (!Directory.Exists(medicalHistorySecReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistorySecReportFolder);
                    }

                    historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", historyPutDTO.SecReportFile.FileName);

                    if (historyPutDTO.SecReport.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistorySecReportFolder, historyPutDTO.SecReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.SecReportFile.CopyToAsync(stream);
                        }
                    }

                    if (historyPutDTO.FirstReport == null && (historyPutDTO.FirstReport == null || medicalHistory.FirstReport == null))
                    {
                        historyPutDTO.FirstReport = null;
                        string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "FirstReport");

                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath); // Delete the old file
                        }
                    }
                    else if (historyPutDTO.FirstReport == null && medicalHistory.FirstReport != null)
                    {
                        historyPutDTO.FirstReport = medicalHistory.FirstReport;
                    }

                    if (enNameExists != enNameExists && medicalHistory.FirstReport != null)
                    {
                        var filesMain = Directory.GetFiles(oldMedicalHistoryFirstReportFolder);

                        var fileName = "";
                        foreach (var file in filesMain)
                        {
                            fileName = Path.GetFileName(file);
                            var destFile = Path.Combine(medicalHistoryFirstReportFolder, fileName);
                            System.IO.File.Move(file, destFile);
                        }

                        Directory.Delete(oldMedicalHistoryFirstReportFolder);
                        Directory.Delete(oldMedicalHistoryFolder);
                        historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", fileName);
                    }
                }
                else
                {
                    historyPutDTO.FirstReport = null;
                    historyPutDTO.SecReport = null;
                }
            }
            else
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");

                var oldMedicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
                var oldFirstReportFolder = Path.Combine(oldMedicalHistoryFolder, "FirstReport");
                var oldSecReportFolder = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                var medicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
                var firstReportFolder = Path.Combine(medicalHistoryFolder, "FirstReport");
                var secReportFolder = Path.Combine(medicalHistoryFolder, "SecReport");

                // Check if the path already there or null, as if null so he wants to delete the existing files
                if (historyPutDTO.FirstReport != null || historyPutDTO.SecReport != null)
                {
                    // Rename the folder if it exists
                    if (enNameExists != enNameExists)
                    {
                        if (Directory.Exists(oldMedicalHistoryFolder))
                        {
                            if (!Directory.Exists(firstReportFolder))
                            {
                                Directory.CreateDirectory(firstReportFolder);
                            }
                            if (!Directory.Exists(secReportFolder))
                            {
                                Directory.CreateDirectory(secReportFolder);
                            }

                            var filesFirst = Directory.GetFiles(oldFirstReportFolder);
                            var filesSec = Directory.GetFiles(oldSecReportFolder);
                            if (historyPutDTO.SecReport != null && medicalHistory.SecReport != null)
                            {
                                var fileName = "";
                                foreach (var file in filesSec)
                                {
                                    fileName = Path.GetFileName(file);
                                    var destFile = Path.Combine(secReportFolder, fileName);
                                    System.IO.File.Move(file, destFile);
                                }

                                historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", fileName);
                            }
                            else
                            {
                                historyPutDTO.SecReport = null;
                            }

                            if (historyPutDTO.FirstReport != null && medicalHistory.FirstReport != null)
                            {
                                var fileName = "";

                                foreach (var file in filesFirst)
                                {
                                    fileName = Path.GetFileName(file);
                                    var destFile = Path.Combine(firstReportFolder, fileName);
                                    System.IO.File.Move(file, destFile);
                                }

                                historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", fileName);
                            }
                            else
                            {
                                historyPutDTO.FirstReport = null;
                            }

                            Directory.Delete(firstReportFolder, true);
                            Directory.Delete(secReportFolder, true);
                            Directory.Delete(medicalHistoryFolder, true);
                        }
                        else
                        {
                            if (historyPutDTO.SecReport != null && medicalHistory.SecReport != null)
                            {
                                historyPutDTO.SecReport = medicalHistory.SecReport;
                            }
                            else
                            {
                                historyPutDTO.SecReport = null;
                            }

                            if (historyPutDTO.FirstReport != null && medicalHistory.FirstReport != null)
                            {
                                historyPutDTO.FirstReport = medicalHistory.FirstReport;
                            }
                            else
                            {
                                historyPutDTO.FirstReport = null;
                            }
                        }
                    }
                    else
                    {
                        if (historyPutDTO.FirstReport != null && historyPutDTO.SecReport != null)
                        {
                            historyPutDTO.FirstReport = medicalHistory.FirstReport;
                            historyPutDTO.SecReport = medicalHistory.SecReport;
                        }
                        else if (historyPutDTO.FirstReport == null && historyPutDTO.SecReport == null)
                        {
                            historyPutDTO.FirstReport = null;
                            string existingFilePath = Path.Combine(medicalHistoryFolder, "FirstReport");

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }

                            historyPutDTO.SecReport = null;
                            string existingFilePathOther = Path.Combine(medicalHistoryFolder, "SecReport");

                            if (System.IO.File.Exists(existingFilePathOther))
                            {
                                System.IO.File.Delete(existingFilePathOther); // Delete the old file
                            }
                        }
                        else if (historyPutDTO.FirstReport == null)
                        {
                            historyPutDTO.FirstReport = null;
                            string existingFilePath = Path.Combine(medicalHistoryFolder, "FirstReport");

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }
                        }
                        else if (historyPutDTO.SecReport == null)
                        {
                            historyPutDTO.SecReport = null;
                            string existingFilePath = Path.Combine(medicalHistoryFolder, "SecReport");

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(medicalHistoryFolder))
                    {
                        if (Directory.Exists(firstReportFolder))
                        {
                            Directory.Delete(firstReportFolder, true);
                        }

                        if (Directory.Exists(secReportFolder))
                        {
                            Directory.Delete(secReportFolder, true);
                        }

                        Directory.Delete(medicalHistoryFolder, true);
                    }
                    historyPutDTO.FirstReport = null;
                    historyPutDTO.SecReport = null;
                }
            }

            _mapper.Map(historyPutDTO, medicalHistory);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medicalHistory.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);

            if (userTypeClaim == "octa")
            {
                medicalHistory.UpdatedByOctaId = userId;
                if (medicalHistory.UpdatedByUserId != null)
                {
                    medicalHistory.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                medicalHistory.UpdatedByUserId = userId;
                if (medicalHistory.UpdatedByOctaId != null)
                {
                    medicalHistory.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.medicalHistory_Repository.Update(medicalHistory);
            Unit_Of_Work.SaveChanges();

            return Ok(historyPutDTO);
        }
        #endregion

        #region Update By Parent
        [HttpPut("UpdateByParentAsync")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Medical History" }
        )]
        public async Task<IActionResult> UpdateByParentAsync([FromForm] MedicalHistoryPutByParentDTO historyPutDTO)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);

            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (historyPutDTO == null)
            {
                return BadRequest("Medical History cannot be null");
            }

            MedicalHistory medicalHistory = await Unit_Of_Work.medicalHistory_Repository.Select_By_IdAsync(historyPutDTO.Id);

            if (medicalHistory == null || medicalHistory.IsDeleted == true)
            {
                return NotFound("No Medical History with this ID");
            }

            string secReportExists = medicalHistory.FirstReport;
            string mainImageLinkExists = medicalHistory.SecReport;
            string enNameExists = userId.ToString();

            if (userTypeClaim == "employee" || userTypeClaim == "parent")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Medical History", roleId, userId, medicalHistory);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            if (historyPutDTO.FirstReportFile != null || historyPutDTO.SecReportFile != null)
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");

                var oldMedicalHistoryFolder = Path.Combine(baseFolder, Guid.NewGuid() + "_" + medicalHistory.Id);
                var oldMedicalHistoryFirstReportFolder = Path.Combine(oldMedicalHistoryFolder, "FirstReport");
                var oldMedicalHistorySecReportFolder = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                var medicalHistoryFolder = Path.Combine(baseFolder, Guid.NewGuid() + "_" + medicalHistory.Id);
                var medicalHistoryFirstReportFolder = Path.Combine(medicalHistoryFolder, "FirstReport");
                var medicalHistorySecReportFolder = Path.Combine(medicalHistoryFolder, "SecReport");

                if (historyPutDTO.FirstReportFile != null)
                {
                    string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "FirstReport");

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath); // Delete the old file
                    }
                }

                if (historyPutDTO.SecReportFile != null)
                {
                    string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath); // Delete the old file
                    }
                }

                if (historyPutDTO.FirstReportFile != null && historyPutDTO.SecReportFile != null)
                {
                    if (Directory.Exists(oldMedicalHistoryFolder))
                    {
                        Directory.Delete(oldMedicalHistoryFirstReportFolder, true);
                        Directory.Delete(oldMedicalHistorySecReportFolder, true);
                        Directory.Delete(oldMedicalHistoryFolder, true);
                    }

                    if (!Directory.Exists(medicalHistoryFirstReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                    }

                    if (!Directory.Exists(medicalHistorySecReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistorySecReportFolder);
                    }

                    historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", historyPutDTO.FirstReportFile.FileName);
                    historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", historyPutDTO.SecReportFile.FileName);

                    if (historyPutDTO.FirstReportFile.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistoryFirstReportFolder, historyPutDTO.FirstReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.FirstReportFile.CopyToAsync(stream);
                        }
                    }

                    if (historyPutDTO.SecReportFile.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistorySecReportFolder, historyPutDTO.SecReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.SecReportFile.CopyToAsync(stream);
                        }
                    }
                }
                else if (historyPutDTO.FirstReportFile != null)
                {
                    if (Directory.Exists(oldMedicalHistoryFirstReportFolder))
                    {
                        Directory.Delete(oldMedicalHistoryFirstReportFolder, true);
                    }

                    if (!Directory.Exists(medicalHistoryFirstReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                    }

                    if (!Directory.Exists(medicalHistorySecReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistorySecReportFolder);
                    }

                    historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", historyPutDTO.FirstReportFile.FileName);

                    if (historyPutDTO.FirstReport.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistoryFirstReportFolder, historyPutDTO.FirstReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.FirstReportFile.CopyToAsync(stream);
                        }
                    }

                    if (historyPutDTO.SecReport == null && (historyPutDTO.SecReport == null || medicalHistory.SecReport == null))
                    {
                        historyPutDTO.SecReport = null;
                        string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath); // Delete the old file
                        }
                    }
                    else if (historyPutDTO.SecReport == null && medicalHistory.SecReport != null)
                    {
                        historyPutDTO.SecReport = medicalHistory.SecReport;
                    }

                    if (enNameExists != enNameExists && medicalHistory.SecReport != null)
                    {
                        var filesOther = Directory.GetFiles(oldMedicalHistorySecReportFolder);

                        var fileName = "";

                        foreach (var file in filesOther)
                        {
                            fileName = Path.GetFileName(file);
                            var destFile = Path.Combine(medicalHistorySecReportFolder, fileName);
                            System.IO.File.Move(file, destFile);
                        }

                        Directory.Delete(oldMedicalHistorySecReportFolder);
                        Directory.Delete(oldMedicalHistoryFolder);
                        historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", fileName);
                    }
                }
                else if (historyPutDTO.SecReport != null)
                {
                    if (Directory.Exists(oldMedicalHistorySecReportFolder))
                    {
                        Directory.Delete(oldMedicalHistorySecReportFolder, true);
                    }

                    if (!Directory.Exists(medicalHistoryFirstReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistoryFirstReportFolder);
                    }

                    if (!Directory.Exists(medicalHistorySecReportFolder))
                    {
                        Directory.CreateDirectory(medicalHistorySecReportFolder);
                    }

                    historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", historyPutDTO.SecReportFile.FileName);

                    if (historyPutDTO.SecReport.Length > 0)
                    {
                        var filePath = Path.Combine(medicalHistorySecReportFolder, historyPutDTO.SecReportFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await historyPutDTO.SecReportFile.CopyToAsync(stream);
                        }
                    }

                    if (historyPutDTO.FirstReport == null && (historyPutDTO.FirstReport == null || medicalHistory.FirstReport == null))
                    {
                        historyPutDTO.FirstReport = null;
                        string existingFilePath = Path.Combine(oldMedicalHistoryFolder, "FirstReport");

                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath); // Delete the old file
                        }
                    }
                    else if (historyPutDTO.FirstReport == null && medicalHistory.FirstReport != null)
                    {
                        historyPutDTO.FirstReport = medicalHistory.FirstReport;
                    }

                    if (enNameExists != enNameExists && medicalHistory.FirstReport != null)
                    {
                        var filesMain = Directory.GetFiles(oldMedicalHistoryFirstReportFolder);

                        var fileName = "";
                        foreach (var file in filesMain)
                        {
                            fileName = Path.GetFileName(file);
                            var destFile = Path.Combine(medicalHistoryFirstReportFolder, fileName);
                            System.IO.File.Move(file, destFile);
                        }

                        Directory.Delete(oldMedicalHistoryFirstReportFolder);
                        Directory.Delete(oldMedicalHistoryFolder);
                        historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", fileName);
                    }
                }
                else
                {
                    historyPutDTO.FirstReport = null;
                    historyPutDTO.SecReport = null;
                }
            }
            else
            {
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/MedicalHistories");

                var oldMedicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
                var oldFirstReportFolder = Path.Combine(oldMedicalHistoryFolder, "FirstReport");
                var oldSecReportFolder = Path.Combine(oldMedicalHistoryFolder, "SecReport");

                var medicalHistoryFolder = Path.Combine(baseFolder, enNameExists + "_" + medicalHistory.Id);
                var firstReportFolder = Path.Combine(medicalHistoryFolder, "FirstReport");
                var secReportFolder = Path.Combine(medicalHistoryFolder, "SecReport");

                // Check if the path already there or null, as if null so he wants to delete the existing files
                if (historyPutDTO.FirstReport != null || historyPutDTO.SecReport != null)
                {
                    // Rename the folder if it exists
                    if (enNameExists != enNameExists)
                    {
                        if (Directory.Exists(oldMedicalHistoryFolder))
                        {
                            if (!Directory.Exists(firstReportFolder))
                            {
                                Directory.CreateDirectory(firstReportFolder);
                            }
                            if (!Directory.Exists(secReportFolder))
                            {
                                Directory.CreateDirectory(secReportFolder);
                            }

                            var filesFirst = Directory.GetFiles(oldFirstReportFolder);
                            var filesSec = Directory.GetFiles(oldSecReportFolder);
                            if (historyPutDTO.SecReport != null && medicalHistory.SecReport != null)
                            {
                                var fileName = "";
                                foreach (var file in filesSec)
                                {
                                    fileName = Path.GetFileName(file);
                                    var destFile = Path.Combine(secReportFolder, fileName);
                                    System.IO.File.Move(file, destFile);
                                }

                                historyPutDTO.SecReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "SecReport", fileName);
                            }
                            else
                            {
                                historyPutDTO.SecReport = null;
                            }

                            if (historyPutDTO.FirstReport != null && medicalHistory.FirstReport != null)
                            {
                                var fileName = "";

                                foreach (var file in filesFirst)
                                {
                                    fileName = Path.GetFileName(file);
                                    var destFile = Path.Combine(firstReportFolder, fileName);
                                    System.IO.File.Move(file, destFile);
                                }

                                historyPutDTO.FirstReport = Path.Combine("Uploads", "MedicalHistories", enNameExists + "_" + medicalHistory.Id, "FirstReport", fileName);
                            }
                            else
                            {
                                historyPutDTO.FirstReport = null;
                            }

                            Directory.Delete(firstReportFolder, true);
                            Directory.Delete(secReportFolder, true);
                            Directory.Delete(medicalHistoryFolder, true);
                        }
                        else
                        {
                            if (historyPutDTO.SecReport != null && medicalHistory.SecReport != null)
                            {
                                historyPutDTO.SecReport = medicalHistory.SecReport;
                            }
                            else
                            {
                                historyPutDTO.SecReport = null;
                            }

                            if (historyPutDTO.FirstReport != null && medicalHistory.FirstReport != null)
                            {
                                historyPutDTO.FirstReport = medicalHistory.FirstReport;
                            }
                            else
                            {
                                historyPutDTO.FirstReport = null;
                            }
                        }
                    }
                    else
                    {
                        if (historyPutDTO.FirstReport != null && historyPutDTO.SecReport != null)
                        {
                            historyPutDTO.FirstReport = medicalHistory.FirstReport;
                            historyPutDTO.SecReport = medicalHistory.SecReport;
                        }
                        else if (historyPutDTO.FirstReport == null && historyPutDTO.SecReport == null)
                        {
                            historyPutDTO.FirstReport = null;
                            string existingFilePath = Path.Combine(medicalHistoryFolder, "FirstReport");

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }

                            historyPutDTO.SecReport = null;
                            string existingFilePathOther = Path.Combine(medicalHistoryFolder, "SecReport");

                            if (System.IO.File.Exists(existingFilePathOther))
                            {
                                System.IO.File.Delete(existingFilePathOther); // Delete the old file
                            }
                        }
                        else if (historyPutDTO.FirstReport == null)
                        {
                            historyPutDTO.FirstReport = null;
                            string existingFilePath = Path.Combine(medicalHistoryFolder, "FirstReport");

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }
                        }
                        else if (historyPutDTO.SecReport == null)
                        {
                            historyPutDTO.SecReport = null;
                            string existingFilePath = Path.Combine(medicalHistoryFolder, "SecReport");

                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath); // Delete the old file
                            }
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(medicalHistoryFolder))
                    {
                        if (Directory.Exists(firstReportFolder))
                        {
                            Directory.Delete(firstReportFolder, true);
                        }

                        if (Directory.Exists(secReportFolder))
                        {
                            Directory.Delete(secReportFolder, true);
                        }

                        Directory.Delete(medicalHistoryFolder, true);
                    }

                    historyPutDTO.FirstReport = null;
                    historyPutDTO.SecReport = null;
                }
            }

            _mapper.Map(historyPutDTO, medicalHistory);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medicalHistory.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            
            if (userTypeClaim == "octa")
            {
                medicalHistory.UpdatedByOctaId = userId;
                if (medicalHistory.UpdatedByUserId != null)
                {
                    medicalHistory.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee" || userTypeClaim == "parent")
            {
                medicalHistory.UpdatedByUserId = userId;
                if (medicalHistory.UpdatedByOctaId != null)
                {
                    medicalHistory.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.medicalHistory_Repository.Update(medicalHistory);
            Unit_Of_Work.SaveChanges();

            return Ok(historyPutDTO);
        }
        #endregion

        #region Delete
        [HttpDelete("id")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Medical History" }
        )]
        public IActionResult DeleteAsync(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            MedicalHistory medicalHistory = Unit_Of_Work.medicalHistory_Repository.First_Or_Default(m => m.IsDeleted != true && m.Id == id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            
            medicalHistory.IsDeleted = true;
            
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            medicalHistory.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            
            if (userTypeClaim == "octa")
            {
                medicalHistory.DeletedByOctaId = userId;
                if (medicalHistory.DeletedByUserId != null)
                {
                    medicalHistory.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee" || userTypeClaim == "parent")
            {
                medicalHistory.DeletedByUserId = userId;
                if (medicalHistory.DeletedByOctaId != null)
                {
                    medicalHistory.DeletedByOctaId = null;
                }
            }
            
            Unit_Of_Work.medicalHistory_Repository.Update(medicalHistory);
            Unit_Of_Work.SaveChanges();
            
            return Ok("Medical History Deleted Successfully");
        }
        #endregion
    }
}
