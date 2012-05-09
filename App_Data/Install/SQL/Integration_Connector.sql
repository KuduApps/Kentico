CREATE TABLE [Integration_Connector] (
		[ConnectorID]               [int] IDENTITY(1, 1) NOT NULL,
		[ConnectorName]             [nvarchar](100) NOT NULL,
		[ConnectorDisplayName]      [nvarchar](440) NOT NULL,
		[ConnectorAssemblyName]     [nvarchar](400) NOT NULL,
		[ConnectorClassName]        [nvarchar](400) NOT NULL,
		[ConnectorEnabled]          [bit] NOT NULL,
		[ConnectorLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [Integration_Connector]
	ADD
	CONSTRAINT [PK_Integration_Connector]
	PRIMARY KEY
	NONCLUSTERED
	([ConnectorID])
	
CREATE CLUSTERED INDEX [IX_Integration_Connector_ConnectorDisplayName]
	ON [Integration_Connector] ([ConnectorDisplayName])
	
CREATE NONCLUSTERED INDEX [IX_Integration_Connector_ConnectorEnabled]
	ON [Integration_Connector] ([ConnectorEnabled])
	
