

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersList]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	SET NOCOUNT ON;

   SELECT U.ID,U.USER_NAME,U.PASSWORD,U.Name,U.isActive,R.Id AS Role_Id,R.Role_Name AS Role_Name FROM [dbo].[User] AS U 
   INNER JOIN [dbo].User_Role AS UR ON UR.User_Id = U.Id
   INNER JOIN [dbo].Role AS R ON R.ID = UR.Role_Id
   WHERE U.isActive = 1
   
END