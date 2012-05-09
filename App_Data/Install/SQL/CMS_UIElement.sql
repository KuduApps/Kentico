CREATE TABLE [CMS_UIElement] (
		[ElementID]               [int] IDENTITY(1, 1) NOT NULL,
		[ElementDisplayName]      [nvarchar](200) NOT NULL,
		[ElementName]             [nvarchar](200) NOT NULL,
		[ElementCaption]          [nvarchar](200) NULL,
		[ElementTargetURL]        [nvarchar](650) NULL,
		[ElementResourceID]       [int] NOT NULL,
		[ElementParentID]         [int] NULL,
		[ElementChildCount]       [int] NOT NULL,
		[ElementOrder]            [int] NULL,
		[ElementLevel]            [int] NOT NULL,
		[ElementIDPath]           [nvarchar](450) NOT NULL,
		[ElementIconPath]         [nvarchar](200) NULL,
		[ElementIsCustom]         [bit] NOT NULL,
		[ElementLastModified]     [datetime] NOT NULL,
		[ElementGUID]             [uniqueidentifier] NOT NULL,
		[ElementSize]             [int] NULL,
		[ElementDescription]      [nvarchar](max) NULL,
		[ElementFromVersion]      [nvarchar](20) NULL
)  
ALTER TABLE [CMS_UIElement]
	ADD
	CONSTRAINT [PK_CMS_UIElement]
	PRIMARY KEY
	NONCLUSTERED
	([ElementID])
	
	
ALTER TABLE [CMS_UIElement]
	ADD
	CONSTRAINT [DEFAULT_CMS_UIElement_ElementSize]
	DEFAULT ((0)) FOR [ElementSize]
CREATE CLUSTERED INDEX [IX_CMS_UIElement_ElementResourceID_ElementLevel_ElementParentID_ElementOrder_ElementCaption]
	ON [CMS_UIElement] ([ElementResourceID], [ElementLevel], [ElementParentID], [ElementOrder], [ElementCaption])
	
	
ALTER TABLE [CMS_UIElement]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UIElement_ElementParentID_CMS_UIElement]
	FOREIGN KEY ([ElementParentID]) REFERENCES [CMS_UIElement] ([ElementID])
ALTER TABLE [CMS_UIElement]
	CHECK CONSTRAINT [FK_CMS_UIElement_ElementParentID_CMS_UIElement]
ALTER TABLE [CMS_UIElement]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UIElement_ElementResourceID_CMS_Resource]
	FOREIGN KEY ([ElementResourceID]) REFERENCES [CMS_Resource] ([ResourceID])
ALTER TABLE [CMS_UIElement]
	CHECK CONSTRAINT [FK_CMS_UIElement_ElementResourceID_CMS_Resource]
