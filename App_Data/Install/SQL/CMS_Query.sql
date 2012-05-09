CREATE TABLE [CMS_Query] (
		[QueryID]                      [int] IDENTITY(1, 1) NOT NULL,
		[QueryName]                    [nvarchar](100) NOT NULL,
		[QueryTypeID]                  [int] NOT NULL,
		[QueryText]                    [nvarchar](max) NOT NULL,
		[QueryRequiresTransaction]     [bit] NOT NULL,
		[ClassID]                      [int] NOT NULL,
		[QueryIsLocked]                [bit] NOT NULL,
		[QueryLastModified]            [datetime] NOT NULL,
		[QueryGUID]                    [uniqueidentifier] NOT NULL,
		[QueryLoadGeneration]          [int] NOT NULL,
		[QueryIsCustom]                [bit] NULL
)  
ALTER TABLE [CMS_Query]
	ADD
	CONSTRAINT [PK_CMS_Query]
	PRIMARY KEY
	NONCLUSTERED
	([QueryID])
	
	
ALTER TABLE [CMS_Query]
	ADD
	CONSTRAINT [DEFAULT_CMS_Query_QueryIsCustom]
	DEFAULT ((0)) FOR [QueryIsCustom]
ALTER TABLE [CMS_Query]
	ADD
	CONSTRAINT [DEFAULT_CMS_Query_QueryLoadGeneration]
	DEFAULT ((0)) FOR [QueryLoadGeneration]
CREATE NONCLUSTERED INDEX [IX_CMS_Query_QueryClassID_QueryName]
	ON [CMS_Query] ([ClassID], [QueryName])
	
	
CREATE CLUSTERED INDEX [IX_CMS_Query_QueryLoadGeneration]
	ON [CMS_Query] ([QueryLoadGeneration])
	
	
ALTER TABLE [CMS_Query]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Query_ClassID_CMS_Class]
	FOREIGN KEY ([ClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_Query]
	CHECK CONSTRAINT [FK_CMS_Query_ClassID_CMS_Class]
