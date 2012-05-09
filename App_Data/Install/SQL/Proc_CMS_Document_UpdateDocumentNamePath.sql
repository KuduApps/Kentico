CREATE PROCEDURE [Proc_CMS_Document_UpdateDocumentNamePath]
	@OriginalNodeID int,
	@SiteID int,
	@DefaultCultureCode nvarchar(10)
AS
BEGIN
	/* Get all starting paths to process */
	DECLARE @nodesTable TABLE (
		NodeAliasPath nvarchar(450)
	);
	
	/* Get the nodes list */
	INSERT INTO @nodesTable SELECT NodeAliasPath FROM CMS_Tree WHERE NodeID = @OriginalNodeID OR NodeLinkedNodeID = @OriginalNodeID ORDER BY NodeLevel ASC;
	/* Declare the cursor to loop through the table */
	DECLARE @nodeCursor CURSOR;
    SET @nodeCursor = CURSOR FOR SELECT NodeAliasPath FROM @nodesTable;
	
	DECLARE @startingAliasPath nvarchar(450)
	
	/* Loop through the table */
	OPEN @nodeCursor
	FETCH NEXT FROM @nodeCursor INTO @startingAliasPath;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Get the list of nodes to update */
		DECLARE @processDocuments TABLE (
				DocumentID int,
				NodeParentID int,
				DocumentCulture nvarchar(10),
				DocumentUseNamePathForUrlPath bit,
				DocumentUrlPath nvarchar(450)
		);
		INSERT INTO @processDocuments SELECT DocumentID, NodeParentID, DocumentCulture, DocumentUseNamePathForUrlPath, DocumentUrlPath FROM View_CMS_Tree_Joined WHERE NodeAliasPath LIKE @startingAliasPath + '/%' AND NodeSiteID = @SiteID AND NodeLinkedNodeID IS NULL ORDER BY NodeLevel ASC
		
		/* Loop through documents */
		DECLARE @documentId int;
		DECLARE @nodeParentId int;
		DECLARE @documentCulture nvarchar(10);
		DECLARE @useNamePathForUrlPath bit;
		DECLARE @documentUrlPath nvarchar(450);
		DECLARE @documentCursor CURSOR;
		SET @documentCursor = CURSOR FOR SELECT DocumentID, NodeParentID, DocumentCulture, DocumentUseNamePathForUrlPath, DocumentUrlPath FROM @processDocuments;
		
		OPEN @documentCursor
		FETCH NEXT FROM @documentCursor INTO @documentId, @nodeParentId, @documentCulture, @useNamePathForUrlPath, @documentUrlPath
		WHILE @@FETCH_STATUS = 0
		BEGIN
			/* Update the name path */
			DECLARE @parentNamePath nvarchar(1500);
			DECLARE @parentUseNamePathForUrlPath bit;
			DECLARE @parentUrlPath nvarchar(450);
			SELECT @parentNamePath = DocumentNamePath, @parentUseNamePathForUrlPath = DocumentUseNamePathForUrlPath, @parentUrlPath = DocumentUrlPath FROM
				(SELECT TOP 1 DocumentNamePath, DocumentUseNamePathForUrlPath, DocumentUrlPath FROM (
					(SELECT DocumentNamePath, DocumentUseNamePathForUrlPath, DocumentUrlPath, 1 AS Priority FROM View_CMS_Tree_Joined WHERE NodeID = @nodeParentId AND DocumentCulture = @documentCulture AND NodeAliasPath <> '/') UNION ALL 
					(SELECT DocumentNamePath, DocumentUseNamePathForUrlPath, DocumentUrlPath, 2 AS Priority FROM View_CMS_Tree_Joined WHERE NodeID = @nodeParentId AND DocumentCulture = @DefaultCultureCode AND NodeAliasPath <> '/') UNION ALL
					(SELECT DocumentNamePath, DocumentUseNamePathForUrlPath, DocumentUrlPath, 3 AS Priority FROM View_CMS_Tree_Joined WHERE NodeID = @nodeParentId AND NodeAliasPath <> '/') UNION ALL
					(SELECT '', 0, '', 4 AS Priority)
				) ParentPaths ORDER BY Priority ASC) TopParentPath;
		
			/* Update the URL path (parent URL path + last part of URL path */
			IF @useNamePathForUrlPath = 1 AND @parentUseNamePathForUrlPath = 1 AND @parentUrlPath <> '' AND  @documentUrlPath <> ''
			BEGIN
				SET @documentUrlPath = @parentUrlPath + '/' + RIGHT(@documentUrlPath, CHARINDEX('/', REVERSE(@documentUrlPath)) - 1);
			END;
			/* Update the document */
			UPDATE CMS_Document SET DocumentNamePath = @parentNamePath + '/' + DocumentName, DocumentUrlPath = @documentUrlPath WHERE DocumentID = @documentId
			
			FETCH NEXT FROM @documentCursor INTO @documentId, @nodeParentId, @documentCulture, @useNamePathForUrlPath, @documentUrlPath
		END
		CLOSE @documentCursor;
		DEALLOCATE @documentCursor;
		FETCH NEXT FROM @nodeCursor INTO @startingAliasPath;
	END
	CLOSE @nodeCursor;
	DEALLOCATE @nodeCursor;
END
