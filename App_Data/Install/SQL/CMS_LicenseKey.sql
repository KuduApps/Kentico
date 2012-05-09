CREATE TABLE [CMS_LicenseKey] (
		[LicenseKeyID]          [int] IDENTITY(1, 1) NOT NULL,
		[LicenseDomain]         [nvarchar](200) NOT NULL,
		[LicenseKey]            [nvarchar](max) NOT NULL,
		[LicenseEdition]        [nchar](1) NULL,
		[LicenseExpiration]     [nvarchar](50) NULL,
		[LicensePackages]       [nvarchar](100) NULL,
		[LicenseServers]        [int] NULL
)  
ALTER TABLE [CMS_LicenseKey]
	ADD
	CONSTRAINT [PK_CMS_LicenseKey]
	PRIMARY KEY
	NONCLUSTERED
	([LicenseKeyID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_LicenseKey_LicenseDomain]
	ON [CMS_LicenseKey] ([LicenseDomain])
	
	
