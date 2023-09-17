USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetPriceMinutelys') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetPriceMinutelys
GO

CREATE PROCEDURE pbl.spGetPriceMinutelys
	@AType TINYINT,
	@APageSize INT,
	@APageIndex INT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@Type TINYINT = @AType,
		@PageSize INT = @APageSize,
		@PageIndex INT =@APageIndex

	IF @PageIndex = 0 
	BEGIN
		SET @pagesize = 14400 --100000000
		SET @PageIndex = 1
	END
	
	IF @Type IN (1,2,3,4) 
	BEGIN
		SELECT * FROM [pbl].[PriceMinutely] 
		WHERE [Type] = @Type
		ORDER BY ID DESC
		OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
	END
	ELSE IF @Type IN (6,7,8,9) 
	BEGIN
		SELECT * FROM [pbl].[PriceMinutelyOther] 
		WHERE [Type] = @Type
		ORDER BY ID DESC
		OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
	END
	ELSE 
	BEGIN
		SELECT * FROM [pbl].[PriceMinutelyIndex]
		WHERE [Type] = @Type
		ORDER BY ID DESC
		OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
	END
			
END 
