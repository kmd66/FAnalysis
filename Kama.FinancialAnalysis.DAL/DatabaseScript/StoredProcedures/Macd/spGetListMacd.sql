USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetListMacd') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetListMacd
GO
CREATE PROCEDURE pbl.spGetListMacd
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
	
		SELECT * FROM [pbl].Macd
		WHERE [Type] = @Type
			and date > '2023-01-01 00:01:00.000'
		ORDER BY ID DESC
		OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
			
END 
