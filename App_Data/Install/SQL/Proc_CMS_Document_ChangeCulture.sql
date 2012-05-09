CREATE PROCEDURE [Proc_CMS_Document_ChangeCulture]
	@ID int,
	@CurrentCulture varchar(20),
	@NewCulture varchar(20)
AS
BEGIN
	UPDATE CMS_Document SET DocumentCulture = @NewCulture WHERE (DocumentCulture = @CurrentCulture) AND (DocumentNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @ID)) AND (DocumentNodeID NOT IN (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @ID AND DocumentCulture = @NewCulture));
END
