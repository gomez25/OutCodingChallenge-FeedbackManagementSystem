-- =============================================
-- Author:		David Gomez
-- Create date: 11/04/2024
-- Description:	Get all feedbacks grouping by categories
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_GetFeedbacksGroupingByCategories]
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT
        c.Id AS 'CategoryId',
        c.Name AS 'CategoryName',
        f.Id AS 'FeedbackId',
        f.CustomerName,
        f.Description,
        f.SubmissionDate
    FROM
        Categories c
    JOIN
        Feedbacks f ON c.Id = f.CategoryId
    WHERE
        f.SubmissionDate BETWEEN @StartDate AND @EndDate
    ORDER BY
        c.Name

END
GO