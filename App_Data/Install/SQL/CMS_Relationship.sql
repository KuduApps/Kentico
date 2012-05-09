CREATE TABLE [CMS_Relationship] (
		[LeftNodeID]                 [int] NOT NULL,
		[RightNodeID]                [int] NOT NULL,
		[RelationshipNameID]         [int] NOT NULL,
		[RelationshipCustomData]     [nvarchar](max) NULL
)  
ALTER TABLE [CMS_Relationship]
	ADD
	CONSTRAINT [PK_CMS_Relationship]
	PRIMARY KEY
	CLUSTERED
	([LeftNodeID], [RightNodeID], [RelationshipNameID])
	
	
ALTER TABLE [CMS_Relationship]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Relationship_LeftNodeID_CMS_Tree]
	FOREIGN KEY ([LeftNodeID]) REFERENCES [CMS_Tree] ([NodeID])
ALTER TABLE [CMS_Relationship]
	CHECK CONSTRAINT [FK_CMS_Relationship_LeftNodeID_CMS_Tree]
ALTER TABLE [CMS_Relationship]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Relationship_RelationshipNameID_CMS_RelationshipName]
	FOREIGN KEY ([RelationshipNameID]) REFERENCES [CMS_RelationshipName] ([RelationshipNameID])
ALTER TABLE [CMS_Relationship]
	CHECK CONSTRAINT [FK_CMS_Relationship_RelationshipNameID_CMS_RelationshipName]
ALTER TABLE [CMS_Relationship]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Relationship_RightNodeID_CMS_Tree]
	FOREIGN KEY ([RightNodeID]) REFERENCES [CMS_Tree] ([NodeID])
ALTER TABLE [CMS_Relationship]
	CHECK CONSTRAINT [FK_CMS_Relationship_RightNodeID_CMS_Tree]
