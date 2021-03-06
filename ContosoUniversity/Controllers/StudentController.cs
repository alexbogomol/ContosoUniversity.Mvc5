﻿using AutoMapper;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Infrastructure.Alerts;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Students;
using PagedList;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(ISchoolUow uow)
        {
            UoW = uow;
        }

        // GET: Student
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var viewmodel = new StudentsListViewModel
            {
                CurrentSort = sortOrder,
                NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "",
                DateSortParm = sortOrder == "Date" ? "date_desc" : "Date"
            };

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            viewmodel.CurrentFilter = searchString;

            var students = UoW.Students.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                            || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            viewmodel.StudentsList = students.ToPagedList(pageNumber, pageSize);

            return View(viewmodel);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = UoW.Students.GetById(id.GetValueOrDefault());

            if (student == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<StudentDetailsViewModel>(student);

            return View(viewmodel);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View(new StudentCreateForm
            {
                EnrollmentDate = DateTime.Now.Date
            });
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCreateForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UoW.Students.Add(new Student
                    {
                        LastName = form.LastName,
                        FirstMidName = form.FirstMidName,
                        EnrollmentDate = form.EnrollmentDate
                    });

                    UoW.Commit();

                    return RedirectToAction<StudentController>(c => c.Index(null, null, null, null))
                        .WithSuccess("Student Created Successfully!");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(form).WithError("Error occured! Look at the info below.");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = UoW.Students.GetById(id.GetValueOrDefault());

            if (student == null)
            {
                return HttpNotFound();
            }

            var form = Mapper.Map<StudentEditForm>(student);

            return View(form);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentEditForm form)
        {
            if (form == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var updatee = UoW.Students.GetById(form.Id);

            var updated = TryUpdateModel(updatee, "", new string[] 
            {
                "LastName",
                "FirstMidName",
                "EnrollmentDate"
            });

            if (updated)
            {
                try
                {
                    UoW.Commit();

                    return RedirectToAction<StudentController>(c => c.Index(null, null, null, null))
                        .WithSuccess("Instructor Updated Successfully!");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(updatee).WithError("Error occured! Look at the info below.");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            var student = UoW.Students.GetById(id.GetValueOrDefault());

            if (student == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<StudentDetailsViewModel>(student);

            return View(viewmodel);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                UoW.Students.Remove(id);
                UoW.Commit();
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction<StudentController>(c => c.Delete(id, true))
                    .WithError("Error occured! Look at the info below.");
            }

            return RedirectToAction<StudentController>(c => c.Index(null, null, null, null))
                        .WithSuccess("Instructor Deleted Successfully!");
        }
    }
}
