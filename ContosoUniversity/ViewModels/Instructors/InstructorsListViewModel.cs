using ContosoUniversity.ViewModels.Courses;
using ContosoUniversity.ViewModels.Enrollments;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Instructors
{
    [DisplayName("Instructors available:")]
    public class InstructorsListViewModel
    {
        [Display(Name = "Courses, taught by selected instructor:")]
        public bool ShowCoursesWidget { get { return InstructorId.HasValue; } }

        [Display(Name = "Students, enrolled in selected course:")]
        public bool ShowEnrollmentsWidget { get { return CourseId.HasValue; } }

        public int? InstructorId { get; set; }
        public int? CourseId { get; set; }
    }

    public class InstructorsWidget
    {
        public IEnumerable<InstructorsListItemViewModel> Instructors { get; set; }
        public int? InstructorId { get; set; }
    }

    public class CoursesWidget
    {
        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }
        public int? InstructorId { get; set; }
        public int? CourseId { get; set; }
    }

    public class EnrollmentsWidget
    {
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}