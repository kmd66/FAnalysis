USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddZigZag') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddZigZag
GO
CREATE PROCEDURE pbl.spAddZigZag
	@AID BIGINT ,
	@ADate DatetIME ,
	@AApproved BIGINT ,
	@AUp BIT ,
	@AType TINYINT 
AS
BEGIN
	
	DECLARE @ID BIGINT = @AID,
		@Date DatetIME = @ADate,
		@Approved BIGINT = @AApproved,
		@Up BIT = @AUp ,
		@Type TINYINT = @AType 
	
	INSERT INTO [pbl].ZigZag(ID, Date, Approved, Up, Type)
	VALUES(@ID, @Date, @Approved, @Up, @Type)


END 
