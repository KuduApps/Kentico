CREATE TABLE [OM_MVTest] (
		[MVTestID]                       [int] IDENTITY(1, 1) NOT NULL,
		[MVTestName]                     [nvarchar](50) NOT NULL,
		[MVTestDescription]              [nvarchar](max) NULL,
		[MVTestPage]                     [nvarchar](450) NOT NULL,
		[MVTestSiteID]                   [int] NOT NULL,
		[MVTestCulture]                  [nvarchar](50) NULL,
		[MVTestOpenFrom]                 [datetime] NULL,
		[MVTestOpenTo]                   [datetime] NULL,
		[MVTestMaxConversions]           [int] NULL,
		[MVTestConversions]              [int] NULL,
		[MVTestTargetConversionType]     [nvarchar](100) NULL,
		[MVTestGUID]                     [uniqueidentifier] NOT NULL,
		[MVTestLastModified]             [datetime] NOT NULL,
		[MVTestEnabled]                  [bit] NOT NULL,
		[MVTestDisplayName]              [nvarchar](100) NOT NULL
)  
ALTER TABLE [OM_MVTest]
	ADD
	CONSTRAINT [PK_OM_MVTest]
	PRIMARY KEY
	CLUSTERED
	([MVTestID])
	
ALTER TABLE [OM_MVTest]
	ADD
	CONSTRAINT [DEFAULT_OM_MVTest_MVTestDisplayName]
	DEFAULT ('') FOR [MVTestDisplayName]
ALTER TABLE [OM_MVTest]
	ADD
	CONSTRAINT [DEFAULT_OM_MVTest_MVTestName]
	DEFAULT ('') FOR [MVTestName]
CREATE NONCLUSTERED INDEX [IX_OM_MVTest_MVTestSiteID]
	ON [OM_MVTest] ([MVTestSiteID])
	
ALTER TABLE [OM_MVTest]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_MVTest_MVTestSiteID_CMS_Site]
	FOREIGN KEY ([MVTestSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_MVTest]
	CHECK CONSTRAINT [FK_OM_MVTest_MVTestSiteID_CMS_Site]
