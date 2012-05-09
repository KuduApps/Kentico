CREATE PROCEDURE [Proc_CMS_Tree_ChangeACLID] 
	@NewACLID int,
    @SiteID int, 
    @NodeAliasPath  nvarchar(450)	
AS
BEGIN
	UPDATE CMS_Tree SET NodeACLID = @NewACLID WHERE NodeSiteID = @SiteID AND (NodeAliasPath LIKE @NodeAliasPath OR NodeAliasPath LIKE @NodeAliasPath + '/%')
	DELETE FROM CMS_ACLItem WHERE ACLID IN (SELECT ACLID FROM CMS_ACL LEFT JOIN CMS_Tree on NodeID = ACLOwnerNodeID WHERE NodeSiteID = @SiteID AND (NodeAliasPath LIKE @NodeAliasPath OR NodeAliasPath LIKE @NodeAliasPath + '/%'))
	DELETE FROM CMS_ACL WHERE ACLID IN (SELECT ACLID FROM CMS_ACL LEFT JOIN CMS_Tree on NodeID = ACLOwnerNodeID WHERE NodeSiteID = @SiteID AND (NodeAliasPath LIKE @NodeAliasPath OR NodeAliasPath LIKE @NodeAliasPath + '/%'))
END
