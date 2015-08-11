using System.Web.Mvc;

namespace ContosoUniversity.ViewModels
{
    public interface IHaveInstructorSelectList
    {
        SelectList InstructorSelectList { get; set; }
    }
}
