CREATE PROCEDURE [Proc_CMS_TransformationProvider_Insert]
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
    INSERT INTO CMS_Transformation (
		TransformationName,
		TransformationCode,
		TransformationType,
		TransformationClassID, 
		TransformationCheckedOutByUserID, 
		TransformationCheckedOutMachineName,
		TransformationCheckedOutFilename,
		TransformationVersionGUID,
		TransformationGUID,
		TransformationLastModified,
		TransformationIsHierarchical,
		TransformationHierarchicalXML,
		TransformationCSS
	) VALUES (
		@TransformationName,
		@TransformationCode,
		@TransformationType,
		@TransformationClassID,
		@TransformationCheckedOutByUserID, 
		@TransformationCheckedOutMachineName,
		@TransformationCheckedOutFilename,
		@TransformationVersionGUID,
		@TransformationGUID,
		@TransformationLastModified,
		@TransformationIsHierarchical,
		@TransformationHierarchicalXML,
		@TransformationCSS
	)
	SELECT SCOPE_IDENTITY()
END
