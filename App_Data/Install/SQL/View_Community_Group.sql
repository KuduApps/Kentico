CREATE VIEW [View_Community_Group]
AS
SELECT Community_Group.*, CMS_Avatar.AvatarID, CMS_Avatar.AvatarFileName, CMS_Avatar.AvatarGUID
FROM   Community_Group 
LEFT JOIN
CMS_Avatar ON Community_Group.GroupAvatarID = CMS_Avatar.AvatarID
GO
