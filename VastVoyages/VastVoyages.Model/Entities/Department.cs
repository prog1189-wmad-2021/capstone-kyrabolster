﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model
{
    public class Department : BaseEntity
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(255, ErrorMessage = "Department name must be between 2 and 255 characters in length.", MinimumLength = 2)]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Department description is required")]
        [StringLength(255, ErrorMessage = "Department description must be between 2 and 255 characters in length.", MinimumLength = 2)]
        public string DepartmentDescription { get; set; }

        [Required(ErrorMessage = "Invocation date is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime InvocationDate { get; set; }
    }
}
