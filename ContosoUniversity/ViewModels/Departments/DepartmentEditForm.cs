using ContosoUniversity.Infrastructure.Mapping;
using ContosoUniversity.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Departments
{
    public class DepartmentEditForm : IHaveInstructorSelectList, IMapFrom<Department>
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public byte[] RowVersion { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Department")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        [Display(Name = "Directed by")]
        public int InstructorId { get; set; }

        public SelectList InstructorSelectList { get; set; }
    }
}