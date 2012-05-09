CREATE TABLE [CMS_Country] (
		[CountryID]               [int] IDENTITY(1, 1) NOT NULL,
		[CountryDisplayName]      [nvarchar](200) NOT NULL,
		[CountryName]             [nvarchar](200) NOT NULL,
		[CountryGUID]             [uniqueidentifier] NOT NULL,
		[CountryLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [CMS_Country]
	ADD
	CONSTRAINT [PK_CMS_Country]
	PRIMARY KEY
	NONCLUSTERED
	([CountryID])
	
	
ALTER TABLE [CMS_Country]
	ADD
	CONSTRAINT [DEFAULT_cms_country_CountryDisplayName]
	DEFAULT ('') FOR [CountryDisplayName]
ALTER TABLE [CMS_Country]
	ADD
	CONSTRAINT [DEFAULT_cms_country_CountryName]
	DEFAULT ('') FOR [CountryName]
CREATE CLUSTERED INDEX [IX_CMS_Country_CountryDisplayName]
	ON [CMS_Country] ([CountryDisplayName])
	
