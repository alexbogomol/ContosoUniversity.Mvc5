using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
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
        public ActionResult Index(int? departmentFilter)
        {
            return View(new CoursesListViewModel
            {
                DepartmentSelectList = UoW.Departments.GetAll().ToSelectList(departmentFilter),
                Courses = UoW.Courses
                             .GetByDepartment(departmentFilter)
                             .OrderBy(course => course.Id)
                             .AsQueryable()
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
        public ActionResult Create()
        {
            return View(new CourseCreateForm
            {
                DepartmentSelectList = UoW.Departments.GetAll().ToSelectList()
            });
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            
            form.DepartmentSelectList = UoW.Departments.GetAll().ToSelectList(form.DepartmentId);

            return View(form);
        }

        // GET: Course/Edit/5
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

            var editform = Mapper.Map<CourseEditForm>(course);

            editform.DepartmentSelectList = UoW.Departments.GetAll().ToSelectList(course.DepartmentId);

            return View(editform);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
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

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            
            form.DepartmentSelectList = UoW.Departments.GetAll().ToSelectList(form.DepartmentId);

            return View(form);
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

            return RedirectToAction("Index");
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
