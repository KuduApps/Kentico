CREATE VIEW [View_Community_Friend_RequestedFriends]
AS
SELECT     Community_Friend.FriendID, Community_Friend.FriendRequestedUserID, Community_Friend.FriendUserID, 
                      Community_Friend.FriendRequestedWhen, Community_Friend.FriendComment, Community_Friend.FriendApprovedBy, 
                      Community_Friend.FriendApprovedWhen, Community_Friend.FriendRejectedBy, Community_Friend.FriendRejectedWhen, 
                      Community_Friend.FriendGUID, Community_Friend.FriendStatus, View_CMS_User.*
FROM         Community_Friend INNER JOIN
                      View_CMS_User ON Community_Friend.FriendRequestedUserID = View_CMS_User.UserID
GO
