﻿using AutoMapper;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Infrastructure.Alerts;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Instructors;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : BaseController
    {
        public InstructorController(ISchoolUow uow)
        {
            UoW = uow;
        }

        // GET: Instructor
        public ActionResult Index(int? instructorId, int? courseId)
        {
            var viewModel = new InstructorsListViewModel
            {
                InstructorId = instructorId,
                CourseId = courseId
            };

            return View(viewModel);
        }

        // GET: Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = UoW.Instructors.GetById(id.Value);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<InstructorDetailsViewModel>(instructor);

            return View(viewmodel);
        }

        // GET: Instructor/Create
        public ActionResult Create()
        {
            return View(new InstructorCreateForm
            {
                AssignedCourses = GetCoursesCheckList(),
                HireDate = DateTime.Now.Date
            });
        }

        private IEnumerable<AssignedCourseOption> GetCoursesCheckList(int instructorId = -1)
        {
            var availableCourses = UoW.Courses.GetAll();

            IEnumerable<int> assignedIds = new List<int> { };

            if (instructorId != -1)
            {
                assignedIds = UoW.Instructors.GetById(instructorId).Courses.Select(c => c.Id);
            }

            foreach (var course in availableCourses)
            {
                yield return new AssignedCourseOption
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Assigned = assignedIds.Contains(course.Id)
                };
            }
        }

        // POST: Instructor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstructorCreateForm form)
        {
            var coursesAssigned = new List<Course> { };

            if (form.SelectedCourses != null)
            {
                coursesAssigned = UoW.Courses.GetAll()
                                     .Where(c => form.SelectedCourses.Contains(c.Id))
                                     .ToList();
            }

            OfficeAssignment office = null;

            if (form.HasAssignedOffice)
            {
                office = new OfficeAssignment
                {
                    Location = form.OfficeAssignmentLocation
                };
            }
            
            if (ModelState.IsValid)
            {
                UoW.Instructors.Add(new Instructor
                {
                    LastName = form.LastName,
                    FirstMidName = form.FirstMidName,
                    HireDate = form.HireDate,
                    Courses = coursesAssigned,
                    OfficeAssignment = office
                });
                UoW.Commit();

                return RedirectToAction<InstructorController>(c => c.Index(null, null))
                        .WithSuccess("Instructor Created Successfully!");
            }

            form.AssignedCourses = GetCoursesCheckList();

            return View(form).WithError("Error occured! Look at the info below.");
        }

        // GET: Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = UoW.Instructors.GetById(id.Value);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            var form = Mapper.Map<InstructorEditForm>(instructor);

            form.AssignedCourses = GetCoursesCheckList(instructor.Id);

            return View(form);
        }

        // POST: Instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstructorEditForm form)
        {
            if (form == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var updatee = UoW.Instructors.GetById(form.Id);

            var updated = TryUpdateModel(updatee, "", new string[] 
            {
                "LastName",
                "FirstMidName",
                "HireDate"
            });

            if (updated)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(form.OfficeAssignmentLocation))
                    {
                        updatee.OfficeAssignment = null;
                    }
                    else
                    {
                        updatee.OfficeAssignment = new OfficeAssignment
                        {
                            Location = form.OfficeAssignmentLocation
                        };
                    }

                    UpdateInstructorCourses(form.SelectedCourses, updatee);

                    UoW.Commit();

                    return RedirectToAction<InstructorController>(c => c.Index(null, null))
                        .WithSuccess("Instructor Edited Successfully!");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            form.AssignedCourses = GetCoursesCheckList(form.Id);

            return View(form).WithError("Error occured! Look at the info below.");
        }

        private void UpdateInstructorCourses(int[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<int>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.Id));

            foreach (var course in UoW.Courses.GetAll())
            {
                if (selectedCoursesHS.Contains(course.Id))
                {
                    if (!instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        // GET: Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = UoW.Instructors.GetById(id.Value);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<InstructorDetailsViewModel>(instructor);

            return View(viewmodel);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var instructor = UoW.Instructors.GetByIdWithOffice(id);

            if (instructor.OfficeAssignment != null)
            {
                UoW.OfficeAssignments.Remove(instructor.OfficeAssignment);
            }
            
            UoW.Instructors.Remove(instructor);

            var department = UoW.Departments.GetAll()
                                .Where(d => d.InstructorId == id)
                                .SingleOrDefault();

            if (department != null)
            {
                department.InstructorId = null;
            }

            UoW.Commit();

            return RedirectToAction<InstructorController>(c => c.Index(null, null))
                        .WithSuccess("Instructor Deleted Successfully!");
        }
    }
}
