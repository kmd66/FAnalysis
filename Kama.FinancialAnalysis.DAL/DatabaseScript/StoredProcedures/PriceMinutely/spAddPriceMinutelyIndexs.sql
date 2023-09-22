USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddPriceMinutelyIndexs') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddPriceMinutelyIndexs
GO

CREATE PROCEDURE pbl.spAddPriceMinutelyIndexs
	@AJson1 VARCHAR(MAX),
	@AType1 TINYINT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @Json NVARCHAR(MAX) = @AJson1,
		@Type TINYINT= @AType1
	
	IF @Type = 10
	BEGIN
		INSERT INTO [pbl].PriceMinutelyIndex
		SELECT JsonData.* ,@Type
		FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			[Open] FLOAT ,
			[Close] FLOAT ,
			[Max] FLOAT ,
			[Min] FLOAT 
		) JsonData
		LEFT JOIN [pbl].PriceMinutelyIndex Price ON Price.ID = JsonData.ID
		WHERE Price.ID IS NULL 
	END
	ELSE IF @Type = 11
	BEGIN
		INSERT INTO [pbl].PriceMinutelyIndex
		SELECT 
			JsonData.ID ,
			JsonData.[Date] ,
			0 ,
			JsonData.[Close] ,
			0 ,
			0 ,
			@Type
		FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			[Close] FLOAT 
		) JsonData
		LEFT JOIN [pbl].PriceMinutelyIndex Price ON Price.ID = JsonData.ID
		WHERE Price.ID IS NULL 

	END

END 
