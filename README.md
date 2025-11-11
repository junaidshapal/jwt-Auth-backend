# JWT Auth Backend

This repository contains a backend implementation for authentication using JWT (JSON Web Tokens).

## Features

- User registration and login
- JWT-based authentication
- Protected routes
- Token refresh and logout (if implemented)
- Secure password handling

## Getting Started

### Prerequisites

- Node.js (version 12+ recommended)
- npm (comes with Node.js)

### Installation

1. **Clone the repository:**
   ```
   git clone https://github.com/junaidshapal/jwt-Auth-backend.git
   cd jwt-Auth-backend
   ```

2. **Install dependencies:**
   ```
   npm install
   ```

3. **Set up environment variables:**

   Create a `.env` file in the root directory and add the following (adjust as needed):

   ```
   PORT=5000
   JWT_SECRET=your_jwt_secret_key
   MONGODB_URI=mongodb://localhost:27017/your_db_name
   ```

4. **Start the development server:**
   ```
   npm run dev
   ```

   The server should now be running on `http://localhost:5000`.

## API Endpoints

*(Update the endpoints below according to your implementation)*

- `POST /api/auth/register` – Register a new user
- `POST /api/auth/login` – Log in and receive a JWT
- `GET /api/protected` – Example protected route (requires JWT)

## Usage

1. Register a new user via `/api/auth/register`.
2. Log in via `/api/auth/login` to receive a JWT token.
3. Use the token in the `Authorization` header as `Bearer <token>` to access protected routes.

## Folder Structure

```
jwt-Auth-backend/
├── controllers/
├── middleware/
├── models/
├── routes/
├── app.js / server.js
└── ...
```

- `controllers/` – Logic for handling requests
- `middleware/` – Authentication and other middleware
- `models/` – Mongoose or other data models
- `routes/` – API route definitions

## Environment Variables

| Name           | Description                  |
|----------------|-----------------------------|
| PORT           | Server running port          |
| JWT_SECRET     | Secret for signing JWT       |
| MONGODB_URI    | MongoDB connection string    |

## License

This project is licensed under the MIT License.

---

*Feel free to update this README with more details as your project evolves!*
