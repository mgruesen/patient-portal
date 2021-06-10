USE [master];
GO

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'app')
BEGIN
    CREATE LOGIN app WITH PASSWORD = N'notsecure';
    
    CREATE USER app FOR LOGIN app;
    
    EXEC sp_addrolemember N'db_owner', N'app';
END;
GO

INSERT INTO 
    dbo.Patients (FirstName, LastName) 
VALUES 
    (N'Bob', N'Roberts'), 
    (N'Bill', N'Williams');
GO

INSERT INTO dbo.Users (Username, [Password]) VALUES (N'bob', N'10000.salt.key');