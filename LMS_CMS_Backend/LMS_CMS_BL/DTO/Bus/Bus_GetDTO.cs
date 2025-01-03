﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Bus
{
    public class Bus_GetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long Capacity { get; set; }
        public bool IsCapacityRestricted { get; set; }
        public long? BusTypeID { get; set; }
        public string? BusTypeName { get; set; }
        public long? BusDistrictID { get; set; }
        public string? BusDistrictName { get; set; }
        public long? BusStatusID { get; set; }
        public string? BusStatusName { get; set; }
        public long? DriverID { get; set; }
        public string? DriverName { get; set; }
        public long? DriverAssistantID { get; set; }
        public string? DriverAssistantName { get; set; }
        public long? BusCompanyID { get; set; }
        public string? BusCompanyName { get; set; }
        public long? InsertedByOctaId { get; set; }
        public long? InsertedByUserId { get; set; }
        public long? UpdatedByOctaId { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long? DeletedByOctaId { get; set; }
        public long? DeletedByUserId { get; set; }
        public int MorningPrice { get; set; }
        public int BackPrice { get; set; }
        public int TwoWaysPrice { get; set; }

    }
}
