# AccruentInventoryControl
Accruent Inventory Control (ACR-InControl) A project to deliver the Exercise for Principal Software Engineer role at Accruent

README.md
Accruent Inventory Control
This project consists of two main components:

API: A backend service built with .NET.
UI: A frontend application built with React.
This document provides instructions for setting up the environment and running both the API and UI.

Prerequisites
Before you begin, ensure you have the following installed on your system:

For the API
.NET SDK (version 8.0)
SQL Server (if required for database setup)
For the UI
Node.js (version 16.x or later)
npm (comes with Node.js)
Setup Instructions
Step 1: Clone the Repository
Clone the repository to your local machine:

Step 2: Configure the API
Navigate to the API directory:
cd API/AccruentInventoryControl

Restore dependencies:
dotnet restore

Run the API:
dotnet run

The API will start on http://localhost:5116 by default (if you prefer another port you can specify it in launchSettings.json).

Step 3: Configure the UI
Navigate to the UI directory:
cd UI/accruent-inventory-control

Install dependencies:
npm install

Create a .env file in the accruent-inventory-control directory:
touch .env

Add the following environment variables to the .env file:
REACT_APP_API_BASE_URL=http://localhost:5000/api/v1

Start the UI:
npm start

The UI will start on http://localhost:5174/.

Testing the Setup
Open your browser and navigate to the UI:

Ensure the UI is able to communicate with the API by performing actions that require API calls (e.g., fetching products, submitting transactions).

Troubleshooting
Common Issues
API Fails to Start:

The database will be running InMemory, it doest required any database setup.

UI Fails to Start:

Ensure the REACT_APP_API_BASE_URL in the .env file points to the correct API URL.
Verify that Node.js and npm are installed.
CORS Issues:

If you encounter CORS errors, ensure the API allows requests from http://localhost:5174/. Update the CORS policy in the API if necessary.

Folder Structure
AccruentInventoryControl/
├── API/
│ └── AccruentInventoryControl/ # API project
├── UI/
│ └── accruent-inventory-control/ # UI project
└── startup.ps1 # Script to start both API and UI
