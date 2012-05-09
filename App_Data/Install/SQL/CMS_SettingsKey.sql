CREATE TABLE [CMS_SettingsKey] (
		[KeyID]                     [int] IDENTITY(1, 1) NOT NULL,
		[KeyName]                   [nvarchar](100) NOT NULL,
		[KeyDisplayName]            [nvarchar](200) NOT NULL,
		[KeyDescription]            [nvarchar](max) NULL,
		[KeyValue]                  [nvarchar](max) NULL,
		[KeyType]                   [nvarchar](50) NOT NULL,
		[KeyCategoryID]             [int] NULL,
		[SiteID]                    [int] NULL,
		[KeyGUID]                   [uniqueidentifier] NOT NULL,
		[KeyLastModified]           [datetime] NOT NULL,
		[KeyOrder]                  [int] NULL,
		[KeyDefaultValue]           [nvarchar](max) NULL,
		[KeyValidation]             [nvarchar](255) NULL,
		[KeyEditingControlPath]     [nvarchar](200) NULL,
		[KeyLoadGeneration]         [int] NOT NULL,
		[KeyIsGlobal]               [bit] NULL,
		[KeyIsCustom]               [bit] NULL,
		[KeyIsHidden]               [bit] NULL
)  
ALTER TABLE [CMS_SettingsKey]
	ADD
	CONSTRAINT [PK_CMS_SettingsKey]
	PRIMARY KEY
	NONCLUSTERED
	([KeyID])
	
	
ALTER TABLE [CMS_SettingsKey]
	ADD
	CONSTRAINT [DEFAULT_CMS_SettingsKey_KeyIsCustom]
	DEFAULT ((0)) FOR [KeyIsCustom]
ALTER TABLE [CMS_SettingsKey]
	ADD
	CONSTRAINT [DEFAULT_CMS_SettingsKey_KeyIsGlobal]
	DEFAULT ((0)) FOR [KeyIsGlobal]
ALTER TABLE [CMS_SettingsKey]
	ADD
	CONSTRAINT [DEFAULT_CMS_SettingsKey_KeyIsHidden]
	DEFAULT ((0)) FOR [KeyIsHidden]
ALTER TABLE [CMS_SettingsKey]
	ADD
	CONSTRAINT [DEFAULT_CMS_SettingsKey_KeyLoadGeneration]
	DEFAULT ((0)) FOR [KeyLoadGeneration]
CREATE NONCLUSTERED INDEX [IX_CMS_SettingsKey_KeyCategoryID]
	ON [CMS_SettingsKey] ([KeyCategoryID])
	
CREATE CLUSTERED INDEX [IX_CMS_SettingsKey_KeyLoadGeneration_SiteID]
	ON [CMS_SettingsKey] ([KeyLoadGeneration], [SiteID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_SettingsKey_SiteID_KeyName]
	ON [CMS_SettingsKey] ([SiteID], [KeyName])
	
	
ALTER TABLE [CMS_SettingsKey]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_SettingsKey_KeyCategoryID_CMS_SettingsCategory]
	FOREIGN KEY ([KeyCategoryID]) REFERENCES [CMS_SettingsCategory] ([CategoryID])
ALTER TABLE [CMS_SettingsKey]
	CHECK CONSTRAINT [FK_CMS_SettingsKey_KeyCategoryID_CMS_SettingsCategory]
ALTER TABLE [CMS_SettingsKey]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_SettingsKey_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_SettingsKey]
	CHECK CONSTRAINT [FK_CMS_SettingsKey_SiteID_CMS_Site]
