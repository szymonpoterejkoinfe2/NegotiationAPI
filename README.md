# NegotiationAPI

A web application implementing a product price negotiation process for an online store.

---

## 📌 Before You Start

Before running the application, make sure you **configure user secrets** to securely store sensitive data like JWT secret keys.

### ✅ Required Step: Add User Secrets

This project uses **JWT authentication**, and the secret key must be stored using the [.NET user secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets).

1. Open a terminal in the project directory.
2. Run the following command:

   ```bash
   dotnet user-secrets init
3. Then set the JWT secret:
   ```bash
   dotnet user-secrets set "JwtSettings:Secret" "your-very-strong-secret-key-123456"
   
You can also add the secret manually to your secrets file. Example content:

  ```json
  {
    "JwtSettings": {
      "Secret": "your-very-strong-secret-key-123456"
    }
  }
```
## Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [API Features](#api-features)
  - [Authentication](#authentication)
  - [Products](#products)
  - [Negotiations](#negotiations)
  - [Negotiation Attempts](#negotiation-attempts)
- [Use Cases](#use-cases)
  - [1. Customer Proposes a Price for a Product](#1-customer-proposes-a-price-for-a-product)
  - [2. Employee Reviews and Responds to a Price Attempt](#2-employee-reviews-and-responds-to-a-price-attempt)
  - [3. Employee Manages Products](#3-employee-manages-products)
  - [4. Customer Continues Negotiation after Rejection](#4-customer-continues-negotiation-after-rejection)

---

## Project Overview

NegotiationAPI allows customers (unauthenticated users) to propose their own price for products, while store employees (authenticated users) can accept or reject these offers. The negotiation process is as follows:

- The customer proposes a product price.
- The employee can accept or reject the offer.
- If rejected, the customer has up to 3 attempts to submit new offers.
- The customer must submit a new offer within 7 days of rejection, or the negotiation will be cancelled.

## Architecture

- **.NET 9 Web API**
- **Clean Architecture** – clear separation of concerns and layers.
- **CQRS** (Command Query Responsibility Segregation) – separating read and write operations.
- **Repository Pattern** – abstract data access.
- **In-memory storage** (Lists) used for simplicity, with easy future migration to a database.
- **Authentication for employees** (JWT).
- **Request validation** (FluentValidation).
- **SignalR** for real-time communication and updates.
- **Unit Tests** for repository classes with MSTest

## API Features

### Authentication

- `POST /Authentication/register`  
  Register a new employee user. Requires a JSON body with registration details.

- `POST /Authentication/login`  
  Login for employees to receive JWT tokens for authenticated access.

---

### Products

- `GET /Product`  
  Retrieve a list of all products.

- `GET /Product/{productId}`  
  Retrieve details for a specific product.

- `POST /Product`  
  Add a new product. Requires employee authentication and JSON body with product info.

- `DELETE /Product/{productId}`  
  Delete a product by ID (employee only).

---

### Negotiations

- `GET /Negotiation`  
  List all negotiations.

- `POST /Negotiation?productId={id}`  
  Start a new negotiation for a product by ID.

- `GET /Negotiation/{negotiationId}`  
  Get negotiation details.

- `PUT /Negotiation/{negotiationId}/status`  
  Change negotiation status with JSON body specifying new status.

- `DELETE /Negotiation/{negotiationId}`  
  Delete a negotiation by ID.

---

### Negotiation Attempts

- `GET /NegotiationAttempt`  
  List all negotiation attempts.

- `GET /NegotiationAttempt/Waiting`  
  List all negotiation attempts currently waiting for customer response.

- `GET /NegotiationAttempt/{attemptId}`  
  Get specific negotiation attempt details.

- `POST /NegotiationAttempt`  
  Customer submits a new price attempt with JSON body including negotiationId and proposedPrice.

- `PUT /NegotiationAttempt/accept/{attemptId}`  
  Employee accepts a negotiation attempt.

- `PUT /NegotiationAttempt/reject/{attemptId}`  
  Employee rejects a negotiation attempt.

- `DELETE /NegotiationAttempt/{attemptId}`  
  Delete a negotiation attempt.

---

## Use Cases

### 1. Customer Proposes a Price for a Product

1. Customer browses products using `GET /Product`.
2. Customer selects a product and starts negotiation with `POST /Negotiation?productId={productId}`.
3. Customer submits a price attempt using `POST /NegotiationAttempt` with `negotiationId` and `proposedPrice`.
4. Employees are notified via SignalR in real time.

---

### 2. Employee Reviews and Responds to a Price Attempt

1. Employee logs in via `POST /Authentication/login` to obtain JWT token.
2. Employee receives SignalR notification of a new price attempt.
3. Employee fetches waiting attempts with `GET /NegotiationAttempt/Waiting`.
4. Employee reviews attempt details via `GET /NegotiationAttempt/{attemptId}`.
5. Employee either:
   - Accepts using `PUT /NegotiationAttempt/accept/{attemptId}`, or
   - Rejects using `PUT /NegotiationAttempt/reject/{attemptId}`.
6. If rejected, customer has 7 days and up to 2 more attempts.

---

### 3. Employee Manages Products

1. Employee logs in via `POST /Authentication/login`.
2. Lists products using `GET /Product`.
3. Adds new products using `POST /Product`.
4. Deletes products with `DELETE /Product/{productId}`.

---

### 4. Customer Continues Negotiation after Rejection

1. Customer receives rejection notification externally (not covered by API).
2. Customer submits a new price attempt within 7 days and within 3 total attempts using `POST /NegotiationAttempt`.
3. SignalR notifies employees in real time of the new attempt.
4. Process repeats until an attempt is accepted or negotiation expires.

---
