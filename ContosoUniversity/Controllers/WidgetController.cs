using AutoMapper.QueryableExtensions;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.ViewModels.Courses;
using ContosoUniversity.ViewModels.Enrollments;
using ContosoUniversity.ViewModels.Instructors;
using System.Linq;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class WidgetController : BaseController
    {
        public WidgetController(ISchoolUow uow)
        {
            UoW = uow;
        }

        [ChildActionOnly]
        public PartialViewResult InstructorsWidget(int? instructorId, int? courseId)
        {
            return PartialView(new InstructorsWidget
            {
                Instructors = UoW.Instructors.GetAll()
                                 .OrderBy(i => i.LastName)
                                 .Project().To<InstructorsListItemViewModel>(),

                InstructorId = instructorId
            });
        }

        [ChildActionOnly]
        public PartialViewResult CoursesWidget(int? instructorId, int? courseId)
        {
            return PartialView(new CoursesWidget
            {
                Courses = UoW.Courses.GetByInstructor(instructorId)
                             .AsQueryable()
                             .Project().To<CourseDetailsViewModel>(),

                InstructorId = instructorId,

                CourseId = courseId
            });
        }

        [ChildActionOnly]
        public PartialViewResult EnrollmentsWidget(int? courseId)
        {
            return PartialView(new EnrollmentsWidget
            {
                Enrollments = UoW.Enrollments.GetByCourse(courseId)
                                 .AsQueryable()
                                 .Project().To<EnrollmentViewModel>()
            });
        }
    }
}