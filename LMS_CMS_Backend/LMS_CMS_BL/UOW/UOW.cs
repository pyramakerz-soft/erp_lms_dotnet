using LMS_CMS_BL.Repository;
using LMS_CMS_DAL.Models;
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
        GenericRepo<Detailed_Permissions> Detailed_Permissions_Repository;
        GenericRepo<Master_Permissions> Master_Permissions_Repository;
        GenericRepo<Employee_Role> Employee_Role_Repository;
        GenericRepo<Role_Permissions> Role_Detailed_Permissions_Repository;
        GenericRepo<Employee_With_Role_Permission_View> Employee_With_Role_Permission_View_Repository;


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
        public GenericRepo<Detailed_Permissions> detailed_Permissions_Repository
        {
            get
            {
                if (Detailed_Permissions_Repository == null)
                {
                    Detailed_Permissions_Repository = new GenericRepo<Detailed_Permissions>(db);
                }
                return Detailed_Permissions_Repository;
            }
        }
        public GenericRepo<Master_Permissions> master_Permissions_Repository
        {
            get
            {
                if (Master_Permissions_Repository == null)
                {
                    Master_Permissions_Repository = new GenericRepo<Master_Permissions>(db);
                }
                return Master_Permissions_Repository;
            }
        }
        public GenericRepo<Employee_Role> employee_Role_Repository
        {
            get
            {
                if (Employee_Role_Repository == null)
                {
                    Employee_Role_Repository = new GenericRepo<Employee_Role>(db);
                }
                return Employee_Role_Repository;
            }
        }

        public GenericRepo<Role_Permissions> role_Detailed_Permissions_Repository
        {
            get
            {
                if (Role_Detailed_Permissions_Repository == null)
                {
                    Role_Detailed_Permissions_Repository = new GenericRepo<Role_Permissions>(db);
                }
                return Role_Detailed_Permissions_Repository;
            }
        }

        public GenericRepo<Employee_With_Role_Permission_View> employee_With_Role_Permission_View_Repository
        {
            get
            {
                if (Employee_With_Role_Permission_View_Repository == null)
                {
                    Employee_With_Role_Permission_View_Repository = new GenericRepo<Employee_With_Role_Permission_View>(db);
                }
                return Employee_With_Role_Permission_View_Repository;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
