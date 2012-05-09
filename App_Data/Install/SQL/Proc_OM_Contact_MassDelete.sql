CREATE PROCEDURE [Proc_OM_Contact_MassDelete]
	@where nvarchar(max),
	@batchLimit int
AS
BEGIN
	SET NOCOUNT ON;
	-- Variables
	DECLARE @DeletedContacts TABLE (
		ContactID int NOT NULL
	);	
	DECLARE @currentContactSiteID int;
	DECLARE @currentDeletedContactID int;
	DECLARE @sqlQuery NVARCHAR(MAX);
	DECLARE @listStr NVARCHAR(MAX);
	DECLARE @top NVARCHAR(20);
	
	-- Limit processed batch
	IF ((@batchLimit IS NOT NULL) OR (@batchLimit > 0))
		SET @top = 'TOP ' + CAST(@batchLimit AS NVARCHAR(10));
	ELSE 
		SET @top = 'TOP 1000';
	
	SET @sqlQuery = 'SELECT ' + @top + ' ContactID FROM OM_Contact WHERE ' + @where;
	INSERT INTO @DeletedContacts EXEC(@sqlQuery);
	
	-- Process first batch of records
	WHILE ((SELECT Count(*) FROM @DeletedContacts) > 0)
	BEGIN			
		-- Loop through records
		IF ((SELECT Count(*) FROM @DeletedContacts) > 0)
		BEGIN	
			-- Add merged contacts to deleted list
			SELECT @listStr = COALESCE(@listStr+',' ,'') + CAST(ContactID AS nvarchar(10)) FROM @DeletedContacts;
			INSERT INTO @DeletedContacts SELECT * FROM Func_OM_Contact_GetChildren_Multiple(@listStr, 1);
			
			/* Remove all references */
			UPDATE o SET o.AccountPrimaryContactID = NULL FROM OM_Account o INNER JOIN @DeletedContacts d ON o.AccountPrimaryContactID = d.ContactID;
			UPDATE o SET o.AccountSecondaryContactID = NULL FROM OM_Account o INNER JOIN @DeletedContacts d ON o.AccountSecondaryContactID = d.ContactID;
			/* Remove all relations */
			DELETE o FROM OM_AccountContact o INNER JOIN @DeletedContacts d ON o.ContactID = d.ContactID;
			DELETE o FROM OM_ContactGroupMember o LEFT JOIN @DeletedContacts d ON o.ContactGroupMemberRelatedID = d.ContactID WHERE o.ContactGroupMemberType=0;
			DELETE o FROM OM_Membership o LEFT JOIN @DeletedContacts do ON o.OriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.ActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			DELETE o FROM OM_IP o LEFT JOIN @DeletedContacts do ON o.IPOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.IPActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			DELETE o FROM OM_UserAgent o LEFT JOIN @DeletedContacts do ON o.UserAgentOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.UserAgentActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			DELETE o FROM OM_ScoreContactRule o INNER JOIN @DeletedContacts d ON o.ContactID = d.ContactID;
			/* Delete relations from depending activity */
			DELETE o FROM OM_PageVisit o INNER JOIN OM_Activity a ON o.PageVisitActivityID = a.ActivityID LEFT JOIN @DeletedContacts do ON a.ActivityOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON a.ActivityActiveContactID = da.ContactID WHERE (do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL) AND (a.ActivityType = 'pagevisit' OR a.ActivityType = 'landingpage');
			DELETE o FROM OM_Search o INNER JOIN OM_Activity a ON o.SearchActivityID = a.ActivityID LEFT JOIN @DeletedContacts do ON a.ActivityOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON a.ActivityActiveContactID = da.ContactID WHERE (do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL) AND (a.ActivityType = 'internalsearch' OR a.ActivityType = 'externalsearch');
			DELETE o FROM OM_Activity o LEFT JOIN @DeletedContacts do ON o.ActivityOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.ActivityActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			
			-- Delete merged and parent records
			DELETE o FROM OM_Contact o INNER JOIN @DeletedContacts d ON o.ContactID = d.ContactID;
			DELETE FROM @DeletedContacts
		END
		
		-- Get next batch
		IF (@batchLimit IS  NULL)
			INSERT INTO @DeletedContacts EXEC(@sqlQuery);
	END
END
