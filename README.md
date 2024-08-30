# ChartCare

## About the Owner and Project Purpose
Hi everyone, my name is Sean Calkins, and I'm a recent computer science graduate from Boise State University (May 2024). I created this project as a way to learn the C# language and the ASP.NET framework. While this project may not yet meet industry standards due to my current level of experience, I'm excited to share it publicly.

## Contributing
Although this project is primarily a learning exercise for me, I warmly welcome contributions from others. Whether you're a seasoned developer or someone who, like me, is also learning, your input is valued. Feel free to suggest improvements, report issues, or even just provide feedback. Every bit of help contributes not only to this project but also to my growth as a developer. See [contributing](./docs/contributing.md) for more information on contributing guidelines.

## Project Overview
ChartCare is an online patient charting system designed to be used in the browser. The goal is to have a public-facing site where companies can learn about the product, its features, pricing, create a corporate account, and allow employees to log in to the system. Upon logging in, users will be directed to the web portal for patient charting, where depending on their role in the company, they will be able to:
- Add new patients to the company's list
- Create a personalized list for their caseload
- View patient profile information (picture, name, address, DOB, etc.)
- View and create daily reports and medical/psychological reports
- Create new employee accounts to grant access to the system

For a more in-depth technical overview of the project, see the [Technical Overview](./docs/TechnicalOverview.md) document.

## Project Setup
Setting up ChartCare is straightforward, with just a few steps required to get the application running in your local development environment. To get started, you'll need to:
- Update the connection string in *appsettings.json* to connect to your local SQL Server instance.
- Configure the SMTP settings in *secrets.json*.

For a more in-depth technical overview of the stup poccess, see the [Project Setup](./docs/ProjectSetup.md) document.