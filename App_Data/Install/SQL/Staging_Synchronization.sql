CREATE TABLE [Staging_Synchronization] (
		[SynchronizationID]               [int] IDENTITY(1, 1) NOT NULL,
		[SynchronizationTaskID]           [int] NOT NULL,
		[SynchronizationServerID]         [int] NOT NULL,
		[SynchronizationLastRun]          [datetime] NULL,
		[SynchronizationErrorMessage]     [nvarchar](max) NULL
)  
ALTER TABLE [Staging_Synchronization]
	ADD
	CONSTRAINT [PK_Staging_Synchronization]
	PRIMARY KEY
	CLUSTERED
	([SynchronizationID])
	
	
CREATE NONCLUSTERED INDEX [IX_Staging_Synchronization_SynchronizationServerID]
	ON [Staging_Synchronization] ([SynchronizationServerID])
	
CREATE NONCLUSTERED INDEX [IX_Staging_Synchronization_SynchronizationTaskID]
	ON [Staging_Synchronization] ([SynchronizationTaskID])
	
ALTER TABLE [Staging_Synchronization]
	WITH CHECK
	ADD CONSTRAINT [FK_Staging_Synchronization_SynchronizationServerID_Staging_Server]
	FOREIGN KEY ([SynchronizationServerID]) REFERENCES [Staging_Server] ([ServerID])
ALTER TABLE [Staging_Synchronization]
	CHECK CONSTRAINT [FK_Staging_Synchronization_SynchronizationServerID_Staging_Server]
ALTER TABLE [Staging_Synchronization]
	WITH CHECK
	ADD CONSTRAINT [FK_Staging_Synchronization_SynchronizationTaskID_Staging_Task]
	FOREIGN KEY ([SynchronizationTaskID]) REFERENCES [Staging_Task] ([TaskID])
ALTER TABLE [Staging_Synchronization]
	CHECK CONSTRAINT [FK_Staging_Synchronization_SynchronizationTaskID_Staging_Task]
