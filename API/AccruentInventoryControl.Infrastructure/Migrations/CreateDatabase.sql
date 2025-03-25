-- Create Product Table
CREATE TABLE Product (
    Id BIGINT PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    DeletedAt DATETIME NULL,
    Name NVARCHAR(255) NOT NULL,
    Code NVARCHAR(100) NOT NULL
);

-- Create WarehouseTransactionStatus Lookup Table
CREATE TABLE WarehouseTransactionStatus (
    StatusId INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

-- Seed Data for WarehouseTransactionStatus
INSERT INTO WarehouseTransactionStatus (StatusId, Name)
VALUES
(1, 'Pending'),
(2, 'OutOfStock'),
(3, 'Completed'),
(4, 'Cancelled');

-- Create WarehouseTransactionType Lookup Table
CREATE TABLE WarehouseTransactionType (
    TypeId INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

-- Seed Data for WarehouseTransactionType
INSERT INTO WarehouseTransactionType (TypeId, Name)
VALUES
(1, 'Inbound'),
(2, 'Outbound');

-- Create WarehouseTransaction Table
CREATE TABLE WarehouseTransaction (
    Id BIGINT PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    DeletedAt DATETIME NULL,
    ProductId BIGINT NOT NULL,
    Quantity INT NOT NULL,
    TypeId INT NOT NULL,
    StatusId INT NOT NULL,
    PreviousQuantity INT NOT NULL,
    TotalQuantity INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(Id),
    FOREIGN KEY (TypeId) REFERENCES WarehouseTransactionType(TypeId),
    FOREIGN KEY (StatusId) REFERENCES WarehouseTransactionStatus(StatusId)
);

-- Create WarehouseTransactionReport Table
CREATE TABLE WarehouseTransactionReport (
    Id BIGINT PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    DeletedAt DATETIME NULL,
    ProductCode NVARCHAR(100) NOT NULL,
    Date DATETIME NOT NULL,
    Balance INT NOT NULL
);

-- Relationship between WarehouseTransactionReport and WarehouseTransaction
ALTER TABLE WarehouseTransaction
ADD ReportId BIGINT NULL;

ALTER TABLE WarehouseTransaction
ADD FOREIGN KEY (ReportId) REFERENCES WarehouseTransactionReport(Id);