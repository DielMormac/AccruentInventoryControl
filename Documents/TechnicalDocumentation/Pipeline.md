# [Back to Index](./Documents/Index.md)

# CI/CD Pipeline

## Description
I created this CI/CD pipeline using GitHub Actions to exemplify the automation of the build, test, and deploy process of the application.

The pipeline is triggered by pushes to the `main` branch or manually via `workflow_dispatch`.

## Trigger Events
The pipeline can be triggered by the following events:
- Push to the `main` branch
- Manually via `workflow_dispatch`

## Jobs

### Build
This job is responsible for building and testing the application.

#### Environment
- `runs-on: ubuntu-latest`

#### Steps
1. **Checkout code**
    ```yaml
    - name: Checkout code
      uses: actions/checkout@v2
    ```

2. **Set up .NET**
    ```yaml
    - name: Set up .NET
      uses: actions/setup-dotnet@v2
      with:
        dotnet-version: "8.0.x"
    ```

3. **Set up Node.js**
    ```yaml
    - name: Set up Node.js
      uses: actions/setup-node@v2
      with:
        node-version: "18"
    ```

4. **Install dependencies and run tests for API**
    ```yaml
    - name: Install dependencies and run tests for API
      run: |
        cd API
        dotnet restore
        dotnet test
    ```

5. **Install dependencies and run tests for UI**
    ```yaml
    - name: Install dependencies and run tests for UI
      run: |
        cd UI/accruent-inventory-control
        npm install
        npm test
    ```

6. **Log in to GitHub Container Registry**
    ```yaml
    - name: Log in to GitHub Container Registry
      uses: docker/login-action@v2
      with:
        registry: ghcr.io
        username: ${{ github.actor }}
        password: ${{ secrets.GITHUB_TOKEN }}
    ```

7. **Build and push API Docker image**
    ```yaml
    - name: Build and push API Docker image
      uses: docker/build-push-action@v2
      with:
        context: ./API
        file: ./API/Dockerfile
        push: true
        tags: ghcr.io/dielmormac/inventory-control-api:latest
    ```

8. **Build and push UI Docker image**
    ```yaml
    - name: Build and push UI Docker image
      uses: docker/build-push-action@v2
      with:
        context: ./UI/accruent-inventory-control
        file: ./UI/accruent-inventory-control/Dockerfile
        push: true
        tags: ghcr.io/dielmormac/inventory-control-ui:latest
    ```

### Deploy
This job is responsible for deploying the application to Azure.

#### Environment
- `runs-on: ubuntu-latest`
- `needs: build`

#### Steps
1. **Checkout code**
    ```yaml
    - name: Checkout code
      uses: actions/checkout@v2
    ```

2. **Login to Azure**
    ```yaml
    - name: Login to Azure
      uses: azure/login@v1
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}
    ```

3. **Deploy to Azure App Service**
    ```yaml
    - name: Deploy to Azure App Service
      uses: azure/webapps-deploy@v2
      with:
        app-name: "inventory-control"
        publish-profile: ${{ secrets.AZURE_PUBLISH_PROFILE }}
        package: "./"
    ```

## Conclusion
This pipeline automates the build, test, and deploy process of the application, ensuring that each code change is validated and deployed efficiently and securely.
