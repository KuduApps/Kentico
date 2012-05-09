CREATE TABLE [COM_Supplier] (
		[SupplierID]               [int] IDENTITY(1, 1) NOT NULL,
		[SupplierDisplayName]      [nvarchar](50) NOT NULL,
		[SupplierPhone]            [nvarchar](50) NOT NULL,
		[SupplierEmail]            [nvarchar](200) NOT NULL,
		[SupplierFax]              [nvarchar](50) NOT NULL,
		[SupplierEnabled]          [bit] NOT NULL,
		[SupplierGUID]             [uniqueidentifier] NOT NULL,
		[SupplierLastModified]     [datetime] NOT NULL,
		[SupplierSiteID]           [int] NULL
) 
ALTER TABLE [COM_Supplier]
	ADD
	CONSTRAINT [PK_COM_Supplier]
	PRIMARY KEY
	NONCLUSTERED
	([SupplierID])
	
	
CREATE CLUSTERED INDEX [IX_COM_Supplier_SupplierDisplayName_SupplierEnabled]
	ON [COM_Supplier] ([SupplierDisplayName], [SupplierEnabled])
	
	
ALTER TABLE [COM_Supplier]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Supplier_SupplierSiteID_CMS_Site]
	FOREIGN KEY ([SupplierSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_Supplier]
	CHECK CONSTRAINT [FK_COM_Supplier_SupplierSiteID_CMS_Site]
