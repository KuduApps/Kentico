CREATE TABLE [OM_MVTCombinationVariation] (
		[MVTCombinationID]     [int] NOT NULL,
		[MVTVariantID]         [int] NOT NULL
) 
ALTER TABLE [OM_MVTCombinationVariation]
	ADD
	CONSTRAINT [PK_OM_MVTCombinationVariation]
	PRIMARY KEY
	CLUSTERED
	([MVTCombinationID], [MVTVariantID])
	
ALTER TABLE [OM_MVTCombinationVariation]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_MVTCombinationVariation_OM_MVTCombination]
	FOREIGN KEY ([MVTCombinationID]) REFERENCES [OM_MVTCombination] ([MVTCombinationID])
ALTER TABLE [OM_MVTCombinationVariation]
	CHECK CONSTRAINT [FK_OM_MVTCombinationVariation_OM_MVTCombination]
ALTER TABLE [OM_MVTCombinationVariation]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_MVTCombinationVariation_OM_MVTVariant]
	FOREIGN KEY ([MVTVariantID]) REFERENCES [OM_MVTVariant] ([MVTVariantID])
ALTER TABLE [OM_MVTCombinationVariation]
	CHECK CONSTRAINT [FK_OM_MVTCombinationVariation_OM_MVTVariant]
