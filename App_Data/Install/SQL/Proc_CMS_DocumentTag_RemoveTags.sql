CREATE PROCEDURE [Proc_CMS_DocumentTag_RemoveTags] 
	-- Add the parameters for the stored procedure here
	@DocumentID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Decrease count of all tags related to specified document
	UPDATE CMS_Tag SET CMS_Tag.TagCount = (CMS_Tag.TagCount - 1) FROM CMS_Tag INNER JOIN CMS_DocumentTag ON CMS_Tag.TagID = CMS_DocumentTag.TagID WHERE CMS_DocumentTag.DocumentID = @DocumentID;
	
	-- Remove all bindings between document and any of ist tags
	DELETE FROM CMS_DocumentTag WHERE DocumentID = @DocumentID;
	-- Remove all tags with count eqal to zero or lower
	DELETE FROM CMS_Tag WHERE TagCount <= 0;	
END
