﻿using ContosoUniversity.ViewModels.Courses;
using ContosoUniversity.ViewModels.Enrollments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Instructors
{
    public class InstructorsListViewModel
    {
        [Display(Name = "Instructors available:")]
        public InstructorsWidget InstructorsWidget { get; set; }

        [Display(Name = "Courses, taught by selected instructor:")]
        public CoursesWidget CoursesWidget { get; set; }

        [Display(Name = "Students, enrolled in selected course:")]
        public EnrollmentsWidget EnrollmentsWidget { get; set; }
    }

    public class InstructorsWidget
    {
        public IEnumerable<InstructorsListItemViewModel> Instructors { get; set; }
        public int InstructorId { get; set; }
    }

    public class CoursesWidget
    {
        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }
        public int CourseId { get; set; }
    }

    public class EnrollmentsWidget
    {
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}