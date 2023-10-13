USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetCci') IS NOT NULL DROP FUNCTION pbl.fnGetCci
GO

CREATE FUNCTION pbl.fnGetCci(@ID BIGINT, @Type tinyint, @period int)
RETURNS float
AS
BEGIN
	
	DECLARE @typicalPrice DECIMAL(18, 2)
    DECLARE @meanTypicalPrice DECIMAL(18, 2)
    DECLARE @meanDeviation DECIMAL(18, 2)
    DECLARE @cci DECIMAL(18, 2)

    ;WITH A AS (
        SELECT TOP (@period + 1) 
               id,date,[close]
        FROM pbl.PriceMinutely
        WHERE type =@Type and id <= @ID
        ORDER BY id DESC
    ), cte AS (
        SELECT ROW_NUMBER() OVER (ORDER BY id) AS rn,
               *
        FROM a
    ), t AS (
		select *,([close] + MAX([close]) OVER (ORDER BY rn) + MIN([close]) OVER (ORDER BY rn)) / 3 b from cte
    ), m AS (
		SELECT rn,b,
			AVG(b) OVER (ORDER BY rn) m
		FROM t
    )
    SELECT @typicalPrice = b,
           @meanTypicalPrice = m,
           @meanDeviation = AVG(ABS(b - m)) OVER (ORDER BY rn) 
    FROM m
	
	if @typicalPrice is null
    RETURN 0
    SET @cci = (@typicalPrice - @meanTypicalPrice) / (0.015 * @meanDeviation)

    RETURN @cci
END
GO