using AutoMapper;
using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.BusModule;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Octa;
using LMS_CMS_PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace LMS_CMS_PL.Controllers.Octa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolTypesController : ControllerBase
    {
        //private readonly UOW _Unit_Of_Work;
        //private readonly DynamicDatabaseService _dynamicDatabaseService;
        //IMapper _mapper;

        //public SchoolTypesController(DynamicDatabaseService dynamicDatabaseService, UOW Unit_Of_Work , IMapper mapper)
        //{
        //    _Unit_Of_Work = Unit_Of_Work;
        //    _dynamicDatabaseService = dynamicDatabaseService;
        //    _mapper = mapper;   
        //}

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var userClaims = HttpContext.User.Claims;
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
        //    if (userTypeClaim == null)
        //    {
        //        return Unauthorized("User Type claim not found.");
        //    }

        //    if (userTypeClaim != "octa")
        //    {
        //        return Unauthorized("Access Denied");
        //    }

        //    List<SchoolType> SchoolTypes = _Unit_Of_Work.schoolType_Repository.Select_All_Octa();
        //    List<SchoolTypeGetDTO> schoolTypeGetDTO = _mapper.Map<List<SchoolTypeGetDTO>>(SchoolTypes);
        //    return Ok(SchoolTypes);
        //}

        //[HttpPost]
        //public IActionResult add(SchoolTypeAddDTO NewSchoolType)
        //{
        //    var userClaims = HttpContext.User.Claims;
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
        //    if (userTypeClaim == null)
        //    {
        //        return Unauthorized("User Type claim not found.");
        //    }

        //    if (userTypeClaim != "octa")
        //    {
        //        return Unauthorized("Access Denied");
        //    }

        //    SchoolType schoolType = _mapper.Map<SchoolType>(NewSchoolType);
        //    _Unit_Of_Work.schoolType_Repository.Add(schoolType);
        //    _Unit_Of_Work.SaveChanges();

        //    return Ok(NewSchoolType);
        //}

        //[HttpGet]
        //public IActionResult getByID(long id) 
        //{
        //    var userClaims = HttpContext.User.Claims;
        //    var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
        //    if (userTypeClaim == null)
        //    {
        //        return Unauthorized("User Type claim not found.");
        //    }

        //    if (userTypeClaim != "octa")
        //    {
        //        return Unauthorized("Access Denied");
        //    }
        //    SchoolType schoolType =_Unit_Of_Work.schoolType_Repository.(id);
        //    SchoolTypeGetDTO schoolTypeGetDTO =_mapper.Map<SchoolTypeGetDTO>(schoolType);
        //    _Unit_Of_Work.SaveChanges();
        //    return Ok(schoolTypeGetDTO);
        //}

        //[HttpDelete]
        //public IActionResult DeleteByID(long id)
        //{
        //    //var userClaims = HttpContext.User.Claims;
        //    //var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
        //    //if (userTypeClaim == null)
        //    //{
        //    //    return Unauthorized("User Type claim not found.");
        //    //}

        //    //if (userTypeClaim != "octa")
        //    //{
        //    //    return Unauthorized("Access Denied");
        //    //}
        //    //SchoolType schoolType = _Unit_Of_Work.schoolType_Repository.Delete_Octa(id);
        //    //_Unit_Of_Work.SaveChanges();
        //    return Ok();
        //}
    }
}
