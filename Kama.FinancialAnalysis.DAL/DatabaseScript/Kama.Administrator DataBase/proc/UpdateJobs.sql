USE [Kama.Administrator]
GO
CREATE OR ALTER PROC dba.UpdateJobs
AS
     DELETE FROM [Kama.Administrator].[alg].[Job]

     INSERT INTO [Kama.Administrator].[alg].[Job]
            SELECT job_id, 
                   name
            FROM [msdb].dbo.sysjobs
            WHERE name NOT LIKE '%sys%';