CREATE VIEW [View_COM_Customer_Joined]
AS
SELECT     COM_Customer.*, CMS_Country.CountryID, CMS_Country.CountryDisplayName, CMS_Country.CountryName, 
                      CMS_State.StateID, CMS_State.StateDisplayName, CMS_State.StateName, CMS_State.StateCode, CMS_State.CountryID AS Expr1,
                       CMS_User.UserID, CMS_User.UserCreated, CMS_Country.CountryGUID, CMS_Country.CountryLastModified, CMS_State.StateGUID, 
                      CMS_State.StateLastModified
FROM         COM_Customer LEFT OUTER JOIN
                      CMS_User ON COM_Customer.CustomerUserID = CMS_User.UserID LEFT OUTER JOIN
                      CMS_Country ON CMS_Country.CountryID = COM_Customer.CustomerCountryID LEFT OUTER JOIN
                      CMS_State ON CMS_State.StateID = COM_Customer.CustomerStateID
GO
