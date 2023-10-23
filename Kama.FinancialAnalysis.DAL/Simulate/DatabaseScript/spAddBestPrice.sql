USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'sim.spAddBestPrice') AND type in (N'P', N'PC'))
    DROP PROCEDURE sim.spAddBestPrice
GO
CREATE PROCEDURE sim.spAddBestPrice
	@AID UNIQUEIDENTIFIER,
	@ATransavtion UNIQUEIDENTIFIER,
	@APriceID BIGINT,
	@AType TINYINT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@ID UNIQUEIDENTIFIER = @AID,
		@Transavtion UNIQUEIDENTIFIER = @ATransavtion,
		@PriceID BIGINT = @APriceID ,
		@Type TINYINT = @AType
	
	INSERT INTO [sim].[BestPrice] (ID, TransactionID, Type)
	VALUES (@ID, @Transavtion, @Type)
END 
