# Architecture Overview

This project follows the principles of Domain-Driven Design (DDD) to model the core business logic of fleet and driver management. The architecture is layered to ensure separation of concerns, scalability, and maintainability.

## Domain-Driven Design (DDD) Architecture

### Core Principles

**Domain-Driven Design** is an approach to software development that emphasizes collaboration between technical and domain experts to model software based on the real-world domain. The key principles include:

- **Ubiquitous Language**: A shared language between developers and domain experts that is used consistently throughout the project
- **Bounded Contexts**: Clear boundaries around different subsystems with their own models and language
- **Domain Model**: Rich object models that encapsulate business logic and rules
- **Context Mapping**: Explicit relationships between different bounded contexts

### Layered Architecture

The system follows a strict layered architecture pattern:

```
┌─────────────────────────────────────────┐
│              User Interface             │
│         (Controllers/Views)            │
├─────────────────────────────────────────┤
│           Application Layer            │
│      (Use Cases/Application          │
│           Services)                  │
├─────────────────────────────────────────┤
│           Domain Layer               │
│      (Entities/Value Objects/        │
│     Domain Services/Domain Events)   │
├─────────────────────────────────────────┤
│         Infrastructure Layer         │
│    (Persistence/External Services)   │
└─────────────────────────────────────────┘
```

### 1. User Interface Layer
- **Responsibility**: Handles HTTP requests, renders views, and delegates to application layer
- **Components**: Controllers, DTOs, View Models
- **DDD Role**: Anti-corruption layer that translates between external requests and internal models

### 2. Application Layer
- **Responsibility**: Orchestrates domain objects to fulfill use cases
- **Components**: Application Services, Use Case Handlers, DTOs
- **DDD Patterns**:
  - **Application Services**: Coordinate between domain objects and infrastructure
  - **Use Cases**: Specific business operations (e.g., AssignDriverToVehicle, ScheduleMaintenance)
  - **DTOs**: Data transfer objects for crossing boundaries

### 3. Domain Layer (Core)
- **Responsibility**: Contains all business logic and domain rules
- **Components**:
  - **Entities**: Objects with identity (Driver, Vehicle, Fleet, Cargo)
  - **Value Objects**: Objects without identity (LicensePlate, GPSCoordinates, Money)
  - **Domain Services**: Domain logic that doesn't naturally fit in entities
  - **Domain Events**: Important business events (DriverAssigned, CargoDelivered)
  - **Aggregates**: Consistency boundaries around related objects
  - **Repositories**: Interfaces for persistence (not implementation)

#### Key Domain Concepts

**Aggregates**:
- **Fleet Aggregate**: Root - Fleet entity, contains Vehicles and Drivers
- **Driver Aggregate**: Root - Driver entity, contains License, Certifications
- **Cargo Aggregate**: Root - Cargo entity, contains Route, Status updates
- **Vehicle Aggregate**: Root - Vehicle entity, contains Maintenance records

**Value Objects**:
- **LicensePlate**: Immutable value object with validation rules
- **GPSCoordinates**: Latitude/Longitude with domain validation
- **Money**: Currency and amount with business rules
- **TimeSlot**: Start/end times with business hour validation

**Domain Events**:
- `DriverRegistered`: When a new driver joins the system
- `VehicleAddedToFleet`: When fleet acquires new vehicle
- `CargoAssigned`: When cargo is assigned to driver/vehicle
- `DeliveryCompleted`: When cargo reaches destination
- `MaintenanceScheduled`: When vehicle maintenance is scheduled

### 4. Infrastructure Layer
- **Responsibility**: Technical concerns and external system integration
- **Components**:
  - **Repository Implementations**: Database persistence using ORM
  - **External Services**: GPS tracking, payment processing, notifications
  - **Cross-cutting Concerns**: Logging, caching, security

### Bounded Contexts

The system is divided into several bounded contexts:

#### 1. Fleet Management Context
- **Core Domain**: Managing fleet composition and vehicle lifecycle
- **Key Entities**: Fleet, Vehicle, MaintenanceRecord
- **Ubiquitous Language**: Fleet composition, vehicle status, maintenance schedules

