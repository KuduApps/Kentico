CREATE TABLE [OM_ABVariant] (
		[ABVariantID]               [int] IDENTITY(1, 1) NOT NULL,
		[ABVariantDisplayName]      [nvarchar](110) NOT NULL,
		[ABVariantName]             [nvarchar](50) NOT NULL,
		[ABVariantTestID]           [int] NOT NULL,
		[ABVariantPath]             [nvarchar](450) NOT NULL,
		[ABVariantViews]            [int] NULL,
		[ABVariantConversions]      [int] NOT NULL,
		[ABVariantGUID]             [uniqueidentifier] NOT NULL,
		[ABVariantLastModified]     [datetime] NOT NULL,
		[ABVariantSiteID]           [int] NOT NULL
) 
ALTER TABLE [OM_ABVariant]
	ADD
	CONSTRAINT [PK_OM_ABVariant]
	PRIMARY KEY
	CLUSTERED
	([ABVariantID])
	
ALTER TABLE [OM_ABVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_ABVariant_ABVariantConversions]
	DEFAULT ((0)) FOR [ABVariantConversions]
ALTER TABLE [OM_ABVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_ABVariant_ABVariantDisplayName]
	DEFAULT ('') FOR [ABVariantDisplayName]
ALTER TABLE [OM_ABVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_ABVariant_ABVariantName]
	DEFAULT ('') FOR [ABVariantName]
ALTER TABLE [OM_ABVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_ABVariant_ABVariantSiteID]
	DEFAULT ((0)) FOR [ABVariantSiteID]
CREATE NONCLUSTERED INDEX [IX_OM_ABVariant_ABVariantSiteID]
	ON [OM_ABVariant] ([ABVariantSiteID])
	
CREATE NONCLUSTERED INDEX [IX_OM_ABVariant_ABVariantTestID]
	ON [OM_ABVariant] ([ABVariantTestID])
	
ALTER TABLE [OM_ABVariant]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ABVariant_ABVariantTestID_OM_ABTest]
	FOREIGN KEY ([ABVariantTestID]) REFERENCES [OM_ABTest] ([ABTestID])
ALTER TABLE [OM_ABVariant]
	CHECK CONSTRAINT [FK_OM_ABVariant_ABVariantTestID_OM_ABTest]
ALTER TABLE [OM_ABVariant]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ABVariant_CMS_Site]
	FOREIGN KEY ([ABVariantSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_ABVariant]
	CHECK CONSTRAINT [FK_OM_ABVariant_CMS_Site]
