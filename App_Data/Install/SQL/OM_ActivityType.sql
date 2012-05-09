CREATE TABLE [OM_ActivityType] (
		[ActivityTypeID]                        [int] IDENTITY(1, 1) NOT NULL,
		[ActivityTypeDisplayName]               [nvarchar](250) NOT NULL,
		[ActivityTypeName]                      [nvarchar](250) NOT NULL,
		[ActivityTypeEnabled]                   [bit] NULL,
		[ActivityTypeIsCustom]                  [bit] NULL,
		[ActivityTypeDescription]               [nvarchar](max) NULL,
		[ActivityTypeManualCreationAllowed]     [bit] NULL,
		[ActivityTypeMainFormControl]           [nvarchar](200) NULL,
		[ActivityTypeDetailFormControl]         [nvarchar](200) NULL
)  
ALTER TABLE [OM_ActivityType]
	ADD
	CONSTRAINT [PK_OM_ActivityType]
	PRIMARY KEY
	CLUSTERED
	([ActivityTypeID])
	
ALTER TABLE [OM_ActivityType]
	ADD
	CONSTRAINT [DEFAULT_OM_ActivityType_ActivityTypeEnabled]
	DEFAULT ((1)) FOR [ActivityTypeEnabled]
ALTER TABLE [OM_ActivityType]
	ADD
	CONSTRAINT [DEFAULT_OM_ActivityType_ActivityTypeIsCustom]
	DEFAULT ((1)) FOR [ActivityTypeIsCustom]
