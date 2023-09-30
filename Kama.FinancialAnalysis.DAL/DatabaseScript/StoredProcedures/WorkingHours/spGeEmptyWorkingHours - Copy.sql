USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetLastPricelastDayBefore') IS NOT NULL DROP FUNCTION pbl.fnGetLastPricelastDayBefore
GO

CREATE FUNCTION pbl.fnGetLastPricelastDayBefore(@Date NVARCHAR(MAX), @Type TINYINT)
RETURNS BIGINT
AS
BEGIN
	DECLARE @P BIGINT; 
	SET  @P = (SELECT TOP 1 [Close] FROM pbl.PriceMinutely WHERE [Type] = @Type AND Date < @Date ORDER BY ID DESC)
	RETURN @P

END
GO