CREATE VIEW [View_PM_ProjectTaskStatus_Joined]
AS
SELECT     PM_ProjectTask.ProjectTaskID, PM_ProjectTask.ProjectTaskDisplayName, tblAssignee.FullName AS AssigneeFullName, 
                      tblAssignee.UserName AS AssigneeUserName, tblOwner.FullName AS OwnerFullName, tblOwner.UserName AS OwnerUserName, 
                      PM_ProjectTaskPriority.TaskPriorityDisplayName, PM_ProjectTaskStatus.TaskStatusDisplayName, PM_ProjectTask.ProjectTaskHours, 
                      PM_ProjectTask.ProjectTaskProgress, PM_ProjectTask.ProjectTaskProjectID, PM_ProjectTaskStatus.TaskStatusColor, 
                      PM_ProjectTask.ProjectTaskProjectOrder, PM_ProjectTask.ProjectTaskUserOrder, PM_ProjectTaskStatus.TaskStatusIcon, 
                      PM_ProjectTask.ProjectTaskAssignedToUserID, PM_ProjectTask.ProjectTaskOwnerID, PM_ProjectTask.ProjectTaskDeadline, 
                      PM_ProjectTask.ProjectTaskIsPrivate, PM_Project.ProjectDisplayName, PM_ProjectTaskStatus.TaskStatusIsFinished, 
                      PM_ProjectTaskPriority.TaskPriorityOrder, PM_Project.ProjectSiteID, PM_Project.ProjectOwner, PM_Project.ProjectGroupID, 
                      PM_Project.ProjectAccess, PM_Project.ProjectID
FROM         PM_ProjectTask LEFT OUTER JOIN
                      PM_ProjectTaskStatus ON PM_ProjectTaskStatus.TaskStatusID = PM_ProjectTask.ProjectTaskStatusID LEFT OUTER JOIN
                      PM_ProjectTaskPriority ON PM_ProjectTask.ProjectTaskPriorityID = PM_ProjectTaskPriority.TaskPriorityID LEFT OUTER JOIN
                      PM_Project ON PM_ProjectTask.ProjectTaskProjectID = PM_Project.ProjectID LEFT OUTER JOIN
                      CMS_User AS tblAssignee ON PM_ProjectTask.ProjectTaskAssignedToUserID = tblAssignee.UserID LEFT OUTER JOIN
                      CMS_User AS tblOwner ON PM_ProjectTask.ProjectTaskOwnerID = tblOwner.UserID
GO
