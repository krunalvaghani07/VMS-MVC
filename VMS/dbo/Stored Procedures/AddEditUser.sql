

CREATE PROCEDURE [dbo].[AddEditUser]
	@Id int,
	@Name nvarchar(500),
	@User_Name nvarchar(100),
	@Password nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF(@Id = 0)
	BEGIN
		INSERT INTO [dbo].[User] (Name, USER_NAME, PASSWORD, isActive) VALUES (@Name, @User_Name, @Password, 1)
		SELECT * FROM [dbo].[User] WHERE ID = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[User] SET Name = @Name, USER_NAME = @User_Name, PASSWORD = @Password WHERE ID = @ID 
		SELECT * FROM [dbo].[User] WHERE ID = @Id
	END

END