CREATE TABLE [Analytics_Campaign] (
		[CampaignID]                         [int] IDENTITY(1, 1) NOT NULL,
		[CampaignName]                       [nvarchar](200) NOT NULL,
		[CampaignDisplayName]                [nvarchar](100) NOT NULL,
		[CampaignDescription]                [nvarchar](max) NULL,
		[CampaignSiteID]                     [int] NOT NULL,
		[CampaignImpressions]                [int] NULL,
		[CampaignOpenFrom]                   [datetime] NULL,
		[CampaignOpenTo]                     [datetime] NULL,
		[CampaignRules]                      [nvarchar](max) NULL,
		[CampaignGUID]                       [uniqueidentifier] NOT NULL,
		[CampaignLastModified]               [datetime] NOT NULL,
		[CampaignUseAllConversions]          [bit] NULL,
		[CampaignEnabled]                    [bit] NULL,
		[CampaignTotalCost]                  [float] NULL,
		[CampaignGoalVisitorsMin]            [float] NULL,
		[CampaignGoalConversionsMin]         [float] NULL,
		[CampaignGoalValueMin]               [float] NULL,
		[CampaignGoalPerVisitorMin]          [float] NULL,
		[CampaignGoalVisitors]               [float] NULL,
		[CampaignGoalConversions]            [float] NULL,
		[CampaignGoalValue]                  [float] NULL,
		[CampaignGoalPerVisitor]             [float] NULL,
		[CampaignGoalVisitorsPercent]        [bit] NULL,
		[CampaignGoalConversionsPercent]     [bit] NULL,
		[CampaignGoalValuePercent]           [bit] NULL,
		[CampaignGoalPerVisitorPercent]      [bit] NULL
)  
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [PK_Analytics_Campaign]
	PRIMARY KEY
	CLUSTERED
	([CampaignID])
	
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Campaign_CampaignDisplayName]
	DEFAULT ('') FOR [CampaignDisplayName]
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Campaign_CampaignEnabled]
	DEFAULT ((1)) FOR [CampaignEnabled]
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Campaign_CampaignGoalValuePercent]
	DEFAULT ((0)) FOR [CampaignGoalValuePercent]
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Campaign_CampaignGoalVisitorsPercent]
	DEFAULT ((0)) FOR [CampaignGoalVisitorsPercent]
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Campaign_CampaignName]
	DEFAULT ('') FOR [CampaignName]
ALTER TABLE [Analytics_Campaign]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Campaign_CampaignUseAllConversions]
	DEFAULT ((1)) FOR [CampaignUseAllConversions]
ALTER TABLE [Analytics_Campaign]
	WITH CHECK
	ADD CONSTRAINT [FK_Analytics_Campaign_StatisticsSiteID_CMS_Site]
	FOREIGN KEY ([CampaignSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Analytics_Campaign]
	CHECK CONSTRAINT [FK_Analytics_Campaign_StatisticsSiteID_CMS_Site]
