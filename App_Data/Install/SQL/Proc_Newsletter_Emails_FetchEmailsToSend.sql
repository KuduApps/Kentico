CREATE PROCEDURE [Proc_Newsletter_Emails_FetchEmailsToSend]
    @FetchFailed bit,
    @FetchNew bit,
    @FirstEmailID int,
    @IssueID int
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Emails TABLE (
        EmailID int
    );
    BEGIN TRANSACTION
    IF @IssueID > 0
    BEGIN
        IF @FetchFailed = 1 AND @FetchNew = 1
            BEGIN
                INSERT INTO @Emails SELECT TOP 10 EmailID FROM Newsletter_Emails WHERE (EmailID > @FirstEmailID) AND (EmailSending IS NULL OR EmailSending = 0) AND (EmailNewsletterIssueID = @IssueID);
            END
        ELSE IF @FetchNew = 1
            BEGIN
                INSERT INTO @Emails SELECT TOP 10 EmailID FROM Newsletter_Emails WHERE (EmailID > @FirstEmailID) AND (EmailSending IS NULL OR EmailSending = 0) AND (EmailNewsletterIssueID = @IssueID) AND (EmailLastSendResult IS NULL OR EmailLastSendResult LIKE '');
            END
        ELSE
            BEGIN
                INSERT INTO @Emails SELECT TOP 10 EmailID FROM Newsletter_Emails WHERE (EmailID > @FirstEmailID) AND (EmailSending IS NULL OR EmailSending = 0) AND (EmailNewsletterIssueID = @IssueID) AND ((NOT EmailLastSendResult IS NULL) OR (EmailLastSendResult NOT LIKE ''));
            END
    END
    ELSE
    BEGIN
        IF @FetchFailed = 1 AND @FetchNew = 1
            BEGIN
                INSERT INTO @Emails SELECT TOP 10 EmailID FROM Newsletter_Emails WHERE (EmailID > @FirstEmailID) AND (EmailSending IS NULL OR EmailSending = 0);
            END
        ELSE IF @FetchNew = 1
            BEGIN
                INSERT INTO @Emails SELECT TOP 10 EmailID FROM Newsletter_Emails WHERE (EmailID > @FirstEmailID) AND (EmailSending IS NULL OR EmailSending = 0) AND (EmailLastSendResult IS NULL OR EmailLastSendResult LIKE '');
            END
        ELSE
            BEGIN
                INSERT INTO @Emails SELECT TOP 10 EmailID FROM Newsletter_Emails WHERE (EmailID > @FirstEmailID) AND (EmailSending IS NULL OR EmailSending = 0) AND ((NOT EmailLastSendResult IS NULL) OR (EmailLastSendResult NOT LIKE ''));
            END
    END
    
    UPDATE Newsletter_Emails SET EmailSending = 1 WHERE EmailID IN (SELECT EmailID FROM @Emails);
    SELECT * FROM Newsletter_Emails WHERE EmailID IN (SELECT EmailID FROM @Emails);
    COMMIT TRANSACTION
END
