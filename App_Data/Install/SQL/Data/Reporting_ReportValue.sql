SET IDENTITY_INSERT [Reporting_ReportValue] ON
INSERT INTO [Reporting_ReportValue] ([ValueID], [ValueName], [ValueDisplayName], [ValueQuery], [ValueQueryIsStoredProcedure], [ValueFormatString], [ValueReportID], [ValueGUID], [ValueLastModified]) VALUES (3, N'UserCount', N'UserCount', N'select count(userid) from CMS_User', 0, N'', 142, '748d38d8-9f62-41df-b229-80280f94072b', '20080822 11:15:10')
INSERT INTO [Reporting_ReportValue] ([ValueID], [ValueName], [ValueDisplayName], [ValueQuery], [ValueQueryIsStoredProcedure], [ValueFormatString], [ValueReportID], [ValueGUID], [ValueLastModified]) VALUES (28, N'DocumentCount', N'DocumentCount', N'SELECT Count(NodeId)
FROM View_CMS_Tree_Joined
WHERE (@OnlyPublished = 0 OR Published = @OnlyPublished)  
AND (NodeSiteID = @CMSContextCurrentSiteID)
AND (@ModifiedFrom IS NULL OR DocumentModifiedWhen >= @ModifiedFrom)
AND (@ModifiedTo IS NULL OR DocumentModifiedWhen < @ModifiedTo) 
AND (NodeAliasPath LIKE @path)
AND (@Language IS NULL OR @Language = ''-1'' OR DocumentCulture = @Language)
AND (@name IS NULL OR DocumentName LIKE ''%''+@name+''%'')', 0, N'', 186, '526030e0-b87b-4f0c-bc2a-54d12e485a38', '20100304 12:08:18')
INSERT INTO [Reporting_ReportValue] ([ValueID], [ValueName], [ValueDisplayName], [ValueQuery], [ValueQueryIsStoredProcedure], [ValueFormatString], [ValueReportID], [ValueGUID], [ValueLastModified]) VALUES (88, N'SampleValue', N'Sample value', N'Select 20', 0, N'Report value: {0}', 361, '01ccf6f3-e3e9-4fc2-912b-8f0ae4cb19b7', '20101125 09:31:50')
SET IDENTITY_INSERT [Reporting_ReportValue] OFF
