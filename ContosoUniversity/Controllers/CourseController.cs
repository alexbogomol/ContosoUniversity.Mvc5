using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Filters;
using ContosoUniversity.Infrastructure.Alerts;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Courses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(ISchoolUow uow)
        {
            UoW = uow;
        }

        // GET: Course
        [PopulateDepartmentsList]
        public ActionResult Index(int? departmentFilter)
        {
            int id = departmentFilter.GetValueOrDefault();

            return View(new CoursesListViewModel
            {
                Courses = UoW.Courses
                             .FindBy(c => !departmentFilter.HasValue || c.DepartmentId == id)
                             .OrderBy(course => course.Id)
                             .Project().To<CourseDetailsViewModel>()
            });
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = UoW.Courses.GetById(id.GetValueOrDefault());

            if (course == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<CourseDetailsViewModel>(course);

            return View(viewmodel);
        }

        // GET: Course/Create
        [PopulateDepartmentsList]
        public ActionResult Create()
        {
            return View(new CourseCreateForm());
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PopulateDepartmentsList]
        public ActionResult Create(CourseCreateForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UoW.Courses.Add(new Course
                    {
                        Id = form.Id,
                        Title = form.Title,
                        Credits = form.Credits,
                        DepartmentId = form.DepartmentId
                    });
                    UoW.Commit();
                    
                    return RedirectToAction<CourseController>(c => c.Index(null))
                            .WithSuccess("Course Created Successfully!");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(form).WithError("Error occured! Look at the info below.");
        }

        // GET: Course/Edit/5
        [PopulateDepartmentsList]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var course = UoW.Courses.GetById(id.GetValueOrDefault());

            if (course == null)
            {
                return HttpNotFound();
            }

            var form = Mapper.Map<CourseEditForm>(course);

            return View(form);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [PopulateDepartmentsList]
        public ActionResult EditPost(CourseEditForm form)
        {
            if (form == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var updatee = UoW.Courses.GetById(form.Id);

            var updated = TryUpdateModel(updatee, "", new string[] 
            {
                "Title",
                "Credits",
                "DepartmentId"
            });

            if (updated)
            {
                try
                {
                    UoW.Commit();

                    return RedirectToAction<CourseController>(c => c.Index(null))
                            .WithSuccess("Course Updated Successfully!");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(form).WithError("Error occured! Look at the info below.");
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = UoW.Courses.GetById(id.GetValueOrDefault());

            if (course == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<CourseDetailsViewModel>(course);

            return View(viewmodel);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UoW.Courses.Delete(id);
            UoW.Commit();

            return RedirectToAction<CourseController>(c => c.Index(null))
                            .WithSuccess("Course Deleted Successfully!");
        }

        public ActionResult UpdateCourseCredits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                ViewBag.RowsAffected =
                    UoW.Courses.UpdateCourseCredits(multiplier.GetValueOrDefault());
            }

            return View();
        }
    }
}
