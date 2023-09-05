USE [Kama.FinancialAnalysis]
GO

CREATE FUNCTION fnTimeStamp(
@ctimestamp datetime
)
RETURNS BIGINT
AS
BEGIN
  /* Function body */
  declare @return BIGINT

  SELECT @return = DATEDIFF(SECOND,{d '1970-01-01'}, @ctimestamp)
  
  return @return * 1000
END
GO

