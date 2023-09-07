USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddAllStandardDeviation') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddAllStandardDeviation
GO
CREATE PROCEDURE pbl.spAddAllStandardDeviation
AS
BEGIN

	INSERT INTO [pbl].StandardDeviation (ID, M10, M30, H1, H12, D1,p1000)
	SELECT 
		t1.ID, 
		0, --pbl.fnGetStandardDeviation(t1.ID,1,10),
		0, --pbl.fnGetStandardDeviation(t1.ID,1,30),
		0, --pbl.fnGetStandardDeviation(t1.ID,1,60),
		0, --pbl.fnGetStandardDeviation(t1.ID,1,(60*12)),
		0, --pbl.fnGetStandardDeviation(t1.ID,1,(60*24))
		pbl.fnGetStandardDeviation2(t1.ID , t1.Type, 1000)
	FROM pbl.PriceMinutely t1
	LEFT JOIN pbl.StandardDeviation t2 ON t1.ID = t2.ID
	WHERE t2.ID IS NULL AND t1.[Type] = 1

END 
