USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetPrveCandleRsi') IS NOT NULL DROP FUNCTION pbl.fnGetPrveCandleRsi
GO

CREATE FUNCTION pbl.fnGetPrveCandleRsi(@ID BIGINT)
RETURNS FLOAT
AS
BEGIN

	DECLARE @v14 FLOAT
	;with a as(
		select top 5 Value14 from pbl.Rsi where id <= @ID
		order by id desc
	)
	select @v14 = avg(Value14) from a
	
	RETURN @v14
END
GO