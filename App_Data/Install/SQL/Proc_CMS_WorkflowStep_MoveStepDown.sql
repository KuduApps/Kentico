CREATE PROCEDURE [Proc_CMS_WorkflowStep_MoveStepDown]
	@StepID int
AS
BEGIN
	/* Get the workflow ID */
	DECLARE @WorkflowID int
	SET @WorkflowID = (SELECT StepWorkflowID FROM CMS_WorkflowStep WHERE StepID = @StepID);
	/* Check out whether the step does not want to move behind the published step */
	DECLARE @MaxStepOrder int
	SET @MaxStepOrder = (SELECT StepOrder FROM CMS_WorkflowStep WHERE StepWorkflowID = @WorkflowID AND StepName = 'published') - 1;
	/* Move the next step(s) up */
	UPDATE CMS_WorkflowStep SET StepOrder = StepOrder - 1 WHERE StepOrder = (SELECT StepOrder FROM CMS_WorkflowStep WHERE StepID = @StepID) + 1 AND StepWorkflowID = @WorkflowID AND StepName NOT IN ('edit', 'published', 'archived')
	/* Move the current step down */
	UPDATE CMS_WorkflowStep SET StepOrder = StepOrder + 1 WHERE StepID = @StepID AND StepOrder < @MaxStepOrder
END
