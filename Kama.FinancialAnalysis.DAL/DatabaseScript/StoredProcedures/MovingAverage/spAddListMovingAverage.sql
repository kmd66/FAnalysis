USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListMovingAverage') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListMovingAverage
GO
CREATE PROCEDURE pbl.spAddListMovingAverage
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].[MovingAverage] (ID, [Date], M5, M30, H1, D, [Type])
	SELECT 
		ID ,
		[Date] ,
		M5  ,
		M30 ,
		H1  ,
		D ,
		[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			M5 FLOAT ,
			M30 FLOAT ,
			H1 FLOAT ,
			D FLOAT ,
			[Type] TINYINT 
		) 


END 
