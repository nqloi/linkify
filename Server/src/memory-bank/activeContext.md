# Active Context: Linkify

## Current Focus
The project is in active development with the following areas of focus:

### Core Features
1. User Authentication and Profiles
   - Registration and login functionality
   - Profile management
   - Username support

2. Social Interactions
   - Post creation and management
   - Comment system
   - Reaction system
   - Friendship management

3. Real-time Features
   - Notification system
   - Chat functionality
   - Live updates

## Recent Changes
Based on migration history:

1. User Profile Updates (March 15, 2025)
   - Username field addition
   - Profile configurations
   - Configuration updates

2. Notification System (March 5, 2025)
   - Notification entity
   - Real-time notifications setup

3. Friendship System (March 4, 2025)
   - Friendship management
   - Relationship tracking

4. Reaction System (February 23, 2025)
   - Reaction entity
   - Post reactions support

5. Comment System (February 15, 2025)
   - Comment configurations
   - Threading support

## Active Decisions

### Architecture
- Clean architecture implementation
- CQRS pattern for data operations
- Domain-driven design principles
- Repository pattern with Unit of Work

### Data Management
- Code-first Entity Framework Core
- SQL Server database
- Migration-based schema evolution
- Cloudinary for file storage

### Authentication
- JWT-based authentication
- Custom token management
- Secure password handling

## Project Insights

### Key Patterns
- Aggregate-based domain modeling
- Specification pattern for queries
- CQRS for command/query separation
- Real-time updates via SignalR

### Important Conventions
1. API Layer
   - Controller-based routing
   - DTO-based data transfer
   - Global exception handling

2. Application Layer
   - Feature-based organization
   - Interface-driven design
   - AutoMapper for object mapping

3. Domain Layer
   - Rich domain models
   - Aggregate roots
   - Domain events

4. Infrastructure Layer
   - Repository implementations
   - External service integrations
   - Real-time communication setup

## Current Considerations

### Performance
- Query optimization
- Efficient data loading
- Caching strategies
- Real-time scaling

### Security
- Authentication flow
- Authorization policies
- Data access control
- Token management

### Scalability
- Database design
- Query performance
- Real-time capabilities
- File storage management
