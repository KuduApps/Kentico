CREATE TABLE [OM_ScoreContactRule] (
		[ScoreID]        [int] NOT NULL,
		[ContactID]      [int] NOT NULL,
		[RuleID]         [int] NOT NULL,
		[Value]          [int] NOT NULL,
		[Expiration]     [datetime] NULL
) 
ALTER TABLE [OM_ScoreContactRule]
	ADD
	CONSTRAINT [PK_OM_ScoreContactRule]
	PRIMARY KEY
	CLUSTERED
	([ScoreID], [ContactID], [RuleID])
	
ALTER TABLE [OM_ScoreContactRule]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ScoreContactRule_OM_Contact]
	FOREIGN KEY ([ContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_ScoreContactRule]
	CHECK CONSTRAINT [FK_OM_ScoreContactRule_OM_Contact]
ALTER TABLE [OM_ScoreContactRule]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ScoreContactRule_OM_Rule]
	FOREIGN KEY ([RuleID]) REFERENCES [OM_Rule] ([RuleID])
ALTER TABLE [OM_ScoreContactRule]
	CHECK CONSTRAINT [FK_OM_ScoreContactRule_OM_Rule]
ALTER TABLE [OM_ScoreContactRule]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ScoreContactRule_OM_Score]
	FOREIGN KEY ([ScoreID]) REFERENCES [OM_Score] ([ScoreID])
ALTER TABLE [OM_ScoreContactRule]
	CHECK CONSTRAINT [FK_OM_ScoreContactRule_OM_Score]
