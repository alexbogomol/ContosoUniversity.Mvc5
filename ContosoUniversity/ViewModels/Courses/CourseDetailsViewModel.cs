using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CourseDetailsViewModel
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
    }
}