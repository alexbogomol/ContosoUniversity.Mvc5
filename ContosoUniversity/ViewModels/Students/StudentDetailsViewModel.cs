using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Enrollments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Students
{
    public class StudentDetailsViewModel : IMapFrom<Student>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Enrolled for")]
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}