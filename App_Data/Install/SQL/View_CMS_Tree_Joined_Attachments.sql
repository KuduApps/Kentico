CREATE VIEW [View_CMS_Tree_Joined_Attachments]
AS
SELECT     View_CMS_Tree_Joined.*, View_Document_Attachment.AttachmentID, 
                      View_Document_Attachment.AttachmentHistoryID, CMS_Attachment.AttachmentName, CMS_Attachment.AttachmentExtension, 
                      CMS_Attachment.AttachmentSize, CMS_Attachment.AttachmentMimeType, CMS_Attachment.AttachmentImageWidth, 
                      CMS_Attachment.AttachmentImageHeight, CMS_Attachment.AttachmentGUID, CMS_Attachment.AttachmentIsUnsorted, 
                      CMS_Attachment.AttachmentOrder, CMS_Attachment.AttachmentGroupGUID, CMS_Attachment.AttachmentTitle, CMS_Attachment.AttachmentDescription
FROM         View_CMS_Tree_Joined LEFT OUTER JOIN
                      View_Document_Attachment ON 
                      View_CMS_Tree_Joined.DocumentID = View_Document_Attachment.AttachmentDocumentID LEFT OUTER JOIN
                      CMS_Attachment ON View_Document_Attachment.AttachmentID = CMS_Attachment.AttachmentID
GO
