USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetStandardDeviation3') IS NOT NULL DROP FUNCTION pbl.fnGetStandardDeviation3
GO

CREATE FUNCTION pbl.fnGetStandardDeviation3(@ID BIGINT, @Type TINYINT, @Reng INT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @StandardDeviation FLOAT

;with a as(
	SELECT TOP(@Reng) [open], [Close]FROM [pbl].[PriceMinutely]
	WHERE ID <= @ID AND [Type] = @Type
	ORDER BY ID DESC

)
select @StandardDeviation= STDEV([close]-[open])  from a
	
	RETURN ROUND (@StandardDeviation ,6) 

END
GO