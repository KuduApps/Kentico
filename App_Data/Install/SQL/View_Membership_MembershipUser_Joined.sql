CREATE VIEW [View_Membership_MembershipUser_Joined]
AS
SELECT MembershipDisplayName, CMS_Membership.MembershipID, ValidTo , CMS_User.UserID, FullName,UserName, MembershipSiteID FROM CMS_MembershipUser 
JOIN CMS_Membership ON CMS_Membership.MembershipID = CMS_MembershipUser.MembershipID 
JOIN CMS_User ON CMS_MembershipUser.UserID = CMS_User.UserID
GO
