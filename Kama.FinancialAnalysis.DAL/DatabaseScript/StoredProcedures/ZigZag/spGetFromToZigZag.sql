USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetFromToZigZag') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetFromToZigZag
GO
CREATE PROCEDURE pbl.spGetFromToZigZag
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
	FROM [pbl].ZigZag 
	WHERE [Type] = @Type
		AND (@FromDate IS NULL OR [Date] >= @FromDate)
		AND (@ToDate IS NULL OR [Date] <= @ToDate)
	ORDER BY ID DESC
END 
