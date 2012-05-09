CREATE PROCEDURE [Proc_CMS_Role_RemoveDependencies]
    @ID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    DELETE FROM CMS_UserRole WHERE RoleID=@ID;
    DELETE FROM CMS_RolePermission WHERE RoleID=@ID; 
    DELETE FROM CMS_ACLItem WHERE RoleID=@ID;
    DELETE FROM CMS_WorkflowStepRoles WHERE RoleID=@ID; 
    DELETE FROM Forums_ForumRoles WHERE RoleID = @ID;
    DELETE FROM Polls_PollRoles WHERE RoleID = @ID;
    DELETE FROM Media_LibraryRolePermission WHERE RoleID = @ID;
    DELETE FROM Community_GroupRolePermission WHERE RoleID = @ID;
    DELETE FROM CMS_WidgetRole WHERE RoleID = @ID;
    DELETE FROM CMS_FormRole WHERE RoleID = @ID;
    -- Message boards
    DELETE FROM Board_Role WHERE RoleID = @ID;
    -- UIProfiles
    DELETE FROM CMS_RoleUIElement WHERE RoleID = @ID;
    --Newsletters
    DELETE FROM [Newsletter_SubscriberNewsletter] WHERE [SubscriberID] IN (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberType LIKE 'cms.role' AND SubscriberRelatedID = @ID);
	DELETE FROM [Newsletter_Emails] WHERE EmailSubscriberID IN (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberType LIKE 'cms.role' AND SubscriberRelatedID = @ID);
	DELETE FROM [Newsletter_OpenedEmail] WHERE SubscriberID IN (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberType LIKE 'cms.role' AND SubscriberRelatedID = @ID);
    DELETE FROM [Newsletter_Subscriber] WHERE SubscriberType LIKE 'cms.role' AND SubscriberRelatedID = @ID;
	-- Project Management
	DELETE FROM [PM_ProjectRolePermission] WHERE RoleID = @ID;   
	-- Membership
	DELETE FROM [CMS_MembershipRole] WHERE RoleID = @ID;  
    
    COMMIT TRANSACTION;
END
