CREATE TABLE [COM_DepartmentTaxClass] (
		[DepartmentID]     [int] NOT NULL,
		[TaxClassID]       [int] NOT NULL
) 
ALTER TABLE [COM_DepartmentTaxClass]
	ADD
	CONSTRAINT [PK_COM_TaxClassDepartment]
	PRIMARY KEY
	CLUSTERED
	([DepartmentID], [TaxClassID])
	
	
ALTER TABLE [COM_DepartmentTaxClass]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxClassDepartment_DepartmentID_COM_Department]
	FOREIGN KEY ([DepartmentID]) REFERENCES [COM_Department] ([DepartmentID])
ALTER TABLE [COM_DepartmentTaxClass]
	CHECK CONSTRAINT [FK_COM_TaxClassDepartment_DepartmentID_COM_Department]
ALTER TABLE [COM_DepartmentTaxClass]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_TaxClassDepartment_TaxClassID_COM_TaxClass]
	FOREIGN KEY ([TaxClassID]) REFERENCES [COM_TaxClass] ([TaxClassID])
ALTER TABLE [COM_DepartmentTaxClass]
	CHECK CONSTRAINT [FK_COM_TaxClassDepartment_TaxClassID_COM_TaxClass]
