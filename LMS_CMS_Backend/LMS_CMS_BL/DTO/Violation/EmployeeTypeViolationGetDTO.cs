﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Violation
{
    public class EmployeeTypeViolationGetDTO
    {
        public long ID { get; set; }
        public long ViolationID { get; set; }
        public string? ViolationsTypeName { get; set; }
        public List<EmployeeTypeGetDTO>  employeeTypes { get; set; }
        public long? InsertedByUserId { get; set; }

    }
}
