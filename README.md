# Sales_Date_Prediction

* Proyecto en .NET 8.0 que sigue principios SOLID y Clean Architecture, implementando un enfoque CQRS (Command Query Responsibility Segregation) mediante MediatR para lograr una organización escalable y fácil de mantener. La capa de persistencia utiliza Dapper como micro-ORM, y el proyecto está preparado para la integración con librerías como Automapper y FluentValidation.

* Versión .NET: El proyecto se desarrolló en .NET 8.0.
* Patrón de Arquitectura: Se adoptó la Clean Architecture para desacoplar la lógica de negocio (Application/Domain) de los detalles de infraestructura (Infrastructure) y la capa de presentación (API).
* SOLID: Cada capa y clase respeta los principios SOLID, facilitando la mantenibilidad y el escalamiento.
* CQRS + MediatR: Se estructura la aplicación con Commands y Queries, manejados por MediatR como patrón mediador.
* Dapper: El micro-ORM empleado para las operaciones de base de datos, basado en ADO.NET, con muy buen rendimiento.
* Procedimientos Almacenados: Se incluyen los procedimientos almacenados necesarios para la correcta ejecución de la aplicación. Es requisito ejecutar dichos procedimientos antes de     
  realizar las pruebas, a fin de asegurar que la base de datos cuente con la estructura y datos adecuados.

* El proyecto se compone de 5 carpetas principales (5 proyectos dentro de la solución):
  * Sales_Date_Prediction.Domain: Contiene las entidades (clases de dominio).
  * No depende de otras capas, mantiene la lógica de dominio pura.
  * Sales_Date_Prediction.Application:Lógica de negocio y casos de uso: Commands, Queries y Handlers (CQRS).
  * Sales_Date_Prediction.Infrastructure: Acceso a datos con Dapper, implementando las interfaces de Application. Maneja repositorios y la configuración de base de datos.
  * Sales_Date_Prediction.API:Exposición de endpoints (controladores) en un proyecto Web API .NET 8. Utiliza MediatR para despachar cada Query o Command según corresponda a la operación 
  * solicitada (GET, POST, etc.).Manejo de CORS, Swagger y configuración de inyección de dependencias.
