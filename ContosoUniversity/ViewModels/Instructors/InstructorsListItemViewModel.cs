using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Instructors
{
    public class InstructorsListItemViewModel : IMapFrom<Instructor>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Office Location")]
        public string OfficeAssignmentLocation { get; set; }

        [Display(Name = "Courses Tought")]
        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }
    }
}