# 🚀 ASP.NET Core 8.0 - Backend Learning Path

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![VS Code](https://img.shields.io/badge/Visual%20Studio%20Code-007ACC?style=for-the-badge&logo=visual-studio-code&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)

This repository contains a structured collection of projects demonstrating my journey in mastering **ASP.NET Core 8** architecture, modern web development patterns, and database management processes.

The projects evolve from basic **MVC** principles to complex scenarios including advanced form handling, validation, file management, and a full-featured security system using **ASP.NET Core Identity**.

---

## 📂 Modules & Technical Competencies

This path consists of 4 main modules, each targeting a specific set of backend skills:

### 1️⃣ Basics & MeetingApp (MVC Fundamentals)
Introduction to the ASP.NET Core ecosystem and the Model-View-Controller (MVC) design pattern.
* **Routing:** Custom URL configurations and Controller-Action mechanisms.
* **Razor Syntax:** Dynamic C# code integration within HTML views.
* **Layouts & Sections:** Creating reusable UI templates (Master Pages) for maintainability.
* **Data Transfer:** Managing data flow using `ViewBag`, `ViewData`, and ViewModels.

### 2️⃣ FormsApp (Advanced Form Management)
Handling user interactions, data entry, and secure data processing.
* **Model Binding:** Automatic mapping of HTTP request data (QueryString, FormCollection) to objects.
* **Tag Helpers:** Utilizing `asp-for` and `asp-controller` for clean and readable HTML generation.
* **Server-Side Validation:** Implementing strict data validation using Data Annotations (`[Required]`, `[StringLength]`).
* **File Upload:** Securely handling and storing product images on the server.

### 3️⃣ EfCoreApp (Database & ORM)
Data persistence implementation using **Entity Framework Core** and SQL operations.
* **Code-First Approach:** Designing database schemas through C# Entity classes.
* **Migrations:** Managing database schema changes professionally via CLI (`add-migration`).
* **Relational Database:**
    * **One-to-Many:** (e.g., An Instructor can have multiple Courses.)
    * **Many-to-Many:** (e.g., Students can enroll in multiple Courses.)
* **LINQ:** Advanced data querying, filtering, and joining operations.

### 4️⃣ IdentityApp (Identity & Security Module)
A **secure, full-featured membership system** built with **ASP.NET Core Identity**.
* **Automated Database Seeding:** Automatic creation of default Admin user and Roles upon startup.
* **Role-Based Access Control (RBAC):** Dynamic Role management and assigning roles to users.
* **Secure Authentication:** Utilized `UserManager` and `SignInManager` for secure login/logout flows.
* **User Management:** Full CRUD operations for user profiles and details.
* **Email Services:** Integrated SMTP for **Account Confirmation** workflows.
* **Password Security:** Secure **Forgot Password & Reset** mechanism using token validation.
* **Authorization:** Implemented Controller and Action-level security policies (`[Authorize]`).

---

## 🛠️ Tech Stack

| Category | Technology / Tool |
|----------|------------------|
| **Backend** | .NET 8.0, C# 12 |
| **Framework** | ASP.NET Core MVC |
| **ORM** | Entity Framework Core |
| **Database** | SQLite / MS SQL Server LocalDB |
| **Frontend** | Bootstrap 5, HTML5, CSS3 |
| **IDE & Tools** | Visual Studio Code, Git |

---
### 👨‍💻 Geliştirici

**Mehmet Yeşil** - Bilgisayar Mühendisi

🔗 **LinkedIn:** [Mehmet Yeşil](https://www.linkedin.com/in/mehmet-yesil/)
