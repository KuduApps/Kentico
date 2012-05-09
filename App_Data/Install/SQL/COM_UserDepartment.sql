CREATE TABLE [COM_UserDepartment] (
		[UserID]           [int] NOT NULL,
		[DepartmentID]     [int] NOT NULL
) 
ALTER TABLE [COM_UserDepartment]
	ADD
	CONSTRAINT [PK_COM_UserDepartment]
	PRIMARY KEY
	CLUSTERED
	([UserID], [DepartmentID])
	
	
ALTER TABLE [COM_UserDepartment]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_UserDepartment_DepartmentID_COM_Department]
	FOREIGN KEY ([DepartmentID]) REFERENCES [COM_Department] ([DepartmentID])
ALTER TABLE [COM_UserDepartment]
	CHECK CONSTRAINT [FK_COM_UserDepartment_DepartmentID_COM_Department]
ALTER TABLE [COM_UserDepartment]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_UserDepartment_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [COM_UserDepartment]
	CHECK CONSTRAINT [FK_COM_UserDepartment_UserID_CMS_User]
