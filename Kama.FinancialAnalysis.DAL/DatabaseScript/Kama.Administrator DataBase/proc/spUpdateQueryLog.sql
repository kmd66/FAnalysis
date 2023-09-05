USE [Kama.Administrator]
GO
/****** Object:  StoredProcedure [alg].[spUpdateQueryLog]    Script Date: 1/2/2023 11:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   PROC [alg].[spUpdateQueryLog]
@DataBaseName NVARCHAR(500)='Kama.Aro.Salary'
AS
BEGIN
DECLARE @cmd NVARCHAR(MAX)='';

SET @cmd='INSERT INTO alg.QueryLog 
       SELECT  
			  qt.[query_sql_text]
			  ,p.query_plan [query_plan_xml]
			  ,rs.[runtime_stats_id]
			  ,rs.[plan_id]
			  ,rs.[runtime_stats_interval_id]
			  ,rs.[execution_type]
			  ,rs.[execution_type_desc]
			  ,rs.[first_execution_time]
			  ,rs.[last_execution_time]
			  ,rs.[count_executions]
			  ,rs.[avg_duration]
			  ,rs.[last_duration]
			  ,rs.[min_duration]
			  ,rs.[max_duration]
			  ,rs.[stdev_duration]
			  ,rs.[avg_cpu_time]
			  ,rs.[last_cpu_time]
			  ,rs.[min_cpu_time]
			  ,rs.[max_cpu_time]
			  ,rs.[stdev_cpu_time]
			  ,rs.[avg_logical_io_reads]
			  ,rs.[last_logical_io_reads]
			  ,rs.[min_logical_io_reads]
			  ,rs.[max_logical_io_reads]
			  ,rs.[stdev_logical_io_reads]
			  ,rs.[avg_logical_io_writes]
			  ,rs.[last_logical_io_writes]
			  ,rs.[min_logical_io_writes]
			  ,rs.[max_logical_io_writes]
			  ,rs.[stdev_logical_io_writes]
			  ,rs.[avg_physical_io_reads]
			  ,rs.[last_physical_io_reads]
			  ,rs.[min_physical_io_reads]
			  ,rs.[max_physical_io_reads]
			  ,rs.[stdev_physical_io_reads]
			  ,rs.[avg_clr_time]
			  ,rs.[last_clr_time]
			  ,rs.[min_clr_time]
			  ,rs.[max_clr_time]
			  ,rs.[stdev_clr_time]
			  ,rs.[avg_dop]
			  ,rs.[last_dop]
			  ,rs.[min_dop]
			  ,rs.[max_dop]
			  ,rs.[stdev_dop]
			  ,rs.[avg_query_max_used_memory]
			  ,rs.[last_query_max_used_memory]
			  ,rs.[min_query_max_used_memory]
			  ,rs.[max_query_max_used_memory]
			  ,rs.[stdev_query_max_used_memory]
			  ,rs.[avg_rowcount]
			  ,rs.[last_rowcount]
			  ,rs.[min_rowcount]
			  ,rs.[max_rowcount]
			  ,rs.[stdev_rowcount]
			  ,rs.[avg_num_physical_io_reads]
			  ,rs.[last_num_physical_io_reads]
			  ,rs.[min_num_physical_io_reads]
			  ,rs.[max_num_physical_io_reads]
			  ,rs.[stdev_num_physical_io_reads]
			  ,rs.[avg_log_bytes_used]
			  ,rs.[last_log_bytes_used]
			  ,rs.[min_log_bytes_used]
			  ,rs.[max_log_bytes_used]
			  ,rs.[stdev_log_bytes_used]
			  ,rs.[avg_tempdb_space_used]
			  ,rs.[last_tempdb_space_used]
			  ,rs.[min_tempdb_space_used]
			  ,rs.[max_tempdb_space_used]
			  ,rs.[stdev_tempdb_space_used]
			  ,OBJECT_NAME(q.object_id) [parent_object]
			  ,'''+@DataBaseName+ ''' [DataBaseName]
     FROM ['+@DataBaseName+'].sys.query_store_query_text AS qt
          RIGHT JOIN ['+@DataBaseName+'].sys.query_store_query AS q ON qt.query_text_id = q.query_text_id
          RIGHT JOIN ['+@DataBaseName+'].sys.query_store_plan AS p ON q.query_id = p.query_id
          RIGHT JOIN ['+@DataBaseName+'].sys.query_store_runtime_stats AS rs ON p.plan_id = rs.plan_id
     WHERE 1 = 1
           AND rs.last_execution_time > DATEADD(hour, -24, GETUTCDATE())'
EXEC (@cmd)
END