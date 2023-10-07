USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetMovingAverage') IS NOT NULL DROP FUNCTION pbl.fnGetMovingAverage
GO

CREATE FUNCTION pbl.fnGetMovingAverage(@Type TINYINT, @ID BIGINT, @Top INT)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @Ma FLOAT
	;WITH M AS(
		SELECT TOP(@Top) [Close] FROM PBL.PriceMinutely WHERE ID < @ID AND Type = @Type ORDER BY ID DESC
	)
	SELECT @Ma = AVG([Close])  FROM M
	if @Ma is null
		RETURN 0
	
	RETURN @Ma
END
GO
