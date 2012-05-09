SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1, N'Culture', N'cms.culture', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Culture">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CultureID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CultureName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CultureCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CultureShortName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CultureGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CultureLastModified" type="xs:dateTime" />
              <xs:element name="CultureAlias" minOccurs="0">
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
      <xs:selector xpath=".//CMS_Culture" />
      <xs:field xpath="CultureID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CultureID" fieldcaption="CultureID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a7cdc074-6681-4037-9c7c-168f92de6ec0" /><field column="CultureName" fieldcaption="CultureName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5523daf0-292c-462e-9b45-a8aff0e555d6" /><field column="CultureCode" fieldcaption="CultureCode" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d9cfc140-95f9-4fc4-b123-0b4b5bdaf347" /><field column="CultureShortName" fieldcaption="CultureShortName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5669759-7b7d-47ae-8aeb-4e1e33490105" /><field column="CultureGUID" fieldcaption="CultureGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="17a5237f-e3ca-4052-848d-cab4b5423494" /><field column="CultureLastModified" fieldcaption="CultureLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29623bff-09c2-4789-86c2-dd326b85ef20" /><field column="CultureAlias" visible="false" columntype="text" fieldtype="label" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="1651cf8a-4c78-4127-b925-1e082b4c4978" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_Culture', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:16:24', '21ea0bc0-a9b7-4888-8255-9bd5255e2ef0', 0, 1, 0, N'', 1, N'CultureName', N'0', N'', N'CultureLastModified', N'<search><item searchable="True" name="CultureID" tokenized="False" content="False" id="57080a99-a409-4b59-bbed-42f0aa2fd14a" /><item searchable="False" name="CultureName" tokenized="True" content="True" id="2e3906db-73cc-4009-9b6a-6bab52ccdb88" /><item searchable="False" name="CultureCode" tokenized="True" content="True" id="b9bac9ed-fb4f-4459-9a5a-5589a0540d94" /><item searchable="False" name="CultureShortName" tokenized="True" content="True" id="74078f24-ef01-40a2-8b85-278ad6ff0d82" /><item searchable="False" name="CultureGUID" tokenized="False" content="False" id="60df829a-fdb0-4e9e-951a-8976d2da034f" /><item searchable="True" name="CultureLastModified" tokenized="False" content="False" id="b01264eb-deba-4e77-9fad-2265784c0747" /><item searchable="False" name="CultureAlias" tokenized="True" content="True" id="564de803-d7d7-46e0-8cde-7b676d78dfa0" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (52, N'Site', N'cms.site', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Site">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SiteID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SiteName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteStatus">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDomainName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDefaultStylesheetID" type="xs:int" minOccurs="0" />
              <xs:element name="SiteDefaultVisitorCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDefaultEditorStylesheet" type="xs:int" minOccurs="0" />
              <xs:element name="SiteGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SiteLastModified" type="xs:dateTime" />
              <xs:element name="SiteIsOffline" type="xs:boolean" minOccurs="0" />
              <xs:element name="SiteOfflineRedirectURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteOfflineMessage" minOccurs="0">
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
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Site" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8f1d2a8a-6f44-41ef-899b-d0dd37b82cab" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SiteName" fieldcaption="SiteName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f69f0f6-e786-435f-83f1-9f771fa2f726" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteDisplayName" fieldcaption="SiteDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="24ef1161-4c27-4bea-8b37-36acc59cff34" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteDescription" fieldcaption="SiteDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c445911-e93e-4449-bb1f-9cc4048e788f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="SiteStatus" fieldcaption="SiteStatus" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4683b075-8db9-4411-bf14-563e795cdd95" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteDomainName" fieldcaption="SiteDomainName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4a553b11-0d6a-44c5-8e2c-7bef0ce6587d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteDefaultStylesheetID" fieldcaption="SiteDefaultStylesheetID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f4474c6-9673-4bbc-8782-d4d24a38826b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteDefaultVisitorCulture" fieldcaption="SiteDefaultVisitorCulture" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ddfbfe8d-3b87-4785-b147-24a28f107937" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteDefaultEditorStylesheet" fieldcaption="SiteDefaultEditorStylesheet" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0c91ee48-7b00-4ece-ab38-39e20fd64788" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteGUID" fieldcaption="SiteGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="de9b4fc5-3c93-4751-9b10-061edd14a3d8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="SiteLastModified" fieldcaption="SiteLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="90802d29-9156-46b9-891a-4612618d8ea5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SiteIsOffline" fieldcaption="Site is offline" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="48a74857-910c-4422-b1e1-beeaf05bae51" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SiteOfflineRedirectURL" fieldcaption="Offline redirect URL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="400" publicfield="false" spellcheck="false" guid="92291451-c76c-46ad-8e3a-7c6d4611d01a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SiteOfflineMessage" fieldcaption="Offline message" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e9dc8486-78b6-46a3-8ea8-635ea7a865d6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><ShowColor>True</ShowColor><ShowQuote>True</ShowQuote><UsePromptDialog>True</UsePromptDialog><controlname>bbeditorcontrol</controlname><ShowItalic>True</ShowItalic><ShowBold>True</ShowBold><DisplayEmailTabSetting>False</DisplayEmailTabSetting><MediaDialogConfiguration>True</MediaDialogConfiguration><ShowImage>True</ShowImage><ShowStrike>True</ShowStrike><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><ShowUnderline>True</ShowUnderline><Autoresize_Hashtable>True</Autoresize_Hashtable><ShowCode>True</ShowCode><ShowUrl>True</ShowUrl><DisplayAutoresize>True</DisplayAutoresize><Dialogs_Content_Hide>False</Dialogs_Content_Hide></settings></field></form>', N'', N'', N'', N'CMS_Site', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110524 17:13:09', '8f2f80f1-13cb-4050-bc10-14a45b09f4e0', 0, 1, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (56, N'Role', N'cms.Role', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Role">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="RoleID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="RoleDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="RoleName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="RoleDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteID" type="xs:int" minOccurs="0" />
              <xs:element name="RoleGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="RoleLastModified" type="xs:dateTime" />
              <xs:element name="RoleGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="RoleIsGroupAdministrator" type="xs:boolean" minOccurs="0" />
              <xs:element name="RoleIsDomain" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Role" />
      <xs:field xpath="RoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0c8cc1de-1c82-4596-b6d0-b2c60bcad3b7" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="RoleDisplayName" fieldcaption="RoleDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="c5f83fe3-f362-431f-80be-2d40122e469e" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="RoleName" fieldcaption="RoleName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="918120c1-6d6b-42f5-be30-37f91af11666" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="RoleDescription" fieldcaption="RoleDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c465483d-4158-42e0-bb25-0c274769c74a" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7238f49e-49eb-46a7-9dc4-811c3ca7d524" visibility="none" ismacro="false" hasdependingfields="false"><settings><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled><controlname>textboxcontrol</controlname></settings></field><field column="RoleGUID" fieldcaption="RoleGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad0f25d2-15b7-4eae-86bd-fc64538c45f9" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="RoleLastModified" fieldcaption="RoleLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="11b1bf06-0088-48a8-87d6-cffafff290ca" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="RoleGroupID" fieldcaption="RoleGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bf9ad143-276c-4860-8005-adf080f77aac" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="RoleIsGroupAdministrator" fieldcaption="RoleIsGroupAdministrator" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e4477aee-81e5-475f-90e2-7bb416b9f412" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="RoleIsDomain" fieldcaption="RoleIsDomain" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aa3f60b2-8781-44d4-8705-d264d514883c" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Role', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110414 17:49:33', '1dba5a45-954e-442c-8a00-41927c501f2b', 0, 1, 0, N'', 1, N'RoleName', N'RoleDisplayName', N'', N'0', N'<search><item searchable="True" name="RoleID" tokenized="False" content="False" id="fe7f1fac-37a8-4c3a-845e-c9df13507bee" /><item searchable="False" name="RoleDisplayName" tokenized="True" content="True" id="98796a0f-f95f-45e6-b261-6549bbb0be1c" /><item searchable="False" name="RoleName" tokenized="True" content="True" id="8201b0ee-6000-4002-a8b8-c59da6b2b2dc" /><item searchable="False" name="RoleDescription" tokenized="True" content="True" id="227ed53c-06eb-4b7f-bc93-407e5e44ef9d" /><item searchable="True" name="SiteID" tokenized="False" content="False" id="491c13dd-fdaa-414a-b89d-2dbb34e6f722" /><item searchable="False" name="RoleGUID" tokenized="False" content="False" id="44e0ac15-b8b5-4cd2-a001-01813d6d1978" /><item searchable="True" name="RoleLastModified" tokenized="False" content="False" id="7cbcbcee-db41-4163-9cdc-42d0df906939" /><item searchable="True" name="RoleGroupID" tokenized="False" content="False" id="c99c3303-ca48-43e8-b3fb-f5dd355a086a" /><item searchable="True" name="RoleIsGroupAdministrator" tokenized="False" content="True" id="5a051762-510e-4ac7-b1ce-91fd8ae4afac" /><item searchable="True" name="RoleIsDomain" tokenized="False" content="True" id="377d9990-4ad0-4458-af7a-dd65333f0b59" /></search>', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (59, N'User', N'cms.user', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_User">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UserName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FirstName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MiddleName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LastName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FullName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="Email" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserPassword">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PreferredCultureCode" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PreferredUICultureCode" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserEnabled" type="xs:boolean" />
              <xs:element name="UserIsEditor" type="xs:boolean" />
              <xs:element name="UserIsGlobalAdministrator" type="xs:boolean" />
              <xs:element name="UserIsExternal" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserPasswordFormat" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserCreated" type="xs:dateTime" minOccurs="0" />
              <xs:element name="LastLogon" type="xs:dateTime" minOccurs="0" />
              <xs:element name="UserStartingAliasPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="UserLastModified" type="xs:dateTime" />
              <xs:element name="UserLastLogonInfo" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserIsHidden" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserVisibility" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserIsDomain" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserHasAllowedCultures" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserSiteManagerDisabled" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_User" />
      <xs:field xpath="UserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="153b1cec-1580-43ae-adf8-ca0e4879d168" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserName" fieldcaption="UserName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="aa4122b7-db14-48a5-885e-07a242f84702" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FirstName" fieldcaption="FirstName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="60af25ea-ad95-49ba-a446-2ea754cd10be" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="MiddleName" fieldcaption="MiddleName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="bc86d726-a42d-4401-b92a-227e8e280293" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LastName" fieldcaption="LastName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="67305fcf-928a-45ea-8e29-735f17da0972" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FullName" fieldcaption="FullName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="5c3003d6-de76-4e96-aca4-cf1efa86b3fa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="Email" fieldcaption="Email" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="false" guid="fcef0fb3-6145-48ed-8f41-e28b71a665a8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="UserPassword" fieldcaption="UserPassword" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="adaad374-af5a-4e1b-afc7-72b4fb798d19" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PreferredCultureCode" fieldcaption="PreferredCultureCode" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="03d85a82-c928-4967-b793-f33bedb426be" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PreferredUICultureCode" fieldcaption="PreferredUICultureCode" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="d0a047c0-ea96-4422-af97-fff6c35a80de" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserEnabled" fieldcaption="UserEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d5d58e7d-6b71-4a51-9226-d7ce166e8580" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserIsEditor" fieldcaption="UserIsEditor" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9804fc8b-7b25-4c23-b0f4-f6c57b7ae77f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserIsGlobalAdministrator" fieldcaption="UserIsGlobalAdministrator" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4de4df49-2e8d-4bb5-9fb9-a3da2a9040ba" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserIsExternal" fieldcaption="UserIsExternal" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="def5feec-077b-4c26-8c4a-005a4a8d82d8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserPasswordFormat" fieldcaption="UserPasswordFormat" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="d3f178a2-6df1-4651-bec5-864be986332a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserCreated" fieldcaption="UserCreated" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d75ebe3e-3d7d-44ab-af34-0ee947c3a3eb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="LastLogon" fieldcaption="LastLogon" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8bb80186-0c9b-45cb-91d1-cbec14b8fdc7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="UserStartingAliasPath" fieldcaption="UserStartingAliasPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="e2152255-6ad0-4b88-8497-2b505c64d23c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserGUID" fieldcaption="UserGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="89fae932-ca98-420b-92a0-0fc146856ce3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserLastModified" fieldcaption="UserLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e1f9c8fd-8c5d-415c-ad0e-935034d751de" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="UserLastLogonInfo" fieldcaption="UserLastLogonInfo" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c17d233f-cd61-42d7-9c98-f96267a12764" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="UserIsHidden" fieldcaption="UserIsHidden" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82febdf9-9057-4cea-bbba-de40b8cf653e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserVisibility" fieldcaption="UserVisibility" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="150ce55f-7617-4fff-9d31-6f3792291b67" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="UserIsDomain" fieldcaption="UserIsDomain" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6ca5c321-81dd-4fbf-8ccc-9df2675839f9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserHasAllowedCultures" fieldcaption="UserHasAllowedCultures" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e7db3339-6c1c-413d-8fa3-fa85d99b8c80" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserSiteManagerDisabled" fieldcaption="UserLastModified" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="175707eb-d3ee-42b1-bd2b-7c2f67a70449" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_User', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20120124 10:42:10', '2e02c378-0f3d-45de-9b2d-b8cf2bd87b55', 0, 0, 0, N'', 0, N'UserNickName', N'UserDescription', N'', N'UserActivationDate', N'<search><item searchable="True" name="UserID" tokenized="False" content="False" id="906a4b9b-8bf2-4751-a526-121307dae0ae" /><item searchable="False" name="UserName" tokenized="True" content="True" id="0b94fb3d-764e-4a05-80a4-42ec5f5833f0" /><item searchable="False" name="FirstName" tokenized="True" content="True" id="fbf31afa-5f01-4cd8-a487-e4a8433ee370" /><item searchable="False" name="MiddleName" tokenized="True" content="True" id="d57ee511-df4a-41fd-bb4a-5d6370663d5d" /><item searchable="False" name="LastName" tokenized="True" content="True" id="b3b4766c-5281-473c-ae98-d36f73ce76cf" /><item searchable="False" name="FullName" tokenized="True" content="True" id="eb5190f9-62f7-4522-ba94-8c81f3ec3b62" /><item searchable="True" name="Email" tokenized="False" content="False" id="1979c352-532a-4554-a04c-1fa7f511c70b" /><item searchable="False" name="UserPassword" tokenized="False" content="False" id="d1e89105-5b6f-47f3-8eaf-6d98f6d58dc3" /><item searchable="False" name="PreferredCultureCode" tokenized="False" content="False" id="e76e7d5a-f101-4efa-83ed-e9e6f1e59582" /><item searchable="False" name="PreferredUICultureCode" tokenized="False" content="False" id="3cdcff76-c5df-4b71-8286-8c8e8aacccc8" /><item searchable="False" name="UserEnabled" tokenized="False" content="False" id="1ccb5cd7-9c8c-4315-889f-995859c9fd56" /><item searchable="False" name="UserIsEditor" tokenized="False" content="False" id="f80dc7fd-48de-4d35-bf29-5e2c84323635" /><item searchable="False" name="UserIsGlobalAdministrator" tokenized="False" content="False" id="14580190-c5d6-4cf1-9085-9964df955900" /><item searchable="False" name="UserIsExternal" tokenized="False" content="False" id="05156dd2-91de-44cb-9d86-ee13d6263dfc" /><item searchable="False" name="UserPasswordFormat" tokenized="False" content="False" id="9f1854bd-4c16-4b27-b7d6-ffff18d8c0c6" /><item searchable="True" name="UserCreated" tokenized="False" content="False" id="2269b09f-c68b-4d02-a4eb-06caa9d507f7" /><item searchable="True" name="LastLogon" tokenized="False" content="False" id="a94c7e3a-2ff1-4c24-97bb-e80383533153" /><item searchable="False" name="UserStartingAliasPath" tokenized="False" content="False" id="6a1bacb5-7540-4597-94ec-f0d80dae4ae2" /><item searchable="False" name="UserGUID" tokenized="False" content="False" id="b39a8c06-0956-46b0-9f57-1160f56f88ff" /><item searchable="False" name="UserLastModified" tokenized="False" content="False" id="b013639c-714c-41ad-a3f4-ef17caae4cbb" /><item searchable="False" name="UserLastLogonInfo" tokenized="False" content="False" id="08f3bfe8-dde6-4141-b991-b96194230424" /><item searchable="False" name="UserIsHidden" tokenized="False" content="False" id="0bff90b3-efd2-4a7d-adcd-4e22374806e7" /><item searchable="False" name="UserVisibility" tokenized="False" content="False" id="4594d717-6576-40e7-9b18-2f98544414c2" /><item searchable="False" name="UserIsDomain" tokenized="False" content="False" id="79fadbf0-ea55-4ecf-873d-fac1ac1b359e" /><item searchable="False" name="UserHasAllowedCultures" tokenized="False" content="False" id="c7ef8410-f41e-47c8-86b4-6b78d4e35911" /><item searchable="False" name="UserSiteManagerDisabled" tokenized="False" content="False" id="a8344b38-c552-4929-8d49-f0d0ea06b102" /><item searchable="False" name="UserSettingsID" tokenized="False" content="False" id="476b97bf-9ad7-4e8b-b3f4-2119d1fbdbf1" /><item searchable="False" name="UserNickName" tokenized="True" content="True" id="c63513a9-4a1b-4594-87b2-8ae998a123f8" /><item searchable="False" name="UserPicture" tokenized="False" content="False" id="8bf2a728-7c12-4a6f-843b-d21856988b8b" /><item searchable="False" name="UserSignature" tokenized="True" content="True" id="2c59ed93-c6c2-4894-ab8c-c04400f2d16e" /><item searchable="False" name="UserURLReferrer" tokenized="False" content="False" id="a2d34fe0-7f76-488c-904d-8637d3b0fb6f" /><item searchable="True" name="UserCampaign" tokenized="False" content="False" id="7ca760d0-7c24-4ade-8f5d-32d6d049d745" /><item searchable="False" name="UserMessagingNotificationEmail" tokenized="False" content="False" id="8023feac-5337-4bfb-b3b8-7b6ab6f45c4c" /><item searchable="False" name="UserCustomData" tokenized="False" content="False" id="c1c89fd3-9920-4de5-8549-5741cd129112" /><item searchable="False" name="UserRegistrationInfo" tokenized="False" content="False" id="96fec15d-70a5-4544-b780-aab02b067c40" /><item searchable="False" name="UserPreferences" tokenized="False" content="False" id="90a88cf6-3570-4c41-9b4b-54897af93b23" /><item searchable="False" name="UserActivationDate" tokenized="False" content="False" id="db7bb756-65cc-4d60-9068-461e6db2723a" /><item searchable="False" name="UserActivatedByUserID" tokenized="False" content="False" id="b3b77c0a-601c-4188-a19e-068dc11927ce" /><item searchable="False" name="UserTimeZoneID" tokenized="False" content="False" id="60da5e8e-47d3-4d8e-afc4-7fbafb3a8545" /><item searchable="False" name="UserAvatarID" tokenized="False" content="False" id="25a29893-a1ef-4b0c-bd36-0e40a52f8434" /><item searchable="False" name="UserBadgeID" tokenized="False" content="False" id="511368d2-579a-48bb-971e-34d84f91993a" /><item searchable="False" name="UserShowSplashScreen" tokenized="False" content="False" id="3aca1ef8-0dbb-4c47-b886-997e9296869e" /><item searchable="False" name="UserActivityPoints" tokenized="False" content="False" id="842bc3f5-99d4-4b81-947b-59b8e7018b69" /><item searchable="False" name="UserForumPosts" tokenized="False" content="False" id="a1a195d6-0e50-4297-a416-7a5ada5f0079" /><item searchable="False" name="UserBlogComments" tokenized="False" content="False" id="f26fc147-1200-480a-be20-4498ce903d80" /><item searchable="False" name="UserGender" tokenized="False" content="False" id="6c259ec2-12e4-40d2-b081-ab6f728810da" /><item searchable="False" name="UserDateOfBirth" tokenized="False" content="False" id="40215bd4-6097-456d-9af9-8fa42f8b2f3f" /><item searchable="False" name="UserMessageBoardPosts" tokenized="False" content="False" id="7ed05964-2022-4720-be24-29c772e18582" /><item searchable="False" name="UserSettingsUserGUID" tokenized="False" content="False" id="78e65697-f010-4f1e-9286-a796f926bbb2" /><item searchable="False" name="UserSettingsUserID" tokenized="False" content="False" id="d84f6ded-9648-4f79-9d55-ab6c63cdc16b" /><item searchable="False" name="WindowsLiveID" tokenized="False" content="False" id="f30e931b-be81-4b32-8309-f42236ed671f" /><item searchable="False" name="UserBlogPosts" tokenized="False" content="False" id="a01aef46-15e3-4c4e-9152-edff35522dd9" /><item searchable="False" name="UserWaitingForApproval" tokenized="False" content="False" id="1d77bf3c-04f1-4821-9099-3ed75067bd3b" /><item searchable="False" name="UserDialogsConfiguration" tokenized="False" content="False" id="dfe24bcd-67f8-4d3c-b5ec-8aa9aacefcfa" /><item searchable="False" name="UserDescription" tokenized="True" content="True" id="46622599-1885-477c-b3ba-e756e5e8148c" /><item searchable="False" name="UserUsedWebParts" tokenized="False" content="False" id="ca5ec30c-e8ed-4c2d-8abd-9e4484e65db6" /><item searchable="False" name="UserUsedWidgets" tokenized="False" content="False" id="1cec03a7-83ef-4f40-a99b-375c08a937dc" /><item searchable="False" name="UserFacebookID" tokenized="False" content="False" id="1bbd4ba6-e98a-4e35-af2b-c4953a6caa87" /><item searchable="False" name="UserLinkedInID" tokenized="False" content="False" id="d80eb0f7-b04e-40b8-b492-ac19be22fa32" /><item searchable="False" name="UserAuthenticationGUID" tokenized="False" content="False" id="7711ec33-cdf7-494b-b8b4-2ba23cdfa845" /><item searchable="False" name="UserSkype" tokenized="False" content="False" id="8b51b6a3-0e20-496f-b779-eeb0e1aef4dd" /><item searchable="False" name="UserIM" tokenized="False" content="False" id="b9d42962-eee7-4927-b823-92bab31af520" /><item searchable="False" name="UserPhone" tokenized="False" content="False" id="cfbdf268-5d6e-4c38-bb41-9324cc62fcd5" /><item searchable="False" name="UserPosition" tokenized="False" content="False" id="2ca6ad24-911b-4885-bee5-931b47a684ff" /><item searchable="False" name="UserBounces" tokenized="False" content="False" id="59eff77a-ab9f-4049-877b-e0759e960c1d" /></search>', NULL, 1, N'', NULL, N'<form><field column="ContactBirthday" mappedtofield="UserDateOfBirth" /><field column="ContactEmail" mappedtofield="Email" /><field column="ContactFirstName" mappedtofield="FirstName" /><field column="ContactGender" mappedtofield="UserGender" /><field column="ContactLastName" mappedtofield="LastName" /><field column="ContactMiddleName" mappedtofield="MiddleName" /><field column="ContactMobilePhone" mappedtofield="UserPhone" /></form>', 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (109, N'UserRole', N'cms.userrole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>  
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"> 
   <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">      <xs:complexType>      
     <xs:choice minOccurs="0" maxOccurs="unbounded">          <xs:element name="CMS_UserRole">            <xs:complexType>   
                <xs:sequence>               
                   <xs:element name="UserRoleID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />  
                   <xs:element name="UserID" type="xs:int" />               
                   <xs:element name="RoleID" type="xs:int" />            
                   <xs:element name="ValidTo" type="xs:dateTime" minOccurs="0" />        
                 </xs:sequence>                            
               </xs:complexType>          
              </xs:element>        
             </xs:choice>   
            </xs:complexType>      
           <xs:unique name="Constraint1" msdata:PrimaryKey="true">   
                                           <xs:selector xpath=".//CMS_UserRole" />        <xs:field xpath="UserRoleID" />    
                                             </xs:unique>    </xs:element>  </xs:schema>', N'<form><field column="UserRoleID" fieldcaption="UserRoleID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a6ca4467-2aca-4cdb-b8e6-218521075d5a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fade36d8-d004-4e7e-a354-614332e49bc9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="61c16b7f-a96a-454e-aa67-9159e1e1d2d2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ValidTo" fieldcaption="ValidTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c2c09583-b8b4-4726-80c3-d1c782a936c9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_UserRole', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110301 10:57:31', '7664a5c5-128c-4546-a7f9-c6d3e694c28a', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (129, N'Email template', N'cms.emailtemplate', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_EmailTemplate">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EmailTemplateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="EmailTemplateName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="EmailTemplateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="EmailTemplateLastModified" type="xs:dateTime" />
              <xs:element name="EmailTemplatePlainText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateSubject" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateFrom" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateCc" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateBcc" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTemplateType" minOccurs="0">
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
      <xs:selector xpath=".//CMS_EmailTemplate" />
      <xs:field xpath="EmailTemplateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EmailTemplateID" fieldcaption="EmailTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ada65d2f-9f6f-4cac-b7b4-380a13ba6a53" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="EmailTemplateName" fieldcaption="EmailTemplateName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db77f0cf-12cf-43e6-be0a-28c1c97fb026" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailTemplateDisplayName" fieldcaption="EmailTemplateDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c80ea4a-4e2f-4f4e-b524-9ec6acb2290b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailTemplateType" fieldcaption="EmailTemplateType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="3c94ab49-0c7e-4546-9528-a2ded8babc29" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="EmailTemplateText" fieldcaption="EmailTemplateText" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7cb8a717-2b0c-4b92-9e38-2530185dcafb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailTemplateSiteID" fieldcaption="EmailTemplateSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="152ad89f-51ae-42f3-a7cd-b28794ebbb73" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailTemplateGUID" fieldcaption="EmailTemplateGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42373ea6-c944-44c2-9a3f-95c5bdc78012" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="EmailTemplateLastModified" fieldcaption="EmailTemplateLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="513a454c-2061-4083-88bb-12b92bdf2a5e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="EmailTemplatePlainText" fieldcaption="EmailTemplatePlainText" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c25debb-1332-41eb-a77c-a69f93b3111c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailTemplateSubject" fieldcaption="EmailTemplateSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="31f6996b-5f0e-4350-bfb7-4857e4b89bb0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailTemplateFrom" fieldcaption="EmailTemplateFrom" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="515d919e-b186-4bc4-9064-c086fd34715c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailTemplateCc" fieldcaption="EmailTemplateCc" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43b52be1-a0d2-4af5-9ae4-5e80ca589396" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailTemplateBcc" fieldcaption="EmailTemplateBcc" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f0fafbef-3dd5-40d8-923c-091f692e7664" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_EmailTemplate', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110715 15:54:13', 'f54a32bf-6218-46cc-802c-89efad7a5740', 0, 1, 0, N'', 1, N'EmailTemplateDisplayName', N'EmailTemplateText', N'', N'EmailTemplateLastModified', N'<search><item searchable="True" name="EmailTemplateID" tokenized="False" content="False" id="331544d4-253f-46cb-86d0-70e533097950" /><item searchable="False" name="EmailTemplateName" tokenized="True" content="True" id="8913291b-5a63-490e-8e49-eee2bd1aae8b" /><item searchable="False" name="EmailTemplateDisplayName" tokenized="True" content="True" id="8a635d8b-f9cc-45f0-8fcf-bcc596d42aa9" /><item searchable="False" name="EmailTemplateText" tokenized="True" content="True" id="928dccdc-014c-41c7-846b-a1ebc26f6bca" /><item searchable="True" name="EmailTemplateSiteID" tokenized="False" content="False" id="4600d382-b9ba-4411-bfb9-c0ad5085853c" /><item searchable="False" name="EmailTemplateGUID" tokenized="False" content="False" id="2c76041b-5e91-4448-903a-f55c8ae61331" /><item searchable="True" name="EmailTemplateLastModified" tokenized="False" content="False" id="7b088452-797d-4e8b-93ae-16bea28891b9" /><item searchable="False" name="EmailTemplatePlainText" tokenized="True" content="True" id="98bc077f-c21c-4e09-b8f4-fe1771ebbf4c" /><item searchable="False" name="EmailTemplateSubject" tokenized="True" content="True" id="ef3f97b2-f6cc-4bf4-8288-1a25d70269f1" /><item searchable="False" name="EmailTemplateFrom" tokenized="True" content="True" id="60af94e7-e451-4549-a0f8-f4f1562d91b3" /><item searchable="False" name="EmailTemplateCc" tokenized="True" content="True" id="a1cf4a97-d5c3-4d97-8974-aa89fd47fad8" /><item searchable="False" name="EmailTemplateBcc" tokenized="True" content="True" id="7fedb62e-d9e9-4f53-b45c-10cb82e0f9d4" /></search>', NULL, 0, N'', NULL, NULL, 0, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (131, N'Permission', N'cms.permission', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Permission">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PermissionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PermissionDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PermissionName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassID" type="xs:int" minOccurs="0" />
              <xs:element name="ResourceID" type="xs:int" minOccurs="0" />
              <xs:element name="PermissionGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PermissionLastModified" type="xs:dateTime" />
              <xs:element name="PermissionDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PermissionDisplayInMatrix" type="xs:boolean" minOccurs="0" />
              <xs:element name="PermissionOrder" type="xs:int" minOccurs="0" />
              <xs:element name="PermissionEditableByGlobalAdmin" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Permission" />
      <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f61417a5-d60b-4c3e-a8f4-6b5d08407b2e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PermissionOrder" fieldcaption="PermissionOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="89564152-f380-49b4-8250-70fbd52ac9eb" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PermissionDisplayName" fieldcaption="PermissionDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="216fca18-3ef5-4346-a067-df2600e48af2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PermissionName" fieldcaption="PermissionName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="81099da7-b5b3-4e0c-af3d-9dc48b3b3238" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassID" fieldcaption="ClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0c800bfe-2dc6-4053-85cb-5d7fcce37ced" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ResourceID" fieldcaption="ResourceID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8b3c89b8-6134-4450-b1db-e20cb98a157b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PermissionGUID" fieldcaption="PermissionGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72fec8a3-7ead-451f-8356-f34dc56df28d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PermissionLastModified" fieldcaption="PermissionLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01413912-03a0-436b-a91f-21cc1d3fe749" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PermissionDescription" fieldcaption="PermissionDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2720ebba-d118-42f8-b4e3-e3b864deddfb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="PermissionDisplayInMatrix" fieldcaption="PermissionDisplayInMatrix" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad45cd7d-f305-42d0-ac09-3aab213c2371" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PermissionEditableByGlobalAdmin" fieldcaption="PermissionEditableByGlobalAdmin" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8b4f42ff-affb-4dd2-af44-7a8f40ede73c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Permission', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110419 14:34:36', '83a574c4-dffd-45f1-bd21-c78f18dcaa72', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (134, N'Resource', N'cms.resource', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Resource">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ResourceID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ResourceDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ResourceName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ResourceDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ShowInDevelopment" type="xs:boolean" minOccurs="0" />
              <xs:element name="ResourceURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ResourceGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ResourceLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Resource" />
      <xs:field xpath="ResourceID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ResourceID" fieldcaption="ResourceID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="870bc6d4-f89a-4116-80b3-45a32604b394" ismacro="false" /><field column="ResourceDisplayName" fieldcaption="ResourceDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="e4714769-8c09-4488-82ee-69f0c7aeccac" ismacro="false" /><field column="ResourceName" fieldcaption="ResourceName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="69930d99-46c8-4be2-b19c-5a9edb05fb79" ismacro="false" /><field column="ResourceDescription" fieldcaption="ResourceDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="27828b12-d1fb-4cb1-866d-a15f683bb674" ismacro="false" /><field column="ShowInDevelopment" fieldcaption="ShowInDevelopment" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b8fdc63e-35c2-400f-af7f-9b04b39c4b2f" ismacro="false" /><field column="ResourceURL" fieldcaption="ResourceURL" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="1000" publicfield="false" spellcheck="true" guid="5585f8e7-88f5-4b0e-88ec-3c695822530c" ismacro="false" /><field column="ResourceGUID" fieldcaption="ResourceGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2389fed5-9c07-45e7-88db-4e56ec0c2889" ismacro="false" /><field column="ResourceLastModified" fieldcaption="ResourceLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a43d2cef-9fdc-43a8-8cd8-37b161ef4582" ismacro="false" /></form>', N'', N'', N'', N'CMS_Resource', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110810 10:04:02', '93746c62-21e4-4fda-bcbd-61c5fcee9945', 0, 1, 0, N'', 1, N'ResourceDisplayName', N'ResourceDescription', N'', N'ResourceLastModified', N'<search><item searchable="True" name="ResourceID" tokenized="False" content="False" id="8c81ce3e-d2ad-4047-a9de-5fa9ecb68f9c" /><item searchable="False" name="ResourceDisplayName" tokenized="True" content="True" id="4dcae158-1288-490f-95d4-db532558bb34" /><item searchable="False" name="ResourceName" tokenized="True" content="True" id="25ffd74c-ae3d-4ea5-b91a-0665b186dcd8" /><item searchable="False" name="ResourceDescription" tokenized="True" content="True" id="c139de5a-615f-4806-b2f7-162daf3d1e18" /><item searchable="True" name="ShowInDevelopment" tokenized="False" content="False" id="6428f209-375b-429c-b205-37362ae56d96" /><item searchable="False" name="ResourceURL" tokenized="True" content="True" id="2f274a66-9fbc-49c2-a137-b481edc7a656" /><item searchable="False" name="ResourceGUID" tokenized="False" content="False" id="828dfcfd-03d3-42c7-b761-71dbdbf197c3" /><item searchable="True" name="ResourceLastModified" tokenized="False" content="False" id="ac1a1a28-22b7-4a99-9c94-3cc7fd7ff012" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (140, N'Event log', N'CMS.EventLog', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_EventLog">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EventID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="EventType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="5" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventTime" type="xs:dateTime" />
              <xs:element name="Source">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserID" type="xs:int" minOccurs="0" />
              <xs:element name="UserName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IPAddress" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteID" type="xs:int" minOccurs="0" />
              <xs:element name="EventUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventUserAgent" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventUrlReferrer" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_EventLog" />
      <xs:field xpath="EventID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EventID" fieldcaption="EventID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="26c35bd1-8066-4da8-a725-07ad0b2b2c6b" /><field column="EventType" fieldcaption="EventType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9faaf8cf-4252-49bf-bda1-9e196842868f" /><field column="EventTime" fieldcaption="EventTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3e07f490-7533-444d-8f56-f524582462dc" /><field column="Source" fieldcaption="Source" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e2efa2a-56af-4977-b7c3-ad308f96aa41" /><field column="EventCode" fieldcaption="EventCode" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0c924b46-c45d-4b5b-b0e0-61e8ae0689bb" /><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c093cdd5-eb7f-40ca-a750-f99fcee80d3b" /><field column="UserName" fieldcaption="UserName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="216b36b3-7748-4782-89bd-1a2098ceda4a" columnsize="250" visibility="none" ismacro="false" /><field column="IPAddress" fieldcaption="IPAddress" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ce09dd69-20b2-4d97-bbf3-7d19cd61904e" /><field column="NodeID" fieldcaption="NodeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d597f6eb-77be-4a4a-994d-b6b868ab6e4a" /><field column="DocumentName" fieldcaption="DocumentName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="52e8e3d1-efb4-4779-b906-a0e8bfb9c2b6" /><field column="EventDescription" fieldcaption="EventDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d5804fb1-6a0c-4cb0-988f-531f2c7c5ba2" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5b5f876-db4c-47bc-8b45-d6c83d3c9da9" /><field column="EventUrl" fieldcaption="EventUrl" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="76a68240-9806-48d0-9c4b-b17eed6f0fba" /><field column="EventMachineName" fieldcaption="EventMachineName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aa1bad36-c476-43f0-8713-c61bdfcecfe5" /><field column="EventUserAgent" fieldcaption="EvetUserAgent" visible="true" columntype="longtext" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="38bf0ded-0615-4469-beb6-b8770631b371" visibility="none" ismacro="false" /><field column="EventUrlReferrer" fieldcaption="EventUrlReferrer" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="2000" publicfield="false" spellcheck="true" guid="95f527b8-6fb0-4f4a-9afd-480f6a42841d" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_EventLog', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100816 11:05:20', 'e497827b-e411-4975-9277-b73235b21f87', 0, 1, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (143, N'Tree', N'cms.tree', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Tree">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="NodeID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="NodeAliasPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeAlias">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeClassID" type="xs:int" />
              <xs:element name="NodeParentID" type="xs:int" />
              <xs:element name="NodeLevel" type="xs:int" />
              <xs:element name="NodeACLID" type="xs:int" minOccurs="0" />
              <xs:element name="NodeSiteID" type="xs:int" />
              <xs:element name="NodeGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="NodeOrder" type="xs:int" minOccurs="0" />
              <xs:element name="IsSecuredNode" type="xs:boolean" minOccurs="0" />
              <xs:element name="NodeCacheMinutes" type="xs:int" minOccurs="0" />
              <xs:element name="NodeSKUID" type="xs:int" minOccurs="0" />
              <xs:element name="NodeDocType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeHeadTags" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeBodyElementAttributes" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeInheritPageLevels" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeChildNodesCount" type="xs:int" minOccurs="0" />
              <xs:element name="RequiresSSL" type="xs:int" minOccurs="0" />
              <xs:element name="NodeLinkedNodeID" type="xs:int" minOccurs="0" />
              <xs:element name="NodeOwner" type="xs:int" minOccurs="0" />
              <xs:element name="NodeCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="NodeLinkedNodeSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Tree" />
      <xs:field xpath="NodeID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="NodeID" fieldcaption="NodeID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0613f642-fa08-4ccf-91db-f5ca551d3638" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="NodeAliasPath" fieldcaption="NodeAliasPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42542959-12de-4bf6-bed2-d07f3c2abddc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeName" fieldcaption="NodeName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e238f7de-90c6-4519-8ae1-a414730ef028" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeAlias" fieldcaption="NodeAlias" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d89ab77f-84e6-43b9-9b9b-6937212b1fcb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeClassID" fieldcaption="NodeClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0efc576e-e707-46a1-9743-d98d3e1e180e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeParentID" fieldcaption="NodeParentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b307bb89-57ad-4f8e-be4d-ac3ef6a86433" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeLevel" fieldcaption="NodeLevel" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2869d3a1-0970-4f75-8dae-7e788fab732d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeACLID" fieldcaption="NodeACLID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f9173dcf-54dd-4175-9d6c-b0112033b967" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeSiteID" fieldcaption="NodeSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e5ea2dd1-ce02-4e0f-8514-1ebb22f17a2b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeGUID" fieldcaption="NodeGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="adbb0aee-8534-4305-93ba-edb98a4fa59f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="NodeOrder" fieldcaption="NodeOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c0154ae9-30e7-4232-8ff9-bc8c8c131ab9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IsSecuredNode" fieldcaption="IsSecuredNode" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7932cb3d-ffb3-4a67-b667-531a4ed31dcc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="NodeCacheMinutes" fieldcaption="NodeCacheMinutes" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bc0e11d5-ba1e-40bd-bc21-ed617bf04545" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeSKUID" fieldcaption="NodeSKUID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f7cb7bc-d9f2-49dc-83ef-1ea8b4ceb77e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeDocType" fieldcaption="NodeDocType" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="39b03150-e45e-4f60-8b51-8ba9b9e998ac" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="NodeHeadTags" fieldcaption="NodeHeadTags" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3b56e3cf-f960-4cf5-a617-5fb0bd97e40e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="NodeBodyElementAttributes" fieldcaption="NodeBodyElementAttributes" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="046d912a-4c8d-4462-9534-b8e1cbff23cd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="NodeInheritPageLevels" fieldcaption="NodeInheritPageLevels" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a6b88ee8-1fa6-4d46-84fc-f178238f9f5f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeChildNodesCount" fieldcaption="NodeChildNodesCount" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1633aa4-61f3-4620-97e6-fed9c80cbb03" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="RequiresSSL" fieldcaption="RequiresSSL" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="84423e46-399f-4416-94ce-45e68dbe0d4f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeLinkedNodeID" fieldcaption="NodeLinkedNodeID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f6f0915-b193-441b-9629-5913925a31e7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeOwner" fieldcaption="NodeOwner" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="320b49a6-e758-4400-b007-35f6ea15b801" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeCustomData" fieldcaption="NodeCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5f806aa-0624-491c-8f3c-b943d106fdc2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="NodeGroupID" fieldcaption="NodeGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6d3b21ac-2686-4766-bad1-80b133ca9ed6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeLinkedNodeSiteID" fieldcaption="NodeID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d1a9b609-1955-4583-89a7-bbaeb0ad0479" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Tree', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110509 14:14:40', '6d418504-6c8b-44f5-853b-10759216a050', 0, 1, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (144, N'Document', N'cms.document', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Document">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DocumentID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="DocumentName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentNamePath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentModifiedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DocumentModifiedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentForeignKeyValue" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentCreatedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentCreatedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DocumentCheckedOutByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentCheckedOutWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DocumentCheckedOutVersionHistoryID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentPublishedVersionHistoryID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentWorkflowStepID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentPublishFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DocumentPublishTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DocumentUrlPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentCulture">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentNodeID" type="xs:int" />
              <xs:element name="DocumentPageTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentPageKeyWords" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentPageDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentShowInSiteMap" type="xs:boolean" />
              <xs:element name="DocumentMenuItemHideInNavigation" type="xs:boolean" />
              <xs:element name="DocumentMenuCaption" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuStyle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemImage" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemLeftImage" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemRightImage" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentPageTemplateID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentMenuJavascript" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuRedirectUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentUseNamePathForUrlPath" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentStylesheetID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentContent" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuClass" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuStyleOver" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuClassOver" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemImageOver" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemLeftImageOver" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemRightImageOver" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuStyleHighlighted" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuClassHighlighted" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemImageHighlighted" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemLeftImageHighlighted" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemRightImageHighlighted" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentMenuItemInactive" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentExtensions" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentCampaign" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentTags" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentTagGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentWildcardRule" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentWebParts" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentRatingValue" type="xs:double" minOccurs="0" />
              <xs:element name="DocumentRatings" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentPriority" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentLastPublished" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DocumentUseCustomExtensions" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentGroupWebParts" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentCheckedOutAutomatically" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentTrackConversionName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentConversionValue" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentSearchExcluded" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentLastVersionName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentLastVersionNumber" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentIsArchived" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentLastVersionType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentLastVersionMenuRedirectUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentHash" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="32" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DocumentLogVisitActivity" type="xs:boolean" minOccurs="0" />
              <xs:element name="DocumentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="DocumentWorkflowCycleGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Document" />
      <xs:field xpath="DocumentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DocumentID" fieldcaption="DocumentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="04c53ea8-89c6-45fe-b9f8-11c869742937" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="DocumentGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b0dc7e57-96dd-4e5b-829a-4ba9bd84ac0f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="DocumentName" fieldcaption="DocumentName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="1e0f27f5-f59a-4fa1-871f-5c2d946453ca" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentNamePath" fieldcaption="DocumentNamePath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="1500" publicfield="false" spellcheck="true" guid="4afd853c-e3da-46dd-87c1-aa931e249b99" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentModifiedWhen" fieldcaption="DocumentModifiedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d86fd91f-9650-459d-a6a9-f101ec936cdf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DocumentModifiedByUserID" fieldcaption="DocumentModifiedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3db3f1c9-02b4-4d37-abd5-1298a9068ac1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentForeignKeyValue" fieldcaption="DocumentForeignKeyValue" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1ab4dfd9-6e8c-4b4d-9526-528890bc9c47" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentCreatedByUserID" fieldcaption="DocumentCreatedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="89690d5f-2c54-4788-8926-b38692719e0e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentCreatedWhen" fieldcaption="DocumentCreatedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82f0e9c1-9dfa-42a1-9b4a-a0090bbfad73" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DocumentCheckedOutByUserID" fieldcaption="DocumentCheckedOutByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d561f437-cf47-4681-a94e-085d9632b926" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentCheckedOutWhen" fieldcaption="DocumentCheckedOutWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="65160c58-f425-4370-baf6-b47dc987611d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DocumentCheckedOutVersionHistoryID" fieldcaption="DocumentCheckedOutVersionHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="31bf940c-d22c-4bf8-a430-776b6d4488c7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentPublishedVersionHistoryID" fieldcaption="DocumentPublishedVersionHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8229ebd2-c4d2-43bd-82e9-ae9af6146c97" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentWorkflowStepID" fieldcaption="DocumentWorkflowStepID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b7238cf1-94ac-4c59-87f4-f472f78245b2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentPublishFrom" fieldcaption="DocumentPublishFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5998e7fe-a503-4c5a-8711-9a4cbe77d8a3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DocumentPublishTo" fieldcaption="DocumentPublishTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="75b22166-d757-485f-901a-6636cabe930e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DocumentUrlPath" fieldcaption="DocumentUrlPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="ce4e33fb-c401-409f-bd58-d43bf642b1af" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentCulture" fieldcaption="DocumentCulture" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="e123ee40-049c-48b6-9dd8-a51b1e7da6b1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentNodeID" fieldcaption="DocumentNodeID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3bc79b36-ba8f-4fea-85a4-c72fdbc315d2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentPageTitle" fieldcaption="DocumentPageTitle" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a2df3057-5b8d-481b-9247-88b970c57a0b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentPageKeyWords" fieldcaption="DocumentPageKeyWords" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e6a709b2-d460-4f13-9abf-c0f100e97033" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentPageDescription" fieldcaption="DocumentPageDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4af8a60e-4d17-42c6-a830-1f6655aa807e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentShowInSiteMap" fieldcaption="DocumentShowInSiteMap" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b82973af-c753-43c4-b98a-1e33382f381f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentMenuItemHideInNavigation" fieldcaption="DocumentMenuItemHideInNavigation" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2551533d-3329-445f-a008-b2912c36b7d5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentMenuCaption" fieldcaption="DocumentMenuCaption" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="b057698c-757f-478f-8e69-4218eff45127" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuStyle" fieldcaption="DocumentMenuStyle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="35cafdb7-af8b-4243-bdce-babe46387a61" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemImage" fieldcaption="DocumentMenuItemImage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="722c4b74-aa68-4ab9-8a34-7cbc711ee0be" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemLeftImage" fieldcaption="DocumentMenuItemLeftImage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="06f35e92-e38e-4af4-a7fd-db3a7e625310" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemRightImage" fieldcaption="DocumentMenuItemRightImage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="8bf6ec0a-3fcc-4024-a9e4-ce022a762b02" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentPageTemplateID" fieldcaption="DocumentPageTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0d26da2b-4ac7-489b-a91b-88034794f569" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuJavascript" fieldcaption="DocumentMenuJavascript" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="538ab879-552c-4d63-b4e0-f41307286544" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuRedirectUrl" fieldcaption="DocumentMenuRedirectUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="6df14253-cc1c-4ea7-8cd7-6c8375c3c44c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentUseNamePathForUrlPath" fieldcaption="DocumentUseNamePathForUrlPath" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3091667f-22b4-4a4e-96ba-709e31f17a8f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentStylesheetID" fieldcaption="DocumentStylesheetID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1d74c412-a60d-4ff9-9966-549f3f9483fd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentContent" fieldcaption="DocumentContent" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7cc90d0b-19c9-49fa-a7ce-14104dd32586" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentMenuClass" fieldcaption="DocumentMenuClass" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="d47569fa-a1a5-476a-bb3f-25751812a790" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuStyleOver" fieldcaption="DocumentMenuStyleOver" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ea47e74e-8ca9-4e31-99a1-650081889f1a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuClassOver" fieldcaption="DocumentMenuClassOver" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="a374c26a-6273-47b2-a4a6-d75efbaf6372" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemImageOver" fieldcaption="DocumentMenuItemImageOver" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="7237fca8-7f53-477a-b075-516bd38d592e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemLeftImageOver" fieldcaption="DocumentMenuItemLeftImageOver" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="1d2f3721-8d4d-4a08-89af-be5a14d1fe9e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemRightImageOver" fieldcaption="DocumentMenuItemRightImageOver" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="e4b0b6ce-a5f7-4b66-bf93-6acfdf437593" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuStyleHighlighted" fieldcaption="DocumentMenuStyleHighlighted" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="e53e6cda-97c2-4fe4-9d04-8a9e639c1ee8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuClassHighlighted" fieldcaption="DocumentMenuClassHighlighted" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="ddaaa43a-c322-4d5e-9cd5-d7139f679883" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemImageHighlighted" fieldcaption="DocumentMenuItemImageHighlighted" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="dd5a4cb8-0fc7-4759-b062-6088b67b04ee" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemLeftImageHighlighted" fieldcaption="DocumentMenuItemLeftImageHighlighted" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="240a6893-095d-44f0-aaa6-e32443d64f34" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemRightImageHighlighted" fieldcaption="DocumentMenuItemRightImageHighlighted" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="3eaff1dd-1492-42f8-8035-1214d1dc0a6f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentMenuItemInactive" fieldcaption="DocumentMenuItemInactive" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e0b5fe75-ad44-4221-a175-54706d85f2ae" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentCustomData" fieldcaption="DocumentCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8c76cc14-a486-44f9-a1f2-1947192ca6a2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentExtensions" fieldcaption="DocumentExtensions" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="1c1cbb47-7832-4158-9d56-fbb34d6ae87a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentCampaign" fieldcaption="DocumentCampaign" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="948c554d-da2a-4e1d-b7ba-9236ab980725" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentTags" fieldcaption="DocumentTags" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6a33a0ff-ffd9-4615-a1c8-5e1bba780964" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentTagGroupID" fieldcaption="DocumentTagGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82d94a1c-a0e9-4496-bad0-6192217212bf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentWildcardRule" fieldcaption="DocumentWildcardRule" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="440" publicfield="false" spellcheck="true" guid="c827cdfb-bd56-4fe3-a781-5b6b12a3e79f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentWebParts" fieldcaption="DocumentWebParts" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="770989d4-ef83-48ac-b6a7-9800cedeb67a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="DocumentGroupWebParts" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="596af945-57d7-432f-a12a-7b16bf385504" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="DocumentRatingValue" fieldcaption="DocumentRatingValue" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="11320c2e-1c67-49e2-bd7a-7273e53f42db" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentRatings" fieldcaption="DocumentRatings" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="abd2bd0d-8911-4e29-abcb-896d7e8cb8bc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentPriority" fieldcaption="DocumentPriority" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="97bfb6a2-ab54-416e-9578-a38844a4b48e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentType" fieldcaption="DocumentType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="bb21f9de-f10c-43e9-b200-cc4c25c97acf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentLastPublished" fieldcaption="DocumentLastPublished" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="834785f6-f532-435f-b76f-c20dfd9399f6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DocumentUseCustomExtensions" fieldcaption="DocumentUseCustomExtensions" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aeae798e-898e-428c-8468-eff4126e0299" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentCheckedOutAutomatically" fieldcaption="DocumentCheckedOutAutomatically" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="false" guid="dbb95f9e-3efd-485d-a5a5-1a4ccb455fbc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentTrackConversionName" fieldcaption="Track conversion name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" columnsize="200" publicfield="false" spellcheck="false" guid="ee4ecdbc-988a-4b25-9a35-e048e0bc0dea" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="DocumentConversionValue" fieldcaption="Document conversion value" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" columnsize="100" publicfield="false" spellcheck="false" guid="84eb8c75-f049-475a-8eae-c69a962afda6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentSearchExcluded" fieldcaption="Document search excluded" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="05110f5f-a8dd-4e30-828c-ef767cfdb1e4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentLastVersionName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="false" guid="8322bc41-81a7-4cad-950a-d6ec1a742c6a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="DocumentLastVersionNumber" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="false" guid="88338d99-7f91-4c20-8c84-a3dd97cb53a1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="DocumentIsArchived" fieldcaption="DocumentID" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="41cad1db-9aa7-4818-ae85-a9651578e554" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="DocumentLastVersionType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="false" guid="86ff7a76-a265-4f78-8e2b-3788804d8bcc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="DocumentLastVersionMenuRedirectUrl" fieldcaption="DocumentLastVersionMenuRedirectUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="3d1e8b7c-3c1d-4fd7-b9e3-0e3ca29d0f01" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="DocumentHash" fieldcaption="DocumentHash" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="32" publicfield="false" spellcheck="true" guid="7311e7a0-7113-4b2a-b136-39b13464eba0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><FilterEnabled>False</FilterEnabled></settings></field><field column="DocumentLogVisitActivity" fieldcaption="DocumentLogVisitActivity" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c1517e1-c1bc-4341-b879-a8e29349b23b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DocumentWorkflowCycleGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7a8dbf8e-a387-4068-b255-247eff08351a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Document', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110917 15:16:48', '4d58a766-f13b-48fe-950b-63dbc2aeca69', 0, 1, 0, N'', 0, N'', N'', N'', N'', N'<search><item searchable="True" name="SKUID" tokenized="False" content="False" id="9e5bd0f9-4912-45b9-b428-277e53a4b84d" /><item searchable="True" name="SKUNumber" tokenized="False" content="False" id="38da5829-d066-4528-99ef-a8b12cee973e" /><item searchable="False" name="SKUName" tokenized="False" content="True" id="c9a93a77-ce82-415b-9f03-ceb18517445f" /><item searchable="False" name="SKUDescription" tokenized="False" content="True" id="e858600b-af66-4233-a0f8-190a032fb4ea" /><item searchable="True" name="SKUPrice" tokenized="False" content="False" id="eb5acaeb-61bd-42df-9b28-b1b38ca2362c" /><item searchable="True" name="SKUEnabled" tokenized="False" content="False" id="1ecb2478-fa59-4510-9887-d8ca33121fae" /><item searchable="True" name="SKUDepartmentID" tokenized="False" content="False" id="c1d75d4e-a024-4372-a69d-4b6587208001" /><item searchable="True" name="SKUManufacturerID" tokenized="False" content="False" id="0f5a0fab-248a-41eb-80c2-19b206d7ea1a" /><item searchable="True" name="SKUInternalStatusID" tokenized="False" content="False" id="1d9838d6-76f6-462a-abe4-06d1e65e3b96" /><item searchable="True" name="SKUPublicStatusID" tokenized="False" content="False" id="f5d5bdf3-49f9-4322-b648-caadb87d909d" /><item searchable="True" name="SKUSupplierID" tokenized="False" content="False" id="d78b133f-8d02-4f06-b4f6-b323f97b2600" /><item searchable="True" name="SKUAvailableInDays" tokenized="False" content="False" id="f7879606-b34b-42a6-a169-1b5b6cbf08f9" /><item searchable="False" name="SKUGUID" tokenized="False" content="False" id="09ae166f-80ee-4163-88d6-b76cec8adf9a" /><item searchable="False" name="SKUImagePath" tokenized="False" content="True" id="63b2a567-73e4-4661-b9ce-4eab52d12b6b" /><item searchable="True" name="SKUWeight" tokenized="False" content="False" id="5324e439-e605-4e08-85e0-fe922c48f886" /><item searchable="True" name="SKUWidth" tokenized="False" content="False" id="26eddc94-5df6-4fda-b094-2af908299e24" /><item searchable="True" name="SKUDepth" tokenized="False" content="False" id="146aee79-5f6b-46cb-94b0-09e4cfdc66cf" /><item searchable="True" name="SKUHeight" tokenized="False" content="False" id="9839ae57-e97c-47f7-b63d-0122397ca57f" /><item searchable="True" name="SKUAvailableItems" tokenized="False" content="False" id="bff042fa-3229-46f3-9c20-af26d89126c8" /><item searchable="True" name="SKUSellOnlyAvailable" tokenized="False" content="False" id="56d70204-845e-4e49-bc48-52448d3d4184" /><item searchable="True" name="SKUCustomData" tokenized="False" content="False" id="e900a950-bb2b-4cf8-b3ca-fb82c2db127f" /><item searchable="True" name="SKUOptionCategoryID" tokenized="False" content="False" id="bed44ce1-a95a-42a8-b676-0d055836fdc2" /><item searchable="True" name="SKUOrder" tokenized="False" content="False" id="fcdd3251-4602-4485-8a43-4572cbfc25f1" /><item searchable="True" name="SKULastModified" tokenized="False" content="False" id="59af621d-d719-42c7-bc4c-749d58693986" /><item searchable="True" name="SKUCreated" tokenized="False" content="False" id="5d281616-f0bb-4709-b69c-d449f444d143" /><item searchable="False" name="SKUSiteID" tokenized="False" content="False" id="80508dd5-8e01-4858-9419-c024dd09edb7" /><item searchable="False" name="SKUProductType" tokenized="False" content="False" id="30cee277-cec3-475a-9343-4b619b62e9c6" /><item searchable="False" name="SKUMembershipGUID" tokenized="False" content="False" id="c513ae3b-7ae1-4eb3-857e-9893154b82ad" /><item searchable="False" name="SKUValidity" tokenized="False" content="False" id="d604f64f-f85e-4e7d-bb7a-11f032ec0a70" /><item searchable="False" name="SKUValidFor" tokenized="False" content="False" id="490090bc-5a17-4d2c-9515-c087a80eaa42" /><item searchable="False" name="SKUValidUntil" tokenized="False" content="False" id="af154af4-2f5d-4745-a345-ab0b79a2bac6" /><item searchable="False" name="SKUMaxDownloads" tokenized="False" content="False" id="6da42f29-4aa4-4080-94b6-c922504a8078" /><item searchable="False" name="SKUInventoryRemoveBundle" tokenized="False" content="False" id="04cc6cde-738e-4334-87e0-32622252f68b" /><item searchable="False" name="SKUPrivateDonation" tokenized="False" content="False" id="c33f9153-4378-44c4-a9b2-0fc279add2d9" /><item searchable="False" name="SKUMinPrice" tokenized="False" content="False" id="ed07502f-6570-4277-b95f-107ba97c9ee2" /><item searchable="False" name="SKUMaxPrice" tokenized="False" content="False" id="c49358c8-fbae-4551-9ecf-e6b7ab026978" /><item searchable="False" name="SKUNeedsShipping" tokenized="False" content="False" id="3ce2232c-9b72-403c-a88e-e7d0a9d619ec" /><item searchable="False" name="SKUMaxItemsInOrder" tokenized="False" content="False" id="e7d89378-7dbf-4416-894a-af4dfa936767" /><item searchable="False" name="SKUConversionName" tokenized="False" content="False" id="e195d8ed-a5d2-4543-bf1c-ced34ed247d0" /><item searchable="False" name="SKUConversionValue" tokenized="False" content="False" id="f37b8f0c-2de2-47ff-8e6c-fd30b73a3ad0" /><item searchable="True" name="DocumentID" tokenized="False" content="False" id="b25a6785-8ea0-4d44-afa8-12a075462793" /><item searchable="False" name="DocumentGUID" tokenized="False" content="False" id="b457ac92-f813-4904-9a79-c951575af587" /><item searchable="False" name="DocumentName" tokenized="True" content="True" id="4719152b-b87d-4cb0-8d21-c89a2a9bed04" /><item searchable="False" name="DocumentNamePath" tokenized="True" content="True" id="54db55de-5f78-40d4-814c-ffedd755155b" /><item searchable="True" name="DocumentModifiedWhen" tokenized="False" content="False" id="f262c02b-2fe8-47cf-a50b-9db1e39f78aa" /><item searchable="True" name="DocumentModifiedByUserID" tokenized="False" content="False" id="0062559b-2f00-451c-8355-854201b0eafa" /><item searchable="True" name="DocumentForeignKeyValue" tokenized="False" content="False" id="692431ac-9a4f-4bb3-885d-c9c1a0f13559" /><item searchable="True" name="DocumentCreatedByUserID" tokenized="False" content="False" id="a7e6954b-2939-439e-b017-c6c889e15396" /><item searchable="True" name="DocumentCreatedWhen" tokenized="False" content="False" id="dc4b845a-1eb4-438a-9587-6ef5175d5206" /><item searchable="True" name="DocumentCheckedOutByUserID" tokenized="False" content="False" id="ccdd709b-42a8-46c4-b1a5-27ee1b75786e" /><item searchable="True" name="DocumentCheckedOutWhen" tokenized="False" content="False" id="546eb5d6-3544-4be7-96d8-8a58996c0ecd" /><item searchable="True" name="DocumentCheckedOutVersionHistoryID" tokenized="False" content="False" id="172aef66-3c7d-42ae-86ed-0305a7f75ad2" /><item searchable="True" name="DocumentPublishedVersionHistoryID" tokenized="False" content="False" id="9bd7b2f8-3657-4034-9777-e6fb223b1a8a" /><item searchable="True" name="DocumentWorkflowStepID" tokenized="False" content="False" id="e7f8a039-3bf2-4f7a-ad2d-d7aecc4dff2b" /><item searchable="True" name="DocumentPublishFrom" tokenized="False" content="False" id="525a1c87-5901-4c1c-afb5-72c086bf4861" /><item searchable="True" name="DocumentPublishTo" tokenized="False" content="False" id="cb991950-0166-4982-a165-a61022f63121" /><item searchable="False" name="DocumentUrlPath" tokenized="True" content="True" id="9d27a580-39f7-4dea-8814-bece614dd521" /><item searchable="True" name="DocumentCulture" tokenized="True" content="False" id="5984eb5e-b28c-4a65-9d97-d7d40ebaceaa" /><item searchable="True" name="DocumentNodeID" tokenized="False" content="False" id="82acc77a-65e6-4f24-96ff-1be6d9a69f6f" /><item searchable="False" name="DocumentPageTitle" tokenized="True" content="True" id="8b4ceaf1-90b7-4e54-ab11-5ad6bb427498" /><item searchable="False" name="DocumentPageKeyWords" tokenized="True" content="True" id="ee2d76b7-a045-4bef-afba-966a38ba11e4" /><item searchable="False" name="DocumentPageDescription" tokenized="True" content="True" id="46c77493-0d34-44ea-adc1-4b67e88e61fb" /><item searchable="True" name="DocumentShowInSiteMap" tokenized="False" content="False" id="13e30c2f-6ace-4ca2-a3cc-02eb5fe962f1" /><item searchable="True" name="DocumentMenuItemHideInNavigation" tokenized="False" content="False" id="09bef4aa-0068-4427-b404-6bf012f19c8e" /><item searchable="False" name="DocumentMenuCaption" tokenized="True" content="True" id="0f4039cc-5245-478b-bae2-80fe7298eb7c" /><item searchable="False" name="DocumentMenuStyle" tokenized="False" content="False" id="0113fe0f-df19-4f25-a0b2-8b8b7445c348" /><item searchable="False" name="DocumentMenuItemImage" tokenized="False" content="False" id="fd469d59-2e8f-4b86-b10e-5fb0eb86a666" /><item searchable="False" name="DocumentMenuItemLeftImage" tokenized="False" content="False" id="f5129d8c-9e6e-4ada-b6a3-8313fb96febc" /><item searchable="False" name="DocumentMenuItemRightImage" tokenized="False" content="False" id="74bb3f8f-42e0-4f3a-86c4-c9ff632c0fb5" /><item searchable="False" name="DocumentPageTemplateID" tokenized="False" content="False" id="9fb61766-0583-4c24-ae24-fefb67749ffe" /><item searchable="False" name="DocumentMenuJavascript" tokenized="False" content="False" id="0921e828-372f-4430-a691-900df863981d" /><item searchable="False" name="DocumentMenuRedirectUrl" tokenized="False" content="False" id="f2808657-32f5-4b11-9c3b-e752b03a8ab0" /><item searchable="False" name="DocumentUseNamePathForUrlPath" tokenized="False" content="False" id="51962e9e-7460-47ae-ba57-fc1a8c6a49c0" /><item searchable="False" name="DocumentStylesheetID" tokenized="False" content="False" id="b3d86de5-4797-4080-959e-db250b359546" /><item searchable="False" name="DocumentContent" tokenized="True" content="True" id="12d24086-64ea-4bde-b967-bd4bd9a95e38" /><item searchable="False" name="DocumentMenuClass" tokenized="False" content="False" id="515f9ef6-92c4-48ed-8020-bfe76016c2b6" /><item searchable="False" name="DocumentMenuStyleOver" tokenized="False" content="False" id="f8ede72a-2e5d-4733-a42c-8770c8673105" /><item searchable="False" name="DocumentMenuClassOver" tokenized="False" content="False" id="08567dc4-bc74-4220-a209-95fd925259cb" /><item searchable="False" name="DocumentMenuItemImageOver" tokenized="False" content="False" id="42e6072b-8c75-4994-a15e-f43bbae0cba9" /><item searchable="False" name="DocumentMenuItemLeftImageOver" tokenized="False" content="False" id="80ad93f1-d393-48db-9298-23363798da3d" /><item searchable="False" name="DocumentMenuItemRightImageOver" tokenized="False" content="False" id="f528e0b4-1153-4f12-ac79-4f49127f5648" /><item searchable="False" name="DocumentMenuStyleHighlighted" tokenized="False" content="False" id="bf2da130-d447-449f-8dba-e51d0ab256c7" /><item searchable="False" name="DocumentMenuClassHighlighted" tokenized="False" content="False" id="f7b12808-4047-4885-b53b-6fcf2cb74d07" /><item searchable="False" name="DocumentMenuItemImageHighlighted" tokenized="False" content="False" id="6878f475-e13b-45bb-acf5-2e9e9ccad718" /><item searchable="False" name="DocumentMenuItemLeftImageHighlighted" tokenized="False" content="False" id="fde996ab-067d-4a74-a39e-50d8bb227ec7" /><item searchable="False" name="DocumentMenuItemRightImageHighlighted" tokenized="False" content="False" id="5b7266ef-a26e-4200-81b3-c5e18fc633f7" /><item searchable="False" name="DocumentMenuItemInactive" tokenized="False" content="False" id="0a475a05-4ca3-4c96-b318-9887dfd9ea2b" /><item searchable="False" name="DocumentCustomData" tokenized="False" content="False" id="3bdd4fb3-938d-4050-9ccd-66ce1198b6b7" /><item searchable="False" name="DocumentExtensions" tokenized="False" content="False" id="27d80bac-21f4-46fa-846f-fc134d910bcf" /><item searchable="False" name="DocumentCampaign" tokenized="False" content="False" id="46af3bce-adca-4b88-839d-a9ac85758c0c" /><item searchable="True" name="DocumentTags" tokenized="True" content="True" id="04d11e09-51db-4cde-815b-ff0b28d8c716" /><item searchable="True" name="DocumentTagGroupID" tokenized="False" content="False" id="2f243f1f-afad-4568-9574-6ca6d0dad5a7" /><item searchable="False" name="DocumentWildcardRule" tokenized="False" content="False" id="e851cd6c-04b2-48e4-8a6a-077332bf267e" /><item searchable="False" name="DocumentWebParts" tokenized="True" content="True" id="66661add-0336-448f-b40f-10d45f12dfcd" /><item searchable="False" name="DocumentGroupWebParts" tokenized="False" content="False" id="aaca8fc6-6ded-4891-89fb-19f352306a25" /><item searchable="True" name="DocumentRatingValue" tokenized="False" content="False" id="b3312d6f-817a-4a02-9279-ed8d8a4f0468" /><item searchable="True" name="DocumentRatings" tokenized="False" content="False" id="25b2f25e-a2bb-4d26-a1ce-7b52fb77dd23" /><item searchable="True" name="DocumentPriority" tokenized="False" content="False" id="7797322b-7c36-4624-938a-90e6cb284000" /><item searchable="True" name="DocumentType" tokenized="False" content="False" id="abd39eaa-a0a6-430e-aa6b-812fd7eeb33d" /><item searchable="True" name="DocumentLastPublished" tokenized="False" content="False" id="f4b2cbf7-ae6a-43d7-9310-9a55fd722019" /><item searchable="False" name="DocumentUseCustomExtensions" tokenized="False" content="False" id="ee3e867d-5b32-4e55-8e99-4a3ba221b9f5" /><item searchable="False" name="DocumentCheckedOutAutomatically" tokenized="False" content="False" id="5e5ee85a-a942-4018-9fde-7e2b3142e9f9" /><item searchable="False" name="DocumentTrackConversionName" tokenized="False" content="False" id="bb7466b3-8f4c-4812-9d17-30b8fdf9bc7c" /><item searchable="False" name="DocumentConversionValue" tokenized="False" content="False" id="2b0a173f-89c0-4018-995a-b998043c6b42" /><item searchable="True" name="DocumentSearchExcluded" tokenized="False" content="False" id="42f446ee-9818-4596-8124-54a38f64aa05" /><item searchable="False" name="DocumentLastVersionName" tokenized="False" content="False" id="d088ccc5-ec04-43d2-b787-9fe20b358687" /><item searchable="False" name="DocumentLastVersionNumber" tokenized="False" content="False" id="656f28e5-9eea-402b-b540-d3c40d52dbaa" /><item searchable="False" name="DocumentIsArchived" tokenized="False" content="False" id="52355126-7729-4d32-92e5-db04ad722528" /><item searchable="False" name="DocumentLastVersionType" tokenized="False" content="False" id="e50b857d-408b-4da9-a04b-c9224a8a9114" /><item searchable="False" name="DocumentLastVersionMenuRedirectUrl" tokenized="False" content="False" id="92092314-0666-4ca0-b158-261e3500d177" /><item searchable="False" name="DocumentHash" tokenized="False" content="False" id="ccbc9e3d-0f66-4e6e-afea-a84fcb7fcd0d" /><item searchable="False" name="DocumentLogVisitActivity" tokenized="False" content="False" id="8a1a1d17-9dac-4175-bb4b-50b6b237bb2e" /><item searchable="False" name="DocumentWorkflowCycleGUID" tokenized="False" content="False" id="632e9004-f401-4493-8c68-7b2b331fc2ee" /><item searchable="True" name="NodeID" tokenized="False" content="False" id="11770a64-cd80-4f99-9e14-813b106c0283" /><item searchable="True" name="NodeAliasPath" tokenized="False" content="False" id="e731bdcc-bb37-491b-95e1-b467335bca00" /><item searchable="False" name="NodeName" tokenized="True" content="True" id="a5d5962c-640b-4567-96ca-71c4fada19aa" /><item searchable="False" name="NodeAlias" tokenized="True" content="True" id="25fed5d4-340a-4d90-955d-293f983eefb8" /><item searchable="True" name="NodeClassID" tokenized="False" content="False" id="bdc075a3-f8eb-4e48-b7ea-6575c07d1c76" /><item searchable="True" name="NodeParentID" tokenized="False" content="False" id="c76eb978-4fb5-4319-9dd6-9e944ecac7f6" /><item searchable="True" name="NodeLevel" tokenized="False" content="False" id="4b1ec13f-1208-4c3f-990a-bcde11d2a803" /><item searchable="True" name="NodeACLID" tokenized="False" content="False" id="befa2056-c6b6-4726-a0f5-8c9b053923c1" /><item searchable="True" name="NodeSiteID" tokenized="False" content="False" id="be5037e4-9d70-4423-9328-90cf8bf7211e" /><item searchable="True" name="NodeGUID" tokenized="False" content="False" id="a58b449f-7f2d-4ae0-b97e-813b7b212ada" /><item searchable="True" name="NodeOrder" tokenized="False" content="False" id="9235a0c5-0723-48ca-93d0-8e82a9ebd946" /><item searchable="True" name="IsSecuredNode" tokenized="False" content="False" id="2e336801-92d9-4566-8ab1-4d520cc34df2" /><item searchable="True" name="NodeCacheMinutes" tokenized="False" content="False" id="a5273557-2fc4-4c13-8666-d588f124b525" /><item searchable="True" name="NodeSKUID" tokenized="False" content="False" id="02876508-04e1-4be4-bf0e-4bcc196b72e4" /><item searchable="False" name="NodeDocType" tokenized="False" content="False" id="a8e8be82-374a-4fd5-94e0-97460353e0ef" /><item searchable="False" name="NodeHeadTags" tokenized="True" content="True" id="531a5a44-5ee5-45de-b4c5-4c9e3fef9262" /><item searchable="False" name="NodeBodyElementAttributes" tokenized="False" content="False" id="757892ec-68c8-4dfc-9c0d-c08ef8706832" /><item searchable="False" name="NodeInheritPageLevels" tokenized="False" content="False" id="c2ee5ec2-13a3-42a7-bc45-2ec3943c32fe" /><item searchable="True" name="NodeChildNodesCount" tokenized="False" content="False" id="7d3c0120-6c23-421f-a0fa-f5f8477a5009" /><item searchable="True" name="RequiresSSL" tokenized="False" content="False" id="be4b80b5-65db-43b8-bf1a-c3f4a0c30432" /><item searchable="True" name="NodeLinkedNodeID" tokenized="False" content="False" id="65c20e48-9628-40d4-8abe-476eb7ad0f9a" /><item searchable="True" name="NodeOwner" tokenized="False" content="False" id="6dd9cffa-1c06-44be-b59d-7034c45cc223" /><item searchable="False" name="NodeCustomData" tokenized="True" content="True" id="60dfbf53-d766-4770-a59b-a4074a797c90" /><item searchable="True" name="NodeGroupID" tokenized="False" content="False" id="08088caa-e93e-4311-a4bc-85859818f46e" /><item searchable="False" name="NodeLinkedNodeSiteID" tokenized="False" content="False" id="e06f1a8e-dd8e-401a-b243-99a631e70c5e" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (145, N'Class', N'cms.class', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Class">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ClassID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ClassDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassUsesVersioning" type="xs:boolean" />
              <xs:element name="ClassIsDocumentType" type="xs:boolean" />
              <xs:element name="ClassIsCoupledClass" type="xs:boolean" />
              <xs:element name="ClassXmlSchema">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassFormDefinition">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassEditingPageUrl">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassListPageUrl">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassNodeNameSource">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassTableName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassViewPageUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassPreviewPageUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassFormLayout" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassNewPageUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassShowAsSystemTable" type="xs:boolean" />
              <xs:element name="ClassUsePublishFromTo" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassShowTemplateSelection" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassSKUMappings" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassIsMenuItemType" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassNodeAliasSource" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassDefaultPageTemplateID" type="xs:int" minOccurs="0" />
              <xs:element name="ClassLastModified" type="xs:dateTime" />
              <xs:element name="ClassGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ClassCreateSKU" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassIsProduct" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassIsCustomTable" type="xs:boolean" />
              <xs:element name="ClassShowColumns" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassLoadGeneration" type="xs:int" />
              <xs:element name="ClassSearchTitleColumn" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassSearchContentColumn" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassSearchImageColumn" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassSearchCreationDateColumn" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassSearchSettings" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassInheritsFromClassID" type="xs:int" minOccurs="0" />
              <xs:element name="ClassSearchEnabled" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassSKUDefaultDepartmentName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassSKUDefaultDepartmentID" type="xs:int" minOccurs="0" />
              <xs:element name="ClassContactMapping" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ClassContactOverwriteEnabled" type="xs:boolean" minOccurs="0" />
              <xs:element name="ClassSKUDefaultProductType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Class" />
      <xs:field xpath="ClassID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ClassID" fieldcaption="ClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0baf1038-97e3-4e65-880c-35da63fee40b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ClassDisplayName" fieldcaption="ClassDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ff01ffa6-bc9a-4760-860e-90e2316256ca" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassName" fieldcaption="ClassName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7797c91f-73b9-4100-b490-34207538c356" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassUsesVersioning" fieldcaption="ClassUsesVersioning" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ba0272bd-4933-470d-880f-c3f7c6b98eb7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassIsDocumentType" fieldcaption="ClassIsDocumentType" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5525a6c9-933d-4bc5-b659-070ecbd8bb9e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassIsCoupledClass" fieldcaption="ClassIsCoupledClass" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6b5e4c5e-9d7c-44f4-a5d8-b22334cb9f95" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassXmlSchema" fieldcaption="ClassXmlSchema" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3db53773-1efb-444e-9a4f-7366788b5564" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ClassFormDefinition" fieldcaption="ClassFormDefinition" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8d9fbca4-3b93-4053-bae2-0cb2fa107e2b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ClassEditingPageUrl" fieldcaption="ClassEditingPageUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d839ce92-981f-4ff3-86c7-f9018fa540a4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassListPageUrl" fieldcaption="ClassListPageUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e07d265c-31fd-406f-9541-16cac0262eaf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassNodeNameSource" fieldcaption="ClassNodeNameSource" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f629c749-07df-4771-86b1-c6cc01a5dd05" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassTableName" fieldcaption="ClassTableName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d760ba06-04f3-4e95-9b0b-28d4ce696946" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassViewPageUrl" fieldcaption="ClassViewPageUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="79a6670e-2b88-4bdb-9091-5a7a4f458a18" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassPreviewPageUrl" fieldcaption="ClassPreviewPageUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6857dea5-dbef-4dd1-88d5-6ba7f22b06b9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassFormLayout" fieldcaption="ClassFormLayout" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb01e0b3-1b93-4d91-93f8-54d8f6f91b15" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ClassNewPageUrl" fieldcaption="ClassNewPageUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="066a9293-b55f-47fc-9c7c-e7f7e8c27644" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassShowAsSystemTable" fieldcaption="ClassShowAsSystemTable" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d87cfae9-2047-4017-a690-136fd115ace2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassUsePublishFromTo" fieldcaption="ClassUsePublishFromTo" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4d010adc-2aef-457b-a562-bb5bb0321769" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassShowTemplateSelection" fieldcaption="ClassShowTemplateSelection" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="84b93b79-ce58-44de-8893-31e7eca71bc8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassSKUMappings" fieldcaption="ClassSKUMappings" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="23b8b405-f437-437f-9c02-7dce6982c0b1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ClassIsMenuItemType" fieldcaption="ClassIsMenuItemType" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3287da27-313b-49a1-ac44-ec688c65e0ed" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassNodeAliasSource" fieldcaption="ClassNodeAliasSource" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="63944d9a-287a-46f5-8386-1976159b07da" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassDefaultPageTemplateID" fieldcaption="ClassDefaultPageTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b1532089-6b7c-4367-9e09-3e7c5911af56" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassSKUDefaultDepartmentID" fieldcaption="ClassSKUDefaultDepartmentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="12639c69-eedf-4384-aac9-13a735cd9b8b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ClassSKUDefaultDepartmentName" fieldcaption="ClassSKUDefaultDepartmentName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="10215ba1-1259-43be-9e88-5ec13e1347e5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ClassLastModified" fieldcaption="ClassLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="77b800c3-26ee-48bb-9608-1054f69aa4b8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ClassGUID" fieldcaption="ClassGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3d1a72e7-721a-40e2-97e6-d5fdf19507f2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ClassCreateSKU" fieldcaption="ClassCreateSKU" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1398a9ae-7b15-4f75-9eda-554897bdae77" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassIsProduct" fieldcaption="ClassIsProduct" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f8608a0-bb65-435a-afc7-3731390f52d7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassLoadGeneration" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="27012dbf-cb1d-48f5-b5a6-8ea49a1cb3ae" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ClassSearchTitleColumn" fieldcaption="ClassSearchTitleColumn" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="cac829b9-641f-48dd-bcac-5c6bbb1bb6a4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassSearchContentColumn" fieldcaption="ClassSearchContentColumn" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="633d3e3a-f961-4959-89d2-b0a9ba6a6f76" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassSearchImageColumn" fieldcaption="ClassSearchImageColumn" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="c54398e5-ee11-4188-8b4b-5133bca4e00c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassSearchCreationDateColumn" fieldcaption="ClassSearchCreationDateColumn" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ee76b6d7-4341-4138-b54b-75f86f063233" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ClassSearchSettings" fieldcaption="ClassSearchSettings" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a2cb82b7-0186-400f-99c2-795dfbb5d871" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ClassInheritsFromClassID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="c08dc0f4-80db-48b1-97ba-696d2fd6206f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="ClassSearchEnabled" fieldcaption="ClassID" visible="false" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="220bed13-39f7-4b8b-8cb4-40ae38b7e491" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ClassContactMapping" fieldcaption="ClassContactMapping" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cf4c46fd-97a6-4e0e-a0b4-2d077569aa06" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ClassContactOverwriteEnabled" fieldcaption="Allow overwrite contact information" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" fielddescription="This setting allows to overwrite existing contact information with submitted data. If the setting is false, only empty values of a contact can be filled." publicfield="false" spellcheck="true" guid="75dfeace-100a-4963-b1b0-05471f116c91" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ClassSKUDefaultProductType" fieldcaption="ClassSKUDefaultProductType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="da3a6813-c7e3-4780-a297-5cf0b7f5a0c3" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Class', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110718 20:53:44', 'd7e91104-201b-4b11-9550-e93ad9a4d81f', 0, 1, 0, N'', 1, N'ClassDisplayName', N'0', N'', N'ClassLastModified', N'<search><item searchable="True" name="ClassID" tokenized="False" content="False" id="1c2051cd-c298-43ad-b099-fb8a31f4abdb" /><item searchable="False" name="ClassDisplayName" tokenized="True" content="True" id="cc3bfcc3-36a8-4b1e-8780-885212b7a129" /><item searchable="False" name="ClassName" tokenized="True" content="True" id="059bfd1f-4496-436e-bda6-31ae751d553c" /><item searchable="True" name="ClassUsesVersioning" tokenized="False" content="False" id="a53c3c63-1a5e-4d9e-9f72-fac632e4f59e" /><item searchable="True" name="ClassIsDocumentType" tokenized="False" content="False" id="e99b92f3-6125-4bc0-a042-8504bff95971" /><item searchable="True" name="ClassIsCoupledClass" tokenized="False" content="False" id="2768efbd-dc43-4520-a380-998c75204a53" /><item searchable="False" name="ClassXmlSchema" tokenized="True" content="True" id="5d60a128-33bc-4f66-a025-8bf6f5aef4c9" /><item searchable="False" name="ClassFormDefinition" tokenized="True" content="True" id="a57a6823-118e-4f6b-a45f-8cde928be41e" /><item searchable="False" name="ClassEditingPageUrl" tokenized="True" content="True" id="a98a90da-760b-42b2-8e6e-ca02aaa0dfe6" /><item searchable="False" name="ClassListPageUrl" tokenized="True" content="True" id="26a55a95-20a9-43b8-baa8-090a22a06c37" /><item searchable="False" name="ClassNodeNameSource" tokenized="True" content="True" id="1fda191e-e73c-4875-b513-9d3264fc5b67" /><item searchable="False" name="ClassTableName" tokenized="True" content="True" id="37e4a28a-e022-44ce-bc67-57510898df62" /><item searchable="False" name="ClassViewPageUrl" tokenized="True" content="True" id="29a4abe3-dc3b-4e55-a52b-efeda6e95563" /><item searchable="False" name="ClassPreviewPageUrl" tokenized="True" content="True" id="a10836a0-6ca9-4c11-ac9c-05a195a424b3" /><item searchable="False" name="ClassFormLayout" tokenized="True" content="True" id="2d0bcd70-506a-47e8-abf3-a9ab247ba35a" /><item searchable="False" name="ClassNewPageUrl" tokenized="True" content="True" id="6a947437-ed63-4848-8776-96fa18c2d066" /><item searchable="True" name="ClassShowAsSystemTable" tokenized="False" content="False" id="5d06611d-2b88-4214-bba0-4b2513935354" /><item searchable="True" name="ClassUsePublishFromTo" tokenized="False" content="False" id="730df344-6045-47d2-ba57-64cfdec02e6c" /><item searchable="True" name="ClassShowTemplateSelection" tokenized="False" content="False" id="831c6d6f-339c-44fc-addd-defc91777129" /><item searchable="False" name="ClassSKUMappings" tokenized="True" content="True" id="37b4d22a-472b-40e2-a4e6-2b226c0adf5d" /><item searchable="True" name="ClassIsMenuItemType" tokenized="False" content="False" id="ea9ac100-2357-4ed2-9e06-176e9e5ae08b" /><item searchable="False" name="ClassNodeAliasSource" tokenized="True" content="True" id="3d0f4ab7-4616-4dc3-9b55-9bc7a389e232" /><item searchable="True" name="ClassDefaultPageTemplateID" tokenized="False" content="False" id="7eb08a63-2da3-4dc6-933c-211d815fec7f" /><item searchable="True" name="ClassSKUDefaultDepartmentID" tokenized="False" content="False" id="5417800f-ae6b-4c3f-bfa4-e635b4d02b3a" /><item searchable="True" name="ClassLastModified" tokenized="False" content="False" id="7559bd37-f81c-40f3-a365-101d84c454bd" /><item searchable="False" name="ClassGUID" tokenized="False" content="False" id="e69c8e58-7e42-4327-b2b4-1a8274adf679" /><item searchable="True" name="ClassCreateSKU" tokenized="False" content="False" id="b625cdda-7351-4bdf-b158-5171ab7e6749" /><item searchable="True" name="ClassIsProduct" tokenized="False" content="False" id="50ab1fed-cbe2-4289-b344-293bfe0e200a" /><item searchable="True" name="ClassLoadGeneration" tokenized="False" content="False" id="15056457-0b99-46a6-8c90-f1e307921e40" /><item searchable="False" name="ClassSearchTitleColumn" tokenized="True" content="True" id="ade29f34-a771-403e-a3d8-56e83af38d44" /><item searchable="False" name="ClassSearchContentColumn" tokenized="True" content="True" id="03c36ead-5fba-4c45-9e59-1e32d38f0359" /><item searchable="False" name="ClassSearchImageColumn" tokenized="True" content="True" id="15770901-f850-4f1e-827e-d13747f7f225" /><item searchable="False" name="ClassSearchCreationDateColumn" tokenized="True" content="True" id="d2cc2153-0b80-4e46-84e2-7aef8ceb2d6b" /><item searchable="False" name="ClassSearchSettings" tokenized="True" content="True" id="981d1582-133c-4a8b-803c-010d65f559fc" /><item searchable="True" name="ClassInheritsFromClassID" tokenized="False" content="False" id="99a8d175-d880-4347-a9e0-0328df28c2c3" /><item searchable="True" name="ClassSearchEnabled" tokenized="False" content="False" id="a3720b7f-d6a5-48d5-b4af-1c60421418ed" /></search>', NULL, 0, N'', NULL, NULL, 0, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (157, N'Page template', N'cms.pagetemplate', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_PageTemplate">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PageTemplateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PageTemplateDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateCodeName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateIsPortal" type="xs:boolean" minOccurs="0" />
              <xs:element name="PageTemplateCategoryID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateLayoutID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateWebParts" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateIsReusable" type="xs:boolean" minOccurs="0" />
              <xs:element name="PageTemplateShowAsMasterTemplate" type="xs:boolean" minOccurs="0" />
              <xs:element name="PageTemplateInheritPageLevels" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateLayout" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateLayoutCheckedOutFileName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateLayoutCheckedOutByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateLayoutCheckedOutMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateVersionGUID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateHeader" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PageTemplateLastModified" type="xs:dateTime" />
              <xs:element name="PageTemplateSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateForAllPages" type="xs:boolean" minOccurs="0" />
              <xs:element name="PageTemplateType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateLayoutType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateCSS" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateFile">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_PageTemplate" />
      <xs:field xpath="PageTemplateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PageTemplateID" fieldcaption="PageTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="1e965561-7a54-4269-bbd3-4784c10e33b8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PageTemplateDisplayName" fieldcaption="PageTemplateDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="62991232-e9a8-494d-b31e-6be68f6610b0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateCodeName" fieldcaption="PageTemplateCodeName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a0e4fcc6-0885-4d0c-a311-679a0b2bd0bb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateDescription" fieldcaption="PageTemplateDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0a71b845-085d-4b4b-a736-242db5a8d7bc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="PageTemplateFile" visible="false" defaultvalue="a" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="400" publicfield="false" spellcheck="true" guid="98909a70-0f54-49d6-957a-fed89cd4db44" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field column="PageTemplateIsPortal" fieldcaption="PageTemplateIsPortal" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="59ed01c6-4b29-439e-8519-593f886be9bb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PageTemplateCSS" fieldcaption="PageTemplateCSS" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="581d351f-202e-46a8-81c6-1da5163afeb5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="PageTemplateCategoryID" fieldcaption="PageTemplateCategoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="60d03ec1-54ec-4531-a401-d8cf50ffd962" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateLayoutID" fieldcaption="PageTemplateLayoutID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="07361311-11ba-43e0-aff0-5c7bc261967b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateWebParts" fieldcaption="PageTemplateWebParts" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9ac0e7d2-2166-440d-b7db-2c93b6671d46" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="PageTemplateIsReusable" fieldcaption="PageTemplateIsReusable" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69c10c7c-0ec3-48ba-9237-78f4fb1e36d9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PageTemplateShowAsMasterTemplate" fieldcaption="PageTemplateShowAsMasterTemplate" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c38d560c-db21-4758-b025-04ddfb1d4c7e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PageTemplateInheritPageLevels" fieldcaption="PageTemplateInheritPageLevels" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db56a561-db7e-4fc7-899e-82fe7a079ad5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateLayoutType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" columnsize="50" publicfield="false" spellcheck="true" guid="5d59dad1-4266-41e2-8cd9-72ac744a3a16" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PageTemplateLayout" fieldcaption="PageTemplateLayout" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fef00776-f430-4164-9d1a-cf4bba1f3ecd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="PageTemplateLayoutCheckedOutFileName" fieldcaption="PageTemplateLayoutCheckedOutFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82fbfd7b-2147-43b7-828a-514fa99404d5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateLayoutCheckedOutByUserID" fieldcaption="PageTemplateLayoutCheckedOutByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6936e715-d20a-4f0e-9659-c98737a045b7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateLayoutCheckedOutMachineName" fieldcaption="PageTemplateLayoutCheckedOutMachineName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="31b1701f-8745-4100-91eb-5e7935ef9512" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateVersionGUID" fieldcaption="PageTemplateVersionGUID" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b42bc101-e5a5-4293-9e1e-ecdbd7159c44" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PageTemplateHeader" fieldcaption="PageTemplateHeader" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="44e13d8a-452b-4e4b-b535-5573a07876c9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="PageTemplateGUID" fieldcaption="PageTemplateGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="46bb92aa-a912-46b3-a858-b35b05e4fd9b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="PageTemplateLastModified" fieldcaption="PageTemplateLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="343c268d-28f9-428f-88fb-38f1f8d0ed39" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PageTemplateSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="352a6057-db2b-463f-82b6-fe29c94e3dde" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PageTemplateForAllPages" visible="false" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69097c75-42f7-43ba-b841-df725d5b2035" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PageTemplateType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="f14378b4-e243-43d1-b6be-b7a5aa0ee58d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_PageTemplate', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110714 08:18:30', '8bb71cc8-1fcb-4073-b127-0e3574ecc207', 0, 1, 0, N'', 0, N'PageTemplateDisplayName', N'PageTemplateDescription', N'', N'PageTemplateLastModified', N'<search><item searchable="True" name="PageTemplateID" tokenized="False" content="False" id="a1b6f140-ef4c-4ca0-a367-f3f41ff729d5" /><item searchable="False" name="PageTemplateDisplayName" tokenized="True" content="True" id="2e225143-d0aa-46b6-81e7-d01704a796cc" /><item searchable="False" name="PageTemplateCodeName" tokenized="True" content="True" id="909c5d79-9a64-44aa-b180-4327d6b554a0" /><item searchable="False" name="PageTemplateDescription" tokenized="True" content="True" id="fb0cdb9e-246c-4e34-8d1a-cc061fe8420d" /><item searchable="False" name="PageTemplateFile" tokenized="True" content="True" id="e32a7294-e500-467a-8017-67e3f2706a10" /><item searchable="True" name="PageTemplateIsPortal" tokenized="False" content="False" id="416eeb4c-7a99-4975-a8ae-dae278789837" /><item searchable="True" name="PageTemplateCategoryID" tokenized="False" content="False" id="192829eb-1ad1-4d6e-a990-a10f606d1e14" /><item searchable="True" name="PageTemplateLayoutID" tokenized="False" content="False" id="0f265a20-d746-4a82-863c-d433534a273f" /><item searchable="False" name="PageTemplateWebParts" tokenized="True" content="True" id="ad073ac9-5126-4aed-b561-e6d81bed0e4a" /><item searchable="True" name="PageTemplateIsReusable" tokenized="False" content="False" id="b78c44f4-9b8f-4f26-a30a-00005d2968ba" /><item searchable="True" name="PageTemplateShowAsMasterTemplate" tokenized="False" content="False" id="e965f620-dd3e-4737-b5c9-f3ead6ec8cbe" /><item searchable="False" name="PageTemplateInheritPageLevels" tokenized="True" content="True" id="d25508e0-325a-429b-a697-542d19fdaef7" /><item searchable="False" name="PageTemplateLayoutType" tokenized="True" content="True" id="2b440e5a-06f1-41ab-acc1-39aa164f78be" /><item searchable="False" name="PageTemplateLayout" tokenized="True" content="True" id="d59ac986-9b30-4839-8701-c136bbd23719" /><item searchable="False" name="PageTemplateLayoutCheckedOutFileName" tokenized="True" content="True" id="ff651ce4-b258-4ad9-bd3e-e8951baadf94" /><item searchable="True" name="PageTemplateLayoutCheckedOutByUserID" tokenized="False" content="False" id="21b93660-1f64-412d-993a-f33ada3218da" /><item searchable="False" name="PageTemplateLayoutCheckedOutMachineName" tokenized="True" content="True" id="ff86a5a2-f52e-4baf-8056-4acf5733c48f" /><item searchable="False" name="PageTemplateVersionGUID" tokenized="True" content="True" id="bb9895d1-93dc-4ed6-a8f6-0be2dbcdcb11" /><item searchable="False" name="PageTemplateHeader" tokenized="True" content="True" id="e9d3ab52-ac73-4287-8bef-25a03f3b52db" /><item searchable="False" name="PageTemplateGUID" tokenized="False" content="False" id="0e524b98-c5b5-4a97-9f11-a90049de4799" /><item searchable="True" name="PageTemplateLastModified" tokenized="False" content="False" id="5207daac-53f4-41eb-8572-d2d5bbd8d347" /><item searchable="True" name="PageTemplateSiteID" tokenized="False" content="False" id="ab4b957a-08da-44c5-bb9a-bcc86dd4fc21" /><item searchable="True" name="PageTemplateForAllPages" tokenized="False" content="False" id="ad5127d1-5324-44f3-8d98-b426693d95e7" /><item searchable="False" name="PageTemplateType" tokenized="True" content="True" id="80045e5c-1e3f-47f6-9bd7-a61dffc3d0e0" /></search>', NULL, 1, N'', NULL, NULL, 0, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (161, N'Query', N'cms.query', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Query">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="QueryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="QueryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="QueryTypeID" type="xs:int" />
              <xs:element name="QueryText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="QueryRequiresTransaction" type="xs:boolean" />
              <xs:element name="ClassID" type="xs:int" />
              <xs:element name="QueryIsLocked" type="xs:boolean" />
              <xs:element name="QueryLastModified" type="xs:dateTime" />
              <xs:element name="QueryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="QueryLoadGeneration" type="xs:int" />
              <xs:element name="QueryIsCustom" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Query" />
      <xs:field xpath="QueryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="QueryID" fieldcaption="QueryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5c3db3dc-6a01-4d2a-9b74-24b79704a82b" /><field column="QueryName" fieldcaption="QueryName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad1056d3-34b8-41b3-b31d-bdf116f010ec" /><field column="QueryTypeID" fieldcaption="QueryTypeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b96bf312-8b7c-42d2-b0b2-a395e10ccfd4" /><field column="QueryText" fieldcaption="QueryText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bcfc8502-008d-4d21-aac1-63810e7d563c" /><field column="QueryRequiresTransaction" fieldcaption="QueryRequiresTransaction" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5cc03d8e-017e-4a5c-bb1e-5f7c42290ab7" /><field column="ClassID" fieldcaption="ClassID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="34bfc28c-0e6a-497b-a761-eb00dd435ae3" /><field column="QueryIsLocked" fieldcaption="QueryIsLocked" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="154d5cd0-615c-4f4f-b158-434357d93a8f" /><field column="QueryLastModified" fieldcaption="QueryLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8763542e-3500-4ae8-8fc7-07cd5a116f06" /><field column="QueryGUID" fieldcaption="QueryGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6f3b0835-98e0-49f8-a972-74e39d7d91d8" /><field column="QueryLoadGeneration" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3505f469-2896-4606-903e-b7bdc7766a89" /><field column="QueryIsCustom" fieldcaption="QueryIsCustom" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="be70ac52-0624-49e5-ae56-d9f95f3ddf56" /></form>', N'', N'', N'', N'CMS_Query', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:13:16', '821c115d-0b5b-4d8a-b5f9-7d2e0f97e0bd', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (162, N'Transformation', N'cms.transformation', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Transformation">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TransformationID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TransformationName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationClassID" type="xs:int" />
              <xs:element name="TransformationCheckedOutByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="TransformationCheckedOutMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationCheckedOutFilename" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationVersionGUID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TransformationLastModified" type="xs:dateTime" />
              <xs:element name="TransformationIsHierarchical" type="xs:boolean" minOccurs="0" />
              <xs:element name="TransformationHierarchicalXML" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TransformationCSS" minOccurs="0">
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
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Transformation" />
      <xs:field xpath="TransformationID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TransformationID" fieldcaption="TransformationID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="15f5489f-158e-434c-b8a8-3b246917aa3a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationName" fieldcaption="TransformationName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="8f32aaee-ed30-464d-b321-d5b1f8779039" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationCode" fieldcaption="TransformationCode" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="87c280d1-e998-451b-9dfb-8cbbd2891b0a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationType" fieldcaption="TransformationType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="12c4729c-16f6-4565-8509-2db6af52df99" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationClassID" fieldcaption="TransformationClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72b254ff-a179-46f9-851e-c968e8324328" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationCSS" fieldcaption="TransformationCSS" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="38e823cd-030a-41b2-b64a-8e680d40c6b7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textareacontrol</controlname></settings></field><field column="TransformationCheckedOutByUserID" fieldcaption="TransformationCheckedOutByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e942461f-8e8f-479e-9f76-7a3f906b9c8c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationCheckedOutMachineName" fieldcaption="TransformationCheckedOutMachineName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="4496ef4c-19af-4250-895d-3998abaf0769" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationCheckedOutFilename" fieldcaption="TransformationCheckedOutFilename" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="a401b484-bb6f-4be5-bb9e-31086bbf8c23" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationVersionGUID" fieldcaption="TransformationVersionGUID" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="e269354c-1f6e-4e55-8158-73d059b01941" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationGUID" fieldcaption="TransformationGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6a375a9a-04d7-4592-8df8-acc1f5127249" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationLastModified" fieldcaption="TransformationLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c19c9cd6-60e5-40de-b547-dd045a32c3de" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TransformationIsHierarchical" fieldcaption="TransformationIsHierarchical" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="true" guid="a6087c19-3262-4b37-9fd2-f3d2c135f260" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TransformationHierarchicalXML" fieldcaption="TransformationHierarchicalXML" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="true" guid="4099b257-bafc-402f-8096-71930c3453df" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Transformation', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:08:07', '719c71f8-4dcd-4ab5-8d4e-84e6a60fe7be', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (171, N'Workflow', N'cms.workflow', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Workflow">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WorkflowID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WorkflowDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WorkflowName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WorkflowGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WorkflowLastModified" type="xs:dateTime" />
              <xs:element name="WorkflowAutoPublishChanges" type="xs:boolean" minOccurs="0" />
              <xs:element name="WorkflowUseCheckinCheckout" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Workflow" />
      <xs:field xpath="WorkflowID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WorkflowID" fieldcaption="WorkflowID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="bf3f5149-ebe0-4c41-8cde-79a0e63bdb66" ismacro="false" /><field column="WorkflowDisplayName" fieldcaption="WorkflowDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1493eba2-b913-4a2f-af71-9df4ebcb6f51" ismacro="false" /><field column="WorkflowName" fieldcaption="WorkflowName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c0821ace-fa5b-4e08-b9eb-32783abd66c9" ismacro="false" /><field column="WorkflowGUID" fieldcaption="WorkflowGUID" visible="true" columntype="file" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a7be7df7-232f-4cf4-8446-09df882727bc" ismacro="false" /><field column="WorkflowLastModified" fieldcaption="WorkflowLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="83b95ad1-000c-4e16-9177-71b438a75d3c" ismacro="false" /><field column="WorkflowAutoPublishChanges" fieldcaption="WorkflowAutoPublishChanges" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="947759f0-e4ee-4286-8ed3-4a33506ee50e" visibility="none" ismacro="false" /><field column="WorkflowUseCheckinCheckout" fieldcaption="WorkflowUseCheckinCheckout" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="64518cad-820b-425a-9553-3a11fe06a609" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_Workflow', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 13:51:09', 'a80047fb-e386-48ea-b433-bcdd92d131e4', 0, 1, 0, N'', 1, N'WorkflowDisplayName', N'0', N'', N'WorkflowLastModified', N'<search><item searchable="True" name="WorkflowID" tokenized="False" content="False" id="e0278c57-1956-4017-8fcc-b00c05e3c351" /><item searchable="False" name="WorkflowDisplayName" tokenized="True" content="True" id="d6d7c536-2469-4a12-b20d-3978ff14e7c2" /><item searchable="False" name="WorkflowName" tokenized="True" content="True" id="c84f56c2-6381-4de9-a0a4-23364863ad50" /><item searchable="False" name="WorkflowGUID" tokenized="False" content="False" id="8d65e12e-beb6-44c2-84dc-e914c0fbed2d" /><item searchable="True" name="WorkflowLastModified" tokenized="False" content="False" id="56cb66ec-4f6a-4ed2-ad94-32fcbe88385c" /><item searchable="True" name="WorkflowAutoPublishChanges" tokenized="False" content="False" id="fa9d7121-6d35-43ed-b55a-7f4d0ab8e373" /><item searchable="True" name="WorkflowUseCheckinCheckout" tokenized="False" content="False" id="9a90c5ca-53ce-4a84-a9ba-60eb4dce5b03" /></search>', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (172, N'Workflow step', N'cms.workflowstep', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WorkflowStep">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StepID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="StepDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StepName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StepOrder" type="xs:int" />
              <xs:element name="StepWorkflowID" type="xs:int" />
              <xs:element name="StepGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="StepLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WorkflowStep" />
      <xs:field xpath="StepID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StepID" fieldcaption="StepID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="dc2613e4-432b-40b2-b6ae-2b0e883309a2" /><field column="StepDisplayName" fieldcaption="StepDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="78ff440b-cf76-4421-b641-c3732ea35f61" /><field column="StepName" fieldcaption="StepName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db5c64f9-63a6-42d7-84ad-3bb20f78d72b" /><field column="StepOrder" fieldcaption="StepOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f12564cc-48d4-45c2-ad20-e6e3379a5710" /><field column="StepWorkflowID" fieldcaption="StepWorkflowID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a13c24d9-9013-42aa-966d-02121b44c37c" /><field column="StepGUID" fieldcaption="StepGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ce724361-4974-4f98-bba8-123c2b66bdb2" /><field column="StepLastModified" fieldcaption="StepLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f31371a3-cf3c-40c5-a0bb-b1ecdd98921f" /></form>', N'', N'', N'', N'CMS_WorkflowStep', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110810 10:25:49', '6fc9d49b-83c2-4a7e-9a33-037883a76a26', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (175, N'Workflow scope', N'cms.workflowscope', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WorkflowScope">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ScopeID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ScopeStartingPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ScopeWorkflowID" type="xs:int" />
              <xs:element name="ScopeClassID" type="xs:int" minOccurs="0" />
              <xs:element name="ScopeSiteID" type="xs:int" />
              <xs:element name="ScopeGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ScopeLastModified" type="xs:dateTime" />
              <xs:element name="ScopeCultureID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WorkflowScope" />
      <xs:field xpath="ScopeID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ScopeID" fieldcaption="ScopeID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="80348be1-c666-4a97-9d48-1d46bb49d061" /><field column="ScopeStartingPath" fieldcaption="ScopeStartingPath" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="e1bfc63e-7081-4b80-acc6-993bcc17becb" /><field column="ScopeWorkflowID" fieldcaption="ScopeWorkflowID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d53bd2fe-2023-4098-9527-fd6d13fc8cff" /><field column="ScopeClassID" fieldcaption="ScopeClassID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="86559d2f-0ac6-4714-adb6-e1b2ce4cae90" /><field column="ScopeSiteID" fieldcaption="ScopeSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="314be952-3e36-4868-b10b-b1026cb021cc" /><field column="ScopeGUID" fieldcaption="ScopeGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b198dac-7fd9-46b5-bfff-e8c087bbbec2" /><field column="ScopeLastModified" fieldcaption="ScopeLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42da4cc7-0319-486d-84ab-0cc0aecab0ad" /><field column="ScopeCultureID" fieldcaption="ScopeCultureID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3fd1415f-d6c5-4305-8c67-5c2cc778768a" /></form>', N'', N'', N'', N'CMS_WorkflowScope', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:52:35', '8cba8304-c75f-45ce-8f39-7f363cf5892a', 0, 1, 0, N'', 1, N'ScopeStartingPath', N'0', N'', N'ScopeLastModified', N'<search><item searchable="True" name="ScopeID" tokenized="False" content="False" id="10e3f770-b6ae-415e-b997-e386858265ff" /><item searchable="False" name="ScopeStartingPath" tokenized="True" content="True" id="fc697fc4-9f43-4fff-9c7d-6670e403c1c1" /><item searchable="True" name="ScopeWorkflowID" tokenized="False" content="False" id="c3bc06a7-86e3-4a71-ad72-e52df1a1643f" /><item searchable="True" name="ScopeClassID" tokenized="False" content="False" id="f7b938fd-f791-460d-98a1-17291995129b" /><item searchable="True" name="ScopeSiteID" tokenized="False" content="False" id="301e0569-9057-4c76-ab1d-40f8386cfbc4" /><item searchable="False" name="ScopeGUID" tokenized="False" content="False" id="6d647006-b642-4947-aea9-b30e93b048e2" /><item searchable="True" name="ScopeLastModified" tokenized="False" content="False" id="b0f0ddc1-202c-4bd7-879a-996c6ce84962" /><item searchable="True" name="ScopeCultureID" tokenized="False" content="False" id="8768d1a8-dfed-4571-98e3-b1dc28151ff2" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (214, N'Version history', N'cms.versionhistory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_VersionHistory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="VersionHistoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="NodeSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentID" type="xs:int" minOccurs="0" />
              <xs:element name="DocumentNamePath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NodeXML">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ModifiedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="ModifiedWhen" type="xs:dateTime" />
              <xs:element name="VersionNumber" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionComment" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ToBePublished" type="xs:boolean" />
              <xs:element name="PublishFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="PublishTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="WasPublishedFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="WasPublishedTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="VersionDocumentName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionDocumentType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionClassID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionMenuRedirectUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionWorkflowID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionWorkflowStepID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionNodeAliasPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="VersionDeletedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="VersionDeletedWhen" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_VersionHistory" />
      <xs:field xpath="VersionHistoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="VersionHistoryID" fieldcaption="VersionHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="032b5d2d-3e97-44da-a573-109f8a26c440" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="NodeSiteID" fieldcaption="NodeSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29aee6c7-b6a2-42d8-8180-d9947a54e404" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentID" fieldcaption="DocumentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="90d894a9-003b-440a-9b99-96d3727a0fed" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DocumentNamePath" fieldcaption="DocumentNamePath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d4324f6f-cb8c-4d4e-8f7d-aebc5b0e355c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NodeXML" fieldcaption="NodeXML" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e2f43580-d08f-41ca-8ed5-2f13eba66a8a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ModifiedByUserID" fieldcaption="ModifiedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a343763c-16aa-4384-bec9-2cea568b40a7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ModifiedWhen" fieldcaption="ModifiedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f42040c2-ecb6-444d-962b-37bf6f5353f8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="VersionNumber" fieldcaption="VersionNumber" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="00fa9c9f-a418-4c97-be43-281dd1ad2d12" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="VersionComment" fieldcaption="VersionComment" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d8ebc7c7-7d2a-4a52-8fff-168c1ac1922e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ToBePublished" fieldcaption="ToBePublished" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="49ac547e-9ac5-4122-afc4-446dfdea5005" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PublishFrom" fieldcaption="PublishFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6e4ae6e1-7e3d-43ce-9087-37737207429d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PublishTo" fieldcaption="PublishTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2c7714af-1a7e-4e94-9c1e-164579ae7921" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="WasPublishedFrom" fieldcaption="WasPublishedFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cf5302b2-d7c4-4597-a79a-f383b16615d5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="WasPublishedTo" fieldcaption="WasPublishedTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0cfbd232-721f-47b6-b9ce-64d7dda4c8b6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="VersionDocumentName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="false" guid="1f031481-47cc-4a0d-b11d-4ee356c98dab" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="VersionDocumentType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="false" guid="e04be7e5-7ace-4321-a6e7-4c7bda8866eb" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="VersionClassID" fieldcaption="VersionClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fbff29c3-e335-480c-93f9-2aa8efc4124d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionMenuRedirectUrl" fieldcaption="VersionMenuRedirectUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="5ed177b4-6133-498a-9fda-96d89bd0545c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionWorkflowID" fieldcaption="VersionWorkflowID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="07148cc2-90bb-4a02-b942-315530e672f9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionWorkflowStepID" fieldcaption="VersionWorkflowStepID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="016a5914-2922-4a05-beb5-d22ed92e1be8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="VersionNodeAliasPath" fieldcaption="VersionNodeAliasPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="bf4bee9d-d5a0-4b4b-8b7c-4d6ce9f667d4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="VersionDeletedByUserID" fieldcaption="VersionDeletedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca451b08-8cc4-4f92-b1ce-cf667ed30fd0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="VersionDeletedWhen" fieldcaption="VersionDeletedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="70838fe4-bff3-423b-aaf6-f93be6bc248a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field></form>', N'', N'', N'', N'CMS_VersionHistory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 09:50:29', '41fd4469-5173-4b22-b89d-5fb5d2e1c5fb', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (225, N'ACL item', N'cms.aclitem', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ACLItem">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ACLItemID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ACLID" type="xs:int" />
              <xs:element name="UserID" type="xs:int" minOccurs="0" />
              <xs:element name="RoleID" type="xs:int" minOccurs="0" />
              <xs:element name="Allowed" type="xs:int" />
              <xs:element name="Denied" type="xs:int" />
              <xs:element name="LastModified" type="xs:dateTime" />
              <xs:element name="LastModifiedByUserID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ACLItem" />
      <xs:field xpath="ACLItemID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ACLItemID" fieldcaption="ACLItemID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="fc46d68f-94de-4f63-bc5d-bee495e2afe9" /><field column="ACLID" fieldcaption="ACLID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="368d7b03-c6a7-4684-8aa5-5e03aab24de9" /><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3a51b2be-1ba4-42fd-b15d-70371c2cf9fb" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9b98e19f-e935-438a-9269-47abb95830f5" /><field column="Allowed" fieldcaption="Allowed" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca5e7c3c-65f6-4e90-9b99-5bae5f769b16" /><field column="Denied" fieldcaption="Denied" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="87100fd6-a32f-4097-b857-696e8d2ddc1b" /><field column="LastModified" fieldcaption="LastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8247705e-9a5a-4aed-a914-61c662306a2b" /><field column="LastModifiedByUserID" fieldcaption="LastModifiedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b3c3129-984b-4528-a639-f1703401961f" /></form>', N'', N'', N'', N'CMS_ACLItem', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 18:19:00', '83fdf79a-9d56-474f-b229-115fccabf042', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (233, N'ACL', N'cms.acl', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ACL">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ACLID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ACLInheritedACLs">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ACLOwnerNodeID" type="xs:int" />
              <xs:element name="ACLGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ACLLastModified" type="xs:dateTime" />
              <xs:element name="ACLSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ACL" />
      <xs:field xpath="ACLID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ACLID" fieldcaption="ACLID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ed3bb1c3-2f77-4362-aaa2-32522e66c3f0" ismacro="false" /><field column="ACLInheritedACLs" fieldcaption="ACLInheritedACLs" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="7db7efe1-669f-4181-98c5-6f137ac342fa" ismacro="false" /><field column="ACLOwnerNodeID" fieldcaption="ACLOwnerNodeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9da03250-37ab-48bd-bb1a-9864426beabc" ismacro="false" /><field column="ACLGUID" fieldcaption="ACLGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="25d0fa3a-f1da-49f8-97c0-d2c67679c4f3" ismacro="false" /><field column="ACLLastModified" fieldcaption="ACLLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1d55ce40-56bf-4319-94f2-c1d357df6047" ismacro="false" /><field column="ACLSiteID" fieldcaption="ACLSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="50cb28fc-3e27-4ce2-978b-7151ccb5768b" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_ACL', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100902 07:59:50', '798885a1-331c-44aa-95c2-45ebb9a46a65', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (298, N'Workflow history', N'cms.workflowhistory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WorkflowHistory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WorkflowHistoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="VersionHistoryID" type="xs:int" />
              <xs:element name="StepID" type="xs:int" minOccurs="0" />
              <xs:element name="StepDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ApprovedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="ApprovedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="Comment" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WasRejected" type="xs:boolean" />
              <xs:element name="StepName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WorkflowHistory" />
      <xs:field xpath="WorkflowHistoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WorkflowHistoryID" fieldcaption="WorkflowHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="216f01a5-16fa-4a22-a0ff-e1eee5700bc4" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="VersionHistoryID" fieldcaption="VersionHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ffc25364-a750-4ac1-a749-a7183e166e80" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StepID" fieldcaption="StepID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="da6cebcb-7dbe-45cf-8596-c175a68eb72e" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StepDisplayName" fieldcaption="StepDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9e9f09c3-97be-4f0e-88a6-aef997532d6e" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ApprovedByUserID" fieldcaption="ApprovedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="51b7ad05-c0ae-42d1-ae89-b8f6bd3e6eb9" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ApprovedWhen" fieldcaption="ApprovedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e8bb8da1-4c12-4137-b98a-6ed43ca5da8a" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="Comment" fieldcaption="Comment" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd6fac2f-dcdd-4c7e-ad68-4255ede92001" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WasRejected" fieldcaption="WasRejected" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="366b4b7a-e22c-4d4c-9fb9-3df3b5efcb1b" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="StepName" fieldcaption="StepName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="440" publicfield="false" spellcheck="false" guid="4ee5b13d-554c-46a7-8c7a-f0cffbf5d475" visibility="none" ismacro="false" hasdependingfields="false"><settings><FilterEnabled>False</FilterEnabled><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_WorkflowHistory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110104 19:43:59', 'accc303b-bfec-49fb-8d65-bef8984b7833', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (405, N'Page template category', N'cms.pagetemplatecategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_PageTemplateCategory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CategoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CategoryDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryParentID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CategoryLastModified" type="xs:dateTime" />
              <xs:element name="CategoryImagePath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryChildCount" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryTemplateChildCount" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryOrder" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryLevel" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_PageTemplateCategory" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="6049bf05-83a6-45c6-93e5-6981f476ec91" /><field column="CategoryDisplayName" fieldcaption="CategoryDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb6f8688-47b1-401a-b552-9cc53bb0b28f" /><field column="CategoryParentID" fieldcaption="CategoryParentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ced6a744-2400-473b-af23-d446df602f58" /><field column="CategoryName" fieldcaption="CategoryName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4cf24965-9b14-4f3f-86eb-316501e42f24" /><field column="CategoryGUID" fieldcaption="CategoryGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2356cad3-2cda-408b-a312-19432631758a" /><field column="CategoryLastModified" fieldcaption="CategoryLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c42aad5-077d-496b-acb2-1a9b76bd2a15" /><field column="CategoryImagePath" visible="true" columntype="text" fieldtype="label" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="482243d1-3780-46c4-99cd-2dba52f201cf" visibility="none" ismacro="false" fieldcaption="CategoryImagePath" /><field column="CategoryChildCount" fieldcaption="CategoryChildCount" visible="true" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f1a4640-8cc4-4b8a-824a-35e2cfe99703" visibility="none" ismacro="false" /><field column="CategoryTemplateChildCount" fieldcaption="CategoryTemplateChildCount" visible="true" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="358b657b-3aff-4580-9838-598a0f22fe9c" visibility="none" ismacro="false" /><field column="CategoryPath" fieldcaption="CategoryPath" visible="true" columntype="text" fieldtype="label" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="ba9b77a6-bfd8-4e14-93fb-8ddff52a098b" visibility="none" ismacro="false" /><field column="CategoryOrder" fieldcaption="CategoryOrder" visible="true" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3670fe8d-2a01-4879-9351-ed8a6cd16615" visibility="none" ismacro="false" /><field column="CategoryLevel" fieldcaption="CategoryLevel" visible="true" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="085396ce-e649-40ae-8309-db9fa4632ae4" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_PageTemplateCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110705 12:09:49', '92a1be18-bad3-4aac-91f0-064d30d452fd', 0, 1, 0, N'', 1, N'CategoryDisplayName', N'0', N'CategoryImagePath', N'CategoryLastModified', N'<search><item searchable="True" name="CategoryID" tokenized="False" content="False" id="12cab9cc-49df-4f81-9794-7470a2815f04" /><item searchable="False" name="CategoryDisplayName" tokenized="True" content="True" id="da0ccbf2-a620-4a1c-badb-2494566798cc" /><item searchable="True" name="CategoryParentID" tokenized="False" content="False" id="3ca6c25a-2f35-418c-8dfc-993ea5413bf0" /><item searchable="False" name="CategoryName" tokenized="True" content="True" id="4e2963f6-9393-4037-9048-35a7e42099f3" /><item searchable="False" name="CategoryGUID" tokenized="False" content="False" id="49c474a0-cb50-4ad1-b6d3-36669cc6a119" /><item searchable="True" name="CategoryLastModified" tokenized="False" content="False" id="559fd159-52db-416e-b2ef-5674bbee23a5" /><item searchable="False" name="CategoryImagePath" tokenized="True" content="True" id="13ad4302-730c-4173-a0c1-83caa471509d" /><item searchable="True" name="CategoryChildCount" tokenized="False" content="False" id="f71a2481-d11b-419f-b726-e487843bc6be" /><item searchable="True" name="CategoryTemplateChildCount" tokenized="False" content="False" id="9b36da8f-2e98-4433-857a-a840e6a29827" /><item searchable="False" name="CategoryPath" tokenized="True" content="True" id="f91bf532-fc5b-423b-9737-8c4454380c4b" /><item searchable="True" name="CategoryOrder" tokenized="False" content="False" id="d8698fdf-680f-4755-95f9-db193e3dc9cd" /><item searchable="True" name="CategoryLevel" tokenized="False" content="False" id="c4770147-17e4-4145-859c-40d74a34339f" /></search>', NULL, 0, N'', NULL, NULL, 0, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (410, N'Layout', N'cms.layout', 1, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Layout">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="LayoutID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="LayoutCodeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutCheckedOutFilename" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutCheckedOutByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="LayoutCheckedOutMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutVersionGUID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="LayoutLastModified" type="xs:dateTime" />
              <xs:element name="LayoutType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LayoutCSS" minOccurs="0">
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
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Layout" />
      <xs:field xpath="LayoutID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="LayoutID" fieldcaption="LayoutID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ca2b14bd-12c8-47c4-8c2a-09a0a7bdc546" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="LayoutCodeName" fieldcaption="LayoutCodeName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ee661fe6-8455-471e-8571-4aecad368b72" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LayoutDisplayName" fieldcaption="LayoutDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="09307aeb-2738-4b0e-b49e-5f76e48bb7a9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LayoutDescription" fieldcaption="LayoutDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e316e3e3-2d28-46d2-87f0-da19bda6bdf5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="LayoutType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="4b29932c-b7a1-492a-92e5-d34a58e6048f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="LayoutCSS" fieldcaption="LayoutCSS" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a88496dc-a31a-444d-bfa6-bb827449feac" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textareacontrol</controlname></settings></field><field column="LayoutCode" fieldcaption="LayoutCode" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="10c4bb79-c4bd-44d6-a590-09f7b9bf9136" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="LayoutCheckedOutFilename" fieldcaption="LayoutCheckedOutFilename" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ebd4efff-5d54-4753-8b21-e499061f462b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LayoutCheckedOutByUserID" fieldcaption="LayoutCheckedOutByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="28099dd9-2a6c-4c96-94c7-33dcfd38ec29" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LayoutCheckedOutMachineName" fieldcaption="LayoutCheckedOutMachineName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6f9f0dc4-de0d-4b0d-8f7c-ec9c4df9f95d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LayoutVersionGUID" fieldcaption="LayoutVersionGUID" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69c017d1-a807-4f34-a55e-911a71b9fe4f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LayoutGUID" fieldcaption="LayoutGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8458d6aa-1b12-491f-80f7-c8223ba9c1a7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="LayoutLastModified" fieldcaption="LayoutLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b2dc55fa-02c4-475f-9bf7-9e84558282a1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Layout', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110324 17:07:19', 'f0ba54c7-64ef-41d9-a4f4-0b792510414d', 0, 1, 0, N'', 1, N'LayoutDisplayName', N'LayoutDescription', N'', N'LayoutLastModified', N'<search><item searchable="True" name="LayoutID" tokenized="False" content="False" id="aef1c509-6756-47e8-953d-a3c6ce855f6b" /><item searchable="False" name="LayoutCodeName" tokenized="True" content="True" id="3c89d01a-0eed-4706-ab94-3c80877f8b03" /><item searchable="False" name="LayoutDisplayName" tokenized="True" content="True" id="96a850e6-1983-4f3f-8fda-c18371d3d882" /><item searchable="False" name="LayoutDescription" tokenized="True" content="True" id="e0ba4743-dbcc-4251-af0d-18b57ca25d7d" /><item searchable="False" name="LayoutType" tokenized="True" content="True" id="da4e9677-1148-4755-b39f-a2a4c1f895ce" /><item searchable="False" name="LayoutCode" tokenized="True" content="True" id="940fcf28-8892-4459-b164-80b313523769" /><item searchable="False" name="LayoutCheckedOutFilename" tokenized="True" content="True" id="d2f49db2-c7d8-4088-a640-d9fe88481b9c" /><item searchable="True" name="LayoutCheckedOutByUserID" tokenized="False" content="False" id="11c03dc1-c0ba-40c1-b447-cb71ae98abb5" /><item searchable="False" name="LayoutCheckedOutMachineName" tokenized="True" content="True" id="bf2c07b8-e259-4e9e-b7cf-f64e754c84ed" /><item searchable="False" name="LayoutVersionGUID" tokenized="True" content="True" id="31ea45bb-33b3-4696-8249-9d511210ea4c" /><item searchable="False" name="LayoutGUID" tokenized="False" content="False" id="ce98daf8-7e3d-46cb-8889-1f457a92ed6e" /><item searchable="True" name="LayoutLastModified" tokenized="False" content="False" id="85666db1-c6d5-4249-bb87-8753ff395901" /></search>', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (424, N'Web part category', N'cms.webpartcategory', 1, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebPartCategory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CategoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CategoryDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryParentID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CategoryLastModified" type="xs:dateTime" />
              <xs:element name="CategoryImagePath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryOrder" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryLevel" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryChildCount" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryWebPartChildCount" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WebPartCategory" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e79a9743-1548-4c03-a0be-bf4e59a59de0" /><field column="CategoryDisplayName" fieldcaption="CategoryDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="557c910c-3b7c-4ebc-9545-ef444d9c3755" /><field column="CategoryParentID" fieldcaption="CategoryParentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6bbbeeaa-b4bc-45f2-9fc8-293e5cf2da27" /><field column="CategoryName" fieldcaption="CategoryName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="fcb3e927-1a84-43db-8cc5-d838ad33d498" /><field column="CategoryGUID" fieldcaption="CategoryGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea4e0ee4-0446-457a-be59-226738008cb7" /><field column="CategoryLastModified" fieldcaption="CategoryLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d1ee711f-e797-4042-a1e9-ba528b5f2ee7" /><field column="CategoryImagePath" fieldcaption="CategoryImagePath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="ea9f0f5e-5496-4b19-85c3-f4dd5c4b57f1" /><field column="CategoryPath" fieldcaption="CategoryPath" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="5fbadd22-4c00-4bc8-9412-e6f1775406ef" /><field column="CategoryOrder" fieldcaption="CategoryOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a0a18eb8-cb03-4700-92dd-d3bc7afcd7ad" /><field column="CategoryLevel" fieldcaption="CategoryLevel" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4ddb3f47-6d98-49bc-b29f-61b71df562bc" /><field column="CategoryChildCount" fieldcaption="CategoryChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7bd82729-6151-450d-b9e8-f79c6907e603" /><field column="CategoryWebPartChildCount" fieldcaption="CategoryWebPartChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a4ae1551-c272-44b9-a63d-afc72d08730e" /></form>', N'', N'', N'', N'CMS_WebPartCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110705 12:09:18', '920ce0d4-cb71-4a7b-bd25-af73d3323951', 0, 1, 0, N'', 1, N'CategoryDisplayName', N'0', N'CategoryImagePath', N'0', N'<search><item searchable="True" name="CategoryID" tokenized="False" content="False" id="49c49a57-20aa-40dd-a0fe-084c14e2a789" /><item searchable="False" name="CategoryDisplayName" tokenized="True" content="True" id="fe4a6a81-2a18-4113-928b-4e75aa50d453" /><item searchable="True" name="CategoryParentID" tokenized="False" content="False" id="47fcf947-c4d6-48a8-8c8c-2f7addd69b2c" /><item searchable="False" name="CategoryName" tokenized="True" content="True" id="b44e8fab-a75a-4e64-9e1d-53df49b3368f" /><item searchable="False" name="CategoryGUID" tokenized="False" content="False" id="36d96746-154c-4ed6-91d2-cb3480d44c6d" /><item searchable="True" name="CategoryLastModified" tokenized="False" content="False" id="5b4be3a3-5b47-4a33-9f1c-dd6e10a69f27" /><item searchable="False" name="CategoryImagePath" tokenized="True" content="True" id="b5837f78-85a7-4a62-b7e0-c82c47059bc7" /><item searchable="False" name="CategoryPath" tokenized="True" content="True" id="eef07646-442f-42a0-8104-5f95ad9fb645" /><item searchable="True" name="CategoryOrder" tokenized="False" content="False" id="125d2c30-d06b-4986-88dd-315291f3791f" /><item searchable="True" name="CategoryLevel" tokenized="False" content="False" id="ed247f64-d138-4b54-bcdb-916f2ac33b48" /><item searchable="True" name="CategoryChildCount" tokenized="False" content="False" id="d77b5453-2999-4f37-89b8-850ae05b97a3" /><item searchable="True" name="CategoryWebPartChildCount" tokenized="False" content="False" id="b8332516-d4e3-4a0b-b47f-d294055ae169" /></search>', NULL, 0, N'', NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [CMS_Class] OFF
