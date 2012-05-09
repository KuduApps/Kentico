SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1643, N'Ecommerce - User department', N'Ecommerce.UserDepartment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_UserDepartment">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="DepartmentID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_UserDepartment" />
      <xs:field xpath="UserID" />
      <xs:field xpath="DepartmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f4b57774-7a85-4334-923c-afa249e390a2" /><field column="DepartmentID" fieldcaption="DepartmentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="17c9e258-80cb-4dc7-a6ac-426246e3d688" /></form>', N'', N'', N'', N'COM_UserDepartment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110315 08:34:26', '0ab42b5c-3dd2-445d-9c2f-3b9cefcd5861', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1644, N'ForumRole', N'Forums.ForumRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_ForumRoles">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ForumID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="PermissionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_ForumRoles" />
      <xs:field xpath="ForumID" />
      <xs:field xpath="RoleID" />
      <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ForumID" fieldcaption="ForumID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a483d3a7-c81f-4545-b730-414fb68c9368" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c32d235b-e430-44dd-9329-b3f9e4a28cc0" /><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cac9ea9d-b936-4c08-9c05-cc660db8d40f" /></form>', N'', N'', N'', N'Forums_ForumRoles', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 16:27:09', '4d044efe-1c53-4e8d-a5d4-0377b1a1f695', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1645, N'PollRole', N'Polls.PollRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Polls_PollRoles">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PollID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Polls_PollRoles" />
      <xs:field xpath="PollID" />
      <xs:field xpath="RoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PollID" fieldcaption="PollID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4a15bfe7-5aa7-4033-b700-64361a6713e0" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9fdc6d4b-a537-471b-9111-224d87baae6d" /></form>', N'', N'', N'', N'Polls_PollRoles', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:16', 'ec99216d-fb11-4812-b105-91d3cab05096', 0, 0, 0, N'', 2, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1646, N'PollSite', N'Polls.PollSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Polls_PollSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PollID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Polls_PollSite" />
      <xs:field xpath="PollID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PollID" fieldcaption="PollID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5925ebb0-bbf5-4c2b-b4dc-4dcec83911e7" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5c1b677-709a-41a4-83ae-54a21554028d" /></form>', N'', N'', N'', N'Polls_PollSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:16', '39dc447c-b9a3-4da3-91dd-41a9206e7e3e', 0, 0, 0, N'', 2, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1648, N'RolePermission', N'cms.rolepermission', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_RolePermission">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="PermissionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_RolePermission" />
      <xs:field xpath="RoleID" />
      <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="7abf6c63-dfdd-4a52-a992-33f6a3af20c9" /><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c68c2890-c2ee-4a9e-b92f-40accf2b8bc1" /></form>', N'', N'', N'', N'CMS_RolePermission', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:22', '30dbec02-7055-4f3c-8318-5bd96c63965d', 0, 0, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1649, N'Settings category', N'cms.settingscategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SettingsCategory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CategoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CategoryDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryOrder" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryParentID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryIDPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryLevel" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryChildCount" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryIconPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryIsGroup" type="xs:boolean" minOccurs="0" />
              <xs:element name="CategoryIsCustom" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SettingsCategory" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="c91d8df8-c913-4a06-9605-900d9d1e6ddf" visibility="none" ismacro="false" /><field column="CategoryParentID" fieldcaption="CategoryParentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7b67a457-09bf-4075-a07f-d9f6cd52e16f" visibility="none" ismacro="false" /><field column="CategoryIDPath" fieldcaption="CategoryIDPath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="53774e18-81e8-4e01-8e14-5fccccbe747b" visibility="none" ismacro="false" /><field column="CategoryLevel" fieldcaption="CategoryLevel" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a2978ca3-c075-47fd-b482-de8eec4c8321" visibility="none" ismacro="false" /><field column="CategoryChildCount" fieldcaption="CategoryChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5af06ddd-f3a6-47c1-ac42-551daaf4535d" visibility="none" ismacro="false" /><field column="CategoryIconPath" fieldcaption="CategoryIconPath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2af723e2-e890-49a5-8f88-2c53e6cec49c" visibility="none" ismacro="false" /><field column="CategoryIsGroup" fieldcaption="CategoryIsGroup" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42f525f5-1eac-4e92-aa73-16ea2419c863" visibility="none" ismacro="false" /><field column="CategoryIsCustom" fieldcaption="CategoryIsCustom" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0825124a-b2cb-44d2-bcb8-1ed956c537f4" visibility="none" ismacro="false" /><field column="CategoryDisplayName" fieldcaption="CategoryDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eec20439-a956-4bbd-8f57-68550f811e89" ismacro="false" /><field column="CategoryOrder" fieldcaption="CategoryOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea9b2037-0238-4303-80ed-181bb044f859" ismacro="false" /><field column="CategoryName" fieldcaption="CategoryName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="41261c56-9595-47e8-b89a-0a28de33f38b" ismacro="false" /></form>', N'', N'', N'', N'CMS_SettingsCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100805 12:03:58', 'e85e89d4-b387-48bc-b414-63ebbd2f6b40', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1650, N'Resource string', N'cms.resourcestring', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ResourceString">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StringID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="StringKey">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StringIsCustom" type="xs:boolean" />
              <xs:element name="StringLoadGeneration" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ResourceString" />
      <xs:field xpath="StringID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StringID" fieldcaption="StringID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="cf3d8cdb-d17e-43ac-8487-cd1560722f47" /><field column="StringKey" fieldcaption="StringKey" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="be08e8ec-c73f-47b8-95f3-6b03a09bdfd1" /><field column="StringIsCustom" fieldcaption="StringIsCustom" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="00f715ad-8e5b-4021-9c5a-2a7b9bce22ee" /><field column="StringLoadGeneration" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d5600355-f601-4e45-b1d3-94cdc66ac93a" /></form>', N'', N'', N'', N'CMS_ResourceString', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:54:43', '77097669-a269-4702-af21-8896af411555', 0, 0, 0, N'', 1, N'StringKey', N'0', N'', N'0', N'<search><item searchable="True" name="StringID" tokenized="False" content="False" id="0845a732-4140-47f8-8594-234324a94fe5" /><item searchable="False" name="StringKey" tokenized="True" content="True" id="a35b3a90-fa73-4147-9804-c7368b48aad7" /><item searchable="True" name="StringIsCustom" tokenized="False" content="False" id="099678e2-bb6f-48d7-a46b-ea7031377836" /><item searchable="True" name="StringLoadGeneration" tokenized="False" content="False" id="3c446e10-467f-4305-8318-53340b34c442" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1651, N'Resource translation', N'cms.resourcetranslation', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ResourceTranslation">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TranslationID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TranslationStringID" type="xs:int" />
              <xs:element name="TranslationUICultureID" type="xs:int" />
              <xs:element name="TranslationText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ResourceTranslation" />
      <xs:field xpath="TranslationID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TranslationID" fieldcaption="TranslationID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="15ab1b59-fa4b-4748-98d9-9dcf6003cb9c" /><field column="TranslationStringID" fieldcaption="TranslationStringID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e8c24c6e-12e2-449b-86e7-53482abd3107" /><field column="TranslationUICultureID" fieldcaption="TranslationUICultureID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7c617155-a9e3-4d14-ad88-d9792698ffb7" /><field column="TranslationText" fieldcaption="TranslationText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cf409753-ee8f-46ff-82c1-bcaa8e98d6bc" /></form>', N'', N'', N'', N'CMS_ResourceTranslation', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:54:49', '38ab8c16-f476-4e9c-adbf-3957853aa8bf', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1658, N'VersionAttachment', N'CMS.VersionAttachment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_VersionAttachment">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="VersionHistoryID" type="xs:int" />
              <xs:element name="AttachmentHistoryID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_VersionAttachment" />
      <xs:field xpath="VersionHistoryID" />
      <xs:field xpath="AttachmentHistoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="VersionHistoryID" fieldcaption="VersionHistoryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8dbf917a-6893-468b-8fe1-645ad8c04525" /><field column="AttachmentHistoryID" fieldcaption="AttachmentHistoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4ec8bf47-4c6d-4c8b-bdb9-eca9a3cfb0fb" /></form>', N'', N'', N'', N'CMS_VersionAttachment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:40', 'c7e663c8-d62f-4f1c-b1e5-6b53c9603222', 0, 0, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1659, N'ForumModerator', N'Forums.ForumModerator', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_ForumModerators">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="ForumID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_ForumModerators" />
      <xs:field xpath="UserID" />
      <xs:field xpath="ForumID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4b7c458b-9fcb-476a-8a97-bd15150b9267" /><field column="ForumID" fieldcaption="ForumID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6d20cb97-7a62-4e23-805c-70354f716be8" /></form>', N'', N'', N'', N'Forums_ForumModerators', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110314 11:35:19', '066eef63-d191-4c22-8496-6df89b336aef', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1661, N'Ecommerce - Payment shipping', N'Ecommerce.PaymentShipping', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_PaymentShipping">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PaymentOptionID" type="xs:int" />
              <xs:element name="ShippingOptionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_PaymentShipping" />
      <xs:field xpath="PaymentOptionID" />
      <xs:field xpath="ShippingOptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PaymentOptionID" fieldcaption="PaymentOptionID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="272ec36e-522b-460a-a7b8-0eda8268fe3a" /><field column="ShippingOptionID" fieldcaption="ShippingOptionID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="40152345-957a-429f-83ec-63831f8d2acb" /></form>', N'', N'', N'', N'COM_PaymentShipping', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110324 14:48:11', '6392c9c9-b737-4d54-9266-9a6f2bc33678', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1682, N'WebPartContainerSite', N'CMS.WebPartContainerSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebPartContainerSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContainerID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WebPartContainerSite" />
      <xs:field xpath="ContainerID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContainerID" fieldcaption="ContainerID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e975c62d-2b6d-4807-b726-a2ea8a7d68b1" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aec1fb18-2a3b-4efd-a060-01105aae6183" /></form>', N'', N'', N'', N'CMS_WebPartContainerSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:41', '188d31e3-cff5-42f9-9e6b-b01cd443d1be', 0, 0, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1685, N'File', N'CMS.File', 0, 1, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CONTENT_File">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FileID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="FileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileAttachment" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CONTENT_File" />
      <xs:field xpath="FileID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FileID" fieldcaption="FileID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" minstringlength="0" maxstringlength="0" minnumericvalue="0" maxnumericvalue="0" publicfield="false" spellcheck="true" guid="ee63e3e8-0fce-4099-82bd-50df0c69b2fe" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileName" fieldcaption="File name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="false" columnsize="100" publicfield="false" spellcheck="true" guid="f820a7ac-1c87-4709-bcf8-f16115a81a8e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileDescription" fieldcaption="Description" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" columnsize="500" publicfield="false" spellcheck="true" guid="e8fb4f99-2300-4f3f-9d7e-66e23f9f696c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="FileAttachment" fieldcaption="File" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="true" guid="ce4c5d10-c143-4ada-9d8a-7e7481b167ef" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Extensions>inherit</Extensions><Autoresize_Hashtable>True</Autoresize_Hashtable><controlname>directuploadcontrol</controlname></settings></field></form>', N'', N'', N'FileName', N'CONTENT_File', N'~/CMSModules/Content/CMSDesk/View/ViewFile.aspx', N'', N'', N'~/CMSModules/Content/CMSDesk/New/NewFile.aspx', 0, 1, 0, N'', 1, N'', NULL, '20111125 17:02:26', '5b168902-89b2-448f-9357-277df7dc7291', 0, 0, 0, N'', 1, N'DocumentName', N'DocumentContent', N'FileAttachment', N'DocumentCreatedWhen', N'<search><item tokenized="False" name="FileID" content="False" searchable="True" id="4c84e843-0844-4e5f-8674-81a281ebf529" /><item tokenized="True" name="FileName" content="True" searchable="False" id="2af5049f-e9dc-4dc4-8455-f349fc2a7131" /><item tokenized="True" name="FileDescription" content="True" searchable="False" id="05c72433-2378-4111-a13a-56ccd4fedad7" /><item tokenized="False" name="FileAttachment" content="False" searchable="False" id="a2a5ef49-bf66-478f-8863-7a8dcd0ed3c1" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1719, N'Document alias', N'CMS.DocumentAlias', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_DocumentAlias">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AliasID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AliasNodeID" type="xs:int" />
              <xs:element name="AliasCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AliasURLPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AliasExtensions" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AliasCampaign" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AliasWildcardRule" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AliasPriority" type="xs:int" minOccurs="0" />
              <xs:element name="AliasGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="AliasLastModified" type="xs:dateTime" />
              <xs:element name="AliasSiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_DocumentAlias" />
      <xs:field xpath="AliasID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AliasID" fieldcaption="AliasID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="87333b1f-9786-492b-97b7-508fdd54225e" /><field column="AliasNodeID" fieldcaption="AliasNodeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6574a743-2505-4cf6-9102-7a832a56cfad" /><field column="AliasCulture" fieldcaption="AliasCulture" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="542cc3b3-3857-4507-9cc4-5b33fe534073" /><field column="AliasURLPath" fieldcaption="AliasURLPath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="37700e05-b8d1-42a5-872a-d665eeb1aa99" /><field column="AliasExtensions" fieldcaption="AliasExtensions" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="91cfed84-abb6-462c-962c-c22bc966245c" /><field column="AliasCampaign" fieldcaption="AliasCampaign" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1710ce23-89d1-4443-ad27-087a0cf35477" /><field column="AliasWildcardRule" fieldcaption="AliasWildcardRule" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f02f7c44-7571-45d3-b73c-2cfe2db20415" columnsize="440" visibility="none" ismacro="false" /><field column="AliasPriority" fieldcaption="AliasPriority" visible="true" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c2134711-a476-45db-966d-aa0d6c082c28" /><field column="AliasGUID" visible="false" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db45b3f2-594b-4ae4-86ab-c9b081f34a0c" /><field column="AliasLastModified" visible="false" columntype="datetime" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5333de7f-c978-43e5-9998-2027e628d39f" /><field column="AliasSiteID" fieldcaption="AliasSiteID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ce9a7e6e-0bba-44e4-84c7-3c673c8a79b2" /></form>', N'', N'', N'', N'CMS_DocumentAlias', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110615 10:07:32', '67726e7f-fd77-4d3f-94dd-a9a3bdaec696', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1720, N'Ecommerce - Department tax class', N'ecommerce.departmenttaxclass', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_DepartmentTaxClass">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DepartmentID" type="xs:int" />
              <xs:element name="TaxClassID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_DepartmentTaxClass" />
      <xs:field xpath="DepartmentID" />
      <xs:field xpath="TaxClassID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DepartmentID" fieldcaption="DepartmentID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ceb1cdfa-285e-418d-b2d4-235f81506645" /><field column="TaxClassID" fieldcaption="TaxClassID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="1713a53c-6b68-4cca-8441-427e456ea5fe" /></form>', N'', N'', N'', N'COM_DepartmentTaxClass', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110211 12:49:09', 'de031fe1-8424-43da-9a04-0635feb47484', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1721, N'Category', N'cms.category', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Category">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CategoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CategoryDisplayName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryCount" type="xs:int" />
              <xs:element name="CategoryEnabled" type="xs:boolean" />
              <xs:element name="CategoryUserID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CategoryLastModified" type="xs:dateTime" />
              <xs:element name="CategorySiteID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryParentID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryIDPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryNamePath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryLevel" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryOrder" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Category" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a7e6de80-6774-4fc9-8764-fde25832fce0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CategoryDisplayName" fieldcaption="CategoryDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="62f59aab-9b3e-4ddc-8f7a-fb97e039de40" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryName" fieldcaption="CategoryName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="6ec936dc-3b0f-476c-8d07-9dbb7e5a19b9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryDescription" fieldcaption="CategoryDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="21b4dc27-69d2-4ee5-ad21-31ce7362a169" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="CategoryCount" fieldcaption="CategoryCount" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d333614c-e26b-45fa-803f-2815644d9b6e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryEnabled" fieldcaption="CategoryEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="60a19437-daca-476d-a9c7-f771810012d2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CategoryUserID" fieldcaption="CategoryUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aebc6d41-6911-4955-8566-3e46053d2243" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryGUID" fieldcaption="CategoryGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="09d6706d-6145-400c-9e12-c47f14fdfa44" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="CategoryLastModified" fieldcaption="CategoryLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="67e9377c-c25a-4dd7-9ef8-9b9d97a408ce" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="CategorySiteID" fieldcaption="CategorySiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="55f19a7f-1e83-4f11-8dcd-b15e2c5e2638" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CategoryParentID" fieldcaption="CategoryParentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="70e95a64-6c83-4dc3-a334-c8a7bf61ea39" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CategoryIDPath" fieldcaption="CategoryIDPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="aac5e74e-aa14-43b5-b18c-fd329ba7974c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CategoryNamePath" fieldcaption="CategoryNamePath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="1500" publicfield="false" spellcheck="true" guid="1591d49a-9209-4d55-851c-9fe1769e9def" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CategoryLevel" fieldcaption="CategoryLevel" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="359e5923-49da-4a39-9910-b9458d5a40a2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CategoryOrder" fieldcaption="CategoryOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3b0a29ce-b93f-4ff8-a86b-5273a4ad1336" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Category', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110822 16:05:43', 'f9bd0914-ee13-41f8-85a3-4f2b50875c99', 0, 0, 0, N'', 1, N'CategoryDisplayName', N'CategoryDescription', N'', N'CategoryLastModified', N'<search><item searchable="True" name="CategoryID" tokenized="False" content="False" id="2af92bba-4837-4ccc-86e0-3a82e7b7240c" /><item searchable="False" name="CategoryDisplayName" tokenized="True" content="True" id="1c19ca41-4592-48d9-b7a5-8f8d0ff62e59" /><item searchable="False" name="CategoryName" tokenized="True" content="True" id="13759ff7-992d-4451-9bdf-c3d8d85f4b68" /><item searchable="False" name="CategoryDescription" tokenized="True" content="True" id="8b185f3a-882a-44ec-b1fb-0fdc97b91285" /><item searchable="True" name="CategoryCount" tokenized="False" content="False" id="6e78d8e5-aa71-4988-9467-bc5a88bae712" /><item searchable="True" name="CategoryEnabled" tokenized="False" content="False" id="e4413b44-15e7-499d-9079-f413fb1d4f33" /><item searchable="True" name="CategoryUserID" tokenized="False" content="False" id="ccdaaf0b-da60-40f8-9cad-03e2e46f4aac" /><item searchable="False" name="CategoryGUID" tokenized="False" content="False" id="e79b5ce7-c9a9-4799-a428-0a8c5cee02d2" /><item searchable="True" name="CategoryLastModified" tokenized="False" content="False" id="8b0278c4-f5c6-4c6b-84c4-de7f404fe95e" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1726, N'Document category', N'cms.documentcategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_DocumentCategory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DocumentID" type="xs:int" />
              <xs:element name="CategoryID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_DocumentCategory" />
      <xs:field xpath="DocumentID" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DocumentID" fieldcaption="DocumentID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ace05777-73b8-4c94-862e-058c49920985" /><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="18ad18e0-4e0f-4cd9-b4cc-2caa3ccb283f" /></form>', N'', N'', N'', N'CMS_DocumentCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110315 08:19:28', '88416cd6-9aa6-4ccc-9f9c-434380b8cdc6', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1736, N'Tag group', N'CMS.TagGroup', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_TagGroup">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TagGroupID" type="xs:int" />
              <xs:element name="TagGroupDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TagGroupName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TagGroupDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TagGroupSiteID" type="xs:int" />
              <xs:element name="TagGroupIsAdHoc" type="xs:boolean" />
              <xs:element name="TagGroupLastModified" type="xs:dateTime" />
              <xs:element name="TagGroupGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_TagGroup" />
      <xs:field xpath="TagGroupID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TagGroupID" fieldcaption="TagGroupID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5e8edd4a-c7b0-48aa-8a15-0a9790b66d0e" /><field column="TagGroupDisplayName" fieldcaption="TagGroupDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7252c09e-85f7-47b9-911d-65b38b00b6bc" /><field column="TagGroupName" fieldcaption="TagGroupName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bff5999d-c415-4dd5-9c54-2c669d04cf59" /><field column="TagGroupDescription" fieldcaption="TagGroupDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="30c422da-9c71-4e22-925f-43242f7a7b26" /><field column="TagGroupSiteID" fieldcaption="TagGroupSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="881da915-c269-4baa-a044-f0ffec268312" /><field column="TagGroupIsAdHoc" fieldcaption="TagGroupIsAdHoc" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2c34c408-d66e-4001-a812-631c75ca01e1" /><field column="TagGroupLastModified" fieldcaption="TagGroupLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5042f698-31d8-4e86-b003-6e50839158a1" /><field column="TagGroupGUID" fieldcaption="TagGroupGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="23bea836-c7e5-46a6-af25-2add63da2e08" /></form>', N'', N'', N'', N'CMS_TagGroup', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:07:56', '2bf6dd1b-4ab4-4146-b7de-fd9cd86db7c2', 0, 0, 0, N'', 1, N'TagGroupDisplayName', N'TagGroupDescription', N'', N'TagGroupLastModified', N'<search><item searchable="True" name="TagGroupID" tokenized="False" content="False" id="f15b6ba9-026b-4537-b257-e2ead9f7e892" /><item searchable="False" name="TagGroupDisplayName" tokenized="True" content="True" id="832057fb-d290-40fe-810b-74b25f0ad6c0" /><item searchable="False" name="TagGroupName" tokenized="True" content="True" id="1498d1a6-e0ed-4b65-83f2-024108f9a8ec" /><item searchable="False" name="TagGroupDescription" tokenized="True" content="True" id="d13048d5-14e4-445e-8ca4-775793cc32bd" /><item searchable="True" name="TagGroupSiteID" tokenized="False" content="False" id="74a5b18a-98c7-4471-95a7-03ab14d37d56" /><item searchable="True" name="TagGroupIsAdHoc" tokenized="False" content="False" id="323ab1e1-ecf9-424c-ac01-6912ff619e90" /><item searchable="True" name="TagGroupLastModified" tokenized="False" content="False" id="5df08403-0612-4758-bd42-17b1c5dc7bff" /><item searchable="False" name="TagGroupGUID" tokenized="False" content="False" id="dbbc310a-2d6a-43d5-8f93-0b77a0a3aa72" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1737, N'Document tag', N'cms.documenttag', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_DocumentTag">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DocumentID" type="xs:int" />
              <xs:element name="TagID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_DocumentTag" />
      <xs:field xpath="DocumentID" />
      <xs:field xpath="TagID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'', N'', N'', N'', N'CMS_DocumentTag', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081231 14:55:00', 'ec4ce0c0-f7e9-43af-935e-c67f38bbfad5', 0, 0, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1738, N'Tag', N'cms.tag', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Tag">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TagID" type="xs:int" />
              <xs:element name="TagName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TagCount" type="xs:int" />
              <xs:element name="TagGroupID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Tag" />
      <xs:field xpath="TagID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TagID" fieldcaption="TagID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="10afa018-8f08-43b9-b61d-2991f7a6ed7e" /><field column="TagName" fieldcaption="TagName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="41a12c14-98dc-453b-8559-17ffb6482399" /><field column="TagCount" fieldcaption="TagCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="abd76b88-a87c-42b1-b27e-6fdf27daf597" /><field column="TagGroupID" fieldcaption="TagGroupID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b1fccb81-7d06-4804-be58-db3784ee57a6" /></form>', N'', N'', N'', N'CMS_Tag', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:07:50', '335d8316-ecf9-46d6-b8a1-5f5c162becd1', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1740, N'Membership - Banned IP', N'cms.BannedIP', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_BannedIP">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="IPAddressID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="IPAddress">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IPAddressRegular">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IPAddressAllowed" type="xs:boolean" />
              <xs:element name="IPAddressAllowOverride" type="xs:boolean" />
              <xs:element name="IPAddressBanReason" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IPAddressBanType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IPAddressBanEnabled" type="xs:boolean" minOccurs="0" />
              <xs:element name="IPAddressSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="IPAddressGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="IPAddressLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_BannedIP" />
      <xs:field xpath="IPAddressID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="IPAddressID" fieldcaption="IPAddressID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f17b1adf-5910-410e-a5ce-d368224d39c5" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IPAddress" fieldcaption="{$banip.IPAddress$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="51d8bbe7-d868-4461-bf0f-63d985be58b6" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="IPAddressBanType" fieldcaption="{$banip.IPAddressBanType$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="059fbe9f-1eb4-41c9-b696-70fb34a5a593" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>dropdownlistcontrol</controlname><Options><item value="complete" text="{$banip.bantypecomplete$}" /><item value="login" text="{$banip.bantypelogin$}" /><item value="registration" text="{$banip.bantyperegistration$}" /><item value="allnoncomplete" text="{$banip.bantypeallnoncomplete$}" /></Options></settings></field><field column="IPAddressBanEnabled" fieldcaption="{$general.enabled$}" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="38e3e820-f239-479d-aeea-b7bdc709743e" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="IPAddressBanReason" fieldcaption="{$banip.IPAddressBanReason$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" controlcssclass="TextAreaLarge" guid="a4fd328a-c60c-4d8e-80f6-a75efe0c01e1" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="IPAddressAllowed" fieldcaption="&amp;nbsp;" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="747a7ded-c824-4085-81e5-e69bb9055146" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>radiobuttonscontrol</controlname><RepeatDirection>vertical</RepeatDirection><Options><item value="0" text="{$banip.radBanIP$}" /><item value="1" text="{$banip.radAllowIP$}" /></Options></settings></field><field column="IPAddressAllowOverride" fieldcaption="{$banip.IPAddressAllowOverride$}" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1e5675e-7faa-4fcb-bb2d-d0fa54d809f1" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="IPAddressRegular" fieldcaption="IPAddressRegular" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="607e55d8-26b2-47bb-95d7-53ea227f8ce4" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IPAddressSiteID" fieldcaption="IPAddressSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f1d44929-cacf-4971-ae2b-c9ad81713a51" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IPAddressGUID" fieldcaption="IPAddressGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2f97a7b0-2fe3-40e0-9b1e-7eb435aed8ed" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IPAddressLastModified" fieldcaption="IPAddressLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3c0734cf-b556-4747-bd97-a9780b7fbc8b" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_BannedIP', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:59:23', '0baf8283-cdd6-4b36-9630-42009ede46af', 0, 0, 0, N'', 0, N'IPAddress', N'IPAddressBanReason', N'', N'IPAddressLastModified', N'<search><item searchable="True" name="IPAddressID" tokenized="False" content="False" id="2a585c44-25b6-4591-b4ba-7ea0bf27209b" /><item searchable="False" name="IPAddress" tokenized="True" content="True" id="52f3216e-df34-4f8b-a3e3-2e04846fc5c8" /><item searchable="False" name="IPAddressBanType" tokenized="True" content="True" id="7ee4139a-7161-4867-b0e1-661a28baa43d" /><item searchable="True" name="IPAddressBanEnabled" tokenized="False" content="False" id="a85a6ae3-9f86-4365-bf8c-d5b77e5de81b" /><item searchable="False" name="IPAddressBanReason" tokenized="True" content="True" id="25e0ca48-ea6e-4303-80b2-b58cc4f41ca7" /><item searchable="True" name="IPAddressAllowed" tokenized="False" content="False" id="7a4263b5-00e1-4695-896b-83a92a8bbb3b" /><item searchable="True" name="IPAddressAllowOverride" tokenized="False" content="False" id="33bfacbb-aa55-43cc-b532-e93cedeb0c31" /><item searchable="False" name="IPAddressRegular" tokenized="True" content="True" id="f80689ef-bc8c-4514-8b6e-a528f977e856" /><item searchable="True" name="IPAddressSiteID" tokenized="False" content="False" id="f08dae54-b19b-483f-9c7a-11fde2d8244f" /><item searchable="False" name="IPAddressGUID" tokenized="False" content="False" id="34906dee-103e-4f71-8a5d-6b98ec359feb" /><item searchable="True" name="IPAddressLastModified" tokenized="False" content="False" id="53ceafe8-8383-4c63-a7ce-24d76d0f8180" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1745, N'Alternative forms', N'cms.AlternativeForm', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_AlternativeForm">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FormID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="FormDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormClassID" type="xs:int" />
              <xs:element name="FormDefinition" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormLayout" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FormLastModified" type="xs:dateTime" />
              <xs:element name="FormCoupledClassID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_AlternativeForm" />
      <xs:field xpath="FormID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FormID" fieldcaption="FormID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d65b8a4f-95fb-4a35-9f98-04c2a746f6de" /><field column="FormDisplayName" fieldcaption="FormDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0be5e0a7-1704-47dc-b063-5e6fb3e91c46" /><field column="FormName" fieldcaption="FormName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c90ce6a-e202-4035-a51d-f27469743148" /><field column="FormClassID" fieldcaption="FormClassID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a4749e1-d4b8-4753-9b2e-298433d675c6" /><field column="FormDefinition" fieldcaption="FormDefinition" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1307cae-635b-48b5-a291-e992f746fc9e" /><field column="FormLayout" fieldcaption="FormLayout" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5c028970-d2db-40c6-9284-f01edf3395cf" /><field column="FormGUID" fieldcaption="FormGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b1ff5048-0f5d-462f-818a-b6c582e45db1" /><field column="FormLastModified" fieldcaption="FormLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="12a04abb-af1c-493c-9350-c8c08f5736ff" /><field column="FormCoupledClassID" visible="true" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b9cb787e-87b6-4ebe-86ba-71470ae3e698" fieldcaption="Form Coupled Class ID" /></form>', N'', N'', N'', N'CMS_AlternativeForm', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:06:53', '7d7cbe11-e101-469f-a4a3-ee452f3982df', 0, 0, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1747, N'Time zone', N'cms.timezone', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_TimeZone">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TimeZoneID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TimeZoneName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TimeZoneDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TimeZoneGMT" type="xs:double" />
              <xs:element name="TimeZoneDaylight" type="xs:boolean" minOccurs="0" />
              <xs:element name="TimeZoneRuleStartIn" type="xs:dateTime" />
              <xs:element name="TimeZoneRuleStartRule">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TimeZoneRuleEndIn" type="xs:dateTime" />
              <xs:element name="TimeZoneRuleEndRule">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TimeZoneGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TimeZoneLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_TimeZone" />
      <xs:field xpath="TimeZoneID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TimeZoneID" fieldcaption="TimeZoneID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="16021269-14ff-482a-be81-8870b15426d9" /><field column="TimeZoneName" fieldcaption="TimeZoneName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a8cde1d0-13dd-424d-a309-d9a0dddd6e19" /><field column="TimeZoneDisplayName" fieldcaption="TimeZoneDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a75bc013-14be-45f5-9ee9-3e5d506f3b8b" /><field column="TimeZoneGMT" fieldcaption="TimeZoneGMT" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a77cd69-6cde-47eb-be36-8e8b50a435a9" /><field column="TimeZoneDaylight" fieldcaption="TimeZoneDaylight" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9e95e18b-299e-4cdb-a6f7-0775003a7181" /><field column="TimeZoneRuleStartIn" fieldcaption="TimeZoneRuleStartIn" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8ea9e596-b370-4e8d-a96f-0ff73f839707" /><field column="TimeZoneRuleStartRule" fieldcaption="TimeZoneRuleStartRule" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e4b0ed67-2e61-44bf-9218-97b965cf7281" /><field column="TimeZoneRuleEndIn" fieldcaption="TimeZoneRuleEndIn" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1271bb6b-16b2-4f16-b2e2-89978d9a60e9" /><field column="TimeZoneRuleEndRule" fieldcaption="TimeZoneRuleEndRule" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9c233cde-ce0f-475e-bd45-4e1aa6976167" /></form>', N'', N'', N'', N'CMS_TimeZone', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:08:26', '01d6dfc3-0adc-444b-a86f-de19e72f76ff', 0, 0, 0, N'', 1, N'TimeZoneDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="TimeZoneID" tokenized="False" content="False" id="6698b050-78ca-446c-bf7d-bc408221e6b0" /><item searchable="False" name="TimeZoneName" tokenized="True" content="True" id="dea6156f-296d-4b59-88e1-e7bf53887bdf" /><item searchable="False" name="TimeZoneDisplayName" tokenized="True" content="True" id="b86120c5-57ab-43e3-b728-c162d26e6100" /><item searchable="True" name="TimeZoneGMT" tokenized="False" content="False" id="e3e19887-74a2-49d1-81b1-1f5e501a7e36" /><item searchable="True" name="TimeZoneDaylight" tokenized="False" content="False" id="83dbc72d-b41b-4fd8-9eb6-30b041e4d2c8" /><item searchable="True" name="TimeZoneRuleStartIn" tokenized="False" content="False" id="a7a17462-243d-4a9a-a496-8a9903d5ef01" /><item searchable="False" name="TimeZoneRuleStartRule" tokenized="True" content="True" id="8b871ba5-87a0-4723-9e9a-63afae47d925" /><item searchable="True" name="TimeZoneRuleEndIn" tokenized="False" content="False" id="e4ce22e7-1496-46a0-8155-998eacc18f04" /><item searchable="False" name="TimeZoneRuleEndRule" tokenized="True" content="True" id="a0db70a0-a1dd-425e-b604-b84ebe9d5a72" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1748, N'Group', N'Community.Group', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Community_Group">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="GroupID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="GroupGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="GroupLastModified" type="xs:dateTime" />
              <xs:element name="GroupSiteID" type="xs:int" />
              <xs:element name="GroupDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupNodeGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="GroupApproveMembers" type="xs:int" />
              <xs:element name="GroupAccess" type="xs:int" />
              <xs:element name="GroupCreatedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="GroupApprovedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="GroupAvatarID" type="xs:int" minOccurs="0" />
              <xs:element name="GroupApproved" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupCreatedWhen" type="xs:dateTime" />
              <xs:element name="GroupSendJoinLeaveNotification" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupSendWaitingForApprovalNotification" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupSecurity" type="xs:int" minOccurs="0" />
              <xs:element name="GroupLogActivity" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Community_Group" />
      <xs:field xpath="GroupID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="GroupID" fieldcaption="GroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d92a0eec-1867-4753-8afa-08f0af1fc023" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GroupGUID" fieldcaption="GroupGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8cf05f22-da47-4502-b676-a0411398bc5b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GroupLastModified" fieldcaption="GroupLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c6035dfc-3b72-4a0f-b133-a92e3587cae8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="GroupSiteID" fieldcaption="GroupSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0ead60a9-72c7-4534-8acc-24037b605e4e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupDisplayName" fieldcaption="GroupDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="7fd08d9d-dd31-491e-9c89-cf5b288e3337" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupName" fieldcaption="GroupName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="35fc6c9e-2c1c-4883-82ac-f095ed5b93b1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupDescription" fieldcaption="GroupDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3838358e-5e5b-4a0a-adb9-ac0769302f3d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="GroupNodeGUID" fieldcaption="GroupNodeGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7b29ef51-8705-4845-a92f-eedf6ddfc49c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GroupApproveMembers" fieldcaption="GroupApproveMembers" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a02c3e8f-c88c-4ec1-af62-1642c3a4cbfb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupAccess" fieldcaption="GroupAccess" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a3a470bb-3bee-4b18-8916-529651e021ac" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupCreatedByUserID" fieldcaption="GroupCreatedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="17195728-441a-49da-8c51-24705c2c935a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupApprovedByUserID" fieldcaption="GroupApprovedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f467aa85-3f49-491e-9b0b-99159b8f0457" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupAvatarID" fieldcaption="GroupAvatarID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1f719675-4d6b-4e04-94c8-90c9fbafebfc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupApproved" fieldcaption="GroupApproved" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d73d91c7-edcb-4c8a-bd1d-90f821f5a345" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupCreatedWhen" fieldcaption="GroupCreatedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="216f55d8-156a-4c00-a3a0-a6d6738c907f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="GroupSendJoinLeaveNotification" fieldcaption="GroupSendJoinLeaveNotification" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="24d6c3d4-8783-45ab-a805-d279c35ac029" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupSendWaitingForApprovalNotification" fieldcaption="GroupSendWaitingForApprovalNotification" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9a3d8f1a-d759-4dbc-9bd2-311649b795c0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupSecurity" fieldcaption="GroupSecurity" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="071780b2-4735-46b4-9185-1a90738989f1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupLogActivity" fieldcaption="GroupLogActivity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b661d19d-5974-49d2-b740-423577800060" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Community_Group', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110811 09:35:36', '78a6ade6-ca83-400c-b537-d5213b7162e4', 0, 0, 0, N'', 2, N'GroupDisplayName', N'GroupDescription', N'', N'GroupCreatedWhen', N'<search><item searchable="True" name="GroupID" tokenized="False" content="False" id="5d27654f-0051-419f-b12f-512c46dd89af" /><item searchable="False" name="GroupGUID" tokenized="False" content="False" id="a8031669-320e-4a25-bcab-f345aa8b8e28" /><item searchable="True" name="GroupLastModified" tokenized="False" content="False" id="9a876c85-bb4d-4a73-8beb-a5d897ad0284" /><item searchable="True" name="GroupSiteID" tokenized="False" content="False" id="82145e35-a778-4a67-a2fe-ea5ba6c2be46" /><item searchable="False" name="GroupDisplayName" tokenized="True" content="True" id="a97de802-a9a4-4dc8-9f19-36667add5767" /><item searchable="False" name="GroupName" tokenized="True" content="True" id="5b8ea36d-f324-4af3-82db-41f97842fa2d" /><item searchable="False" name="GroupDescription" tokenized="True" content="True" id="e84e8781-d00f-4614-b319-58c238596fdf" /><item searchable="False" name="GroupNodeGUID" tokenized="False" content="False" id="17c92aa5-4344-4d87-8dc3-8f1db74966a6" /><item searchable="True" name="GroupApproveMembers" tokenized="False" content="False" id="bf0a5032-930f-4133-a55d-c62791e46852" /><item searchable="True" name="GroupAccess" tokenized="False" content="False" id="c31528fb-ba51-427e-9635-5afb3ba71e1e" /><item searchable="True" name="GroupCreatedByUserID" tokenized="False" content="False" id="c1ade4da-5e84-47ea-bc60-a3b3866a5f5f" /><item searchable="True" name="GroupApprovedByUserID" tokenized="False" content="False" id="adfbe820-971a-49ae-9f97-19df480a1f3d" /><item searchable="True" name="GroupAvatarID" tokenized="False" content="False" id="2d02ada0-644e-4186-a557-1a3f23eb13fe" /><item searchable="True" name="GroupApproved" tokenized="False" content="False" id="95510e41-ef1b-4948-a933-7005bff26908" /><item searchable="True" name="GroupCreatedWhen" tokenized="False" content="False" id="733510bb-cff9-4800-b3bd-5ee80418bc5c" /><item searchable="True" name="GroupSendJoinLeaveNotification" tokenized="False" content="False" id="618eb170-17f8-4b78-818e-6231245529e3" /><item searchable="True" name="GroupSendWaitingForApprovalNotification" tokenized="False" content="False" id="d7a821de-3a4a-47bd-951b-3f1fb992dbb6" /><item searchable="True" name="GroupSecurity" tokenized="False" content="False" id="3e4ecd6a-32bf-4a7d-b88b-a9794bffcf6f" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1749, N'Group member', N'Community.GroupMember', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Community_GroupMember">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MemberID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MemberGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MemberUserID" type="xs:int" />
              <xs:element name="MemberGroupID" type="xs:int" />
              <xs:element name="MemberJoined" type="xs:dateTime" />
              <xs:element name="MemberApprovedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="MemberRejectedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="MemberApprovedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="MemberComment" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MemberInvitedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="MemberStatus" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Community_GroupMember" />
      <xs:field xpath="MemberID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MemberID" fieldcaption="MemberID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="6fc6b001-4f9b-4c0e-af51-32a192f22acf" /><field column="MemberGUID" fieldcaption="MemberGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6ef2c92f-5917-4c95-b14d-41b544f2b244" /><field column="MemberUserID" fieldcaption="MemberUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="061e30be-08bf-48a0-844f-6325030adbca" /><field column="MemberGroupID" fieldcaption="MemberGroupID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f5e1481-fdbb-4083-9c95-8fe2574612ff" /><field column="MemberJoined" fieldcaption="MemberJoined" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72f36245-c011-4be6-8069-801f4b382700" /><field column="MemberStatus" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d5bd6bf1-1751-4e8d-b0eb-86bb5d216f5e" /><field column="MemberApprovedWhen" fieldcaption="MemberApprovedWhen" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1f51c903-8960-40d7-ae12-5e44eeaaf966" /><field column="MemberRejectedWhen" fieldcaption="MemberRejectedWhen" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd7aabec-14e4-4614-84a4-ea1a8b802860" /><field column="MemberApprovedByUserID" fieldcaption="MemberApprovedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c7ecf313-ac27-44f1-8375-cb2453df25f0" /><field column="MemberComment" fieldcaption="MemberComment" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9008448d-cca2-4297-93f9-aa65a78110bc" /><field column="MemberInvitedByUserID" fieldcaption="MemberInvitedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="32410c3a-92ca-48aa-987c-b8a73a884961" /></form>', N'', N'', N'', N'Community_GroupMember', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20090824 09:42:49', 'e15cfc90-107f-4196-a39d-7f5ea7824b08', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [CMS_Class] OFF
