﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class BusRestrictGetDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }
        public long DomainId { get; set; }
    }
}