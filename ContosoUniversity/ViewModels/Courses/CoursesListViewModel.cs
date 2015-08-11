using System.Collections.Generic;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CoursesListViewModel : IHaveDepartmentSelectList
    {
        public SelectList DepartmentSelectList { get; set; }
        
        public IEnumerable<CourseDetailsViewModel> Courses { get; set; }
    }
}