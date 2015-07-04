using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Instructors
{
    public class InstructorDetailsViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string OfficeAssignmentLocation { get; set; }
    }
}