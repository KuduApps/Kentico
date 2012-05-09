CREATE TABLE [OM_MVTCombination] (
		[MVTCombinationID]                 [int] IDENTITY(1, 1) NOT NULL,
		[MVTCombinationName]               [nvarchar](200) NOT NULL,
		[MVTCombinationCustomName]         [nvarchar](200) NULL,
		[MVTCombinationPageTemplateID]     [int] NOT NULL,
		[MVTCombinationEnabled]            [bit] NOT NULL,
		[MVTCombinationGUID]               [uniqueidentifier] NOT NULL,
		[MVTCombinationLastModified]       [datetime] NOT NULL,
		[MVTCombinationIsDefault]          [bit] NULL,
		[MVTCombinationConversions]        [int] NULL,
		[MVTCombinationDocumentID]         [int] NULL
) 
ALTER TABLE [OM_MVTCombination]
	ADD
	CONSTRAINT [PK_OM_MVTCombination]
	PRIMARY KEY
	CLUSTERED
	([MVTCombinationID])
	
ALTER TABLE [OM_MVTCombination]
	ADD
	CONSTRAINT [DEFAULT_OM_MVTCombination_MVTCombinationIsDefault]
	DEFAULT ((0)) FOR [MVTCombinationIsDefault]
CREATE NONCLUSTERED INDEX [IX_OM_MVTCombination_MVTCombinationPageTemplateID]
	ON [OM_MVTCombination] ([MVTCombinationPageTemplateID])
	
ALTER TABLE [OM_MVTCombination]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_MVTCombination_MVTCombinationPageTemplateID_CMS_PageTemplate]
	FOREIGN KEY ([MVTCombinationPageTemplateID]) REFERENCES [CMS_PageTemplate] ([PageTemplateID])
ALTER TABLE [OM_MVTCombination]
	CHECK CONSTRAINT [FK_OM_MVTCombination_MVTCombinationPageTemplateID_CMS_PageTemplate]
