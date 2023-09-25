USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddBiggerThanSDs') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddBiggerThanSDs
GO
CREATE PROCEDURE pbl.spAddBiggerThanSDs
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].BiggerThanSD(ID, PriceID, R1000, Rate, Type, Session, MaxPriceID, MinPriceID)
	SELECT JsonData.* 
	FROM OPENJSON(@Json) WITH (
		ID UNIQUEIDENTIFIER,
		PriceID BIGINT ,
		R1000 FLOAT ,
		Rate FLOAT ,
		[Type] TINYINT ,
		[Session] TINYINT ,
		MaxPriceID BIGINT ,
		MinPriceID BIGINT 
	) JsonData

END 

