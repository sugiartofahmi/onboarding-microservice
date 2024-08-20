## Description

Dot Net Microservice Onboarding [DOT Indonesia](https://www.dot.co.id/)

## Project Setup

- Change directory to specific service (`cd OrderService`)
- Copy file `appsettings.example.json`, rename to `appsettings.json` and adjust the configuration
- Run migration (`dotnet ef database update`)

## Directory Structure

- ApiGateway
- OrderService
- InventoryService

## Running the app

```bash
# Change directory to microservice apps
$ cd OrderService

# Run watch mode
$ dotnet watch run
```

## Endpoints

| Method | URL                    | Description            |
| ------ | ---------------------- | ---------------------- |
| `POST` | `/api/v1/auth/sign-in` | Login.                 |
| `GET`  | `/api/v1/orders`       | Retrieve all orders.   |
| `POST` | `/api/v1/orders`       | Create order.          |
| `GET`  | `/api/v1/product`      | Retrieve all products. |

## API Documentation

- [Postman](https://documenter.getpostman.com/view/28846904/2sA3sAhTLs)
