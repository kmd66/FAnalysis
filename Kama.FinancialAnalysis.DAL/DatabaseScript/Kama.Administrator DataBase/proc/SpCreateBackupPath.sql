USE [Kama.Administrator]
GO
/****** Object:  StoredProcedure [bak].[SpFullBackup]    Script Date: 11/12/2022 9:17:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   PROC [bak].[SpCreateBackupPath]
AS
BEGIN
DECLARE @NowDate datetime=GETDATE();
declare 
     @BackupPath varchar(1024) 
    ,@fileDate VARCHAR(20) = CONVERT(VARCHAR(20),@NowDate,112) 

SET @BackupPath = '\\arodg01.arodc.ir\DG01\Full\'+@fileDate+'\'

DECLARE 
     @OldBackupPath VARCHAR(1024) 
   ,@DeletefileDate VARCHAR(20) = CONVERT(VARCHAR(20),DATEADD(DAY,-7,@NowDate),112)

SET @OldBackupPath = '\\arodg01.arodc.ir\DG01\Full\'+@DeletefileDate+'\'
INSERT INTO [bak].[BackupPath]
           ([BackupPath]
           ,[OldBackupPath]
           ,[BackupDate]
           ,[BackupStatus])
     VALUES
           (@BackupPath
           ,@OldBackupPath
           ,GETDATE()
           ,0)
END
