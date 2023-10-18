USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListRsi') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListRsi
GO
CREATE PROCEDURE pbl.spAddListRsi
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].Rsi (ID, Date, Value14, Value32, Value70, Type)
	SELECT 
		jsonData.ID ,
		jsonData.[Date] ,
		jsonData.Value14,
		jsonData.Value32,
		jsonData.Value70,
		jsonData.[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			Value14 FLOAT ,
			Value32 FLOAT ,
			Value70 FLOAT ,
			[Type] TINYINT 
		) jsonData
	LEFT JOIN [pbl].Rsi r ON r.ID = JsonData.ID
	WHERE r.ID IS NULL


END 
