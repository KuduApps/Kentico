CREATE TABLE [CMS_DocumentTag] (
		[DocumentID]     [int] NOT NULL,
		[TagID]          [int] NOT NULL
) 
ALTER TABLE [CMS_DocumentTag]
	ADD
	CONSTRAINT [PK_CMS_DocumentTag]
	PRIMARY KEY
	CLUSTERED
	([DocumentID], [TagID])
	
	
ALTER TABLE [CMS_DocumentTag]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_DocumentTag_DocumentID_CMS_Document]
	FOREIGN KEY ([DocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [CMS_DocumentTag]
	CHECK CONSTRAINT [FK_CMS_DocumentTag_DocumentID_CMS_Document]
ALTER TABLE [CMS_DocumentTag]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_DocumentTag_TagID_CMS_Tag]
	FOREIGN KEY ([TagID]) REFERENCES [CMS_Tag] ([TagID])
ALTER TABLE [CMS_DocumentTag]
	CHECK CONSTRAINT [FK_CMS_DocumentTag_TagID_CMS_Tag]
