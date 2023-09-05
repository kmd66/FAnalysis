USE [Kama.Administrator]
GO
CREATE OR ALTER PROC [bak].[SpFullBackup]
AS
    BEGIN
        DECLARE @NowDate DATETIME= GETDATE();
        DECLARE @dirPath VARCHAR(1024), @fileDate VARCHAR(20)= CONVERT(VARCHAR(20), @NowDate, 112);
        SET @dirPath = '\\arodg01.arodc.ir\DG01\Full\' + @fileDate + '\';
        DECLARE @name NVARCHAR(256); -- database name  
        DECLARE @fileName NVARCHAR(512); -- filename for backup
        DECLARE @cmd NVARCHAR(MAX); -- filename for backup

        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpFullBackupStarted');
        DECLARE db_cursor CURSOR READ_ONLY
        FOR SELECT name
            FROM master.sys.databases
            WHERE name NOT IN('master', 'model', 'msdb', 'tempdb')
            AND state = 0 -- database is online
            AND is_in_standby = 0; -- database is not read only for log shipping

        OPEN db_cursor;
        FETCH NEXT FROM db_cursor INTO @name;
        WHILE @@FETCH_STATUS = 0
            BEGIN
                BEGIN TRY
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName]
                    )
                    VALUES
                    ('FullBackupStarted', 
                     @name
                    );
                    SELECT @fileName =
                    (
                        SELECT @dirPath + @name + 'Full' + '_' + CONVERT(VARCHAR(500), @NowDate, 112) + '.bak'
                    );
                    SET @cmd = 'BACKUP DATABASE ' + '[' + @name + ']' + ' TO DISK = ' + '''' + @fileName + '''' + ' WITH  NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10';
                    EXEC (@cmd);
                    --حذف کردن بک آپ قبلی
                    DECLARE @DeletedirPath VARCHAR(1024), @DeletefileDate VARCHAR(20)= CONVERT(VARCHAR(20), DATEADD(DAY, -7, @NowDate), 112), @OLdBackupfileName NVARCHAR(512)= @name + 'Full' + '_' + CONVERT(VARCHAR(20), DATEADD(DAY, -7, @NowDate), 112) + '.bak';
                    SET @DeletedirPath = '\\arodg01.arodc.ir\DG01\Full\' + @DeletefileDate + '\' + @OLdBackupfileName;
                    EXEC master.sys.xp_delete_files 
                         @DeletedirPath;
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName]
                    )
                    VALUES
                    ('FullBackupFinished', 
                     @name
                    );
                END TRY
                BEGIN CATCH
                    DECLARE @ErrorMessage VARCHAR(MAX);
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName], 
                     ErrorMessage
                    )
                    VALUES
                    ('FullBackupFinished', 
                     @name, 
                     'ErrorNumber: ' + CAST(ERROR_NUMBER() AS VARCHAR(MAX)) + ' ,ErrorState: ' + CAST(ISNULL(ERROR_STATE(), '') AS VARCHAR(MAX)) + ' ,ErrorSeverity: ' + CAST(ISNULL(ERROR_SEVERITY(), '') AS VARCHAR(MAX)) + ' ,ErrorProcedure: ' + CAST(ISNULL(ERROR_PROCEDURE(), '') AS VARCHAR(MAX)) + ' ,ErrorLine: ' + CAST(ISNULL(ERROR_LINE(), '') AS VARCHAR(MAX)) + ' ,ErrorMessage: ' + CAST(ISNULL(ERROR_MESSAGE(), '') AS VARCHAR(MAX))
                    );
                END CATCH;
                FETCH NEXT FROM db_cursor INTO @name;
            END;
        CLOSE db_cursor;
        DEALLOCATE db_cursor;
        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpFullBackupFinished');
    END;