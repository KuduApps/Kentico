CREATE PROCEDURE [Proc_CMS_TagGroup_RemoveDependencies]
	@ID int
AS
BEGIN
	-- Remove tags and tag group from all documents:
    -- 1) with tags from specified tag group
	--    (it covers documents with tags and set/inherited tag group)
	-- 2) with specified tag group
	--    (it covers documents without tags and set tag group)
	UPDATE CMS_Document SET DocumentTagGroupID = NULL, DocumentTags = NULL 
	WHERE (DocumentID IN (SELECT DISTINCT DocumentID FROM CMS_Tag JOIN CMS_DocumentTag ON CMS_DocumentTag.TagID = CMS_Tag.TagID WHERE TagGroupID = @ID)) 
		OR (DocumentTagGroupID = @ID);
	
	-- Remove all bindings between documents and tags with specified tag group
	DELETE FROM CMS_DocumentTag WHERE TagID IN (SELECT TagID FROM CMS_Tag WHERE TagGroupID = @ID);
	-- Remove all tags with specified tag group
	DELETE FROM CMS_Tag WHERE TagGroupID = @ID;
	
END
