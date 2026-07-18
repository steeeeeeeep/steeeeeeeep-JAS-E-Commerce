# JAS E-Commerce Mobile App

React Native mobile application built with Expo, TypeScript, and Zustand state management.

## Features

✅ **Authentication** - JWT-based login/register with secure token storage  
✅ **Product Catalog** - Browse and search products  
✅ **Shopping Cart** - Add/remove items, manage quantities  
✅ **Order Management** - View order history  
✅ **User Profile** - Manage account settings  
✅ **State Management** - Zustand for global state  
✅ **API Integration** - Axios with JWT interceptors  

## Tech Stack

- **Framework**: Expo 51 with React Native 0.74
- **Language**: TypeScript
- **Routing**: Expo Router
- **State Management**: Zustand
- **HTTP Client**: Axios
- **Styling**: NativeWind (Tailwind CSS)
- **Security**: Expo Secure Store

## Project Structure

```
mobile/
├── app/                 # Expo Router pages
│   ├── (tabs)/         # Tab-based navigation
│   └── auth/           # Authentication screens
├── stores/             # Zustand state management
│   ├── authStore.ts
│   ├── productStore.ts
│   ├── cartStore.ts
│   └── orderStore.ts
├── api/                # API integration
│   └── client.ts       # Axios client with interceptors
├── components/         # Reusable UI components
├── app.json            # Expo configuration
├── package.json        # Dependencies
└── tsconfig.json       # TypeScript configuration
```

## Installation

```bash
cd mobile
npm install
npm start
```

## Environment Setup

Create a `.env` file based on `.env.example`:

```bash
EXPO_PUBLIC_API_BASE_URL=http://localhost:5000/api
EXPO_PUBLIC_API_TIMEOUT=30000
```

## Running the App

```bash
# Start the development server
npm start

# Run on iOS
npm run ios

# Run on Android
npm run android

# Run on Web
npm run web
```

## Authentication Flow

1. User opens app → `initializeAuth()` checks for stored token
2. If token exists → Redirect to Home (tabs)
3. If no token → Redirect to Login
4. After login/register → Token stored in Secure Store
5. JWT automatically attached to all API requests
6. On 401 response → Token cleared, user redirected to login

## State Management

### AuthStore
- User login/register
- Token management
- Profile fetching
- Logout functionality

### ProductStore
- Fetch all products
- Product list caching

### CartStore
- Add/remove items
- Update quantities
- Clear cart

### OrderStore
- Fetch user orders
- Order history

## API Integration

Axios client with:
- Request interceptor: Attaches JWT token from Secure Store
- Response interceptor: Handles 401 errors and clears token
- Configurable base URL and timeout

## Next Steps

- [ ] Implement product detail screens
- [ ] Add product filtering and search
- [ ] Complete checkout flow
- [ ] Add payment integration
- [ ] Implement notifications
- [ ] Add error handling UI
- [ ] Unit and E2E tests
