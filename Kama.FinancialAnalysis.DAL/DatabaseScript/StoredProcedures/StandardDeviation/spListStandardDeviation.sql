USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spListStandardDeviation') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spListStandardDeviation
GO
CREATE PROCEDURE pbl.spListStandardDeviation
	@AType TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType
		
	SELECT * FROM pbl.StandardDeviation
	WHERE @Type =0 OR [Type]= @Type
	
END 
