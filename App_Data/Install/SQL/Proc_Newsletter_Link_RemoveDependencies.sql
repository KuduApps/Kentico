-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Newsletter_Link_RemoveDependencies]
	@ID int
AS
BEGIN
SET NOCOUNT ON;	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION;
	
	-- Newsletter_SubscriberLink
    DELETE FROM Newsletter_SubscriberLink WHERE LinkID = @ID;       
	
	COMMIT TRANSACTION
	
END
