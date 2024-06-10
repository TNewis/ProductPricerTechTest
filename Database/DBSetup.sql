IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ProductPricer')
BEGIN
CREATE DATABASE [ProductPricer]
END
GO

USE [ProductPricer]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Product' and xtype='U')
BEGIN
    CREATE TABLE Product (
        Id INT PRIMARY KEY IDENTITY (1, 1),
		Guid UNIQUEIDENTIFIER,
        Name VARCHAR(100),
		Price DECIMAL(10,2)
    )
END

IF EXISTS (SELECT * FROM sysobjects WHERE name='AddNewProduct' and xtype='P')
BEGIN
DROP PROCEDURE AddNewProduct
END
GO

CREATE PROCEDURE AddNewProduct @ProductName VARCHAR(100), @Price DECIMAL(10,2)
AS
DECLARE @guid UNIQUEIDENTIFIER = NEWID();
INSERT INTO dbo.Product (Guid, Name, Price)
VALUES (@guid, @ProductName, @Price)

SELECT Guid, Name, Price FROM dbo.Product WHERE Guid = @guid;
GO