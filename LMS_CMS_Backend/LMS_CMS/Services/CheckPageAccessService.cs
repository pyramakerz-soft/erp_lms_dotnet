using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Services
{
    public class CheckPageAccessService
    {
        public IActionResult? CheckIfEditPageAvailable(UOW Unit_Of_Work, string pageName, long roleId, long userId, dynamic objToCheck)
        {
            Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == pageName);

            if (page == null)
            {
                return new BadRequestObjectResult($"{pageName} page doesn't exist");
            }

            Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);

            if (roleDetails != null && roleDetails.Allow_Edit_For_Others == false)
            {
                if (objToCheck.InsertedByUserId != userId)
                {
                    return new UnauthorizedResult();
                }
            }
            return null;
        }
        
        public IActionResult? CheckIfDeletePageAvailable(UOW Unit_Of_Work, string pageName, long roleId, long userId, dynamic objToCheck)
        {
            Page page = Unit_Of_Work.page_Repository.First_Or_Default(page => page.en_name == pageName);

            if (page == null)
            {
                return new BadRequestObjectResult($"{pageName} page doesn't exist");
            }

            Role_Detailes roleDetails = Unit_Of_Work.role_Detailes_Repository.First_Or_Default(RD => RD.Page_ID == page.ID && RD.Role_ID == roleId);

            if (roleDetails != null && roleDetails.Allow_Delete_For_Others == false)
            {
                if (objToCheck.InsertedByUserId != userId)
                {
                    return new UnauthorizedResult();
                }
            }
            return null;
        }
    }
}
