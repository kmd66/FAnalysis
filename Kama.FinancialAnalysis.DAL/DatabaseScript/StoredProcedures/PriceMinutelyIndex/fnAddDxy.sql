USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnAddDxy') IS NOT NULL DROP FUNCTION pbl.fnAddDxy
GO

CREATE FUNCTION pbl.fnAddDxy(@ID BIGINT, @Date DATETIME, @p1 FLOAT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @p3 FLOAT, @p6 FLOAT, @p7 FLOAT, @p8 FLOAT, @p9 FLOAT
	
	
	SET @p3 = (SELECT [Close] FROM [pbl].[PriceMinutely] WHERE Type = 3 AND [Date] = @Date)-- usd chf
	SET @p6 = (SELECT [Close] FROM [pbl].[PriceMinutelyOther] WHERE Type = 6 AND [Date] = @Date)-- usd jpy
	SET @p7 = (SELECT [Close] FROM [pbl].[PriceMinutelyOther] WHERE Type = 7 AND [Date] = @Date)-- gbp usd
	SET @p8 = (SELECT [Close] FROM [pbl].[PriceMinutelyOther] WHERE Type = 8 AND [Date] = @Date)-- usd cad
	SET @p9 = (SELECT [Close] FROM [pbl].[PriceMinutelyOther] WHERE Type = 9 AND [Date] = @Date)-- usd sek

	IF @p1 > 0 AND @p3 > 0 AND @p6 > 0 AND @p7 > 0 AND @p8 > 0 AND @p9 > 0
	BEGIN
		DECLARE @p FLOAT = 50.14348112 * POWER(@p1, -0.576) * POWER( @p6, 0.136 )* POWER(@p7, -0.119 )* POWER(@p8, 0.091 )* POWER(@p9, 0.042 ) * POWER( @p3, 0.0361)	
		RETURN @p
	
	END
	
	RETURN 0

END
GO
