# 📚 Jobverse Technical Documentation

This document provides technical details for developers and contributors to the Jobverse Job Portal System.

---

##1. Project Structure

- **Controllers/** – MVC controllers for all features (Job, Account, Admin, Employer, Application, Messages, etc.)
- **Data/** – Entity Framework DbContext, configurations, seed data
- **Mappings/** – AutoMapper profiles
- **Models/**
 - **Domain/** – Core business entities (User, Employer, JobSeeker, Job, Application, etc.)
 - **DTOs/** – Data transfer objects for service/repo boundaries
 - **ViewModels/** – View-specific models for Razor Pages
- **Repositories/** – Data access layer (interfaces and implementations)
- **Services/** – Business logic layer (interfaces and implementations)
- **Views/** – Razor Pages UI (organized by feature)
- **wwwroot/** – Static assets (css, js, images, README screenshots)

---

##2. Key Technologies

- **.NET9 / ASP.NET Core Razor Pages**
- **Entity Framework Core** (SQL Server)
- **ASP.NET Core Identity** (role-based auth)
- **AutoMapper** (DTO/ViewModel mapping)
- **Bootstrap5** (UI)
- **OfficeOpenXml** (Excel export)

---

##3. Database & Seeding

- **DbContext:** `ApplicationDbContext` (see `Data/`)
- **Seeding:** `SeedData.cs` creates demo users, employers, job seekers, jobs, and categories
- **Migrations:**
 ```bash
 dotnet ef migrations add <Name>
 dotnet ef database update
 ```

---

##4. Authentication & Roles

- **Roles:** Admin, Employer, JobSeeker
- **Identity:** Custom `User` entity extends `IdentityUser`
- **Role-based navigation and page access**

---

##5. Services & Repositories

- **Repositories:** CRUD and custom queries for each entity
- **Services:** Business logic, validation, and orchestration
- **AutoMapper:** Used for mapping between domain, DTO, and ViewModel

---

##6. Features Overview

- **Job Management:** Employers can post, edit, delete jobs; Job Seekers can search and apply
- **Applications:** Track, withdraw, and manage job applications
- **Messaging:** Secure, role-based messaging system
- **Profile Management:** Update info, upload images/CVs
- **Admin:** Approve users, manage jobs, export reports

---

##7. Configuration

- **appsettings.json:**
 - `ConnectionStrings:DefaultConnection` – SQL Server connection
 - `Email` – SMTP settings for notifications

---

##8. Contribution Guidelines

- Fork the repo and create a feature branch
- Follow C# and .NET best practices
- Add/Update tests for new features
- Submit a pull request with a clear description

---

##9. Contact & Support

- For questions, open a GitHub Issue or contact the maintainer via social links in the README

---

> _For more details, see the code comments and explore the folder structure. Happy coding!_
