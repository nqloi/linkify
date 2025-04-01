# System Patterns: Linkify

## Architecture Overview
1. Frontend Architecture
   - Vue.js 3 with Composition API
   - Pinia for state management
   - Vue Router for navigation
   - Component-based structure

2. Service Layer
   - Axios-based HTTP client
   - Base service factory pattern
   - Interceptors for authentication
   - WebSocket integration for real-time features

## Design Patterns
1. Component Patterns
   - Composable functions for reusable logic
   - Base components for common UI elements
   - Layout components for page structure
   - Modal components for overlay interactions

2. State Management
   - Pinia stores for global state
   - Modular store organization by feature
   - Reactive state management
   - Action-based mutations

3. Service Layer
   - Factory pattern for service creation
   - Repository pattern for data access
   - Interceptor pattern for request/response handling
   - Cache provider pattern for local storage

## Component Relationships
1. Layout Structure
   ```
   MainLayout
   ├── TheHeader
   ├── NavigationSidebar
   ├── RouterView (main content)
   └── SidebarRight
   ```

2. Profile Feature
   ```
   ProfileLayout
   ├── ProfileHeader
   │   ├── ProfileCover
   │   └── ProfileInfo
   ├── ProfileNavigation
   └── ProfileContent
       ├── ProfilePosts
       ├── ProfileFriends
       └── ProfilePhotos
   ```

3. Feed Feature
   ```
   FeedList
   ├── PostInput
   ├── FeedPost
   │   ├── ReactionButton
   │   └── CommentSection
   └── InfiniteScroll
   ```

## Critical Implementation Paths
1. Authentication Flow
   - Login/Register authentication
   - Token management
   - Session timeout handling
   - Refresh token mechanism

2. Post Creation Flow
   - Media upload handling
   - Post creation
   - Feed update
   - Real-time notifications

3. Profile Management
   - Profile data updates
   - Avatar/cover photo management
   - Friend connection handling
   - Privacy settings

## Styling Patterns
1. Tailwind Integration
   - Utility-first approach
   - Custom component classes
   - Responsive design patterns
   - Dark mode support

2. SCSS Structure
   - Variable organization
   - Utility mixins
   - Theme management
   - Common styles

## Error Handling
1. Global Error Management
   - Exception type constants
   - Error interceptors
   - Toast notifications
   - Fallback UI states

2. Form Validation
   - Input validation
   - Error messaging
   - Field state management
   - Submit handling
