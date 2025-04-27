# FinShark WebAPI
Overview
FinShark is a financial Web API designed to help users manage and track stocks for companies. It provides robust functionality for creating portfolios, adding stocks, and engaging with community feedback through a comment system.

# Features
Stock Management
- View detailed information about stocks from various companies
- Search and filter stocks based on different parameters
- Add and remove stock from db

Portfolio Management
- Create personalized stock portfolios
- Add and remove stocks from portfolios
- Track portfolio performance
- Set investment goals and track progress

Comments System
- Leave comments on stocks and portfolios

Security
- Secure user authentication
- Authorization with JWT (JSON Web Token) implementation

# Installation

- Clone the repository
- Configure the database connection in appsettings.json

json"ConnectionStrings": {
  "DefaultConnection": "Your_Database_Connection_String"
}

- Apply migrations to create the database
dotnet ef database update

- Run the application

dotnet run (or dotnet watch run for "cold" run)
