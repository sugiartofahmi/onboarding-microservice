## Description

Dot Net Microservice Onboarding [DOT Indonesia](https://www.dot.co.id/)

## Project Setup

- Change directory to specific service (`cd OrderService`)
- Copy file `appsettings.example.json`, rename to `appsettings.json` and adjust the configuration
- Run migration (`dotnet ef database update`)

## Directory Structure

- IamService
- OrderService
- InventoryService

## Running the app

```bash
# Change directory to microservice apps
$ cd OrderService

# Run in watch mode and adjust the port to avoid conflicts with other applications
$ dotnet watch run dotnet watch run --urls="http://localhost:5000"
```

## Endpoints

| Method | URL                    | Description            |
| ------ | ---------------------- | ---------------------- |
| `POST` | `/api/v1/auth/sign-in` | Login.                 |
| `GET`  | `/api/v1/orders`       | Retrieve all orders.   |
| `POST` | `/api/v1/orders`       | Create order.          |
| `GET`  | `/api/v1/product`      | Retrieve all products. |

## Documentation

- [Postman](https://documenter.getpostman.com/view/28846904/2sA3sAhTLs)
- [DB Diagram](https://dbdiagram.io/d/Dotnet-Microservices-66b9a75c8b4bb5230ed724e2)
- [Excalidraw](https://excalidraw.com/#json=54kQyBH87vEe1l6Nn7FQo,d1oXhFFjsmrq7uICiLNopQ)
