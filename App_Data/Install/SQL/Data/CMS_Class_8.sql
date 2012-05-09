SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2697, N'Newsletter - SubscriberLink', N'newsletter.subscriberlink', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_SubscriberLink">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriberID" type="xs:int" />
              <xs:element name="LinkID" type="xs:int" />
              <xs:element name="Clicks" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_SubscriberLink" />
      <xs:field xpath="SubscriberID" />
      <xs:field xpath="LinkID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriberID" fieldcaption="SubscriberID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="e2789598-471a-4fb5-881b-38176c69a4d5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="LinkID" fieldcaption="LinkID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="4f3f2332-4984-49bd-bc1b-30a47c47c8e4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="Clicks" fieldcaption="Clicks" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="91b88752-c63f-4f1f-8888-97ab98344257" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'Newsletter_SubscriberLink', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111122 09:04:20', '558cd6c1-1683-4580-9e37-085cf874da0c', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2742, N'Project management - Project', N'PM.Project', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="PM_Project">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ProjectID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ProjectNodeID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ProjectName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ProjectDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ProjectStartDate" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ProjectDeadline" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ProjectOwner" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectCreatedByID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectStatusID" type="xs:int" />
              <xs:element name="ProjectSiteID" type="xs:int" />
              <xs:element name="ProjectGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ProjectLastModified" type="xs:dateTime" />
              <xs:element name="ProjectAllowOrdering" type="xs:boolean" minOccurs="0" />
              <xs:element name="ProjectAccess" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//PM_Project" />
      <xs:field xpath="ProjectID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ProjectID" fieldcaption="ProjectID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="2dcb0b1e-270d-488b-886c-f391ed84d0b2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ProjectNodeID" fieldcaption="ProjectNodeID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="c0d9dd33-f0e0-4a07-9217-e69f7076adc0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectGroupID" fieldcaption="ProjectGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="3d784ec1-83de-40dc-a740-ea1645c3c9a4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectDisplayName" fieldcaption="ProjectDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="d01a8b60-d3f7-4fb7-97ec-d8d1d8b5a1e8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectName" fieldcaption="ProjectName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ef316832-6dd1-49a9-8794-90a1efd466f5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectDescription" fieldcaption="ProjectDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42f7b825-fc5c-4656-ba99-f81a28b08157" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ProjectStartDate" fieldcaption="ProjectStartDate" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e5947c56-19be-45ec-a011-010101a2b8b5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>False</EditTime></settings></field><field column="ProjectDeadline" fieldcaption="ProjectDeadline" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="4f56eeed-6963-47ef-916c-ea94821f17dc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>False</EditTime></settings></field><field column="ProjectOwner" fieldcaption="ProjectOwner" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="bc978801-aa83-499c-a1ed-a9c47b44b10e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectCreatedByID" fieldcaption="ProjectCreatedByID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="fa6e7f1e-6956-4bc7-adfa-0443cdd981ac" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectStatusID" fieldcaption="ProjectStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="f852a6d8-7fe7-41ea-b174-7f4efa2b0b95" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectSiteID" fieldcaption="ProjectSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="6f2fdd13-74fc-4d4c-a005-45112ce6936d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectGUID" fieldcaption="ProjectGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="44942b2c-5045-4f9b-9114-c343f1d5b52a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ProjectLastModified" fieldcaption="ProjectLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="0d283a79-c53f-4b72-884f-459ed4b9afbf" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ProjectAllowOrdering" fieldcaption="ProjectAllowOrdering" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="ae52b009-2a0e-4e8a-9ba2-469c397f4b6b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ProjectAccess" fieldcaption="ProjectAccess" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="9f8419b6-35e2-4090-ba00-c6fc937991ae" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'PM_Project', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110325 10:21:19', '6abb39dc-0f10-47ee-890e-ea6e8c3a22a7', 0, 0, 0, N'', 0, N'ProjectID', N'0', N'', N'0', N'<search><item searchable="True" name="ProjectID" tokenized="False" content="False" id="b814d925-e0c4-417c-aa68-311354cc4013" /><item searchable="True" name="ProjectNodeID" tokenized="False" content="False" id="820ca7c6-ad5b-4719-be6e-9d964e2c9cd9" /><item searchable="True" name="ProjectGroupID" tokenized="False" content="False" id="16289fbb-3268-4370-ab1e-8f80a25f7017" /><item searchable="False" name="ProjectDisplayName" tokenized="True" content="True" id="c48d6ab3-7929-49f5-b355-874f48eb895d" /><item searchable="False" name="ProjectName" tokenized="True" content="True" id="8a46958f-fea4-4c32-beaa-015d730e814b" /><item searchable="False" name="ProjectDescription" tokenized="True" content="True" id="89ab10f4-5186-49c7-bad3-34806e09be1f" /><item searchable="True" name="ProjectStartDate" tokenized="False" content="False" id="67b15c06-c915-4d58-8a7a-d67331d1acac" /><item searchable="False" name="ProjectDeadline" tokenized="False" content="False" id="484442b3-6e2a-4683-a5d8-0d781fa67868" /><item searchable="True" name="ProjectOwner" tokenized="False" content="False" id="73023b49-f32f-4ab7-839d-2fdb4861f975" /><item searchable="False" name="ProjectCreatedByID" tokenized="False" content="False" id="6b3fe8c0-b8b8-47c4-8e42-41ed151605a7" /><item searchable="True" name="ProjectStatusID" tokenized="False" content="False" id="67f9de02-5afa-4a6f-9590-1bc3432d9982" /><item searchable="True" name="ProjectSiteID" tokenized="False" content="False" id="61c496a2-5b5d-43da-9c88-eb643acb15e4" /><item searchable="False" name="ProjectGUID" tokenized="False" content="False" id="0953ac78-263d-4256-a506-7e66f907d5d6" /><item searchable="True" name="ProjectLastModified" tokenized="False" content="False" id="fde146c1-10d5-483e-94a6-a13a8f4602de" /><item searchable="True" name="ProjectAllowOrdering" tokenized="False" content="False" id="2557d21b-2695-4dd2-af8a-f0db2718c762" /><item searchable="False" name="ProjectAccess" tokenized="False" content="False" id="930c7c63-9db5-4d01-b14e-74cdf8dc904e" /></search>', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2743, N'Project management - ProjectRolePermission', N'PM.ProjectRolePermission', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="PM_ProjectRolePermission">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ProjectID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="PermissionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//PM_ProjectRolePermission" />
      <xs:field xpath="ProjectID" />
	  <xs:field xpath="RoleID" />
	  <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ProjectID" fieldcaption="ProjectID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="0b384028-9d88-4e7c-a4e1-145fe42edebe" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="0171f9a3-4513-447b-be21-d50ca3af40b9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="00682bc8-5d11-41a7-b480-1cb7ada5ce37" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'PM_ProjectRolePermission', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 16:48:06', '7da9759d-710d-44e8-9d2d-e3ee5a6efee0', 0, 0, 0, N'', 0, N'RoleID', N'RoleID', N'', N'0', N'<search><item searchable="True" name="ProjectID" tokenized="False" content="True" id="69ef10e6-e59b-4139-8d34-f86ce5738d81" /><item searchable="True" name="RoleID" tokenized="False" content="False" id="9bafef12-d273-4020-9dbc-2624fba4efb8" /><item searchable="True" name="PermissionID" tokenized="False" content="False" id="d68f1596-dbdb-422f-8aa9-82a12a77366c" /></search>', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2744, N'Project management - Project status', N'PM.ProjectStatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="PM_ProjectStatus">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StatusID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="StatusName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatusDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatusOrder" type="xs:int" />
              <xs:element name="StatusColor" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="7" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatusIcon" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatusGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="StatusLastModified" type="xs:dateTime" />
              <xs:element name="StatusEnabled" type="xs:boolean" />
              <xs:element name="StatusIsFinished" type="xs:boolean" />
              <xs:element name="StatusIsNotStarted" type="xs:boolean" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//PM_ProjectStatus" />
      <xs:field xpath="StatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StatusID" fieldcaption="StatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="084b271c-e7f6-4283-8d98-de08d2284890" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="StatusName" fieldcaption="Status name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="23a9e9f2-8008-4842-a1f4-492360193f99" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusDisplayName" fieldcaption="Status display name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2abfc7a0-efe1-4209-88f4-701afc9cbcee" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusOrder" fieldcaption="Status order" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e9ae3f7c-4127-4862-8de7-575e34613af3" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusColor" fieldcaption="Status color" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="7" publicfield="false" spellcheck="false" guid="49e24dba-0ca8-4dc8-8afe-926cd045f796" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusIcon" fieldcaption="Status icon" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="false" guid="c93c8ddc-3555-4a74-8f0f-6ad73ff429e7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>imageselectioncontrol</controlname><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="StatusGUID" fieldcaption="Status GUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="7a85f333-903f-4317-9e1e-8f36f610ed92" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="StatusLastModified" fieldcaption="Status last modified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="24f013e0-374a-4a09-b35e-3051f74441b5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>False</EditTime></settings></field><field column="StatusEnabled" fieldcaption="Status enabled" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="10599945-4baa-4b2b-afeb-fe67c0d1aff5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="StatusIsFinished" fieldcaption="Status is finished" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="d73fc374-492a-4bf2-bb40-eddcca633a6a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="StatusIsNotStarted" fieldcaption="Status is not started" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="80beea5e-8da0-4575-87fa-692650e19050" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'PM_ProjectStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:56:14', 'feaf428a-42c3-4eda-94b9-b5589102855a', 0, 0, 0, N'', 0, N'StatusDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="StatusID" tokenized="False" content="False" id="52ada4ab-d7ed-4d3d-9596-f73dc4d899db" /><item searchable="False" name="StatusName" tokenized="True" content="True" id="aea88fe0-a705-41ce-85c9-1d540ca21921" /><item searchable="False" name="StatusDisplayName" tokenized="True" content="True" id="bbed6654-5743-401a-9719-04024f25c102" /><item searchable="True" name="StatusOrder" tokenized="False" content="False" id="e0580c82-c64c-4d8c-8897-53cde04ab848" /><item searchable="False" name="StatusColor" tokenized="True" content="True" id="9f6f4c84-a8f9-449a-b5db-9f6b850e975d" /><item searchable="False" name="StatusIcon" tokenized="True" content="True" id="48cac1ea-b72c-4c74-a1ea-9474f028835b" /><item searchable="False" name="StatusGUID" tokenized="False" content="False" id="d8f94008-10c9-4247-bac4-8eed259f4a19" /><item searchable="True" name="StatusLastModified" tokenized="False" content="False" id="0962ebc0-4d4d-4782-8a26-21c5609135f5" /><item searchable="True" name="StatusEnabled" tokenized="False" content="False" id="be66dc60-6bee-4898-9cc5-133f6ecc484e" /><item searchable="True" name="StatusIsFinished" tokenized="False" content="False" id="4cb44776-3f9b-4732-9ee9-709b0ff3d505" /><item searchable="True" name="StatusIsNotStarted" tokenized="False" content="False" id="3c370bbd-e014-418a-9270-ebd80f4cf3bd" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2745, N'Project management - Project task', N'PM.ProjectTask', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="PM_ProjectTask">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ProjectTaskID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ProjectTaskProjectID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectTaskDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ProjectTaskDeadline" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ProjectTaskStatusID" type="xs:int" />
              <xs:element name="ProjectTaskPriorityID" type="xs:int" />
              <xs:element name="ProjectTaskDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ProjectTaskOwnerID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectTaskCreatedByID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectTaskAssignedToUserID" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectTaskProgress" type="xs:int" />
              <xs:element name="ProjectTaskHours" type="xs:double" />
              <xs:element name="ProjectTaskGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ProjectTaskLastModified" type="xs:dateTime" />
              <xs:element name="ProjectTaskProjectOrder" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectTaskUserOrder" type="xs:int" minOccurs="0" />
              <xs:element name="ProjectTaskNotificationSent" type="xs:boolean" minOccurs="0" />
              <xs:element name="ProjectTaskIsPrivate" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//PM_ProjectTask" />
      <xs:field xpath="ProjectTaskID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ProjectTaskID" fieldcaption="ProjectTaskID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="840cf915-7882-487e-9a51-6805137f8e61" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ProjectTaskProjectID" fieldcaption="Project task project ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="f9183aba-1a51-4710-8b36-085487d3f499" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskDisplayName" fieldcaption="Project task display name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="0286877d-3227-4079-83f0-aaa08988519b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskDeadline" fieldcaption="Project task due date" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="6e9a5e49-aa8d-4991-84de-36b3ffb41f9a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="ProjectTaskStatusID" fieldcaption="Project task status ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="2e6350fd-6e44-435e-84e6-cd844e4d44c1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskPriorityID" fieldcaption="Project task priority ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="36698c97-01c7-431e-9a6d-1ea69317fc5a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskDescription" fieldcaption="Project task description" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7968d38b-3584-4c53-9ec5-c22f7538c488" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ProjectTaskOwnerID" fieldcaption="Project task owner ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="4e1c6c51-dbbf-4bcc-a6c5-1a32809e7e0a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskCreatedByID" fieldcaption="Project task created by ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="06b73593-e41e-400b-859f-e6a2be9ae629" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskAssignedToUserID" fieldcaption="Project task assigned to user ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e1d09d3d-498b-470f-82ee-4b8c96c926e4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskProgress" fieldcaption="Project task progress" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="9693d535-9662-4b46-99fd-a340dccfe93b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskHours" fieldcaption="Project task hours" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="4dbf52d4-3778-4aa5-a0d3-f26fc3f00dca" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskGUID" fieldcaption="Project task GUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="5f398615-d2c9-4542-adcf-72ed81941aaa" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ProjectTaskLastModified" fieldcaption="Project task last modified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="8b4f065c-c2b2-467b-b812-f2b23ab4145f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>False</EditTime></settings></field><field column="ProjectTaskProjectOrder" fieldcaption="Project task project order" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="86e8e239-fdff-4741-a887-b022c6a2a885" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskUserOrder" fieldcaption="Project task user order" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="7bb12625-709a-4ce0-a461-c0eb5a822d88" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ProjectTaskNotificationSent" fieldcaption="Project task notification sent" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="39ff078e-d8b5-4110-9168-bb7bb91a2b35" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ProjectTaskIsPrivate" fieldcaption="Project task is private" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="f03a065c-fc29-4920-bf1a-c88bb6df1783" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'PM_ProjectTask', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110311 13:54:42', '99e7fdfe-e7ba-4076-8580-20e0a2eb787b', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2746, N'Project management - Project task priority', N'PM.ProjectTaskPriority', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="PM_ProjectTaskPriority">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaskPriorityID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaskPriorityName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskPriorityDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskPriorityOrder" type="xs:int" minOccurs="0" />
              <xs:element name="TaskPriorityGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TaskPriorityLastModified" type="xs:dateTime" />
              <xs:element name="TaskPriorityEnabled" type="xs:boolean" />
              <xs:element name="TaskPriorityDefault" type="xs:boolean" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//PM_ProjectTaskPriority" />
      <xs:field xpath="TaskPriorityID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaskPriorityID" fieldcaption="Task priority ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="1d026ecb-6cc4-4e50-a63a-7c6fa914c335" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskPriorityName" fieldcaption="Task priority name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2a59e5cf-e2fe-4044-bf93-6ebdb81f98b4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskPriorityDisplayName" fieldcaption="Task priority display name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="cf29601b-ef9e-462d-a5a1-53cd5737c904" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskPriorityOrder" fieldcaption="Task priority order" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="94996418-c650-432a-9e7b-6994c52447f9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskPriorityGUID" fieldcaption="Task priority GUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="27d19753-73f9-4ade-97ae-ef77d50edb29" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskPriorityLastModified" fieldcaption="Task priority last modified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="cf2fab4b-0893-44ab-be4c-5a1651de8b1a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>False</EditTime></settings></field><field column="TaskPriorityEnabled" fieldcaption="Task priority enabled" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="331d5c9b-413d-4779-ab02-6896bcc61d06" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskPriorityDefault" fieldcaption="Task priority default" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="7d63354b-20bd-48eb-9f45-29242f540e47" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'PM_ProjectTaskPriority', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:56:00', '31d1d341-49de-4add-9ec9-dd846b0d04f7', 0, 0, 0, N'', 0, N'TaskPriorityDisplayName', N'0', N'', N'TaskPriorityLastModified', N'<search><item searchable="True" name="TaskPriorityID" tokenized="False" content="False" id="0995473c-2522-4f5a-bd4d-ae644b6da327" /><item searchable="False" name="TaskPriorityName" tokenized="True" content="True" id="8ca3d055-20a7-4567-b20c-61cb2406ba96" /><item searchable="False" name="TaskPriorityDisplayName" tokenized="True" content="True" id="30a859cc-c9c7-43f5-8e19-da65ca919db7" /><item searchable="True" name="TaskPriorityOrder" tokenized="False" content="False" id="860d6ad5-5a95-4860-9b66-06ebbd010114" /><item searchable="False" name="TaskPriorityGUID" tokenized="False" content="False" id="c0a372d0-792b-4027-b803-46697011f7fc" /><item searchable="True" name="TaskPriorityLastModified" tokenized="False" content="False" id="5c6da448-3fb2-4653-8a9f-c55d539ee529" /><item searchable="True" name="TaskPriorityEnabled" tokenized="False" content="False" id="100b85a9-db4b-457f-961f-7f161754c704" /><item searchable="True" name="TaskPriorityDefault" tokenized="False" content="False" id="8bfd56cd-7df7-4b20-8ee8-fdea221cc983" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2747, N'Project management - Project task status', N'PM.ProjectTaskStatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="PM_ProjectTaskStatus">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaskStatusID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaskStatusName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskStatusDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskStatusOrder" type="xs:int" />
              <xs:element name="TaskStatusColor" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="7" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskStatusIcon" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskStatusGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TaskStatusLastModified" type="xs:dateTime" />
              <xs:element name="TaskStatusEnabled" type="xs:boolean" />
              <xs:element name="TaskStatusIsNotStarted" type="xs:boolean" />
              <xs:element name="TaskStatusIsFinished" type="xs:boolean" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//PM_ProjectTaskStatus" />
      <xs:field xpath="TaskStatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaskStatusID" fieldcaption="Task status ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="false" guid="ba554702-ff87-48a5-b227-74e2df8a1a43" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskStatusName" fieldcaption="Task status name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="26ee6dd6-f087-4ada-9076-7eac9d6496fb" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskStatusDisplayName" fieldcaption="Status display name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="0431ddc2-d1d7-442b-9130-5a732839f657" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskStatusOrder" fieldcaption="Task status order" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="c6ab0bae-b4cc-43a8-a72c-488d0b4f816e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskStatusColor" fieldcaption="Task status color" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="7" publicfield="false" spellcheck="false" guid="27887a30-07ca-45e8-93d7-97d9be3c0e8a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskStatusIcon" fieldcaption="Task status icon" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="false" guid="b306f7f2-3bc6-4189-8bc3-80a2fbcb8344" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>imageselectioncontrol</controlname><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="TaskStatusGUID" fieldcaption="Task status GUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="2c5aa008-2d11-4859-aa0f-9862cdf63dec" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskStatusLastModified" fieldcaption="Task status last modified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="02a69f40-b0d7-4f8c-bcbb-40f4f43b43d7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>False</EditTime></settings></field><field column="TaskStatusEnabled" fieldcaption="Task status enabled" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="022a2153-fd01-4972-9265-e2a2fcfb64e0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskStatusIsNotStarted" fieldcaption="Task status is not started" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="300eaadf-06ac-48f8-8b73-da7501a02223" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskStatusIsFinished" fieldcaption="Task status is finished" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="b274de8f-6640-4ada-aacc-64d5197bd37c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'PM_ProjectTaskStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:55:52', '831a75ef-3f16-4d6d-9694-2e29d896f21f', 0, 0, 0, N'', 0, N'TaskStatusDisplayName', N'0', N'', N'TaskStatusLastModified', N'<search><item searchable="True" name="TaskStatusID" tokenized="False" content="False" id="55083d34-90c3-4d4b-b7b4-4138056cfabd" /><item searchable="False" name="TaskStatusName" tokenized="True" content="True" id="520b2634-233e-4ade-95f6-c2c82a7c727e" /><item searchable="False" name="TaskStatusDisplayName" tokenized="True" content="True" id="4213f304-1600-4cc0-b0aa-a5c4ba85a548" /><item searchable="True" name="TaskStatusOrder" tokenized="False" content="False" id="101b170a-690d-4cab-8cf6-d3d8db332190" /><item searchable="False" name="TaskStatusColor" tokenized="True" content="True" id="65da0c13-5819-43a6-a29f-0de578c279aa" /><item searchable="False" name="TaskStatusIcon" tokenized="True" content="True" id="2863f0df-67ef-44e3-b658-72104a7ea67c" /><item searchable="False" name="TaskStatusGUID" tokenized="False" content="False" id="ba46fa9c-fac9-4169-8bb6-d044e8219554" /><item searchable="True" name="TaskStatusLastModified" tokenized="False" content="False" id="3e9cfc0f-cc09-4f60-aa9c-4263d8fa015b" /><item searchable="True" name="TaskStatusEnabled" tokenized="False" content="False" id="d68a948a-44d1-47e3-9caa-e5bdd5be313d" /><item searchable="True" name="TaskStatusIsNotStarted" tokenized="False" content="False" id="d2e84080-cd47-444b-bb0c-ae8bb32824e6" /><item searchable="True" name="TaskStatusIsFinished" tokenized="False" content="False" id="e1b48266-8bc1-4de0-b530-a612859ab313" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2785, N'Membership', N'CMS.Membership', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Membership">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MembershipID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MembershipName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MembershipDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MembershipDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MembershipLastModified" type="xs:dateTime" />
              <xs:element name="MembershipGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MembershipSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Membership" />
      <xs:field xpath="MembershipID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MembershipID" fieldcaption="MembershipID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="82ca12fe-f665-4bac-91f0-4cefaf6a1b58" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MembershipName" fieldcaption="Membership name" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="02a50016-72bb-4c2b-b3b1-c29b8df0efcc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MembershipDisplayName" fieldcaption="Membership display name" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="9741ab58-eafd-4dac-9cd5-d1c5a9dd0e8a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MembershipDescription" fieldcaption="Membership description" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="53113c46-b7b9-4d1b-961b-057084ed7789" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MembershipSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e24f1e3-c8b9-4bf7-ac39-06d892c7825d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="MembershipLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c260fe5f-edb0-4868-a3e4-0262b45c652c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="MembershipGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a874b04b-d2fb-4ecc-9a7c-84b3af3efd0b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Membership', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110922 10:38:52', 'b9a0da7c-f86f-4d25-baec-5f612b480b77', 0, 0, 0, N'', 0, N'MembershipDisplayName', N'MembershipDescription', N'', N'MembershipLastModified', N'<search><item searchable="True" name="MembershipID" tokenized="False" content="False" id="3dc538d1-adc4-4b36-8cf6-eb76fbc5b37e" /><item searchable="False" name="MembershipName" tokenized="True" content="True" id="9584ec11-919f-463e-9dd4-38d0091ee1bb" /><item searchable="False" name="MembershipDisplayName" tokenized="True" content="True" id="0c55d843-d9cd-4752-8dbf-9c31215d84b5" /><item searchable="False" name="MembershipDescription" tokenized="True" content="True" id="484f00c2-c380-409b-9d08-5535f49054ce" /><item searchable="True" name="MembershipSiteID" tokenized="False" content="False" id="275090db-beb5-48ec-92a6-88982903dbfe" /><item searchable="True" name="MembershipLastModified" tokenized="False" content="False" id="20a33a0c-7285-4acc-afac-42d21d17411f" /><item searchable="False" name="MembershipGUID" tokenized="False" content="False" id="897ba080-2f33-4ffd-8cd8-66c74c1e47c0" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2804, N'SMTP server', N'CMS.SMTPServer', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SMTPServer">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ServerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ServerName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerUserName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerPassword" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerUseSSL" type="xs:boolean" />
              <xs:element name="ServerEnabled" type="xs:boolean" />
              <xs:element name="ServerIsGlobal" type="xs:boolean" />
              <xs:element name="ServerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ServerLastModified" type="xs:dateTime" />
              <xs:element name="ServerPriority" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SMTPServer" />
      <xs:field xpath="ServerID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ServerID" fieldcaption="ServerID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="c331e619-6707-4b28-83f5-0dc06ab3b1fc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ServerName" fieldcaption="{$smtpserver_edit.servernamelabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="false" guid="378adb50-4930-4f66-bab8-ed5b9663ea8a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="ServerUserName" fieldcaption="{$smtpserver_edit.serverusernamelabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="false" guid="e2eea375-809a-4875-b70f-4275d4d6ac63" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="ServerPassword" fieldcaption="{$smtpserver_edit.serverpasswordlabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="false" guid="3e6aa6f0-4b4c-4ddd-8ef3-5266c26c8b4b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ServerUseSSL" fieldcaption="{$smtpserver_edit.serverusessllabel$}" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="73f0a42d-78ad-4d62-981a-403f29295f56" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ServerEnabled" fieldcaption="{$smtpserver_edit.serverenabledlabel$}" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0b471c96-3f09-480e-92fa-d110d7602889" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ServerPriority" fieldcaption="ServerPriority" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="85b25e3a-728f-4583-bc16-cdf1f30cf7f1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ServerIsGlobal" fieldcaption="{$smtpserver_edit.serverisgloballabel$}" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="274aa6e5-bdb1-4fa8-ac44-0c817c89a832" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ServerGUID" fieldcaption="ServerLastModified" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e8d4e762-d3a1-4937-9e92-878fc56d141d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ServerLastModified" fieldcaption="ServerLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6be76690-b010-4c8b-ad84-8a8d76e62602" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field></form>', N'', N'', N'', N'CMS_SMTPServer', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110818 14:29:55', '21fdc065-df67-473b-b859-e5e0fc51b60b', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2805, N'SMTPServerSite', N'CMS.SMTPServerSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SMTPServerSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ServerID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SMTPServerSite" />
      <xs:field xpath="ServerID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ServerID" fieldcaption="ServerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f843e190-91f3-4716-84cc-81fca71017d7" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="80c45591-cb2c-4606-b2f4-c1e94bdb99cd" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_SMTPServerSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110217 14:59:15', '87288e5a-17f4-4041-a950-c3adf9b7e51e', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2806, N'MVT - Test', N'OM.MVTest', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_MVTest">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MVTestID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MVTestName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTestDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTestPage">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTestSiteID" type="xs:int" />
              <xs:element name="MVTestCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTestOpenFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="MVTestOpenTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="MVTestMaxConversions" type="xs:int" minOccurs="0" />
              <xs:element name="MVTestConversions" type="xs:int" minOccurs="0" />
              <xs:element name="MVTestTargetConversionType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTestGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MVTestLastModified" type="xs:dateTime" />
              <xs:element name="MVTestEnabled" type="xs:boolean" />
              <xs:element name="MVTestDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_MVTest" />
      <xs:field xpath="MVTestID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MVTestID" fieldcaption="MVTestID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="3b65d24f-95d9-4a7b-a832-731c108e846a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MVTestName" fieldcaption="MVTest name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="false" guid="345a8b88-c067-425f-b97d-3e10fd5787bf" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="MVTestDisplayName" fieldcaption="MVTestDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="59476b0e-2174-4fda-95b8-bc221c9185ec" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="MVTestDescription" fieldcaption="MVTestDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="35551e56-593f-40a0-9c75-2cd040b4f461" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestPage" fieldcaption="MVTestPage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="4abe295b-00f6-4fce-b3b2-4d505d4e21a3" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestSiteID" fieldcaption="MVTestSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c8ac16a7-3514-4c05-87e3-a1ffdadb4534" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestCulture" fieldcaption="MVTestCulture" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="f4e6c039-af2a-4368-8926-05b7024575ab" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestOpenFrom" fieldcaption="MVTestOpenFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f6b36279-35fa-49ee-90e8-6a4a0ac56723" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="MVTestOpenTo" fieldcaption="MVTestOpenTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="71b1b34a-8511-43c7-a01f-2bd232a47cdd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="MVTestMaxConversions" fieldcaption="MVTestMaxConversions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9d75f02c-8481-4ca5-bc42-00eb5a4d30c1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestConversions" fieldcaption="MVTestConversions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e0689f1-52d7-4dc8-b3a9-52dbaa57c65a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestTargetConversionType" fieldcaption="MVTestTargetConversionType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="d5c245a3-1dcd-41cb-afe8-e4d1695eba6e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestGUID" fieldcaption="MVTestGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c98038b3-38a6-4d3c-bfc6-52aaa47e55e1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTestLastModified" fieldcaption="MVTestLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="39cd459e-5fbf-4d72-968b-2e2e654e3f88" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="MVTestEnabled" fieldcaption="MVTestEnabled" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8dcc291a-11be-4ccf-8cb0-fb3ef59836f9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_MVTest', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110914 16:38:01', 'fca8a7a7-ff70-4a2e-8f5e-fd415978f28c', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2807, N'MVT - Variant', N'OM.MVTVariant', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_MVTVariant">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MVTVariantID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MVTVariantName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTVariantDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTVariantInstanceGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="MVTVariantZoneID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTVariantPageTemplateID" type="xs:int" />
              <xs:element name="MVTVariantEnabled" type="xs:boolean" />
              <xs:element name="MVTVariantWebParts" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTVariantGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MVTVariantLastModified" type="xs:dateTime" />
              <xs:element name="MVTVariantDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTVariantDocumentID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_MVTVariant" />
      <xs:field xpath="MVTVariantID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MVTVariantID" fieldcaption="MVTVariantID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a5715b9a-a841-4dfc-925e-82a53b4e4714" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MVTVariantDisplayName" fieldcaption="{$general.displayname$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="9cc775f7-894f-4fcd-9aab-4c219331533b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>localizabletextbox</controlname></settings></field><field column="MVTVariantName" fieldcaption="{$general.codename$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="c51a03a2-64fb-4dbd-bdf0-9dd607db3efe" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="MVTVariantDescription" fieldcaption="{$general.description$}" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8d1824e3-6d91-4cb1-9a42-9b1cded417ad" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><IsTextArea>True</IsTextArea><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="MVTVariantInstanceGUID" fieldcaption="MVTVariantInstanceGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2ff516b9-4f4a-4047-ba65-177f18342ad0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode></settings></field><field column="MVTVariantZoneID" fieldcaption="MVTVariantZoneID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="86df35ef-4c8f-439b-99a3-aa2bc5eb4e2a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode></settings></field><field column="MVTVariantPageTemplateID" fieldcaption="MVTVariantPageTemplateID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aff52b6b-e6ce-4974-9fb9-3c65cc8033b7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode></settings></field><field column="MVTVariantEnabled" fieldcaption="{$general.enabled$}" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f5e0a086-04ee-4081-93c6-9f3565dfd66d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="MVTVariantWebParts" fieldcaption="MVTVariantWebParts" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0d438521-cfaa-4491-8b50-b94593e315cd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><IsTextArea>True</IsTextArea></settings></field><field column="MVTVariantGUID" fieldcaption="MVTVariantGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a0140a2-bf3d-4cb6-baad-79f937f58dc2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode></settings></field><field column="MVTVariantLastModified" fieldcaption="MVTVariantLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="96a68c5d-ae97-41d1-8a1c-9b40d8e4a846" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="MVTVariantDocumentID" fieldcaption="MVTVariantDocumentID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea924169-2853-449a-885a-6c796a9eb032" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field></form>', N'', N'', N'', N'OM_MVTVariant', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110914 16:38:29', '2eb17c8d-39a1-4289-9d71-d29e683c882c', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2808, N'MVT - Combination', N'OM.MVTCombination', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_MVTCombination">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MVTCombinationID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MVTCombinationName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTCombinationCustomName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MVTCombinationPageTemplateID" type="xs:int" />
              <xs:element name="MVTCombinationEnabled" type="xs:boolean" />
              <xs:element name="MVTCombinationGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MVTCombinationLastModified" type="xs:dateTime" />
              <xs:element name="MVTCombinationIsDefault" type="xs:boolean" minOccurs="0" />
              <xs:element name="MVTCombinationConversions" type="xs:int" minOccurs="0" />
              <xs:element name="MVTCombinationDocumentID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_MVTCombination" />
      <xs:field xpath="MVTCombinationID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MVTCombinationID" fieldcaption="MVTCombinationID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="d60dcb84-8e12-4929-93b3-1e3300119ccb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MVTCombinationName" fieldcaption="MVTCombinationName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="0e880d0d-d373-4a10-a873-4f3e7c4c9945" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTCombinationCustomName" fieldcaption="MVTCombinationCustomName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="660ecbd6-132f-4aef-b7e7-5957a78233af" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTCombinationPageTemplateID" fieldcaption="MVTCombinationPageTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="39b24e80-9a06-4153-8b5c-6b96b5bdfbc4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTCombinationEnabled" fieldcaption="MVTCombinationEnabled" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="34b70b76-8db7-4655-8e90-a18339b3e337" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="MVTCombinationIsDefault" fieldcaption="MVTCombinationIsDefault" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0bdd8d01-0118-4c9d-bbf7-3bf42c31a2fa" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="MVTCombinationConversions" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3ff9976b-1a74-4318-8c01-269061dd48c1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="MVTCombinationGUID" fieldcaption="MVTCombinationGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e3c27c75-5071-49d8-ad5c-70de0061bf0b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="MVTCombinationLastModified" fieldcaption="MVTCombinationLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6088481c-11b3-4f99-99b1-1a1c3752cd89" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="MVTCombinationDocumentID" fieldcaption="MVTCombinationDocumentID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aaf4e191-4cd3-4f31-8390-a8a4c6983db0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_MVTCombination', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111019 09:07:55', '3a7ada8b-283a-4bea-90b3-80c6ac5645ab', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2814, N'MVT - CombinationVariation', N'OM.MVTCombinationVariation', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_MVTCombinationVariation">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MVTCombinationID" type="xs:int" />
              <xs:element name="MVTVariantID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_MVTCombinationVariation" />
      <xs:field xpath="MVTCombinationID" />
      <xs:field xpath="MVTVariantID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MVTCombinationID" fieldcaption="MVTCombinationID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="21220e3e-373b-48d1-bea2-b5227b0c7aba" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MVTVariantID" fieldcaption="MVTVariantID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="174a550e-f881-4bfb-ae78-69be2931bf4b" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_MVTCombinationVariation', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110422 11:35:41', '878cf557-64b8-4f29-8b82-b0c8faa17305', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2815, N'Membership role', N'CMS.MembershipRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_MembershipRole">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MembershipID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_MembershipRole" />
      <xs:field xpath="MembershipID" />
      <xs:field xpath="RoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MembershipID" fieldcaption="MembershipID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="9ce96cb6-f5b4-467a-82c5-496a13769f1c" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="RoleID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="7ddde3cc-0118-43fc-b93d-c65d59924398" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_MembershipRole', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110216 15:59:28', '63e61e6f-db2f-45c5-b958-25d1c1dcde2f', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2820, N'Membership user', N'CMS.MembershipUser', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_MembershipUser">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MembershipUserID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MembershipID" type="xs:int" />
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="ValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="SendNotification" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_MembershipUser" />
      <xs:field xpath="MembershipUserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MembershipUserID" fieldcaption="MembershipUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="037bd218-d53c-49b1-ad49-b8a06bdb7bf4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MembershipID" fieldcaption="MembershipID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29a99044-1fe2-41a2-a6a2-0060ce9c7010" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1da6fa3f-1dd5-4b03-8a9e-7f01e5ec7f22" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ValidTo" fieldcaption="ValidTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9a53a5a0-32a2-4e97-b17b-522e8e5f59d5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SendNotification" fieldcaption="SendNotification" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2a9ef497-6b7d-41ed-abf2-12272f0791a9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_MembershipUser', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110801 20:03:05', 'b5209315-936d-4853-b679-42abb2afdafd', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2822, N'Ecommerce - Wishlist', N'Ecommerce.Wishlist', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Wishlist">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="SKUID2" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
              <xs:element name="SKUID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Wishlist" />
      <xs:field xpath="UserID" />
      <xs:field xpath="SiteID" />
      <xs:field xpath="SKUID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b5ecb673-d68e-491e-95dc-65ae911830f6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SKUID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5f3ead4f-13ae-4f0b-b746-734ec946b594" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a6fd0d5d-e573-4412-8e21-5f7b8211097e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_Wishlist', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:00:12', '33aedb3b-cd67-4c49-b1f6-481bac97050f', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2828, N'Object version history', N'CMS.ObjectVersionHistory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ObjectVersionHistory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="VersionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="VersionObjectID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionObjectType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionObjectSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionObjectDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionXML">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionBinaryDataXML" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionModifiedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionModifiedWhen" type="xs:dateTime" />
              <xs:element name="VersionDeletedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionDeletedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="VersionNumber">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionSiteBindingIDs" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1">
      <xs:selector xpath=".//CMS_ObjectVersionHistory" />
      <xs:field xpath="VersionID" />
      <xs:field xpath="VersionObjectID" />
      <xs:field xpath="VersionObjectType" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="VersionID" fieldcaption="VersionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5b013966-6612-4c40-ad3c-b1125fe256e9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="VersionObjectID" fieldcaption="VersionObjectID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb185c41-e062-4519-b676-3b76bebb8806" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="VersionObjectType" fieldcaption="VersionObjectType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="98bc58f4-4b7c-4147-ae1a-ee30ba1a00bd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="VersionObjectSiteID" fieldcaption="VersionObjectSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4bcc1b99-124d-4ccf-90e9-d5819154c8ab" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionObjectDisplayName" fieldcaption="VersionObjectDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="808f9bb7-6f25-4197-919d-9b653b891bb7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionXML" fieldcaption="VersionXML" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="716a3431-3367-4f39-af51-cc97baf9558d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionBinaryDataXML" fieldcaption="VersionBinaryDataXML" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="85ee2f69-638b-4295-876d-3199e9752567" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionModifiedByUserID" fieldcaption="VersionModifiedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="74d1d8b3-c78e-4793-a030-0de5a0850c52" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionModifiedWhen" fieldcaption="VersionModifiedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a5768aaa-b093-46b2-b0f3-081f927ceae5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="VersionDeletedByUserID" fieldcaption="VersionDeletedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="45bfcfdf-88f0-4de1-9c2e-bd0a7ce4f9e8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionDeletedWhen" fieldcaption="VersionDeletedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="639d41ae-06eb-4346-9cbf-281855122ec2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="VersionNumber" fieldcaption="VersionNumber" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="33b565a9-acb0-41f6-9dab-cdbeb6c04458" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionSiteBindingIDs" fieldcaption="VersionSiteBindingIDs" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b6add8d5-b0ab-457e-afde-3d36c233ecb7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><FilterEnabled>False</FilterEnabled><controlname>textareacontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_ObjectVersionHistory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110726 13:27:33', '0abc9b34-986b-4abe-9000-daea24a956cf', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2837, N'Contact management - Account', N'OM.Account', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_Account">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AccountID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AccountName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountAddress1" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountAddress2" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountCity" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountZIP" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountStateID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountCountryID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountWebSite" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountPhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="26" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountFax" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="26" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountPrimaryContactID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountSecondaryContactID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountNotes" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountOwnerUserID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountSubsidiaryOfID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountMergedWithAccountID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AccountLastModified" type="xs:dateTime" />
              <xs:element name="AccountCreated" type="xs:dateTime" />
              <xs:element name="AccountGlobalAccountID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_Account" />
      <xs:field xpath="AccountID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AccountID" fieldcaption="AccountID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="30e6d460-b720-4e7d-aa05-6755e7f42fb1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AccountName" fieldcaption="AccountName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2121ec13-08c4-410f-8bc5-4e144faaeda9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountAddress1" fieldcaption="AccountAddress1" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="15aafbc2-50e0-479f-80a9-7b2fecba9a04" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountAddress2" fieldcaption="AccountAddress2" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="78c861e1-58e5-4068-96ea-01b4965d1d36" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountCity" fieldcaption="AccountCity" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="05698095-34d1-427c-8e05-92e1f1e002d8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountZIP" fieldcaption="AccountZIP" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="20" publicfield="false" spellcheck="true" guid="3c5a601f-e091-48b6-a04f-b71470b0a78f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountStateID" fieldcaption="AccountStateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f0535eb-1db7-4b81-9e24-70ce2dc8b46a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountCountryID" fieldcaption="AccountCountryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8a936012-7ba3-40df-a979-7af60d3e66d1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountWebSite" fieldcaption="AccountWebSite" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="a707d146-d098-452d-bd16-03316fa14e8b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountPhone" fieldcaption="AccountPhone" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="26" publicfield="false" spellcheck="true" guid="0160de91-12cf-441f-a2da-3ea06b0a13b5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountEmail" fieldcaption="AccountEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="404a6653-8529-4750-af37-3397b866a3b9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountFax" fieldcaption="AccountFax" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="26" publicfield="false" spellcheck="true" guid="c68fc6ef-67a1-4b5d-b31f-96a1720d4397" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountPrimaryContactID" fieldcaption="AccountPrimaryContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1bede702-e61d-47a9-aefe-02b5fb9065ac" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountSecondaryContactID" fieldcaption="AccountSecondaryContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8d9118e3-dc00-4e5f-8a2c-c6bc16361e52" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountStatusID" fieldcaption="AccountStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1bded22a-f32a-405c-b0c3-c9f423876d0d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountNotes" fieldcaption="AccountNotes" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="751d92e0-d2f5-4a36-aa19-5740b1044704" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="AccountOwnerUserID" fieldcaption="AccountOwnerUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="81e30d84-b285-45a9-88a6-63701c66b2bc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountSubsidiaryOfID" fieldcaption="AccountSubsidiaryOfID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="898a28d9-f8de-4acc-839d-41eb1848ed73" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountMergedWithAccountID" fieldcaption="AccountMergedWithAccountID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="513ed011-2949-4ebb-87f0-e02ba493ace2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountSiteID" fieldcaption="AccountSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8dd653df-c4d4-443c-bc81-e8286dd6fb88" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountGUID" fieldcaption="AccountGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5c423918-1f4c-4172-922e-00a7ba7ca96e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AccountLastModified" fieldcaption="AccountLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5e1fdba4-9315-477e-8943-e1592abe8496" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="AccountCreated" fieldcaption="AccountCreated" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f69cda62-f20e-43e9-978b-6fd1c82448b5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="AccountGlobalAccountID" fieldcaption="AccountGlobalAccountID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fcdf88ae-6baa-4559-a169-c2115a90b844" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_Account', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110824 09:28:14', '8e13fd06-a0fa-47d9-b335-17ddffb0d0a7', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2840, N'Contact management - Account status', N'OM.AccountStatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_AccountStatus">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AccountStatusID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AccountStatusName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountStatusDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountStatusDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AccountStatusSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_AccountStatus" />
      <xs:field xpath="AccountStatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AccountStatusID" fieldcaption="AccountStatusID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0611d4f5-56b4-48d7-9308-5f5c913b0cb5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AccountStatusDisplayName" fieldcaption="{$general.displayname$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="cde400d2-cbef-4ec3-af8c-19d10a28d559" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>localizabletextbox</controlname></settings></field><field column="AccountStatusName" fieldcaption="{$general.codename$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="4b284edd-af5c-4706-b798-99a946dc4f25" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="AccountStatusDescription" fieldcaption="{$general.description$}" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0102efd5-de2a-4d3d-80e7-445c35c4fcc5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><IsTextArea>True</IsTextArea><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="AccountStatusSiteID" fieldcaption="om.accountstatus.statussite" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="28851791-62c5-49d4-9d80-30bd74ce5ac0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field></form>', N'', N'', N'', N'OM_AccountStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110811 15:27:04', '39c48783-a43a-4ac4-b68d-3097fa876843', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2841, N'Contact management - Contact', N'OM.Contact', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_Contact">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContactID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ContactFirstName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactMiddleName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactLastName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactSalutation" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactTitleBefore" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactTitleAfter" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactJobTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactAddress1" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactAddress2" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactCity" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactZIP" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactStateID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactCountryID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactMobilePhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="26" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactHomePhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="26" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactBusinessPhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="26" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactWebSite" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactBirthday" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ContactGender" type="xs:int" minOccurs="0" />
              <xs:element name="ContactStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactNotes" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactOwnerUserID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactMonitored" type="xs:boolean" minOccurs="0" />
              <xs:element name="ContactMergedWithContactID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactIsAnonymous" type="xs:boolean" />
              <xs:element name="ContactSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ContactLastModified" type="xs:dateTime" />
              <xs:element name="ContactCreated" type="xs:dateTime" />
              <xs:element name="ContactMergedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ContactGlobalContactID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactBounces" type="xs:int" minOccurs="0" />
              <xs:element name="ContactLastLogon" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ContactCampaign" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_Contact" />
      <xs:field xpath="ContactID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContactID" fieldcaption="ContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="18650e04-a7a9-47fc-98e9-f692386f29aa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ContactFirstName" fieldcaption="ContactFirstName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="84594ecb-a64d-4860-8f4a-d7ee5a08c354" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactMiddleName" fieldcaption="ContactMiddleName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="ccf8d540-3f72-4b50-82a9-7349059035a3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactLastName" fieldcaption="ContactLastName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="cd41b1d5-e269-44d0-af12-d323ef2d9716" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactSalutation" fieldcaption="ContactSalutation" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="173ab8d6-e871-4bd4-87ed-85a1b98e6df3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactTitleBefore" fieldcaption="ContactTitleBefore" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="b475c693-db39-4003-9258-e80a781e609f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactTitleAfter" fieldcaption="ContactTitleAfter" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="c17b7368-e2aa-4865-99e1-082c9dc10b02" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactJobTitle" fieldcaption="ContactJobTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="2a60347a-6997-4fe1-90fc-ddc3e07d80c4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactAddress1" fieldcaption="ContactAddress1" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="bd8c4074-63e8-4251-8293-f91987ede2de" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactAddress2" fieldcaption="ContactAddress2" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="4b8554c3-6cfb-453a-8fda-ea924b62886d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactCity" fieldcaption="ContactCity" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="06f53e80-b855-4ab4-bd83-30d3056b1841" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactZIP" fieldcaption="ContactZIP" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="20" publicfield="false" spellcheck="true" guid="7692d80e-b523-4051-93dd-f7e6606d9d5e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactStateID" fieldcaption="ContactStateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f851997-36c8-4d84-a627-3715fe954ce7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactCountryID" fieldcaption="ContactCountryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02b0d2d1-854c-4771-9126-da1ba713890a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactMobilePhone" fieldcaption="ContactMobilePhone" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="26" publicfield="false" spellcheck="true" guid="2427c757-3860-431e-892e-eecd1da26410" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactHomePhone" fieldcaption="ContactHomePhone" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="26" publicfield="false" spellcheck="true" guid="5bebebaa-4e84-4d4f-a924-7e2daf8d9767" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactBusinessPhone" fieldcaption="ContactBusinessPhone" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="26" publicfield="false" spellcheck="true" guid="278d9fd3-4c08-415c-8c1f-f0df9feab23b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactEmail" fieldcaption="ContactEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="cc4e5abe-0ab1-4526-b792-e8cdf907ad4c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactWebSite" fieldcaption="ContactWebSite" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="9ed41a75-cc71-49f1-8142-733ba9aac0a4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactBirthday" fieldcaption="ContactBirthday" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="966346ba-d00e-4ac3-ab8a-1ad08bbd8ac6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ContactGender" fieldcaption="ContactGender" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6eb0cdc8-d4f1-433c-a39d-9fef3487d29e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactStatusID" fieldcaption="ContactStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db330726-0a21-4714-9374-18991f8f2cd7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactCampaign" fieldcaption="Campaign" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="df3722c4-a6b7-47d2-9535-209ad749bbf5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ContactNotes" fieldcaption="ContactNotes" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="92e4dd75-3cbe-43f5-be33-0e4c99db025a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ContactOwnerUserID" fieldcaption="ContactOwnerUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6b6c5e38-9b27-4500-b6c5-72511196d59c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactMonitored" fieldcaption="ContactMonitored" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cb68cef6-6a79-44d2-8b38-4afbf19c571a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ContactMergedWithContactID" fieldcaption="ContactMergedWithContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="14af1be4-5f57-420d-8bb8-e4df3188a4f4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactIsAnonymous" fieldcaption="ContactIsAnonymous" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f4693a12-35c1-4238-a030-d0ceb8c73df6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ContactSiteID" fieldcaption="ContactSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0649f220-2dee-41fe-9bb0-2647a5e329bd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactGUID" fieldcaption="ContactGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e55c63af-e2e1-4f5b-9cc3-770e7f7d80eb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ContactLastModified" fieldcaption="ContactLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9fe0b186-d2d0-40af-a791-f78e2c447cb4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ContactCreated" fieldcaption="ContactCreated" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="770b979e-92f8-44c3-8d84-410135a6b409" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ContactMergedWhen" fieldcaption="ContactMergedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="47b72355-d047-40b8-9d61-f88dc8f3d111" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ContactGlobalContactID" fieldcaption="ContactGlobalContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43e89736-5d65-45b0-9d4e-708d4a7d685c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactBounces" fieldcaption="ContactBounces" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0e414b04-bfd2-4191-998a-3f9f9124879f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactLastLogon" fieldcaption="ContactLastLogon" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8f8914b7-b88d-49fc-9ed9-566be9dfd9ac" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_Contact', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110929 14:19:36', 'a7ca5324-19a5-4104-9579-ef54c7fca5fa', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2843, N'Contact management - Contact status', N'OM.ContactStatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_ContactStatus">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContactStatusID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ContactStatusName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactStatusDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactStatusDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactStatusSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_ContactStatus" />
      <xs:field xpath="ContactStatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContactStatusID" fieldcaption="ContactStatusID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ee533c1f-3b09-431c-b84b-20469cb0f002" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ContactStatusDisplayName" fieldcaption="{$general.displayname$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ab35703c-8527-4f07-b98a-5dc8cf33b825" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>localizabletextbox</controlname></settings></field><field column="ContactStatusName" fieldcaption="{$general.codename$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="540a85be-c86b-45de-97e1-721af811a8bf" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ContactStatusDescription" fieldcaption="{$general.description$}" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ccf8688b-03dc-4b5f-a177-e893abadd45b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><IsTextArea>True</IsTextArea><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ContactStatusSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c32fe1f8-70f0-4c71-b1a7-5a5630386ef4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_ContactStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110811 17:50:09', '8c6ea654-3e7f-4838-89b4-6633ab851b8c', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2844, N'Contact management - Contact role', N'OM.ContactRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_ContactRole">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContactRoleID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ContactRoleName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactRoleDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactRoleDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactRoleSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_ContactRole" />
      <xs:field xpath="ContactRoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContactRoleID" fieldcaption="ContactRoleID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="378c0bef-aa2e-4038-93fe-0d378a526b9e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ContactRoleDisplayName" fieldcaption="{$general.displayname$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="4d9eacf2-3bb1-47b8-b32e-a34d55adb36f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>localizabletextbox</controlname></settings></field><field column="ContactRoleName" fieldcaption="{$general.codename$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2447dbcc-aa1f-4be9-a1b3-d35567bec9f0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ContactRoleDescription" fieldcaption="{$general.description$}" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="86e4aa34-d34c-423f-9705-e9ddce795b54" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><IsTextArea>True</IsTextArea><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ContactRoleSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="357fdbc7-b7f3-4bf9-be8b-8f8d7ed8800d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_ContactRole', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110811 17:49:25', '6ef5e1b9-08d9-4dfa-a748-e483afe72621', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2848, N'Contact management - AccountContact', N'OM.AccountContact', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_AccountContact">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AccountContactID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ContactRoleID" type="xs:int" minOccurs="0" />
              <xs:element name="AccountID" type="xs:int" />
              <xs:element name="ContactID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_AccountContact" />
      <xs:field xpath="AccountContactID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AccountContactID" fieldcaption="AccountContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f4b7d8c1-777b-410e-b1c2-6e6251449216" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ContactRoleID" fieldcaption="ContactRoleID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bcfc3aba-b780-4350-b6a7-930d6d4ff8d8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AccountID" fieldcaption="AccountID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="67ab562d-fa08-4bfd-930c-de1b9d916b6f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactID" fieldcaption="ContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="96361cb2-9434-4b11-b430-b799dbce84ad" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_AccountContact', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110906 20:55:30', '6f8e05a4-3bc3-4d45-a4ea-905664c3a84e', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2850, N'Contact management - Contact group', N'OM.ContactGroup', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_ContactGroup">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContactGroupID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ContactGroupName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactGroupDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactGroupDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactGroupSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactGroupDynamicCondition" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContactGroupScheduledTaskID" type="xs:int" minOccurs="0" />
              <xs:element name="ContactGroupEnabled" type="xs:boolean" minOccurs="0" />
              <xs:element name="ContactGroupLastModified" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ContactGroupGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="ContactGroupStatus" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_ContactGroup" />
      <xs:field xpath="ContactGroupID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContactGroupID" fieldcaption="ContactGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8254be8f-7554-4c85-8d8b-f16e0ec2bd5f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ContactGroupName" fieldcaption="ContactGroupName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="5c5692fb-85c4-409f-a6e6-6f6670a52f7d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactGroupDisplayName" fieldcaption="ContactGroupDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="0b8230c0-06a2-4378-9435-8f475865687a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactGroupDescription" fieldcaption="ContactGroupDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="99c12065-9aa0-4dce-835d-949a31f4162e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ContactGroupSiteID" fieldcaption="ContactGroupSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3fd1a0bc-a707-47ac-8697-639814b7d8c5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactGroupDynamicCondition" fieldcaption="ContactGroupDynamicCondition" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd9391ed-bfab-4753-bc4d-a98e0457ce1a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ContactGroupScheduledTaskID" fieldcaption="ContactGroupScheduledTaskID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="30f002f8-04f7-4af6-8e4c-b0984ac7c36c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ContactGroupEnabled" fieldcaption="ContactGroupEnabled" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b2572394-a920-4f7e-81a9-5cc435925099" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ContactGroupLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b6dff322-4c32-46ec-a06b-2b096a148658" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="ContactGroupGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="affe9d0f-3c70-4a67-90f8-d61075eb1b55" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="ContactGroupStatus" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8c338293-e30c-4b0c-bae3-58eea2993f58" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_ContactGroup', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110816 18:28:40', '97f104ff-d87a-48b8-a5db-216786f9344e', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
SET IDENTITY_INSERT [CMS_Class] OFF
