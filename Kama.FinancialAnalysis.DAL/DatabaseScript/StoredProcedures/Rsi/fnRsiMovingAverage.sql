USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnRsiMovingAverage') IS NOT NULL DROP FUNCTION pbl.fnRsiMovingAverage
GO

CREATE FUNCTION pbl.fnRsiMovingAverage(@Type TINYINT, @ID BIGINT, @Top INT)
RETURNS FLOAT
AS
BEGIN
	DECLARE @Ma FLOAT
	;WITH M AS(
		SELECT TOP(@Top) Value32 m FROM PBL.Rsi WHERE ID < @ID AND Type = @Type ORDER BY ID DESC
	)
	SELECT @Ma = AVG(m)  FROM M
	if @Ma is null
		RETURN 0
	
	
  RETURN @Ma
	
	
  RETURN @Ma

END
GO
