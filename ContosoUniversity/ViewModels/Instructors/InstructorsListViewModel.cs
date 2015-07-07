using ContosoUniversity.ViewModels.Courses;
using ContosoUniversity.ViewModels.Enrollments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Instructors
{
    public class InstructorsListViewModel
    {
        [Display(Name = "Instructors available:")]
        public IEnumerable<InstructorsListItemViewModel> Instructors { get; set; }

        [Display(Name = "Courses, taught by selected instructor:")]
        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }

        [Display(Name = "Students, enrolled in selected course:")]
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }

        public int InstructorId { get; set; }
        public int CourseId { get; set; }
    }
}