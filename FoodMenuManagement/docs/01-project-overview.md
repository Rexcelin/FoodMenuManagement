
# Food Menu Management System

## 1. Project Overview

The Food Menu Management System is a console-based application
developed using C# and .NET 10.

The application is designed to manage food items for a small
restaurant or food service business.

It allows users to maintain food information, manage prices,
control item availability, and organize menu items by category.

## 2. Project Objectives

- Maintain restaurant food menu information.
- Add new food items.
- View all available food items.
- Retrieve individual food items.
- Update existing food information.
- Delete food items.
- Filter food items by category.
- Filter food items based on availability.
- Validate user input and prevent invalid data.

## 3. Technologies

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 10 | Application framework |
| Entity Framework Core | ORM and database access |
| SQL Server Express | Relational database |
| Git | Version control |
| GitHub | Source code hosting |

## 4. Primary Entity

### FoodItem

The application maintains the following properties:

| Property | Data Type | Description |
|---|---|---|
| Id | int | Unique food item identifier |
| Name | string | Food item name |
| Category | string | Food category |
| Description | string | Optional food description |
| Price | decimal | Food item price |
| IsAvailable | bool | Availability status |

## 5. Application Architecture

The application will follow a layered architecture.

- Models: Entity classes.
- Data: Database context and configuration.
- Repositories: Database operations.
- Services: Business logic and validation.
- Menus: Interactive console interface.
- Helper: Reusable console utilities.
- Migrations: Database schema versioning.
- docs: Project documentation.

## 6. Database Information

Database Name: FoodMenuDB

Database Provider: SQL Server Express

Main Table: FoodItems

Entity Framework Core will be used to manage
database operations and schema migrations.

## 7. Project Scope

This project focuses on food menu management.

It does not include customer ordering, payment processing,
delivery tracking, or restaurant billing.

## 8. Expected Outcome

A working console application that supports food item
CRUD operations, category filtering, availability filtering,
input validation, and database management.
