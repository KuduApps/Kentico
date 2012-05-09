CREATE TABLE [Integration_Synchronization] (
		[SynchronizationID]               [int] IDENTITY(1, 1) NOT NULL,
		[SynchronizationTaskID]           [int] NOT NULL,
		[SynchronizationConnectorID]      [int] NOT NULL,
		[SynchronizationLastRun]          [datetime] NOT NULL,
		[SynchronizationErrorMessage]     [nvarchar](max) NULL,
		[SynchronizationIsRunning]        [bit] NULL
)  
ALTER TABLE [Integration_Synchronization]
	ADD
	CONSTRAINT [PK_Integration_Synchronization]
	PRIMARY KEY
	CLUSTERED
	([SynchronizationID])
	
CREATE NONCLUSTERED INDEX [IX_Integration_Synchronization_SynchronizationConnectorID]
	ON [Integration_Synchronization] ([SynchronizationConnectorID])
	
CREATE NONCLUSTERED INDEX [IX_Integration_Synchronization_SynchronizationTaskID]
	ON [Integration_Synchronization] ([SynchronizationTaskID])
	
ALTER TABLE [Integration_Synchronization]
	WITH CHECK
	ADD CONSTRAINT [FK_Integration_Synchronization_SynchronizationConnectorID_Integration_Connector]
	FOREIGN KEY ([SynchronizationConnectorID]) REFERENCES [Integration_Connector] ([ConnectorID])
ALTER TABLE [Integration_Synchronization]
	CHECK CONSTRAINT [FK_Integration_Synchronization_SynchronizationConnectorID_Integration_Connector]
ALTER TABLE [Integration_Synchronization]
	WITH CHECK
	ADD CONSTRAINT [FK_Integration_Synchronization_SynchronizationTaskID_Integration_Task]
	FOREIGN KEY ([SynchronizationTaskID]) REFERENCES [Integration_Task] ([TaskID])
ALTER TABLE [Integration_Synchronization]
	CHECK CONSTRAINT [FK_Integration_Synchronization_SynchronizationTaskID_Integration_Task]
