# User Management System

This is a sample User Management Web API built using ASP.NET Core and Clean Architecture principles. It allows creating, updating, retrieving, and listing users via RESTful endpoints.

## Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (In-Memory or SQL Server)
- xUnit for unit testing
- Clean Architecture
- SOLID Principles

## How to Run

1. **Clone the repo**
   ```bash
   git clone https://github.com/brandondsz/UserManagementSystem.git
   cd UserManagementSystem/UserManagementSystem

2. **Configure database**   
  - By default, the project uses InMemoryDatabase for simplicity.
  - To use SQL Server instead, update the ConnectionStrings:Default in UserManagementSystem/appsettings.json and update builder.Services.AddDbContext<UmsDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure())); in Program.cs.


3. **Run the API**
   - Open UserManagementSystem.sln in VisualStudio
   - Build and Run the project
   - The Swagger UI will launch automatically at: https://localhost:port/swagger/index.html

   

   
