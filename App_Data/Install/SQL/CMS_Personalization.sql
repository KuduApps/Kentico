CREATE TABLE [CMS_Personalization] (
		[PersonalizationID]                [int] IDENTITY(1, 1) NOT NULL,
		[PersonalizationGUID]              [uniqueidentifier] NOT NULL,
		[PersonalizationLastModified]      [datetime] NOT NULL,
		[PersonalizationUserID]            [int] NULL,
		[PersonalizationDocumentID]        [int] NULL,
		[PersonalizationWebParts]          [nvarchar](max) NULL,
		[PersonalizationDashboardName]     [nvarchar](200) NULL,
		[PersonalizationSiteID]            [int] NULL
)  
ALTER TABLE [CMS_Personalization]
	ADD
	CONSTRAINT [PK_CMS_Personalization]
	PRIMARY KEY
	NONCLUSTERED
	([PersonalizationID])
	
	
ALTER TABLE [CMS_Personalization]
	ADD
	CONSTRAINT [DEFAULT_CMS_Personalization_PersonalizationGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [PersonalizationGUID]
ALTER TABLE [CMS_Personalization]
	ADD
	CONSTRAINT [DEFAULT_CMS_Personalization_PersonalizationLastModified]
	DEFAULT ('9/2/2008 5:36:59 PM') FOR [PersonalizationLastModified]
CREATE UNIQUE NONCLUSTERED INDEX [IX_CMS_Personalization_PersonalizationID_PersonalizationUserID_PersonalizationDocumentID_PersonalizationDashboardName]
	ON [CMS_Personalization] ([PersonalizationID], [PersonalizationUserID], [PersonalizationDocumentID], [PersonalizationDashboardName])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Personalization_PersonalizationSiteID_SiteID]
	ON [CMS_Personalization] ([PersonalizationSiteID])
	
CREATE CLUSTERED INDEX [IX_CMS_Personalization_PersonalizationUserID_PersonalizationDocumentID]
	ON [CMS_Personalization] ([PersonalizationDocumentID])
	
ALTER TABLE [CMS_Personalization]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Personalization_CMS_Personalization]
	FOREIGN KEY ([PersonalizationID]) REFERENCES [CMS_Personalization] ([PersonalizationID])
ALTER TABLE [CMS_Personalization]
	CHECK CONSTRAINT [FK_CMS_Personalization_CMS_Personalization]
ALTER TABLE [CMS_Personalization]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Personalization_PersonalizationDocumentID_CMS_Document]
	FOREIGN KEY ([PersonalizationDocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [CMS_Personalization]
	CHECK CONSTRAINT [FK_CMS_Personalization_PersonalizationDocumentID_CMS_Document]
ALTER TABLE [CMS_Personalization]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Personalization_PersonalizationSiteID_CMS_Site]
	FOREIGN KEY ([PersonalizationSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_Personalization]
	CHECK CONSTRAINT [FK_CMS_Personalization_PersonalizationSiteID_CMS_Site]
ALTER TABLE [CMS_Personalization]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Personalization_PersonalizationUserID_CMS_User]
	FOREIGN KEY ([PersonalizationUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_Personalization]
	CHECK CONSTRAINT [FK_CMS_Personalization_PersonalizationUserID_CMS_User]
