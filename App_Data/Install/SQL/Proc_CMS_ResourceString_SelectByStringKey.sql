CREATE PROCEDURE [Proc_CMS_ResourceString_SelectByStringKey]
	@StringKey nvarchar(200)
AS
BEGIN
  SELECT * FROM CMS_ResourceString WHERE StringKey = @StringKey
END
