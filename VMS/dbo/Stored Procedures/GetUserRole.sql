-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetUserRole
	-- Add the parameters for the stored procedure here
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT U.[Id]
      ,U.[Name]
      ,U.[USER_NAME]
      ,U.[PASSWORD]
	  ,R.Role_Name
	  ,R.Id AS Role_Id
      ,U.[isActive]
  FROM [dbo].[User] AS U 
  INNER JOIN [dbo].[User_Role] AS UR ON U.Id = UR.User_Id 
  INNER JOIN [dbo].[Role] AS R ON R.Id = UR.Role_Id
  WHERE U.Id = @UserId
END