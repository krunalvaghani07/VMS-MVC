


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ExitVisitor] 
	@Id INT,
	@Exit_Time Datetime
AS
BEGIN
	
		UPDATE [dbo].[Visitor] set Exit_Time = @Exit_Time WHERE Id = @Id;
		Select * FROM  [dbo].[Visitor] WHERE Id = @Id;

END