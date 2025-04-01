# AccruentInventoryControl
Accruent Inventory Control (ACR-InControl) A project to deliver the Exercise for Principal Software Engineer role at Accruent

## Accruent Inventory Control
This project consists of two main components:

**API:** A backend service built with .NET.

**UI:** A frontend application built with React.

# Index:

## Technical Documentation

- [Setup Instructions](./Documents/TechnicalDocumentation/Setup.md)
- [Pipeline](./Documents/TechnicalDocumentation/Pipeline.md)
- [General Architecture Example](./Documents/TechnicalDocumentation/GeneralArchitectureExample.md)
- [Database Design](./Documents/TechnicalDocumentation/DatabaseDesign.md)
- [Warehouse Transaction Flow Example](./Documents/TechnicalDocumentation/WarehouseTransactionFlowExample.md)
- [Terraform](./Documents/TechnicalDocumentation/Terraform.md)

## Developer Notes

- [Candidate Conclusion](./Documents/DeveloperNotes/CandidateConclusion.md)
- [Principal Software Engineer Duties](./Documents/DeveloperNotes/PrincipalEngineerDuties.md)
- [AI Disclaimer](./Documents/DeveloperNotes/AIDisclaimer.md)

## Folder Structure
```markdown
AccruentInventoryControl/
 ├── .github/ # Github Action pipeline files
 ├── API/
 │   └── AccruentInventoryControl/ # API project
 ├── Documents/ # Documentations
 │   ├── DeveloperNotes/ # Developer Notes
 │   ├── Diagrams/ # Diagram images
 │   └── TechnicalDocumentation/ # Technical Documentation
 ├── Infra/ # Infrastructure Scripts
 │   ├── Azure/ # Azure Terraform scripts
 │   └── Docker/ # Docker Terraform scripts
 ├── UI/
 │   └── accruent-inventory-control/ # UI project
 ├── startup.ps1 # Script to start both API and UI
 └── docker-compose.yml # Docker compose file
```
