using AutoMapper;
using LMS_CMS_BL.DTO;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.DTO.Registration;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using LMS_CMS_PL.Attribute;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Controllers.Domains.Registeration
{
    [Route("api/with-domain/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationFormController : ControllerBase
    {
        private readonly DbContextFactoryService _dbContextFactory;
        IMapper mapper;

        public RegistrationFormController(DbContextFactoryService dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet("{id}")]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Registration Form" }
        )]
        public async Task<IActionResult> GetById(long id)
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

            if (id == 0)
            {
                return BadRequest("Enter Registration Form ID");
            }

            RegistrationForm registrationForm = Unit_Of_Work.registrationForm_Repository.First_Or_Default(
                t => t.IsDeleted != true && t.ID == id);

            if (registrationForm == null)
            {
                return NotFound("No Registration Form with this ID");
            }

            List<RegistrationFormCategory> RegistrationFormCategories = await Unit_Of_Work.registrationFormCategory_Repository.Select_All_With_IncludesById
                <RegistrationFormCategory>(
                    t => t.RegistrationFormID == id && t.IsDeleted != true,
                    query => query.Include(rfc => rfc.RegistrationCategory)
                    .ThenInclude(rc => rc.CategoryFields)
                    .ThenInclude(cf => cf.FieldOptions)
                    );

            var categories = RegistrationFormCategories
                .Select(rfc => new RegistrationCategoryGetDTO
                {
                    ID = rfc.RegistrationCategory?.ID ?? 0, 
                    EnName = rfc.RegistrationCategory?.EnName ?? string.Empty,
                    ArName = rfc.RegistrationCategory?.ArName ?? string.Empty,
                    OrderInForm = rfc.RegistrationCategory?.OrderInForm ?? 0,
                    InsertedByUserId = rfc.RegistrationCategory?.InsertedByUserId,
                    Fields = Unit_Of_Work.categoryField_Repository
                        .FindBy(cf => cf.RegistrationCategoryID == rfc.RegistrationCategoryID && cf.IsDeleted != true)
                        .Select(field => new CategoryFieldGetDTO
                        {
                            ID = field.ID,
                            EnName = field.EnName ?? string.Empty, 
                            ArName = field.ArName ?? string.Empty,  
                            OrderInForm = field.OrderInForm,
                            IsMandatory = field.IsMandatory,
                            InsertedByUserId = field.InsertedByUserId,
                            FieldTypeID = field.FieldTypeID,
                            FieldTypeName = Unit_Of_Work.fieldType_Repository
                                .First_Or_Default(ft => ft.ID == field.FieldTypeID)?.Name ?? string.Empty,  
                            RegistrationCategoryID = field.RegistrationCategoryID,
                            RegistrationCategoryName = rfc.RegistrationCategory?.EnName ?? string.Empty, 
                            Options = Unit_Of_Work.fieldOption_Repository
                                .FindBy(fo => fo.CategoryFieldID == field.ID && fo.IsDeleted != true)
                                .Select(option => new FieldOptionGetDTO
                                {
                                    ID = option.ID,
                                    Name = option.Name ?? string.Empty, 
                                    CategoryFieldID = option.CategoryFieldID,
                                    CategoryFieldName = field.EnName ?? string.Empty  
                                })
                                .ToList() ?? new List<FieldOptionGetDTO>() // Default to empty list
                        })
                        .ToList() ?? new List<CategoryFieldGetDTO>() 
                })
                .ToList() ?? new List<RegistrationCategoryGetDTO>();

            var response = new RegistrationFormGetDTO
            {
                ID = registrationForm.ID,
                Name = registrationForm.Name,
                Categories = categories
            };

            return Ok(response);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Authorize_Endpoint_(
            allowedTypes: new[] { "octa", "employee", "parent" },
            pages: new[] { "Registration Form" }
        )]
        public async Task<IActionResult> Add([FromForm] RegisterationFormParentAddDTO registerationFormParentAddDTO,
            [FromForm] List<RegisterationFormSubmittionForFiles> filesFieldCat = null)
        {
            /*
                1) get the Email (21) and search in the parent table for the id if exists, get it if not skip
                2) add RegisterationFormParent:
                    get from the returned data StudentName(1), Phone(20), GradeID(9), Email (21)
                    make StateID = pending (1)
                    if parent has the id from step 1
                    put the RegisterFormID
                3) make RegisterationFormSubmittion
                    RegistrationFormParentID from step 2
                    loop over the array for other data

                {
                    RegistrationFormID : 1,
                    RegisterationFormSubmittion:
                        [
                            {
                                CategoryFieldID
                                SelectedFieldOptionID  OORR TextAnswer
                            }
                        ]
                }
             */

            //List<RegisterationFormSubmittionForFiles> filesFieldCat = new List<RegisterationFormSubmittionForFiles>();


            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");

            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(HttpContext);

            var userClaims = HttpContext.User.Claims;
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            long.TryParse(userIdClaim, out long userId);
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;

            if (userIdClaim == null || userTypeClaim == null)
            {
                return Unauthorized("User ID or Type claim not found.");
            }
            if (registerationFormParentAddDTO == null)
            {
                return NotFound();
            }

            RegistrationForm registrationForm = Unit_Of_Work.registrationForm_Repository.First_Or_Default(s => s.ID == registerationFormParentAddDTO.RegistrationFormID && s.IsDeleted != true);
            if (registrationForm == null)
            {
                return NotFound("There is no Registration Form with this ID");
            }

            string ParentEmail = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 21)?.TextAnswer ?? string.Empty;

            string MotherEmail = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 28)?.TextAnswer ?? string.Empty;

            string StudentName = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 1)?.TextAnswer ?? string.Empty;

            string Phone = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 20)?.TextAnswer ?? string.Empty;

            string GradeID = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 9)?.TextAnswer ?? string.Empty;

            string SchoolID = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 7)?.TextAnswer ?? string.Empty;

            string AcademicYearID = registerationFormParentAddDTO.RegisterationFormSubmittions
                .FirstOrDefault(s => s.CategoryFieldID == 8)?.TextAnswer ?? string.Empty;

            // Validate 
            if (string.IsNullOrWhiteSpace(GradeID) || !long.TryParse(GradeID, out long gradeId))
            {
                return BadRequest("Invalid or missing GradeID. Please provide a valid numeric value.");
            }

            if (string.IsNullOrWhiteSpace(SchoolID) || !long.TryParse(SchoolID, out long schoolId))
            {
                return BadRequest("Invalid or missing SchoolID. Please provide a valid numeric value.");
            }

            if (string.IsNullOrWhiteSpace(AcademicYearID) || !long.TryParse(AcademicYearID, out long academicYearId))
            {
                return BadRequest("Invalid or missing AcademicYearID. Please provide a valid numeric value.");
            }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(ParentEmail);
                if (mailAddress.Address != ParentEmail)
                {
                    return BadRequest("Invalid Guardian's email Format");
                }
            }
            catch
            {
                return BadRequest("Invalid Guardian's email Format");
            }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(MotherEmail);
                if (mailAddress.Address != MotherEmail)
                {
                    return BadRequest("Invalid Mother's email Format");
                }
            }
            catch
            {
                return BadRequest("Invalid Mother's email Format");
            }

            if (userTypeClaim == "parent")
            {
                Parent parentExists = Unit_Of_Work.parent_Repository.Select_By_Id(userId);
                if(parentExists.Email != ParentEmail)
                {
                    return BadRequest("Guardian's Email must be the same as parent Email");
                }
            }

            Grade grade = Unit_Of_Work.grade_Repository.First_Or_Default(s => s.ID == long.Parse(GradeID) && s.IsDeleted != true);
            if (grade == null)
            {
                return NotFound("There is no Grade with this ID");
            }

            School school = Unit_Of_Work.school_Repository.First_Or_Default(s => s.ID == long.Parse(SchoolID) && s.IsDeleted != true);
            if (school == null)
            {
                return NotFound("There is no School with this ID");
            }

            AcademicYear academicYear = Unit_Of_Work.academicYear_Repository.First_Or_Default(s => s.ID == long.Parse(AcademicYearID) && s.IsDeleted != true);
            if (academicYear == null)
            {
                return NotFound("There is no Academic Year with this ID");
            }

            long parentID = 0;
            Parent parent = Unit_Of_Work.parent_Repository.First_Or_Default(s => s.Email == ParentEmail && s.IsDeleted != true);
            if (parent != null)
            {
                parentID = parent.ID;
            }
            for (int i = 0; i < registerationFormParentAddDTO.RegisterationFormSubmittions.Count; i++)
            {
                CategoryField categoryField = Unit_Of_Work.categoryField_Repository.First_Or_Default(s => s.ID == registerationFormParentAddDTO.RegisterationFormSubmittions[i].CategoryFieldID && s.IsDeleted != true);
                if (categoryField == null)
                {
                    return NotFound("There is no Category Field with this ID");
                }

                if (categoryField.IsMandatory)
                {
                    bool isThereAFile = false;
                    if (filesFieldCat != null)
                    {
                        for (int j = 0; j < filesFieldCat.Count; j++)
                        {
                            if (filesFieldCat[j].SelectedFile.Length > 0)
                            {
                                if (filesFieldCat[j].CategoryFieldID == registerationFormParentAddDTO.RegisterationFormSubmittions[i].CategoryFieldID)
                                {
                                    isThereAFile = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID == null && registerationFormParentAddDTO.RegisterationFormSubmittions[i].TextAnswer == null && !isThereAFile)
                    {
                        return BadRequest($"Field {categoryField.EnName} is required");
                    }
                }

                if (registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID != null)
                {
                    FieldOption fieldOption = Unit_Of_Work.fieldOption_Repository.First_Or_Default(s => s.ID == registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID && s.IsDeleted != true);
                    if (fieldOption == null)
                    {
                        return NotFound("There is no Field Option with this ID");
                    }
                }
            }

            RegisterationFormParent registerationFormParent = new RegisterationFormParent
            {
                StudentName = StudentName,
                Phone = Phone,
                GradeID = GradeID,
                Email = ParentEmail,
                AcademicYearID = AcademicYearID,
                RegisterationFormStateID = 1, // Pending
                RegistrationFormID = registerationFormParentAddDTO.RegistrationFormID,
                ParentID = parentID != 0 ? parentID : (long?)null,
                InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
            };

            //RegisterationFormParent existsRegisterationFormParent = Unit_Of_Work.registerationFormParent_Repository.First_Or_Default(s => s.Email == ParentEmail && s.Email != null && s.IsDeleted != true);

            //if(existsRegisterationFormParent != null)
            //{
            //    return BadRequest("Email Already Exists");
            //}

            if (userTypeClaim == "octa")
            {
                registerationFormParent.InsertedByOctaId = userId;
            }
            else if (userTypeClaim == "employee")
            {
                registerationFormParent.InsertedByOctaId = userId;
            }

            Unit_Of_Work.registerationFormParent_Repository.Add(registerationFormParent);
            Unit_Of_Work.SaveChanges();

            registerationFormParent.StudentName = $"{registerationFormParent.StudentName.Replace(" ", "_")}_{registerationFormParent.ID}";
            Unit_Of_Work.registerationFormParent_Repository.Update(registerationFormParent);

            Unit_Of_Work.SaveChanges();


            long newRegisterationFormParentID = registerationFormParent.ID;

            var fileFolder = "";

            /// If File
            if (filesFieldCat != null)
            {
                for (int j = 0; j < filesFieldCat.Count; j++)
                {
                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/RegistrationForm");
                    fileFolder = Path.Combine(baseFolder,
                    registerationFormParentAddDTO.RegistrationFormID.ToString(),
                    newRegisterationFormParentID.ToString(),
                    filesFieldCat[j].CategoryFieldID.ToString());

                    if (filesFieldCat[j].SelectedFile.Length > 0)
                    {
                        if (!Directory.Exists(fileFolder))
                        {
                            Directory.CreateDirectory(fileFolder);
                        }

                        var filePath = Path.Combine(fileFolder, filesFieldCat[j].SelectedFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await filesFieldCat[j].SelectedFile.CopyToAsync(stream);
                        }

                        RegisterationFormSubmittion registerationFormSubmittion = new RegisterationFormSubmittion
                        {
                            RegisterationFormParentID = newRegisterationFormParentID,
                            CategoryFieldID = filesFieldCat[j].CategoryFieldID,
                            SelectedFieldOptionID = (long?)null,
                            TextAnswer = Path.Combine("Uploads", "RegistrationForm", registerationFormParentAddDTO.RegistrationFormID.ToString(),
                            newRegisterationFormParentID.ToString(),
                            filesFieldCat[j].CategoryFieldID.ToString()
                            , filesFieldCat[j].SelectedFile.FileName),
                            InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
                        };
                        if (userTypeClaim == "octa")
                        {
                            registerationFormSubmittion.InsertedByOctaId = userId;
                        }
                        else if (userTypeClaim == "employee")
                        {
                            registerationFormSubmittion.InsertedByOctaId = userId;
                        }
                        Unit_Of_Work.registerationFormSubmittion_Repository.Add(registerationFormSubmittion);
                    }
                }
                for (int i = 0; i < registerationFormParentAddDTO.RegisterationFormSubmittions.Count; i++)
                {
                    RegisterationFormSubmittion registerationFormSubmittion = new RegisterationFormSubmittion
                    {
                        RegisterationFormParentID = newRegisterationFormParentID,
                        CategoryFieldID = registerationFormParentAddDTO.RegisterationFormSubmittions[i].CategoryFieldID,
                        SelectedFieldOptionID = registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID != null ? registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID : (long?)null,
                        TextAnswer = registerationFormParentAddDTO.RegisterationFormSubmittions[i].TextAnswer != null ? registerationFormParentAddDTO.RegisterationFormSubmittions[i].TextAnswer : null,
                        InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
                    };
                    if (userTypeClaim == "octa")
                    {
                        registerationFormSubmittion.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        registerationFormSubmittion.InsertedByOctaId = userId;
                    }
                    Unit_Of_Work.registerationFormSubmittion_Repository.Add(registerationFormSubmittion);
                }
            }
            else
            {
                for (int i = 0; i < registerationFormParentAddDTO.RegisterationFormSubmittions.Count; i++)
                {
                    RegisterationFormSubmittion registerationFormSubmittion = new RegisterationFormSubmittion
                    {
                        RegisterationFormParentID = newRegisterationFormParentID,
                        CategoryFieldID = registerationFormParentAddDTO.RegisterationFormSubmittions[i].CategoryFieldID,
                        SelectedFieldOptionID = registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID != null ? registerationFormParentAddDTO.RegisterationFormSubmittions[i].SelectedFieldOptionID : (long?)null,
                        TextAnswer = registerationFormParentAddDTO.RegisterationFormSubmittions[i].TextAnswer != null ? registerationFormParentAddDTO.RegisterationFormSubmittions[i].TextAnswer : null,
                        InsertedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone)
                    };
                    if (userTypeClaim == "octa")
                    {
                        registerationFormSubmittion.InsertedByOctaId = userId;
                    }
                    else if (userTypeClaim == "employee")
                    {
                        registerationFormSubmittion.InsertedByOctaId = userId;
                    }
                    Unit_Of_Work.registerationFormSubmittion_Repository.Add(registerationFormSubmittion);
                }
            }

            Unit_Of_Work.SaveChanges();

            return Ok(registerationFormParentAddDTO);
        }
    }
}