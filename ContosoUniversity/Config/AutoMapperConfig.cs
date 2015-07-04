using AutoMapper;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Courses;
using ContosoUniversity.ViewModels.Instructors;
using ContosoUniversity.ViewModels.Students;

namespace ContosoUniversity.Config
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMap<Course, CourseEditForm>();
                cfg.CreateMap<Course, CourseDetailsViewModel>();

                cfg.CreateMap<Student, StudentEditForm>();
                
                cfg.CreateMap<Instructor, InstructorEditForm>();
                cfg.CreateMap<Instructor, InstructorDetailsViewModel>();
            });
        }
    }
}