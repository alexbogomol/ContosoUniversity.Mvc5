using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels
{
    public static class ViewModelsExtentions
    {
        public static SelectList ToSelectList(this IEnumerable<Department> departments, 
                                              object selectedId = null)
        {
            return new SelectList(items: departments.OrderBy(d => d.Name),
                                  dataValueField: "Id",
                                  dataTextField: "Name",
                                  selectedValue: selectedId);
        }
    }
}