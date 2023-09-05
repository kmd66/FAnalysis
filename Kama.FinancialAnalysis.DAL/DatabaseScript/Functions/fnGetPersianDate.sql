USE [Kama.FinancialAnalysis]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetPersianDate]    Script Date: 4/4/2022 2:49:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER FUNCTION [dbo].[fnGetPersianDate] (@ACristianDate AS DATETIME = NULL)
RETURNS NVARCHAR(10)
 
AS
BEGIN
	IF @ACristianDate IS NULL
		SET @ACristianDate = GETDATE()

DECLARE @shYear AS INT ,@shMonth AS INT ,@shDay AS INT ,@intYY AS INT ,@intMM AS INT ,@intDD AS INT ,@Kabiseh1 AS INT ,@Kabiseh2 AS INT ,@d1 AS INT ,@m1 AS INT, @shMaah AS NVARCHAR(max),@shRooz AS NVARCHAR(max), @strShDay AS NVARCHAR(max)
DECLARE @DayDate AS NVARCHAR(max)

SET @intYY = DATEPART(yyyy, @ACristianDate)

IF @intYY < 1000 SET @intYY = @intYY + 2000

SET @intMM = MONTH(@ACristianDate)
SET @intDD = DAY(@ACristianDate)
SET @shYear = @intYY - 622

SET @m1 = 1
SET @d1 = 1
SET @shMonth = 10
SET @shDay = 11

IF ( ( @intYY - 1993 ) % 4 = 0 ) SET @shDay = 12

WHILE ( @m1 != @intMM ) OR ( @d1 != @intDD )
BEGIN

  SET @d1 = @d1 + 1

  IF ( ( @intYY - 1992 ) % 4 = 0) SET @Kabiseh1 = 1 ELSE SET @Kabiseh1 = 0

  IF ( ( @shYear - 1371 ) % 4 = 0) SET @Kabiseh2 = 1 ELSE SET @Kabiseh2 = 0

  IF
  (@d1 = 32 AND (@m1 = 1 OR @m1 = 3 OR @m1 = 5 OR @m1 = 7 OR @m1 = 8 OR @m1 = 10 OR @m1 = 12))
  OR
  (@d1 = 31 AND (@m1 = 4 OR @m1 = 6 OR @m1 = 9 OR @m1 = 11))
  OR
  (@d1 = 30 AND @m1 = 2 AND @Kabiseh1 = 1)
  OR
  (@d1 = 29 AND @m1 = 2 AND @Kabiseh1 = 0)
  BEGIN
    SET @m1 = @m1 + 1
    SET @d1 = 1
  END

  IF @m1 > 12
  BEGIN
    SET @intYY = @intYY + 1
    SET @m1 = 1
  END
 

 SET @shDay = @shDay + 1


  IF
  (@shDay = 32 AND @shMonth < 7)
  OR
  (@shDay = 31 AND @shMonth > 6 AND @shMonth < 12)
  OR
  (@shDay = 31 AND @shMonth = 12 AND @Kabiseh2 = 1)
  OR
  (@shDay = 30 AND @shMonth = 12 AND @Kabiseh2 = 0)
  BEGIN
    SET @shMonth = @shMonth + 1
    SET @shDay = 1
  END

  IF @shMonth > 12
  BEGIN
    SET @shYear = @shYear + 1
    SET @shMonth = 1
  END
 
END

IF @shMonth = 1 SET @shMaah=N'01'
IF @shMonth = 2 SET @shMaah=N'02'
IF @shMonth = 3 SET @shMaah=N'03'
IF @shMonth = 4 SET @shMaah=N'04'
IF @shMonth = 5 SET @shMaah=N'05'
IF @shMonth = 6 SET @shMaah=N'06'
IF @shMonth = 7 SET @shMaah=N'07'
IF @shMonth = 8 SET @shMaah=N'08'
IF @shMonth = 9 SET @shMaah=N'09'
IF @shMonth = 10 SET @shMaah=N'10'
IF @shMonth = 11 SET @shMaah=N'11'
IF @shMonth = 12 SET @shMaah=N'12'



IF LTRIM(STR(@shDay, 2)) = '1' SET @strShDay = '01'
ELSE IF LTRIM(STR(@shDay, 2)) = '2' SET @strShDay = '02'
ELSE IF LTRIM(STR(@shDay, 2)) = '3' SET @strShDay = '03'
ELSE IF LTRIM(STR(@shDay, 2)) = '4' SET @strShDay = '04'
ELSE IF LTRIM(STR(@shDay, 2)) = '5' SET @strShDay = '05'
ELSE IF LTRIM(STR(@shDay, 2)) = '6' SET @strShDay = '06'
ELSE IF LTRIM(STR(@shDay, 2)) = '7' SET @strShDay = '07'
ELSE IF LTRIM(STR(@shDay, 2)) = '8' SET @strShDay = '08'
ELSE IF LTRIM(STR(@shDay, 2)) = '9' SET @strShDay = '09'
ELSE SET @strShDay = LTRIM(STR(@shDay, 2))


--SET @DayDate = @shRooz + '/' + LTRIM(STR(@shDay, 2)) + '/' + @shMaah + '/' + STR(@shYear,4)


SET @DayDate = STR(@shYear, 4) + '/' + @shMaah + '/' + @strShDay

	RETURN @DayDate
	
END

GO


