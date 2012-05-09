CREATE VIEW [View_PageInfo_Blank]
AS
SELECT     CMS_Tree.NodeID, CMS_Tree.NodeAliasPath, CMS_Tree.NodeName, CMS_Tree.NodeAlias, CMS_Tree.NodeClassID, 
                      CMS_Tree.NodeParentID, CMS_Tree.NodeLevel, CMS_Tree.NodeACLID, CMS_Tree.NodeSiteID, CMS_Tree.NodeGUID, 
                      CMS_Tree.NodeOrder, CMS_Document.DocumentID, CMS_Document.DocumentName, CMS_Document.DocumentNamePath, 
                      CMS_Document.DocumentPublishFrom, CMS_Document.DocumentPublishTo, CMS_Document.DocumentUrlPath, 
                      CMS_Document.DocumentCulture, CMS_Document.DocumentPageTitle, CMS_Document.DocumentPageKeyWords, 
                      CMS_Document.DocumentPageDescription, CMS_Document.DocumentMenuCaption, CMS_Document.DocumentPageTemplateID, 
                      CMS_Class.ClassName, CMS_Document.DocumentContent, CMS_Document.DocumentStylesheetID, CMS_Tree.IsSecuredNode, 
                      CMS_Document.DocumentMenuRedirectUrl, CMS_Document.DocumentMenuJavascript, CMS_Tree.NodeCacheMinutes, 
                      CMS_Tree.NodeSKUID, CMS_Tree.NodeDocType, CMS_Tree.NodeHeadTags, CMS_Tree.NodeInheritPageLevels, 
                      CMS_Document.DocumentMenuItemInactive, CMS_Document.DocumentMenuClass, CMS_Document.DocumentMenuStyle, 
                      CMS_Document.DocumentMenuItemHideInNavigation, CMS_Tree.NodeChildNodesCount, CMS_Tree.NodeBodyElementAttributes, 
                      CMS_Tree.RequiresSSL, CMS_Tree.NodeLinkedNodeID, CMS_Tree.NodeOwner, 
                      CMS_Document.DocumentCheckedOutVersionHistoryID, CMS_Document.DocumentPublishedVersionHistoryID, 
                      CMS_Document.DocumentWorkflowStepID, CMS_WorkflowStep.StepName, CMS_Document.DocumentExtensions, 
                      CMS_Document.DocumentCampaign, CMS_Tree.NodeGroupID, CMS_Document.DocumentWebParts, 
                      CMS_Document.DocumentGroupWebParts,CMS_Document.DocumentTrackConversionName,CMS_Document.DocumentConversionValue, CMS_Tree.NodeLinkedNodeSiteID, CMS_Document.DocumentWorkflowCycleGUID, CMS_Document.DocumentGUID
FROM         CMS_WorkflowStep INNER JOIN
                      CMS_Document ON CMS_WorkflowStep.StepID = CMS_Document.DocumentWorkflowStepID RIGHT OUTER JOIN
                      CMS_Tree INNER JOIN
                      CMS_Class ON CMS_Tree.NodeClassID = CMS_Class.ClassID ON 1 = 0
GO
