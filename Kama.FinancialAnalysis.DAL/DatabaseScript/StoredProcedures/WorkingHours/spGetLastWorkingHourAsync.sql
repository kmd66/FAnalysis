USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetLastWorkingHourAsync') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetLastWorkingHourAsync
GO

CREATE PROCEDURE pbl.spGetLastWorkingHourAsync
AS
BEGIN
	SELECT TOP 1 * FROM pbl.WorkingHours 
	ORDER BY ID DESC

END 
