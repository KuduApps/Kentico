CREATE TABLE [CMS_WebPart] (
		[WebPartID]                 [int] IDENTITY(1, 1) NOT NULL,
		[WebPartName]               [nvarchar](100) NOT NULL,
		[WebPartDisplayName]        [nvarchar](100) NOT NULL,
		[WebPartDescription]        [nvarchar](max) NOT NULL,
		[WebPartFileName]           [nvarchar](100) NOT NULL,
		[WebPartProperties]         [nvarchar](max) NOT NULL,
		[WebPartCategoryID]         [int] NOT NULL,
		[WebPartParentID]           [int] NULL,
		[WebPartDocumentation]      [nvarchar](max) NULL,
		[WebPartGUID]               [uniqueidentifier] NOT NULL,
		[WebPartLastModified]       [datetime] NOT NULL,
		[WebPartType]               [int] NULL,
		[WebPartLoadGeneration]     [int] NOT NULL,
		[WebPartLastSelection]      [datetime] NULL,
		[WebPartDefaultValues]      [nvarchar](max) NULL,
		[WebPartResourceID]         [int] NULL,
		[WebPartCSS]                [nvarchar](max) NULL
)  
ALTER TABLE [CMS_WebPart]
	ADD
	CONSTRAINT [PK_CMS_WebPart]
	PRIMARY KEY
	NONCLUSTERED
	([WebPartID])
	
	
ALTER TABLE [CMS_WebPart]
	ADD
	CONSTRAINT [DEFAULT_CMS_WebPart_WebPartLoadGeneration]
	DEFAULT ((0)) FOR [WebPartLoadGeneration]
CREATE NONCLUSTERED INDEX [IX_CMS_WebPart_WebPartCategoryID]
	ON [CMS_WebPart] ([WebPartCategoryID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_WebPart_WebPartLastSelection]
	ON [CMS_WebPart] ([WebPartLastSelection])
	
	
CREATE CLUSTERED INDEX [IX_CMS_WebPart_WebPartLoadGeneration]
	ON [CMS_WebPart] ([WebPartLoadGeneration])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WebPart_WebPartName]
	ON [CMS_WebPart] ([WebPartName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WebPart_WebPartParentID]
	ON [CMS_WebPart] ([WebPartParentID])
	
ALTER TABLE [CMS_WebPart]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WebPart_WebPartCategoryID_CMS_WebPartCategory]
	FOREIGN KEY ([WebPartCategoryID]) REFERENCES [CMS_WebPartCategory] ([CategoryID])
ALTER TABLE [CMS_WebPart]
	CHECK CONSTRAINT [FK_CMS_WebPart_WebPartCategoryID_CMS_WebPartCategory]
ALTER TABLE [CMS_WebPart]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WebPart_WebPartParentID_CMS_WebPart]
	FOREIGN KEY ([WebPartParentID]) REFERENCES [CMS_WebPart] ([WebPartID])
ALTER TABLE [CMS_WebPart]
	CHECK CONSTRAINT [FK_CMS_WebPart_WebPartParentID_CMS_WebPart]
ALTER TABLE [CMS_WebPart]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WebPart_WebPartResourceID_CMS_Resource]
	FOREIGN KEY ([WebPartResourceID]) REFERENCES [CMS_Resource] ([ResourceID])
ALTER TABLE [CMS_WebPart]
	CHECK CONSTRAINT [FK_CMS_WebPart_WebPartResourceID_CMS_Resource]
