CREATE TABLE [CMS_Membership] (
		[MembershipID]               [int] IDENTITY(1, 1) NOT NULL,
		[MembershipName]             [nvarchar](200) NOT NULL,
		[MembershipDisplayName]      [nvarchar](200) NOT NULL,
		[MembershipDescription]      [nvarchar](max) NULL,
		[MembershipLastModified]     [datetime] NOT NULL,
		[MembershipGUID]             [uniqueidentifier] NOT NULL,
		[MembershipSiteID]           [int] NULL
)  
ALTER TABLE [CMS_Membership]
	ADD
	CONSTRAINT [PK_CMS_Membership]
	PRIMARY KEY
	CLUSTERED
	([MembershipID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Membership_MembershipSiteID]
	ON [CMS_Membership] ([MembershipSiteID])
	
ALTER TABLE [CMS_Membership]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Membership_MembershipSiteID_CMS_Site]
	FOREIGN KEY ([MembershipSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_Membership]
	CHECK CONSTRAINT [FK_CMS_Membership_MembershipSiteID_CMS_Site]
