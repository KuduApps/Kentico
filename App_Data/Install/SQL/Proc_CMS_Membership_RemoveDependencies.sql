-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Membership_RemoveDependencies]
    @ID int
AS
BEGIN
    BEGIN TRANSACTION;
    
    DELETE  FROM CMS_MembershipRole WHERE MembershipID = @ID
    DELETE  FROM CMS_MembershipUser WHERE MembershipID = @ID
	
    
    COMMIT TRANSACTION;
END
