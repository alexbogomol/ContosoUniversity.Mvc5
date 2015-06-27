## ContosoUniversity.Mvc5 (Asp.Net 4.6)

Further development for the ContosoUniversity tutorial, based on the traditional **Asp.Net 4.6** stack:
* Mvc 5
* Entity Framework 6
* Ninject
* AutoMapper

#### Refactoring the CourseController

The **CourseController** is taken as the first refactoring target for being the most simple one. It is also easier to apply further practices and involve the third-party libs (IoC/Ninject, ViewModels/AutoMapper, etc.) when the most simple case is a subject.

##### Tasks for CourseController

- [x] Split the original project apart to separate tasks and responsibilities (`.DataAccess` and `.Models` projects introduced)
- [x] Adding repositories (`IRepository<T>`) and unit-of-work (`ISchoolUow`) contracts for data-access layer. Implementations are available in `EfRepository<T>` (EntityFramework-specific) and `SchoolUow`
- [x] Introducing the **BaseController**, backed with the unit-of-work and disposing logic for it. **CourseController** is now a **BaseController**
- [x] Total use of unit-of-work in the **CourseController**. No more use of **DbContext**
- [x] A new `ICoursesRepository` contract is an `IRepository<Course>` itself and also introduces two specific methods: `.UpdateCourseCredits()` and `.GetByDepartment()`. Implementations available in `CoursesRepository` which itself is an `EfRepository<Course>` also
- [x] Selectlist for departments is not any more a boring controller void-method `.PopulateDepartmentsDropDownList()`. Extension method `.ToSelectList()` for `IEnumerable<Departments>` can now issue `SelectList` **fluently** :sparkles:
- [x] Introduce view-models for Index-Create-Edit action methods
- [x] `SelectList`'s in view-models. No more use of `ViewBag`
- [x] Started with **Ninject.MVC5** and **AutoMapper** packages
- [x] Ctor-Inject `ISchoolUow` into the **CourseController**
- [x] Some view-models mappings for **CourseController**
- [ ] Action-filter for `IHaveDepartmentSelectList`

#### Refactoring the StudentController

##### Tasks for StudentController

- [x] Refactor the controller to operate on unit-of-work
- [x] Introduce view-models for Create-Edit action methods
- [ ] Fix this ugly-url (when sorting) with specific routing rules. Should be smth like:

      `Student/Order/LastName`
      
      `Student/Order/LastName/Desc`
      
      `Student/Order/Date`
      
      `Student/Order/Date/Desc`

- [ ] Same for pages. Like:

      `Student/Page2`
      
      `Student/Page3`
- [ ] Refactor Index action (it's really mess for now)
- [ ] Calendar drop-down (jquery-ui) for `EnrollmentDate' would be fine
