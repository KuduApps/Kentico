CREATE PROCEDURE [Proc_Newsletter_Subscriber_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  
  BEGIN TRANSACTION;
  
  -- Newsletter_SubscriberNewsletter
    DELETE FROM Newsletter_SubscriberNewsletter WHERE SubscriberID = @ID; 
  
  -- Newsletter_Emails
	DELETE FROM Newsletter_Emails WHERE EmailSubscriberID = @ID;
  
  -- Newsletter_SubscriberLink
    DELETE FROM Newsletter_SubscriberLink WHERE SubscriberID = @ID;           
  
  -- Newsletter_OpenedEmail
    DELETE FROM Newsletter_OpenedEmail WHERE SubscriberID = @ID;
    
  -- Online marekting membership relation
    DELETE FROM OM_Membership WHERE RelatedID = @ID AND MemberType = 2; -- 2 = newsletter subscriber  
    
  COMMIT TRANSACTION;
END
