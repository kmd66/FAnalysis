USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddWorkingHours') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddWorkingHours
GO

CREATE PROCEDURE pbl.spAddWorkingHours
AS
BEGIN

	DECLARE @Daily TABLE (ID BIGINT, [Date] DATE)
	INSERT INTO @Daily
	exec pbl.spGeEmptyWorkingHours

	;WITH WorkingHour As(
		SELECT 
			*,
			DATEADD(Day, DATEDIFF(Day, 0, [Date]), '7:00:00.000') [LndOpen],
			DATEADD(Day, DATEDIFF(Day, 0, [Date]), '15:59:00.000') [LndClose],
			DATEADD(Day, DATEDIFF(Day, 0, [Date]), '12:00:00.000') [NykOpen],
			DATEADD(Day, DATEDIFF(Day, 0, [Date]), '20:59:00.000') [NykClose]
			--DATEADD(Day, DATEDIFF(Day, 1, [Date]), '23:00:00.000') [TkoOpen],
			--DATEADD(Day, DATEDIFF(Day, 0, [Date]), '7:59:00.000') [TkoClose]
		FROM @Daily
	),
	NewyorkOpen As(
		SELECT t1.ID,
			t2.[Open] Price
		FROM WorkingHour t1
		INNER JOIN [pbl].[PriceMinutely] t2 On t1.NykOpen = t2.Date
		WHERE [Type] = 1
	),
	NewyorkClose As(
		SELECT t1.ID,
			t2.[Close] Price
		FROM WorkingHour t1
		INNER JOIN [pbl].[PriceMinutely] t2 On t1.NykClose = t2.Date
		WHERE [Type] = 1
	),
	LondanOpen As(
		SELECT t1.ID,
			t2.[Open] Price
		FROM WorkingHour t1
		INNER JOIN [pbl].[PriceMinutely] t2 On t1.LndOpen = t2.Date
		WHERE [Type] = 1
	),
	LondanClose As(
		SELECT t1.ID,
			t2.[Close]  Price
		FROM WorkingHour t1
		INNER JOIN [pbl].[PriceMinutely] t2 On t1.LndClose = t2.Date
		WHERE [Type] = 1
	)
	INSERT INTO [pbl].[WorkingHours](ID, Date, LndOpen, LndClose, NykOpen, NykClose, TkoOpen, TkoClose)
	SELECT 
		t1.*,
		t2.Price NykOpen,
		t3.Price NykClose,
		t4.Price LndOpen,
		t5.Price LndClose, 
		0,0
	FROM @Daily t1
	INNER JOIN NewyorkOpen t2 ON t1.ID = t2.ID
	INNER JOIN NewyorkClose t3 ON t1.ID = t3.ID
	INNER JOIN LondanOpen t4 ON t1.ID = t4.ID
	INNER JOIN LondanClose t5 ON t1.ID = t5.ID

END 
