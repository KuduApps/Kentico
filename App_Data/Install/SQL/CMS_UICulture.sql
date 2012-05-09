CREATE TABLE [CMS_UICulture] (
		[UICultureID]               [int] IDENTITY(1, 1) NOT NULL,
		[UICultureName]             [nvarchar](200) NOT NULL,
		[UICultureCode]             [nvarchar](50) NOT NULL,
		[UICultureGUID]             [uniqueidentifier] NOT NULL,
		[UICultureLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [CMS_UICulture]
	ADD
	CONSTRAINT [PK_CMS_UICulture]
	PRIMARY KEY
	NONCLUSTERED
	([UICultureID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_UICulture_UICultureName]
	ON [CMS_UICulture] ([UICultureName])
	
	
