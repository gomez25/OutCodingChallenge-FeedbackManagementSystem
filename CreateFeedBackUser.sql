﻿USE [master]
GO

IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'FeedbackUser')
BEGIN
    DROP LOGIN [FeedbackUser]
END

CREATE LOGIN [FeedbackUser] WITH PASSWORD=N'ZEllxaI0zKl0cq7hpddAQASy40RMVCPRmDyxhKU9jg4=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [FeedbackUser] DISABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [FeedbackUser]
GO
