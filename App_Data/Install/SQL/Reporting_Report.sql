CREATE TABLE [Reporting_Report] (
		[ReportID]               [int] IDENTITY(1, 1) NOT NULL,
		[ReportName]             [nvarchar](100) NOT NULL,
		[ReportDisplayName]      [nvarchar](440) NOT NULL,
		[ReportLayout]           [nvarchar](max) NOT NULL,
		[ReportParameters]       [nvarchar](max) NOT NULL,
		[ReportCategoryID]       [int] NOT NULL,
		[ReportAccess]           [int] NOT NULL,
		[ReportGUID]             [uniqueidentifier] NOT NULL,
		[ReportLastModified]     [datetime] NOT NULL
)  
ALTER TABLE [Reporting_Report]
	ADD
	CONSTRAINT [PK_Reporting_Report]
	PRIMARY KEY
	NONCLUSTERED
	([ReportID])
	
	
ALTER TABLE [Reporting_Report]
	ADD
	CONSTRAINT [DEFAULT_Reporting_Report_ReportDisplayName]
	DEFAULT ('') FOR [ReportDisplayName]
CREATE CLUSTERED INDEX [IX_Reporting_Report_ReportCategoryID_ReportDisplayName]
	ON [Reporting_Report] ([ReportDisplayName], [ReportCategoryID])
	
	
CREATE NONCLUSTERED INDEX [IX_Reporting_Report_ReportGUID_ReportName]
	ON [Reporting_Report] ([ReportGUID], [ReportName])
	
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_Reporting_Report_ReportName]
	ON [Reporting_Report] ([ReportName])
	
	
ALTER TABLE [Reporting_Report]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_Report_ReportCategory_Reporting_ReportCategory]
	FOREIGN KEY ([ReportCategoryID]) REFERENCES [Reporting_ReportCategory] ([CategoryID])
ALTER TABLE [Reporting_Report]
	CHECK CONSTRAINT [FK_Reporting_Report_ReportCategory_Reporting_ReportCategory]
ALTER TABLE [Reporting_Report]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_Report_ReportCategoryID_Reporting_ReportCategory]
	FOREIGN KEY ([ReportCategoryID]) REFERENCES [Reporting_ReportCategory] ([CategoryID])
ALTER TABLE [Reporting_Report]
	CHECK CONSTRAINT [FK_Reporting_Report_ReportCategoryID_Reporting_ReportCategory]
