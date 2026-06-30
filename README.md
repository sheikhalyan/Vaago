# 🍽️ Vaago — Restaurant Management System

### A digital ordering and table management system built on ASP.NET MVC with applied software design patterns

[![C#](https://img.shields.io/badge/C%23-ASP.NET_MVC-512BD4?style=flat-square&logo=csharp&logoColor=white)](https://dotnet.microsoft.com)
[![SQL Server](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![JavaScript](https://img.shields.io/badge/JavaScript-ES6-F7DF1E?style=flat-square&logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
[![Design Patterns](https://img.shields.io/badge/Design_Patterns-6_Applied-blueviolet?style=flat-square)](#design-patterns)

> A university project built during BS Computer Science at PAF-KIET, replacing traditional paper-based restaurant menus with a digital ordering and management system — engineered with six classical software design patterns following IEEE Std 830-1998 SRS guidelines.

---

## What It Does

Vaago digitizes the restaurant ordering experience, replacing static paper menus with an interactive system for both customers and restaurant staff.

**Customer side:**
- Browse the digital menu
- Add items to cart and check out
- Reserve a table
- View order history and profile
- Apply to become a chef
- View gallery, contact, and about pages

**Admin side:**
- Manage customers, menu, orders, reservations, and chef applications
- Full dashboard with role-based views
- Settings and configuration management

---

## Design Patterns

This project was built specifically to apply core Gang of Four design patterns to real controller logic — not just as theory, but as working implementations solving actual problems in the system.

| Pattern | Applied On | Purpose |
|---------|------------|---------|
| **Factory Pattern** | Admin → Customer Controller | Centralizes and abstracts the creation of customer-related objects, decoupling instantiation logic from the controller |
| **Observer Pattern** | Admin → Order Controller | Notifies dependent components (e.g. dashboard, order status views) automatically when an order's state changes |
| **Command Pattern** | Checkout Controller | Encapsulates checkout actions as command objects, enabling clean execution, validation, and potential undo/redo of checkout steps |
| **Repository Pattern** | Menu Controller | Abstracts data access logic for menu items, decoupling business logic from direct database/Entity Framework calls |
| **Strategy Pattern** | Cart Controller | Allows interchangeable algorithms for cart operations (e.g. pricing/discount strategies) without changing the controller's core logic |
| **Singleton Pattern** | Authentication Controller | Ensures a single, shared authentication context/instance is used across the application, preventing redundant session state |

---

## Tech Stack

| Layer | Technology |
|-------|------------|
| **Backend** | C#, ASP.NET MVC |
| **ORM** | Entity Framework (Model1.edmx) |
| **Database** | SQL Server |
| **Dependency Injection** | Unity (UnityConfig) |
| **Frontend** | Razor Views, JavaScript, CSS |
| **Architecture** | MVC + Repository + Dependency Injection |
| **API Layer** | ASP.NET Web API (WebApiConfig) |

---

## Project Structure

```
Vaago/
│
├── Vaago.csproj
├── Web.config
├── Global.asax
├── VaagoScripts.sql                    # Database schema & seed scripts
│
├── App_Start/
│   ├── BundleConfig.cs                 # CSS/JS bundling
│   ├── FilterConfig.cs                 # Global action filters
│   ├── RouteConfig.cs                  # URL routing configuration
│   ├── UnityConfig.cs                  # Dependency injection setup
│   └── WebApiConfig.cs                 # Web API routing
│
├── Controllers/
│   ├── AboutUsController.cs
│   ├── AuthenticationController.cs     # 🔒 Singleton Pattern
│   ├── CartController.cs               # 🎯 Strategy Pattern
│   ├── CheckoutController.cs           # 📦 Command Pattern
│   ├── ChefapplyController.cs
│   ├── ChefreqController.cs
│   ├── ContactController.cs
│   ├── GalleryController.cs
│   ├── HomeController.cs
│   ├── MenuController.cs               # 🗄️ Repository Pattern
│   ├── ReservationsController.cs
│   ├── UOrderHistoryController.cs
│   ├── UProfileController.cs
│   │
│   └── Admin/
│       ├── AdminController.cs
│       ├── DashboardController.cs
│       ├── AdminCustomersController.cs # 🏭 Factory Pattern
│       ├── AdminMenuController.cs
│       ├── AdminOrdersController.cs    # 👁️ Observer Pattern
│       ├── AdminReservationController.cs
│       ├── AdminChefController.cs
│       ├── ChefsController.cs
│       └── SettingController.cs
│
├── Models/
│   ├── Account.cs
│   ├── Cart.cs
│   ├── CartItem.cs
│   ├── CheckoutModel.cs
│   ├── Chef.cs
│   ├── ChefViewModel.cs
│   ├── Login.cs
│   ├── Menu.cs
│   ├── Order.cs
│   ├── Order_Details.cs
│   ├── Reservation.cs
│   ├── Role.cs
│   ├── SiteMessage.cs
│   ├── ViewModel.cs
│   └── Model1.edmx                     # Entity Framework data model
│
├── Views/
│   ├── AboutUs/
│   ├── Admin/
│   ├── AdminChef/
│   ├── AdminCustomers/
│   ├── AdminReservation/
│   ├── Authentication/
│   ├── Cart/
│   ├── Checkout/
│   ├── Chefapply/
│   ├── Contact/
│   ├── Gallery/
│   └── ...
│
├── Content/                            # CSS / SCSS stylesheets
├── Scripts/                            # Client-side JavaScript
├── images/                             # Static assets
└── Properties/
    └── AssemblyInfo.cs
```

---

## System Roles

| Role | Capabilities |
|------|--------------|
| **Customer** | Browse menu, manage cart, checkout, reserve tables, view order history, apply as chef |
| **Admin** | Manage customers, menu, orders, reservations, and chef applications via dashboard |
| **Chef** *(via application flow)* | Apply through `ChefapplyController`, reviewed via `AdminChefController` |

---

## Getting Started

### Prerequisites
- Visual Studio 2019+
- .NET Framework
- SQL Server (LocalDB or full instance)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/sheikhalyan/Vaago.git
   ```
2. Open `Vaago.sln` in Visual Studio
3. Restore NuGet packages
4. Update the connection string in `Web.config` to point to your SQL Server instance
5. Run `VaagoScripts.sql` against your database to create schema and seed data
6. Build and run (F5)

---

## Key Design Considerations

From the original system design (IEEE Std 830-1998 compliant SRS):

- **Reliability** — system must return correct order and billing results regardless of order volume
- **Security** — encrypted login authentication for admin access, enforced via the Singleton-based authentication context
- **Maintainability** — modular MVC architecture with design patterns isolating change — e.g. swapping a pricing algorithm only touches the Strategy implementation, not the Cart controller itself
- **Usability** — interface designed to be usable by walk-in customers with no prior training

---

## Documentation

Full system design and requirements are documented in the project's **Software Requirements Specification** (IEEE Std 830-1998), including use case diagrams, functional/non-functional requirements, and user characteristic analysis.

---

## Context

Built as a university project for the **Object-Oriented Analysis and Design (OOAD)** course at PAF-KIET, applying formal requirements engineering and six core design patterns to a real-world restaurant management system.



**Submitted to:** Dr. Muhammad Fahad

---

## Author

**Sheikh Alyan** — BS Computer Science, PAF-KIET


[![GitHub](https://img.shields.io/badge/GitHub-@sheikhalyan-181717?style=flat-square&logo=github)](https://github.com/sheikhalyan)
