USE [master];
GO

/* Add application database user */
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'app')
BEGIN
    CREATE LOGIN app WITH PASSWORD = N'notsecure';
    
    CREATE USER app FOR LOGIN app;
    
    EXEC sp_addrolemember N'db_owner', N'app';
END;
GO

/* Add test users */
IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username IN (N'bob', N'bill'))
BEGIN
    INSERT INTO 
        dbo.Users (Id, Username, [Password]) 
    VALUES 
        -- bob:password1
        ('41fb6be0-5827-4226-8c93-e9a96701644d', N'bob', N'10000.WqcaDoKgiNEloduj0hELNA==.P7r8Tq20te86gr8ZpwJRs5JmvMo2wiHxsTA95C3ZyqM='),
        -- bill:password2
        ('75bf4d2e-3410-4d79-a33b-1c63402a6af3', N'bill', N'10000.iLl3E45elWO0WEJp4EVc9Q==.3a/Bo6vx2NXweAodzPhLTSiFmJYInMRYXPYMA4eLYfE=');
END;
GO
