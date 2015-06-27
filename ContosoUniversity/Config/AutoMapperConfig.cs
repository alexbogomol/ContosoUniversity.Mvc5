using AutoMapper;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.Courses;
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
                cfg.CreateMap<Student, StudentEditForm>();
            });
        }
    }
}