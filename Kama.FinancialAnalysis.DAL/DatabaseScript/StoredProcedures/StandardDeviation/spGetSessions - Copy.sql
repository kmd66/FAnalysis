USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetSessions') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetSessions
GO
CREATE PROCEDURE pbl.spGetSessions
AS
BEGIN
	
	SELECT 
		ID, CAST([Open] AS nvarchar(MAX)) [Open], CAST([Close] AS nvarchar(MAX)) [Close]
	FROM [pbl].[Sessions]
	
END 
