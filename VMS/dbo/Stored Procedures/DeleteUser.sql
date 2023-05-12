

CREATE PROCEDURE [dbo].[DeleteUser]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
		UPDATE [dbo].[User] SET isActive = 0 WHERE ID = @ID 
		SELECT * FROM [dbo].[User] WHERE ID = @Id

END