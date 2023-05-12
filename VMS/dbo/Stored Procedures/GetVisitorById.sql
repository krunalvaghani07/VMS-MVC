﻿
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetVisitorById]
	@Id int
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
      ,[Photo]
      ,[Person_to_Meet]
      ,[Department]
      ,[Carried_Assets]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[ModifiedOn]
	  ,[Exit_Time]
      ,[ModifiedBy]
  FROM [dbo].[Visitor] WHERE Id = @Id
END