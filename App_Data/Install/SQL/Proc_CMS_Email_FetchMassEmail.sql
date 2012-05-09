CREATE PROCEDURE [Proc_CMS_Email_FetchMassEmail]
	@FetchFailed bit,
	@FetchNew bit,
	@EmailID int,
	@FirstUserID int,
	@BatchSize int
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Users TABLE (UserID int, PRIMARY KEY(UserID))
	BEGIN TRANSACTION
	
	SET ROWCOUNT @BatchSize
	
	IF @FetchFailed = 1 AND @FetchNew = 1
		/* Get failed and waiting */
		INSERT INTO @Users 
			SELECT UserID FROM CMS_EmailUser 
				WHERE (EmailID = @EmailID AND UserID > @FirstUserID AND Status = 1) ORDER BY UserID;
		
	ELSE IF @FetchNew = 1		
		/* Get only waiting */
		INSERT INTO @Users 
			SELECT UserID FROM CMS_EmailUser 
				WHERE (EmailID = @EmailID AND UserID > @FirstUserID AND Status = 1 AND LastSendResult IS NULL) ORDER BY UserID;		
		
	ELSE IF @FetchFailed = 1
		/* Get only failed */
		INSERT INTO @Users 
			SELECT UserID FROM CMS_EmailUser 
				WHERE (EmailID = @EmailID AND UserID > @FirstUserID AND Status = 1 AND LastSendResult IS NOT NULL) ORDER BY UserID;
		
	ELSE
		/* Get archived */
		INSERT INTO @Users 
			SELECT UserID FROM CMS_EmailUser 
				WHERE (EmailID = @EmailID AND UserID > @FirstUserID AND Status = 3) ORDER BY UserID;	
	
	SET ROWCOUNT 0
	
	/* Status: 0 - created; 1 - waiting; 2 - sending; 3 - archived */
	UPDATE CMS_EmailUser SET Status = 2 WHERE (EmailID = @EmailID AND UserID IN (SELECT UserID FROM @Users));
	
	COMMIT TRANSACTION
	SELECT UserID, Email FROM CMS_User WHERE UserID IN (SELECT UserID FROM @Users);
END
