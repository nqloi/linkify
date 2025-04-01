# Technical Context: Linkify

## Development Stack
1. Core Technologies
   - Vue.js 3 (Frontend Framework)
   - Vite (Build Tool)
   - Pinia (State Management)
   - Vue Router (Routing)
   - Tailwind CSS (Styling)
   - SCSS (Custom Styling)

2. Key Dependencies
   - Axios (HTTP Client)
   - socket.io-client (WebSocket)
   - @vueuse/core (Vue Composables)
   - dayjs (Time Formatting)

## Development Setup
1. Project Structure
   ```
   src/
   ├── assets/         # Static resources
   ├── common/         # Constants and enums
   ├── components/     # Reusable components
   ├── layout/        # Layout components
   ├── router/        # Route definitions
   ├── services/      # API services
   ├── stores/        # Pinia stores
   ├── utils/         # Utility functions
   └── views/         # Page components
   ```

2. Build Configuration
   - Vite for development and production builds
   - PostCSS for CSS processing
   - ESLint for code linting
   - Prettier for code formatting

## Technical Constraints
1. Browser Compatibility
   - Modern browsers (Chrome, Firefox, Safari, Edge)
   - ES6+ JavaScript features
   - CSS Grid and Flexbox
   - WebSocket support

2. Performance Requirements
   - Initial load time < 3s
   - Time to interactive < 5s
   - Smooth scrolling (60fps)
   - Responsive image loading

3. Security Constraints
   - JWT authentication
   - XSS prevention
   - CSRF protection
   - Secure WebSocket connections

## Development Patterns
1. Code Organization
   - Feature-based directory structure
   - Composition API for component logic
   - Composables for shared logic
   - Service layer abstraction

2. State Management
   - Pinia stores by feature domain
   - Local component state when appropriate
   - Reactive computed properties
   - Watchers for side effects

3. API Integration
   - RESTful endpoints
   - Service factory pattern
   - Interceptor middleware
   - Error handling standardization

4. Testing Approach
   - Unit tests for utilities
   - Component testing with Vue Test Utils
   - E2E testing with Cypress
   - API mocking capabilities

## Tool Usage
1. Version Control
   - Git for source control
   - Feature branch workflow
   - Conventional commits
   - Pull request reviews

2. Development Tools
   - VS Code as primary IDE
   - Vue DevTools for debugging
   - Chrome DevTools for performance
   - Postman for API testing

3. Build Tools
   - npm for package management
   - Vite for development server
   - Build scripts customization
   - Environment configuration

4. Deployment
   - CI/CD pipeline integration
   - Build optimization
   - Asset optimization
   - Environment management
