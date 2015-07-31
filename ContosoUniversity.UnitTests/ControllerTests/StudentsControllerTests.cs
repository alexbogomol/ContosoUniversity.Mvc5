using ContosoUniversity.Controllers;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using Moq;
using NUnit.Framework;
using System.Linq;
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

            uow = new Mock<ISchoolUow>();
            uow.Setup(uow => uow.Students).Returns(studentsrepo.Object);

            controller = new StudentController(uow.Object);
        }

        [Test]
        public void ShouldRenderDefaultView()
        {
            controller.WithCallTo(c => c.Index(null, null, null, null))
                      .ShouldRenderDefaultView();
        }
    }
}
