# Feedback Service API and Feedback Management System

This repository contains the source code for a comment service API built with ASP.NET Core and MediatR to handle commands and queries. The API allows users to manage comment entries and categories associated with them. And a Feedback Management System project created with ASP.NET MVC 5 that provides a web page to manage all feedbacks, including a main web page that returns all feedbacks from the last month categorized by category name and each feedback can be edited or deleted and have an extra button is needed to create a new feedback.

## Setup Instructions

### Prerequisites
- SQL Server Management Studio or any other SQL database management tool.
- .NET SDK installed on your machine.

### Database Setup
1. Open SQL Server Management Studio.
2. Execute the SQL files provided in the solution to create the necessary database schema:
   - `FeedbackSystem.sql`: Create the database, stored procedures and 3 categories.
3. Make sure to note down the connection string for the created database as it will be required for configuring the API.

### Running the Projects
1. **MVC 5 Project:** **ASP.NET Core API:**
   - Open the Start Dropdown box.
   - Select Configure Startup Projects.
   - Change the action for both Projects `FeedbackManagementSystem` and `Services/Feedback.Service.FeedbackService`.

## API Endpoints

### Category Controller
- **GET /api/category**
  - Description: Retrieve all categories.
  - Response:
    - 200 OK: Returns the list of categories.
    - Other Status Codes: Returns an error message if the request fails.

### Feedback Controller
- **GET /api/feedback**
  - Description: Retrieve feedback entries from the last month.
  - Response:
    - 200 OK: Returns the feedback entries from the last month.
    - Other Status Codes: Returns an error message if the request fails.

- **GET /api/feedback/{id}**
  - Description: Retrieve a specific feedback entry by its ID.
  - Parameters:
    - id: The ID of the feedback entry.
  - Response:
    - 200 OK: Returns the feedback entry if found.
    - 404 Not Found: If the feedback entry with the specified ID does not exist.
    - Other Status Codes: Returns an error message if the request fails.

- **POST /api/feedback**
  - Description: Add a new feedback entry.
  - Request Body: JSON object representing the feedback entry.
  - Response:
    - 200 OK: Returns the added feedback entry.
    - Other Status Codes: Returns an error message if the request fails.

- **PUT /api/feedback**
  - Description: Update an existing feedback entry.
  - Request Body: JSON object representing the updated feedback entry.
  - Response:
    - 200 OK: Returns the updated feedback entry.
    - Other Status Codes: Returns an error message if the request fails.

- **DELETE /api/feedback/{id}**
  - Description: Delete a feedback entry by its ID.
  - Parameters:
    - id: The ID of the feedback entry to delete.
  - Response:
    - 200 OK: Returns success message.
    - 404 Not Found: If the feedback entry with the specified ID does not exist.
    - Other Status Codes: Returns an error message if the request fails.

## Dependencies
- **MediatR**: A simple, unambitious mediator implementation in .NET.
- **Microsoft.AspNetCore.Mvc**: Provides support for building web APIs.
- **Microsoft.EntityFrameworkCore**: Entity Framework Core is a lightweight, extensible, and cross-platform version of the popular Entity Framework data access technology.
- **Microsoft.EntityFrameworkCore.SqlServer**: EF Core SQL Server database provider allows EF Core to be used with Microsoft SQL Server.
- **Microsoft.EntityFrameworkCore.Design**: Provides design-time support for Entity Framework Core tools.
- **FluentValidation**: A popular .NET library for building strongly-typed validation rules.

## Design Patterns and Principles Used
- **CQRS**: To make a more orderly separation, thinking about the scalability, flexibility and optimization that this pattern offers us.
- **Unit Of Work**: To maintain consistency and transactional integrity across multiple database operations.
- **Abstraction**: Fundamental principle of software design that promotes modularity, maintainability, and testability.
- **Repository**: To abstract the data access logic from the rest of the application. It defines a set of methods for performing CRUD (Create, Read, Update, Delete) operations on data entities without exposing the underlying data access mechanisms.
