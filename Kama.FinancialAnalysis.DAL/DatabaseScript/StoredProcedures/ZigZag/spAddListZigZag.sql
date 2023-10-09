USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListZigZag') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListZigZag
GO
CREATE PROCEDURE pbl.spAddListZigZag
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].ZigZag (ID, Date, Approved, Up, [Value], Type)
	SELECT 
		jsonData.ID ,
		jsonData.[Date] ,
		jsonData.Approved  ,
		jsonData.Up,
		jsonData.[Value],
		jsonData.[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			Approved BIGINT ,
			Up BIT ,
			[Value] FLOAT,
			[Type] TINYINT 
		) jsonData
	LEFT JOIN [pbl].ZigZag z ON z.ID = JsonData.ID
	WHERE z.ID IS NULL


END 
