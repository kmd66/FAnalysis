USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGeEmptyWorkingHours') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGeEmptyWorkingHours
GO

CREATE PROCEDURE pbl.spGeEmptyWorkingHours
AS
BEGIN
	;with DailyDate as (
		SELECT distinct 
			cast ([Date] as DATE) [Date]
		FROM [pbl].[PriceMinutely] 
		WHERE [Type] = 1
	),
	AddID As(
		SELECT  
			dbo.fnTimeStamp([Date] ) ID,[Date]
		FROM DailyDate
	)
	SELECT 
		aid.* 
	FROM AddID aid
	LEFT JOIN [pbl].[WorkingHours] wkh ON aid.ID = wkh.ID
	WHERE wkh.ID IS NULL

END 
