CREATE TABLE [COM_DiscountLevelDepartment] (
		[DiscountLevelID]     [int] NOT NULL,
		[DepartmentID]        [int] NOT NULL
) 
ALTER TABLE [COM_DiscountLevelDepartment]
	ADD
	CONSTRAINT [PK_COM_DiscountLevelDepartment]
	PRIMARY KEY
	CLUSTERED
	([DiscountLevelID], [DepartmentID])
	
	
ALTER TABLE [COM_DiscountLevelDepartment]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_DiscountLevelDepartment_DepartmentID_COM_Department]
	FOREIGN KEY ([DepartmentID]) REFERENCES [COM_Department] ([DepartmentID])
ALTER TABLE [COM_DiscountLevelDepartment]
	CHECK CONSTRAINT [FK_COM_DiscountLevelDepartment_DepartmentID_COM_Department]
ALTER TABLE [COM_DiscountLevelDepartment]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_DiscountLevelDepartment_DiscountLevelID_COM_DiscountLevel]
	FOREIGN KEY ([DiscountLevelID]) REFERENCES [COM_DiscountLevel] ([DiscountLevelID])
ALTER TABLE [COM_DiscountLevelDepartment]
	CHECK CONSTRAINT [FK_COM_DiscountLevelDepartment_DiscountLevelID_COM_DiscountLevel]
