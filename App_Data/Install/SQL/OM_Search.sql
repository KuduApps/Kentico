CREATE TABLE [OM_Search] (
		[SearchID]             [int] IDENTITY(1, 1) NOT NULL,
		[SearchActivityID]     [int] NOT NULL,
		[SearchProvider]       [nvarchar](250) NULL,
		[SearchKeywords]       [nvarchar](max) NOT NULL
)  
ALTER TABLE [OM_Search]
	ADD
	CONSTRAINT [PK_OM_Search]
	PRIMARY KEY
	CLUSTERED
	([SearchID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Search_SearchActivityID]
	ON [OM_Search] ([SearchActivityID])
	
ALTER TABLE [OM_Search]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Search_OM_Activity]
	FOREIGN KEY ([SearchActivityID]) REFERENCES [OM_Activity] ([ActivityID])
ALTER TABLE [OM_Search]
	CHECK CONSTRAINT [FK_OM_Search_OM_Activity]
