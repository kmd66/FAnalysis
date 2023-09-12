USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spGetMovingAverages') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spGetMovingAverages
GO

CREATE PROCEDURE pbl.spGetMovingAverages
	@APageSize INT,
	@APageIndex INT,
	@AType TINYINT
--WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE 
		@PageSize INT = @APageSize,
		@PageIndex INT =@APageIndex,
		@Type INT =@AType

	IF @PageIndex = 0 
	BEGIN
		SET @pagesize = 100
		SET @PageIndex = 1
	END

	SELECT 
		*
	FROM [pbl].MovingAverage
	WHERE Type = @Type
	ORDER BY ID DESC
	OFFSET ((@PageIndex - 1) * @PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY;
			
END 
