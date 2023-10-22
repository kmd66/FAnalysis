USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnZigZagPrevPivote') IS NOT NULL DROP FUNCTION pbl.fnZigZagPrevPivote
GO

CREATE FUNCTION pbl.fnZigZagPrevPivote(@ID BIGINT, @type tinyint)
RETURNS BIGINT
AS
BEGIN

	DECLARE @BackID BIGINT
	SET @BackID = (select top 1 id from pbl.ZigZag where ID < @ID and Type = @type Order by ID DESC)
	RETURN @BackID 
END
GO