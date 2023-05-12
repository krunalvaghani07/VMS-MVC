-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE CheckUserPassword
	-- Add the parameters for the stored procedure here
	@User_Name NVARCHAR(100),
	@Password NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT [Id]
      ,[Name]
      ,[USER_NAME]
      ,[PASSWORD]
      ,[isActive]
  FROM [dbo].[User] WHERE USER_NAME = @User_Name AND PASSWORD = @Password
END