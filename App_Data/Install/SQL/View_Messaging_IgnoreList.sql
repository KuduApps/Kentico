CREATE VIEW [View_Messaging_IgnoreList]
AS
SELECT     IgnoreListUserID, IgnoreListIgnoredUserID, UserName, UserNickName, FullName
FROM       View_CMS_User INNER JOIN
           Messaging_IgnoreList ON Messaging_IgnoreList.IgnoreListIgnoredUserID = UserID
GO
