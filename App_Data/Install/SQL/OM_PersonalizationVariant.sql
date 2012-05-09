CREATE TABLE [OM_PersonalizationVariant] (
		[VariantID]                   [int] IDENTITY(1, 1) NOT NULL,
		[VariantEnabled]              [bit] NOT NULL,
		[VariantName]                 [nvarchar](200) NOT NULL,
		[VariantDisplayName]          [nvarchar](200) NOT NULL,
		[VariantInstanceGUID]         [uniqueidentifier] NULL,
		[VariantZoneID]               [nvarchar](200) NULL,
		[VariantPageTemplateID]       [int] NOT NULL,
		[VariantWebParts]             [nvarchar](max) NOT NULL,
		[VariantPosition]             [int] NULL,
		[VariantGUID]                 [uniqueidentifier] NOT NULL,
		[VariantLastModified]         [datetime] NOT NULL,
		[VariantDescription]          [nvarchar](max) NULL,
		[VariantDocumentID]           [int] NULL,
		[VariantDisplayCondition]     [nvarchar](max) NOT NULL
)  
ALTER TABLE [OM_PersonalizationVariant]
	ADD
	CONSTRAINT [PK_OM_PersonalizationVariant]
	PRIMARY KEY
	CLUSTERED
	([VariantID])
	
ALTER TABLE [OM_PersonalizationVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_PersonalizationVariant_VariantDisplayCondition]
	DEFAULT ('') FOR [VariantDisplayCondition]
ALTER TABLE [OM_PersonalizationVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_PersonalizationVariant_VariantDisplayName]
	DEFAULT ('') FOR [VariantDisplayName]
ALTER TABLE [OM_PersonalizationVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_PersonalizationVariant_VariantEnabled]
	DEFAULT ((1)) FOR [VariantEnabled]
ALTER TABLE [OM_PersonalizationVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_PersonalizationVariant_VariantName]
	DEFAULT ('') FOR [VariantName]
ALTER TABLE [OM_PersonalizationVariant]
	ADD
	CONSTRAINT [DEFAULT_OM_PersonalizationVariant_VariantPageTemplateID]
	DEFAULT ((0)) FOR [VariantPageTemplateID]
