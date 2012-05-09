CREATE TABLE [CMS_State] (
		[StateID]               [int] IDENTITY(1, 1) NOT NULL,
		[StateDisplayName]      [nvarchar](200) NOT NULL,
		[StateName]             [nvarchar](200) NOT NULL,
		[StateCode]             [nvarchar](100) NULL,
		[CountryID]             [int] NOT NULL,
		[StateGUID]             [uniqueidentifier] NOT NULL,
		[StateLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [CMS_State]
	ADD
	CONSTRAINT [PK_CMS_State]
	PRIMARY KEY
	NONCLUSTERED
	([StateID])
	
	
ALTER TABLE [CMS_State]
	ADD
	CONSTRAINT [DEFAULT_CMS_State_StateDisplayName]
	DEFAULT ('') FOR [StateDisplayName]
ALTER TABLE [CMS_State]
	ADD
	CONSTRAINT [DEFAULT_CMS_State_StateName]
	DEFAULT ('') FOR [StateName]
CREATE CLUSTERED INDEX [IX_CMS_State_CountryID_StateDisplayName]
	ON [CMS_State] ([StateDisplayName])
	
ALTER TABLE [CMS_State]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_State_CountryID_CMS_Country]
	FOREIGN KEY ([CountryID]) REFERENCES [CMS_Country] ([CountryID])
ALTER TABLE [CMS_State]
	CHECK CONSTRAINT [FK_CMS_State_CountryID_CMS_Country]
