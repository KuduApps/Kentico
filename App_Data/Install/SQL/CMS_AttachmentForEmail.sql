CREATE TABLE [CMS_AttachmentForEmail] (
		[EmailID]          [int] NOT NULL,
		[AttachmentID]     [int] NOT NULL
) 
ALTER TABLE [CMS_AttachmentForEmail]
	ADD
	CONSTRAINT [PK_CMS_AttachmentForEmail]
	PRIMARY KEY
	CLUSTERED
	([EmailID], [AttachmentID])
	
	
ALTER TABLE [CMS_AttachmentForEmail]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_AttachmentForEmail_AttachmentID_CMS_EmailAttachment]
	FOREIGN KEY ([AttachmentID]) REFERENCES [CMS_EmailAttachment] ([AttachmentID])
ALTER TABLE [CMS_AttachmentForEmail]
	CHECK CONSTRAINT [FK_CMS_AttachmentForEmail_AttachmentID_CMS_EmailAttachment]
ALTER TABLE [CMS_AttachmentForEmail]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_AttachmentForEmail_EmailID_CMS_Email]
	FOREIGN KEY ([EmailID]) REFERENCES [CMS_Email] ([EmailID])
ALTER TABLE [CMS_AttachmentForEmail]
	CHECK CONSTRAINT [FK_CMS_AttachmentForEmail_EmailID_CMS_Email]
