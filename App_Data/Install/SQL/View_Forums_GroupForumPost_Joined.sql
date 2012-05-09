CREATE VIEW [View_Forums_GroupForumPost_Joined]
AS
SELECT     Forums_Forum.ForumID, Forums_Forum.ForumGroupID, Forums_Forum.ForumName, Forums_Forum.ForumDisplayName, 
                      Forums_Forum.ForumDescription, Forums_Forum.ForumOrder, Forums_Forum.ForumDocumentID, Forums_Forum.ForumOpen, 
                      Forums_Forum.ForumModerated, Forums_Forum.ForumDisplayEmails, Forums_Forum.ForumRequireEmail, Forums_Forum.ForumAccess, 
                      Forums_Forum.ForumThreads, Forums_Forum.ForumPosts, Forums_Forum.ForumLastPostTime, Forums_Forum.ForumLastPostUserName, 
                      Forums_Forum.ForumBaseUrl, Forums_Forum.ForumAllowChangeName, Forums_Forum.ForumHTMLEditor, Forums_Forum.ForumUseCAPTCHA, 
                      Forums_Forum.ForumGUID, Forums_Forum.ForumLastModified, Forums_Forum.ForumUnsubscriptionUrl, Forums_Forum.ForumIsLocked, 
                      Forums_Forum.ForumSettings, Forums_Forum.ForumAuthorEdit, Forums_Forum.ForumAuthorDelete, Forums_Forum.ForumType, 
                      Forums_Forum.ForumIsAnswerLimit, Forums_Forum.ForumImageMaxSideSize, Forums_Forum.ForumLastPostTimeAbsolute, 
                      Forums_Forum.ForumLastPostUserNameAbsolute, Forums_Forum.ForumPostsAbsolute, Forums_Forum.ForumThreadsAbsolute, 
                      Forums_Forum.ForumAttachmentMaxFileSize, Forums_Forum.ForumDiscussionActions, Forums_Forum.ForumSiteID, Forums_ForumGroup.GroupID, 
                      Forums_ForumGroup.GroupSiteID, Forums_ForumGroup.GroupName, Forums_ForumGroup.GroupDisplayName, Forums_ForumGroup.GroupOrder, 
                      Forums_ForumGroup.GroupDescription, Forums_ForumGroup.GroupGUID, Forums_ForumGroup.GroupLastModified, 
                      Forums_ForumGroup.GroupBaseUrl, Forums_ForumGroup.GroupUnsubscriptionUrl, Forums_ForumGroup.GroupGroupID, 
                      Forums_ForumGroup.GroupAuthorEdit, Forums_ForumGroup.GroupAuthorDelete, Forums_ForumGroup.GroupType, 
                      Forums_ForumGroup.GroupIsAnswerLimit, Forums_ForumGroup.GroupImageMaxSideSize, Forums_ForumGroup.GroupDisplayEmails, 
                      Forums_ForumGroup.GroupRequireEmail, Forums_ForumGroup.GroupHTMLEditor, Forums_ForumGroup.GroupUseCAPTCHA, 
                      Forums_ForumGroup.GroupAttachmentMaxFileSize, Forums_ForumGroup.GroupDiscussionActions, Forums_ForumPost.PostId, 
                      Forums_ForumPost.PostForumID, Forums_ForumPost.PostParentID, Forums_ForumPost.PostIDPath, Forums_ForumPost.PostLevel, 
                      Forums_ForumPost.PostSubject, Forums_ForumPost.PostUserID, Forums_ForumPost.PostUserName, Forums_ForumPost.PostUserMail, 
                      Forums_ForumPost.PostText, Forums_ForumPost.PostTime, Forums_ForumPost.PostApprovedByUserID, Forums_ForumPost.PostThreadPosts, 
                      Forums_ForumPost.PostThreadLastPostUserName, Forums_ForumPost.PostThreadLastPostTime, Forums_ForumPost.PostUserSignature, 
                      Forums_ForumPost.PostGUID, Forums_ForumPost.PostLastModified, Forums_ForumPost.PostApproved, Forums_ForumPost.PostIsLocked, 
                      Forums_ForumPost.PostIsAnswer, Forums_ForumPost.PostStickOrder, Forums_ForumPost.PostViews, Forums_ForumPost.PostLastEdit, 
                      Forums_ForumPost.PostInfo, Forums_ForumPost.PostAttachmentCount, Forums_ForumPost.PostType, 
                      Forums_ForumPost.PostThreadPostsAbsolute, Forums_ForumPost.PostThreadLastPostUserNameAbsolute, 
                      Forums_ForumPost.PostThreadLastPostTimeAbsolute, Forums_ForumPost.PostQuestionSolved, Forums_ForumPost.PostIsNotAnswer
FROM         Forums_ForumPost LEFT OUTER JOIN
                      Forums_Forum ON Forums_ForumPost.PostForumID = Forums_Forum.ForumID LEFT OUTER JOIN
                      Forums_ForumGroup ON Forums_Forum.ForumGroupID = Forums_ForumGroup.GroupID
GO
