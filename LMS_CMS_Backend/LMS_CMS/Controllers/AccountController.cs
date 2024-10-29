using AutoMapper;
using LMS_CMS_BL.UOW;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        UOW Unit_Of_Work;

        public AccountController(UOW Unit_Of_Work)
        {
            this.Unit_Of_Work = Unit_Of_Work;
           
        }

    }
}
