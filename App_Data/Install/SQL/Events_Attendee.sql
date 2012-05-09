CREATE TABLE [Events_Attendee] (
		[AttendeeID]               [int] IDENTITY(1, 1) NOT NULL,
		[AttendeeEmail]            [nvarchar](250) NOT NULL,
		[AttendeeFirstName]        [nvarchar](100) NULL,
		[AttendeeLastName]         [nvarchar](100) NULL,
		[AttendeePhone]            [nvarchar](50) NULL,
		[AttendeeEventNodeID]      [int] NOT NULL,
		[AttendeeGUID]             [uniqueidentifier] NOT NULL,
		[AttendeeLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [Events_Attendee]
	ADD
	CONSTRAINT [PK_Events_Attendee]
	PRIMARY KEY
	NONCLUSTERED
	([AttendeeID])
	
	
CREATE CLUSTERED INDEX [IX_Events_Attendee_AttendeeEmail_AttendeeFirstName_AttendeeLastName]
	ON [Events_Attendee] ([AttendeeEmail], [AttendeeFirstName], [AttendeeLastName])
	
	
CREATE NONCLUSTERED INDEX [IX_Events_Attendee_AttendeeEventNodeID]
	ON [Events_Attendee] ([AttendeeEventNodeID])
	
ALTER TABLE [Events_Attendee]
	WITH CHECK
	ADD CONSTRAINT [FK_Events_Attendee_AttendeeEventNodeID_CMS_Tree]
	FOREIGN KEY ([AttendeeEventNodeID]) REFERENCES [CMS_Tree] ([NodeID])
ALTER TABLE [Events_Attendee]
	CHECK CONSTRAINT [FK_Events_Attendee_AttendeeEventNodeID_CMS_Tree]
