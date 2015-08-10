## ContosoUniversity.Mvc5 (Asp.Net 4.6)

Further development for the ContosoUniversity tutorial, based on the traditional **Asp.Net 4.6** stack:
* Mvc 5
* Entity Framework 6
* Ninject
* AutoMapper

#### General Issues on Original Project State

* Mixed controller-layer, data-layer and business-domain
* All things done in a single project
* Direct `Controller` <=> `DbContext` interaction on controller-layer
* Fragile view generation:
  * mixed use of data-access-specific and view-specific metadata
  * massive use of `ViewBag`
  * DRY-breaking select-list generation, etc.
* The project is unaware of Inversion-Of-Control

#### Data Layer Concepts

* General abstractions:
  * `IRepository<T>` - general repository contract
  * `IAsyncRepository<T>` - async version of `IRepository<T>`
  * `ISchoolUow` - unit-of-work contract
* General implementations:
  * `EfRepository<T>` - EntityFramework-dependent implementation of `IRepository<T>`
  * `EfAsyncRepository<T>` - async version of `EfRepository<T>`
  * `SchoolUow` - unit-of-work implementation
* Specific abstractions and implemetations for repositories:
* Data-layer is accessible for the controller via the ctor-injected implementation of `ISchoolUow`
* Extending standard repository interface with extension-methods (like **.FindBy()**, **.Query()**, etc.)

#### Controller Layer Concepts

* Introduced the **BaseController**, backed with the unit-of-work and disposing logic for it
* Views are generated from the viewmodels (not domain or entities)
* Viewmodels mapped from entities with AutoMapper
* Entity-2-viewmodel mappings for AutoMapper are built with the self-discovering process (viewmodels implement `IMapFrom<T>` or `IHaveCustomMappings`)
* Viewmodels generally have relations to other viewmodels (not domain or entities)
* Viewmodels concerns only presentational metadata
* Widget-based concept for structuring complex viewmodels
* Fluent select-list generation - **.ToSelectList()** extension-method for `IEnumerable<T>`'s
