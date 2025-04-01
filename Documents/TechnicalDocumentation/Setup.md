{% include "./Documents/Index.md" %}

# Setup

This document provides instructions for setting up the environment and running both the API and UI.

### Prerequisites
Before you begin, ensure you have the following installed on your system:

### For the API
.NET SDK (version 8.0): https://dotnet.microsoft.com/pt-br/download/dotnet/8.0

### For the UI
Node.js (version 16.x or later): https://nodejs.org/pt

npm (comes with Node.js)

## Setup Instructions

### Step 1: Clone the Repository
Clone the repository to your local machine:

`git clone https://github.com/DielMormac/AccruentInventoryControl.git`

### Step 2: Configure the API

Navigate to the API directory:

`cd API/AccruentInventoryControl`

Restore dependencies:

`dotnet restore`

Run the API:

`dotnet run`

The API will start on `http://localhost:5116` by default (if you prefer another port you can specify it in launchSettings.json).

### Step 3: Configure the UI

Navigate to the UI directory:

`cd UI/accruent-inventory-control`

### Step 4: Install dependencies:

`npm install`

### Step 5: Start the UI:

`npm run dev`

The UI will start on `http://localhost:5174/`

### Step 6 (optional): Running via `startup.ps1`

Once you have all the dependencies installed in your environment, you can opt for starting the project via script.

Navigate to the UI directory:

`cd UI/accruent-inventory-control`

Type:

`.\startup.ps1`

Press `Enter`

This file will open two `terminal` one for the API and other for the UI.

Open your browser and navigate to: 

**UI:** [http://localhost:5174/](http://localhost:5173/)

**API (Documentation):** [http://localhost:5174/](http://localhost:5116/swagger/index.html)

### Step 6 (optional): Running via docker

Navigate to the UI root directory:

execute `docker compose up --build`

### Testing the Setup

Open your browser and navigate to the UI:

Ensure the UI is able to communicate with the API by performing actions that require API calls (e.g., fetching products, submitting transactions).

# Troubleshooting

## API Fails to Start:

- Verify that you have the 8.0 installed
  - open your cmd and type:
    >`dotnet --list-sdks`
    >
    >`dotnet --list-runtimes`

## UI Fails to Start:
Verify that Node.js and npm are installed.

## CORS Issues:
If you encounter CORS errors, ensure the API allows requests from http://localhost:5174/. Update the CORS policy in the API if necessary.

## Folder Structure
```markdown
AccruentInventoryControl/
 ├── API/
 │   └── AccruentInventoryControl/ # API project
 ├── UI/
 │   └── accruent-inventory-control/ # UI project
 └── startup.ps1 # Script to start both API and UI
```
