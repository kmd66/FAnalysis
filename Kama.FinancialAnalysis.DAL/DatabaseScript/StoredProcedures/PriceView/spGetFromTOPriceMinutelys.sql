USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetFromTOPriceMinutelys') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetFromTOPriceMinutelys
GO

CREATE PROCEDURE pbl.spGetFromTOPriceMinutelys
	@AType TINYINT,
	@AFromDate DATETIME,
	@AToDate DATETIME
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@Type TINYINT = @AType,
		@FromDate DATETIME = @AFromDate,
		@ToDate DATETIME = @AToDate
	
	
	SELECT
		P.*
	FROM [pbl].PriceMinutely p
	WHERE p.[Type] = @Type
		AND p.[Date] >= @FromDate
		AND p.[Date] <= @ToDate
	ORDER BY ID DESC
END 
