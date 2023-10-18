USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListMacd') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListMacd
GO
CREATE PROCEDURE pbl.spAddListMacd
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].Macd (ID, Date, E12, E26, E60, E130, E120, E260, Type)
	SELECT 
		jsonData.ID ,
		jsonData.[Date] ,
		jsonData.E12 ,
		jsonData.E26 ,
		jsonData.E60 ,
		jsonData.E130,
		jsonData.E120,
		jsonData.E260,
		jsonData.[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			E12  FLOAT ,
			E26  FLOAT ,
			E60  FLOAT ,
			E130 FLOAT ,
			E120 FLOAT ,
			E260 FLOAT ,
			[Type] TINYINT 
		) jsonData
	LEFT JOIN [pbl].Macd m ON m.ID = JsonData.ID
	WHERE m.ID IS NULL


END 
