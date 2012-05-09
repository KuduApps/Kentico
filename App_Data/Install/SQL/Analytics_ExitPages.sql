CREATE TABLE [Analytics_ExitPages] (
		[SessionIdentificator]     [nvarchar](200) NOT NULL,
		[ExitPageNodeID]           [int] NOT NULL,
		[ExitPageLastModified]     [datetime] NOT NULL,
		[ExitPageSiteID]           [int] NOT NULL,
		[ExitPageCulture]          [nvarchar](10) NULL
) 
ALTER TABLE [Analytics_ExitPages]
	ADD
	CONSTRAINT [PK_Analytics_ExitPages]
	PRIMARY KEY
	CLUSTERED
	([SessionIdentificator])
	
