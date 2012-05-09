CREATE TABLE [CMS_InlineControl] (
		[ControlID]                [int] IDENTITY(1, 1) NOT NULL,
		[ControlDisplayName]       [nvarchar](200) NOT NULL,
		[ControlName]              [nvarchar](200) NOT NULL,
		[ControlParameterName]     [nvarchar](200) NULL,
		[ControlDescription]       [nvarchar](max) NULL,
		[ControlGUID]              [uniqueidentifier] NOT NULL,
		[ControlLastModified]      [datetime] NOT NULL,
		[ControlFileName]          [nvarchar](400) NULL,
		[ControlProperties]        [nvarchar](max) NULL,
		[ControlResourceID]        [int] NULL
)  
ALTER TABLE [CMS_InlineControl]
	ADD
	CONSTRAINT [PK_CMS_InlineControl]
	PRIMARY KEY
	NONCLUSTERED
	([ControlID])
	
	
ALTER TABLE [CMS_InlineControl]
	ADD
	CONSTRAINT [DEFAULT_CMS_InlineControl_ControlFileName]
	DEFAULT ('') FOR [ControlFileName]
CREATE CLUSTERED INDEX [IX_CMS_InlineControl_ControlDisplayName]
	ON [CMS_InlineControl] ([ControlDisplayName])
	
	
ALTER TABLE [CMS_InlineControl]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_InlineControl_ControlResourceID_CMS_Resource]
	FOREIGN KEY ([ControlResourceID]) REFERENCES [CMS_Resource] ([ResourceID])
ALTER TABLE [CMS_InlineControl]
	CHECK CONSTRAINT [FK_CMS_InlineControl_ControlResourceID_CMS_Resource]
