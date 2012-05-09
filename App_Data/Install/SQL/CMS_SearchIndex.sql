CREATE TABLE [CMS_SearchIndex] (
		[IndexID]                             [int] IDENTITY(1, 1) NOT NULL,
		[IndexName]                           [nvarchar](200) NOT NULL,
		[IndexDisplayName]                    [nvarchar](200) NOT NULL,
		[IndexAnalyzerType]                   [nvarchar](200) NULL,
		[IndexIsCommunityGroup]               [bit] NOT NULL,
		[IndexSettings]                       [nvarchar](max) NULL,
		[IndexGUID]                           [uniqueidentifier] NOT NULL,
		[IndexLastModified]                   [datetime] NOT NULL,
		[IndexLastRebuildTime]                [datetime] NULL,
		[IndexType]                           [nvarchar](200) NOT NULL,
		[IndexStopWordsFile]                  [nvarchar](200) NULL,
		[IndexCustomAnalyzerAssemblyName]     [nvarchar](200) NULL,
		[IndexCustomAnalyzerClassName]        [nvarchar](200) NULL,
		[IndexBatchSize]                      [int] NULL,
		[IndexStatus]                         [nvarchar](10) NULL,
		[IndexLastUpdate]                     [datetime] NULL
)  
ALTER TABLE [CMS_SearchIndex]
	ADD
	CONSTRAINT [PK_CMS_SearchIndex]
	PRIMARY KEY
	NONCLUSTERED
	([IndexID])
	
	
ALTER TABLE [CMS_SearchIndex]
	ADD
	CONSTRAINT [DEFAULT_CMS_SearchIndex_IndexType]
	DEFAULT ('') FOR [IndexType]
CREATE CLUSTERED INDEX [IX_CMS_SearchIndex_IndexDisplayName]
	ON [CMS_SearchIndex] ([IndexDisplayName])
	
	
