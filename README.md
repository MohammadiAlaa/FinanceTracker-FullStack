# 💰 Finance Tracker - Full Stack Application

Finance Tracker is a comprehensive solution designed to help users manage their daily finances, track income/expenses, and visualize their spending habits through interactive charts.

---

## 🚀 Tech Stack

- **Front-end:** Angular 17+ (Signals, Standalone Components, SCSS)
- **Back-end:** .NET 8 Web API
- **Database:** Entity Framework Core with SQL Server
- **UI/UX:** Bootstrap 5, Bootstrap Icons, SweetAlert2
- **Data Visualization:** Chart.js

---

## ✨ Key Features

- **Secure Authentication:** JWT-based login and registration with password hashing.
- **Transaction Management:** Create, Read, Update, and Delete (CRUD) for daily transactions.
- **Dynamic Categories:** Categorize spending (Food, Rent, Salary, etc.) for better organization.
- **Interactive Dashboard:** - Real-time summary of Total Balance, Income, and Expenses.
  - Responsive UI for mobile and desktop.

---

## 📊 Data Visualization & Charts

One of the highlights of this project is the **Financial Analytics Chart**. We implemented this using **Chart.js** to provide a visual breakdown of expenses.

### How it was implemented:
1. **Backend Aggregation:** Created a specific DTO (`TransactionSummaryDto`) in the .NET API to group transactions by category and calculate total amounts.
2. **API Endpoint:** A specialized endpoint calculates the percentage of each category relative to total spending.
3. **Frontend Integration:**
    - We used **Chart.js** to render a *Doughnut Chart* and *Line Chart*.
    - **Signals** were used to reactively update the chart whenever a new transaction is added without refreshing the page.
    - Custom color palettes are dynamically assigned based on the transaction type (Income = Green, Expense = Red).

---

## 🛠️ Installation & Setup

### Prerequisites
- .NET 8 SDK
- Node.js & Angular CLI
- SQL Server

### Steps

#### 1. Clone the Repo:
``bash
git clone [https://github.com/MohammadiAlaa/FinanceTracker-FullStack.git](https://github.com/MohammadiAlaa/FinanceTracker-FullStack.git)

2. **Setup Backend**:
Navigate to FinanceTracker_API.

Update appsettings.json with your connection string.

Run the following commands:

Bash

dotnet ef database update
dotnet run

3.**Setup Frontend**:
Navigate to FinanceTracker-UI.

Run the following commands:

Bash

npm install
ng serve

👨‍💻 Author
Mohammadi Alaa

GitHub: MohammadiAlaa

LinkedIn: www.linkedin.com/in/mohammadi-alaa-98a627264
