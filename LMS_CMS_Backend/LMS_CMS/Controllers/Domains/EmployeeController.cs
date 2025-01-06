using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.BusModule;
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

        public EmployeeController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Administrator", "Employee" }
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
            pages: new[] { "Administrator", "Employee" }
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
            allowedTypes: new[] { "octa", "employee" },
            pages: new[] { "Administrator", "Employee" }
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

            Employee employee = Unit_Of_Work.employee_Repository.First_Or_Default(e => e.User_Name == NewEmployee.User_Name);
            if (employee != null)
            {
                return BadRequest("This User Name Already Exist");
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
            pages: new[] { "Administrator", "Employee" }
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

            string mobilePattern = @"^0(10|11|12|15)\d{8}$";
            if (newEmployee.Mobile != null && !Regex.IsMatch(newEmployee.Mobile, mobilePattern))
            {
                return BadRequest("Mobile is not valid.");
            }

            if (newEmployee.Phone != null && !Regex.IsMatch(newEmployee.Phone, mobilePattern))
            {
                return BadRequest("Phone is not valid.");
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

            // Update employee
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Employee");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
                    {
                        if (oldEmp.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Employee page doesn't exist");
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
            oldEmp.Password = BCrypt.Net.BCrypt.HashPassword(newEmployee.Password);

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
            pages: new[] { "Administrator", "Employee" }
        )]
        public IActionResult delete(long id)
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
                return NotFound("No semester with this ID");
            }
            if (userTypeClaim == "employee")
            {
                Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == "Employee");
                if (page != null)
                {
                    Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);
                    if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
                    {
                        if (employee.InsertedByUserId != userId)
                        {
                            return Unauthorized();
                        }
                    }
                }
                else
                {
                    return BadRequest("Employee page doesn't exist");
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

    }
}


