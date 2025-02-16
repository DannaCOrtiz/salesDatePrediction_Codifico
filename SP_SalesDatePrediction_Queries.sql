USE [StoreSample]
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesDatePrediction_Queries]    Script Date: 27/01/2025 3:53:45 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===========================================================================================================================================
-- Author:		Danna Camargo
-- Create date: 26/01/2024
-- Description:	Stored procedure containing Sales Date Prediction queries,HR.Employees,Sales.Shippers,Production.Products
-- ===========================================================================================================================================
ALTER PROCEDURE [dbo].[SP_SalesDatePrediction_Queries]
	-- Add the parameters for the stored procedure here
	@Option INT,
	@custid INT = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here}
	--======================================
	--SALES DATE PREDICTION
	--======================================
	IF(@Option = 100)
	BEGIN 
	WITH CTE AS (
		SELECT ord.custid,
			   ord.orderdate,
			   ISNULL(LAG(ord.orderdate) OVER (PARTITION BY ord.custid ORDER BY ord.orderdate),orderdate) AS PreviousDate
		  FROM Sales.Orders ord
		),
		Averages AS (
			SELECT custid, 
				   AVG(DATEDIFF(DAY, PreviousDate, orderdate)) AS AverageDays
			  FROM CTE
			 WHERE PreviousDate IS NOT NULL  
			 GROUP BY custid
		),
		LastOrder AS (
			SELECT custid, 
				   MAX(orderdate) AS LastDateOrder
			  FROM Sales.Orders
			 GROUP BY custid
		)
		SELECT cust.companyname AS CustomerName,
			   lo.LastDateOrder AS LastOrderDate,
			   DATEADD(DAY, avgs.AverageDays, lo.LastDateOrder)  AS NextPredictedOrder
		  FROM LastOrder lo
		 INNER JOIN Averages avgs ON lo.custid = avgs.custid
		 INNER JOIN Sales.Customers cust ON lo.custid = cust.custid
		 ORDER BY cust.companyname;  
	END
	--======================================
	--GET CLIENT ORDERS
	--======================================
	ELSE IF (@Option = 200)
	BEGIN 
		SELECT orderid,
		       requireddate,
			   shippeddate,
			   shipname,
			   shipaddress,
			   shipcity
		FROM Sales.Orders
		WHERE custid =  @custid
		
	END
	ELSE IF(@Option = 300)
	BEGIN 
		SELECT empid,
		       CONCAT(firstname,' ',lastname) AS fullName
		  FROM HR.Employees
	END
	--======================================
	--GET SHIPPERS
	--======================================
	ELSE IF (@Option = 400)
	BEGIN 
		SELECT shipperid,
		       companyname
		  FROM Sales.Shippers
	END
	--======================================
	--GET PRODUCTS
	--======================================
	ELSE IF(@Option = 500)
	BEGIN
		SELECT productid,
		       productname
		  FROM Production.Products
	END
    --======================================
	--GET CUSTOMERS
	--======================================
	ELSE IF(@Option = 600)
	BEGIN 
		SELECT custid,
		       companyname,
			   contactname,
			   contacttitle,
			   address,
			   city,
			   region,
			   postalcode,
			   country,
			   phone,
			   fax
		  FROM Sales.Customers
	END
	--======================================
	--GET EMPLOYEES
	--======================================
	ELSE IF(@Option = 700)
	BEGIN 
		  SELECT empid,	
		         concat(firstname,' ',lastname) as EmployeeName,
		         title,
		         titleofcourtesy,
		         birthdate,
		         hiredate,
		         address,
		         city,
		         region,
		         postalcode,
		         country,
		         phone,
		         mgrid
		    FROM HR.Employees
	END
	--======================================
	--GET ORDERS
	--======================================
	ELSE IF(@Option = 800)
	BEGIN 
		SELECT orderid,
			   custid,
			   empid,
			   orderdate,
			   requireddate,
			   shippeddate,
			   shipperid,
			   freight,
			   shipname,
			   shipaddress,
			   shipcity,
			   shipregion,
			   shippostalcode,
			   shipcountry
		  FROM Sales.Orders
		  ORDER BY custid ASC
	END
END

