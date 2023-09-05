USE [Kama.Administrator]
GO

CREATE PROC [dsk].[spCreateDifferentialBackupFolder]
AS
     DECLARE @dirPath VARCHAR(1024), @fileDate VARCHAR(20)= CONVERT(VARCHAR(20), GETDATE(), 112);
     SET @dirPath = '\\arodg01.arodc.ir\DG01\Diff\' + @fileDate + '\';
     EXEC xp_create_subdir 
          @dirPath;
GO