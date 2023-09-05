USE [Kama.Administrator]
GO
CREATE OR ALTER PROC [bak].[SpDIFFERENTIALBackup]
AS
    BEGIN
        DECLARE @name NVARCHAR(256); -- database name  
        DECLARE @fileName NVARCHAR(512); -- filename for backup
        DECLARE @cmd NVARCHAR(MAX); -- filename for backup
        DECLARE @fileDate VARCHAR(20)= CONVERT(VARCHAR(20), GETDATE(), 112);
        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpDIFFERENTIALBackupStarted');
        DECLARE db_cursor CURSOR READ_ONLY
        FOR SELECT name
            FROM master.sys.databases
            WHERE name NOT IN('master', 'model', 'msdb', 'tempdb')  -- exclude these databases
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
                    ('DIFFERENTIALBackupStarted', 
                     @name
                    );
                    SELECT @fileName =
                    (
                        SELECT '\\arodg01.arodc.ir\DG01\Diff\' + @fileDate + '\' + @name + 'DIFFERENTIAL' + '_' + CONVERT(VARCHAR(500), GETDATE(), 112) + '_' + CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR) + CAST(DATEPART(MINUTE, GETDATE()) AS VARCHAR) + '.bak'
                    );
                    SET @cmd = 'BACKUP DATABASE ' + '[' + @name + ']' + ' TO DISK = ' + '''' + @fileName + '''' + ' WITH DIFFERENTIAL , NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10';
                    EXEC (@cmd);
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName]
                    )
                    VALUES
                    ('DIFFERENTIALBackupFinished', 
                     @name
                    );
                END TRY
                BEGIN CATCH

                    --PRINT 'Error'
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName], 
                     ErrorMessage
                    )
                    VALUES
                    ('DIFFERENTIALBackupFinishedWithError', 
                     @name, 
                     'ErrorNumber: ' + CAST(ERROR_NUMBER() AS VARCHAR(MAX)) + ' ,ErrorState: ' + CAST(ISNULL(ERROR_STATE(), '') AS VARCHAR(MAX)) + ' ,ErrorSeverity: ' + CAST(ISNULL(ERROR_SEVERITY(), '') AS VARCHAR(MAX)) + ' ,ErrorProcedure: ' + CAST(ISNULL(ERROR_PROCEDURE(), '') AS VARCHAR(MAX)) + ' ,ErrorLine: ' + CAST(ISNULL(ERROR_LINE(), '') AS VARCHAR(MAX)) + ' ,ErrorMessage: ' + CAST(ISNULL(ERROR_MESSAGE(), '') AS VARCHAR(MAX))
                    );
                END CATCH;
                FETCH NEXT FROM db_cursor INTO @name;
            END;
        CLOSE db_cursor;
        DEALLOCATE db_cursor;
    END;