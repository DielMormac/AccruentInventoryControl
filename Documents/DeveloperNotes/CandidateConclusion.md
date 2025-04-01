# [Back to Index](../Index.md)

# Conclusion on the Exercise

I created this documentation to explain a bit about my understanding of the exercise and how I made my decisions based on the time and needs to be met.

## Requirements Gathering
The requirements presented in the exercise statement leave room for many questions. In a real development scenario, before starting the application development, it would be necessary to better align the product team's expectations regarding the system's behavior in some specific scenarios.

In an initial meeting with the product team, I would ask questions such as:
- Should the system support orders with movements of more than one product?
- What happens if any of the products in this order are unavailable?
- What is the expected number of stock movements?
- If we do not have available stock, should the movement be canceled or queued until the stock becomes available?

## Concurrency Management
A point of complexity in the exercise is the management of stock concurrency. With the technical requirements presented in the exercise statement, it is not possible to define the volume of transactions for the entry and exit of products from the stock.

In my approach, thinking of an initial MVP version, I will consider a system with a low volume of transactions. In this case, using Locking in the database will not be a problem, as the demand is low, and the risk of a Deadlock in the database will also be low.

I am also using the server's local time as a reference to ensure that the database entries are always ordered by a single timezone.

In a real scenario, I would seek more information to set up the requirements for these transactions by asking questions such as:
- What is the expected number of stock movements per minute?
- Does the user feedback on the availability of the requested product quantity need to be immediate?
- If we do not have available stock, should the movement be canceled or queued until the stock becomes available?
- Is there an expiration date for a movement?

## Feature Ideas for Product Improvements

### Security Improvements
- API Authentication
- Retry policy (Polly) when interacting with databases or messaging systems
- Include SonarLint and Veracode to ensure code quality and avoid vulnerabilities
- Virtual signing of artifacts (CodeSign) to ensure compliance and guarantee that public artifacts are not altered

### Performance Improvements
- Implement caching for queries with data that does not mutate frequently

### Technical Improvements
- Support for pagination and filters in API endpoints
  - This was not implemented due to strategic decisions regarding the exercise, as I decided to use an in-memory database with EntityFramework to facilitate the evaluator running the project locally without the need to install environment dependencies. However, this causes limitations with IQueryable methods, making it difficult to implement filters.
- Docker implementation to facilitate project deployment and setup
  - In a real scenario, using a database instance, all project setup configuration can be done in Docker to facilitate both deployment and development team setup.
- Setup configuration scripts: startup.ps1, .env, .makefile, package.json, dockerfiles, to facilitate shortcut commands for running tests, execution, setup, etc.
- Create pipeline script with IaC (Terraform/Pulumi)
- Consult a UX/UI professional to help create a UI framework and React components.

### Functionality Improvements
- Support for product registration
- Support for orders with multiple products
- Export reports in PDF or CSV
- Reports with charts for stock visualization and product movement comparison
- Routine for automated report sending via email
