USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetRsi') IS NOT NULL DROP FUNCTION pbl.fnGetRsi
GO

CREATE FUNCTION pbl.fnGetRsi(@ID BIGINT, @Type tinyint)
RETURNS float
AS
BEGIN
	
	DECLARE @v14 float,@v32 float,@v70 float
	select @v14= Value14,@v32=Value32,@v70 = Value70 from pbl.Rsi where id = @ID
	
	if @v14 is null 
		RETURN 0
	
	if @Type = 70
		RETURN @v70
    if @Type = 32
		RETURN @v32
    
	RETURN @v14
END
GO