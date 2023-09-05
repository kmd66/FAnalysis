USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetStandardDeviation') IS NOT NULL DROP FUNCTION pbl.fnGetStandardDeviation
GO

CREATE FUNCTION pbl.fnGetStandardDeviation(@ID BIGINT, @Type TINYINT, @Reng INT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @Daily TABLE (ID BIGINT, [Close] FLOAT, Variance FLOAT)
	DECLARE	@Average FLOAT
		,@StandardDeviation FLOAT

	INSERT INTO @Daily
	SELECT TOP(@Reng) ID, [Close], 0.0 FROM [pbl].[PriceMinutely]
	WHERE ID <= @ID AND [Type] = @Type
	ORDER BY ID DESC

	SET @Average = (SELECT ROUND (SUM([Close]) / COUNT([Close]),6)  FROM @Daily )

	UPDATE @Daily SET Variance = [Close] - @Average
	UPDATE @Daily SET Variance = POWER(Variance, 2)

	SET  @StandardDeviation= (select SQRT(SUM(Variance) /COUNT(Variance))  from @Daily )
	
	RETURN ROUND (@StandardDeviation ,6) 

END
GO
