CREATE TABLE [Analytics_Index] (
		[IndexID]            [int] IDENTITY(1, 1) NOT NULL,
		[IndexZero]          [int] NULL,
		[IndexMonthName]     [nvarchar](50) NULL,
		[IndexDayName]       [nvarchar](50) NULL
) 
ALTER TABLE [Analytics_Index]
	ADD
	CONSTRAINT [PK_Analytics_Index]
	PRIMARY KEY
	NONCLUSTERED
	([IndexID])
	
	
CREATE CLUSTERED INDEX [IX_Analytics_Index_All]
	ON [Analytics_Index] ([IndexID], [IndexZero], [IndexMonthName], [IndexDayName])
	
	
