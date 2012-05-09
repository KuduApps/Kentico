CREATE TABLE [COM_TaxClassState] (
		[TaxClassStateID]     [int] IDENTITY(1, 1) NOT NULL,
		[TaxClassID]          [int] NOT NULL,
		[StateID]             [int] NOT NULL,
		[TaxValue]            [float] NOT NULL,
		[IsFlatValue]         [bit] NOT NULL
) 
ALTER TABLE [COM_TaxClassState]
	ADD
	CONSTRAINT [PK_COM_TaxClassState]
	PRIMARY KEY
	CLUSTERED
	([TaxClassStateID])
	
ALTER TABLE [COM_TaxClassState]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxClassState_StateID_CMS_State]
	FOREIGN KEY ([StateID]) REFERENCES [CMS_State] ([StateID])
ALTER TABLE [COM_TaxClassState]
	CHECK CONSTRAINT [FK_COM_TaxClassState_StateID_CMS_State]
ALTER TABLE [COM_TaxClassState]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxClassState_TaxClassID_COM_TaxClass]
	FOREIGN KEY ([TaxClassID]) REFERENCES [COM_TaxClass] ([TaxClassID])
ALTER TABLE [COM_TaxClassState]
	CHECK CONSTRAINT [FK_COM_TaxClassState_TaxClassID_COM_TaxClass]
