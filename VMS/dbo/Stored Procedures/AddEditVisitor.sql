

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddEditVisitor] 
	@Id INT,
	@CreatedBy INT,
	@ModifiedBy INT,
	@Name NVARCHAR(MAX),
	@Address NVARCHAR(MAX),
	@Phone NVARCHAR(10),
	@Purpose NVARCHAR(MAX),
	@Photo NVARCHAR(MAX),
	@Department NVARCHAR(100),
	@Carried_Assets NVARCHAR(MAX),
	@CreatedOn DATETIME,
	@ModifiedOn DATETIME,
	@Entry_Time DATETIME,
	@Exit_Time DATETIME,
	@Person_to_Meet NVARCHAR(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@Id = 0)
	BEGIN
		INSERT INTO [dbo].[Visitor] (Name,Address,Phone,Purpose,Entry_Time,Exit_Time,Photo,Person_to_Meet,Department,Carried_Assets,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy) values (@Name,@Address,@Phone,@Purpose,@Entry_Time,@Exit_Time,@Photo,@Person_to_Meet,@Department,@Carried_Assets,@CreatedOn,@CreatedBy,@ModifiedOn,@ModifiedBy)
		SELECT * FROM [dbo].[Visitor] WHERE Id = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Visitor] SET NAME = @Name,
			   Address = @Address, Phone = @Phone, Purpose = @Purpose,
			   Photo = @Photo, Department = @Department, Carried_Assets = @Carried_Assets,
			   ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn, Entry_Time = @Entry_Time, Exit_Time =@Exit_Time,
			   Person_to_Meet = @Person_to_Meet WHERE Id = @Id

	    SELECT * FROM [dbo].[Visitor] WHERE Id = @Id
	END
END