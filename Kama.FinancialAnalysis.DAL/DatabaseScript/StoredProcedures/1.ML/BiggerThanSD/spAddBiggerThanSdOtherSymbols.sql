USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddBiggerThanSdOtherSymbols') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddBiggerThanSdOtherSymbols
GO
CREATE PROCEDURE pbl.spAddBiggerThanSdOtherSymbols
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].BiggerThanSdOtherSymbol(ID, BiggerThanSdID, PriceID, MaxPriceID, MinPriceID, R1000, Rate, Type, Session)
	SELECT
		NEWID() ID,
		JsonData.ID BiggerThanSdID,
		JsonData.PriceID,
		JsonData.MaxPriceID, 
		JsonData.MinPriceID ,
		JsonData.R1000,
		JsonData.Rate,
		JsonData.[Type],
		JsonData.[Session]
	FROM OPENJSON(@Json) WITH (
		ID UNIQUEIDENTIFIER,
		PriceID BIGINT ,
		MaxPriceID BIGINT ,
		MinPriceID BIGINT  ,
		R1000 FLOAT ,
		Rate FLOAT ,
		[Type] TINYINT ,
		[Session] TINYINT
	) JsonData

END 

