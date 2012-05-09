CREATE VIEW [View_Integration_Task_Joined]
AS
SELECT     Integration_Synchronization.SynchronizationID, Integration_Synchronization.SynchronizationTaskID, 
                      Integration_Synchronization.SynchronizationConnectorID, Integration_Synchronization.SynchronizationLastRun, 
                      Integration_Synchronization.SynchronizationErrorMessage, Integration_Synchronization.SynchronizationIsRunning, Integration_Connector.ConnectorID, Integration_Connector.ConnectorName, 
                      Integration_Connector.ConnectorDisplayName, Integration_Connector.ConnectorAssemblyName, Integration_Connector.ConnectorClassName, 
                      Integration_Connector.ConnectorEnabled, Integration_Connector.ConnectorLastModified, Integration_Task.*
FROM         Integration_Synchronization RIGHT OUTER JOIN
                      Integration_Task ON Integration_Synchronization.SynchronizationTaskID = Integration_Task.TaskID INNER JOIN
                      Integration_Connector ON Integration_Synchronization.SynchronizationConnectorID = Integration_Connector.ConnectorID
GO
