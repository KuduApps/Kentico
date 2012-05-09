-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_QueryProvider_InsertQuery]
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
    INSERT INTO [CMS_Query] (
        [QueryName],
        [QueryTypeID],
        [QueryText],
        [QueryRequiresTransaction],
        [ClassID],
        [QueryIsLocked],
        [QueryLastModified],
        [QueryGUID],
        [QueryLoadGeneration],
        [QueryIsCustom]
    )
    VALUES (
        @QueryName, 
        @QueryTypeID, 
        @QueryText, 
        @QueryRequiresTransaction, 
        @ClassID, 
        @QueryIsLocked,
        @QueryLastModified,
        @QueryGUID,
        @QueryLoadGeneration,
        @QueryIsCustom
    )
    SELECT SCOPE_IDENTITY()
END
