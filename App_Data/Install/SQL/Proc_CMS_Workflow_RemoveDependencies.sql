CREATE PROCEDURE [Proc_CMS_Workflow_RemoveDependencies]
	@ID int
AS
BEGIN
	  -- Remove step roles
  DELETE FROM CMS_WorkflowStepRoles WHERE StepID IN (SELECT StepID FROM CMS_WorkflowStep WHERE StepWorkflowID = @ID);
  -- Clear the documents steps
  UPDATE CMS_Document SET DocumentWorkflowStepID = NULL WHERE DocumentWorkflowStepID IN (SELECT StepID FROM CMS_WorkflowStep WHERE StepWorkflowID = @ID);
  -- Clear steps within workflow history
  UPDATE CMS_WorkflowHistory SET StepID = NULL WHERE StepID IN (SELECT StepID FROM CMS_WorkflowStep WHERE StepWorkflowID = @ID);
  -- Clear steps within version history
  UPDATE CMS_VersionHistory SET VersionWorkflowStepID = NULL WHERE VersionWorkflowStepID IN (SELECT StepID FROM CMS_WorkflowStep WHERE StepWorkflowID = @ID);
  -- Clear workflow information within version history
  UPDATE CMS_VersionHistory SET VersionWorkflowID = NULL WHERE VersionWorkflowID = @ID;
  -- Remove steps
  DELETE FROM CMS_WorkflowStep WHERE StepWorkflowID = @ID;
    -- Remove scopes
  DELETE FROM CMS_WorkflowScope WHERE ScopeWorkflowID = @ID;
END
