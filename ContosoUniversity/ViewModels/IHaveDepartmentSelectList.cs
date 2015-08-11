using System.Web.Mvc;

namespace ContosoUniversity.ViewModels
{
    public interface IHaveDepartmentSelectList
    {
        SelectList DepartmentSelectList { get; set; }
    }
}
