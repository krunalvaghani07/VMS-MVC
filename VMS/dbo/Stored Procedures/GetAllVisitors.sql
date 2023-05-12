

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllVisitors]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT [Id]
      ,[Name]
      ,[Address]
      ,[Phone]
      ,[Purpose]
      ,[Entry_Time]
      ,[Exit_Time]
      ,[Photo]
      ,[Person_to_Meet]
      ,[Department]
      ,[Carried_Assets]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
      ,[ModifiedBy]
  FROM [dbo].[Visitor] WHERE DATEDIFF(DAY, Entry_Time, GETDATE()) < 365
END