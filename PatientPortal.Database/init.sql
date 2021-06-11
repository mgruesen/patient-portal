USE [master];
GO

/* Add application database user */
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'app')
BEGIN
    CREATE LOGIN app WITH PASSWORD = N'Notreallysecure1';
    
    CREATE USER app FOR LOGIN app;
    
    EXEC sp_addrolemember N'db_owner', N'app';
END;
GO
