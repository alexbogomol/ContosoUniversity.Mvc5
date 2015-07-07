using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.Models;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Enrollments
{
    public class EnrollmentViewModel : IMapFrom<Enrollment>
    {
        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; }

        [Display(Name = "Enrolled Grade")]
        [DisplayFormat(NullDisplayText = "No grade")]
        public string Grade { get; set; }

        [Display(Name = "Enrolled Student")]
        public string StudentFullName { get; set; }
    }
}