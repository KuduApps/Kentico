CREATE TABLE [CMS_BannedIP] (
		[IPAddressID]                [int] IDENTITY(1, 1) NOT NULL,
		[IPAddress]                  [nvarchar](100) NOT NULL,
		[IPAddressRegular]           [nvarchar](200) NOT NULL,
		[IPAddressAllowed]           [bit] NOT NULL,
		[IPAddressAllowOverride]     [bit] NOT NULL,
		[IPAddressBanReason]         [nvarchar](450) NULL,
		[IPAddressBanType]           [nvarchar](100) NOT NULL,
		[IPAddressBanEnabled]        [bit] NULL,
		[IPAddressSiteID]            [int] NULL,
		[IPAddressGUID]              [uniqueidentifier] NOT NULL,
		[IPAddressLastModified]      [datetime] NOT NULL
) 
ALTER TABLE [CMS_BannedIP]
	ADD
	CONSTRAINT [PK_CMS_BannedIP]
	PRIMARY KEY
	NONCLUSTERED
	([IPAddressID])
	
	
ALTER TABLE [CMS_BannedIP]
	ADD
	CONSTRAINT [DEFAULT_CMS_BannedIP_IPAddressAllowed]
	DEFAULT ((0)) FOR [IPAddressAllowed]
ALTER TABLE [CMS_BannedIP]
	ADD
	CONSTRAINT [DEFAULT_CMS_BannedIP_IPAddressAllowOverride]
	DEFAULT ((0)) FOR [IPAddressAllowOverride]
ALTER TABLE [CMS_BannedIP]
	ADD
	CONSTRAINT [DEFAULT_CMS_BannedIP_IPAddressBanEnabled]
	DEFAULT ((0)) FOR [IPAddressBanEnabled]
CREATE CLUSTERED INDEX [IX_CMS_BannedIP_IPAddressSiteID_IPAddress]
	ON [CMS_BannedIP] ([IPAddress], [IPAddressSiteID])
	
	
ALTER TABLE [CMS_BannedIP]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_BannedIP_IPAddressSiteID_CMS_Site]
	FOREIGN KEY ([IPAddressSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_BannedIP]
	CHECK CONSTRAINT [FK_CMS_BannedIP_IPAddressSiteID_CMS_Site]
