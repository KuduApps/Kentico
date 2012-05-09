CREATE TABLE [PM_Project] (
		[ProjectID]                [int] IDENTITY(1, 1) NOT NULL,
		[ProjectNodeID]            [int] NULL,
		[ProjectGroupID]           [int] NULL,
		[ProjectDisplayName]       [nvarchar](200) NOT NULL,
		[ProjectName]              [nvarchar](200) NOT NULL,
		[ProjectDescription]       [nvarchar](max) NULL,
		[ProjectStartDate]         [datetime] NULL,
		[ProjectDeadline]          [datetime] NULL,
		[ProjectOwner]             [int] NULL,
		[ProjectCreatedByID]       [int] NULL,
		[ProjectStatusID]          [int] NOT NULL,
		[ProjectSiteID]            [int] NOT NULL,
		[ProjectGUID]              [uniqueidentifier] NOT NULL,
		[ProjectLastModified]      [datetime] NOT NULL,
		[ProjectAllowOrdering]     [bit] NULL,
		[ProjectAccess]            [int] NOT NULL
)  
ALTER TABLE [PM_Project]
	ADD
	CONSTRAINT [PK_PM_Project]
	PRIMARY KEY
	CLUSTERED
	([ProjectID])
	
CREATE NONCLUSTERED INDEX [IX_PM_Project_ProjectGroupID]
	ON [PM_Project] ([ProjectGroupID])
	
CREATE NONCLUSTERED INDEX [IX_PM_Project_ProjectNodeID]
	ON [PM_Project] ([ProjectNodeID])
	
CREATE NONCLUSTERED INDEX [IX_PM_Project_ProjectOwner]
	ON [PM_Project] ([ProjectOwner])
	
CREATE NONCLUSTERED INDEX [IX_PM_Project_ProjectSiteID]
	ON [PM_Project] ([ProjectSiteID])
	
CREATE NONCLUSTERED INDEX [IX_PM_Project_ProjectStatusID]
	ON [PM_Project] ([ProjectStatusID])
	
ALTER TABLE [PM_Project]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_Project_ProjectCreatedByID_CMS_User]
	FOREIGN KEY ([ProjectCreatedByID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [PM_Project]
	CHECK CONSTRAINT [FK_PM_Project_ProjectCreatedByID_CMS_User]
ALTER TABLE [PM_Project]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_Project_ProjectGroupID_Community_Group]
	FOREIGN KEY ([ProjectGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [PM_Project]
	CHECK CONSTRAINT [FK_PM_Project_ProjectGroupID_Community_Group]
ALTER TABLE [PM_Project]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_Project_ProjectNodeID_CMS_Tree]
	FOREIGN KEY ([ProjectNodeID]) REFERENCES [CMS_Tree] ([NodeID])
ALTER TABLE [PM_Project]
	CHECK CONSTRAINT [FK_PM_Project_ProjectNodeID_CMS_Tree]
ALTER TABLE [PM_Project]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_Project_ProjectOwner_CMS_User]
	FOREIGN KEY ([ProjectOwner]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [PM_Project]
	CHECK CONSTRAINT [FK_PM_Project_ProjectOwner_CMS_User]
ALTER TABLE [PM_Project]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_Project_ProjectSiteID_CMS_Site]
	FOREIGN KEY ([ProjectSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [PM_Project]
	CHECK CONSTRAINT [FK_PM_Project_ProjectSiteID_CMS_Site]
ALTER TABLE [PM_Project]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_Project_ProjectStatusID_PM_ProjectStatus]
	FOREIGN KEY ([ProjectStatusID]) REFERENCES [PM_ProjectStatus] ([StatusID])
ALTER TABLE [PM_Project]
	CHECK CONSTRAINT [FK_PM_Project_ProjectStatusID_PM_ProjectStatus]
