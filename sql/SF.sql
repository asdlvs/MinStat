USE [MinStat3]
GO

/****** Object:  UserDefinedFunction [dbo].[CountPersonByEnteprise]    Script Date: 07/18/2012 22:23:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[CountPersonByEnteprise] 
(
	-- Add the parameters for the function here
	@JobLevelId int,
	@EducationLevelId int,
	@ActivityId int,
	@EnterpriseId int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @resultCount int

	-- Add the T-SQL statements to compute the return value here
	SELECT @resultCount = COUNT(*) FROM 
		dbo.People p 
	WHERE 
		p.JobLevelId = @JobLevelId 
		and p.EducationLevelId = @EducationLevelId 
		and p.ActivityId = @ActivityId
		and p.SummaryId = (SELECT TOP 1 Id FROM dbo.Summaries s WHERE s.EnterpriseId = @EnterpriseId ORDER BY s.Id DESC)

	-- Return the result of the function
	RETURN @resultCount

END

GO

