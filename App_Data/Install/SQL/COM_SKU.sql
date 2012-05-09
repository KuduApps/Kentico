CREATE TABLE [COM_SKU] (
		[SKUID]                      [int] IDENTITY(1, 1) NOT NULL,
		[SKUNumber]                  [nvarchar](200) NULL,
		[SKUName]                    [nvarchar](440) NOT NULL,
		[SKUDescription]             [nvarchar](max) NULL,
		[SKUPrice]                   [float] NOT NULL,
		[SKUEnabled]                 [bit] NOT NULL,
		[SKUDepartmentID]            [int] NULL,
		[SKUManufacturerID]          [int] NULL,
		[SKUInternalStatusID]        [int] NULL,
		[SKUPublicStatusID]          [int] NULL,
		[SKUSupplierID]              [int] NULL,
		[SKUAvailableInDays]         [int] NULL,
		[SKUGUID]                    [uniqueidentifier] NOT NULL,
		[SKUImagePath]               [nvarchar](450) NULL,
		[SKUWeight]                  [float] NULL,
		[SKUWidth]                   [float] NULL,
		[SKUDepth]                   [float] NULL,
		[SKUHeight]                  [float] NULL,
		[SKUAvailableItems]          [int] NULL,
		[SKUSellOnlyAvailable]       [bit] NULL,
		[SKUCustomData]              [nvarchar](max) NULL,
		[SKUOptionCategoryID]        [int] NULL,
		[SKUOrder]                   [int] NULL,
		[SKULastModified]            [datetime] NOT NULL,
		[SKUCreated]                 [datetime] NULL,
		[SKUSiteID]                  [int] NULL,
		[SKUPrivateDonation]         [bit] NULL,
		[SKUNeedsShipping]           [bit] NULL,
		[SKUMaxDownloads]            [int] NULL,
		[SKUValidUntil]              [datetime] NULL,
		[SKUProductType]             [nvarchar](50) NULL,
		[SKUMaxItemsInOrder]         [int] NULL,
		[SKUMaxPrice]                [float] NULL,
		[SKUValidity]                [nvarchar](50) NULL,
		[SKUValidFor]                [int] NULL,
		[SKUMinPrice]                [float] NULL,
		[SKUMembershipGUID]          [uniqueidentifier] NULL,
		[SKUConversionName]          [nvarchar](100) NULL,
		[SKUConversionValue]         [nvarchar](200) NULL,
		[SKUBundleInventoryType]     [nvarchar](50) NULL
)  
ALTER TABLE [COM_SKU]
	ADD
	CONSTRAINT [PK_COM_SKU]
	PRIMARY KEY
	NONCLUSTERED
	([SKUID])
	
	
ALTER TABLE [COM_SKU]
	ADD
	CONSTRAINT [DEFAULT_COM_SKU_SKUConversionValue]
	DEFAULT ('0') FOR [SKUConversionValue]
ALTER TABLE [COM_SKU]
	ADD
	CONSTRAINT [DEFAULT_COM_SKU_SKUGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [SKUGUID]
ALTER TABLE [COM_SKU]
	ADD
	CONSTRAINT [DEFAULT_COM_SKU_SKUName]
	DEFAULT ('') FOR [SKUName]
ALTER TABLE [COM_SKU]
	ADD
	CONSTRAINT [DEFAULT_COM_SKU_SKUSellOnlyAvailable]
	DEFAULT ((0)) FOR [SKUSellOnlyAvailable]
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUDepartmentID]
	ON [COM_SKU] ([SKUDepartmentID])
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUEnabled_SKUAvailableItems]
	ON [COM_SKU] ([SKUEnabled], [SKUAvailableItems])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUInternalStatusID]
	ON [COM_SKU] ([SKUInternalStatusID])
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUManufacturerID]
	ON [COM_SKU] ([SKUManufacturerID])
	
CREATE CLUSTERED INDEX [IX_COM_SKU_SKUName]
	ON [COM_SKU] ([SKUName])
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUName_SKUEnabled]
	ON [COM_SKU] ([SKUDepartmentID])
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUOptionCategoryID]
	ON [COM_SKU] ([SKUOptionCategoryID])
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUPrice]
	ON [COM_SKU] ([SKUPrice])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUPublicStatusID]
	ON [COM_SKU] ([SKUPublicStatusID])
	
CREATE NONCLUSTERED INDEX [IX_COM_SKU_SKUSupplierID]
	ON [COM_SKU] ([SKUSupplierID])
	
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUDepartmentID_COM_Department]
	FOREIGN KEY ([SKUDepartmentID]) REFERENCES [COM_Department] ([DepartmentID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUDepartmentID_COM_Department]
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUInternalStatusID_COM_InternalStatus]
	FOREIGN KEY ([SKUInternalStatusID]) REFERENCES [COM_InternalStatus] ([InternalStatusID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUInternalStatusID_COM_InternalStatus]
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUManufacturerID_COM_Manifacturer]
	FOREIGN KEY ([SKUManufacturerID]) REFERENCES [COM_Manufacturer] ([ManufacturerID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUManufacturerID_COM_Manifacturer]
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUOptionCategoryID_COM_OptionCategory]
	FOREIGN KEY ([SKUOptionCategoryID]) REFERENCES [COM_OptionCategory] ([CategoryID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUOptionCategoryID_COM_OptionCategory]
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUPublicStatusID_COM_PublicStatus]
	FOREIGN KEY ([SKUPublicStatusID]) REFERENCES [COM_PublicStatus] ([PublicStatusID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUPublicStatusID_COM_PublicStatus]
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUSiteID_CMS_Site]
	FOREIGN KEY ([SKUSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUSiteID_CMS_Site]
ALTER TABLE [COM_SKU]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKU_SKUSupplierID_COM_Supplier]
	FOREIGN KEY ([SKUSupplierID]) REFERENCES [COM_Supplier] ([SupplierID])
ALTER TABLE [COM_SKU]
	CHECK CONSTRAINT [FK_COM_SKU_SKUSupplierID_COM_Supplier]
