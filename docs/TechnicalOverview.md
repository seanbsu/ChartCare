# Technical Overview

## Purpose
The purpose of this document is to provide a more in-depth technical overview of the project and its designs for those interested in contributing.

## Application Design
The project structure has been updated to include a **Service Layer** that encapsulates business logic and ensures a clean separation between the **Controller** and the **Model**. This helps to maintain a scalable and testable codebase.

The current goal is to have two main parts:
1. A public-facing site where companies can learn about the service and sign up.
2. A web portal for the charting application.

The public-facing site will use the .NET MVC framework and host the backend API. The web portal will use .NET Blazor Server and operate as a Single Page Application (SPA).

When a company initially signs up using the public-facing site (with the Identity framework), two actions will occur:
1. The company profile will be created and added to the `Company` table in the database.
2. A new user will be created for the company with ADMIN privileges, allowing the company to log in and begin adding employees and patients through the SPA.

**New in the Design:**
- Data displayed on the page, such as company descriptions or feature sets, will be **populated dynamically from the database** using services.
- Business logic related to retrieving or manipulating data is abstracted into the **Service Layer**, ensuring a clean separation between the business logic and the presentation layer.

## Technologies Used

### Programming Languages
The primary language for the project is C#. JavaScript may be used for front-end responsiveness, but as of now, it hasn't been implemented. HTML and CSS are used in the Razor pages.

### Database Language
The database language will be SQL Server. To view the design document, MySQL Workbench will be required; however, a design image is provided, which will need to be updated when the database changes.

### Frameworks
- **ASP.NET MVC** - Public-facing site
- **ASP.NET Core** - Web backend
- **ASP.NET Blazor Server** - SPA patient portal
- **Entity.Identity** - Login, Registration, and Role Management
- **Service Layer** - Encapsulates business logic and retrieves data from the database to populate the front-end.

### Dependencies
- **Bootstrap** - Used in most website components
- **jQuery** - Currently unused, but available if needed

### Service Layer
The **Service Layer** is a key architectural addition to the project:
- Every business logic service implements a corresponding **interface**, adhering to the **Dependency Inversion Principle (DIP)**. This promotes flexibility and testability.
- When creating a new service:
  1. An **interface** for the service must be created.
  2. A **concrete implementation** of the service will be added.
  3. The service must be **injected** into the controller that uses it via the controller's **constructor**, with the service set as a **field** in that controller using the service interface class.
  4. The new service will be **registered in `Program.cs`** using **Dependency Injection (DI)**, allowing it to be injected into any class that requires it.

## Database Design
An initial design for the database tables can be found [here](./docs/db-design.png). A `.mwb` file is also provided to view and edit table designs using MySQL Workbench's design tools. Please note that this diagram provides a rough overview of the tables; there are additional columns that will be included when the context migration file is generated, due to the Identity framework's default columns. This design specifies the columns that will be added to the default Identity tables, with data provided from the registration forms.

Additionally, the service layer will be responsible for **retrieving descriptions and features dynamically** from the database to be displayed on various pages. This helps ensure that updates to the database will automatically reflect on the website without requiring hard-coded values.

Currently, there is no live database; all work will be done locally.

## Figma
A preliminary site wireframe can be found [here](https://www.figma.com/team_invite/redeem/IcpYMmIl2glnnjy2VihFVm). Currently, the project wireframe is incomplete, with only the core public-facing pages finished.
