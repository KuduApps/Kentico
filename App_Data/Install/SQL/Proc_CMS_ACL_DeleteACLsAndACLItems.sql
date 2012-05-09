CREATE PROCEDURE [Proc_CMS_ACL_DeleteACLsAndACLItems]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Delete ACLItems which ACLs are not used (are not in CMS_Tree)
	DELETE FROM [CMS_ACLItem] WHERE ACLID IN 
		(SELECT ACLID FROM CMS_ACL WHERE ACLID NOT IN 
			(SELECT NodeACLID FROM CMS_Tree));
	-- Delete ACLs which are not used (are not in CMS_Tree)
	DELETE FROM [CMS_ACL] WHERE ACLID NOT IN
		(SELECT NodeACLID FROM CMS_Tree);
END
