CREATE PROCEDURE [Proc_OM_Score_UpdateContactScore]
@RuleID int,
 @WhereCond nvarchar(max),
 @ContactID int,
 @RuleExpiration datetime,
 @RuleValidUnits int,
 @RuleValidFor int
AS
BEGIN
DECLARE @ruleType int;
DECLARE @ruleScoreID int;
DECLARE @ruleValue int;
DECLARE @ruleParameter nvarchar(250);
DECLARE @ruleIsRecurring bit;
DECLARE @ruleSiteID int;
DECLARE @ruleMaxPoints int;
DECLARE @currentContactID int;
DECLARE @previousContactID int;
DECLARE @currentPoints int;
DECLARE @currentExpiration datetime;
DECLARE @expirationDate nvarchar(300);
DECLARE @timeRestriction nvarchar(300);
-- Exception handling
DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorNumber INT;
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;
DECLARE @maxValue int;
-- Get rule info
SELECT @ruleSiteID=[RuleSiteID], @ruleType=[RuleType], @ruleScoreID=[RuleScoreID], @ruleValue=[RuleValue], @ruleParameter=[RuleParameter], @ruleIsRecurring=[RuleIsRecurring],
@ruleMaxPoints=[RuleMaxPoints] FROM OM_Rule WHERE RuleID=@RuleID
-- Init temporary table for storing contact IDs (these contacts will be updated)
CREATE TABLE #tempContactIDs (ContactID int NOT NULL, Points int NOT NULL, Expiration datetime NULL);    
------------------------------------------ Activity rule
IF @ruleType=0
BEGIN
    IF @ContactID=0 OR @ContactID IS NULL
    BEGIN
      -- Retrieve all contacts for specified condition
      SET @WhereCond = 'ContactMergedWithContactID IS NULL AND ContactSiteID=' + CAST(@ruleSiteID as varchar(15)) + ' AND (' + @WhereCond + ')';
    END
    ELSE
    BEGIN
      -- Try to retrieve given contact for specified condition
      SET @WhereCond = 'ContactMergedWithContactID IS NULL AND ContactID=' + CAST(@ContactID as varchar(50)) + ' AND (' + @WhereCond + ')';
    END
    -- Prepare expiration date query
    SET @expirationDate =
    CASE @RuleValidUnits
       WHEN 0 THEN 'DATEADD(dd, ' + CAST(@RuleValidFor as varchar(50)) + ', MAX(ActivityCreated)) AS ActCreated'
       WHEN 1 THEN 'DATEADD(wk, ' + CAST(@RuleValidFor as varchar(50)) + ', MAX(ActivityCreated)) AS ActCreated'
       WHEN 2 THEN 'DATEADD(mm, ' + CAST(@RuleValidFor as varchar(50)) + ', MAX(ActivityCreated)) AS ActCreated'
       WHEN 3 THEN 'DATEADD(yy, ' + CAST(@RuleValidFor as varchar(50)) + ', MAX(ActivityCreated)) AS ActCreated'
       ELSE ISNULL('''' + CAST(@RuleExpiration as varchar(250)) + '''', 'NULL') + ' AS ActCreated'
    END;
    -- Prepare time restriction in the past for time interval activities
    SET @timeRestriction =
    CASE @RuleValidUnits
       WHEN 0 THEN 'ActivityCreated >= DATEADD(dd, -' + CAST(@RuleValidFor as varchar(50)) + ', GETDATE()) AND '
       WHEN 1 THEN 'ActivityCreated >= DATEADD(wk, -' + CAST(@RuleValidFor as varchar(50)) + ', GETDATE()) AND '
       WHEN 2 THEN 'ActivityCreated >= DATEADD(mm, -' + CAST(@RuleValidFor as varchar(50)) + ', GETDATE()) AND '
       WHEN 3 THEN 'ActivityCreated >= DATEADD(yy, -' + CAST(@RuleValidFor as varchar(50)) + ', GETDATE()) AND '
       ELSE ''
    END;
    -- Join additional table for page visit and landing page
    IF @ruleParameter='pagevisit' OR @ruleParameter='landingpage'
    BEGIN
      BEGIN TRY
        EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, COUNT(ContactID)*' + @ruleValue + ' AS Points, ' + @expirationDate +
        ' FROM OM_Activity INNER JOIN OM_Contact ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID LEFT JOIN OM_PageVisit ON OM_Activity.ActivityID=OM_PageVisit.PageVisitActivityID ' +
        'WHERE ' + @timeRestriction + ' (' + @WhereCond + ') GROUP BY ContactID ORDER BY ContactID');
      END TRY
      BEGIN CATCH
        -- Get error info
        SELECT @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        
        IF @ErrorNumber = 8115   -- Is it arithmetic overflow error?
        BEGIN
          IF (@ruleValue >= 0)
             SET @maxValue = 2147483647;
          ELSE
             SET @maxValue = -2147483647;
          -- Set max value
          EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, ' + @maxValue + ' AS Points, ' + @expirationDate +
          ' FROM OM_Activity INNER JOIN OM_Contact ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID LEFT JOIN OM_PageVisit ON OM_Activity.ActivityID=OM_PageVisit.PageVisitActivityID ' +
          'WHERE ' + @timeRestriction + ' (' + @WhereCond + ') GROUP BY ContactID ORDER BY ContactID');
        END
        ELSE
        BEGIN
          RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        END      
      END CATCH
    END
    ELSE
    BEGIN
      -- Join additional table for internal and external search 
      IF @ruleParameter='internalsearch' OR @ruleParameter='externalsearch'
      BEGIN
        BEGIN TRY
          EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, COUNT(ContactID)*' + @ruleValue + ' AS Points, ' + @expirationDate +
          ' FROM OM_Activity INNER JOIN OM_Contact ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID LEFT JOIN OM_Search ON OM_Activity.ActivityID=OM_Search.SearchActivityID ' +
          'WHERE ' + @timeRestriction + ' (' + @WhereCond + ') GROUP BY ContactID ORDER BY ContactID');
        END TRY
        BEGIN CATCH
          -- Get error info
          SELECT @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        
          IF @ErrorNumber = 8115   -- Is it arithmetic overflow error?
          BEGIN
            IF (@ruleValue >= 0)
               SET @maxValue = 2147483647;
            ELSE
               SET @maxValue = -2147483647;
            -- Set max value
            EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, ' + @maxValue + ' AS Points, ' + @expirationDate +
            ' FROM OM_Activity INNER JOIN OM_Contact ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID LEFT JOIN OM_Search ON OM_Activity.ActivityID=OM_Search.SearchActivityID ' +
            'WHERE ' + @timeRestriction + ' (' + @WhereCond + ') GROUP BY ContactID ORDER BY ContactID');
          END
          ELSE
          BEGIN
            RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
          END      
        END CATCH         
      END
      ELSE
      BEGIN
        BEGIN TRY
          EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, COUNT(ContactID)*' + @ruleValue + ' AS Points, ' + @expirationDate +
          ' FROM OM_Activity INNER JOIN OM_Contact ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID WHERE ' + @timeRestriction +
          ' (' + @WhereCond + ') GROUP BY ContactID ORDER BY ContactID');
        END TRY
        BEGIN CATCH
          -- Get error info
          SELECT @ErrorNumber = ERROR_NUMBER(), @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        
          IF @ErrorNumber = 8115   -- Is it arithmetic overflow error?
          BEGIN
            IF (@ruleValue >= 0)
               SET @maxValue = 2147483647;
            ELSE
               SET @maxValue = -2147483647;
            -- Set max value
            EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, ' + @maxValue + ' AS Points, ' + @expirationDate +
            ' FROM OM_Activity INNER JOIN OM_Contact ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID WHERE ' + @timeRestriction +
            ' (' + @WhereCond + ') GROUP BY ContactID ORDER BY ContactID');
          END
          ELSE
          BEGIN
            RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
          END      
        END CATCH
      END
    END
   
    -- Set (update) correct points for non-recurring activities
    IF @ruleIsRecurring=0
    BEGIN
      UPDATE #tempContactIDs SET [Points]=@ruleValue
    END
    ELSE
    BEGIN
      IF (@ruleMaxPoints IS NOT NULL) AND (@ruleMaxPoints <> 0)
      BEGIN
         IF @ruleMaxPoints > 0
           UPDATE #tempContactIDs SET [Points]=@ruleMaxPoints WHERE [Points]>@ruleMaxPoints
         ELSE
           UPDATE #tempContactIDs SET [Points]=@ruleMaxPoints WHERE [Points]<@ruleMaxPoints
      END
    END
