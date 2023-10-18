USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListCci') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListCci
GO
CREATE PROCEDURE pbl.spAddListCci
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].Cci (ID, Date, Value14, Value32, Value70, Type)
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
	LEFT JOIN [pbl].Cci c ON c.ID = JsonData.ID
	WHERE c.ID IS NULL


END 
