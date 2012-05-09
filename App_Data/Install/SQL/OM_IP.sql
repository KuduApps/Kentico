CREATE TABLE [OM_IP] (
		[IPID]                    [int] IDENTITY(1, 1) NOT NULL,
		[IPActiveContactID]       [int] NOT NULL,
		[IPOriginalContactID]     [int] NOT NULL,
		[IPAddress]               [nvarchar](100) NULL,
		[IPCreated]               [datetime] NOT NULL
) 
ALTER TABLE [OM_IP]
	ADD
	CONSTRAINT [PK_OM_IP]
	PRIMARY KEY
	CLUSTERED
	([IPID])
	
CREATE NONCLUSTERED INDEX [IX_OM_IP_IPActiveContactID]
	ON [OM_IP] ([IPActiveContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_IP_IPOriginalContactID]
	ON [OM_IP] ([IPOriginalContactID])
	
ALTER TABLE [OM_IP]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_IP_OM_Contact_Active]
	FOREIGN KEY ([IPActiveContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_IP]
	CHECK CONSTRAINT [FK_OM_IP_OM_Contact_Active]
ALTER TABLE [OM_IP]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_IP_OM_Contact_Original]
	FOREIGN KEY ([IPOriginalContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_IP]
	CHECK CONSTRAINT [FK_OM_IP_OM_Contact_Original]
