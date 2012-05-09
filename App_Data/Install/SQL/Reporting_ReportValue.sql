CREATE TABLE [Reporting_ReportValue] (
		[ValueID]                         [int] IDENTITY(1, 1) NOT NULL,
		[ValueName]                       [nvarchar](100) NOT NULL,
		[ValueDisplayName]                [nvarchar](450) NOT NULL,
		[ValueQuery]                      [nvarchar](max) NOT NULL,
		[ValueQueryIsStoredProcedure]     [bit] NOT NULL,
		[ValueFormatString]               [nvarchar](200) NULL,
		[ValueReportID]                   [int] NOT NULL,
		[ValueGUID]                       [uniqueidentifier] NOT NULL,
		[ValueLastModified]               [datetime] NOT NULL
)  
ALTER TABLE [Reporting_ReportValue]
	ADD
	CONSTRAINT [PK_Reporting_ReportValue]
	PRIMARY KEY
	CLUSTERED
	([ValueID])
	
	
CREATE NONCLUSTERED INDEX [IX_Reporting_ReportValue_ValueName_ValueReportID]
	ON [Reporting_ReportValue] ([ValueName], [ValueReportID])
	
	
ALTER TABLE [Reporting_ReportValue]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_ReportValue_ValueReportID_Reporting_Report]
	FOREIGN KEY ([ValueReportID]) REFERENCES [Reporting_Report] ([ReportID])
ALTER TABLE [Reporting_ReportValue]
	CHECK CONSTRAINT [FK_Reporting_ReportValue_ValueReportID_Reporting_Report]
