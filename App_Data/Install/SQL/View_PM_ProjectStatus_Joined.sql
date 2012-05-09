CREATE VIEW [View_PM_ProjectStatus_Joined]
AS
SELECT     PM_Project.ProjectID, PM_Project.ProjectDisplayName, PM_Project.ProjectDeadline, CMS_User.FullName AS ProjectOwnerFullName, 
                      CMS_User.UserName AS ProjectOwnerUserName, PM_ProjectStatus.StatusDisplayName AS ProjectStatus, 
                      PM_ProjectStatus.StatusColor AS ProjectStatusColor, PM_Project.ProjectDescription, PM_ProjectStatus.StatusIcon AS ProjectStatusIcon, 
                      PM_Project.ProjectNodeID, PM_Project.ProjectGroupID, PM_ProjectStatus.StatusIsFinished, PM_Project.ProjectOwner, PM_Project.ProjectSiteID, 
                      PM_Project.ProjectAccess, CMS_Tree.NodeAliasPath, COALESCE (tasks.ProjectProgress, 0) AS ProjectProgress, PM_Project.ProjectCreatedByID, 
                      ProjectCreator.FullName AS ProjectCreatedFullName, ProjectCreator.UserName AS ProjectCreatedUserName
FROM         PM_Project LEFT OUTER JOIN
                      PM_ProjectStatus ON PM_Project.ProjectStatusID = PM_ProjectStatus.StatusID LEFT OUTER JOIN
                      CMS_User ON PM_Project.ProjectOwner = CMS_User.UserID LEFT OUTER JOIN
                      CMS_User AS ProjectCreator ON PM_Project.ProjectCreatedByID = ProjectCreator.UserID LEFT OUTER JOIN
                      CMS_Tree ON PM_Project.ProjectNodeID = CMS_Tree.NodeID LEFT OUTER JOIN
                          (SELECT     ProjectTaskProjectID, ROUND(SUM(ProjectTaskHours * ProjectTaskProgress) / SUM(ProjectTaskHours), 0, 1) AS ProjectProgress
                            FROM          (SELECT     ProjectTaskProjectID, ProjectTaskProgress, 
                                                                           CASE WHEN ProjectTaskHours = 0 THEN 1 ELSE ProjectTaskHours END AS ProjectTaskHours
                                                    FROM          PM_ProjectTask) AS modifiedTasks
                            GROUP BY ProjectTaskProjectID) AS tasks ON PM_Project.ProjectID = tasks.ProjectTaskProjectID
GO
