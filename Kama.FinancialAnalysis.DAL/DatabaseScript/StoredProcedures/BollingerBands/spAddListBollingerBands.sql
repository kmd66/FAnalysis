USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListBollingerBands') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListBollingerBands
GO
CREATE PROCEDURE pbl.spAddListBollingerBands
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].BollingerBands (ID, Date, A20, U20, L20, A30, U30, L30, A40, U40, L40, Type)
	SELECT 
		jsonData.ID ,
		jsonData.[Date] ,
		jsonData.A20,
		jsonData.U20,
		jsonData.L20,
		jsonData.A30,
		jsonData.U30,
		jsonData.L30,
		jsonData.A40,
		jsonData.U40,
		jsonData.L40,
		jsonData.[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			A20 FLOAT ,
			U20 FLOAT ,
			L20 FLOAT ,
			A30 FLOAT ,
			U30 FLOAT ,
			L30 FLOAT ,
			A40 FLOAT ,
			U40 FLOAT ,
			L40 FLOAT ,
			[Type] TINYINT 
		) jsonData
	LEFT JOIN [pbl].BollingerBands b ON b.ID = JsonData.ID
	WHERE b.ID IS NULL


END 
