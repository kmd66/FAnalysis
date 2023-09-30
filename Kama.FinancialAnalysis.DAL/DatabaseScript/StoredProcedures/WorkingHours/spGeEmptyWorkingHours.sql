USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGeEmptyWorkingHours') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGeEmptyWorkingHours
GO

CREATE PROCEDURE pbl.spGeEmptyWorkingHours
	@AType TINYINT
AS
BEGIN
	
	DECLARE  @Type TINYINT = @AType
	
	;WITH DailyDate as (
		SELECT 
			DATEADD(dd, 0, DATEDIFF(dd, 0, Date)) d from pbl.PriceMinutely
		WHERE Type = @Type
	),Distinc as (
		SELECT DISTINCT d FROM DailyDate
	)
	SELECT d FROM Distinc
	LEFT JOIN pbl.WorkingHours w On Distinc.d = w.Date AND w.Type = @Type
	WHERE w.ID IS NULL
	ORDER BY d

END 
