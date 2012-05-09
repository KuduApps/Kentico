CREATE TABLE [COM_Department] (
		[DepartmentID]                    [int] IDENTITY(1, 1) NOT NULL,
		[DepartmentName]                  [nvarchar](200) NOT NULL,
		[DepartmentDisplayName]           [nvarchar](200) NOT NULL,
		[DepartmentDefaultTaxClassID]     [int] NULL,
		[DepartmentGUID]                  [uniqueidentifier] NOT NULL,
		[DepartmentLastModified]          [datetime] NOT NULL,
		[DepartmentSiteID]                [int] NULL
) 
ALTER TABLE [COM_Department]
	ADD
	CONSTRAINT [PK_COM_Department]
	PRIMARY KEY
	NONCLUSTERED
	([DepartmentID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_Department_DepartmentDefaultTaxClassID]
	ON [COM_Department] ([DepartmentDefaultTaxClassID])
	
CREATE CLUSTERED INDEX [IX_COM_Department_DepartmentDisplayName]
	ON [COM_Department] ([DepartmentDisplayName])
	
	
ALTER TABLE [COM_Department]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Department_DepartmentDefaultTaxClassID_COM_TaxClass]
	FOREIGN KEY ([DepartmentDefaultTaxClassID]) REFERENCES [COM_TaxClass] ([TaxClassID])
ALTER TABLE [COM_Department]
	CHECK CONSTRAINT [FK_COM_Department_DepartmentDefaultTaxClassID_COM_TaxClass]
ALTER TABLE [COM_Department]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Department_DepartmentSiteID_CMS_Site]
	FOREIGN KEY ([DepartmentSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_Department]
	CHECK CONSTRAINT [FK_COM_Department_DepartmentSiteID_CMS_Site]
