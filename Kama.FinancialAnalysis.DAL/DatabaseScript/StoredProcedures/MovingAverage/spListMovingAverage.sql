USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spListMovingAverage') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spListMovingAverage
GO
CREATE PROCEDURE pbl.spListMovingAverage
	@AType TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType
		
	SELECT * FROM pbl.MovingAverage
	WHERE @Type =0 OR [Type]= @Type
	
END 
