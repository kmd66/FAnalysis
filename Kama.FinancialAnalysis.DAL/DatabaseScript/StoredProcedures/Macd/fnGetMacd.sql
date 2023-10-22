USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetMacd') IS NOT NULL DROP FUNCTION pbl.fnGetMacd
GO

CREATE FUNCTION pbl.fnGetMacd(@ID BIGINT, @Type tinyint)
RETURNS float
AS
BEGIN
	
	DECLARE 
		@v12 float,@v26 float,
		@v60 float,@v130 float,
		@v120 float,@v260 float
	
	select 
		@v12= E12, @v26 = E26,
		@v60= E60, @v130= E130,
		@v120= E120, @v260 = E260
	from pbl.Macd where id = @ID
	
	if @v12 is null 
		RETURN 0
	
	if @Type = 120
		RETURN @v120 - @v260
    if @Type = 60
		RETURN @v60 - @v130
    
	RETURN @v12 -@v26
END
GO