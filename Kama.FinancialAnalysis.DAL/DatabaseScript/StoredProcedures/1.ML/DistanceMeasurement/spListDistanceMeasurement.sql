USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spListDistanceMeasurement') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spListDistanceMeasurement
GO
CREATE PROCEDURE pbl.spListDistanceMeasurement
	@AType TINYINT,
	@ASession TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType,
		@Session TINYINT = @ASession
	
	SELECT * FROM pbl.DistanceMeasurement
	WHERE (@Type = 0 OR [Type] = @Type)
		AND (@Session = 0 OR [Session] = @Session)

END 