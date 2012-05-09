CREATE TABLE [Reporting_SavedReport] (
		[SavedReportID]                  [int] IDENTITY(1, 1) NOT NULL,
		[SavedReportReportID]            [int] NOT NULL,
		[SavedReportGUID]                [uniqueidentifier] NOT NULL,
		[SavedReportTitle]               [nvarchar](200) NULL,
		[SavedReportDate]                [datetime] NOT NULL,
		[SavedReportHTML]                [nvarchar](max) NOT NULL,
		[SavedReportParameters]          [nvarchar](max) NOT NULL,
		[SavedReportCreatedByUserID]     [int] NULL,
		[SavedReportLastModified]        [datetime] NOT NULL
)  
ALTER TABLE [Reporting_SavedReport]
	ADD
	CONSTRAINT [PK_Reporting_SavedReport]
	PRIMARY KEY
	NONCLUSTERED
	([SavedReportID])
	
	
CREATE NONCLUSTERED INDEX [IX_Reporting_SavedReport_SavedReportCreatedByUserID]
	ON [Reporting_SavedReport] ([SavedReportCreatedByUserID])
	
CREATE CLUSTERED INDEX [IX_Reporting_SavedReport_SavedReportReportID_SavedReportDate]
	ON [Reporting_SavedReport] ([SavedReportReportID], [SavedReportDate] DESC)
	
	
ALTER TABLE [Reporting_SavedReport]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_SavedReport_SavedReportCreatedByUserID_CMS_User]
	FOREIGN KEY ([SavedReportCreatedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Reporting_SavedReport]
	CHECK CONSTRAINT [FK_Reporting_SavedReport_SavedReportCreatedByUserID_CMS_User]
ALTER TABLE [Reporting_SavedReport]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_SavedReport_SavedReportReportID_Reporting_Report]
	FOREIGN KEY ([SavedReportReportID]) REFERENCES [Reporting_Report] ([ReportID])
ALTER TABLE [Reporting_SavedReport]
	CHECK CONSTRAINT [FK_Reporting_SavedReport_SavedReportReportID_Reporting_Report]
