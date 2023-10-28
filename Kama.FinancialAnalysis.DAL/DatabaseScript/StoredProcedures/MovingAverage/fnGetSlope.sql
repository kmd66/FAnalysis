USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetSlope') IS NOT NULL DROP FUNCTION pbl.fnGetSlope
GO


CREATE FUNCTION pbl.fnGetSlope(@Type TINYINT, @ID BIGINT, @Top INT)
RETURNS FLOAT
AS
BEGIN
	
	DECLARE @Ma FLOAT
	
	
	;WITH M AS(
		SELECT TOP(@Top)  ID,[Close],ROW_NUMBER() OVER(ORDER BY ID DESC) r FROM PBL.PriceMinutely WHERE ID <= @ID AND Type = @Type ORDER BY ID DESC
	) ,F AS(
		SELECT TOP 1 [Close] FROM M 
		ORDER BY r 
	),L AS(
		SELECT TOP 1 [Close] FROM M 
		ORDER BY r DESC
	)
	SELECT @Ma= ROUND(F.[Close]- L.[Close], 8) FROM F 
	INNER JOIN L ON 1=1
	
	--;WITH M AS(
	--	SELECT TOP(@Top)  ID,[Close] FROM PBL.PriceMinutely WHERE ID <= @ID AND Type = @Type ORDER BY ID DESC
	--) ,M2 AS(
	--	SELECT *,ROW_NUMBER() OVER(ORDER BY ID DESC) r from m
	--) ,F AS(
	--	SELECT TOP 1 m2.*, D FROM M2 
	--	inner join pbl.MovingAverage m on M2.ID = m.ID
	--	ORDER BY r 
	--),L AS(
	--	SELECT TOP 1 m2.*, D FROM M2 
	--	inner join pbl.MovingAverage m on M2.ID = m.ID
	--	ORDER BY r DESC
	--)
	--SELECT @Ma= ROUND(F.D- L.D, 8) FROM F 
	--INNER JOIN L ON 1=1

	--;WITH M AS(
	--	SELECT TOP(@Top)  ID,[Close] FROM PBL.PriceMinutely WHERE ID <= @ID AND Type = @Type ORDER BY ID DESC
	--) ,M2 AS(
	--	SELECT *,ROW_NUMBER() OVER(ORDER BY ID DESC) r from m
	--) ,F AS(
	--	SELECT TOP 1 * FROM M2 ORDER BY r 
	--),L AS(
	--	SELECT TOP 1 * FROM M2 ORDER BY r DESC
	--)
	--SELECT @Ma= ROUND(F.[Close] - L.[Close], 8) FROM F 
	--INNER JOIN L ON 1=1

	--SELECT ROUND(SQRT( POWER(50,2)+ POWER((F.[Close] - L.[Close]),2)), 8),F.[Close] - L.[Close] FROM F 
	--INNER JOIN L ON 1=1

	RETURN @Ma

END
GO
