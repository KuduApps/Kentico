CREATE PROCEDURE [Proc_CMS_User_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    -- deletes dependencies in CMS_UserSite, CMS_UserRole and CMS_EmailUser
    DELETE FROM [CMS_UserSite] WHERE UserID=@ID;
    DELETE FROM [CMS_UserRole] WHERE UserID=@ID;
    DELETE FROM [CMS_EmailUser] WHERE UserID=@ID;
    -- delete dependencies in CMS_UserCulture
    DELETE FROM [CMS_UserCulture] WHERE UserID=@ID;
    -- delete dependencies in Forums_ForumModerators
    DELETE FROM [Forums_ForumModerators] WHERE UserID=@ID;
    -- delete dependencies in Forums_ForumSubscription
    DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionUserID=@ID;
    -- updates dependencies in CMS_EventLog, CMC_Document, CMS_VersionHistory and CMS_WorkflowHistory
    UPDATE [CMS_Document] SET DocumentCreatedByUserID=NULL WHERE DocumentCreatedByUserID=@ID;
    UPDATE [CMS_Document] SET DocumentCheckedOutByUserID=NULL WHERE DocumentCheckedOutByUserID=@ID;
    UPDATE [CMS_Document] SET DocumentModifiedByUserID=NULL WHERE DocumentModifiedByUserID=@ID;
    UPDATE [CMS_EventLog] SET UserID=NULL WHERE UserID=@ID
    UPDATE CMS_VersionHistory SET ModifiedByUserID=NULL WHERE ModifiedByUserID=@ID;
    UPDATE CMS_VersionHistory SET VersionDeletedByUserID=NULL WHERE VersionDeletedByUserID=@ID;
    UPDATE CMS_WorkflowHistory SET ApprovedByUserID=NULL WHERE ApprovedByUserID=@ID;
    -- remove dependencies in NodeOwner
    UPDATE [CMS_Tree] SET NodeOwner=NULL WHERE NodeOwner=@ID;
    -- remove dependencies in CMS_ACLItem
    DELETE FROM CMS_ACLItem WHERE UserID = @ID;
    UPDATE CMS_ACLItem SET LastModifiedByUserID=NULL WHERE LastModifiedByUserID=@ID;
    -- update dependencies in COM_Customer, COM_OrderStatusUser, COM_ShoppingCart
    UPDATE COM_Customer SET CustomerUserID=NULL WHERE CustomerUserID=@ID;
    UPDATE COM_OrderStatusUser SET ChangedByUserID=NULL WHERE ChangedByUserID=@ID;
    UPDATE COM_ShoppingCart SET ShoppingCartUserID=NULL WHERE ShoppingCartUserID=@ID;
    -- remove dependencies in COM_UserDepartment
    DELETE FROM COM_UserDepartment WHERE UserID=@ID;
    -- remove dependencies in [COM_Wishlist]
    DELETE FROM [COM_Wishlist] WHERE UserID=@ID;
    -- set null to media file FKs
    UPDATE Media_File SET FileCreatedByUserID=NULL WHERE FileCreatedByUserID=@ID;
    UPDATE Media_File SET FileModifiedByUserID=NULL WHERE FileModifiedByUserID=@ID;
    -- set null to saved reports
    UPDATE Reporting_SavedReport SET SavedReportCreatedByUserID = NULL WHERE SavedReportCreatedByUserID=@ID
    -- set null to checkouted templates, layouts and css styles
    UPDATE CMS_PageTemplate SET PageTemplateLayoutCheckedOutByUserID = NULL WHERE PageTemplateLayoutCheckedOutByUserID = @ID;
    UPDATE CMS_Layout SET LayoutCheckedOutByUserID = NULL WHERE LayoutCheckedOutByUserID=@ID;
    UPDATE CMS_CssStyleSheet SET StylesheetCheckedOutByUserID = NULL WHERE StylesheetCheckedOutByUserID=@ID;
    -- Blog_Comment - set CommentUserID a CommentApprovedByUserID to null
    UPDATE Blog_Comment SET CommentUserID = NULL WHERE CommentUserID=@ID;
    UPDATE Blog_Comment SET CommentApprovedByUserID = NULL WHERE CommentApprovedByUserID=@ID;
    UPDATE Blog_PostSubscription SET SubscriptionUserID = NULL WHERE SubscriptionUserID=@ID;
    -- Messaging_Message - set MessageSenderUserID to null and MessageSenderDeleted to true
    UPDATE Messaging_Message SET MessageSenderUserID = NULL, MessageSenderDeleted=1 WHERE MessageSenderUserID=@ID;
    UPDATE Messaging_Message SET MessageRecipientUserID = NULL, MessageRecipientDeleted=1 WHERE MessageRecipientUserID=@ID;
    DELETE FROM Messaging_Message WHERE MessageRecipientDeleted=1 AND MessageSenderDeleted=1;
    -- ForumsPost
    UPDATE Forums_ForumPost SET PostUserID = NULL WHERE PostUserID = @ID;
    UPDATE Forums_ForumPost SET PostApprovedByUserID = NULL WHERE PostApprovedByUserID = @ID;
    -- Forum user favorites
    DELETE FROM [Forums_UserFavorites] WHERE UserID = @ID;
    -- Export history
    UPDATE Export_History SET ExportUserID = NULL
    -- Groups
    UPDATE [Community_Group] SET GroupCreatedByUserID = NULL WHERE GroupCreatedByUserID = @ID;
    UPDATE [Community_Group] SET GroupApprovedByUserID = NULL WHERE GroupApprovedByUserID = @ID;
    DELETE FROM [Community_GroupMember] WHERE MemberUserID = @ID;
    UPDATE [Community_GroupMember] SET MemberApprovedByUserID = NULL WHERE MemberApprovedByUserID = @ID;
    UPDATE [Community_GroupMember] SET MemberInvitedByUserID = NULL WHERE MemberInvitedByUserID = @ID;
    -- Notifications
    DELETE FROM Notification_Subscription WHERE SubscriptionUserID = @ID;
    -- Message boards
    DELETE Board_Moderator WHERE UserID = @ID;
    UPDATE Board_Message SET MessageUserID = NULL WHERE MessageUserID = @ID;
    UPDATE Board_Message SET MessageApprovedByUserID = NULL WHERE MessageApprovedByUserID = @ID;
    DELETE Board_Moderator WHERE UserID = @ID;
    -- User message boards
    DELETE FROM Board_Message WHERE MessageBoardID IN ( SELECT BoardID FROM Board_Board WHERE BoardUserID = @ID );
    DELETE FROM Board_Moderator WHERE BoardID IN ( SELECT BoardID FROM Board_Board WHERE BoardUserID = @ID );
    DELETE FROM Board_Role WHERE BoardID IN ( SELECT BoardID FROM Board_Board WHERE BoardUserID = @ID );        
  DELETE FROM Board_Subscription WHERE SubscriptionUserID = @ID; -- delete users subscriptions   
    DELETE FROM Board_Subscription WHERE SubscriptionBoardID IN (SELECT BoardID FROM Board_Board WHERE BoardUserID = @ID); --delete subscriptions on users boards
    DELETE FROM Board_Board WHERE BoardUserID = @ID;
    -- Abuse report
    UPDATE CMS_AbuseReport SET ReportUserID = NULL WHERE ReportUserID = @ID;
    -- Contact list
    DELETE Messaging_ContactList WHERE ContactListUserID = @ID OR ContactListContactUserID = @ID;
    -- Ignore list
    DELETE Messaging_IgnoreList WHERE IgnoreListUserID = @ID OR IgnoreListIgnoredUserID = @ID;
    -- Friends
    DELETE Community_Friend WHERE FriendRequestedUserID = @ID OR FriendUserID = @ID OR FriendApprovedBy = @ID OR FriendRejectedBy = @ID;
    -- Categories
    DELETE CMS_DocumentCategory WHERE CategoryID IN ( SELECT CategoryID FROM CMS_Category WHERE CategoryUserID = @ID );
    DELETE CMS_Category WHERE CategoryUserID = @ID;
    -- Delete user sessions
    DELETE FROM CMS_Session WHERE SessionUserID = @ID;
    -- Invitations
    DELETE FROM [Community_Invitation] WHERE InvitedUserID = @ID OR InvitedByUserID = @ID;
    -- Activated by user id
    UPDATE [CMS_UserSettings] SET UserActivatedByUserID = NULL WHERE UserActivatedByUserID = @ID;
    --Widgets
    DELETE FROM CMS_Personalization WHERE PersonalizationUserID = @ID;
    --OpenIDUser
    DELETE FROM CMS_OpenIDUser WHERE UserID = @ID;
    -- Membership
    DELETE FROM CMS_MembershipUser WHERE UserID = @ID
    --Newsletters
    DELETE FROM [Newsletter_SubscriberNewsletter] WHERE [SubscriberID] IN (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberType LIKE 'cms.user' AND SubscriberRelatedID = @ID);
  DELETE FROM [Newsletter_Emails] WHERE EmailSubscriberID IN (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberType LIKE 'cms.user' AND SubscriberRelatedID = @ID);
  DELETE FROM [Newsletter_OpenedEmail] WHERE SubscriberID IN (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberType LIKE 'cms.user' AND SubscriberRelatedID = @ID);
    DELETE FROM [Newsletter_Subscriber] WHERE SubscriberType LIKE 'cms.user' AND SubscriberRelatedID = @ID;
  -- Project management
  UPDATE [PM_Project] SET ProjectOwner = NULL WHERE ProjectOwner = @ID;
  UPDATE [PM_Project] SET ProjectCreatedByID = NULL WHERE ProjectCreatedByID = @ID;
  UPDATE [PM_ProjectTask] SET ProjectTaskCreatedByID = NULL WHERE ProjectTaskCreatedByID = @ID;
  -- Clear OwnerID, AssignedToUserID if it is a project task
  UPDATE [PM_ProjectTask] SET ProjectTaskOwnerID = NULL WHERE (ProjectTaskOwnerID = @ID) AND (ProjectTaskProjectID IS NOT NULL);
  UPDATE [PM_ProjectTask] SET ProjectTaskAssignedToUserID = NULL WHERE (ProjectTaskAssignedToUserID = @ID) AND (ProjectTaskProjectID IS NOT NULL);
  -- Ad-hoc tasks
  UPDATE [PM_ProjectTask] SET ProjectTaskOwnerID = COALESCE(ProjectTaskCreatedByID, ProjectTaskAssignedToUserID, ProjectTaskOwnerID)
    WHERE (ProjectTaskOwnerID = @ID) AND (ProjectTaskProjectID IS NULL);
  UPDATE [PM_ProjectTask] SET ProjectTaskAssignedToUserID = COALESCE(ProjectTaskOwnerID, ProjectTaskCreatedByID, ProjectTaskAssignedToUserID)
    WHERE (ProjectTaskAssignedToUserID = @ID) AND (ProjectTaskProjectID IS NULL);
  DELETE FROM [PM_ProjectTask]
  WHERE ((ProjectTaskCreatedByID = @ID) OR (ProjectTaskCreatedByID IS NULL)) AND
      ((ProjectTaskOwnerID = @ID) OR ProjectTaskOwnerID IS NULL) AND
      ((ProjectTaskAssignedToUserID = @ID) OR ProjectTaskAssignedToUserID IS NULL) AND
      (ProjectTaskProjectID IS NULL);  
 -- Object version history
 UPDATE [CMS_ObjectVersionHistory] SET VersionModifiedByUserID = NULL WHERE VersionModifiedByUserID = @ID;
 UPDATE [CMS_ObjectVersionHistory] SET VersionDeletedByUserID = NULL WHERE VersionDeletedByUserID = @ID;
    -- Online marketing - Contact management relation
  	DELETE FROM [OM_Membership] WHERE [RelatedID] = @ID AND [MemberType] = 0 -- 0 = user
    UPDATE [OM_Contact] SET [ContactOwnerUserID] = NULL WHERE [ContactOwnerUserID] = @ID
    UPDATE [OM_Account] SET [AccountOwnerUserID] = NULL WHERE [AccountOwnerUserID] = @ID
 
    COMMIT TRANSACTION;
END
