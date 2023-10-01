USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnAscendingPrice') IS NOT NULL DROP FUNCTION pbl.fnAscendingPrice
GO

CREATE FUNCTION pbl.fnAscendingPrice(@Open FLOAT, @Close FLOAT)
RETURNS BIT
AS
BEGIN

	IF @Open < @Close
		RETURN 1
	RETURN 0

END
GO
