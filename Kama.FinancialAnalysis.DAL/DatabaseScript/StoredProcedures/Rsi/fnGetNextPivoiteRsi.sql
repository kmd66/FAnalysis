USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetNextPivoiteRsi') IS NOT NULL DROP FUNCTION pbl.fnGetNextPivoiteRsi
GO

CREATE FUNCTION pbl.fnGetNextPivoiteRsi(@ID BIGINT, @NextPivoteApproved BIGINT)
RETURNS BIGINT
AS
BEGIN

	DECLARE @v14 BIGINT

	select top 1 @v14 = ID from pbl.Rsi where id >@ID and id <@NextPivoteApproved and Value14 < 30
	
	RETURN @v14
END
GO