USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddAllMovingAverage') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddAllMovingAverage
GO
CREATE PROCEDURE pbl.spAddAllMovingAverage
AS
BEGIN

	INSERT INTO [pbl].[MovingAverage] (ID, M5, M10, M20, M30, H1)
	SELECT 
		t1.ID, 
		pbl.fnAddMovingAverage(t1.ID, 1, 5),
		pbl.fnAddMovingAverage(t1.ID, 1, 10),
		pbl.fnAddMovingAverage(t1.ID, 1, 20),
		pbl.fnAddMovingAverage(t1.ID, 1, 30),
		pbl.fnAddMovingAverage(t1.ID, 1, 60)
	FROM pbl.PriceMinutely t1
	LEFT JOIN pbl.MovingAverage t2 ON t1.ID = t2.ID
	WHERE t2.ID IS NULL AND t1.[Type] = 1

END 
