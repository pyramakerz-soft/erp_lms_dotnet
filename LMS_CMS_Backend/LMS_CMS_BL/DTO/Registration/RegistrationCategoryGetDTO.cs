﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegistrationCategoryGetDTO
    {
        public long ID { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public int OrderInForm { get; set; }
        public long? InsertedByUserId { get; set; }
        public List<CategoryFieldGetDTO> Fields { get; set; }
    }
}
