﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Models
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "Code Is Requierd!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Requierd!")]
        public string Name { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}