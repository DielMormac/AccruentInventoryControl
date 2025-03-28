# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY . ./
RUN dotnet restore

# Copy the rest of the application files and build the application
WORKDIR /app/AccruentInventoryControl
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /out .

# Expose the port the application runs on
EXPOSE 80
EXPOSE 8080
EXPOSE 443
EXPOSE 5116

# Set the entry point for the container
ENTRYPOINT ["dotnet", "AccruentInventoryControlAPI.dll", "--environment", "Development"]