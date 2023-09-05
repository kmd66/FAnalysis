USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnAscendingOrDescending') IS NOT NULL DROP FUNCTION pbl.fnAscendingOrDescending
GO

CREATE FUNCTION pbl.fnAscendingOrDescending(@ID BIGINT, @Type TINYINT, @Close FLOAT)
RETURNS BIT
AS
BEGIN

	DECLARE @SelectClose FLOAT
	SET @SelectClose = (SELECT TOP 1 [Close] FROM [pbl].PriceMinutely WHERE ID < @ID AND [Type] = @Type  ORDER BY ID DESC  )
	
	IF @SelectClose IS NULL OR @SelectClose <= @Close
		RETURN 1
    
	RETURN 0

END
GO
