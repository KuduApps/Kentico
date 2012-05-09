CREATE VIEW [View_CMS_UserRole_MembershipRole_ValidOnly_Joined]
AS
SELECT UserID, RoleID, ValidTo  FROM CMS_UserRole WHERE ISNULL(ValidTo,DATEADD(DAY,1,GetDate())) > GetDate()
UNION ALL
SELECT UserID, RoleID, ValidTo FROM CMS_MembershipRole INNER JOIN CMS_MembershipUser ON CMS_MembershipUser.MembershipID = CMS_MembershipRole.MembershipID WHERE  ISNULL(ValidTo,DATEADD(DAY,1,GetDate())) > GetDate()
GO
