CREATE TABLE [COM_TaxClassCountry] (
		[TaxClassCountryID]     [int] IDENTITY(1, 1) NOT NULL,
		[TaxClassID]            [int] NOT NULL,
		[CountryID]             [int] NOT NULL,
		[TaxValue]              [float] NOT NULL,
		[IsFlatValue]           [bit] NOT NULL
) 
ALTER TABLE [COM_TaxClassCountry]
	ADD
	CONSTRAINT [PK_COM_TaxClassCountry]
	PRIMARY KEY
	CLUSTERED
	([TaxClassCountryID])
	
ALTER TABLE [COM_TaxClassCountry]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxCategoryCountry_CountryID_CMS_Country]
	FOREIGN KEY ([CountryID]) REFERENCES [CMS_Country] ([CountryID])
ALTER TABLE [COM_TaxClassCountry]
	CHECK CONSTRAINT [FK_COM_TaxCategoryCountry_CountryID_CMS_Country]
ALTER TABLE [COM_TaxClassCountry]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxCategoryCountry_TaxClassID_COM_TaxClass]
	FOREIGN KEY ([TaxClassID]) REFERENCES [COM_TaxClass] ([TaxClassID])
ALTER TABLE [COM_TaxClassCountry]
	CHECK CONSTRAINT [FK_COM_TaxCategoryCountry_TaxClassID_COM_TaxClass]
