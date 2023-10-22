USE [Kama.FinancialAnalysis]
GO

IF OBJECT_ID('pbl.fnGetIchimoku') IS NOT NULL DROP FUNCTION pbl.fnGetIchimoku
GO

CREATE FUNCTION pbl.fnGetIchimoku(@ID BIGINT, @Close float, @Type tinyint)
RETURNS bit
AS
BEGIN
	
	DECLARE 
      @t  float
      ,@k float
      ,@sa float
      ,@sb  float
	
	select 
      @t =(Max45 + Min45) / 2 
      ,@k =(Max130 + Min130) / 2 
      ,@sb =(Max260 + Min260) / 2 
	from pbl.Ichimoku where id = @ID
	set @sa = (@t + @k) / 2 
	
	if @t is null 
		RETURN 0
	
    if @Type = 2
	begin
		if @Close > @sb-- >0
		RETURN 0
    
		RETURN 1
	END
		if @Close > @sb-- >0
		RETURN 0
    
		RETURN 1
END
GO