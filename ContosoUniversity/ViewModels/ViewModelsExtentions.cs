using System.Collections.Generic;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels
{
    public static class ViewModelsExtentions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> list,
                                                 object selectedId = null,
                                                 string valueMember = "Id",
                                                 string textMember = "Name")
        {
            return new SelectList(items: list,
                                  dataValueField: valueMember,
                                  dataTextField: textMember,
                                  selectedValue: selectedId);
        }
    }
}