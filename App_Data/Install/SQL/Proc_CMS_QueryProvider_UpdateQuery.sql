-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_QueryProvider_UpdateQuery] 
    @QueryID int,
    @QueryName nvarchar(100),
    @QueryTypeID int,
    @QueryText ntext,
    @QueryRequiresTransaction bit,
    @QueryIsLocked bit,
    @ClassID int,
    @QueryLastModified datetime,
    @QueryGUID uniqueidentifier,
    @QueryLoadGeneration int,
    @QueryIsCustom bit
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [CMS_Query]
    SET
        QueryName = @QueryName,
        QueryTypeID = @QueryTypeID,
        QueryText = @QueryText,
        QueryRequiresTransaction = @QueryRequiresTransaction,
        QueryIsLocked = @QueryIsLocked,
        ClassID = @ClassID,
        QueryLastModified = @QueryLastModified,
        QueryGUID = @QueryGUID,
        QueryLoadGeneration = @QueryLoadGeneration,
        QueryIsCustom = @QueryIsCustom
    WHERE 
        QueryID = @QueryID
END
