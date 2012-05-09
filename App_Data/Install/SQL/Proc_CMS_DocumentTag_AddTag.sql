CREATE PROCEDURE [Proc_CMS_DocumentTag_AddTag] 
	-- Add the parameters for the stored procedure here
	@TagName nvarchar(250),
	@TagGroupID int,
	@DocumentID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Initialize variables
	DECLARE @TagID int;
	-- Get the current tag ID
	SET @TagID = ( SELECT TagID FROM CMS_Tag WHERE TagName = @TagName AND TagGroupID = @TagGroupID );
    -- Update information on tag count
	IF (@TagID > 0)
		BEGIN
			UPDATE CMS_Tag SET TagCount = TagCount + 1 WHERE TagID = @TagID;
		END
	ELSE
		BEGIN
			INSERT INTO CMS_Tag (TagName, TagCount, TagGroupID) VALUES (@TagName, 1, @TagGroupID);			
			-- Get newly added tag ID
			SET @TagID = ( SELECT TagID FROM CMS_Tag WHERE TagName = @TagName AND TagGroupID = @TagGroupID );			
		END
	-- Insert binding for document and tag if necessary
	INSERT INTO CMS_DocumentTag (DocumentID, TagID) VALUES (@DocumentID, @TagID);
END
