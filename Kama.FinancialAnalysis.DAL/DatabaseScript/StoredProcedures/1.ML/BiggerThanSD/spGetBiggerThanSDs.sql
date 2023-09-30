USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetBiggerThanSDs') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetBiggerThanSDs
GO
CREATE PROCEDURE pbl.spGetBiggerThanSDs
	@AType TINYINT,
	@ASession TINYINT
AS
BEGIN
	
	DECLARE 
		@Type TINYINT = @AType,
		@Session TINYINT = @ASession
	
	;WITH p AS (
		SELECT 
			*
			, ABS([CLOSE] - [OPEN]) Rate
			,CONVERT(VARCHAR(8),[date],108) Time
		FROM [pbl].PriceMinutely
		WHERE Type = @Type
	), 
	t AS (
		SELECT DISTINCT
			CONVERT(VARCHAR(8),[date],108) Time
			,R1000 R1000
		FROM [pbl].[StandardDeviation]
		WHERE Type = @Type
	
	), 
	s AS (
		SELECT DISTINCT	
			[Open] sessionsOpen, [Close] sessionsClose
		FROM [pbl].[Sessions]
		WHERE ID =@Session
	), mainData as (
		SELECT DISTINCT	
			 P.*,
			 R1000
		from P 
		INNER JOIN T on t.Time = p.Time
	)
	SELECT DISTINCT 
		mainData.ID PriceID,
		mainData.R1000,
		mainData.Rate,
		@Type Type,
		@Session Session
	FROM mainData
	INNER JOIN s on 1  = 1
	WHERE Rate > R1000
	AND Time > sessionsOpen
	AND Time < sessionsClose
	ORDER BY ID DESC

END 