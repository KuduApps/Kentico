CREATE TABLE [Reporting_SavedGraph] (
		[SavedGraphID]                [int] IDENTITY(1, 1) NOT NULL,
		[SavedGraphSavedReportID]     [int] NOT NULL,
		[SavedGraphGUID]              [uniqueidentifier] NOT NULL,
		[SavedGraphBinary]            [varbinary](max) NOT NULL,
		[SavedGraphMimeType]          [nvarchar](100) NOT NULL,
		[SavedGraphLastModified]      [datetime] NOT NULL
)  
ALTER TABLE [Reporting_SavedGraph]
	ADD
	CONSTRAINT [PK_Reporting_SavedGraph]
	PRIMARY KEY
	CLUSTERED
	([SavedGraphID])
	
	
CREATE NONCLUSTERED INDEX [IX_Reporting_SavedGraph_SavedGraphGUID]
	ON [Reporting_SavedGraph] ([SavedGraphGUID])
	
	
CREATE NONCLUSTERED INDEX [IX_Reporting_SavedGraph_SavedGraphSavedReportID]
	ON [Reporting_SavedGraph] ([SavedGraphSavedReportID])
	
ALTER TABLE [Reporting_SavedGraph]
	WITH CHECK
	ADD CONSTRAINT [FK_Reporting_SavedGraph_SavedGraphSavedReportID_Reporting_SavedReport]
	FOREIGN KEY ([SavedGraphSavedReportID]) REFERENCES [Reporting_SavedReport] ([SavedReportID])
ALTER TABLE [Reporting_SavedGraph]
	CHECK CONSTRAINT [FK_Reporting_SavedGraph_SavedGraphSavedReportID_Reporting_SavedReport]
