USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetStandardDeviation2') IS NOT NULL DROP FUNCTION pbl.fnGetStandardDeviation2
GO

CREATE FUNCTION pbl.fnGetStandardDeviation2(@ID BIGINT, @Type TINYINT, @Reng INT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @Time CHAR(8)
	DECLARE @Daily TABLE (ID BIGINT, [Close] FLOAT, [Time] CHAR(8), Variance FLOAT)
	DECLARE	@Average FLOAT, @StandardDeviation FLOAT
	
	SELECT top 1 
		@Time = convert(time(0),[DATE]) 
		from pbl.PriceMinutely
	WHERE  ID = @ID AND [Type] = @Type
	ORDER BY ID DESC
	
	INSERT INTO @Daily
	SELECT TOP(@Reng) ID, [close] - [open], convert(time(0),[DATE]) [Time],  0.0 FROM [pbl].[PriceMinutely]
	WHERE ID <= @ID AND [Type] = @Type
		AND convert(time(0),[DATE]) = @Time
	ORDER BY ID DESC
	
	SET @Average = (SELECT ROUND (SUM([Close]) / COUNT([Close]),6)  FROM @Daily )
	
	UPDATE @Daily SET Variance = [Close] - @Average
	UPDATE @Daily SET Variance = POWER(Variance, 2)
	
	SET  @StandardDeviation= (select SQRT(SUM(Variance) /COUNT(Variance))  from @Daily )
	
	RETURN ROUND (@StandardDeviation ,6) 

END
GO
