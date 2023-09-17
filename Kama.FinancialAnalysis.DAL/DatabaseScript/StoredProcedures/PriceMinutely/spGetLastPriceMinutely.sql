USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetLastPriceMinutely') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetLastPriceMinutely
GO

CREATE PROCEDURE pbl.spGetLastPriceMinutely
	@AType TINYINT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@Type TINYINT = @AType
	
	IF @Type IN (1,2,3,4) 
	BEGIN
		SELECT Top 1 * FROM [pbl].[PriceMinutely] 
		WHERE [Type] = @Type
		ORDER BY ID DESC
	END
	ELSE IF @Type IN (6,7,8,9) 
	BEGIN
		SELECT Top 1 * FROM [pbl].[PriceMinutelyOther] 
		WHERE [Type] = @Type
		ORDER BY ID DESC
	END
	ELSE 
	BEGIN
		SELECT Top 1 * FROM [pbl].[PriceMinutelyIndex]
		WHERE [Type] = @Type
		ORDER BY ID DESC
	END
			
END 
