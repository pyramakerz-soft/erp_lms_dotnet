using AutoMapper;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Accounting
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class FeesActivationController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public FeesActivationController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ////

        [HttpGet]
        [Authorize_Endpoint_(
           allowedTypes: new[] { "octa", "employee" },
           pages: new[] { "Accounting", "Fees Activation" }
       )]
        public async Task<IActionResult> GetAsync()
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            // Get all students
            List<StudentAcademicYear> studentsAcademicYear = await Unit_Of_Work.studentAcademicYear_Repository
                .Select_All_With_IncludesById<StudentAcademicYear>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.Student),
                    query => query.Include(emp => emp.Grade).ThenInclude(g => g.Section),
                    query => query.Include(emp => emp.Classroom),
                    query => query.Include(emp => emp.School)
                );

            // Get all fees activations
            List<FeesActivation> feesActivations = await Unit_Of_Work.feesActivation_Repository
                .Select_All_With_IncludesById<FeesActivation>(
                    f => f.IsDeleted != true,
                    query => query.Include(Income => Income.Student),
                    query => query.Include(Income => Income.TuitionDiscountType),
                    query => query.Include(Income => Income.AcademicYear),
                    query => query.Include(Income => Income.TuitionFeesType)
                );

            // Perform left join: Get all students and match with feesActivations where available
            var result = studentsAcademicYear
                .GroupJoin(
                    feesActivations,
                    student => student.StudentID, // Key from StudentAcademicYear
                    fee => fee.StudentID,        // Key from FeesActivation
                    (student, fees) => new
                    {
                        student,
                        fee = fees.FirstOrDefault() // Get the first match if exists, otherwise null
                    }
                )
                .Select(joined => new FeesActivationGetDTO
                {
                    StudentID = joined.student.StudentID,
                    StudentName = joined.student.Student.User_Name,
                    AcademicYearId = joined.student.GradeID, // Assuming GradeID represents the academic year
                    AcademicYearName = joined.student.Grade?.Name ?? "",

                    // Fees Information (nullable when no matching fee exists)
                    ID = joined.fee?.ID ?? 0,
                    Amount = joined.fee?.Amount ?? 0,
                    Discount = joined.fee?.Discount ?? 0,
                    Net = joined.fee?.Net ?? 0,
                    FeeTypeID = joined.fee?.FeeTypeID ?? 0,
                    FeeTypeName = joined.fee?.TuitionFeesType?.Name ?? null,
                    FeeDiscountTypeID = joined.fee?.FeeDiscountTypeID,
                    FeeDiscountTypeName = joined.fee?.TuitionDiscountType?.Name ?? null
                })
                .ToList();

            return Ok(result);
        }

        /////

        [HttpPost]
        [Authorize_Endpoint_(
             allowedTypes: new[] { "octa", "employee" },
             pages: new[] { "Fees Activation", "Accounting" }
         )]
        public IActionResult Add(FeesActivationAdddDTO newActivation)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (newActivation == null)
            {
                return BadRequest("Debit cannot be null");
            }

            TuitionFeesType tuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newActivation.FeeTypeID && t.IsDeleted != true);
            if (tuitionFeesType == null)
            {
                return NotFound();
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newActivation.StudentID && t.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }

            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(t => t.ID == newActivation.AcademicYearId && t.IsDeleted != true);
            if (academicYear == null)
            {
                return NotFound();
            }

            if (newActivation.FeeDiscountTypeID!=0 && newActivation.FeeDiscountTypeID != null)
            {
                TuitionDiscountType tuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newActivation.FeeDiscountTypeID && t.IsDeleted != true);
                if (tuitionDiscountType == null)
                {
                    return NotFound();
                }

            }
            else
            {
                newActivation.FeeDiscountTypeID = null;
            }

            FeesActivation feesActivation = mapper.Map<FeesActivation>(newActivation);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            feesActivation.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                feesActivation.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                feesActivation.InsertedByUserId = userId;
            }

            Unit_Of_Work.feesActivation_Repository.Add(feesActivation);
            Unit_Of_Work.SaveChanges();
            return Ok(newActivation);
        }

        //////

        [HttpPut]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowEdit: 1,
          pages: new[] { "Fees Activation", "Accounting" }
      )]
        public IActionResult Edit(FeesActivationGetDTO newActivation)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID, Type claim not found.");
            }

            if (newActivation == null)
            {
                return BadRequest("Income cannot be null");
            }

            FeesActivation feesActivation = Unit_Of_Work.feesActivation_Repository.First_Or_Default(f=>f.ID==newActivation.ID && f.IsDeleted !=true);
            if (feesActivation == null) 
            {
                return NotFound();
            }
            TuitionFeesType tuitionFeesType = Unit_Of_Work.tuitionFeesType_Repository.First_Or_Default(t => t.ID == newActivation.FeeTypeID && t.IsDeleted != true);
            if (tuitionFeesType == null)
            {
                return NotFound();
            }

            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(t => t.ID == newActivation.AcademicYearId && t.IsDeleted != true);
            if (academicYear == null)
            {
                return NotFound();
            }

            Student student = Unit_Of_Work.student_Repository.First_Or_Default(t => t.ID == newActivation.StudentID && t.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }

            if (newActivation.FeeDiscountTypeID != 0 && newActivation.FeeDiscountTypeID != null)
            {
                TuitionDiscountType tuitionDiscountType = Unit_Of_Work.tuitionDiscountType_Repository.First_Or_Default(t => t.ID == newActivation.FeeDiscountTypeID && t.IsDeleted != true);
                if (tuitionDiscountType == null)
                {
                    return NotFound();
                }

            }
            else
            {
                newActivation.FeeDiscountTypeID = null;
            }
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Fees Activation");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (feesActivation.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Fees Activation page doesn't exist");
                }
            }

            mapper.Map(newActivation, feesActivation);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            feesActivation.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                feesActivation.UpdatedByOctaId = userId;
                if (feesActivation.UpdatedByUserId != null)
                {
                    feesActivation.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                feesActivation.UpdatedByUserId = userId;
                if (feesActivation.UpdatedByOctaId != null)
                {
                    feesActivation.UpdatedByOctaId = null;
                }
            }

            Unit_Of_Work.feesActivation_Repository.Update(feesActivation);
            Unit_Of_Work.SaveChanges();
            return Ok(newActivation);
        }

        ///////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
          allowedTypes: new[] { "octa", "employee" },
          allowDelete: 1,
          pages: new[] { "Fees Activation", "Accounting" }
      )]
        public IActionResult Delete(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
            var userRoleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            long.TryParse(userRoleClaim, out long roleId);

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }

            if (id == 0)
            {
                return BadRequest("Enter Income ID");
            }

            FeesActivation feesActivation = Unit_Of_Work.feesActivation_Repository.First_Or_Default(f => f.ID == id && f.IsDeleted != true);
            if (feesActivation == null)
            {
                return NotFound();
            }

            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Fees Activation");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (feesActivation.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Income page doesn't exist");
                }
            }

            feesActivation.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            feesActivation.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                feesActivation.DeletedByOctaId = userId;
                if (feesActivation.DeletedByUserId != null)
                {
                    feesActivation.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                feesActivation.DeletedByUserId = userId;
                if (feesActivation.DeletedByOctaId != null)
                {
                    feesActivation.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.feesActivation_Repository.Update(feesActivation);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

    }
}
