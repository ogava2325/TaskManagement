# Task Management System

A task management application built with ASP.NET Core, CQRS pattern, and Clean Architecture principles. The application provides user-specific task management, JWT authentication stored in cookies, and ensures that users can only manage their own tasks.

## Features

- **User Registration and Authentication:** Users can register, log in, and manage their tasks securely using JWT tokens stored in cookies.
- **Task Management:** Users can create, update, delete, and view their own tasks.
- **CQRS Pattern:** Separation of commands (create, update, delete) and queries (read tasks) for better maintainability.
- **Clean Architecture:** Ensures the project is scalable and maintainable by following clean architecture principles.
- **JWT Authorization:** Secured endpoints where users can only modify their own tasks.

## Project Structure

This project is organized following clean architecture principles with the following layers:

- **Api**: This layer contains the Web API controllers and exposes the endpoints for the client. It also handles HTTP requests, responses, and middleware configurations like authentication.
  
- **Application**: Handles the core business logic, implementing the **CQRS** pattern (Command and Query Responsibility Segregation), request validation using **FluentValidation**, and defining use cases.
  
- **Domain**: Contains the core entities, enums, exceptions, and interfaces. This layer is independent of other layers, focusing on business rules and domain logic.
  
- **Infrastructure**: Handles third-party integrations, such as external services, file systems, or email services. It may also handle logging and other cross-cutting concerns.

- **Persistence**: Responsible for database access and implementing repositories using **Entity Framework Core**. It manages migrations and database-related concerns.

Each layer is designed to be independent and decoupled from others, making the system easier to maintain, test, and scale.


- **Commands** are responsible for handling operations that modify the state of the system (e.g., creating, updating, or deleting tasks).
- **Queries** are used for retrieving data (e.g., fetching the list of tasks for a specific user).
- Each command and query is handled by a separate handler class, making the code more modular and easier to test.

## Technologies Used

- **ASP.NET Core 8**
- **CQRS Pattern**
- **Entity Framework Core**
- **JWT Authentication**
- **FluentValidation** for request validation
- **Clean Architecture**

## Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any other supported database

### Steps

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/task-management-system.git
    cd task-management-system
    ```

2. Set up the database connection in `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Your-SQL-Server-Connection-String"
      },
      "JwtOptions": {
        "Issuer": "myIssuer",
        "Audience": "myAudience",
        "SecretKey": "mySecretKey",
        "ExpiresHours": "myExpiresHours"
      }
    }
    ```

    Make sure to replace `myIssuer`, `myAudience`, `mySecretKey`, and `myExpiresHours` with appropriate values for your environment.

3. Apply migrations to set up the database:
    ```bash
    dotnet ef database update
    ```

4. Run the application:
    ```bash
    dotnet run
    ```

### JWT Configuration

- **Issuer:** The entity issuing the JWT (e.g., your app or organization).
- **Audience:** Who the JWT is intended for (e.g., the API users).
- **SecretKey:** A strong key used to sign the JWT (ensure this is stored securely).
- **ExpiresHours:** The duration in hours that the token remains valid.

### Testing the API

- Register a new user and log in to get the JWT token.
- Use the JWT token in cookies for all subsequent requests to manage tasks.

## API Endpoints

### Authentication

- `POST /api/users/register`: Register a new user.
- `POST /api/users/login`: Authenticate a user and receive a JWT.

### Tasks (Requires Authorization)

- `GET /api/tasks`: Get all tasks for the authenticated user.
- `GET /api/tasks/{id:guid}`: Get details of a specific task by its ID (only if it belongs to the user).
- `POST /api/tasks`: Create a new task (the task is associated with the authenticated user).
- `PUT /api/tasks/{id:guid}`: Update an existing task by its ID (only if it belongs to the user).
- `DELETE /api/tasks/{id:guid}`: Delete a task by its ID (only if it belongs to the user).

## Planned Features

- **Task Filtering:** Allow filtering of tasks based on status, due date, and priority. ✅
- **Sorting:** Implement sorting options for tasks based on due date and priority. ✅
- **Pagination:** Add pagination for the `GET /tasks` endpoint to support large datasets. ✅
- **Unit Testing:** Implement unit tests for key components such as the service layer and repository layer.
- **CI/CD:** Set up a basic CI/CD pipeline (e.g., using GitHub Actions) to automate build and testing.
- **Docker:** Add Docker support to containerize the application for easier deployment.
