USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetEmptysStandardDeviation') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetEmptysStandardDeviation
GO
CREATE PROCEDURE pbl.spGetEmptysStandardDeviation
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	SELECT 
		CAST(t.value AS BIGINT) ID
	FROM  OPENJSON(@Json) t
	LEFT JOIN pbl.StandardDeviation m ON t.value = m.ID
	WHERE m.ID IS NULL
	
END 
