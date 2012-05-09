CREATE PROCEDURE [Proc_CMS_WebFarmTask_Insert]
	@TaskType nvarchar(50), 
	@TaskTextData ntext, 
	@TaskCreated datetime,
	@TaskMachineName nvarchar(450),
	@TaskTarget nvarchar(450),
	@TaskBinary image,
	@TaskIsAnonymous bit,
	@TaskEnabled bit
AS
BEGIN
	INSERT INTO [CMS_WebFarmTask] ( [TaskType], [TaskTextData], [TaskBinaryData], [TaskCreated], [TaskEnabled], [TaskMachineName], [TaskTarget], [TaskIsAnonymous]) VALUES (  @TaskType, @TaskTextData, @TaskBinary, @TaskCreated, @TaskEnabled, @TaskMachineName, @TaskTarget, @TaskIsAnonymous); SELECT SCOPE_IDENTITY() AS [TaskID] 
END
