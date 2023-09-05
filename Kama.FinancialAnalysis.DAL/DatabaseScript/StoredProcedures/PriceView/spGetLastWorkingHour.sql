USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetLastWorkingHour') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetLastWorkingHour
GO

CREATE PROCEDURE pbl.spGetLastWorkingHour
--WITH ENCRYPTION
AS
BEGIN
    
	SELECT TOP (1) [ID]
	    ,[Date]
	    ,[LndOpen]
	    ,[LndClose]
	    ,[NykOpen]
	    ,[NykClose]
	    ,[TkoOpen]
	    ,[TkoClose]
	FROM [Kama.FinancialAnalysis].[pbl].[WorkingHours]
	ORDER BY ID DESC
			
END 
