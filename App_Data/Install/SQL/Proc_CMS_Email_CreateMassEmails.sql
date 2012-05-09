-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Email_CreateMassEmails]
	@EmailID int,
	@SiteID int,
	@users ntext,
	@roles ntext,
	@groups ntext,
	@Everyone bit
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION
    
	/* Select IDs of users who the e-mails will be send to */
	DECLARE @IDCursor CURSOR;
	IF @Everyone = 0
	BEGIN
		IF @SiteID > 0
		BEGIN
			SET @IDCursor = CURSOR FOR SELECT DISTINCT UserID FROM CMS_User WHERE (@users LIKE '%;'+cast(UserID as nvarchar(250))+';%')
			OR (UserID IN (SELECT UserID FROM View_CMS_UserRole_Joined WHERE (@roles LIKE '%;'+cast(RoleID as nvarchar(250))+';%' AND SiteID = @SiteID)))
			OR (UserID IN (SELECT MemberUserID FROM Community_GroupMember INNER JOIN Community_Group
			ON MemberGroupID=GroupID WHERE (@groups LIKE '%;'+cast(GroupID as nvarchar(250))+';%' AND GroupSiteID = @SiteID)));
		END
		ELSE
		BEGIN
			SET @IDCursor = CURSOR FOR SELECT DISTINCT UserID FROM CMS_User WHERE (@users LIKE '%;'+cast(UserID as nvarchar(250))+';%')
			OR (UserID IN (SELECT UserID FROM View_CMS_UserRole_Joined WHERE (@roles LIKE '%;'+cast(RoleID as nvarchar(250))+';%')))
			OR (UserID IN (SELECT MemberUserID FROM Community_GroupMember INNER JOIN Community_Group
			ON MemberGroupID=GroupID WHERE (@groups LIKE '%;'+cast(GroupID as nvarchar(250))+';%')));
		END
	END
	ELSE
	BEGIN
		if @SiteID > 0
		BEGIN
			/* Select all users of specified site with entered e-mail address */
			SET @IDCursor = CURSOR FOR SELECT DISTINCT CMS_User.UserID FROM
			CMS_User INNER JOIN CMS_UserSite ON	CMS_User.UserID = CMS_UserSite.UserID WHERE
			(NOT (Email IS NULL OR Email = '') AND SiteID = @SiteID);
		END
		ELSE
		BEGIN
			/* Select all users with entered e-mail address */
			SET @IDCursor = CURSOR FOR SELECT DISTINCT UserID FROM CMS_User WHERE (
			NOT (Email IS NULL OR Email = ''));
		END
	END
	DECLARE @UserID int;
	OPEN @IDCursor;
	FETCH NEXT FROM @IDCursor INTO @UserID;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Insert the binding */
		INSERT INTO CMS_EmailUser (EmailID, UserID, Status) VALUES (@EmailID, @UserID, 1);
	
		/* Get next record */
		FETCH NEXT FROM @IDCursor INTO @UserID;
	END
	/* Set pattern e-mail's status as 'waiting' so it is prepared to be sent  */
	UPDATE CMS_Email SET EmailStatus = 1 WHERE EmailID = @EmailID;
	COMMIT TRANSACTION;
END
