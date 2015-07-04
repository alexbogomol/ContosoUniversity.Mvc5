using System.Collections.Generic;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CoursesListViewModel
    {
        public SelectList DepartmentSelectList { get; set; }
        
        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }
    }
}