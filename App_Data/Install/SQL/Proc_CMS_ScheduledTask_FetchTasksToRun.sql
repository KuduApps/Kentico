CREATE PROCEDURE [Proc_CMS_ScheduledTask_FetchTasksToRun]
	@TaskSiteID int,
	@DateTime datetime,
	@TaskServerName nvarchar(100),
	@UseExternalService bit = null
AS
BEGIN
	DECLARE @TaskIDs TABLE (TaskID int);
	BEGIN TRAN
		IF (@TaskSiteID IS NOT NULL) AND (@UseExternalService = 0)
		/* Get tasks for specified site and global tasks that should be processed by application */
		BEGIN
			INSERT INTO @TaskIDs SELECT TaskID FROM CMS_ScheduledTask WHERE TaskNextRunTime IS NOT NULL AND TaskNextRunTime <= @DateTime AND TaskEnabled = 1
				AND (TaskSiteID = @TaskSiteID OR TaskSiteID IS NULL) AND (TaskServerName IS NULL OR TaskServerName = '' OR TaskServerName = @TaskServerName) AND (TaskUseExternalService = @UseExternalService OR TaskUseExternalService IS NULL);  
		END
		ELSE IF (@TaskSiteID IS NOT NULL) AND (@UseExternalService = 1)
		/* Get tasks for specified site and global tasks that should be processed by external service */
			BEGIN
				INSERT INTO @TaskIDs SELECT TaskID FROM CMS_ScheduledTask WHERE TaskNextRunTime IS NOT NULL AND TaskNextRunTime <= @DateTime AND TaskEnabled = 1
					AND (TaskSiteID = @TaskSiteID OR TaskSiteID IS NULL) AND (TaskServerName IS NULL OR TaskServerName = '' OR TaskServerName = @TaskServerName) AND (TaskUseExternalService = @UseExternalService);  
			END
		ELSE IF (@TaskSiteID IS NULL) AND (@UseExternalService = 1)
		/* Get sites and global tasks for external service (only for running sites) */
			BEGIN
				INSERT INTO @TaskIDs SELECT TaskID FROM CMS_ScheduledTask
				  WHERE TaskNextRunTime IS NOT NULL AND TaskNextRunTime <= @DateTime AND TaskEnabled = 1 AND (TaskSiteID IN (SELECT SiteID FROM CMS_Site WHERE SiteStatus = N'RUNNING') OR TaskSiteID IS NULL)
				  AND (TaskServerName IS NULL OR TaskServerName = '' OR TaskServerName = @TaskServerName) AND (TaskUseExternalService = @UseExternalService);  
			END
		ELSE IF (@TaskSiteID IS NOT NULL) AND (@UseExternalService IS NULL)
		/* Get tasks for specified site and global tasks */
			BEGIN
				INSERT INTO @TaskIDs SELECT TaskID FROM CMS_ScheduledTask WHERE TaskNextRunTime IS NOT NULL AND TaskNextRunTime <= @DateTime AND TaskEnabled = 1
					AND (TaskSiteID = @TaskSiteID OR TaskSiteID IS NULL) AND (TaskServerName IS NULL OR TaskServerName = '' OR TaskServerName = @TaskServerName);    
			END
	
	UPDATE CMS_ScheduledTask SET TaskNextRunTime = NULL WHERE TaskID IN (SELECT TaskID FROM @TaskIDs);
	COMMIT TRAN
	SELECT * FROM @TaskIDs;
END
