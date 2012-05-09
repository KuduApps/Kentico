CREATE TABLE [CMS_Widget] (
		[WidgetID]                [int] IDENTITY(1, 1) NOT NULL,
		[WidgetWebPartID]         [int] NOT NULL,
		[WidgetDisplayName]       [nvarchar](100) NOT NULL,
		[WidgetName]              [nvarchar](100) NOT NULL,
		[WidgetDescription]       [nvarchar](max) NULL,
		[WidgetCategoryID]        [int] NOT NULL,
		[WidgetProperties]        [nvarchar](max) NULL,
		[WidgetSecurity]          [int] NOT NULL,
		[WidgetGUID]              [uniqueidentifier] NOT NULL,
		[WidgetLastModified]      [datetime] NOT NULL,
		[WidgetIsEnabled]         [bit] NOT NULL,
		[WidgetForGroup]          [bit] NOT NULL,
		[WidgetForEditor]         [bit] NOT NULL,
		[WidgetForUser]           [bit] NOT NULL,
		[WidgetForDashboard]      [bit] NOT NULL,
		[WidgetForInline]         [bit] NOT NULL,
		[WidgetDocumentation]     [nvarchar](max) NULL,
		[WidgetDefaultValues]     [nvarchar](max) NULL
)  
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [PK_CMS_Widget]
	PRIMARY KEY
	NONCLUSTERED
	([WidgetID])
	
	
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DEFAULT_CMS_Widget_WidgetForDashboard]
	DEFAULT ((0)) FOR [WidgetForDashboard]
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DEFAULT_CMS_Widget_WidgetForEditor]
	DEFAULT ((0)) FOR [WidgetForEditor]
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DEFAULT_CMS_Widget_WidgetForGroup]
	DEFAULT ((0)) FOR [WidgetForGroup]
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DEFAULT_CMS_Widget_WidgetForInline]
	DEFAULT ((0)) FOR [WidgetForInline]
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DEFAULT_CMS_Widget_WidgetForUser]
	DEFAULT ((0)) FOR [WidgetForUser]
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DEFAULT_CMS_Widget_WidgetIsEnabled]
	DEFAULT ((0)) FOR [WidgetIsEnabled]
ALTER TABLE [CMS_Widget]
	ADD
	CONSTRAINT [DF_CMS_Widget_WidgetSecurity]
	DEFAULT ((2)) FOR [WidgetSecurity]
CREATE CLUSTERED INDEX [IX_CMS_Widget_WidgetCategoryID_WidgetDisplayName]
	ON [CMS_Widget] ([WidgetCategoryID], [WidgetDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Widget_WidgetIsEnabled_WidgetForGroup_WidgetForEditor_WidgetForUser]
	ON [CMS_Widget] ([WidgetIsEnabled], [WidgetForGroup], [WidgetForEditor], [WidgetForUser])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Widget_WidgetWebPartID]
	ON [CMS_Widget] ([WidgetWebPartID])
	
ALTER TABLE [CMS_Widget]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Widget_WidgetCategoryID_CMS_WidgetCategory]
	FOREIGN KEY ([WidgetCategoryID]) REFERENCES [CMS_WidgetCategory] ([WidgetCategoryID])
ALTER TABLE [CMS_Widget]
	CHECK CONSTRAINT [FK_CMS_Widget_WidgetCategoryID_CMS_WidgetCategory]
ALTER TABLE [CMS_Widget]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Widget_WidgetWebPartID_CMS_WebPart]
	FOREIGN KEY ([WidgetWebPartID]) REFERENCES [CMS_WebPart] ([WebPartID])
ALTER TABLE [CMS_Widget]
	CHECK CONSTRAINT [FK_CMS_Widget_WidgetWebPartID_CMS_WebPart]
