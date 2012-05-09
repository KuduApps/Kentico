CREATE TABLE [Analytics_ConversionCampaign] (
		[CampaignID]       [int] NOT NULL,
		[ConversionID]     [int] NOT NULL
) 
ALTER TABLE [Analytics_ConversionCampaign]
	ADD
	CONSTRAINT [PK_Analytics_ConversionCampaign]
	PRIMARY KEY
	NONCLUSTERED
	([CampaignID], [ConversionID])
	
CREATE UNIQUE CLUSTERED INDEX [IX_Analytics_ConversionCampaign]
	ON [Analytics_ConversionCampaign] ([CampaignID], [ConversionID])
	
