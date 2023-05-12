


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserById]
	-- Add the parameters for the stored procedure here
	@Id int
	
AS
BEGIN
	SET NOCOUNT ON;

   SELECT U.[Id]
      ,U.[Name]
      ,U.[USER_NAME]
      ,U.[PASSWORD]
	  ,R.Role_Name
	  ,R.Id AS Role_Id
	  ,UR.Id as UserRoleId
      ,U.[isActive] FROM [dbo].[User] AS U INNER JOIN [dbo].[User_Role] AS UR ON U.Id = UR.User_Id 
  INNER JOIN [dbo].[Role] AS R ON R.Id = UR.Role_Id
  WHERE U.Id = @Id 
END