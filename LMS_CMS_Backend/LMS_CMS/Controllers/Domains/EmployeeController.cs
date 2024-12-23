using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Migrations.Domains;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace LMS_CMS_PL.Controllers.Domains
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public EmployeeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////////

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
            List<Employee> Employees = await Unit_Of_Work.employee_Repository.Select_All_With_IncludesById<Employee>(
                    sem => sem.IsDeleted != true,
                    query => query.Include(emp => emp.BusCompany),
                    query => query.Include(emp => emp.EmployeeType),
                    query => query.Include(emp => emp.Role));

            if (Employees == null || Employees.Count == 0)
            {
                return NotFound();
            }

            List<Employee_GetDTO> EmployeesDTO = mapper.Map<List<Employee_GetDTO>>(Employees);
            foreach (var employeeDTO in EmployeesDTO)
            {
                var employeeFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments", employeeDTO.User_Name);
                if (Directory.Exists(employeeFolder))
                {
                    var files = Directory.GetFiles(employeeFolder)
                                         .Select(filePath => new FileDetailsDTO
                                         {
                                             FileName = Path.GetFileName(filePath),
                                             DownloadUrl = $"{Request.Scheme}://{Request.Host}/Attachments/{employeeDTO.User_Name}/{Path.GetFileName(filePath)}"
                                         })
                                         .ToList();
                    employeeDTO.Files = files;
                }
                else
                {
                    employeeDTO.Files = new List<FileDetailsDTO>();
                }
            }

            return Ok(EmployeesDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("GetByTypeId/{TypeId}")]
        public async Task<IActionResult> GetByTypeIDAsync(long TypeId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
            EmployeeType type = Unit_Of_Work.employeeType_Repository.Select_By_Id(TypeId);
            if (type == null)
            {
                return NotFound("No Type with this Id");
            }
            List<Employee> employees = await Unit_Of_Work.employee_Repository.Select_All_With_IncludesById<Employee>(
                    sem => sem.IsDeleted != true && sem.EmployeeTypeID == TypeId,
                    query => query.Include(emp => emp.BusCompany),
                    query => query.Include(emp => emp.EmployeeType),
                    query => query.Include(emp => emp.Role));

            if (employees == null || employees.Count == 0)
            {
                return NotFound("There is no employees with this type");
            }

            List<Employee_GetDTO> employeeDTOs = mapper.Map<List<Employee_GetDTO>>(employees);
            foreach (var employeeDTO in employeeDTOs)
            {
                var employeeFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments", employeeDTO.User_Name);
                if (Directory.Exists(employeeFolder))
                {
                    var files = Directory.GetFiles(employeeFolder)
                                         .Select(filePath => new FileDetailsDTO
                                         {
                                             FileName = Path.GetFileName(filePath),
                                             DownloadUrl = $"{Request.Scheme}://{Request.Host}/Attachments/{employeeDTO.User_Name}/{Path.GetFileName(filePath)}"
                                         })
                                         .ToList();
                    employeeDTO.Files = files; 
                }
                else
                {
                    employeeDTO.Files = new List<FileDetailsDTO>(); 
                }
            }

            return Ok(employeeDTOs);
        }


        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{empId}")]
        public async Task<IActionResult> GetByIDAsync(long empId)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            Employee employee = await Unit_Of_Work.employee_Repository.FindByIncludesAsync(
                    emp => emp.IsDeleted != true && emp.ID == empId,
                    query => query.Include(emp => emp.BusCompany),
                    query => query.Include(emp => emp.EmployeeType),
                    query => query.Include(emp => emp.Role));

            if (employee == null || employee.IsDeleted == true)
            {
                return NotFound("No employee found");
            }

            Employee_GetDTO employeeDTO = mapper.Map<Employee_GetDTO>(employee);

            var employeeFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments", employee.User_Name);

            if (Directory.Exists(employeeFolder))
            {
                var files = Directory.GetFiles(employeeFolder)
                                     .Select(filePath => new FileDetailsDTO
                                     {
                                         FileName = Path.GetFileName(filePath),
                                         DownloadUrl = $"{Request.Scheme}://{Request.Host}/Attachments/{employee.User_Name}/{Path.GetFileName(filePath)}"
                                     })
                                     .ToList();

                employeeDTO.Files = files; // Assigning List<FileDetailsDTO>
            }
            else
            {
                employeeDTO.Files = new List<FileDetailsDTO>(); // Empty list if folder does not exist
            }

            return Ok(employeeDTO);
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        //D:\ERP_System\erp_lms_dotnet\LMS_CMS_Backend\LMS_CMS\Attachments\
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] EmployeeAddDTO NewEmployee, [FromForm] List<IFormFile> files)
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
            if (NewEmployee == null)
            {
                return BadRequest("Employee data is required.");
            }

            Employee employee = mapper.Map<Employee>(NewEmployee);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            employee.InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employee.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                employee.InsertedByUserId = userId;
            }

            Unit_Of_Work.employee_Repository.Add(employee);
            Unit_Of_Work.SaveChanges();

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments");
            var employeeFolder = Path.Combine(baseFolder, employee.User_Name);
            if (!Directory.Exists(employeeFolder))
            {
                Directory.CreateDirectory(employeeFolder);
            }

            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine(employeeFolder, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    EmployeeAttachment uploadedFile = new EmployeeAttachment
                    {
                        EmployeeID = employee.ID,
                        Link = Path.Combine("Attachments", employee.User_Name, file.FileName), // Relative path
                        Name = file.FileName,
                    };

                    Unit_Of_Work.employeeAttachment_Repository.Add(uploadedFile);
                    Unit_Of_Work.SaveChanges();
                }
            }

            return Ok(NewEmployee);
        }

        ////////////////////////////////////////////////////

        [HttpPut]
        public async Task<IActionResult> EditAsync([FromForm] EmployeePutDTO newEmployee, [FromForm] List<IFormFile> files)
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

            if (newEmployee == null)
            {
                return BadRequest("School cannot be null");
            }

            Employee employee = mapper.Map<Employee>(newEmployee);

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            employee.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employee.UpdatedByOctaId = userId;
                if (employee.UpdatedByUserId != null)
                {
                    employee.UpdatedByUserId = null;
                }

            }
            else if (userTypeClaim == "employee")
            {
                employee.UpdatedByUserId = userId;
                if (employee.UpdatedByOctaId != null)
                {
                    employee.UpdatedByOctaId = null;
                }
            }
            Employee oldEmp = await Unit_Of_Work.employee_Repository.Select_By_IdAsync(newEmployee.ID);
            if (oldEmp.User_Name != newEmployee.User_Name)
            {
                var OldbaseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments");
                var oldEmployeeFolder = Path.Combine(OldbaseFolder, oldEmp.User_Name);
                var newEmployeeFolder = Path.Combine(OldbaseFolder, newEmployee.User_Name);

                if (Directory.Exists(oldEmployeeFolder))
                {
                    try
                    {
                       Directory.Move(oldEmployeeFolder, newEmployeeFolder);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while renaming the folder: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Old folder does not exist.");
                }
            }
            Unit_Of_Work.employee_Repository.Update(employee);
            Unit_Of_Work.SaveChanges();
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Attachments");
            var employeeFolder = Path.Combine(baseFolder, employee.User_Name);
            if (!Directory.Exists(employeeFolder))
            {
                Directory.CreateDirectory(employeeFolder);
            }

            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine(employeeFolder, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    EmployeeAttachment uploadedFile = new EmployeeAttachment
                    {
                        EmployeeID = employee.ID,
                        Link = Path.Combine("Attachments", employee.User_Name, file.FileName), // Relative path
                        Name = file.FileName,
                    };
                }
                Unit_Of_Work.SaveChanges();
            }
            return Ok(newEmployee);

        }

        //////////////////////////////////////////////////////

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
            Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(id);

            if (employee == null || employee.IsDeleted == true)
            {
                return NotFound("No semester with this ID");
            }
            employee.IsDeleted = true;
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            employee.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employee.DeletedByOctaId = userId;
                if (employee.DeletedByUserId != null)
                {
                    employee.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                employee.DeletedByUserId = userId;
                if (employee.DeletedByOctaId != null)
                {
                    employee.DeletedByOctaId = null;
                }
            }

            Unit_Of_Work.employee_Repository.Update(employee);
            Unit_Of_Work.SaveChanges();
            return Ok();
        }

        //////////////////////////////////////////////////////


      [HttpDelete("DeleteFiles")]
    public IActionResult DeleteFiles(FileDetailsDTO file)
    {
        if (string.IsNullOrEmpty(file.FolderName) || string.IsNullOrEmpty(file.FileName))
        {
            return BadRequest(new { message = "Invalid file details provided." });
        }
        if (file.FolderName.Contains("..") || file.FileName.Contains(".."))
        {
            return BadRequest(new { message = "Invalid file path." });
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Attachments", file.FolderName, file.FileName);
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(new { message = "File not found." });
        }

        try
        {
            System.IO.File.Delete(filePath);
            return Ok(new { message = "File deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred while deleting the file: {ex.Message}" });
        }
    }



}
}
