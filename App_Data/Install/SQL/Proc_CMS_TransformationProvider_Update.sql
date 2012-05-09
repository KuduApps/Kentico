CREATE PROCEDURE [Proc_CMS_TransformationProvider_Update]
	@TransformationID int,
	@TransformationName nvarchar(100),
	@TransformationCode ntext,
	@TransformationType nvarchar(50),
	@TransformationClassID int,
	@TransformationCheckedOutByUserID int, 
	@TransformationCheckedOutMachineName nvarchar(100),
	@TransformationCheckedOutFilename nvarchar(450),
	@TransformationVersionGUID nvarchar(50),
	@TransformationGUID uniqueidentifier,
	@TransformationLastModified datetime,
	@TransformationIsHierarchical bit,
	@TransformationHierarchicalXML nvarchar (max),
	@TransformationCSS nvarchar (max)
AS
BEGIN
	UPDATE CMS_Transformation
	SET
		TransformationName = @TransformationName,
		TransformationCode = @TransformationCode,
		TransformationType = @TransformationType,
		TransformationClassID = @TransformationClassID,
		TransformationCheckedOutByUserID = @TransformationCheckedOutByUserID,
		TransformationCheckedOutMachineName = @TransformationCheckedOutMachineName,
		TransformationCheckedOutFilename = @TransformationCheckedOutFilename,
		TransformationVersionGUID = @TransformationVersionGUID,
		TransformationGUID = @TransformationGUID,
		TransformationLastModified = @TransformationLastModified,
		TransformationIsHierarchical = @TransformationIsHierarchical,
		TransformationHierarchicalXML = @TransformationHierarchicalXML,
		TransformationCSS = @TransformationCSS
	WHERE
		TransformationID = @TransformationID
END
