CREATE TABLE [OM_PageVisit] (
		[PageVisitID]                     [int] IDENTITY(1, 1) NOT NULL,
		[PageVisitDetail]                 [nvarchar](max) NULL,
		[PageVisitActivityID]             [int] NOT NULL,
		[PageVisitABVariantName]          [nvarchar](200) NULL,
		[PageVisitMVTCombinationName]     [nvarchar](200) NULL
)  
ALTER TABLE [OM_PageVisit]
	ADD
	CONSTRAINT [PK_OM_PageVisit]
	PRIMARY KEY
	CLUSTERED
	([PageVisitID])
	
CREATE NONCLUSTERED INDEX [IX_OM_PageVisit_PageVisitActivityID]
	ON [OM_PageVisit] ([PageVisitActivityID])
	
ALTER TABLE [OM_PageVisit]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_PageVisit_OM_Activity]
	FOREIGN KEY ([PageVisitActivityID]) REFERENCES [OM_Activity] ([ActivityID])
ALTER TABLE [OM_PageVisit]
	CHECK CONSTRAINT [FK_OM_PageVisit_OM_Activity]
