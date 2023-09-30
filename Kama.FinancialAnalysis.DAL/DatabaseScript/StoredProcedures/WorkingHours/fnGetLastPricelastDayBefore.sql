USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetLastPricelastDayBefore') IS NOT NULL DROP FUNCTION pbl.fnGetLastPricelastDayBefore
GO

CREATE FUNCTION pbl.fnGetLastPricelastDayBefore(@Dete DateTime, @Type tinyint)
RETURNS FLOAT
AS
BEGIN
	DECLARE  @f FLOAT,
		@D DateTime
	SET @D = DATEADD("mi", -1, @Dete)
	SET @f = (select top 1 [close] from pbl.PriceMinutely where Date < @D and Type = @Type order by id desc)
	IF @f IS NULL
		SET @f = 0;

	RETURN @f

END
GO