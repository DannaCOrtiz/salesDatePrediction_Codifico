USE [StoreSample]
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesDatePrediction_Commands]    Script Date: 27/01/2025 3:52:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================================================================================================
-- Author:		Danna Camargo
-- Create date: 2024-01-06
-- Description:	Stored procedure that contains the creation of a new order and the addition of a product to that order
-- =======================================================================================================================================
ALTER PROCEDURE [dbo].[SP_SalesDatePrediction_Commands]
	-- Add the parameters for the stored procedure here
	@custid INT,
	@EmpID INT,
	@ShipperID INT,
	@ShipName NVARCHAR(255),
	@ShipAddress NVARCHAR(255),
	@ShipCity NVARCHAR(100),
	@OrderDate DATE,
	@RequiredDate DATE,
	@ShippedDate DATE,
	@Freight DECIMAL(10, 2),
	@ShipCountry NVARCHAR(100),
	@ProductID INT,
	@UnitPrice DECIMAL(10, 2),
	@Qty INT,
	@Discount DECIMAL(5, 2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRANSACTION;
			DECLARE @OrderID INT;
			--
			INSERT INTO Sales.Orders
					   (custid
					   ,empid
					   ,orderdate
					   ,requireddate
					   ,shippeddate
					   ,shipperid
					   ,freight
					   ,shipname
					   ,shipaddress
					   ,shipcity
					   ,shipcountry)
				 VALUES
					   (@custid
					   ,@empid
					   ,@orderdate
					   ,@requireddate
					   ,@shippeddate
					   ,@shipperid
					   ,@freight
					   ,@shipname
					   ,@shipaddress
					   ,@shipcity
					   ,@shipcountry)

			SET @OrderID = SCOPE_IDENTITY();

			INSERT INTO Sales.OrderDetails
					   (orderid
					   ,productid
					   ,unitprice
					   ,qty
					   ,discount)
				 VALUES
					   (@OrderID
					   ,@productid
					   ,@unitprice
					   ,@qty
					   ,@discount)

		SELECT 1
		--
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
			SELECT 0
	THROW;
	END CATCH;

END
