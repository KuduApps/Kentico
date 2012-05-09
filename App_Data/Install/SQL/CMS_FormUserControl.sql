CREATE TABLE [CMS_FormUserControl] (
		[UserControlID]                      [int] IDENTITY(1, 1) NOT NULL,
		[UserControlDisplayName]             [nvarchar](200) NOT NULL,
		[UserControlCodeName]                [nvarchar](200) NOT NULL,
		[UserControlFileName]                [nvarchar](400) NOT NULL,
		[UserControlForText]                 [bit] NOT NULL,
		[UserControlForLongText]             [bit] NOT NULL,
		[UserControlForInteger]              [bit] NOT NULL,
		[UserControlForDecimal]              [bit] NOT NULL,
		[UserControlForDateTime]             [bit] NOT NULL,
		[UserControlForBoolean]              [bit] NOT NULL,
		[UserControlForFile]                 [bit] NOT NULL,
		[UserControlShowInBizForms]          [bit] NOT NULL,
		[UserControlDefaultDataType]         [nvarchar](50) NOT NULL,
		[UserControlDefaultDataTypeSize]     [int] NULL,
		[UserControlShowInDocumentTypes]     [bit] NULL,
		[UserControlShowInSystemTables]      [bit] NULL,
		[UserControlShowInWebParts]          [bit] NULL,
		[UserControlShowInReports]           [bit] NULL,
		[UserControlGUID]                    [uniqueidentifier] NOT NULL,
		[UserControlLastModified]            [datetime] NOT NULL,
		[UserControlForGuid]                 [bit] NOT NULL,
		[UserControlShowInCustomTables]      [bit] NULL,
		[UserControlForVisibility]           [bit] NOT NULL,
		[UserControlParameters]              [nvarchar](max) NULL,
		[UserControlForDocAttachments]       [bit] NULL,
		[UserControlForLongInteger]          [bit] NULL,
		[UserControlResourceID]              [int] NULL,
		[UserControlType]                    [int] NULL
)  
ALTER TABLE [CMS_FormUserControl]
	ADD
	CONSTRAINT [PK_CMS_FormUserControl]
	PRIMARY KEY
	NONCLUSTERED
	([UserControlID])
	
	
ALTER TABLE [CMS_FormUserControl]
	ADD
	CONSTRAINT [DEFAULT_CMS_FormUserControl_UserControlForDocAttachments]
	DEFAULT ((0)) FOR [UserControlForDocAttachments]
ALTER TABLE [CMS_FormUserControl]
	ADD
	CONSTRAINT [DEFAULT_CMS_FormUserControl_UserControlForGuid]
	DEFAULT ((0)) FOR [UserControlForGuid]
ALTER TABLE [CMS_FormUserControl]
	ADD
	CONSTRAINT [DEFAULT_CMS_FormUserControl_UserControlForLongInteger]
	DEFAULT ((0)) FOR [UserControlForLongInteger]
ALTER TABLE [CMS_FormUserControl]
	ADD
	CONSTRAINT [DEFAULT_CMS_FormUserControl_UserControlForVisibility]
	DEFAULT ((0)) FOR [UserControlForVisibility]
ALTER TABLE [CMS_FormUserControl]
	ADD
	CONSTRAINT [DEFAULT_cms_FormUserControl_UserControlShowInCustomTables]
	DEFAULT ((0)) FOR [UserControlShowInCustomTables]
CREATE CLUSTERED INDEX [IX_CMS_FormUserControl_UserControlDisplayName]
	ON [CMS_FormUserControl] ([UserControlDisplayName])
	
	
ALTER TABLE [CMS_FormUserControl]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_FormUserControl_UserControlResourceID_CMS_Resource]
	FOREIGN KEY ([UserControlResourceID]) REFERENCES [CMS_Resource] ([ResourceID])
ALTER TABLE [CMS_FormUserControl]
	CHECK CONSTRAINT [FK_CMS_FormUserControl_UserControlResourceID_CMS_Resource]
