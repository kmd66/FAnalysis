USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetRsi') IS NOT NULL DROP FUNCTION pbl.fnGetRsi
GO

CREATE FUNCTION pbl.fnGetRsi(@ID BIGINT, @Type tinyint, @period int)
RETURNS float
AS
BEGIN
	DECLARE @avgGain DECIMAL(18, 2)
    DECLARE @avgLoss DECIMAL(18, 2)
    DECLARE @rs DECIMAL(18, 2)
    DECLARE @rsi float

    ;WITH a AS (
        SELECT TOP (@period + 1) 
               id,date,[close]
        FROM pbl.PriceMinutely
        WHERE type =@Type and id <= @ID
        ORDER BY id DESC
    ), cte AS (
        SELECT ROW_NUMBER() OVER (ORDER BY id) AS rn,
               *
        FROM a
    ), LAGOVER AS (
		select *,LAG([close]) OVER (ORDER BY rn) b from cte
    ), Main AS (
		select 
			CASE WHEN rn > 1 AND [close] > b THEN [close] - b ELSE 0 END g,
			CASE WHEN rn > 1 AND [close] < b THEN b - [close] ELSE 0 END l,
			* 
		from LAGOVER
		where b is not null
    )
	select 
		@avgGain =AVG(g), 
		 @avgLoss =AVG(l) 
	from main

    SET @rs = @avgGain /@avgLoss
    SET @rsi = 100 - (100 / (1 + @rs))

    RETURN @rsi
END
GO