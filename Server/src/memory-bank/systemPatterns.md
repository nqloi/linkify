# System Patterns: Linkify

## Architecture Overview

### Clean Architecture
The system follows clean architecture principles with four main layers:

1. API Layer (Linkify.Api)
   - REST endpoints
   - Controller logic
   - DTO mappings
   - Middleware configurations

2. Application Layer (Linkify.Application)
   - Use case implementations
   - Business logic
   - Interface definitions
   - Data mapping configurations

3. Domain Layer (Linkify.Domain)
   - Core business entities
   - Domain logic
   - Interfaces
   - Business rules

4. Infrastructure Layer (Linkify.Infrastructure)
   - Data persistence
   - External service integrations
   - Security implementations
   - Real-time communication

## Design Patterns

### Repository Pattern
- IBaseCommandRepository and IBaseQueryRepository for data access
- Specialized repositories (IPostRepository, ITokenRepository)
- Unit of Work pattern for transaction management

### CQRS Pattern
- Separate command and query handlers
- BaseCommandHandler and BaseQueryHandler implementations
- Clear separation of read and write operations

### Aggregate Pattern
- Domain entities organized into aggregates
- Each aggregate maintains its invariants
- Key aggregates:
  - UserProfile
  - Post
  - Friendship
  - Message
  - Notification

### Specification Pattern
- BaseSpecification for reusable query criteria
- Specialized specifications for Posts and Comments
- Supports complex querying requirements

## Key Implementation Paths

### Authentication Flow
1. User submits credentials
2. AuthenticationController processes request
3. IdentityService validates credentials
4. TokenService generates JWT
5. Response includes authentication token

### Post Creation Flow
1. Client submits post data
2. PostsController validates request
3. Post aggregate created/updated
4. File attachments processed if present
5. Notifications dispatched if needed
6. Changes committed via Unit of Work

### Real-time Notification Flow
1. Action triggers notification
2. NotificationService creates notification
3. SignalR hub notifies relevant users
4. Clients receive real-time updates

## Component Relationships

### Controller Dependencies
- Base API Controller provides common functionality
- Controllers depend on Application layer interfaces
- DTOs handle data transfer with clients

### Service Interactions
- File Service for media handling
- Identity Service for authentication
- Notification Service for real-time updates
- Chat Service for messaging

### Data Flow
1. API Controllers accept requests
2. Application layer processes business logic
3. Domain layer enforces business rules
4. Infrastructure layer handles persistence
5. Real-time updates via SignalR when needed

## Critical Paths
1. User Authentication
2. Post Management
3. Friend Relationships
4. Real-time Notifications
5. Chat Messaging
6. File Operations
