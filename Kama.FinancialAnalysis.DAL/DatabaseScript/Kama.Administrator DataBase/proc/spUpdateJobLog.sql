USE [Kama.Administrator]
GO
/****** Object:  StoredProcedure [alg].[spUpdateJobLog]    Script Date: 4/8/2023 10:53:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROC [alg].[spUpdateJobLog]
AS
     WITH CTE
          AS (SELECT [sJOB].[job_id] AS [JobID], 
                     [sJOB].[name] AS [JobName],
                     CASE
                         WHEN [sJOBH].[run_date] IS NULL
                              OR [sJOBH].[run_time] IS NULL
                         THEN NULL
                         ELSE CAST(CAST([sJOBH].[run_date] AS CHAR(8)) + ' ' + STUFF(STUFF(RIGHT('000000' + CAST([sJOBH].[run_time] AS VARCHAR(6)), 6), 3, 0, ':'), 6, 0, ':') AS DATETIME)
                     END AS [LastRunDateTime], 
                     --[sJOBH].[run_status],
					 CASE [sJOBH].[run_status]
                         WHEN 0
                         THEN 1
                         WHEN 1
                         THEN 2
                         WHEN 2
                         THEN 0
                         WHEN 3
                         THEN 0
                         WHEN 4
                         THEN 0 -- In Progress
                     END AS [run_status],
                     CASE [sJOBH].[run_status]
                         WHEN 0
                         THEN 'Failed'
                         WHEN 1
                         THEN 'Succeeded'
                         WHEN 2
                         THEN 'Retry'
                         WHEN 3
                         THEN 'Canceled'
                         WHEN 4
                         THEN 'Running' -- In Progress
                     END AS [LastRunStatus], 
                     STUFF(STUFF(RIGHT('000000' + CAST([sJOBH].[run_duration] AS VARCHAR(6)), 6), 3, 0, ':'), 6, 0, ':') AS [LastRunDuration], 
                     [sJOBH].[message] AS [LastRunStatusMessage],
                     CASE [sJOBSCH].[NextRunDate]
                         WHEN 0
                         THEN NULL
                         ELSE CAST(CAST([sJOBSCH].[NextRunDate] AS CHAR(8)) + ' ' + STUFF(STUFF(RIGHT('000000' + CAST([sJOBSCH].[NextRunTime] AS VARCHAR(6)), 6), 3, 0, ':'), 6, 0, ':') AS DATETIME)
                     END AS [NextRunDateTime], 
                     [sJOBH].[RowNumber], 
                     [sJOB].description JobDescription, 
                     [StepJOBH].step_uid,
                     CASE [StepJOBH].run_status
                         WHEN 0
                         THEN 'Failed'
                         WHEN 1
                         THEN 'Succeeded'
                         WHEN 2
                         THEN 'Retry'
                         WHEN 3
                         THEN 'Canceled'
                         WHEN 4
                         THEN 'Running' -- In Progress
                     END AS [StepRunStatusStr], 
					  CASE [StepJOBH].run_status
                         WHEN 0
                         THEN 1
                         WHEN 1
                         THEN 2
                         WHEN 2
                         THEN 0
                         WHEN 3
                         THEN 0
                         WHEN 4
                         THEN 0 -- In Progress
                     END AS [StepRunStatus]--,
                    -- [StepJOBH].run_status StepRunStatus
              FROM [msdb].[dbo].[sysjobs] AS [sJOB]
                   LEFT JOIN
              (
                  SELECT [job_id], 
                         MIN([next_run_date]) AS [NextRunDate], 
                         MIN([next_run_time]) AS [NextRunTime]
                  FROM [msdb].[dbo].[sysjobschedules]
                  GROUP BY [job_id]
              ) AS [sJOBSCH] ON [sJOB].[job_id] = [sJOBSCH].[job_id]
                   LEFT JOIN
              (
                  SELECT [job_id], 
                         [run_date], 
                         [run_time], 
                         [run_status], 
                         [run_duration], 
                         [message], 
                         ROW_NUMBER() OVER(PARTITION BY [job_id]
                         ORDER BY [run_date] DESC, 
                                  [run_time] DESC) AS RowNumber
                  FROM [msdb].[dbo].[sysjobhistory]
                  WHERE [step_id] = 0
              ) AS [sJOBH] ON [sJOB].[job_id] = [sJOBH].[job_id]
                   LEFT JOIN
              (
                  SELECT DISTINCT 
                         h.job_id, 
                         h.step_id, 
                         h.step_name, 
                         h.run_status, 
                         h.run_date, 
                         h.run_time, 
                         st.step_uid
                  FROM [msdb].[dbo].[sysjobhistory] h
                       INNER JOIN msdb.dbo.sysjobsteps st ON st.job_id = h.[job_id]
                                                             AND st.step_name = h.step_name
                  WHERE h.[step_id] <> 0
              ) AS [StepJOBH] ON [StepJOBH].job_id = [sJOBH].[job_id])

    INSERT INTO [alg].[JobLog]
			  ([JobID], 
			   [StepID], 
			   [JobName], 
			   [LastRunDateTime], 
			   [RunStatus], 
			   [LastRunStatus], 
			   [LastRunDuration], 
			   [LastRunStatusMessage], 
			   [NextRunDateTime], 
			   [CreationDate], 
			   [RowNumber], 
			   [JobDescription], 
			   [StepRunStatusStr], 
			   [StepRunStatus]
			  )
    SELECT  [JobID], 
			step_uid, 
			[JobName], 
			[LastRunDateTime], 
			[run_status], 
			[LastRunStatus], 
			[LastRunDuration], 
			[LastRunStatusMessage], 
			[NextRunDateTime], 
			GETDATE(), 
			[RowNumber], 
			JobDescription, 
			StepRunStatusStr, 
			StepRunStatus
    FROM CTE
    WHERE LastRunDateTime IS NOT NULL;