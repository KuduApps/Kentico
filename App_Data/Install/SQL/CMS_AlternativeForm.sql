CREATE TABLE [CMS_AlternativeForm] (
		[FormID]                 [int] IDENTITY(1, 1) NOT NULL,
		[FormDisplayName]        [nvarchar](250) NOT NULL,
		[FormName]               [nvarchar](250) NOT NULL,
		[FormClassID]            [int] NOT NULL,
		[FormDefinition]         [nvarchar](max) NULL,
		[FormLayout]             [nvarchar](max) NULL,
		[FormGUID]               [uniqueidentifier] NOT NULL,
		[FormLastModified]       [datetime] NOT NULL,
		[FormCoupledClassID]     [int] NULL
)  
ALTER TABLE [CMS_AlternativeForm]
	ADD
	CONSTRAINT [PK_CMS_AlternativeForm]
	PRIMARY KEY
	CLUSTERED
	([FormID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_AlternativeForm_FormClassID_FormName]
	ON [CMS_AlternativeForm] ([FormClassID], [FormName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_AlternativeForm_FormCoupledClassID]
	ON [CMS_AlternativeForm] ([FormCoupledClassID])
	
ALTER TABLE [CMS_AlternativeForm]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_AlternativeForm_FormClassID_CMS_Class]
	FOREIGN KEY ([FormClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_AlternativeForm]
	CHECK CONSTRAINT [FK_CMS_AlternativeForm_FormClassID_CMS_Class]
ALTER TABLE [CMS_AlternativeForm]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_AlternativeForm_FormCoupledClassID_CMS_Class]
	FOREIGN KEY ([FormCoupledClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_AlternativeForm]
	CHECK CONSTRAINT [FK_CMS_AlternativeForm_FormCoupledClassID_CMS_Class]
