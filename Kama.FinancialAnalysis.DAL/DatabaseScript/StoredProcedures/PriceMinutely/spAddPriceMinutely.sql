USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddPriceMinutely') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddPriceMinutely
GO

CREATE PROCEDURE pbl.spAddPriceMinutely
	@AID BIGINT ,
	@ADate DATETIME ,
	@AOpen FLOAT,
	@AClose FLOAT,
	@AMax FLOAT,
	@AMin FLOAT,
	@AType TINYINT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@ID BIGINT = @AID,
		@Date DATETIME =@ADate,
		@Open FLOAT = @AOpen,
		@Close FLOAT = @AClose,
		@Max FLOAT = @AMax,
		@Min FLOAT = @AMin,
		@Type TINYINT= @AType
	

	
		INSERT INTO [pbl].[PriceMinutely]
			([ID], [Date], [Open], [Close], [Max], [Min], [Type])
		VALUES
			(@ID, @Date, @Open, @Close, @Max, @Min, @Type)
	
END 
