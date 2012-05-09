-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Email_DeleteEmail]
    @ID int 
AS
BEGIN
    BEGIN TRANSACTION;
	SET NOCOUNT ON;
    -- Delete attachment binding
    DELETE FROM CMS_AttachmentForEmail WHERE EmailID = @ID;
	-- Delete bindings from CMS_EmailUser
	DELETE FROM CMS_EmailUser WHERE EmailID = @ID;
    -- Delete all attachments that have no bindings
    DELETE FROM CMS_EmailAttachment WHERE
    AttachmentID NOT IN (SELECT DISTINCT AttachmentID FROM CMS_AttachmentForEmail)
    DELETE FROM CMS_Email WHERE EmailID = @ID
    COMMIT TRANSACTION;
END
