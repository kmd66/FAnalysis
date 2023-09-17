USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetPriceViewBases') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetPriceViewBases
GO

CREATE PROCEDURE pbl.spGetPriceViewBases
	@AType TINYINT,
	@AFromDate DATETIME,
	@AToDate DATETIME
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@Type TINYINT = @AType,
		@FromDate DATETIME = @AFromDate,
		@ToDate DATETIME = @AToDate
	
	;WITH sd AS(
		SELECT distinct 
			CONVERT(VARCHAR(8),[Date],108) Time
			,[R100]
			,[R500]
			,[R1000]
		FROM [Kama.FinancialAnalysis].[pbl].[StandardDeviation]
		WHERE Type = @Type
	),pr AS(
		SELECT
			P.*,
			M5, M30, H1, D
			--pbl.fnAscendingOrDescending(ID, @Type, [Close]) [Asc]
		FROM [pbl].PriceMinutely p
		INNER JOIN pbl.MovingAverage m on p.ID = m.ID
		WHERE p.[Type] = 1
			AND p.[Date] >= @FromDate
			AND p.[Date] <= @ToDate
	)
	SELECT 
		pr.*
		,[R100]
		,[R500]
		,[R1000]
	FROM pr
	INNER JOIN sd ON sd.Time = CONVERT(VARCHAR(8),pr.[Date],108)
	ORDER BY ID DESC
END 
