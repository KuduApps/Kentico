CREATE PROCEDURE [Proc_CMS_Email_ExpiredCount]
@Now datetime
AS
BEGIN
SET NOCOUNT ON;
-- Get default value for number of days to keep archived emails
DECLARE @DefaultDays AS int
SET @DefaultDays = (SELECT KeyValue FROM CMS_SettingsKey WHERE KeyName = 'CMSArchiveEmails' AND SiteID IS NULL)
-- Create a table variable that contains SiteID's and Expiration Dates for archived emails
-- Expiration dates are calculated as today - number of days to archive emails
DECLARE @ExpTable AS TABLE ( SiteID int NOT NULL, ExpirationDate datetime NOT NULL )
INSERT INTO @ExpTable 
	SELECT ISNULL(SiteID, 0), DATEADD(day, -CONVERT(int, ISNULL(KeyValue, @DefaultDays)), @Now) AS ExpirationDate
	FROM CMS_SettingsKey
	WHERE KeyName = 'CMSArchiveEmails' AND ISNULL(KeyValue, @DefaultDays) > 0
DECLARE @Archived AS int
SET @Archived = 3
-- Get number of expired emails in archive for each site along with the expiration date
SELECT ISNULL(CMS_Email.EmailSiteID, 0) AS SiteID, ExpTable.ExpirationDate AS ExpirationDate, COUNT(*) AS ExpiredEmailsCount
FROM CMS_Email LEFT OUTER JOIN  @ExpTable AS ExpTable ON ISNULL(CMS_Email.EmailSiteID, 0) = ExpTable.SiteID
WHERE EmailStatus = @Archived AND EmailLastSendAttempt <= ExpirationDate
GROUP BY EmailSiteID, ExpTable.ExpirationDate
END
