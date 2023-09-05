USE [Kama.Administrator];
GO
CREATE OR ALTER PROC dba.UpdateJobStepDescription 
@StepID      UNIQUEIDENTIFIER, 
@Description NVARCHAR(200)
AS
    BEGIN
        UPDATE [Kama.Administrator].[alg].[JobStep]
          SET 
              StepDescription = @Description
        WHERE StepID = @StepID;
--dba.UpdateJobStepDescription @StepID='',@Description=N'„—Õ·Â'
    END;