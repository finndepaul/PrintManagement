﻿using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.ResponseModels
{
    public class DataResponseProject
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string EmployeeName { get; set; } //Nhân viên phụ trách project
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
    }
}