#### 2. Driver Management Context
- **Core Domain**: Driver lifecycle and certification management
- **Key Entities**: Driver, License, Certification, Availability
- **Ubiquitous Language**: Driver onboarding, certification tracking, availability

#### 3. Cargo Operations Context
- **Core Domain**: Cargo lifecycle from booking to delivery
- **Key Entities**: Cargo, Route, Delivery, Customer
- **Ubiquitous Language**: Cargo booking, route optimization, delivery confirmation

#### 4. Financial Context
- **Supporting Domain**: Billing, payments, and financial transactions
- **Key Entities**: Invoice, Payment, Transaction
- **Ubiquitous Language**: Billing cycles, payment processing, financial reporting

### Context Mapping

Relationships between bounded contexts:

```
┌─────────────────┐     ┌─────────────────┐
│ Fleet Management │────▶│ Driver Management│
│    Context      │     │    Context      │
└─────────────────┘     └─────────────────┘
         ▲                       ▲
         │                       │
         └───────────┬───────────┘
                     │
         ┌─────────────────┐
         │ Cargo Operations│
         │    Context      │
         └─────────────────┘
                     │
                     ▼
         ┌─────────────────┐
         │   Financial     │
         │    Context      │
         └─────────────────┘
```

### Tactical Design Patterns

#### Repositories
```csharp
public interface IDriverRepository
{
    Task<Driver> GetByIdAsync(DriverId id);
    Task AddAsync(Driver driver);
    Task UpdateAsync(Driver driver);
    Task<IReadOnlyList<Driver>> FindAvailableAsync();
}
```

#### Specifications
```csharp
public class AvailableDriversSpecification : Specification<Driver>
{
    public AvailableDriversSpecification()
    {
        AddCriteria(driver => driver.IsAvailable && driver.IsCertified);
    }
}
```

#### Domain Services
```csharp
public class DriverAssignmentService
{
    public async Task<AssignmentResult> AssignDriverToVehicle(
        DriverId driverId, 
        VehicleId vehicleId)
    {
        // Business logic for assignment
        // Validates driver availability, vehicle capacity, etc.
    }
}
```

### Strategic Design Patterns

#### Context Integration
- **Published Language**: Shared kernel for common concepts (IDs, basic types)
- **Customer/Supplier**: Fleet context supplies vehicle data to Cargo context
- **Conformist**: Financial context conforms to Cargo context's event structure

#### Event-Driven Architecture
- **Domain Events**: Decouple bounded contexts through events
- **Event Store**: Append-only log of all domain events
- **Eventual Consistency**: Acceptable delays between context synchronization

### Anti-Corruption Layers

Each bounded context has anti-corruption layers to prevent external models from corrupting the domain model:

- **DTOs**: Translate between external API models and domain entities
- **Facades**: Simplify complex domain interactions for external systems
- **Translators**: Convert between different model representations

### Testing Strategy

#### Unit Tests
- **Domain Layer**: Test entities, value objects, and domain services in isolation
- **Test Data Builders**: Create test objects with sensible defaults

#### Integration Tests
- **Repository Tests**: Test persistence layer integration
- **Application Service Tests**: Test use case orchestration

#### Acceptance Tests
- **BDD Scenarios**: Business-readable scenarios using Gherkin syntax
- **Domain Expert Involvement**: Tests reviewed by domain experts

### Benefits of DDD in This System

1. **Business Alignment**: Models reflect actual business operations
2. **Maintainability**: Clear separation of concerns enables independent evolution
3. **Scalability**: Bounded contexts can be developed and deployed independently
4. **Testability**: Rich domain models are easily testable in isolation
5. **Flexibility**: New business rules can be added without affecting existing code

### Technology Mapping

- **Domain Model**: Plain C# classes with rich behavior
- **Repositories**: Entity Framework Core for persistence
- **Application Services**: MediatR handlers for CQRS
- **Events**: MassTransit for event publishing
- **API**: ASP.NET Core controllers with clean architecture

This architecture ensures that the complex business rules of fleet and cargo management are properly captured in code while maintaining flexibility for future business changes.
