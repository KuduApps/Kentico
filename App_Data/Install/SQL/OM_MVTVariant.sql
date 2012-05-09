CREATE TABLE [OM_MVTVariant] (
		[MVTVariantID]                 [int] IDENTITY(1, 1) NOT NULL,
		[MVTVariantName]               [nvarchar](100) NOT NULL,
		[MVTVariantDisplayName]        [nvarchar](200) NOT NULL,
		[MVTVariantInstanceGUID]       [uniqueidentifier] NULL,
		[MVTVariantZoneID]             [nvarchar](200) NULL,
		[MVTVariantPageTemplateID]     [int] NOT NULL,
		[MVTVariantEnabled]            [bit] NOT NULL,
		[MVTVariantWebParts]           [nvarchar](max) NULL,
		[MVTVariantGUID]               [uniqueidentifier] NOT NULL,
		[MVTVariantLastModified]       [datetime] NOT NULL,
		[MVTVariantDescription]        [nvarchar](max) NULL,
		[MVTVariantDocumentID]         [int] NULL
)  
ALTER TABLE [OM_MVTVariant]
	ADD
	CONSTRAINT [PK_OM_MVTVariant]
	PRIMARY KEY
	CLUSTERED
	([MVTVariantID])
	
ALTER TABLE [OM_MVTVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_MVTVariant_MVTVariantEnabled]
	DEFAULT ((1)) FOR [MVTVariantEnabled]
ALTER TABLE [OM_MVTVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_MVTVariant_MVTVariantName]
	DEFAULT ('') FOR [MVTVariantName]
CREATE NONCLUSTERED INDEX [IX_OM_MVTVariant_MVTVariantPageTemplateID]
	ON [OM_MVTVariant] ([MVTVariantPageTemplateID])
	
ALTER TABLE [OM_MVTVariant]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_MVTVariant_MVTVariantPageTemplateID_CMS_PageTemplate]
	FOREIGN KEY ([MVTVariantPageTemplateID]) REFERENCES [CMS_PageTemplate] ([PageTemplateID])
ALTER TABLE [OM_MVTVariant]
	CHECK CONSTRAINT [FK_OM_MVTVariant_MVTVariantPageTemplateID_CMS_PageTemplate]
