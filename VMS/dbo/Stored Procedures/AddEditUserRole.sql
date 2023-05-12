


CREATE PROCEDURE [dbo].[AddEditUserRole]
	@User_Id int,
	@Role_Id int,
	@UserRole_Id int
AS
BEGIN


	IF (@UserRole_Id = 0 )
	BEGIN
		INSERT INTO dbo.User_Role (User_Id,Role_Id) values (@User_Id,@Role_Id)
	END
	ELSE
	BEGIN
		UPDATE dbo.User_Role set Role_Id =@Role_Id where Id = @UserRole_Id
	END
    

END