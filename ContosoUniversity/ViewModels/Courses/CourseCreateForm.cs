﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels.Courses
{
    public class CourseCreateForm : IHaveDepartmentSelectList
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }
        
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public SelectList DepartmentSelectList { get; set; }
    }
}