﻿using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Instructors
{
    public class InstructorEditForm : IMapFrom<Instructor>
    {
        [HiddenInput]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "The first character must upper case and the remaining characters must be alphabetical")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Assigned Courses")]
        public IEnumerable<AssignedCourseOption> AssignedCourses { get; set; }

        public int[] SelectedCourses { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string OfficeAssignmentLocation { get; set; }
    }
}