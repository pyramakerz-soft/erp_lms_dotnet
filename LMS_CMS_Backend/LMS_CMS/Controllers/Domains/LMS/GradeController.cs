﻿using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Unicode;

namespace LMS_CMS_PL.Controllers.Domains.LMS
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class GradeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public GradeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]

        public async Task<IActionResult> GetAsync()
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
            List<Grade> Grades = await Unit_Of_Work.grade_Repository.Select_All_With_IncludesById<Grade>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.Section));

            if (Grades == null || Grades.Count == 0)
            {
                return NotFound();
            }

            List<GradeGetDTO> GradeDTO = mapper.Map<List<GradeGetDTO>>(Grades);

            return Ok(GradeDTO);
        }
        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetBySection/{id}")]

        public async Task<IActionResult> GetAsync(long id)
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
            List<Grade> Grades = await Unit_Of_Work.grade_Repository.Select_All_With_IncludesById<Grade>(
                    sem => sem.IsDeleted != true&&sem.SectionID==id,
                    query => query.Include(emp => emp.Section));

            if (Grades == null || Grades.Count == 0)
            {
                return NotFound();
            }

            List<GradeGetDTO> GradeDTO = mapper.Map<List<GradeGetDTO>>(Grades);

            return Ok(GradeDTO);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("id")]

        public async Task<IActionResult> GetAsyncByID(long id)
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
            Grade grade = await Unit_Of_Work.grade_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.Section));

            if (grade == null)
            {
                return NotFound("there is no Grade With This Id");
            }

            GradeGetDTO gradeDTO = mapper.Map<GradeGetDTO>(grade);

            return Ok(gradeDTO);
        }

        //////////////////////////////////////////////////////////////////////////////////

        [HttpPost]

        public async Task<IActionResult> Add(GradeAddDTO Newgrade)
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
            if (Newgrade == null)
            {
                return BadRequest("Grade is empty");
            }
            if (Newgrade.SectionID == null)
            {
                return BadRequest("section id can not be null");
            }
            Section section=Unit_Of_Work.section_Repository.First_Or_Default(s=>s.ID== Newgrade.SectionID);
            if (section == null)
            {
                return BadRequest("this section not found");
            }
            Grade grade = mapper.Map<Grade>(Newgrade);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            grade.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                grade.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                grade.InsertedByUserId = userId;
            }
            Unit_Of_Work.grade_Repository.Add(grade);
            Unit_Of_Work.SaveChanges();
            return Ok(Newgrade);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        public async Task<IActionResult> EditAsync(GradeGetDTO newGrade)
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

            if (newGrade == null)
            {
                return BadRequest("Semester cannot be null");
            }
            if (newGrade.SectionID == null)
            {
                return BadRequest("section id can not be null");
            }
            Section section = Unit_Of_Work.section_Repository.First_Or_Default(s => s.ID == newGrade.SectionID);
            if (section == null)
            {
                return BadRequest("this section not found");
            }

            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(s => s.ID == newGrade.ID);
            if (grade == null)
            {
                return BadRequest("this grade not exist");
            }
            mapper.Map(newGrade, grade);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            grade.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                grade.UpdatedByOctaId = userId;
                if (grade.UpdatedByUserId != null)
                {
                    grade.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                grade.UpdatedByUserId = userId;
                if (grade.UpdatedByOctaId != null)
                {
                    grade.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.grade_Repository.Update(grade);
            Unit_Of_Work.SaveChanges();
            return Ok(newGrade);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete]
        public IActionResult delete(long id)
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

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            Grade grade = Unit_Of_Work.grade_Repository.Select_By_Id(id);

            if (grade == null || grade.IsDeleted == true)
            {
                return NotFound("No grade with this ID");
            }
            grade.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            grade.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                grade.DeletedByOctaId = userId;
                if (grade.DeletedByUserId != null)
                {
                    grade.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                grade.DeletedByUserId = userId;
                if (grade.DeletedByOctaId != null)
                {
                    grade.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.grade_Repository.Update(grade);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }
    }
}