CREATE TABLE [CMS_VersionAttachment] (
		[VersionHistoryID]        [int] NOT NULL,
		[AttachmentHistoryID]     [int] NOT NULL
) 
ALTER TABLE [CMS_VersionAttachment]
	ADD
	CONSTRAINT [PK_CMS_VersionAttachment]
	PRIMARY KEY
	CLUSTERED
	([VersionHistoryID], [AttachmentHistoryID])
	
	
ALTER TABLE [CMS_VersionAttachment]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionAttachment_AttachmentHistoryID_CMS_AttachmentHistory]
	FOREIGN KEY ([AttachmentHistoryID]) REFERENCES [CMS_AttachmentHistory] ([AttachmentHistoryID])
ALTER TABLE [CMS_VersionAttachment]
	CHECK CONSTRAINT [FK_CMS_VersionAttachment_AttachmentHistoryID_CMS_AttachmentHistory]
ALTER TABLE [CMS_VersionAttachment]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionAttachment_VersionHistoryID_CMS_VersionHistory]
	FOREIGN KEY ([VersionHistoryID]) REFERENCES [CMS_VersionHistory] ([VersionHistoryID])
ALTER TABLE [CMS_VersionAttachment]
	CHECK CONSTRAINT [FK_CMS_VersionAttachment_VersionHistoryID_CMS_VersionHistory]
