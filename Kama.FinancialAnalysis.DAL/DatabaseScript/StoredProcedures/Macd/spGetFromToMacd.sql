USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetFromToMacd') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetFromToMacd
GO
CREATE PROCEDURE pbl.spGetFromToMacd
	@AType TINYINT,
	@AFromDate DATETIME,
	@AToDate DATETIME
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@Type TINYINT = @AType,
		@FromDate DATETIME = @AFromDate,
		@ToDate DATETIME = @AToDate
	
	
	SELECT
		*
	FROM [pbl].Macd
	WHERE [Type] = @Type
		AND (@FromDate IS NULL OR [Date] >= @FromDate)
		AND (@ToDate IS NULL OR [Date] <= @ToDate)
	ORDER BY ID DESC
END 
