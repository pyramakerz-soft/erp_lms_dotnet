using AutoMapper;
using LMS_CMS_BL.UOW;
using LMS_CMS_DAL.Models.Domains.LMS;
using LMS_CMS_DAL.Models.Domains.RegisterationModule;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_PL.Services
{
    public class CancelInterviewDayMessageService
    {
        private readonly DbContextFactoryService _dbContextFactory;

        public CancelInterviewDayMessageService(DbContextFactoryService dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async void CancelInterviewDayMessage(long interviewID, HttpContext httpContext)
        {
            UOW Unit_Of_Work = _dbContextFactory.CreateOneDbContext(httpContext);

            List<RegisterationFormInterview> registerationFormInterviews = await Unit_Of_Work.registerationFormInterview_Repository.Select_All_With_IncludesById<RegisterationFormInterview>(
                    f => f.IsDeleted != true && f.InterviewTimeID == interviewID,
                    query => query.Include(emp => emp.RegisterationFormParent)
                    );

            if(registerationFormInterviews != null && registerationFormInterviews.Count != 0)
            {
                Console.WriteLine("Interview Canceled");
            }

            Console.WriteLine("Interview Canceled");
        }
    }
}
