CREATE PROCEDURE [Proc_CMS_ACL_RemoveACLID] 
	@ACLIDs nvarchar(450),
	@SiteID int, 
	@NodeAliasPath nvarchar(450)
	
AS
BEGIN
	
	DECLARE @ACLInheritedACLs nvarchar(450);
	DECLARE @ACL int;
	DECLARE @ACLID nvarchar(15);
	DECLARE @InheritedACLsLength int;
	DECLARE @SplitChar char;
	DECLARE @Pos int;
	DECLARE @NewPos int;
	
	DECLARE @aclCursor CURSOR;
	SET @aclCursor = CURSOR FOR SELECT ACLInheritedACLs,ACLID FROM CMS_ACL WHERE CMS_ACL.ACLID IN (SELECT DISTINCT CMS_Tree.NodeACLID FROM CMS_Tree INNER JOIN CMS_ACL ON CMS_ACL.ACLID = CMS_Tree.NodeACLID WHERE NodeSiteID = @SiteID AND NodeAliasPath LIKE @NodeAliasPath)
	SET @SplitChar = ','
	SET @ACLIDs = ',' + @ACLIDs + ','
OPEN @aclCursor
FETCH NEXT FROM @aclCursor INTO @ACLInheritedACLs, @ACL
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @Pos = 1
	WHILE(@Pos IS NOT NULL AND @Pos != 0)
	BEGIN
		SET @NewPos = CHARINDEX(@SplitChar,@ACLIDs,@Pos)
		IF (@NewPos IS NOT NULL AND @NewPos != 0)
		BEGIN
			SET @ACLID = SUBSTRING(@ACLIDs,@Pos,@NewPos - @Pos)
			SET @ACLInheritedACLs = REPLACE(',' + @ACLInheritedACLs + ',', ',' +  @ACLID + ',', ',')
			SET @InheritedACLsLength = LEN(@ACLInheritedACLs)
			IF @InheritedACLsLength > 1
			BEGIN
				SET @ACLInheritedACLs = SUBSTRING(@ACLInheritedACLs, 2,@InheritedACLsLength - 2)
			END
			ELSE
			BEGIN
				SET @ACLInheritedACLs = ''
			END
			SET @Pos = @NewPos + 1			
		END
		ELSE
			SET @Pos = 0
    END
    UPDATE CMS_ACL SET ACLInheritedACLs = @ACLInheritedACLs WHERE ACLID = @ACL
    FETCH NEXT FROM @aclCursor INTO @ACLInheritedACLs, @ACL
END
CLOSE @aclCursor
DEALLOCATE @aclCursor
		
END
