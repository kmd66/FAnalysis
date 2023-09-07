USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddAllDxy') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddAllDxy
GO

CREATE PROCEDURE pbl.spAddAllDxy
AS
BEGIN
	INSERT INTO [pbl].[PriceMinutelyIndex]
	SELECT 
		P.ID, 
		p.[Date], 
		pbl.fnAddDxy(p.ID ,p.[Date],p.[Close]),
		11 
	from pbl.PriceMinutely p
	LEFT JOIN [pbl].[PriceMinutelyIndex] pin On p.ID = pin.ID
	WHERE p.[Type] = 1 
	AND pin.ID IS NULL

END 
