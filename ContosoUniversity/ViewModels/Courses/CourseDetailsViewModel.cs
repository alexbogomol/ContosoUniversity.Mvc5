using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.Models;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CourseDetailsViewModel : IMapFrom<Course>
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        public string TitleWithId
        {
            get { return string.Format("{0} {1}", Id, Title); }
        }
    }
}