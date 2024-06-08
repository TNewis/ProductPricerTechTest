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

--EXEC AddNewProduct @ProductName = 'test2', @Price = 2.31