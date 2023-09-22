USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetDxyOpen') IS NOT NULL DROP FUNCTION pbl.fnGetDxyOpen
GO

CREATE FUNCTION pbl.fnGetDxyOpen(@ID BIGINT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @Open FLOAT
	SET @Open = (SELECT TOP 1 [Close] FROM [pbl].PriceMinutelyIndex WHERE ID < @ID AND [Type] = 11 )
	
	IF @Open IS NULL
	BEGIN
		SET @Open = (SELECT TOP 1 [Close] FROM [pbl].PriceMinutelyIndex WHERE ID  = @ID AND [Type] = 11 )
	END
  
  RETURN @Open

END
GO
