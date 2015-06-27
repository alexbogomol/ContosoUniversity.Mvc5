## ContosoUniversity.Mvc5 (Asp.Net 4.6)

Further development for the ContosoUniversity tutorial, based on the traditional **Asp.Net 4.6** stack:
* Mvc 5
* Entity Framework 6
* Ninject
* AutoMapper

#### Refactoring the **CourseController**

The **CourseController** is taken as the first refactoring target for being the most simple one. It is also easier to apply further practices and involve the third-party libs (**IoC**/**Ninject**, **ViewModels**/**AutoMapper**, etc.) when the most simple case is a subject.

##### Tasks for CourseController

- [x] Split the original project apart to separate tasks and responsibilities (`.DataAccess` and `.Models` projects introduced)
- [x] Adding *Repositories* (`IRepository<T>`) and *Unit-Of-Work* (`ISchoolUow`) contracts for data-access layer. Implementations available in `EfRepository<T>` (EntityFramework-specific) and `SchoolUow`
- [x] Introducing the **BaseController**, backed with the *Unit-Of-Work* and disposing logic for it. **CourseController** is now a **BaseController**
- [x] Total use of *Unit-Of-Work* in the **CourseController**. No more use of *DbContext*
- [x] A new `ICoursesRepository` contract is an `IRepository<Course>` itself and also introduces two specific methods: `.UpdateCourseCredits()` and `.GetByDepartment()`. Implementations available in `CoursesRepository` which itself is an `EfRepository<Course>` also
- [x] Select list for departments is not any more a boring controller void-method `.PopulateDepartmentsDropDownList()`. Extension method `.ToSelectList()` for `IEnumerable<Departments>` can now issue `SelectList` **fluently** :sparkles:
- [x] Introduce view-models for Index-Create-Edit action methods
- [x] `SelectList`'s in view-models. No more use of `ViewBag`
- [x] Started with **Ninject.MVC5** and **AutoMapper** packages
- [x] Ctor-Inject *Unit-Of-Work* in the **CourseController**
- [x] Some view-models mappings for **CourseController**
- [ ] Action-filter for `IHaveDepartmentSelectList`
