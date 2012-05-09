CREATE PROCEDURE [Proc_CMS_WebPartContainer_RemoveDependencies]
	@ID int
AS
BEGIN
	DELETE FROM CMS_WebPartContainerSite WHERE ContainerID=@ID;
END
