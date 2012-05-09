CREATE PROCEDURE [Proc_CMS_WorkflowStep_InitStepOrders]
	@StepWorkflowID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @stepsTable TABLE (
		StepID int NOT NULL,
		StepOrder int NOT NULL,
		StepName nvarchar(450) NOT NULL
	);
	
	/* Get the steps list (except for default steps) */
	INSERT INTO @stepsTable SELECT StepID, StepOrder, StepName FROM CMS_WorkflowStep WHERE StepWorkflowID = @StepWorkflowID AND StepName NOT IN (N'Edit', N'Published', N'Archived');
	
	/* Declare the cursor to loop through the table */
	DECLARE @stepCursor CURSOR;
    SET @stepCursor = CURSOR FOR SELECT StepID, StepOrder FROM @stepsTable ORDER BY StepOrder ASC, StepID ASC, StepName ASC;
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentStepOrder int;
	SET @currentIndex = 2;
	DECLARE @currentStepId int;
	/* Set the edit step as first */
	UPDATE CMS_WorkflowStep SET StepOrder = 1 WHERE StepWorkflowID = @StepWorkflowID AND StepName = N'Edit';
	
	/* Loop through the table */
	OPEN @stepCursor
	FETCH NEXT FROM @stepCursor INTO @currentStepId, @currentStepOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentStepOrder IS NULL OR @currentStepOrder <> @currentIndex
			UPDATE CMS_WorkflowStep SET StepOrder = @currentIndex WHERE StepID = @currentStepId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @stepCursor INTO @currentStepId, @currentStepOrder;
	END
	CLOSE @stepCursor;
	DEALLOCATE @stepCursor;
	/* Set the published step */
	UPDATE CMS_WorkflowStep SET StepOrder = @currentIndex WHERE StepWorkflowID = @StepWorkflowID AND StepName = N'Published';
	SET @currentIndex = @currentIndex + 1;
	/* Set the archived step */
	UPDATE CMS_WorkflowStep SET StepOrder = @currentIndex WHERE StepWorkflowID = @StepWorkflowID AND StepName = N'Archived';
END
