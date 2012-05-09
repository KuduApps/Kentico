CREATE TABLE [Analytics_Conversion] (
		[ConversionID]               [int] IDENTITY(1, 1) NOT NULL,
		[ConversionName]             [nvarchar](200) NOT NULL,
		[ConversionDisplayName]      [nvarchar](200) NOT NULL,
		[ConversionDescription]      [nvarchar](max) NULL,
		[ConversionGUID]             [uniqueidentifier] NOT NULL,
		[ConversionLastModified]     [datetime] NOT NULL,
		[ConversionSiteID]           [int] NOT NULL
)  
ALTER TABLE [Analytics_Conversion]
	ADD
	CONSTRAINT [PK_CMS_Conversion]
	PRIMARY KEY
	CLUSTERED
	([ConversionID])
	
ALTER TABLE [Analytics_Conversion]
	WITH CHECK
	ADD CONSTRAINT [FK_Analytics_Conversion_ConversionSiteID_CMS_Site]
	FOREIGN KEY ([ConversionSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Analytics_Conversion]
	CHECK CONSTRAINT [FK_Analytics_Conversion_ConversionSiteID_CMS_Site]
