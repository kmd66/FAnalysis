USE [Kama.FinancialAnalysis]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'pbl.spAddDistanceMeasurements') AND type in (N'P', N'PC'))
    DROP PROCEDURE pbl.spAddDistanceMeasurements
GO
CREATE PROCEDURE pbl.spAddDistanceMeasurements
	@AJson VARCHAR(MAX)
AS
BEGIN
	
	DECLARE @Json NVARCHAR(MAX) = @AJson
	
	INSERT INTO [pbl].[DistanceMeasurement](ID, BiggerThanSdID, Rate, MAcd, UpGs, DownGs, Type, Session)
	SELECT JsonData.* 
	FROM OPENJSON(@Json) WITH (
		ID UNIQUEIDENTIFIER,
		BiggerThanSdID UNIQUEIDENTIFIER,
		Rate FLOAT ,
		Macd FLOAT ,
		UpGs FLOAT ,
		DownGs FLOAT ,
		[Type] TINYINT ,
		[Session] TINYINT 
	) JsonData

END 

