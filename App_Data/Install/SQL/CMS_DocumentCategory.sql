CREATE TABLE [CMS_DocumentCategory] (
		[DocumentID]     [int] NOT NULL,
		[CategoryID]     [int] NOT NULL
) 
ALTER TABLE [CMS_DocumentCategory]
	ADD
	CONSTRAINT [PK_CMS_DocumentCategory]
	PRIMARY KEY
	CLUSTERED
	([DocumentID], [CategoryID])
	
	
ALTER TABLE [CMS_DocumentCategory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_DocumentCategory_CategoryID_CMS_Category]
	FOREIGN KEY ([CategoryID]) REFERENCES [CMS_Category] ([CategoryID])
ALTER TABLE [CMS_DocumentCategory]
	CHECK CONSTRAINT [FK_CMS_DocumentCategory_CategoryID_CMS_Category]
ALTER TABLE [CMS_DocumentCategory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_DocumentCategory_DocumentID_CMS_Document]
	FOREIGN KEY ([DocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [CMS_DocumentCategory]
	CHECK CONSTRAINT [FK_CMS_DocumentCategory_DocumentID_CMS_Document]
