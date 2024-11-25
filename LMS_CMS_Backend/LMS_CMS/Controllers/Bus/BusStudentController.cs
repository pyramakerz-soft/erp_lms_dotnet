using AutoMapper;
using LMS_CMS_BL.DTO.Bus;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations;
using LMS_CMS_DAL.Models;
using LMS_CMS_DAL.Models.BusModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusModel = LMS_CMS_DAL.Models.BusModule.Bus;

namespace LMS_CMS_PL.Controllers.Bus
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusStudentController : ControllerBase
    {
        private UOW Unit_Of_Work;
        IMapper mapper;

        public BusStudentController(UOW Unit_Of_Work, IMapper mapper)
        {
            this.Unit_Of_Work = Unit_Of_Work;
            this.mapper = mapper;
        }

        [HttpGet("GetByBusId/{busId}")]
        public async Task<IActionResult> GetByBusID(long busId)
        {
            List<BusStudent> busStudents = await Unit_Of_Work.busStudent_Repository.Select_All_With_IncludesById<BusStudent>(
                bus => bus.BusID == busId && bus.IsDeleted!=true,
                query => query.Include(bus => bus.Bus),
                query => query.Include(stu => stu.Student),
                query => query.Include(busCat => busCat.BusCategory),
                query => query.Include(sem => sem.Semester)
                );

            if (busStudents == null || busStudents.Count == 0)
            {
                return NotFound();
            }

            List<BusStudentGetDTO> busStudentDTOs = mapper.Map<List<BusStudentGetDTO>>(busStudents);

            return Ok(busStudentDTOs);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByID(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("Enter Bus Student ID");
            }

            var busStudent = await Unit_Of_Work.busStudent_Repository.FindByIncludesAsync(
                busStu => busStu.ID == Id&& busStu.IsDeleted!=true,
                query => query.Include(e => e.Bus),
                query => query.Include(e => e.Student),
                query => query.Include(e => e.BusCategory),
                query => query.Include(e => e.Semester)
                );

            if (busStudent == null)
            {
                return NotFound();
            }

            BusStudentGetDTO busStudentDTO = mapper.Map<BusStudentGetDTO>(busStudent);

            return Ok(busStudentDTO);
        }

        [HttpPost]
        public ActionResult Add(BusStudent_AddDTO busStudentAddDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest(new { message = "Invalid User ID in token." });
            }
            if (busStudentAddDTO == null)
            {
                return BadRequest("Bus Student cannot be null.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(busStudentAddDTO.BusID);
            if (bus == null)
            {
                return NotFound("No Bus with this ID");
            }

            Student student = Unit_Of_Work.student_Repository.Select_By_Id(busStudentAddDTO.StudentID);
            if (bus == null)
            {
                return NotFound("No Student with this ID");
            }

            if (busStudentAddDTO.SemseterID != null)
            {
                Semester semester = Unit_Of_Work.semester_Repository.Select_By_Id(busStudentAddDTO.SemseterID);
                if (semester == null)
                {
                    return NotFound("No Semester with this ID");
                }
            }

            if (busStudentAddDTO.BusCategoryID != null)
            {
                BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(busStudentAddDTO.BusCategoryID);
                if (busCategory == null)
                {
                    return NotFound("No Bus Category with this ID");
                }
            }

            BusStudent busStudent = mapper.Map<BusStudent>(busStudentAddDTO);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busStudent.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            busStudent.InsertedByUserId = userId;
            Unit_Of_Work.busStudent_Repository.Add(busStudent);
            Unit_Of_Work.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { Id = bus.ID }, busStudentAddDTO);
        }

        [HttpPut]
        public ActionResult Edit(BusStudent_PutDTO busStudentPutDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest(new { message = "Invalid User ID in token." });
            }
            if (busStudentPutDTO == null)
            {
                return BadRequest("Bus Student cannot be null.");
            }

            BusModel bus = Unit_Of_Work.bus_Repository.Select_By_Id(busStudentPutDTO.BusID);
            if (bus == null)
            {
                return NotFound("No Bus with this ID");
            }

            Student student = Unit_Of_Work.student_Repository.Select_By_Id(busStudentPutDTO.StudentID);
            if (bus == null)
            {
                return NotFound("No Student with this ID");
            }

            if (busStudentPutDTO.SemseterID != null)
            {
                Semester semester = Unit_Of_Work.semester_Repository.Select_By_Id(busStudentPutDTO.SemseterID);
                if (semester == null)
                {
                    return NotFound("No Semester with this ID");
                }
            }

            if (busStudentPutDTO.BusCategoryID != null)
            {
                BusCategory busCategory = Unit_Of_Work.busCategory_Repository.Select_By_Id(busStudentPutDTO.BusCategoryID);
                if (busCategory == null)
                {
                    return NotFound("No Bus Category with this ID");
                }
            }

            BusStudent busStudentExists = Unit_Of_Work.busStudent_Repository.Select_By_Id(busStudentPutDTO.ID);
            if (busStudentExists == null)
            {
                return NotFound("No Bus Student with this ID");
            }

            mapper.Map(busStudentPutDTO, busStudentExists);
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            busStudentExists.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            busStudentExists.UpdatedByUserId = userId;
            Unit_Of_Work.busStudent_Repository.Update(busStudentExists);
            Unit_Of_Work.SaveChanges();

            return Ok(busStudentPutDTO);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(long Id)
        {
            if (Id == 0)
            {
                return BadRequest("Bus Student ID cannot be null.");
            }

            BusStudent busStudent = Unit_Of_Work.busStudent_Repository.Select_By_Id(Id);
            if (busStudent == null)
            {
                return NotFound();
            }
            else
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return BadRequest(new { message = "User ID not found in token." });
                }
                int userId;
                if (!int.TryParse(userIdClaim.Value, out userId))
                {
                    return BadRequest(new { message = "Invalid User ID in token." });
                }
                busStudent.IsDeleted = true;
                busStudent.DeletedByUserId = userId;
                TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                busStudent.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                Unit_Of_Work.busStudent_Repository.Update(busStudent);
                Unit_Of_Work.SaveChanges();
                return Ok("Bus Student has Successfully been deleted");
            }
        }
    }
}
