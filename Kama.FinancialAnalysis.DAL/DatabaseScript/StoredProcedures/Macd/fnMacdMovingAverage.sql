USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnMacdMovingAverage') IS NOT NULL DROP FUNCTION pbl.fnMacdMovingAverage
GO

CREATE FUNCTION pbl.fnMacdMovingAverage(@Type TINYINT, @ID BIGINT, @Top INT)
RETURNS FLOAT
AS
BEGIN
	DECLARE @Ma FLOAT
	;WITH M AS(
		SELECT TOP(@Top) e60 - e130 m  FROM PBL.Macd WHERE ID < @ID AND Type = @Type ORDER BY ID DESC
	)
	SELECT @Ma = AVG(m)  FROM M
	if @Ma is null
		RETURN 0
	
	
  RETURN @Ma

END
GO
