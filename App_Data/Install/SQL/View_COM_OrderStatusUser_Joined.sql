CREATE VIEW [View_COM_OrderStatusUser_Joined]
AS
SELECT     StatusFrom.StatusDisplayName AS FromStatusDisplayName, COM_OrderStatusUser.OrderID, COM_OrderStatusUser.FromStatusID, 
                      COM_OrderStatusUser.ToStatusID, COM_OrderStatusUser.ChangedByUserID, COM_OrderStatusUser.Date, 
                      COM_OrderStatusUser.Note, COM_OrderStatusUser.OrderStatusUserID, StatusTo.StatusDisplayName AS ToStatusDisplayName, 
                      CMS_User.UserID, CMS_User.UserName, CMS_User.FullName
FROM         COM_OrderStatusUser LEFT OUTER JOIN
                      COM_OrderStatus AS StatusFrom ON StatusFrom.StatusID = COM_OrderStatusUser.FromStatusID LEFT OUTER JOIN
                      COM_OrderStatus AS StatusTo ON StatusTo.StatusID = COM_OrderStatusUser.ToStatusID LEFT OUTER JOIN
                      CMS_User ON CMS_User.UserID = COM_OrderStatusUser.ChangedByUserID
GO
