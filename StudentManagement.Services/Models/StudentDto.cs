﻿using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NIC { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }
        public DateTime last_sync_date_time { get; set; }
    }
}