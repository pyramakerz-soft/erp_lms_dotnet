﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class FeesActivationAdddDTO
    {
        public float Amount { get; set; }
        public float Discount { get; set; }
        public float Net { get; set; }
        public string Date { get; set; }
        public long FeeTypeID { get; set; }
        public long? FeeDiscountTypeID { get; set; }
        public long StudentID { get; set; }
        public long AcademicYearId { get; set; }

    }
}
