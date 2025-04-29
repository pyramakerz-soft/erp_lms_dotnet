using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.Accounting;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Domains.Administration;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Attribute;
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
        private readonly UOW _Unit_Of_Work_Octa;
        private readonly CheckPageAccessService _checkPageAccessService;

        public EmployeeController(DbContextFactoryService dbContextFactory, IMapper mapper, UOW Unit_Of_Work, CheckPageAccessService checkPageAccessService)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
            _Unit_Of_Work_Octa = Unit_Of_Work;
            _checkPageAccessService = checkPageAccessService;
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Employee" }
        )]
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
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Employee" }
        )]
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
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" }
        )]
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
            //string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Attachments", employee.User_Name.Trim());
            //if (Directory.Exists(folderPath))
            //{
            //    var fileInfos = new DirectoryInfo(folderPath).GetFiles();

            //    employeeDTO.Files = fileInfos.Select(fileInfo => new EmployeeAttachmentDTO
            //    {
            //        Name = fileInfo.Name,
            //        Type = GetMimeType(fileInfo.FullName),
            //        Size = fileInfo.Length,
            //        LastModified = new DateTimeOffset(fileInfo.LastWriteTimeUtc).ToUnixTimeMilliseconds(),
            //        Link = $"{Request.Scheme}://{Request.Host}/Uploads/Attachments/{employee.User_Name.Trim()}/{fileInfo.Name}"
            //    }).ToList();
            //}
            List<EmployeeAttachment> employeeAttachments = Unit_Of_Work.employeeAttachment_Repository.FindBy(s => s.EmployeeID == employeeDTO.ID &&s.IsDeleted!=true);
            List<EmployeeAttachmentDTO> filesDTO = mapper.Map<List<EmployeeAttachmentDTO>>(employeeAttachments);
            if (filesDTO != null)
                employeeDTO.Files = filesDTO;
            else
                employeeDTO.Files = new List<EmployeeAttachmentDTO>();

            foreach (var file in filesDTO)
            {
                //file.Size = Math.Round(file.Size / (1024.0 * 1024.0), 2);

                string filePath = Path.Combine("Uploads", "Attachments", employee.User_Name.Trim(), file.Name);

                if (System.IO.File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath); 
                    file.Size = Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2);
                }
            }

            return Ok(employeeDTO); 
        }
        private string GetMimeType(string filePath)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var mimeType))
            {
                mimeType = "application/octet-stream"; // Default MIME type
            }
            return mimeType;
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Employee Create" }
        )]
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
            if (NewEmployee.EmployeeTypeID == 2)
            {
                if (NewEmployee.LicenseNumber == null)
                    return BadRequest("LicenseNumber Is Required");
                if (NewEmployee.ExpireDate == null)
                    return BadRequest("ExpireDate Is Required");
            }

            Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(e => e.User_Name == NewEmployee.User_Name);
            if (employee != null)
            {
                return BadRequest("This User Name Already Exist");
            }
            Employee CheckEmail = Unit_Of_Work.employee_Repository.First_Or_Default(e => e.Email == NewEmployee.Email);
            if (CheckEmail != null)
            {
                return BadRequest("This Email Already Exist");
            }
            if (NewEmployee.BusCompanyID != null && NewEmployee.BusCompanyID != 0)
            {
                BusCompany bus = Unit_Of_Work.busCompany_Repository.First_Or_Default(b => b.ID == NewEmployee.BusCompanyID && b.IsDeleted != true);
                if (bus == null)
                {
                    return BadRequest("this bus company doesn't exist");
                }
            }
            else
            {
                NewEmployee.BusCompanyID = null;
            }
            if (NewEmployee.EmployeeTypeID != 0 && NewEmployee.EmployeeTypeID != null)
            {
                EmployeeType empType = Unit_Of_Work.employeeType_Repository.First_Or_Default(b => b.ID == NewEmployee.EmployeeTypeID);
                if (empType == null)
                {
                    return BadRequest("this Employee Type doesn't exist");
                }
            }
            else
            {
                return BadRequest("this Employee Type cannot be null");

            }
            if (NewEmployee.Role_ID != 0 && NewEmployee.Role_ID != null)
            {
                Role rolee = Unit_Of_Work.role_Repository.First_Or_Default(b => b.ID == NewEmployee.Role_ID && b.IsDeleted != true);
                if (rolee == null)
                {
                    return BadRequest("this role doesn't exist");
                }
            }
            else
            {
                return BadRequest("this role cannot be null");

            }
            ///create the object 
            if (employee == null)
            {
                employee = new Employee();
            }
            mapper.Map(NewEmployee, employee);
            employee.Password= BCrypt.Net.BCrypt.HashPassword(NewEmployee.Password);

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
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Employee Edit" }
        )]
        public async Task<IActionResult> EditAsync([FromForm] EmployeePutDTO newEmployee, [FromForm] List<IFormFile> files)
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

            if (newEmployee == null)
            {
                return BadRequest("Employee cannot be null");
            }
            Employee CheckEmail = Unit_Of_Work.employee_Repository.First_Or_Default(e => e.Email == newEmployee.Email && e.ID!= newEmployee.ID);
            if (CheckEmail != null)
            {
                return BadRequest("This Email Already Exist");
            }
            Employee oldEmp = await Unit_Of_Work.employee_Repository.Select_By_IdAsync(newEmployee.ID);
            if (oldEmp == null)
            {
                return NotFound("Employee not found.");
            }

            if (newEmployee.BusCompanyID != null && newEmployee.BusCompanyID != 0)
            {
                BusCompany busCompany = Unit_Of_Work.busCompany_Repository.First_Or_Default(r => r.ID == newEmployee.BusCompanyID && r.IsDeleted != true);
                if (busCompany == null)
                {
                    return NotFound("There is no bus company with this ID.");
                }
            }
            else
            {
                newEmployee.BusCompanyID = null;
            }

            if (newEmployee.EmployeeTypeID != 0 && newEmployee.EmployeeTypeID != null)
            {
                EmployeeType empType = Unit_Of_Work.employeeType_Repository.First_Or_Default(b => b.ID == newEmployee.EmployeeTypeID);
                if (empType == null)
                {
                    return BadRequest("this Employee Type doesn't exist");
                }
            }
            else
            {
                return BadRequest("this Employee Type cannot be null");

            }

            if (newEmployee.Role_ID != 0 && newEmployee.Role_ID != null)
            {
                Role rolee = Unit_Of_Work.role_Repository.First_Or_Default(b => b.ID == newEmployee.Role_ID && b.IsDeleted != true);
                if (rolee == null)
                {
                    return BadRequest("this role doesn't exist");
                }
            }
            else
            {
                return BadRequest("this role cannot be null");

            }

            // Validation
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (newEmployee.Email != null && !Regex.IsMatch(newEmployee.Email, emailPattern))
            {
                return BadRequest("Email is not valid.");
            } 

            if (newEmployee.EmployeeTypeID == 2)
            {
                if (newEmployee.LicenseNumber == null)
                {
                    return BadRequest("LicenseNumber is required.");
                }

                if (newEmployee.ExpireDate == null)
                {
                    return BadRequest("ExpireDate is required.");
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
            }


            // Folder and File Management
            //var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/Attachments");

            // Handle old folder if User_Name changed
            //if (oldEmp.User_Name != newEmployee.User_Name)
            //{
            //var oldEmployeeFolder = Path.Combine(baseFolder, oldEmp.User_Name.Trim());
            //if (Directory.Exists(oldEmployeeFolder))
            //{
            //    var filesInFolder = Directory.GetFiles(oldEmployeeFolder);
            //    foreach (var file in filesInFolder)
            //    {
            //        System.IO.File.Delete(file);
            //    }
            //    Directory.Delete(oldEmployeeFolder);
            //}
            //}

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Employee", roleId, userId, oldEmp);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            if(oldEmp.User_Name!=newEmployee.User_Name)
            {
                Employee emp2 =Unit_Of_Work.employee_Repository.First_Or_Default(e=>e.User_Name== newEmployee.User_Name);
                if(emp2 != null)
                {
                    return BadRequest("this user name already exist");
                }
            }
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
            await Unit_Of_Work.SaveChangesAsync();

            //// Delete existing employee attachments
            //var existingAttachments = Unit_Of_Work.employeeAttachment_Repository.FindBy(s => s.EmployeeID == oldEmp.ID);
            //foreach (var item in existingAttachments)
            //{
            //    Unit_Of_Work.employeeAttachment_Repository.Delete(item.ID);
            //    await Unit_Of_Work.SaveChangesAsync();
            //}

            // Create new folder for employee
            var sanitizedUserName = newEmployee.User_Name.Trim();
            var employeeFolder = Path.Combine(baseFolder, sanitizedUserName);

            if (!Directory.Exists(employeeFolder))
            {
                Directory.CreateDirectory(employeeFolder);
            }

            // Handle new files
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

                        var uploadedFile = new EmployeeAttachment
                        {
                            EmployeeID = oldEmp.ID,
                            Link = $"{Request.Scheme}://{Request.Host}/Uploads/Attachments/{sanitizedUserName}/{file.FileName}",
                            Name = file.FileName,
                        };

                        Unit_Of_Work.employeeAttachment_Repository.Add(uploadedFile);
                        await Unit_Of_Work.SaveChangesAsync();
                    }
                }
            }

            return Ok(newEmployee);
        }



        //////////////////////////////////////////////////////

        [HttpDelete("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowDelete: 1,
            pages: new[] { "Employee" }
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

            if (id == null)
            {
                return BadRequest("id cannot be null");
            }
            Employee employee = Unit_Of_Work.employee_Repository.Select_By_Id(id);

            if (employee == null || employee.IsDeleted == true)
            {
                return NotFound("No employee with this ID");
            }
            
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfDeletePageAvailable(Unit_Of_Work, "Employee", roleId, userId, employee);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
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


      [HttpDelete("DeleteFiles/{id}")]
    public IActionResult DeleteFiles(long id)
    {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            //var userClaims = HttpContext.User.Claims;
            //var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            //long.TryParse(userIdClaim, out long userId);
            //var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            //if (userIdClaim == null || userTypeClaim == null)
            //{
            //    return Unauthorized("User ID or Type claim not found.");
            //}

            //TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            EmployeeAttachment employeeAttachment = Unit_Of_Work.employeeAttachment_Repository.First_Or_Default(s => s.ID == id);
            //employeeAttachment.DeletedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            //if (userTypeClaim == "octa")
            //{
            //    employeeAttachment.DeletedByOctaId = userId;
            //    if (employeeAttachment.DeletedByUserId != null)
            //    {
            //        employeeAttachment.DeletedByUserId = null;
            //    }
            //}
            //else if (userTypeClaim == "employee")
            //{
            //    employeeAttachment.DeletedByUserId = userId;
            //    if (employeeAttachment.DeletedByOctaId != null)
            //    {
            //        employeeAttachment.DeletedByOctaId = null;
            //    }
            //}
            //employeeAttachment.IsDeleted = true;

            Unit_Of_Work.employeeAttachment_Repository.Delete(id);
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

        ///////////////////////////////////////////////////////////////////////////

        [HttpPut("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Employee" }
        )]
        public async Task<IActionResult> EditpasswordAsync(EditPasswordDTO model)
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

            if (model.Password == "")
            {
                return BadRequest("password cannot be empty");
            }

            Employee oldEmp = await Unit_Of_Work.employee_Repository.Select_By_IdAsync(model.Id);
            if (oldEmp == null || oldEmp.IsDeleted == true)
            {
                return NotFound("Employee not found.");
            }
             
            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Employee", roleId, userId, oldEmp);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
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
            oldEmp.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            oldEmp.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            Unit_Of_Work.employee_Repository.Update(oldEmp);
            await Unit_Of_Work.SaveChangesAsync();
            return Ok();
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("getByAccountingEmployee/{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Employee" }
        )]
        public async Task<IActionResult> GetByAccounting(long id)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);
          
            Employee employee = await Unit_Of_Work.employee_Repository.FindByIncludesAsync(
                    sem => sem.IsDeleted != true && sem.ID == id,
                    query => query.Include(emp => emp.ReasonForLeavingWork),
                    query => query.Include(emp => emp.AccountNumber),
                    query => query.Include(emp => emp.Job),
                    query => query.Include(emp => emp.Department),
                    query => query.Include(emp => emp.AccountNumber),
                    query => query.Include(emp => emp.AcademicDegree));

            if (employee == null )
            {
                return NotFound("There is no employees with this id");
            }

            EmployeeAccountingGetDTO employeeDTO = mapper.Map<EmployeeAccountingGetDTO>(employee);
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(employeeDTO.Nationality);
            if (nationality != null)
            {
                employeeDTO.NationalityName = nationality.Name;
            }

            List<EmployeeDays > days = await Unit_Of_Work.employeeDays_Repository.Select_All_With_IncludesById<EmployeeDays>(
                sem => sem.IsDeleted != true && sem.EmployeeID == id
                );

            if(days != null &&days.Count>0)
            {
              employeeDTO.Days = days.Select(day => day.DayID).ToList();

            }
            else
            {
                employeeDTO.Days =new List<long> { };
            }

            List<EmployeeStudent> students = await Unit_Of_Work.employeeStudent_Repository.Select_All_With_IncludesById<EmployeeStudent>(
               sem => sem.IsDeleted != true && sem.EmployeeID == id
               );

            if (students != null && students.Count > 0)
            {
                employeeDTO.Students = students.Select(day => day.StudentID).ToList();

            }
            else
            {
                employeeDTO.Students = new List<long> { };
            }

            return Ok(employeeDTO);
        }
        //////////////////////////////////////////////////////////////////////////////

        [HttpPut("EmpployeeAccounting")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            allowEdit: 1,
            pages: new[] { "Employee Accounting" }
        )]
        public async Task<IActionResult> EditEmployeeAccountingAsync(EmployeeAccountingPut newEmployee)
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
                return Unauthorized("User ID, Type claim not found.");
            }

            Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(s => s.ID == newEmployee.ID && s.IsDeleted != true);
            if (employee == null || employee.IsDeleted == true)
            {
                return NotFound("No Employee with this ID");
            }

            if (newEmployee.AccountNumberID != 0 && newEmployee.AccountNumberID != null)
            {
                AccountingTreeChart account = Unit_Of_Work.accountingTreeChart_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newEmployee.AccountNumberID);

                if (account == null)
                {
                    return NotFound("No Account chart with this Id");
                }
                else
                {
                    if (account.SubTypeID == 1)
                    {
                        return BadRequest("You can't use main account, only sub account");
                    }

                    if (account.LinkFileID != 10)
                    {
                        return BadRequest("Wrong Link File, it should be Asset file link");
                    }
                }
            }
            else
            {
                newEmployee.AccountNumberID = null;
            }

            if (newEmployee.AcademicDegreeID != 0 && newEmployee.AcademicDegreeID != null)
            {
                AcademicDegree academicDegree = Unit_Of_Work.academicDegree_Repository.First_Or_Default(t => t.ID == newEmployee.AcademicDegreeID);

                if (academicDegree == null)
                {
                    return NotFound("No academicDegree with this Id");
                }
            }
            else
            {
                newEmployee.AcademicDegreeID = null;
            }

            if (newEmployee.JobID != 0 && newEmployee.JobID != null)
            {

                 Job job = Unit_Of_Work.job_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newEmployee.JobID);

                if (job == null)
                {
                    return NotFound("No Job  with this Id");
                }
            }
            else
            {
                newEmployee.JobID = null;
            }

            if (newEmployee.DepartmentID != 0 && newEmployee.DepartmentID != null)
            {
                Department department = Unit_Of_Work.department_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newEmployee.DepartmentID);

                if (department == null)
                {
                    return NotFound("No department with this Id");
                }
            }
            else
            {
                newEmployee.DepartmentID = null;
            }

            if (newEmployee.ReasonOfLeavingID != 0 && newEmployee.ReasonOfLeavingID != null)
            {
                 ReasonForLeavingWork reasonForLeavingWork = Unit_Of_Work.reasonForLeavingWork_Repository.First_Or_Default(t => t.IsDeleted != true && t.ID == newEmployee.ReasonOfLeavingID);

                if (reasonForLeavingWork == null)
                {
                    return NotFound("No reasonForLeavingWork with this Id");
                }

            }
            else
            {
                newEmployee.ReasonOfLeavingID = null;
            }

            if(newEmployee.Nationality!=0&& newEmployee.Nationality != null)
            {
            Nationality nationality = _Unit_Of_Work_Octa.nationality_Repository.Select_By_Id_Octa(newEmployee.Nationality);
            if (nationality == null)
            {
                return BadRequest("There is no nationality with this id");
            }

            }

            if (userTypeClaim == "employee")
            {
                IActionResult? accessCheck = _checkPageAccessService.CheckIfEditPageAvailable(Unit_Of_Work, "Employee Accounting", roleId, userId, employee);
                if (accessCheck != null)
                {
                    return accessCheck;
                }
            }

            mapper.Map(newEmployee, employee);
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

            Unit_Of_Work.employee_Repository.Update(employee);
            Unit_Of_Work.SaveChanges();


            //////delete all empDays
            List<EmployeeDays> employeeDays = await Unit_Of_Work.employeeDays_Repository.Select_All_With_IncludesById<EmployeeDays>(
                    sem => sem.EmployeeID==newEmployee.ID);

             foreach(EmployeeDays day in employeeDays)
             {
                Unit_Of_Work.employeeDays_Repository.Delete(day.ID); 
                Unit_Of_Work.SaveChanges();
             }

            if (newEmployee.Days != null &&newEmployee.Days.Count != 0)
            {
                foreach (var day in newEmployee.Days)
                {
                    if (day != 0)
                    {
                    EmployeeDays empDay = new EmployeeDays();
                    empDay.EmployeeID = newEmployee.ID;
                    empDay.DayID = day;
                    Unit_Of_Work.employeeDays_Repository.Add(empDay);
                    Unit_Of_Work.SaveChanges();
                    }
                }

            }
            //////delete all empStudents
          List<EmployeeStudent> employeeStudents = await Unit_Of_Work.employeeStudent_Repository.Select_All_With_IncludesById<EmployeeStudent>(
                  sem => sem.EmployeeID == newEmployee.ID);
          
          foreach (EmployeeStudent emp in employeeStudents)
          {
              Unit_Of_Work.employeeStudent_Repository.Delete(emp.ID);
              Unit_Of_Work.SaveChanges();
          }
          
          foreach (var empStudent in newEmployee.Students)
          {
                Student student = Unit_Of_Work.student_Repository.First_Or_Default(s => s.ID == empStudent && s.IsDeleted != true);
                if (student != null)
                {
                  EmployeeStudent emp = new EmployeeStudent();
                  emp.EmployeeID = newEmployee.ID;
                  emp.StudentID = empStudent;
                  Unit_Of_Work.employeeStudent_Repository.Add(emp);
                  Unit_Of_Work.SaveChanges();

                }
          }
          
          return Ok(newEmployee);
        }
    }   
}


