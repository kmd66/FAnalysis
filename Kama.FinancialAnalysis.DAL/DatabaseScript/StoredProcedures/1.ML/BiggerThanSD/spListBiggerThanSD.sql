USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spListBiggerThanSD') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spListBiggerThanSD
GO
CREATE PROCEDURE pbl.spListBiggerThanSD
	@AType TINYINT,
	@ASession TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType,
		@Session TINYINT = @ASession
	
	SELECT * FROM pbl.BiggerThanSD
	WHERE (@Type = 0 OR [Type] = @Type)
		AND (@Session = 0 OR [Session] = @Session)
	ORDER BY PriceID DESC

END 