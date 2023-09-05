USE [Kama.Administrator];
GO
CREATE OR ALTER PROC dba.UpdateJobSteps
AS
     INSERT INTO [Kama.Administrator].[alg].[JobStep]
            SELECT js.job_id, 
                   js.step_name, 
                   js.database_name, 
                   js.step_id, 
                   js.step_uid, 
                   NULL
            FROM [Kama.Administrator].[alg].[Job] j
                 INNER JOIN msdb.dbo.sysjobsteps js ON js.job_id = j.JobID
            WHERE js.step_uid NOT IN
            (
                SELECT StepID
                FROM [Kama.Administrator].[alg].[JobStep]
            );