-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Notification_Template_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from	
	SET NOCOUNT ON;
    DELETE FROM [Notification_Subscription] WHERE SubscriptionTemplateID=@ID;
	DELETE FROM [Notification_TemplateText] WHERE TemplateID=@ID;
END
