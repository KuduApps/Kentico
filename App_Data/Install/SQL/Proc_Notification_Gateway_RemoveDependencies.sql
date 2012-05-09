CREATE PROCEDURE [Proc_Notification_Gateway_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from  
  SET NOCOUNT ON;
    DELETE FROM [Notification_Subscription] WHERE SubscriptionGatewayID=@ID;
  DELETE FROM [Notification_TemplateText] WHERE GatewayID=@ID;
END
