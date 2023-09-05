USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddMovingAverages') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddMovingAverages
GO
CREATE PROCEDURE pbl.spAddMovingAverages
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	--DECLARE @Json NVARCHAR(MAX) ='[11683561960000,11683562020000,11683561240000,11683561300000,11683561360000,11683561420000,11683561480000,11683561540000,11683561600000,11683561660000,11683561720000,11683561780000,11683561840000,11683561900000]'
	
	INSERT INTO [pbl].[MovingAverage] (ID, M5, M10, M20, M30, H1)
	SELECT 
		t2.ID, 
		pbl.fnAddMovingAverage(t2.ID, 1, 5),
		pbl.fnAddMovingAverage(t2.ID, 1, 10),
		pbl.fnAddMovingAverage(t2.ID, 1, 20),
		pbl.fnAddMovingAverage(t2.ID, 1, 30),
		pbl.fnAddMovingAverage(t2.ID, 1, 60)
	FROM OPENJSON(@Json) t1
	INNER JOIN pbl.PriceMinutely t2 ON t1.value = t2.ID
	LEFT JOIN pbl.MovingAverage t3 ON t3.ID = t2.ID
	WHERE t3.ID IS NULL AND t2.[Type] = 1


END 
