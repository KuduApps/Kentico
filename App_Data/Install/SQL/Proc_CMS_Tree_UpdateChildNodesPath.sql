CREATE PROCEDURE [Proc_CMS_Tree_UpdateChildNodesPath]
@OldAliasPath nvarchar(450), 
    @NewAliasPath nvarchar(450),
    @OldSiteID int, 
    @NewSiteID int,
    @NodeLevelChange int,
	@GenerateAliases bit
AS
BEGIN
/* Generate aliases for the child documents*/
IF @GenerateAliases = 1
BEGIN
  DECLARE @nodeCursor CURSOR;
  SET @nodeCursor = CURSOR FOR SELECT DISTINCT NodeID, NodeAliasPath, DocumentExtensions FROM View_CMS_Tree_Joined
    WHERE NodeAliasPath LIKE @OldAliasPath + '/%' AND NodeSiteID = @OldSiteID AND NOT EXISTS (SELECT * FROM CMS_DocumentAlias WHERE AliasNodeID = View_CMS_Tree_Joined.NodeID AND AliasURLPath = View_CMS_Tree_Joined.NodeAliasPath AND ((View_CMS_Tree_Joined.DocumentExtensions IS NULL AND AliasExtensions IS NULL) OR (AliasExtensions = View_CMS_Tree_Joined.DocumentExtensions)))
  DECLARE @currentNodeId int;
  DECLARE @currentNodeAliasPath nvarchar(450);
  DECLARE @currentNodeExtensions nvarchar(100);
  /* Loop through the cursor */
  OPEN @nodeCursor
  FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeAliasPath, @currentNodeExtensions;
  WHILE @@FETCH_STATUS = 0
  BEGIN
    /* Insert new alias for child document */
    INSERT INTO CMS_DocumentAlias ([AliasNodeID], [AliasCulture], [AliasURLPath], [AliasExtensions], [AliasCampaign], [AliasWildcardRule], [AliasPriority], [AliasGUID], [AliasLastModified], [AliasSiteID] )
    VALUES ( @currentNodeId, '', @currentNodeAliasPath, @currentNodeExtensions, '', '', 1, NEWID(), GETDATE(), @NewSiteID);
    FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeAliasPath, @currentNodeExtensions;
  END
  CLOSE @nodeCursor;
  DEALLOCATE @nodeCursor;
END;
/* Update the version history SiteID */
IF @OldSiteID <> @NewSiteID
    UPDATE CMS_VersionHistory SET NodeSiteID = @NewSiteID WHERE DocumentID IN
        (SELECT DocumentID FROM View_CMS_Tree_Joined WHERE NodeAliasPath LIKE @OldAliasPath + '/%' AND NodeSiteID = @OldSiteID);
/* Update the child nodes path */
UPDATE CMS_Tree SET 
    NodeAliasPath = @NewAliasPath + right(NodeAliasPath, LEN(NodeAliasPath) - LEN(@OldAliasPath)), 
    NodeSiteID = @NewSiteID,
    NodeLevel = NodeLevel + @NodeLevelChange
WHERE
    NodeAliasPath LIKE @OldAliasPath + '/%'
AND
    NodeSiteID = @OldSiteID
/* Update document aliases */
UPDATE CMS_DocumentAlias SET 
    AliasSiteID = @NewSiteID 
WHERE 
    AliasSiteID = @OldSiteID
/* Update document message boards */
UPDATE Board_Board SET 
    BoardSiteID = @NewSiteID 
WHERE 
    BoardSiteID = @OldSiteID
END
