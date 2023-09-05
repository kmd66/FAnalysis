USE [Kama.Administrator]
GO
CREATE PROC [dsk].[spDeleteDifferentialBackupFolder]
AS
     DECLARE @DeletedirPath VARCHAR(1024), @DeletefileDate VARCHAR(20)= CONVERT(VARCHAR(20), DATEADD(DAY, -7, GETDATE()), 112);
     SET @DeletedirPath = '\\arodg01.arodc.ir\DG01\Diff\' + @DeletefileDate;
     EXEC master.sys.xp_delete_files 
          @DeletedirPath;