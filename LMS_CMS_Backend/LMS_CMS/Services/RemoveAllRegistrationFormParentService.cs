using LMS_CMS_BL.DTO.LMS;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using Microsoft.AspNetCore.Mvc;

namespace LMS_CMS_PL.Services
{
    public class RemoveAllRegistrationFormParentService
    {
        public void RemoveAllRegistrationFormParent(UOW Unit_Of_Work, long registrationFormParentID, string userTypeClaim, long userId)
        {
            TimeZoneInfo cairoZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            
            List<RegisterationFormSubmittion> forms = Unit_Of_Work.registerationFormSubmittion_Repository.FindBy(s => s.RegisterationFormParentID == registrationFormParentID);
            foreach (var item in forms)
            {
                item.IsDeleted = true;
                item.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    item.UpdatedByOctaId = userId;
                    if (item.UpdatedByUserId != null)
                    {
                        item.UpdatedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    item.UpdatedByUserId = userId;
                    if (item.UpdatedByOctaId != null)
                    {
                        item.UpdatedByOctaId = null;
                    }
                }
                Unit_Of_Work.registerationFormSubmittion_Repository.Update(item);
                Unit_Of_Work.SaveChanges();
            }


            List<RegisterationFormTest> tests = Unit_Of_Work.registerationFormTest_Repository.FindBy(s => s.RegisterationFormParentID == registrationFormParentID);
            foreach (var item in tests)
            {
                item.IsDeleted = true;
                item.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    item.UpdatedByOctaId = userId;
                    if (item.UpdatedByUserId != null)
                    {
                        item.UpdatedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    item.UpdatedByUserId = userId;
                    if (item.UpdatedByOctaId != null)
                    {
                        item.UpdatedByOctaId = null;
                    }
                }
                Unit_Of_Work.registerationFormTest_Repository.Update(item);
                Unit_Of_Work.SaveChanges();
            }


            List<RegisterationFormTestAnswer> answers = Unit_Of_Work.registerationFormTestAnswer_Repository.FindBy(s => s.RegisterationFormParentID == registrationFormParentID);
            foreach (var item in answers)
            {
                item.IsDeleted = true;
                item.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    item.UpdatedByOctaId = userId;
                    if (item.UpdatedByUserId != null)
                    {
                        item.UpdatedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    item.UpdatedByUserId = userId;
                    if (item.UpdatedByOctaId != null)
                    {
                        item.UpdatedByOctaId = null;
                    }
                }
                Unit_Of_Work.registerationFormTestAnswer_Repository.Update(item);
                Unit_Of_Work.SaveChanges();
            }


            List<RegisterationFormInterview> interviews = Unit_Of_Work.registerationFormInterview_Repository.FindBy(s => s.RegisterationFormParentID == registrationFormParentID);
            foreach (var item in interviews)
            {
                item.IsDeleted = true;
                item.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
                if (userTypeClaim == "octa")
                {
                    item.UpdatedByOctaId = userId;
                    if (item.UpdatedByUserId != null)
                    {
                        item.UpdatedByUserId = null;
                    }
                }
                else if (userTypeClaim == "employee")
                {
                    item.UpdatedByUserId = userId;
                    if (item.UpdatedByOctaId != null)
                    {
                        item.UpdatedByOctaId = null;
                    }
                }
                Unit_Of_Work.registerationFormInterview_Repository.Update(item);
                Unit_Of_Work.SaveChanges();
            }

            RegisterationFormParent r = Unit_Of_Work.registerationFormParent_Repository.First_Or_Default(s => s.ID == registrationFormParentID);
            r.IsDeleted = true;
            r.UpdatedAt = TimeZoneInfo.ConvertTime(DateTime.Now, cairoZone);
            if (userTypeClaim == "octa")
            {
                r.UpdatedByOctaId = userId;
                if (r.UpdatedByUserId != null)
                {
                    r.UpdatedByUserId = null;
                }
            }
            else if (userTypeClaim == "employee")
            {
                r.UpdatedByUserId = userId;
                if (r.UpdatedByOctaId != null)
                {
                    r.UpdatedByOctaId = null;
                }
            }
            Unit_Of_Work.registerationFormParent_Repository.Update(r);
            Unit_Of_Work.SaveChanges();
        }
    }
}
