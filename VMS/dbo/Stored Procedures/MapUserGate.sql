
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MapUserGate] 
	@Id int,
	@UserId int,
	@GateId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF(@Id = 0)
	BEGIN
		INSERT INTO [dbo].[UserGate] (User_Id, Gate_Id) VALUES (@UserId, @GateId)
		SELECT Id AS UserGateId, USER_Id AS Id, Gate_Id AS GateId FROM [dbo].[UserGate] WHERE ID = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[UserGate] SET Gate_Id = @GateId WHERE ID = @ID 
		SELECT Id AS UserGateId, USER_Id AS Id, Gate_Id AS GateId FROM [dbo].[UserGate] WHERE Id = @Id
	END
END