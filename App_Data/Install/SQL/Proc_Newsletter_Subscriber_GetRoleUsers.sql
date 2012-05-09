CREATE PROCEDURE [Proc_Newsletter_Subscriber_GetRoleUsers]
	@RoleID int,
	@TopN int,
	@LastUserID int,
	@MonitoringEnabled bit,
	@BounceLimit int
AS
BEGIN
    SET NOCOUNT ON
    
    -- Select only TOP N
    IF (@TopN > 0)
		BEGIN
			IF (@MonitoringEnabled = 1)
				BEGIN
						
					-- Get all active users in a role
					SELECT TOP (@TopN) UserID
					  FROM CMS_UserRole   
						   INNER JOIN
						   CMS_UserSettings ON CMS_UserRole.UserID = CMS_UserSettings.UserSettingsUserID
					 WHERE CMS_UserRole.RoleID = @RoleID AND
			 			   CMS_UserRole.UserID > @LastUserID AND
						   (CMS_UserSettings.UserBounces IS NULL OR CMS_UserSettings.UserBounces < @BounceLimit OR @BounceLimit = 0)	
					 ORDER BY UserID	
				END
			ELSE
				BEGIN
				
					-- Get all users in a role
					SELECT TOP (@TopN) UserID
					  FROM CMS_UserRole				   
					 WHERE CMS_UserRole.RoleID = @RoleID AND
						   CMS_UserRole.UserID > @LastUserID
					 ORDER BY UserID
				END
		END
	-- Select all
	ELSE
		BEGIN
			IF (@MonitoringEnabled = 1)
				BEGIN
						
					-- Get all active users in a role
					SELECT UserID
					  FROM CMS_UserRole   
						   INNER JOIN
						   CMS_UserSettings ON CMS_UserRole.UserID = CMS_UserSettings.UserSettingsUserID
					 WHERE CMS_UserRole.RoleID = @RoleID AND
			 			   CMS_UserRole.UserID > @LastUserID AND
						   (CMS_UserSettings.UserBounces IS NULL OR CMS_UserSettings.UserBounces < @BounceLimit OR @BounceLimit = 0)
					 ORDER BY UserID
				END
			ELSE
				BEGIN
				
					-- Get all users in a role
					SELECT UserID
					  FROM CMS_UserRole				   
					 WHERE CMS_UserRole.RoleID = @RoleID AND
						   CMS_UserRole.UserID > @LastUserID
					 ORDER BY UserID
				END
		END
END
