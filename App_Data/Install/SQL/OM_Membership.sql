CREATE TABLE [OM_Membership] (
		[MembershipID]          [int] IDENTITY(1, 1) NOT NULL,
		[RelatedID]             [int] NOT NULL,
		[MemberType]            [int] NOT NULL,
		[MembershipGUID]        [uniqueidentifier] NOT NULL,
		[MembershipCreated]     [datetime] NOT NULL,
		[OriginalContactID]     [int] NOT NULL,
		[ActiveContactID]       [int] NOT NULL
) 
ALTER TABLE [OM_Membership]
	ADD
	CONSTRAINT [PK_OM_Membership]
	PRIMARY KEY
	CLUSTERED
	([MembershipID])
	
ALTER TABLE [OM_Membership]
	ADD
	CONSTRAINT [DEFAULT_OM_Membership_MemberType]
	DEFAULT ((0)) FOR [MemberType]
CREATE NONCLUSTERED INDEX [IX_OM_Membership_ActiveContactID]
	ON [OM_Membership] ([ActiveContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Membership_OriginalContactID]
	ON [OM_Membership] ([OriginalContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Membership_RelatedID]
	ON [OM_Membership] ([RelatedID])
	
ALTER TABLE [OM_Membership]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Membership_OM_Contact]
	FOREIGN KEY ([OriginalContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_Membership]
	CHECK CONSTRAINT [FK_OM_Membership_OM_Contact]
ALTER TABLE [OM_Membership]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Membership_OM_Contact1]
	FOREIGN KEY ([ActiveContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_Membership]
	CHECK CONSTRAINT [FK_OM_Membership_OM_Contact1]
