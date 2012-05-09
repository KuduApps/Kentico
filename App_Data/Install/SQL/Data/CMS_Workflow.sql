SET IDENTITY_INSERT [CMS_Workflow] ON
INSERT INTO [CMS_Workflow] ([WorkflowID], [WorkflowDisplayName], [WorkflowName], [WorkflowGUID], [WorkflowLastModified], [WorkflowAutoPublishChanges], [WorkflowUseCheckinCheckout]) VALUES (1, N'Default workflow', N'default', '331fd346-2a1b-4a63-bb26-474e75a14807', '20120209 20:01:06', NULL, NULL)
INSERT INTO [CMS_Workflow] ([WorkflowID], [WorkflowDisplayName], [WorkflowName], [WorkflowGUID], [WorkflowLastModified], [WorkflowAutoPublishChanges], [WorkflowUseCheckinCheckout]) VALUES (46, N'Versioning without workflow', N'versioningWithoutWorkflow', '38bbee58-ba57-438b-a3db-9b0c153315c6', '20110113 19:14:24', 1, NULL)
SET IDENTITY_INSERT [CMS_Workflow] OFF
