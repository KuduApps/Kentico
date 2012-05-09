CREATE TABLE [CMS_ResourceTranslation] (
		[TranslationID]              [int] IDENTITY(1, 1) NOT NULL,
		[TranslationStringID]        [int] NOT NULL,
		[TranslationUICultureID]     [int] NOT NULL,
		[TranslationText]            [nvarchar](max) NULL
)  
ALTER TABLE [CMS_ResourceTranslation]
	ADD
	CONSTRAINT [PK_CMS_ResourceTranslation]
	PRIMARY KEY
	CLUSTERED
	([TranslationID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_ResourceTranslation_TranslationStringID]
	ON [CMS_ResourceTranslation] ([TranslationStringID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_ResourceTranslation_TranslationUICultureID]
	ON [CMS_ResourceTranslation] ([TranslationUICultureID])
	
ALTER TABLE [CMS_ResourceTranslation]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ResourceTranslation_TranslationStringID_CMS_ResourceString]
	FOREIGN KEY ([TranslationStringID]) REFERENCES [CMS_ResourceString] ([StringID])
ALTER TABLE [CMS_ResourceTranslation]
	CHECK CONSTRAINT [FK_CMS_ResourceTranslation_TranslationStringID_CMS_ResourceString]
ALTER TABLE [CMS_ResourceTranslation]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ResourceTranslation_TranslationUICultureID_CMS_UICulture]
	FOREIGN KEY ([TranslationUICultureID]) REFERENCES [CMS_UICulture] ([UICultureID])
ALTER TABLE [CMS_ResourceTranslation]
	CHECK CONSTRAINT [FK_CMS_ResourceTranslation_TranslationUICultureID_CMS_UICulture]
