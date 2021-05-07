﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model
{
    public class Review : BaseEntity
    {
        [Required]
        public int EmployeeReviewId { get; set; }


        [Required]
        public int ReviewerId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int RatingId { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [StringLength(255, ErrorMessage = "Comment must be between 2 and 255 characters", MinimumLength = 2)]
        public string Comment { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Review Date is required.")]
        public DateTime ReviewDate { get; set; }

        public virtual Rating Rating { get; set; }

    }

    public enum Rating
    {
        [Display(Name = "Below Expectations")] BelowExpectations = 1,
        [Display(Name = "Meets Expectations")] MeetsExpectations = 2,
        [Display(Name = "Exceeds Expectations")] ExceedsExpectations = 3
    }
}
