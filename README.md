# Company Management System

## Overview
This is a **.NET MVC Company Management System** that allows administrators to manage departments, employees, roles, and users. The application includes search functionality, image uploading, and authentication using ASP.NET Identity.

## Features
- **Modules:**
  - Department Management
  - Employee Management
  - Account Management
  - Role Management
  - User Management  
- **Functionality:**
  - Search for employees, users, and roles
  - Image uploading for employees
  - Secure authentication with ASP.NET Identity
- **Design Patterns Used:**
  - Generic Repository Pattern
  - Unit of Work Pattern
- **Frontend:**
  - HTML, CSS, Bootstrap, jQuery

## Technologies Used
- **Backend:** .NET MVC, C#
- **Database:** SQL Server, Entity Framework Core (EF Core), LINQ
- **Architecture:** N-Tier (Presentation Layer, Business Logic, Data Access)
- **Other Libraries:** AutoMapper for object mapping

## Installation and Setup
Follow these steps to set up the project:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
2. **Open the solution in Visual Studio.**
3. **Update the Connection String in appsettings.json to match your database.**
4. **Go to Package Manager Console and Update-Database**
5. **Run the application**

## Usage Instructions
1. **Create the First Admin Account** <br>
Open the application and go to the Register page. <br>
Register your first account. <br>
After registration, log in to access the system. <br>

2. **Set Up Admin Role**<br>
Navigate to the Roles section.<br>
Click on Create Role and enter "Admin" as the role name.<br>
After creating the role, go to Update and go to Add Or RemoveUser. <br>
assign your account to the "Admin" role. <br>

3. **Secure User & Role Management**
To ensure that only Admins can access Users and Roles: <br>
Open your code editor and navigate to:<br>
UserController.cs → Uncomment line 13 <br>
RoleController.cs → Uncomment line 10 <br>

4. **Restart the Application** <br>
After making the changes, restart the project. <br>
Now, Users and Roles can only be accessed by Admins.
