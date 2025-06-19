# 🛍️ QuickOrders

QuickOrders is a microservices-based order management system designed to streamline the process of product browsing, cart handling, and order placement. It follows clean architecture principles, separates concerns via distinct services, and integrates an API Gateway to centralize routing.

---

## ✨ Features

- **User Authentication & Management**: Register and log in users.
- **Product Management**: Add, update, delete, and fetch items via the item microservice.
- **Cart Handling**: Add to cart, update quantity, and manage cart contents.
- **Service-to-Service Communication**: Orders service calls Items service via API Gateway to enrich order details.
- **Frontend Interface**: Angular app for user interaction.
- **API Gateway**: Centralized routing via Ocelot.
- **Robust API Contracts**: All endpoints follow DTO patterns with Swagger/OpenAPI.

---

## ⚙️ Architecture & Modules

### 1. API Gateway (`ApiGateway`)
- Centralized request routing using **Ocelot**.
- Handles service-level routing and acts as the main entry point.
- Routes like `/gateway/items/...`, `/gateway/orders/...`.

### 2. Users Service
- ASP.NET Core Web API.
- Handles registration, login, and user-related operations.
- Secured and stateless with JWT (if implemented).

### 3. Items Service
- CRUD operations for items.
- Each item contains ID, name, price, description, and product image.
- New endpoint: `POST /api/items/getusersproducts` accepts a list of item IDs and returns detailed product data.

### 4. Cart Service
- Add/remove/update products in a user’s cart.
- Retrieves active cart data by user ID.
- Stateless cart handling using EF Core.

### 5. Orders Service
- Places and fetches user orders.
- Sends item ID list to Items service through Gateway for full item detail enrichment.
- Uses service-to-service communication over HTTP.

### 6. Frontend (Angular)
- User-facing UI.
- Built with Angular, integrated with backend via API Gateway.
- Responsive design and clean UX.

---

## 📦 Technologies Used

| Layer           | Technology         |
|----------------|--------------------|
| Backend         | ASP.NET Core Web API |
| Frontend        | Angular            |
| Database        | SQL Server         |
| API Gateway     | Ocelot             |
| ORM             | Entity Framework Core |
| Styling (UI)    | Bootstrap, CSS,SweetAlerts    |
| Logging         | Console + Serilog (optional) |

---

## 🏁 Getting Started

### 🔹 Step 1: Clone the Repository

```bash
git clone https://github.com/SidhantUppal/QuickOrders.Microservices.git
cd QuickOrders

Step 2: Start Backend Microservices
Each microservice is a separate ASP.NET Core Web API project:

cd UsersService
dotnet run

cd ../ItemsService
dotnet run

cd ../CartService
dotnet run

cd ../OrdersService
dotnet run

cd ../ApiGateway
dotnet run
The default ports can be configured in each launchSettings.json or appsettings.json.

🔹 Step 3: Start Frontend (Angular)

cd eCommerceUI
npm install
ng serve
Frontend will be hosted at http://localhost:4200.

🔹 Step 4: Configure Database
Ensure SQL Server is installed and running.

Each service has its own DbContext and migration.

Update the connection strings in each service’s appsettings.json:


"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=QuickOrdersDb;User Id=sa;Password=yourpassword;"
}

Run EF Core migrations:

dotnet ef database update
📡 API Endpoints
Endpoint	Description
GET /gateway/items/getitem/{id}	Get a single item by ID
POST /gateway/items/getusersproducts	Get list of items by item IDs
GET /gateway/orders/user/{id}	Get orders for a user (with item info)
POST /gateway/cart/add	Add item to cart
POST /gateway/orders/place	Place order

🧪 Testing & Postman
All endpoints are tested with Postman collections.

Ensure Gateway is running to route requests correctly.

Use /swagger on each microservice to explore the APIs.

📁 Project Structure

QuickOrders/
│
├── UsersService/            # User registration & login
├── ItemsService/            # Product/item CRUD
├── CartService/             # Cart operations
├── OrdersService/           # Orders & service-to-service logic
├── ApiGateway/              # Ocelot gateway
├── QuickOrdersUI/           # Angular frontend
└── README.md

✅ Post-Setup Checklist
 All services restored and running

 API Gateway configured and operational

 Database migrations run

 Angular frontend compiled

 Gateway endpoints tested via Postman