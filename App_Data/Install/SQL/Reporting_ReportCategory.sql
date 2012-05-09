CREATE TABLE [Reporting_ReportCategory] (
		[CategoryID]                   [int] IDENTITY(1, 1) NOT NULL,
		[CategoryDisplayName]          [nvarchar](200) NOT NULL,
		[CategoryCodeName]             [nvarchar](200) NOT NULL,
		[CategoryGUID]                 [uniqueidentifier] NOT NULL,
		[CategoryLastModified]         [datetime] NOT NULL,
		[CategoryParentID]             [int] NULL,
		[CategoryImagePath]            [nvarchar](450) NULL,
		[CategoryPath]                 [nvarchar](450) NOT NULL,
		[CategoryOrder]                [int] NULL,
		[CategoryLevel]                [int] NULL,
		[CategoryChildCount]           [int] NULL,
		[CategoryReportChildCount]     [int] NULL
) 
ALTER TABLE [Reporting_ReportCategory]
	ADD
	CONSTRAINT [PK_Reporting_ReportCategory]
	PRIMARY KEY
	NONCLUSTERED
	([CategoryID])
	
	
ALTER TABLE [Reporting_ReportCategory]
	ADD
	CONSTRAINT [DEFAULT_Reporting_ReportCategory_CategoryPath]
	DEFAULT ('') FOR [CategoryPath]
CREATE NONCLUSTERED INDEX [IX_Reporting_ReportCategory_CategoryParentID]
	ON [Reporting_ReportCategory] ([CategoryParentID])
	
CREATE CLUSTERED INDEX [IX_Reporting_ReportCategory_CategoryPath]
	ON [Reporting_ReportCategory] ([CategoryPath])
	
ALTER TABLE [Reporting_ReportCategory]
	WITH NOCHECK
	ADD CONSTRAINT [FK_Reporting_ReportCategory_CategoryID_Reporting_ReportCategory_ParentCategoryID]
	FOREIGN KEY ([CategoryParentID]) REFERENCES [Reporting_ReportCategory] ([CategoryID])
ALTER TABLE [Reporting_ReportCategory]
	CHECK CONSTRAINT [FK_Reporting_ReportCategory_CategoryID_Reporting_ReportCategory_ParentCategoryID]
