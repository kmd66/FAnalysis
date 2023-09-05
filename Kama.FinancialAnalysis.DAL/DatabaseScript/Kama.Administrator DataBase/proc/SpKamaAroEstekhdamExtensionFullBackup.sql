USE [Kama.Administrator]
GO
CREATE PROC [bak].[SpKamaAroEstekhdamExtensionFullBackup]
AS
    BEGIN
        DECLARE @name NVARCHAR(256); -- database name  
        DECLARE @fileName NVARCHAR(512); -- filename for backup
        DECLARE @cmd NVARCHAR(MAX); -- filename for backup
        DECLARE @dirPath VARCHAR(1024), @fileDate VARCHAR(20)= CONVERT(VARCHAR(20), GETDATE(), 112);
        SET @dirPath = '\\arodg01.arodc.ir\DG01\Full\' + @fileDate + '\';
        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpKamaAroEstekhdamExtensionFullBackupStarted');
        DECLARE db_cursor CURSOR READ_ONLY
        FOR SELECT name
            FROM master.sys.databases
            WHERE name IN('Kama.Aro.Estekhdam.Extension')  -- exclude these databases
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
                        SELECT @dirPath + @name + 'Full' + '_' + CONVERT(VARCHAR(500), GETDATE(), 112) + '_' + CAST(DATEPART(HOUR, GETDATE()) AS VARCHAR) + CAST(DATEPART(MINUTE, GETDATE()) AS VARCHAR) + '.bak'
                    );
                    SET @cmd = 'BACKUP DATABASE ' + '[' + @name + ']' + ' TO DISK = ' + '''' + @fileName + '''' + ' WITH  NOFORMAT, NOINIT, SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10';
                    EXEC (@cmd);
                    INSERT INTO [bak].[BackupLog]
                    ([BackupStatusDescription], 
                     [DataBaseName]
                    )
                    VALUES
                    ('KamaAroEstekhdamExtensionFullBackupFinished', 
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
                    ('SpKamaAroEstekhdamExtensionFullBackupFinished', 
                     @name, 
                     'ErrorNumber: ' + CAST(ERROR_NUMBER() AS VARCHAR(MAX)) + ' ,ErrorState: ' + CAST(ISNULL(ERROR_STATE(), '') AS VARCHAR(MAX)) + ' ,ErrorSeverity: ' + CAST(ISNULL(ERROR_SEVERITY(), '') AS VARCHAR(MAX)) + ' ,ErrorProcedure: ' + CAST(ISNULL(ERROR_PROCEDURE(), '') AS VARCHAR(MAX)) + ' ,ErrorLine: ' + CAST(ISNULL(ERROR_LINE(), '') AS VARCHAR(MAX)) + ' ,ErrorMessage: ' + CAST(ISNULL(ERROR_MESSAGE(), '') AS VARCHAR(MAX))
                    );
                END CATCH;
                FETCH NEXT FROM db_cursor INTO @name;
            END;
        CLOSE db_cursor;
        DEALLOCATE db_cursor;
        INSERT INTO [bak].[BackupLog]([BackupStatusDescription])
        VALUES('SpKamaAroEstekhdamExtensionFullBackupFinished');
    END;