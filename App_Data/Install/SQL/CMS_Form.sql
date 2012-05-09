CREATE TABLE [CMS_Form] (
		[FormID]                            [int] IDENTITY(1, 1) NOT NULL,
		[FormDisplayName]                   [nvarchar](100) NOT NULL,
		[FormName]                          [nvarchar](100) NOT NULL,
		[FormSendToEmail]                   [nvarchar](400) NULL,
		[FormSendFromEmail]                 [nvarchar](400) NULL,
		[FormEmailSubject]                  [nvarchar](250) NULL,
		[FormEmailTemplate]                 [nvarchar](max) NULL,
		[FormEmailAttachUploadedDocs]       [bit] NULL,
		[FormClassID]                       [int] NOT NULL,
		[FormItems]                         [int] NOT NULL,
		[FormReportFields]                  [nvarchar](max) NULL,
		[FormRedirectToUrl]                 [nvarchar](400) NULL,
		[FormDisplayText]                   [nvarchar](max) NULL,
		[FormClearAfterSave]                [bit] NOT NULL,
		[FormSubmitButtonText]              [nvarchar](400) NULL,
		[FormSiteID]                        [int] NOT NULL,
		[FormConfirmationEmailField]        [nvarchar](100) NULL,
		[FormConfirmationTemplate]          [nvarchar](max) NULL,
		[FormConfirmationSendFromEmail]     [nvarchar](400) NULL,
		[FormConfirmationEmailSubject]      [nvarchar](250) NULL,
		[FormAccess]                        [int] NULL,
		[FormSubmitButtonImage]             [nvarchar](255) NULL,
		[FormGUID]                          [uniqueidentifier] NOT NULL,
		[FormLastModified]                  [datetime] NOT NULL,
		[FormLogActivity]                   [bit] NULL
)  
ALTER TABLE [CMS_Form]
	ADD
	CONSTRAINT [PK_CMS_Form]
	PRIMARY KEY
	NONCLUSTERED
	([FormID])
	
	
ALTER TABLE [CMS_Form]
	ADD
	CONSTRAINT [DF__CMS_Form__FormCl__2645B050]
	DEFAULT ((0)) FOR [FormClearAfterSave]
ALTER TABLE [CMS_Form]
	ADD
	CONSTRAINT [DF__CMS_Form__FormIt__2739D489]
	DEFAULT ((0)) FOR [FormItems]
ALTER TABLE [CMS_Form]
	ADD
	CONSTRAINT [DF_CMS_Form_FormEmailAttachUploadedDocs]
	DEFAULT ((1)) FOR [FormEmailAttachUploadedDocs]
CREATE NONCLUSTERED INDEX [IX_CMS_Form_FormClassID]
	ON [CMS_Form] ([FormClassID])
	
CREATE CLUSTERED INDEX [IX_CMS_Form_FormDisplayName]
	ON [CMS_Form] ([FormDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Form_FormSiteID]
	ON [CMS_Form] ([FormSiteID])
	
ALTER TABLE [CMS_Form]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Form_FormClassID_CMS_Class]
	FOREIGN KEY ([FormClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_Form]
	CHECK CONSTRAINT [FK_CMS_Form_FormClassID_CMS_Class]
ALTER TABLE [CMS_Form]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Form_FormSiteID_CMS_Site]
	FOREIGN KEY ([FormSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_Form]
	CHECK CONSTRAINT [FK_CMS_Form_FormSiteID_CMS_Site]
