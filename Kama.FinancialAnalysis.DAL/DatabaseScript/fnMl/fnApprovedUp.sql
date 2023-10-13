USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnApprovedUp') IS NOT NULL DROP FUNCTION pbl.fnApprovedUp
GO

CREATE FUNCTION pbl.fnApprovedUp(@ID BIGINT, @ApprovedID BIGINT)
RETURNS bit
AS
BEGIN
	
	DECLARE @p1 float
    DECLARE @p2 float
	
	set @p1 = (select top 1 [close] from pbl.PriceMinutely where id = @ID)
	set @p2 = (select top 1 [close] from pbl.PriceMinutely where id = @ApprovedID)
	if(@p2 > @p1)
		RETURN  1
    RETURN 0
END
GO