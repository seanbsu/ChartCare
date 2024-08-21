# Technical Overview

## Purpose
The purpose of this document is to provide a more in-depth technical overview of the project and its designs for those interested in contributing.

## Application Design
The current goal is to have two main parts:
1. A public-facing site where companies can learn about the service and sign up.
2. A web portal for the charting application.

The public-facing site will use the .NET MVC framework and host the backend API. The web portal will use .NET Core and operate as a Single Page Application (SPA), making API calls as needed.

When a company initially signs up using the public-facing site (with the Identity framework), two actions will occur:
1. The company profile will be created and added to the `Company` table in the database.
2. A new user will be created for the company with ADMIN privileges, allowing the company to log in and begin adding employees and patients through the SPA.

## Technologies Used

### Programming Languages
The primary language for the project is C#. JavaScript may be used for front-end responsiveness, but as of now, it hasn't been implemented. HTML and CSS are used in the Razor pages.

### Database Language
The database language will be SQL Server. To view the design document, MySQL Workbench will be required; however, a design image is provided, which will need to be updated when the database changes.

### Frameworks
- **ASP.NET MVC** - Public-facing site
- **ASP.NET Core** - SPA web portal
- **Entity.Identity** - Login, Registration and Role Management

### Dependencies
- **Bootstrap** - Used in most website components
- **jQuery** - Currently unused, but available if needed

## Database Design
An initial design for the database tables can be found [here](./docs/db-design.png). A `.mwb` file is also provided to view and edit table designs using MySQL Workbench's design tools. Please note that this diagram provides a rough overview of the tables; there are additional columns that will be included when the context migration file is generated, due to the Identity framework's default columns. This design specifies the columns that will be added to the default Identity tables, with data provided from the registration forms.

Currently, there is no live database; all work will be done locally.

## Figma
A preliminary site wireframe can be found [here](https://www.figma.com/team_invite/redeem/IcpYMmIl2glnnjy2VihFVm). Currently, the project wireframe is incomplete, with only the core public-facing pages finished.
