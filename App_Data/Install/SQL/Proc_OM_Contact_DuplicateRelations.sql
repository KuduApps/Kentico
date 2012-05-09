CREATE PROCEDURE [Proc_OM_Contact_DuplicateRelations]
	@parentContactID int,
	@mergedContactID int,
	@mergedContactWhen datetime
AS
BEGIN
	SET NOCOUNT ON;
	-- Dupliacate IPs
	INSERT INTO OM_IP SELECT @mergedContactID AS IPActiveContactID, @mergedContactID AS IPOriginalContactID, IPAddress, IPCreated FROM OM_IP WHERE IPOriginalContactID = @parentContactID AND IPCreated > @mergedContactWhen AND IPAddress NOT IN (SELECT IPAddress FROM OM_IP WHERE IPOriginalContactID = @mergedContactID)
	-- Dupliacate UserAgents
	INSERT INTO OM_UserAgent SELECT UserAgentString, @mergedContactID AS UserAgentActiveContactID, @mergedContactID AS UserAgentOriginalContactID, UserAgentCreated FROM OM_UserAgent WHERE UserAgentOriginalContactID = @parentContactID AND UserAgentCreated > @mergedContactWhen AND UserAgentString NOT IN (SELECT UserAgentString FROM OM_UserAgent WHERE UserAgentOriginalContactID = @mergedContactID)
	-- Dupliacate Memberships
	INSERT INTO OM_Membership SELECT RelatedID, MemberType, MembershipGUID, MembershipCreated, @mergedContactID AS OriginalContactID, @mergedContactID AS ActiveContactID FROM OM_Membership WHERE OriginalContactID = @parentContactID AND MembershipCreated > @mergedContactWhen AND MembershipGUID NOT IN (SELECT MembershipGUID FROM OM_Membership WHERE OriginalContactID = @mergedContactID)
	
END
