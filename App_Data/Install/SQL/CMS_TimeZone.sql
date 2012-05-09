CREATE TABLE [CMS_TimeZone] (
		[TimeZoneID]                [int] IDENTITY(1, 1) NOT NULL,
		[TimeZoneName]              [nvarchar](200) NOT NULL,
		[TimeZoneDisplayName]       [nvarchar](200) NOT NULL,
		[TimeZoneGMT]               [float] NOT NULL,
		[TimeZoneDaylight]          [bit] NULL,
		[TimeZoneRuleStartIn]       [datetime] NOT NULL,
		[TimeZoneRuleStartRule]     [nvarchar](200) NOT NULL,
		[TimeZoneRuleEndIn]         [datetime] NOT NULL,
		[TimeZoneRuleEndRule]       [nvarchar](200) NOT NULL,
		[TimeZoneGUID]              [uniqueidentifier] NOT NULL,
		[TimeZoneLastModified]      [datetime] NOT NULL
) 
ALTER TABLE [CMS_TimeZone]
	ADD
	CONSTRAINT [PK_CMS_TimeZone]
	PRIMARY KEY
	NONCLUSTERED
	([TimeZoneID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_TimeZone_TimeZoneDisplayName]
	ON [CMS_TimeZone] ([TimeZoneDisplayName])
	
	
