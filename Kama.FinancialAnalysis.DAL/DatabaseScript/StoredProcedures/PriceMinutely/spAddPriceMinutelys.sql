USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddPriceMinutelys') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddPriceMinutelys
GO

CREATE PROCEDURE pbl.spAddPriceMinutelys
	@AJson VARCHAR(MAX),
	@AType TINYINT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @Json NVARCHAR(MAX) = @AJson,
		@Type TINYINT= @AType
	--DECLARE @Json NVARCHAR(MAX) ='[{"ID":1692023520000,"Date":"2023-08-14T14:32:00","Open":1.09391,"Close":1.0937,"Max":1.09391,"Min":1.0937},{"ID":1692023580000,"Date":"2023-08-14T14:33:00","Open":1.0937,"Close":1.09378,"Max":1.09378,"Min":1.09368},{"ID":1692023640000,"Date":"2023-08-14T14:34:00","Open":1.09378,"Close":1.09381,"Max":1.0939,"Min":1.09377},{"ID":1692023700000,"Date":"2023-08-14T14:35:00","Open":1.09382,"Close":1.09387,"Max":1.09389,"Min":1.09374},{"ID":1692023760000,"Date":"2023-08-14T14:36:00","Open":1.09387,"Close":1.09381,"Max":1.0939,"Min":1.09375},{"ID":1692023820000,"Date":"2023-08-14T14:37:00","Open":1.09381,"Close":1.09396,"Max":1.09399,"Min":1.09381},{"ID":1692023880000,"Date":"2023-08-14T14:38:00","Open":1.09396,"Close":1.09391,"Max":1.0941,"Min":1.0939},{"ID":1692023940000,"Date":"2023-08-14T14:39:00","Open":1.09391,"Close":1.09382,"Max":1.09391,"Min":1.0938},{"ID":1692024000000,"Date":"2023-08-14T14:40:00","Open":1.09381,"Close":1.09388,"Max":1.09393,"Min":1.09381},{"ID":1692024060000,"Date":"2023-08-14T14:41:00","Open":1.09388,"Close":1.0939,"Max":1.09391,"Min":1.09381}]'
	
	IF @Type IN(1,2,3,4)
	BEGIN
		INSERT INTO [pbl].[PriceMinutely]
		SELECT JsonData.* ,@Type
		FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			[Open] FLOAT ,
			[Close] FLOAT ,
			[Max] FLOAT ,
			[Min] FLOAT 
		) JsonData
		LEFT JOIN [pbl].[PriceMinutely] Price ON Price.ID = JsonData.ID
		WHERE Price.ID IS NULL 
	END
	ELSE IF @Type IN (6,7,8,9) 
	BEGIN
		INSERT INTO [pbl].PriceMinutelyOther
		SELECT JsonData.* ,@Type
		FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			[Open] FLOAT ,
			[Close] FLOAT ,
			[Max] FLOAT ,
			[Min] FLOAT 
		) JsonData
		LEFT JOIN [pbl].PriceMinutelyOther Price ON Price.ID = JsonData.ID
		WHERE Price.ID IS NULL 
	END
	ELSE
	BEGIN
		INSERT INTO [pbl].[PriceMinutelyIndex]
		SELECT JsonData.* ,@Type
		FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			[Close] FLOAT 
		) JsonData
		LEFT JOIN [pbl].[PriceMinutelyIndex] Price ON Price.ID = JsonData.ID
		WHERE Price.ID IS NULL 
	END
END 
