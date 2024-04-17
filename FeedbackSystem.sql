USE master
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'FeedbackSystem')
BEGIN
    -- Create the database FeedbackSystem
    CREATE DATABASE FeedbackSystem
END
GO

USE FeedbackSystem
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Categories')
BEGIN
    -- Create the Categories table
    CREATE TABLE Categories (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        Name NVARCHAR(MAX) NOT NULL
    )
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Feedbacks')
BEGIN
    -- Create the Feedbacks table
    CREATE TABLE Feedbacks (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        CustomerName NVARCHAR(MAX) NOT NULL,
        Description NVARCHAR(MAX) NOT NULL,
        SubmissionDate DATETIME NOT NULL,
        CategoryId INT NOT NULL,
        FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
    )
END
GO

-- Create the stored procedure sp_AddFeedback
CREATE OR ALTER PROCEDURE sp_AddFeedback
    @CustomerName NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
    @CategoryId INT
AS
BEGIN 
    BEGIN TRY
        BEGIN TRANSACTION

        INSERT INTO Feedbacks (CustomerName, Description, SubmissionDate, CategoryId)
        VALUES (@CustomerName, @Description, GETDATE(), @CategoryId)

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        THROW
    END CATCH
END 
GO

-- Create the stored procedure sp_UpdateFeedback
CREATE OR ALTER PROCEDURE sp_UpdateFeedback
    @FeedbackId INT,
    @CustomerName NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
    @CategoryId INT
AS
BEGIN 
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE Feedbacks 
        SET CustomerName = @CustomerName, 
            Description = @Description,
            CategoryId = @CategoryId
        WHERE Id = @FeedbackId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        THROW
    END CATCH
END
GO

-- Create the stored procedure sp_DeleteFeedback
CREATE OR ALTER PROCEDURE sp_DeleteFeedback
    @FeedbackId INT
AS
BEGIN 
    BEGIN TRY
        BEGIN TRANSACTION

        DELETE FROM Feedbacks WHERE Id = @FeedbackId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        THROW
    END CATCH
END 
GO

-- Create the stored procedure sp_GetFeedbacksByDatesRange
CREATE OR ALTER PROCEDURE sp_GetFeedbacksByDatesRange
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    BEGIN TRY
        SELECT 
            F.Id AS FeedbackId, 
            F.CustomerName, 
            F.Description, 
            F.SubmissionDate, 
            F.CategoryId, 
            C.Name AS CategoryName
        FROM 
            Feedbacks F
        JOIN 
            Categories C ON F.CategoryId = C.Id
        WHERE 
            F.SubmissionDate BETWEEN @StartDate AND @EndDate
    END TRY
    BEGIN CATCH
        THROW
    END CATCH
END
GO

-- Create the stored procedure sp_GetFeedbackById
CREATE OR ALTER PROCEDURE sp_GetFeedbackById
    @Id INT
AS
BEGIN
    BEGIN TRY
        SELECT 
            F.Id AS FeedbackId, 
            F.CustomerName, 
            F.Description, 
            F.SubmissionDate, 
            F.CategoryId, 
            C.Name AS CategoryName
        FROM 
            Feedbacks F
        JOIN 
            Categories C ON F.CategoryId = C.Id
        WHERE 
            F.Id = @Id
    END TRY
    BEGIN CATCH
        THROW
    END CATCH
END
GO

-- Create the stored procedure sp_GetCategories
CREATE OR ALTER PROCEDURE sp_GetCategories
AS
BEGIN
    BEGIN TRY
        SELECT 
            Id, 
            Name
        FROM 
            Categories
    END TRY
    BEGIN CATCH
        THROW
    END CATCH
END
GO

-- Add data to Categories if it's empty
IF NOT EXISTS (SELECT 1 FROM Categories)
BEGIN
    INSERT INTO Categories (Name) VALUES ('Category 1')
    INSERT INTO Categories (Name) VALUES ('Category 2')
    INSERT INTO Categories (Name) VALUES ('Category 3')
END

USE master
GO

-- Create the login and user for FeedbackSystem
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'FeedbackUser')
BEGIN
    CREATE LOGIN FeedbackUser WITH PASSWORD = 'FeedbackUser', CHECK_POLICY = OFF
END
GO

USE FeedbackSystem
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'FeedbackUser')
BEGIN
    CREATE USER FeedbackUser FOR LOGIN FeedbackUser
END
GO

IF NOT EXISTS (SELECT * FROM sys.database_role_members WHERE role_principal_id = DATABASE_PRINCIPAL_ID('sysadmin') AND member_principal_id = USER_ID('FeedbackUser'))
BEGIN
ALTER SERVER ROLE sysadmin ADD MEMBER FeedbackUser
END
GO