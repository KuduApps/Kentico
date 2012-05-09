CREATE PROCEDURE [Proc_CMS_Email_FetchEmailToSend] 
	@FetchFailed bit,
	@FetchNew bit,
	@FirstEmailID int,	
	@BatchSize int
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Emails TABLE (EmailID int, PRIMARY KEY(EmailID))
	
	SET ROWCOUNT @BatchSize
	
	IF @FetchFailed = 1 AND @FetchNew = 1
		/* Get failed and waiting */
		INSERT INTO @Emails 
			SELECT EmailID FROM CMS_Email WHERE (EmailStatus = 1 AND EmailID > @FirstEmailID) ORDER BY EmailID;
	ELSE IF @FetchNew = 1	
		/* Get only waiting */	
		INSERT INTO @Emails 
			SELECT EmailID FROM CMS_Email WHERE (EmailStatus = 1 AND EmailID > @FirstEmailID AND EmailLastSendResult IS NULL) ORDER BY EmailID;
				
	ELSE IF @FetchFailed = 1
		/* Get only failed */
		INSERT INTO @Emails 
			SELECT EmailID FROM CMS_Email WHERE (EmailStatus = 1 AND EmailID > @FirstEmailID AND EmailLastSendResult IS NOT NULL) ORDER BY EmailID;
	
	SET ROWCOUNT 0
	
	/* Status: 0 - created; 1 - waiting; 2 - sending; 3 - archived */
	UPDATE CMS_Email SET EmailStatus = 2 WHERE EmailID IN (SELECT EmailID FROM @Emails);	
	SELECT * FROM CMS_Email WHERE EmailID IN (SELECT EmailID FROM @Emails) ORDER BY EmailID;
END
