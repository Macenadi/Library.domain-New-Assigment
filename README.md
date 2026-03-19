# 📚 Library Management System

ASP.NET Core MVC application for managing a community library.

---

## 🚀 Features

- User authentication (Register / Login)
- Role-based access (Admin)
- Manage Books (Create, Edit, Delete, Details)
- Manage Members (Create, Edit, Delete, Details)
- Manage Loans (Borrow / Return books)
- Book availability tracking
- Overdue loan detection
- Book filtering (search, category, availability)

---

## 🧪 Testing

- Unit tests implemented using **xUnit**
- In-memory database for testing (EF Core InMemory)
- Controllers tested:
  - BooksController
  - LoansController
  - MembersController
- Business logic tested:
  - Loan creation
  - Book availability updates
  - Loan return logic

---

## ⚙️ Technologies

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- SQL Server / LocalDB
- Bootstrap (UI)
- xUnit (Testing)
- GitHub Actions (CI/CD)

---

## ▶️ How to Run

1. Clone the repository  
2. Open the solution in Visual Studio  
3. Run the migration command:

```powershell
Update-Database
