# E-commerce Web API

A modular and scalable .NET Core-based Web API project for a complete E-commerce system, built using Domain-Driven Design (DDD) principles.

## 🧱 Project Structure

The solution is organized into clear layers following clean architecture:

### 1. **Core**
Contains domain models, interfaces, and business logic.

- **Entities**: Definitions for `Product`, `Order`, `User`, etc.
- **Contracts**: Interfaces for repositories and services (`IGenericRepository`, `IUnitOfWork`, etc.)
- **Specifications**: Custom query specifications.
- **Exceptions**: Custom exception handling logic.

### 2. **Infrastructure**
Implements the abstractions from the Core layer.

- **Persistence**: 
  - Entity Framework DbContexts and configurations
  - Seeding sample data from JSON files
- **Repositories**: Implements data access logic.
- **Presentation**: Controllers for handling API endpoints.

### 3. **Shared**
Contains common DTOs, error models, and helper classes.

- **DTOs**: Models for transferring data (`ProductResultDto`, `OrderItemDto`, etc.)
- **ErrorModels**: Standardized error responses.
- **JWT & Pagination**: Token configuration and pagination helpers.

### 4. **Web API**
Entry point of the application.

- **Extensions**: Dependency injection setup for services.
- **Middlewares**: Global error handling.
- **Controllers**: Top-level routing and request processing.
- **Factories**: Standardized API responses.

---

## 🚀 Features

- Clean architecture with DDD
- Entity Framework Core with code-first migrations
- Repository & Unit of Work patterns
- AutoMapper and DTOs for clean API responses
- JWT-based Authentication
- Role-based Authorization
- Redis caching support
- Global error handling middleware

---

## 📦 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-repo.git
