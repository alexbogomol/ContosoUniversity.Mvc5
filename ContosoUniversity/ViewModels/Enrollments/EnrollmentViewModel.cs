using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Enrollments
{
    public class EnrollmentViewModel
    {
        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; }

        [Display(Name = "Enrolled Grade")]
        public string Grade { get; set; }
    }
}