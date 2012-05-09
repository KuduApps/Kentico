CREATE PROCEDURE [Proc_CMS_WorkflowStep_RemoveDependencies]
	@ID int
AS
BEGIN
  -- Remove step roles
  DELETE FROM CMS_WorkflowStepRoles WHERE StepID = @ID;
  -- Clear the documents steps
  UPDATE CMS_Document SET DocumentWorkflowStepID = NULL WHERE DocumentWorkflowStepID = @ID;
  -- Clear step in workflow history
  UPDATE CMS_WorkflowHistory SET StepID = NULL WHERE StepID = @ID
  -- Clear step in version history
  UPDATE CMS_VersionHistory SET VersionWorkflowStepID = NULL WHERE VersionWorkflowStepID = @ID
END
