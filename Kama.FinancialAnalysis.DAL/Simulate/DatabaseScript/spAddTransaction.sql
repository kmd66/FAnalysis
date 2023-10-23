USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'sim.spAddTransaction') AND type in (N'P', N'PC'))
    DROP PROCEDURE sim.spAddTransaction
GO
CREATE PROCEDURE sim.spAddTransaction
	@AID UNIQUEIDENTIFIER,
	@ASignalPriceID BIGINT,
	@AStartPriceID BIGINT,
	@AEndPriceID BIGINT,
	@AProfit FLOAT,
	@AType TINYINT,
	@AReturned BIT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@ID UNIQUEIDENTIFIER = @AID,
		@SignalPriceID BIGINT = @ASignalPriceID,
		@StartPriceID BIGINT = @AStartPriceID,
		@EndPriceID BIGINT = @AEndPriceID,
		@Profit FLOAT = @AProfit,
		@Type TINYINT = @AType,
		@Returned BIT = @AReturned
	
	INSERT INTO [sim].[Transaction] (ID, SignalPriceID, StartPriceID, EndPriceID, Profit, Type, Returned)
	VALUES (@ID, @SignalPriceID, @StartPriceID, @EndPriceID, @Profit, @Type, @Returned)

END 
