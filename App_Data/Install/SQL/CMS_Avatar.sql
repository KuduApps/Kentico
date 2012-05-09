CREATE TABLE [CMS_Avatar] (
		[AvatarID]                    [int] IDENTITY(1, 1) NOT NULL,
		[AvatarName]                  [nvarchar](200) NULL,
		[AvatarFileName]              [nvarchar](200) NOT NULL,
		[AvatarFileExtension]         [nvarchar](10) NOT NULL,
		[AvatarBinary]                [varbinary](max) NULL,
		[AvatarType]                  [nvarchar](50) NOT NULL,
		[AvatarIsCustom]              [bit] NOT NULL,
		[AvatarGUID]                  [uniqueidentifier] NOT NULL,
		[AvatarLastModified]          [datetime] NOT NULL,
		[AvatarMimeType]              [nvarchar](100) NOT NULL,
		[AvatarFileSize]              [int] NOT NULL,
		[AvatarImageHeight]           [int] NULL,
		[AvatarImageWidth]            [int] NULL,
		[DefaultMaleUserAvatar]       [bit] NULL,
		[DefaultFemaleUserAvatar]     [bit] NULL,
		[DefaultGroupAvatar]          [bit] NULL,
		[DefaultUserAvatar]           [bit] NULL
)  
ALTER TABLE [CMS_Avatar]
	ADD
	CONSTRAINT [PK_CMS_Avatar]
	PRIMARY KEY
	NONCLUSTERED
	([AvatarID])
	
	
ALTER TABLE [CMS_Avatar]
	ADD
	CONSTRAINT [DEFAULT_CMS_Avatar_DefaultFemaleUserAvatar]
	DEFAULT ((0)) FOR [DefaultFemaleUserAvatar]
ALTER TABLE [CMS_Avatar]
	ADD
	CONSTRAINT [DEFAULT_CMS_Avatar_DefaultGroupAvatar]
	DEFAULT ((0)) FOR [DefaultGroupAvatar]
ALTER TABLE [CMS_Avatar]
	ADD
	CONSTRAINT [DEFAULT_CMS_Avatar_DefaultMaleUserAvatar]
	DEFAULT ((0)) FOR [DefaultMaleUserAvatar]
ALTER TABLE [CMS_Avatar]
	ADD
	CONSTRAINT [DEFAULT_CMS_Avatar_DefaultUserAvatar]
	DEFAULT ((0)) FOR [DefaultUserAvatar]
CREATE NONCLUSTERED INDEX [IX_CMS_Avatar_AvatarGUID]
	ON [CMS_Avatar] ([AvatarGUID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_Avatar_AvatarName]
	ON [CMS_Avatar] ([AvatarName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Avatar_AvatarType_AvatarIsCustom]
	ON [CMS_Avatar] ([AvatarType], [AvatarIsCustom])
	
	
