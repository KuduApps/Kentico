CREATE TABLE [Reporting_ReportTable] (
		[TableID]                         [int] IDENTITY(1, 1) NOT NULL,
		[TableName]                       [nvarchar](100) NOT NULL,
		[TableDisplayName]                [nvarchar](450) NOT NULL,
		[TableQuery]                      [nvarchar](max) NOT NULL,
		[TableQueryIsStoredProcedure]     [bit] NOT NULL,
		[TableReportID]                   [int] NOT NULL,
		[TableSettings]                   [nvarchar](max) NULL,
		[TableGUID]                       [uniqueidentifier] NOT NULL,
		[TableLastModified]               [datetime] NOT NULL
)  
ALTER TABLE [Reporting_ReportTable]
	ADD
	CONSTRAINT [PK_Reporting_ReportTable]
	PRIMARY KEY
	CLUSTERED
	([TableID])
	
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_Reporting_ReportTable_TableReportID_TableName]
	ON [Reporting_ReportTable] ([TableName], [TableReportID])
	
	
ALTER TABLE [Reporting_ReportTable]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_ReportTable_TableReportID_Reporting_Report]
	FOREIGN KEY ([TableReportID]) REFERENCES [Reporting_Report] ([ReportID])
ALTER TABLE [Reporting_ReportTable]
	CHECK CONSTRAINT [FK_Reporting_ReportTable_TableReportID_Reporting_Report]
