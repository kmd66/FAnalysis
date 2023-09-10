USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetEmptysMovingAverage') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetEmptysMovingAverage
GO
CREATE PROCEDURE pbl.spGetEmptysMovingAverage
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	SELECT 
		CAST(t.value AS BIGINT) ID
	FROM  OPENJSON(@Json) t
	LEFT JOIN pbl.MovingAverage m ON t.value = m.ID
	WHERE m.ID IS NULL
	
END 
