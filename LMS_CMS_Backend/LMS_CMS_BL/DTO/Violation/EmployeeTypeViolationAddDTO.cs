﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Violation
{
    public class EmployeeTypeViolationAddDTO
    {
        public String ViolationName { get; set; }
        public List<long> EmployeeTypeID { get; set; }

    }
}
