USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spBiggerThanSD') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spBiggerThanSD
GO
CREATE PROCEDURE pbl.spBiggerThanSD
	@AType TINYINT,
	@ASessionID TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType,
		@SessionID TINYINT = @ASessionID
	
	;WITH p AS (
		SELECT 
			*
			, ABS([CLOSE] - [OPEN]) pABS
			,CONVERT(VARCHAR(8),[date],108) Time
		FROM [pbl].PriceMinutely
		WHERE Type = @Type
	), 
	t AS (
		SELECT DISTINCT
			CONVERT(VARCHAR(8),[date],108) Time
			,R1000 * 1.5 R1000
		FROM [pbl].[StandardDeviation]
		WHERE Type = @Type
	
	), 
	s AS (
		SELECT DISTINCT	
			[Open] sessionsOpen, [Close] sessionsClose
		FROM [pbl].[Sessions]
		WHERE ID =@SessionID
	), mainData as (
		SELECT DISTINCT	
			 P.*,
			 R1000
		from P 
		INNER JOIN T on t.Time = p.Time
	)
	SELECT DISTINCT mainData.* FROM mainData
	INNER JOIN s on 1  = 1
	WHERE pABS > R1000
	AND Time > sessionsOpen
	AND Time < sessionsClose
	ORDER BY ID DESC

END 