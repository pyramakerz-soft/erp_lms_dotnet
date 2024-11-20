using LMS_CMS_BL.Repository;
using LMS_CMS_DAL.Models;
using LMS_CMS_DAL.Models.BusModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.UOW
{
    public class UOW
    {
        LMS_CMS_Context db;

        GenericRepo<Employee> Employee_Repository;
        GenericRepo<Parent> Parent_Repository;
        GenericRepo<Student> Student_Repository;
        GenericRepo<Role> Role_Repository;
        GenericRepo<Domain> Domain_Repository;
        GenericRepo<School> School_Repository;
        GenericRepo<Domain_Page_Detailes> Domain_Page_Detailes_Repository;
        GenericRepo<Page> Page_Repository;
        GenericRepo<Role_Detailes> Role_Detailes_Repository;
        GenericRepo<Pyramakerz> Pyramakerz_Repository;
        GenericRepo<Bus> Bus_Repository;
        GenericRepo<BusStudent> BusStudent_Repository;
        GenericRepo<BusType> BusType_Repository;
        GenericRepo<BusRestrict> BusRestrict_Repository;
        GenericRepo<BusCategory> BusCategory_Repository;
        GenericRepo<BusStatus> BusStatus_Repository;
        GenericRepo<BusCompany> BusCompany_Repository;

        public UOW(LMS_CMS_Context db)
        {
            this.db = db;
        }

        public GenericRepo<Employee> employee_Repository
        {
            get
            {
                if (Employee_Repository == null)
                {
                    Employee_Repository = new GenericRepo<Employee>(db);
                }
                return Employee_Repository;
            }
        }

        public GenericRepo<Parent> parent_Repository
        {
            get
            {
                if (Parent_Repository == null)
                {
                    Parent_Repository = new GenericRepo<Parent>(db);
                }
                return Parent_Repository;
            }
        }
        public GenericRepo<Student> student_Repository
        {
            get
            {
                if (Student_Repository == null)
                {
                    Student_Repository = new GenericRepo<Student>(db);
                }
                return Student_Repository;
            }
        }
        public GenericRepo<Role> role_Repository
        {
            get
            {
                if (Role_Repository == null)
                {
                    Role_Repository = new GenericRepo<Role>(db);
                }
                return Role_Repository;
            }
        }

        public GenericRepo<Domain> domain_Repository
        {
            get
            {
                if (Domain_Repository == null)
                {
                    Domain_Repository = new GenericRepo<Domain>(db);
                }
                return Domain_Repository;
            }
        }

        public GenericRepo<School> school_Repository
        {
            get
            {
                if (School_Repository == null)
                {
                    School_Repository = new GenericRepo<School>(db);
                }
                return School_Repository;
            }
        }

        public GenericRepo<Pyramakerz> pyramakerz_Repository
        {
            get
            {
                if (Pyramakerz_Repository == null)
                {
                    Pyramakerz_Repository = new GenericRepo<Pyramakerz>(db);
                }
                return Pyramakerz_Repository;
            }
        }

        public GenericRepo<Domain_Page_Detailes> domain_Page_Detailes_Repository
        {
            get
            {
                if (Domain_Page_Detailes_Repository == null)
                {
                    Domain_Page_Detailes_Repository = new GenericRepo<Domain_Page_Detailes>(db);
                }
                return Domain_Page_Detailes_Repository;
            }
        }

        public GenericRepo<Page> page_Repository
        {
            get
            {
                if (Page_Repository == null)
                {
                    Page_Repository = new GenericRepo<Page>(db);
                }
                return Page_Repository;
            }
        }

        public GenericRepo<Role_Detailes> role_Detailes_Repository
        {
            get
            {
                if (Role_Detailes_Repository == null)
                {
                    Role_Detailes_Repository = new GenericRepo<Role_Detailes>(db);
                }
                return Role_Detailes_Repository;
            }
        }

        public GenericRepo<Bus> bus_Repository
        {
            get
            {
                if (Bus_Repository == null)
                {
                    Bus_Repository = new GenericRepo<Bus>(db);
                }
                return Bus_Repository;
            }
        }

        public GenericRepo<BusStudent> busStudent_Repository
        {
            get
            {
                if (BusStudent_Repository == null)
                {
                    BusStudent_Repository = new GenericRepo<BusStudent>(db);
                }
                return BusStudent_Repository;
            }
        }

        public GenericRepo<BusType> busType_Repository
        {
            get
            {
                if (BusType_Repository == null)
                {
                    BusType_Repository = new GenericRepo<BusType>(db);
                }
                return BusType_Repository;
            }
        }

        public GenericRepo<BusRestrict> busRestrict_Repository
        {
            get
            {
                if (BusRestrict_Repository == null)
                {
                    BusRestrict_Repository = new GenericRepo<BusRestrict>(db);
                }
                return BusRestrict_Repository;
            }
        }
        public GenericRepo<BusCategory> busCategory_Repository
        {
            get
            {
                if (BusCategory_Repository == null)
                {
                    BusCategory_Repository = new GenericRepo<BusCategory>(db);
                }
                return BusCategory_Repository;
            }
        }
        public GenericRepo<BusStatus> busStatus_Repository
        {
            get
            {
                if (BusStatus_Repository == null)
                {
                    BusStatus_Repository = new GenericRepo<BusStatus>(db);
                }
                return BusStatus_Repository;
            }
        }
        public GenericRepo<BusCompany> busCompany_Repository
        {
            get
            {
                if (BusCompany_Repository == null)
                {
                    BusCompany_Repository = new GenericRepo<BusCompany>(db);
                }
                return BusCompany_Repository;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
