# Technical Context: Linkify

## Technology Stack

### Backend Framework
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- SignalR for real-time communication

### Database
- MySql (implied by EF Core migrations)
- Code-first approach with EF Core
- Migration-based schema management

### External Services
- Cloudinary for file storage (CloudinaryService implementation)
- JWT for authentication

### Development Tools
- Visual Studio / VS Code
- Entity Framework CLI tools
- NuGet package management

## Project Structure

### Linkify.Api
- Main API entry point
- Controller implementations
- DTO definitions
- Middleware configurations
- API documentation

### Linkify.Application
- Business logic implementation
- Interface definitions
- CQRS implementations
- AutoMapper configurations
- External service interfaces

### Linkify.Domain
- Core business entities
- Domain logic and rules
- Aggregates definitions
- Specifications
- Common interfaces

### Linkify.Infrastructure
- Data access implementation
- External service implementations
- Security implementations
- Real-time communication setup
- File management services

## Key Dependencies

### Data Access
- Microsoft.EntityFrameworkCore
- EF Core SQL Server provider
- EF Core tools for migrations

### Authentication
- JWT bearer authentication
- Identity framework integration
- Custom token management

### Real-time Communication
- SignalR for notifications
- SignalR for chat functionality

### File Management
- Cloudinary SDK
- File service abstractions

### Mapping
- AutoMapper for DTO mappings
- Custom mapping configurations

## Development Setup

### Required Tools
- .NET 8.0 SDK
- SQL Server
- Development IDE (VS Code/Visual Studio)

### Configuration
- appsettings.json for application settings
- Environment-specific configurations
- Cloudinary credentials
- Database connection strings
- JWT settings

### Database Management
- EF Core migrations
- Code-first approach
- Data seeding capabilities
- Migration history tracking

## Development Patterns

### Code Organization
- Feature-based organization
- Clean architecture layers
- CQRS pattern implementation
- Domain-driven design

### Testing Strategy
- Unit testing setup
- Integration testing capability
- Test data factories
- Mocking frameworks support

### Error Handling
- Global exception handling
- Domain-specific exceptions
- Custom middleware for errors
- Consistent error responses

### Security Practices
- JWT token validation
- Secure password handling
- Authorization policies
- Data access control

## Deployment Considerations

### Environment Setup
- Production configuration
- Environment variables
- Secret management
- Database deployment

### Performance
- Database indexing
- Query optimization
- Caching strategies
- Real-time scaling

### Monitoring
- Logging setup
- Performance metrics
- Error tracking
- User activity monitoring

## Development Workflow

### Version Control
- Feature branching
- Pull request workflow
- Code review process
- Release management

### CI/CD
- Build automation
- Test automation
- Deployment automation
- Environment management
