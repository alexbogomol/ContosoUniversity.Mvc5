using ContosoUniversity.Config;
using ContosoUniversity.Controllers;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Students;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace ContosoUniversity.UnitTests.ControllerTests
{
    [TestFixture]
    public class StudentsControllerTests
    {
        IQueryable<Student> studentslist;
        Mock<IStudentsRepository> studentsrepo;
        Mock<ISchoolUow> uow;
        StudentController controller;

        [TestFixtureSetUp]
        public void Initialize()
        {
            AutoMapperConfig.Init();

            studentslist = (new Student[]
            {
                new Student { Id = 1, LastName = "Student1" },
                new Student { Id = 2, LastName = "Student2" },
                new Student { Id = 3, LastName = "Student3" },
                new Student { Id = 4, LastName = "Student4" },
                new Student { Id = 5, LastName = "Student5" }
            }).AsQueryable();

            studentsrepo = new Mock<IStudentsRepository>();
            studentsrepo.Setup(repo => repo.GetAll())
                        .Returns(studentslist);
            studentsrepo.Setup(repo => repo.GetById(It.IsAny<int>()))
                        .Returns((int id) => studentslist.Where(s => s.Id == id).SingleOrDefault());

            uow = new Mock<ISchoolUow>();
            uow.Setup(uow => uow.Students).Returns(studentsrepo.Object);

            controller = new StudentController(uow.Object);
        }

        [Test]
        public void IndexShouldRenderDefaultView()
        {
            controller.WithCallTo(c => c.Index(null, null, null, null))
                      .ShouldRenderDefaultView();
        }

        [Test]
        public void IndexShouldRenderFirstPage()
        {
            var result = controller.Index(null, null, null, null) as ViewResult;

            var viewmodel = result.Model as StudentsListViewModel;

            Assert.That(viewmodel.StudentsList.PageNumber == 1);
            Assert.That(viewmodel.StudentsList.PageCount == 2);
            Assert.That(viewmodel.StudentsList.PageSize == 3);
            Assert.That(viewmodel.StudentsList[0].Id == 1);
            Assert.That(viewmodel.StudentsList[1].Id == 2);
            Assert.That(viewmodel.StudentsList[2].Id == 3);
        }

        [Test]
        public void IndexShouldRenderSecondPage()
        {
            var result = controller.Index(null, null, null, 2) as ViewResult;

            var viewmodel = result.Model as StudentsListViewModel;

            Assert.That(viewmodel.StudentsList.PageNumber == 2);
            Assert.That(viewmodel.StudentsList.PageCount == 2);
            Assert.That(viewmodel.StudentsList.PageSize == 3);
            Assert.That(viewmodel.StudentsList[0].Id == 4);
            Assert.That(viewmodel.StudentsList[1].Id == 5);
        }

        [Test]
        public void DetailsShouldReturnBadRequest()
        {
            controller.WithCallTo(c => c.Details(null))
                      .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DetailsShouldReturnNotFound()
        {
            controller.WithCallTo(c => c.Details(12345))
                      .ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [Test]
        public void DetailsShouldRenderStudent()
        {
            var result = controller.Details(3) as ViewResult;

            var viewmodel = result.Model as StudentDetailsViewModel;

            Assert.That(viewmodel.Id == 3);
            Assert.That(viewmodel.LastName == "Student3");
        }
    }
}
