
# Food Menu Management System

A console-based Food Menu Management System developed using C#, .NET 10, Entity Framework Core, and SQL Server Express.

The application supports food item CRUD operations, filtering, validation, database error handling, and automatic audit logging.

## Technologies Used

- C#
- .NET 10
- Entity Framework Core 10
- SQL Server Express
- LINQ
- Dependency Injection
- Asynchronous Programming
- Git and GitHub

## Project Features

### 1. Food Item Management
- Add new food items
- View all food items
- View food item by ID
- Update food item details
- Delete food items

### 2. Filtering
- Filter food items by category
- Filter available food items
- Combine category and availability filters

### 3. Validation
- Food item name is required
- Category is required
- Price must be greater than zero
- Prevent duplicate food item names
- Validate food item IDs

### 4. Audit Logging
- Automatically record food item creation
- Automatically record food item updates
- Automatically record food item deletions
- Store old and new values for relevant operations
- Record transaction date and time
- View all audit logs
- View audit history by food item ID

### 5. Database and Error Handling
- SQL Server database integration
- Entity Framework Core migrations
- Asynchronous database operations
- Dependency Injection
- Database error handling
- Transaction-based audit logging

## Project Structure

```text
FoodMenuManagement/
│
├── Data/
│   └── AppDbContext.cs
│
├── Helper/
│   └── DatabaseErrorHandler.cs
│
├── Menus/
│   └── FoodMenu.cs
│
├── Models/
│   ├── FoodItem.cs
│   └── AuditLog.cs
│
├── Repositories/
│   ├── FoodItemRepository.cs
│   └── AuditLogRepository.cs
│
├── Services/
│   ├── FoodItemService.cs
│   └── AuditLogService.cs
│
├── Migrations/
│
├── docs/
│
├── Program.cs
├── appsettings.json
└── README.md
```

## Audit Log Details

The AuditLog table stores:

| Field | Description |
|---|---|
| AuditLogId | Unique audit record ID |
| EntityName | Name of the affected entity |
| EntityId | ID of the affected food item |
| Action | CREATE, UPDATE, or DELETE |
| Details | Description of the operation |
| OldValues | Previous food item data |
| NewValues | Updated or newly created data |
| PerformedAt | Date and time of the operation |

Audit records are saved within the same database transaction as the corresponding food item operation. If an operation fails, its audit record is rolled back as well.

## Database Setup

1. Install SQL Server Express.
2. Update the connection string in `appsettings.json`.
3. Open the project in Visual Studio.
4. Run the database migrations.

## Run the Application

```bash
dotnet restore
dotnet build
dotnet run
```

## Learning Outcomes

- Implemented CRUD operations using EF Core.
- Used the Repository and Service patterns.
- Configured Dependency Injection.
- Implemented asynchronous programming.
- Applied data validation.
- Used LINQ for filtering.
- Implemented database transactions.
- Designed an audit logging mechanism.

## Author

Developed as a C# and .NET learning project.
