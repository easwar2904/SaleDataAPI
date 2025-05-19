
---

# Lumel Assessment
# 📊 Sales Data API

This project provides a .NET Web API for importing, managing, and analyzing large sales data from a CSV file into a MySQL database using Entity Framework Core.

## 🔧 Features

- Import and persist sales data from CSV files.
- Automatically refresh data via API endpoint.
- Query analytics such as Top N Products:
  - Overall
  - By Category
  - By Region
- Clean architecture with layered structure and logging.
- Error logging to file system with daily log files.

---

## 🚀 API Endpoints

### 🔄 Refresh Data

Trigger data refresh from the source CSV file.

POST /api/data/TriggerRefresh

This will parse and persist data across the following tables:

- `Customers`
- `Products`
- `Orders`
- `OrderDetails`

Ensure your CSV file follows the required schema before triggering this.

---

### 📈 Get Top N Products

Retrieve analytics for top selling products based on quantity sold and date range.

POST /api/analytics/GetTopProductsOverall

**Request Body:**
```json
{
  "topN": 5,
  "startDate": "2024-01-01",
  "endDate": "2024-12-31",
  "category": "Electronics",  // Optional
  "region": "North"           // Optional
}
Functionality Supported:

✅ Overall Top Products

✅ By Category

✅ By Region

Leave category or region null to omit that filter.

🗂 Project Structure
Models: Entity definitions for Customer, Product, Order, OrderDetail

Services: Data import, analytics logic

Controllers: API entry points

Data: SalesDbContext configured for MySQL

Logs: Written to /Logs/log_yyyyMMdd.txt on error

📦 Prerequisites
.NET 8 SDK
MySQL Server
Ensure Pomelo.EntityFrameworkCore.MySql is installed for EF Core MySQL support




📁 Sample CSV Header Format

Order ID	Product ID	Customer ID	Product Name	Category	Region	Date of Sale	Quantity Sold	Unit Price	Discount	Shipping Cost	Payment Method	Customer Name	Customer Email	Customer Address
✍️ License
This project is licensed under the MIT License.

yaml
Copy
Edit

---

## Project Setup

### Step 1: Create the Project

Create a new project using the **.Net Web API** with **.NET 8**.

### Step 2: Open Package Manager Console

Navigate to:

```
Tools → NuGet Package Manager → Package Manager Console
```

### Step 3: Install Required Packages

Run the following commands in the Package Manager Console:

```bash
dotnet add package CsvHelper
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.15
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.15
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.15
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
```

---

## Database Setup

### Step 4: Run the Following MySQL Scripts

> Ensure your MySQL database is running and properly configured.

#### 🧾 `customers` Table

```sql
CREATE TABLE customers (
  Id INT NOT NULL AUTO_INCREMENT,
  CustomerId VARCHAR(255) NOT NULL,
  NAME VARCHAR(255) NOT NULL,
  Email VARCHAR(320) NOT NULL, -- 320 is the max email length
  Address TEXT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_CustomerId (CustomerId)
);
```

#### 📦 `products` Table

```sql
CREATE TABLE products (
  Id INT NOT NULL AUTO_INCREMENT,
  ProductId VARCHAR(255) NOT NULL,
  NAME TEXT NOT NULL,
  Category TEXT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_ProductId (ProductId)
);
```

#### 🧾 `orders` Table

```sql
CREATE TABLE orders (
  Id INT NOT NULL AUTO_INCREMENT,
  OrderId VARCHAR(255) NOT NULL,
  DateOfSale DATETIME NOT NULL,
  Region TEXT NOT NULL,
  PaymentMethod VARCHAR(200) NOT NULL,
  Shippingcost DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  CustomerRefId VARCHAR(100) NOT NULL,
  CustomerId INT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_OrderId (OrderId),
  FOREIGN KEY (CustomerId) REFERENCES customers (Id) ON DELETE CASCADE
);
```

#### 📦 `orderdetails` Table

```sql
CREATE TABLE orderdetails (
  Id INT NOT NULL AUTO_INCREMENT,
  OrderRefId INT NOT NULL,
  OrderId INT NOT NULL,
  ProductRefId VARCHAR(100) NOT NULL,
  ProductId INT NOT NULL,
  QuantitySold INT NOT NULL,
  UnitPrice DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  Discount DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (Id),
  FOREIGN KEY (OrderId) REFERENCES orders (Id) ON DELETE CASCADE,
  FOREIGN KEY (ProductId) REFERENCES products (Id) ON DELETE CASCADE
);
```

---

