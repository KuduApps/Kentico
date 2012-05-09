-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Tree_UpdateChildNodesCount]
	@NodeID int
AS
BEGIN
	DECLARE @ChildNodes int;
	SET @ChildNodes = (SELECT COUNT(*) FROM CMS_Tree WHERE NodeParentID = @NodeID AND NodeID <> @NodeID);
	UPDATE CMS_Tree SET NodeChildNodesCount = @ChildNodes WHERE NodeID = @NodeID
END
