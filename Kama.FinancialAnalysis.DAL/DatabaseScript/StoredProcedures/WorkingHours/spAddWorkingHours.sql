USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddWorkingHours') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddWorkingHours
GO

CREATE PROCEDURE pbl.spAddWorkingHours
	@AType TINYINT
AS
BEGIN
	DECLARE  @Type TINYINT = @AType,
		 @Time NVARCHAR(MAX);
	SET @Time = (select top 1 CONVERT(varchar, [Close],120) from pbl.Sessions where ID = @Type)

	DECLARE @Daily TABLE ([Date] DATE)
	INSERT INTO @Daily
	EXEC pbl.spGeEmptyWorkingHours @AType = @Type

	;WITH a AS(
		SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, Date)) D from pbl.PriceMinutely 
		WHERE Type = @Type
	)
	
	,DistincA AS(
		SELECT DISTINCT D FROM a
	),Main AS(
		SELECT p.D,DATEADD(day, DATEDIFF(day, 0, p.D), @Time) T from @Daily d
		INNER JOIN DistincA p ON p.D = d.Date
		--WHERE p.D = @Type
	)
	INSERT INTO [pbl].[WorkingHours](ID, Date, Session, LndOpen, LndClose, NykOpen, NykClose, TkoOpen, TkoClose, Type)
	SELECT NEWID(), D, pbl.fnGetLastPricelastDayBefore(T, @Type), 0, 0, 0, 0, 0, 0, @Type FROM Main

END 
