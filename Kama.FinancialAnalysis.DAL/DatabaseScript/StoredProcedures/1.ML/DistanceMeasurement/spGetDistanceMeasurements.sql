USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetDistanceMeasurements') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetDistanceMeasurements
GO
CREATE PROCEDURE pbl.spGetDistanceMeasurements
	@AType TINYINT,
	@ASession TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType,
		@Session TINYINT = @ASession
	
	;WITH Main AS(
	SELECT 
		b.ID,
		b.R1000,
		b.PriceID,
		p.[Close],
		p.[Open],
		pMAx.[Close] pMAxClose,
		pMin.[Close] pMinClose,
		m.D
	FROM [pbl].[BiggerThanSD] b
	INNER JOIN pbl.PriceMinutely pMAx ON pMAx.ID = b.MaxPriceID
	INNER JOIN pbl.PriceMinutely pMin ON pMin.ID = b.MinPriceID
	INNER JOIN pbl.PriceMinutely p ON p.ID = b.PriceID
	INNER JOIN pbl.MovingAverage m ON m.ID = p.ID
	WHERE b.Session = @ASession 
		AND b.Type = @Type
	)
	SELECT 
		ID,
		ROUND (([Close] - D) / R1000, 10) Macd,
		ROUND (([Close] - [Open])/ R1000, 10) R,
		ROUND ((pMAxClose - [Close])/ R1000, 10) Up,
		ROUND (([Close] - pMinClose)/ R1000, 10) Down
	FROM Main

END 