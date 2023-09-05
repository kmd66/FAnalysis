USE [Kama.Administrator]
GO
/****** Object:  StoredProcedure [bak].[SpFullBackup]    Script Date: 11/12/2022 9:35:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   PROC [bak].[SpFullBackupWithDataBaseName]
 @DataBaseName NVARCHAR(200)
AS
BEGIN

DECLARE
@Date datetime,
@dirPath varchar(1024),
@fileName NVARCHAR(512),
@cmd NVARCHAR(max) ;

select top 1 @Date=BackupDate from bak.BackupPath order by [BackupDate] desc
declare @fileDate VARCHAR(20) = CONVERT(VARCHAR(20),@Date,112) 

select top 1 @dirPath=BackupPath from bak.BackupPath order by [BackupDate] desc
select @dirPath

INSERT INTO [bak].[BackupLog]([BackupStatusDescription],[DataBaseName])VALUES('FullBackupStarted',@DataBaseName)

SELECT @fileName = (SELECT @dirPath+@DataBaseName+'Full'+'_' + CONVERT(VARCHAR(500),@Date,112) +'.bak')

SET @cmd='BACKUP DATABASE '+'['+@DataBaseName+']'+' TO DISK = '+''''+@fileName+''''+' WITH  NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10' 
 EXEC(@cmd);
  --حذف کردن بک آپ قبلی
 DECLARE 
     @DeletedirPath VARCHAR(1024) 
   ,@DeletefileDate VARCHAR(20) = CONVERT(VARCHAR(20),DATEADD(DAY,-7,@Date),112)
   ,@OLdBackupfileName NVARCHAR(512)=@DataBaseName+'Full'+'_' + CONVERT(VARCHAR(20),DATEADD(DAY,-7,@Date),112) +'.bak'
   select top 1 @DeletedirPath=OldBackupPath from bak.BackupPath order by [BackupDate] desc
SET @DeletedirPath = @DeletedirPath+@OLdBackupfileName
select @DeletedirPath
EXEC master.sys.xp_delete_files @DeletedirPath

INSERT INTO [bak].[BackupLog]([BackupStatusDescription],[DataBaseName])VALUES('FullBackupFinished',@DataBaseName)
 
END
