CREATE TABLE [CMS_Tag] (
		[TagID]          [int] IDENTITY(1, 1) NOT NULL,
		[TagName]        [nvarchar](250) NOT NULL,
		[TagCount]       [int] NOT NULL,
		[TagGroupID]     [int] NOT NULL
) 
ALTER TABLE [CMS_Tag]
	ADD
	CONSTRAINT [PK_CMS_Tag]
	PRIMARY KEY
	NONCLUSTERED
	([TagID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Tag_TagGroupID]
	ON [CMS_Tag] ([TagGroupID])
	
CREATE CLUSTERED INDEX [IX_CMS_Tag_TagName]
	ON [CMS_Tag] ([TagName])
	
	
ALTER TABLE [CMS_Tag]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Tag_TagGroupID_CMS_TagGroup]
	FOREIGN KEY ([TagGroupID]) REFERENCES [CMS_TagGroup] ([TagGroupID])
ALTER TABLE [CMS_Tag]
	CHECK CONSTRAINT [FK_CMS_Tag_TagGroupID_CMS_TagGroup]
