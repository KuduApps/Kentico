CREATE VIEW [View_CMS_EventLog_Joined]
AS
SELECT     CMS_EventLog.EventID, CMS_EventLog.EventType, CMS_EventLog.EventTime, CMS_EventLog.Source, 
                      CMS_EventLog.EventCode, CMS_EventLog.UserID, CMS_EventLog.UserName, CMS_EventLog.IPAddress, 
                      CMS_EventLog.NodeID, CMS_EventLog.DocumentName, CMS_EventLog.EventDescription, CMS_EventLog.SiteID, 
                      CMS_EventLog.EventUrl, CMS_EventLog.EventMachineName, CMS_Site.SiteDisplayName, CMS_EventLog.EventUserAgent, 
                      CMS_EventLog.EventUrlReferrer
FROM         CMS_EventLog LEFT OUTER JOIN
                      CMS_Site ON CMS_EventLog.SiteID = CMS_Site.SiteID
GO
