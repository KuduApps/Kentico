CREATE TABLE [OM_ABTest] (
		[ABTestID]                       [int] IDENTITY(1, 1) NOT NULL,
		[ABTestName]                     [nvarchar](50) NOT NULL,
		[ABTestDescription]              [nvarchar](max) NULL,
		[ABTestCulture]                  [nvarchar](50) NULL,
		[ABTestOriginalPage]             [nvarchar](450) NOT NULL,
		[ABTestOpenFrom]                 [datetime] NULL,
		[ABTestOpenTo]                   [datetime] NULL,
		[ABTestEnabled]                  [bit] NOT NULL,
		[ABTestSiteID]                   [int] NOT NULL,
		[ABTestMaxConversions]           [int] NULL,
		[ABTestConversions]              [int] NULL,
		[ABTestGUID]                     [uniqueidentifier] NOT NULL,
		[ABTestLastModified]             [datetime] NOT NULL,
		[ABTestTargetConversionType]     [nvarchar](100) NULL,
		[ABTestDisplayName]              [nvarchar](100) NOT NULL
)  
ALTER TABLE [OM_ABTest]
	ADD
	CONSTRAINT [PK_OM_ABTest]
	PRIMARY KEY
	CLUSTERED
	([ABTestID])
	
ALTER TABLE [OM_ABTest]
	ADD
	CONSTRAINT [DEFAULT_OM_ABTest_ABTestDisplayName]
	DEFAULT ('') FOR [ABTestDisplayName]
ALTER TABLE [OM_ABTest]
	ADD
	CONSTRAINT [DEFAULT_OM_ABTest_ABTestName]
	DEFAULT ('') FOR [ABTestName]
CREATE NONCLUSTERED INDEX [IX_OM_ABTest_SiteID]
	ON [OM_ABTest] ([ABTestSiteID])
	
ALTER TABLE [OM_ABTest]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ABTest_SiteID_CMS_Site]
	FOREIGN KEY ([ABTestSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_ABTest]
	CHECK CONSTRAINT [FK_OM_ABTest_SiteID_CMS_Site]
