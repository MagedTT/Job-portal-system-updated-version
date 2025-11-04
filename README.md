# Jobverse – Job Portal System

A modern, full-featured job portal built with ASP.NET Core Razor Pages and .NET9. Jobverse connects employers and job seekers with a seamless, intuitive experience for posting jobs, applying, messaging, and managing careers.

---

## ?? Project Overview

Jobverse is a professional job portal system designed for real-world use. It supports multiple user roles (Admin, Employer, Job Seeker), advanced job search, application management, messaging, and robust admin reporting. The system is built with scalability, security, and user experience in mind.

---

## ??? Tech Stack

- **Backend:** .NET9, ASP.NET Core Razor Pages
- **Frontend:** Razor Pages, Bootstrap5, custom CSS
- **Database:** SQL Server, Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Mapping:** AutoMapper
- **Email:** SMTP (SendGrid-ready)
- **Excel Export:** OfficeOpenXml
- **Other:** Dependency Injection, Clean Architecture

---

## ??? Architecture

- Models, DTOs, ViewModels
- **Layered Structure:**
 - Controllers (UI logic)
 - Services (business logic)
 - Repositories (data access)
 - Data (DbContext, migrations, seeding)
- **Identity Integration:** Role-based access (Admin, Employer, Job Seeker)
- **AutoMapper:** For DTO/ViewModel mapping
- **Custom Attributes:** For authentication/authorization

---

## ? Features

- User registration & login (Job Seeker, Employer, Admin)
- Job posting, editing, and management (Employers)
- Job search and application (Job Seekers)
- Application tracking and withdrawal
- Messaging system (Job Seekers & Employers)
- Profile management (all roles)
- Admin dashboard: user/job management, reports, Excel export
- Responsive, modern UI with Bootstrap5
- Theming and accessibility support

---

## ?? Folder Structure

```
??? Controllers/ # MVC controllers for all features
??? Data/ # DbContext, configurations, seed data
??? Mappings/ # AutoMapper profiles
??? Models/ # Domain, DTOs, ViewModels
??? Repositories/ # Data access layer
??? Services/ # Business logic layer
??? Views/ # Razor Pages UI
??? wwwroot/ # Static assets (css, js, images)
??? appsettings.json # Configuration
??? Program.cs # App entry point
```

---

## ?? How to Run the Project

1. **Clone the repository:**
 ```bash
 git clone https://github.com/your-username/job-portal-system.git
 cd job-portal-system
 ```
2. **Configure the database:**
 - Update `appsettings.json` with your SQL Server connection string.
3. **Apply migrations & seed data:**
 ```bash
 dotnet ef database update
 ```
4. **Run the application:**
 ```bash
 dotnet run
 ```
5. **Access in browser:**
 - Navigate to `https://localhost:5001` (or the port shown in console)

---

## ??? Screenshots

| Home Page | Admin Dashboard | Employer Dashboard |
|-----------|----------------|-------------------|
| ![Home](wwwroot/README-assets/screenshot-home.png) | ![Admin](wwwroot/README-assets/screenshot-admin-dashboard.png) | ![Employer](wwwroot/README-assets/screenshot-employer-dashboard.png) |

| Job Details | Jobseeker Profile | Application List |
|-------------|-------------------|-----------------|
| ![Job Details](wwwroot/README-assets/screenshot-job-details.png) | ![Profile](wwwroot/README-assets/screenshot-jobseeker-profile.png) | ![Applications](wwwroot/README-assets/screenshot-application-list.png) |

| Messages | Register | Login | Admin Reports |
|----------|----------|-------|---------------|
| ![Messages](wwwroot/README-assets/screenshot-messages.png) | ![Register](wwwroot/README-assets/screenshot-register.png) | ![Login](wwwroot/README-assets/screenshot-login.png) | ![Reports](wwwroot/README-assets/screenshot-admin-reports.png) |

---

## ?? Social Links

- [LinkedIn](https://www.linkedin.com/in/magedt/)
---

> _Proudly built with .NET9, Razor Pages, and a passion for connecting talent with opportunity._

