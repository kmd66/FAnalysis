USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetBiggerThanSDBetweenID') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetBiggerThanSDBetweenID
GO
CREATE PROCEDURE pbl.spGetBiggerThanSDBetweenID
	@AFromID BIGINT,
	@AToID BIGINT,
	@AType TINYINT

AS
BEGIN
	
	DECLARE 
		@FromID BIGINT = @AFromID,
		@ToID BIGINT = @AToID,
		@Type TINYINT = @AType
	
	SELECT * FROM pbl.BiggerThanSD
	WHERE 
		[Type] = @Type 
		AND PriceID >= @FromID
		AND PriceID <= @AToID
END 

