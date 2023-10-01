USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnAscendingPrice2') IS NOT NULL DROP FUNCTION pbl.fnAscendingPrice2
GO

CREATE FUNCTION pbl.fnAscendingPrice2(@Type TINYINT, @ID BIGINT, @Top INT)
RETURNS BIT
AS
BEGIN

	
	DECLARE @Ma FLOAT
	;WITH M AS(
		SELECT TOP(@Top) [Open], [Close] FROM PBL.PriceMinutely WHERE ID < @ID AND Type = @Type ORDER BY ID DESC
	)
	SELECT @Ma = AVG([Close])- AVG([Open]) FROM M

	IF @Ma > 0
		RETURN 1
	RETURN 0

END
GO
