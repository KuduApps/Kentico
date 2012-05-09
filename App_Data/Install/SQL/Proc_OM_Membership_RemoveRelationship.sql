CREATE PROCEDURE [Proc_OM_Membership_RemoveRelationship]
@MembershipID int,
  @ContactID int
AS
BEGIN
-- Load ContactID if not given
  IF (@ContactID=0)
    SELECT @ContactID=ActiveContactID FROM OM_Membership WHERE MembershipID=@MembershipID
    
  IF (@ContactID IS NOT NULL)
  BEGIN
    DECLARE @id int;
    BEGIN TRANSACTION t1
      DELETE FROM OM_Membership WHERE MembershipID=@MembershipID
      -- Check if any other relation exists (if not update "IsAnonymous" flag)
      SELECT TOP 1 @id=MembershipID FROM OM_Membership WHERE ActiveContactID=@ContactID AND MemberType=0
      IF (@id IS NULL) UPDATE OM_Contact SET ContactIsAnonymous=1 WHERE ContactID=@ContactID
    COMMIT TRANSACTION t1    
  END
END
