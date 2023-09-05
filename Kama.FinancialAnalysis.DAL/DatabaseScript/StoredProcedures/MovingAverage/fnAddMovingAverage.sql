USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnAddMovingAverage') IS NOT NULL DROP FUNCTION pbl.fnAddMovingAverage
GO

CREATE FUNCTION pbl.fnAddMovingAverage(@ID BIGINT, @Type TINYINT, @Reng INT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @M5 FLOAT
	SET @M5 = (SELECT TOP 1 M5 FROM [pbl].[MovingAverage] WHERE ID = @ID )
	
	IF @M5 IS NULL
	BEGIN
		;WITH SelectItem AS (
			SELECT TOP(@Reng) [Close] FROM [pbl].[PriceMinutely]
			WHERE ID <= @ID AND [Type] = @Type
			ORDER BY ID DESC
		)
		--INSERT INTO [pbl].[MovingAverage] (ID, M5)
		SELECT @M5 = ROUND (SUM([Close]) / COUNT([Close]),6)  FROM SelectItem 
	END
  
  RETURN @M5

END
GO
