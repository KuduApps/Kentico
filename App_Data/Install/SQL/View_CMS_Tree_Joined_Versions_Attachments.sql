CREATE VIEW [View_CMS_Tree_Joined_Versions_Attachments]
AS
SELECT     View_CMS_Tree_Joined_Versions.*, View_Document_Attachment.AttachmentID, 
                      View_Document_Attachment.AttachmentHistoryID, CMS_Attachment.AttachmentName, CMS_Attachment.AttachmentExtension, 
                      CMS_Attachment.AttachmentSize, CMS_Attachment.AttachmentMimeType, CMS_Attachment.AttachmentImageWidth, 
                      CMS_Attachment.AttachmentImageHeight, CMS_Attachment.AttachmentGUID, CMS_Attachment.AttachmentIsUnsorted, 
                      CMS_Attachment.AttachmentOrder, CMS_Attachment.AttachmentGroupGUID, CMS_Attachment.AttachmentTitle, CMS_Attachment.AttachmentDescription
FROM         View_CMS_Tree_Joined_Versions LEFT OUTER JOIN
                      View_Document_Attachment ON 
                      View_CMS_Tree_Joined_Versions.DocumentID = View_Document_Attachment.AttachmentDocumentID LEFT OUTER JOIN
                      CMS_Attachment ON View_Document_Attachment.AttachmentID = CMS_Attachment.AttachmentID
WHERE     (View_Document_Attachment.AttachmentHistoryID IS NULL)
UNION ALL
SELECT     View_CMS_Tree_Joined_Versions_1.*, View_Document_Attachment_1.AttachmentID, View_Document_Attachment_1.AttachmentHistoryID, 
                      CMS_AttachmentHistory.AttachmentName, CMS_AttachmentHistory.AttachmentExtension, CMS_AttachmentHistory.AttachmentSize, 
                      CMS_AttachmentHistory.AttachmentMimeType, CMS_AttachmentHistory.AttachmentImageWidth, 
                      CMS_AttachmentHistory.AttachmentImageHeight, CMS_AttachmentHistory.AttachmentGUID, 
                      CMS_AttachmentHistory.AttachmentIsUnsorted, CMS_AttachmentHistory.AttachmentOrder, 
                      CMS_AttachmentHistory.AttachmentGroupGUID, CMS_AttachmentHistory.AttachmentTitle, CMS_AttachmentHistory.AttachmentDescription
FROM         View_CMS_Tree_Joined_Versions AS View_CMS_Tree_Joined_Versions_1 LEFT OUTER JOIN
                      View_Document_Attachment AS View_Document_Attachment_1 ON 
                      View_CMS_Tree_Joined_Versions_1.DocumentID = View_Document_Attachment_1.AttachmentDocumentID LEFT OUTER JOIN
                      CMS_AttachmentHistory ON View_Document_Attachment_1.AttachmentHistoryID = CMS_AttachmentHistory.AttachmentHistoryID
WHERE     (View_Document_Attachment_1.AttachmentHistoryID IS NOT NULL)
GO
