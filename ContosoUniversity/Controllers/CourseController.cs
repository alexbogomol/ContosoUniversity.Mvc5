using ContosoUniversity.DataAccess;
using ContosoUniversity.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController()
        {
            UoW = new SchoolUow();
        }

        // GET: Course
        public ActionResult Index(int? selectedDepartment)
        {
            ViewBag.SelectedDepartment = PopulateDepartmentsDropDownList(selectedDepartment);

            var courses = UoW.Courses
                             .GetByDepartment(selectedDepartment)
                             .OrderBy(course => course.Id);

            return View(courses);
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

            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = PopulateDepartmentsDropDownList();

            return View();
        }

        private SelectList PopulateDepartmentsDropDownList(object selectedId = null)
        {
            var query = from dpt in UoW.Departments.GetAll()
                        orderby dpt.Name
                        select dpt;
            
            return new SelectList(items: query, 
                                  dataValueField: "Id",
                                  dataTextField: "Name",
                                  selectedValue: selectedId);
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Credits,DepartmentId")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UoW.Courses.Add(course);
                    UoW.Commit();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            ViewBag.DepartmentId = PopulateDepartmentsDropDownList(course.DepartmentId);

            return View(course);
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

            ViewBag.DepartmentId = PopulateDepartmentsDropDownList(course.DepartmentId);

            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseToUpdate = UoW.Courses.GetById(id.GetValueOrDefault());

            var updated = TryUpdateModel(courseToUpdate, "", new string[] 
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

            ViewBag.DepartmentId = PopulateDepartmentsDropDownList(courseToUpdate.DepartmentId);

            return View(courseToUpdate);
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

            return View(course);
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
