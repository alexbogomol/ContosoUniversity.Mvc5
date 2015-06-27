using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Students
{
    /// <remarks>
    /// TODO: - form name, metadata, etc.
    /// TODO: - the same to do with 'Instructor' model
    ///         (clean off all presentational metadata)
    /// </remarks>
    public class StudentCreateForm
    {
        //public int Id { get; set; }
        
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "The first character must upper case and the remaining characters must be alphabetical")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get
        //    {
        //        return string.Format("{0}, {1}", LastName, FirstMidName);
        //    }
        //}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
    }
}