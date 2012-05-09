CREATE TABLE [COM_TaxClass] (
		[TaxClassID]                   [int] IDENTITY(1, 1) NOT NULL,
		[TaxClassName]                 [nvarchar](200) NOT NULL,
		[TaxClassDisplayName]          [nvarchar](200) NOT NULL,
		[TaxClassZeroIfIDSupplied]     [bit] NULL,
		[TaxClassGUID]                 [uniqueidentifier] NOT NULL,
		[TaxClassLastModified]         [datetime] NOT NULL,
		[TaxClassSiteID]               [int] NULL
) 
ALTER TABLE [COM_TaxClass]
	ADD
	CONSTRAINT [PK_COM_TaxClass]
	PRIMARY KEY
	NONCLUSTERED
	([TaxClassID])
	
	
CREATE CLUSTERED INDEX [IX_COM_TaxClass_TaxClassDisplayName]
	ON [COM_TaxClass] ([TaxClassDisplayName])
	
	
ALTER TABLE [COM_TaxClass]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxClass_TaxClassSiteID_CMS_Site]
	FOREIGN KEY ([TaxClassSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_TaxClass]
	CHECK CONSTRAINT [FK_COM_TaxClass_TaxClassSiteID_CMS_Site]
