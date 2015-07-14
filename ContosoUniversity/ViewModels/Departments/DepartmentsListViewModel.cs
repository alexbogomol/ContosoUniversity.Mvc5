using System.Collections.Generic;

namespace ContosoUniversity.ViewModels.Departments
{
    public class DepartmentsListViewModel
    {
        public IEnumerable<DepartmentDetailsViewModel> Departments { get; set; }
    }
}