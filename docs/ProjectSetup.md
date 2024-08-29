# Project Setup

## Purpose
The purpose of this document is to aid in setting up the project so it will build and run in your local development environment.

## Prerequisites
- **Visual Studio 2022** or **Visual Studio Code** version **1.92.2** or later
- **ASP.NET 8.0**
- **Microsoft SQL Server**

## SQL Server Connection
To connect to your local SQL Server instance, navigate to the `appsettings.json` file and update the connection string. Locate the line:

```json
"CompanyDbContextConnection": "Server=(localdb)\\mssqllocaldb;Database=ChartCareMVC;Trusted_Connection=True;MultipleActiveResultSets=true"
```
Replace it with your specific `Server` and `Database` values.

If you encounter certificate errors, append `TrustServerCertificate=True;` to the connection string.

**Note:** Do not commit changes to the `appsettings.json` file. These settings are local and should be reverted before committing. Ensure the settings are restored to their default values before pushing changes.



## SMTP Settings
To configure SMTP settings for email functionality, add the following configuration to your secrets.json file:
```json
  "SmtpSettings": {
    "SiteEmail": "example@example.com",
    "SmtpUsername": "username@example.com",
    "SmtpPassword": "password",
    "SmtpHost": "smtp.example.com",
    "SmtpPort": 587,
    "EnableSsl": true
  }
```
To test email functionality, sign up for an SMTP service and replace the placeholder values with those provided by the service. A recommended free SMTP service is [Brevo](https://www.brevo.com/).

**Note:** Ensure that sensitive information, such as SMTP credentials, is managed securely and is not committed to version control.