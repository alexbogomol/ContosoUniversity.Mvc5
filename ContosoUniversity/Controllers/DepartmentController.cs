using AutoMapper;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Filters;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Departments;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class DepartmentController : BaseController
    {
        public DepartmentController(ISchoolUow uow)
        {
            UoW = uow;
        }

        // GET: Department
        public async Task<ActionResult> Index()
        {
            var departments = await UoW.Departments.GetAllAsync();

            return View(departments);
        }

        // GET: Department/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var department = await UoW.Departments.GetByIdAsync(id.Value);
            
            if (department == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<DepartmentDetailsViewModel>(department);

            return View(viewmodel);
        }

        // GET: Department/Create
        [PopulateInstructorsList]
        public ActionResult Create()
        {
            return View(new DepartmentCreateForm());
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PopulateInstructorsList]
        public async Task<ActionResult> Create(DepartmentCreateForm form)
        {
            if (ModelState.IsValid)
            {
                UoW.Departments.Add(new Department
                {
                    Name = form.Name,
                    Budget = form.Budget,
                    StartDate = form.StartDate,
                    InstructorId = form.InstructorId
                });
                await UoW.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(form);
        }

        // GET: Department/Edit/5
        [PopulateInstructorsList]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = await UoW.Departments.GetByIdAsync(id.Value);

            if (department == null)
            {
                return HttpNotFound();
            }

            var viewmodel = Mapper.Map<DepartmentEditForm>(department);

            return View(viewmodel);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        [PopulateInstructorsList]
        public async Task<ActionResult> Edit(DepartmentEditForm form)
        {
            string[] fieldsToBind = new string[]
            {
                "Name", "Budget", "StartDate", "InstructorID", "RowVersion"
            };

            if (form == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var departmentToUpdate = await UoW.Departments.GetByIdAsync(id.Value);
            var departmentToUpdate = await UoW.Departments.GetByIdAsync(form.Id);

            if (departmentToUpdate == null)
            {
                Department deletedDepartment = new Department();

                TryUpdateModel(deletedDepartment, fieldsToBind);

                ModelState.AddModelError(string.Empty, "Unable to save changes. The department was deleted by another user.");

                return View(deletedDepartment);
            }

            if (TryUpdateModel(departmentToUpdate, fieldsToBind))
            {
                try
                {
                    departmentToUpdate.RowVersion = form.RowVersion;
                    await UoW.CommitAsync();
                    
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Department)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("Name", "Current value: " + databaseValues.Name);

                        if (databaseValues.Budget != clientValues.Budget)
                            ModelState.AddModelError("Budget", "Current value: " + string.Format("{0:c}", databaseValues.Budget));

                        if (databaseValues.StartDate != clientValues.StartDate)
                            ModelState.AddModelError("StartDate", "Current value: " + string.Format("{0:d}", databaseValues.StartDate));

                        if (databaseValues.InstructorId != clientValues.InstructorId)
                            ModelState.AddModelError("InstructorId", "Current value: " + UoW.Instructors.GetById(databaseValues.InstructorId.Value).FullName);

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");

                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View(departmentToUpdate);
        }

        // GET: Department/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = await UoW.Departments.GetByIdAsync(id.Value);

            if (department == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }
            
            var viewmodel = Mapper.Map<DepartmentDetailsViewModel>(department);

            return View(viewmodel);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Department department)
        {
            try
            {
                UoW.Departments.Delete(department);
                await UoW.CommitAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = department.Id });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(department);
            }
        }
    }
}