------------------------------------------ Attribute rule    
END ELSE IF @ruleType=1
BEGIN
    IF @ContactID=0 OR @ContactID IS NULL
    BEGIN
      -- Retrieve all contacts for specified condition
      EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, ' + @ruleValue + ', NULL FROM OM_Contact WHERE ContactMergedWithContactID IS NULL AND ContactSiteID=' + @ruleSiteID + ' AND (' + @WhereCond + ') ORDER BY ContactID');     
      -- Remove scores of particular rule for all contacts
    END
    ELSE
    BEGIN
      -- Try to retrieve given contact for specified condition
      EXEC ('INSERT INTO #tempContactIDs SELECT ContactID, ' + @ruleValue + ', NULL FROM OM_Contact WHERE ContactMergedWithContactID IS NULL AND ContactID=' + @ContactID + ' AND (' + @WhereCond + ')');
      -- Remove scores of particular rule for the given contact
    END
END
------------------------------------------ Recalculation
-- Init current contact ID for the first run
SELECT TOP 1 @currentContactID=[ContactID], @currentPoints=[Points], @currentExpiration=[Expiration] FROM #tempContactIDs
-- Insert(calculate) new points
WHILE (@currentContactID IS NOT NULL)
BEGIN
  SET @previousContactID=@currentContactID;    
  INSERT INTO OM_ScoreContactRule(ScoreID,ContactID,RuleID,Value,Expiration) VALUES (@ruleScoreID,@currentContactID,@RuleID,@currentPoints,@currentExpiration)
  -- Move to next contact
  SET @currentContactID=NULL
  SELECT TOP 1 @currentContactID=[ContactID], @currentPoints=[Points], @currentExpiration=[Expiration] FROM #tempContactIDs WHERE ContactID>@previousContactID;     
END
-- Delete temporary table
DROP TABLE #tempContactIDs     
END
