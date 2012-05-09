CREATE PROCEDURE [Proc_OM_Contact_MoveRelations]
	@mergeIntoContactID int,
	@mergeFromContactID int
AS
BEGIN
	SET NOCOUNT ON;
		-- Move Activities into parent contact
    	UPDATE OM_Activity SET ActivityActiveContactID = @mergeIntoContactID WHERE ActivityActiveContactID = @mergeFromContactID AND ActivityGUID NOT IN (SELECT ActivityGUID FROM OM_Activity WHERE ActivityOriginalContactID = @mergeIntoContactID)
    	-- Move memberships 
    	UPDATE OM_Membership SET ActiveContactID = @mergeIntoContactID WHERE ActiveContactID = @mergeFromContactID AND RelatedID NOT IN (SELECT Parent.RelatedID FROM OM_Membership AS Parent WHERE Parent.OriginalContactID = @mergeIntoContactID AND Parent.MemberType = MemberType)
    	-- IPs
    	UPDATE OM_IP SET IPActiveContactID = @mergeIntoContactID WHERE IPActiveContactID = @mergeFromContactID AND IPAddress NOT IN (SELECT IPAddress FROM OM_IP WHERE IPOriginalContactID = @mergeIntoContactID)
    	-- User agents
    	UPDATE OM_UserAgent SET UserAgentActiveContactID = @mergeIntoContactID WHERE UserAgentActiveContactID = @mergeFromContactID AND UserAgentString NOT IN (SELECT UserAgentString FROM OM_UserAgent WHERE UserAgentOriginalContactID = @mergeIntoContactID)
END
