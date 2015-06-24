using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CourseIndexViewModel
    {
        public SelectList DepartmentSelectList { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}