CREATE TABLE [OM_Rule] (
		[RuleID]               [int] IDENTITY(1, 1) NOT NULL,
		[RuleScoreID]          [int] NOT NULL,
		[RuleDisplayName]      [nvarchar](200) NOT NULL,
		[RuleName]             [nvarchar](200) NOT NULL,
		[RuleValue]            [int] NOT NULL,
		[RuleIsRecurring]      [bit] NULL,
		[RuleMaxPoints]        [int] NULL,
		[RuleValidUntil]       [datetime] NULL,
		[RuleValidity]         [nvarchar](50) NULL,
		[RuleValidFor]         [int] NULL,
		[RuleType]             [int] NOT NULL,
		[RuleParameter]        [nvarchar](250) NOT NULL,
		[RuleCondition]        [nvarchar](max) NOT NULL,
		[RuleLastModified]     [datetime] NOT NULL,
		[RuleGUID]             [uniqueidentifier] NOT NULL,
		[RuleSiteID]           [int] NOT NULL
)  
ALTER TABLE [OM_Rule]
	ADD
	CONSTRAINT [PK_OM_Rule]
	PRIMARY KEY
	CLUSTERED
	([RuleID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Rule_RuleScoreID]
	ON [OM_Rule] ([RuleScoreID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Rule_RuleSiteID]
	ON [OM_Rule] ([RuleSiteID])
	
ALTER TABLE [OM_Rule]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Rule_CMS_Site]
	FOREIGN KEY ([RuleSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_Rule]
	CHECK CONSTRAINT [FK_OM_Rule_CMS_Site]
ALTER TABLE [OM_Rule]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Rule_OM_Score]
	FOREIGN KEY ([RuleScoreID]) REFERENCES [OM_Score] ([ScoreID])
ALTER TABLE [OM_Rule]
	CHECK CONSTRAINT [FK_OM_Rule_OM_Score]
