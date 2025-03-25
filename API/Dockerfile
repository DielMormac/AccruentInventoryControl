# Use the official .NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files
COPY *.sln ./
COPY AccruentInventoryControl/*.csproj ./AccruentInventoryControl.API/
COPY AccruentInventoryControl.Application/*.csproj ./AccruentInventoryControl.Application/
COPY AccruentInventoryControl.Domain/*.csproj ./AccruentInventoryControl.Domain/
COPY AccruentInventoryControl.Infrastructure/*.csproj ./AccruentInventoryControl.Infrastructure/
WORKDIR /app/AccruentInventoryControl.API
RUN dotnet restore

# Copy the rest of the files, excluding test files
COPY AccruentInventoryControl ./AccruentInventoryControl.API/
COPY AccruentInventoryControl.Application ./AccruentInventoryControl.Application/
COPY AccruentInventoryControl.Domain ./AccruentInventoryControl.Domain/
COPY AccruentInventoryControl.Infrastructure ./AccruentInventoryControl.Infrastructure/

# Build the application
WORKDIR /app/AccruentInventoryControl.API
RUN dotnet publish AccruentInventoryControl.API.csproj -c Release -o /out

# Use the runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
EXPOSE 5116
ENTRYPOINT ["dotnet", "AccruentInventoryControl.API.dll"]