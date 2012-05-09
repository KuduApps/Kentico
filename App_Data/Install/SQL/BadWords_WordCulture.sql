CREATE TABLE [BadWords_WordCulture] (
		[WordID]        [int] NOT NULL,
		[CultureID]     [int] NOT NULL
) 
ALTER TABLE [BadWords_WordCulture]
	ADD
	CONSTRAINT [PK_BadWords_WordCulture]
	PRIMARY KEY
	CLUSTERED
	([WordID], [CultureID])
	
	
ALTER TABLE [BadWords_WordCulture]
	WITH CHECK
	ADD CONSTRAINT [FK_BadWords_WordCulture_CultureID_CMS_Culture]
	FOREIGN KEY ([CultureID]) REFERENCES [CMS_Culture] ([CultureID])
ALTER TABLE [BadWords_WordCulture]
	CHECK CONSTRAINT [FK_BadWords_WordCulture_CultureID_CMS_Culture]
ALTER TABLE [BadWords_WordCulture]
	WITH CHECK
	ADD CONSTRAINT [FK_BadWords_WordCulture_WordID_BadWords_Word]
	FOREIGN KEY ([WordID]) REFERENCES [BadWords_Word] ([WordID])
ALTER TABLE [BadWords_WordCulture]
	CHECK CONSTRAINT [FK_BadWords_WordCulture_WordID_BadWords_Word]
