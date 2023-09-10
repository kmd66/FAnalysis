USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddListStandardDeviation') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddListStandardDeviation
GO
CREATE PROCEDURE pbl.spAddListStandardDeviation
	@AJson VARCHAR(MAX)
AS
BEGIN

	
	DECLARE @Json NVARCHAR(MAX) = @AJson

	INSERT INTO [pbl].StandardDeviation (ID, [Date], R100, R500, R1000, [Type])
	SELECT 
		jsonData.ID ,
		jsonData.[Date] ,
		jsonData.R100  ,
		jsonData.R500 ,
		jsonData.R1000  ,
		jsonData.[Type]
	FROM OPENJSON(@Json) WITH (
			ID BIGINT ,
			[Date] DatetIME ,
			R100 FLOAT ,
			R500 FLOAT ,
			R1000 FLOAT ,
			[Type] TINYINT 
		) jsonData
	LEFT JOIN [pbl].StandardDeviation SD ON SD.ID = JsonData.ID
	WHERE SD.ID IS NULL
	--INSERT INTO [pbl].StandardDeviation (ID, M10, M30, H1, H12, D1,p1000)
	--SELECT 
	--	t1.ID, 
	--	0, --pbl.fnGetStandardDeviation(t1.ID,1,10),
	--	0, --pbl.fnGetStandardDeviation(t1.ID,1,30),
	--	0, --pbl.fnGetStandardDeviation(t1.ID,1,60),
	--	0, --pbl.fnGetStandardDeviation(t1.ID,1,(60*12)),
	--	0, --pbl.fnGetStandardDeviation(t1.ID,1,(60*24))
	--	pbl.fnGetStandardDeviation2(t1.ID , t1.Type, 1000)
	--FROM pbl.PriceMinutely t1
	--LEFT JOIN pbl.StandardDeviation t2 ON t1.ID = t2.ID
	--WHERE t2.ID IS NULL AND t1.[Type] = 1

END 
