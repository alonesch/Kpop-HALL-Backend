# Kpop! HALL - Backend

## Overview

Kpop! HALL is a backend system designed to manage K-pop photocards collections with a structured and scalable architecture.

This project follows Clean Architecture principles and is built using .NET 10 with JWT authentication and domain-driven design practices.

---

## Architecture

### Domain Layer
Contains core business rules and entities:
- Artist
- Album
- Photocard
- User
- DistributionContext
- DomainException

Business rules are fully encapsulated within entities.

### Application Layer
Implements use cases and application logic:
- Create, List and Get operations for Artists, Albums and Photocards
- User Registration
- User Login
- Interfaces for repositories and security services

### Infrastructure Layer
Provides implementations for:
- Fake repositories (in-memory persistence only for tests or when you dont want to build docker in localhost)
- BCrypt password hashing
- JWT token generation

### API Layer
- Versioned Controllers (v1)
- Global exception middleware
- JWT Authentication configuration
- Authorization attributes

---

## Authentication

- Password hashing via BCrypt
- JWT token generation
- Role support
- Protected endpoints using [Authorize]

Claims included:
- NameIdentifier
- Name
- Email
- Role

---

## Protected Endpoints

Requires Bearer token:

- POST /api/v1/artists
- POST /api/v1/artists/{artistId}/albums
- POST /api/v1/artists/{artistId}/albums/{albumId}/photocards

---

## Implemented Features

Artists
- Create
- List
- Get by ID

Albums
- Create linked to Artist
- Duplicate validation per Artist

Photocards
- Create Regular
- Create Irregular (with DistributionContext)
- List by Album
- Get by ID
- Duplicate validation per Album

Users
- Register
- Login
- JWT Authentication
- Role support

---

## Technologies

- .NET 10
- Clean Architecture
- JWT Authentication
- BCrypt
- In-memory repositories (temporary)
- Designed for SQL Server migration

---

## Planned Improvements

- EF Core integration
- SQL Server persistence
- Refresh Token implementation
- Role-based authorization policies
- User Collections
- API versioning improvements
- Structured logging
- Docker deployment
- Linux VPS production setup

---

## Running the Project

1. Configure JWT key in appsettings.json
2. Run the API
3. Register a user
4. Login to obtain a token
5. Use the token to access protected routes