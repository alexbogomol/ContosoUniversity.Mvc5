using ContosoUniversity.Models;
using PagedList;

namespace ContosoUniversity.ViewModels.Students
{
    public class StudentsListViewModel
    {
        public string CurrentSort { get; set; }
        public string NameSortParm { get; set; }
        public string DateSortParm { get; set; }

        public string CurrentFilter { get; set; }

        public IPagedList<Student> StudentsList { get; set; }

        public string PagesLegendText
        {
            get
            {
                var number = StudentsList.PageCount < StudentsList.PageNumber 
                               ? 0 
                               : StudentsList.PageNumber;

                var count = StudentsList.PageCount;

                return $"Page { number } of { count }";
            }
        }
    }
}