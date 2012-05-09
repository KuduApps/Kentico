CREATE TABLE [COM_Address] (
		[AddressID]               [int] IDENTITY(1, 1) NOT NULL,
		[AddressName]             [nvarchar](200) NOT NULL,
		[AddressLine1]            [nvarchar](100) NOT NULL,
		[AddressLine2]            [nvarchar](100) NOT NULL,
		[AddressCity]             [nvarchar](100) NOT NULL,
		[AddressZip]              [nvarchar](20) NOT NULL,
		[AddressPhone]            [nvarchar](100) NULL,
		[AddressCustomerID]       [int] NOT NULL,
		[AddressCountryID]        [int] NOT NULL,
		[AddressStateID]          [int] NULL,
		[AddressIsBilling]        [bit] NOT NULL,
		[AddressEnabled]          [bit] NOT NULL,
		[AddressPersonalName]     [nvarchar](200) NOT NULL,
		[AddressIsShipping]       [bit] NOT NULL,
		[AddressIsCompany]        [bit] NULL,
		[AddressGUID]             [uniqueidentifier] NULL,
		[AddressLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [COM_Address]
	ADD
	CONSTRAINT [PK_COM_CustomerAdress]
	PRIMARY KEY
	CLUSTERED
	([AddressID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_Address_AddressCountryID]
	ON [COM_Address] ([AddressCountryID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Address_AddressCustomerID]
	ON [COM_Address] ([AddressCustomerID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Address_AddressEnabled_AddressIsBilling_AddressIsShipping_AddressIsCompany]
	ON [COM_Address] ([AddressEnabled], [AddressIsBilling], [AddressIsShipping], [AddressIsCompany])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_Address_AddressStateID]
	ON [COM_Address] ([AddressStateID])
	
ALTER TABLE [COM_Address]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Address_AddressCountryID_CMS_Country]
	FOREIGN KEY ([AddressCountryID]) REFERENCES [CMS_Country] ([CountryID])
ALTER TABLE [COM_Address]
	CHECK CONSTRAINT [FK_COM_Address_AddressCountryID_CMS_Country]
ALTER TABLE [COM_Address]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Address_AddressCustomerID_COM_Customer]
	FOREIGN KEY ([AddressCustomerID]) REFERENCES [COM_Customer] ([CustomerID])
ALTER TABLE [COM_Address]
	CHECK CONSTRAINT [FK_COM_Address_AddressCustomerID_COM_Customer]
ALTER TABLE [COM_Address]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Address_AddressStateID_CMS_State]
	FOREIGN KEY ([AddressStateID]) REFERENCES [CMS_State] ([StateID])
ALTER TABLE [COM_Address]
	CHECK CONSTRAINT [FK_COM_Address_AddressStateID_CMS_State]
