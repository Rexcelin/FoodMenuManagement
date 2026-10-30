# Food Menu Management System

A console-based Food Menu Management System developed using C#, .NET 10, Entity Framework Core, and SQL Server Express.

## Features

- Add new food items.
- View all food items.
- Search food items by ID.
- Update existing food items.
- Delete food items.
- Filter food items by category.
- Display only available food items.
- Combine category and availability filters.
- Validate required fields and positive prices.
- Prevent duplicate food item names.
- Handle database errors.
- Use asynchronous database operations.
- Dependency Injection for application services.

## Technologies Used

- C#
- .NET 10
- Entity Framework Core
- SQL Server Express
- LINQ
- Dependency Injection
- Git and GitHub

## Project Structure

```text
FoodMenuManagement/
│
├── Data/
│   └── AppDbContext.cs
│
├── docs/
│   └── 01-project-overview.md
│
├── Helper/
│   └── DatabaseErrorHandler.cs
│
├── Menus/
│   └── FoodMenu.cs
│
├── Migrations/
│
├── Models/
│   └── FoodItem.cs
│
├── Repositories/
│   └── FoodItemRepository.cs
│
├── Services/
│   └── FoodItemService.cs
│
├── Program.cs
├── appsettings.json
└── README.md
```

## Food Item Properties

| Property | Data Type | Description |
|---|---|---|
| Id | int | Unique food item ID |
| Name | string | Food item name |
| Category | string | Food category |
| Description | string? | Optional description |
| Price | decimal | Food item price |
| IsAvailable | bool | Availability status |

## Database Configuration

This project uses SQL Server Express.

Update the `DefaultConnection` value in `appsettings.json` to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=FoodMenuDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## Installation and Setup

### 1. Clone the repository

```bash
git clone YOUR_GITHUB_REPOSITORY_URL
```

### 2. Open the project folder

```bash
cd FoodMenuManagement
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Apply database migrations

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

## Application Menu

1. Add Food Item
2. View All Food Items
3. View Food Item By ID
4. Update Food Item
5. Delete Food Item
6. Filter Food Items
7. Exit

## Architecture

The application follows a layered architecture:

- **Models:** Defines the FoodItem entity.
- **Data:** Contains the Entity Framework Core database context.
- **Repositories:** Handles database CRUD and filtering operations.
- **Services:** Contains business logic and validation.
- **Menus:** Provides the interactive console interface.
- **Helper:** Provides database error-handling utilities.

## Learning Objectives

This project demonstrates practical usage of:

- Entity Framework Core
- SQL Server database integration
- CRUD operations
- LINQ filtering
- Asynchronous programming
- Dependency Injection
- Repository and Service patterns
- Input validation
- Exception handling

## Author

Developed as a C# and .NET learning project.