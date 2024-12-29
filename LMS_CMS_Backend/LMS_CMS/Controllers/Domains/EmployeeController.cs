using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Text.RegularExpressions;


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
                List<EmployeeAttachment> employeeAttachments = Unit_Of_Work.employeeAttachment_Repository.FindBy(s => s.EmployeeID == employeeDTO.ID);
                List<EmployeeAttachmentDTO> filesDTO = mapper.Map<List<EmployeeAttachmentDTO>>(employeeAttachments);
                if(filesDTO!=null)
                employeeDTO.Files = filesDTO;
                else
                employeeDTO.Files = new List<EmployeeAttachmentDTO>();

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
                List<EmployeeAttachment> employeeAttachments = Unit_Of_Work.employeeAttachment_Repository.FindBy(s => s.EmployeeID == employeeDTO.ID);
                List<EmployeeAttachmentDTO> filesDTO = mapper.Map<List<EmployeeAttachmentDTO>>(employeeAttachments);
                if (filesDTO != null)
                    employeeDTO.Files = filesDTO;
                else
                    employeeDTO.Files = new List<EmployeeAttachmentDTO>();

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


                List<EmployeeAttachment> employeeAttachments = Unit_Of_Work.employeeAttachment_Repository.FindBy(s => s.EmployeeID == employeeDTO.ID);
                List<EmployeeAttachmentDTO> filesDTO = mapper.Map<List<EmployeeAttachmentDTO>>(employeeAttachments);
                if (filesDTO != null)
                    employeeDTO.Files = filesDTO;
                else
                    employeeDTO.Files = new List<EmployeeAttachmentDTO>();

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

            //Validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (NewEmployee.Email != null && !Regex.IsMatch(NewEmployee.Email, pattern))
            {
                return BadRequest("Email Is Not Valid");
            }
            string MobilePattern = @"^0(10|11|12|15)\d{8}$";
            if (NewEmployee.Mobile != null && !Regex.IsMatch(NewEmployee.Mobile, MobilePattern))
            {
                return BadRequest("Mobile Is Not Valid");
            }
            if (NewEmployee.Phone != null && !Regex.IsMatch(NewEmployee.Phone, MobilePattern))
            {
                return BadRequest("Phone Is Not Valid");
            }
            if (NewEmployee.EmployeeTypeID == 2)
            {
                if (NewEmployee.LicenseNumber == null)
                    return BadRequest("LicenseNumber Is Required");
                if (NewEmployee.ExpireDate == null)
                    return BadRequest("ExpireDate Is Required");
            }

            ///create the object 
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

            ////create attachment folder
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Attachments");
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
                        Link = $"{Request.Scheme}://{Request.Host}/Uploads/Attachments/{employee.User_Name}/{file.FileName}",
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
                return BadRequest("Employee cannot be null");
            }


            Employee oldEmp = await Unit_Of_Work.employee_Repository.Select_By_IdAsync(newEmployee.ID);
            if (oldEmp == null)
            {
                return NotFound("Employee not found.");
            }
            Role role = Unit_Of_Work.role_Repository.First_Or_Default(r=>r.ID==newEmployee.Role_ID&&r.IsDeleted!=true);
            if (role == null) 
            {
                return NotFound("there is no role with this id");
            }
            if (newEmployee.BusCompanyID != null)
            {
                 BusCompany busCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(r => r.ID == newEmployee.BusCompanyID && r.IsDeleted != true);
                if (busCompany == null)
                {
                    return NotFound("there is no busCompany with this id");
                }
            }
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Attachments");
            if (oldEmp.User_Name != newEmployee.User_Name)
            {
                var oldEmployeeFolder = Path.Combine(baseFolder, oldEmp.User_Name);
                var newEmployeeFolder = Path.Combine(baseFolder, newEmployee.User_Name);

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

            //Validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (newEmployee.Email != null && !Regex.IsMatch(newEmployee.Email, pattern))
            {
                return BadRequest("Email Is Not Valid");
            }
            string MobilePattern = @"^0(10|11|12|15)\d{8}$";
            if (newEmployee.Mobile != null && !Regex.IsMatch(newEmployee.Mobile, MobilePattern))
            {
                return BadRequest("Mobile Is Not Valid");
            }
            if (newEmployee.Phone != null && !Regex.IsMatch(newEmployee.Phone, MobilePattern))
            {
                return BadRequest("Phone Is Not Valid");
            }
            if (newEmployee.EmployeeTypeID == 2)
            {
                if (newEmployee.LicenseNumber == null)
                    return BadRequest("LicenseNumber Is Required");
                if (newEmployee.ExpireDate == null)
                    return BadRequest("ExpireDate Is Required");
            }


            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            mapper.Map(newEmployee, oldEmp);
            if (userTypeClaim == "octa")
            {
                oldEmp.UpdatedByOctaId = userId;
                oldEmp.UpdatedByUserId = null;
            }
            else if (userTypeClaim == "employee")
            {
                oldEmp.UpdatedByUserId = userId;
                oldEmp.UpdatedByOctaId = null;
            }
            oldEmp.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.employee_Repository.Update(oldEmp);
            Unit_Of_Work.SaveChanges();


            var employeeFolder = Path.Combine(baseFolder, oldEmp.User_Name);
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

                        EmployeeAttachment uploadedFile = new EmployeeAttachment
                        {
                            EmployeeID = oldEmp.ID,
                            Link = $"{Request.Scheme}://{Request.Host}/Uploads/Attachments/{oldEmp.User_Name}/{file.FileName}",
                            Name = file.FileName,
                        };
                        Unit_Of_Work.employeeAttachment_Repository.Add(uploadedFile);
                        Unit_Of_Work.SaveChanges();
                    }
                }
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
    public IActionResult DeleteFiles(long id)
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

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            EmployeeAttachment employeeAttachment = Unit_Of_Work.employeeAttachment_Repository.First_Or_Default(s => s.ID == id);
            employeeAttachment.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                employeeAttachment.DeletedByOctaId = userId;
                if (employeeAttachment.DeletedByUserId != null)
                {
                    employeeAttachment.DeletedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                employeeAttachment.DeletedByUserId = userId;
                if (employeeAttachment.DeletedByOctaId != null)
                {
                    employeeAttachment.DeletedByOctaId = null;
                }
            }
            employeeAttachment.IsDeleted = true;
            Unit_Of_Work.employeeAttachment_Repository.Update(employeeAttachment);
            Unit_Of_Work.SaveChanges();

            Uri uri = new Uri(employeeAttachment.Link);
            string path = uri.LocalPath; 
            string fileName = Path.GetFileName(path); 
            string directory = Path.GetDirectoryName(path); 
            string folderName = Path.GetFileName(directory);
            if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName))
            {
                return BadRequest(new { message = "Invalid file details provided." });
            }
            if (folderName.Contains("..") || fileName.Contains(".."))
            {
                return BadRequest(new { message = "Invalid file path." });
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Attachments", folderName, fileName);
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


