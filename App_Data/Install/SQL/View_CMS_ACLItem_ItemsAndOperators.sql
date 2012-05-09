CREATE VIEW [View_CMS_ACLItem_ItemsAndOperators]
AS
SELECT     CMS_ACL.ACLOwnerNodeID, CMS_ACLItem.ACLItemID, CMS_ACLItem.Allowed, CMS_ACLItem.Denied, 
                      CASE WHEN CMS_ACLItem.UserID IS NULL THEN 'R' + CAST(CMS_ACLItem.RoleID AS nvarchar(50)) 
                      ELSE 'U' + CAST(CMS_ACLItem.UserID AS nvarchar(50)) END AS Operator, CASE WHEN CMS_ACLItem.UserID IS NULL 
                      THEN CMS_Role.RoleDisplayName ELSE CMS_User.UserName END AS OperatorName, CMS_ACLItem.ACLID, 
                      CASE WHEN CMS_ACLItem.UserID IS NULL THEN NULL ELSE CMS_User.FullName END AS OperatorFullName,
                      CMS_ACLItem.UserID,CMS_ACLItem.RoleID, CASE WHEN CMS_ACLItem.RoleID IS NULL THEN NULL ELSE CMS_Role.RoleGroupID END AS RoleGroupID, 
                      CASE WHEN CMS_ACLItem.RoleID IS NULL THEN NULL ELSE CMS_Role.SiteID END AS SiteID
FROM         CMS_ACL INNER JOIN
                      CMS_ACLItem ON CMS_ACLItem.ACLID = CMS_ACL.ACLID LEFT OUTER JOIN
                      CMS_User ON CMS_ACLItem.UserID = CMS_User.UserID LEFT OUTER JOIN
                      CMS_Role ON CMS_ACLItem.RoleID = CMS_Role.RoleID
GO
