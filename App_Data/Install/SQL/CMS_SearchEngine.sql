CREATE TABLE [CMS_SearchEngine] (
		[SearchEngineID]                   [int] IDENTITY(1, 1) NOT NULL,
		[SearchEngineDisplayName]          [nvarchar](200) NOT NULL,
		[SearchEngineName]                 [nvarchar](200) NOT NULL,
		[SearchEngineDomainRule]           [nvarchar](450) NOT NULL,
		[SearchEngineKeywordParameter]     [nvarchar](200) NULL,
		[SearchEngineGUID]                 [uniqueidentifier] NOT NULL,
		[SearchEngineLastModified]         [datetime] NOT NULL
) 
ALTER TABLE [CMS_SearchEngine]
	ADD
	CONSTRAINT [PK_CMS_SearchEngine]
	PRIMARY KEY
	CLUSTERED
	([SearchEngineID])
	
