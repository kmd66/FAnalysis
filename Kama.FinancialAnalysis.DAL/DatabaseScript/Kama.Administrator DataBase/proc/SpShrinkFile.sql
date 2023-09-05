USE [Kama.Administrator]
GO
CREATE OR ALTER PROC [shr].[SpShrinkFile]
AS
    BEGIN
        DECLARE @fileDate VARCHAR(20)= CONVERT(VARCHAR(20), DATEADD(DAY, -2, GETDATE()), 112);
        DECLARE @name NVARCHAR(256); -- database name  
        DECLARE @fileName NVARCHAR(512); -- filename for backup
        DECLARE @cmd NVARCHAR(MAX); -- filename for backup
        DECLARE @LogFileName NVARCHAR(MAX);
        DECLARE @dirPath VARCHAR(1024);
        SET @dirPath = '\\arodg01.arodc.ir\DG01\Full\' + @fileDate + '\TransactionLogBackup\';
        EXEC xp_create_subdir 
             @dirPath;
        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpTransactionalLogBackupStarted');
        DECLARE db_cursor CURSOR READ_ONLY
        FOR SELECT name
            FROM master.sys.databases
            WHERE name NOT IN('master', 'model', 'msdb', 'tempdb', 'Kama.Aro.Estekhdam.Extension')
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
                    ('TransactionalLogBackupStarted', 
                     @name
                    );
                    SELECT @fileName =
                    (
                        SELECT @dirPath + @name + 'LOG' + '_' + CONVERT(VARCHAR(500), GETDATE(), 112) + '_' + CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR) + CAST(DATEPART(MINUTE, GETDATE()) AS VARCHAR) + '.bak'
                    );
                    SET @cmd = 'BACKUP LOG ' + '[' + @name + ']' + ' TO DISK = ' + '''' + @fileName + '''' + ' WITH  NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10';

                    --EXEC(@cmd);

                    SELECT @LogFileName = files.name
                    FROM sys.master_files files
                         JOIN sys.databases db ON db.database_id = files.database_id
                    WHERE type = 1
                          AND db.name = @name;
                    SET @cmd = 'USE [' + @name + ']  DBCC SHRINKFILE (' + '''' + @LogFileName + '''' + ' , 0)';
                    PRINT @cmd;
                    EXEC (@cmd);
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName]
                    )
                    VALUES
                    ('TransactionalLogBackupFinished', 
                     @name
                    );
                END TRY
                BEGIN CATCH
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName], 
                     ErrorMessage
                    )
                    VALUES
                    ('TransactionalLogBackupFinishedWithError', 
                     @name, 
                     'ErrorNumber: ' + CAST(ERROR_NUMBER() AS VARCHAR(MAX)) + ' ,ErrorState: ' + CAST(ISNULL(ERROR_STATE(), '') AS VARCHAR(MAX)) + ' ,ErrorSeverity: ' + CAST(ISNULL(ERROR_SEVERITY(), '') AS VARCHAR(MAX)) + ' ,ErrorProcedure: ' + CAST(ISNULL(ERROR_PROCEDURE(), '') AS VARCHAR(MAX)) + ' ,ErrorLine: ' + CAST(ISNULL(ERROR_LINE(), '') AS VARCHAR(MAX)) + ' ,ErrorMessage: ' + CAST(ISNULL(ERROR_MESSAGE(), '') AS VARCHAR(MAX))
                    );
                END CATCH;
                FETCH NEXT FROM db_cursor INTO @name;
            END;
        CLOSE db_cursor;
        DEALLOCATE db_cursor;
        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpTransactionalLogBackupFinished');
    END;