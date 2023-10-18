USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListIchimoku') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListIchimoku
GO
CREATE PROCEDURE pbl.spAddListIchimoku
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].[Ichimoku](ID, Date, Max9, Max26, Max52, Max45, Max130, Max260, Min9, Min26, Min52, Min45, Min130, Min260, Type)
	SELECT 
		jsonData.ID ,
		jsonData.[Date] ,
		jsonData.Max9  ,
		jsonData.Max26 ,
		jsonData.Max52 ,
		jsonData.Max45 ,
		jsonData.Max130,
		jsonData.Max260,
		jsonData.Min9  ,
		jsonData.Min26 ,
		jsonData.Min52 ,
		jsonData.Min45 ,
		jsonData.Min130,
		jsonData.Min260,
		jsonData.[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			Max9   FLOAT ,
			Max26  FLOAT ,
			Max52  FLOAT ,
			Max45  FLOAT ,
			Max130 FLOAT ,
			Max260 FLOAT ,
			Min9   FLOAT ,
			Min26  FLOAT ,
			Min52  FLOAT ,
			Min45  FLOAT ,
			Min130 FLOAT ,
			Min260 FLOAT ,
			[Type] TINYINT 
		) jsonData
	LEFT JOIN [pbl].[Ichimoku] i ON i.ID = JsonData.ID
	WHERE i.ID IS NULL


END 
