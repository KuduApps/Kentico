SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (425, N'Web part', N'cms.webpart', 1, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebPart">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WebPartID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WebPartName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartProperties">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartCategoryID" type="xs:int" />
              <xs:element name="WebPartParentID" type="xs:int" minOccurs="0" />
              <xs:element name="WebPartDocumentation" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WebPartLastModified" type="xs:dateTime" />
              <xs:element name="WebPartType" type="xs:int" minOccurs="0" />
              <xs:element name="WebPartLoadGeneration" type="xs:int" />
              <xs:element name="WebPartLastSelection" type="xs:dateTime" minOccurs="0" />
              <xs:element name="WebPartDefaultValues" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartResourceID" type="xs:int" minOccurs="0" />
              <xs:element name="WebPartCSS" minOccurs="0">
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
      <xs:selector xpath=".//CMS_WebPart" />
      <xs:field xpath="WebPartID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WebPartID" fieldcaption="WebPartID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d3a38f4f-3cab-4767-a9fb-dcd3295ad0e8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="WebPartName" fieldcaption="WebPartName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="1d4f4dfe-8290-4cf4-9ed1-2e39d97902ad" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartDisplayName" fieldcaption="WebPartDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="79585e4d-5501-4cee-b37a-906c43f67fdb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartDescription" fieldcaption="WebPartDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="436ca21c-abcd-4175-b564-f554ff24349f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebPartCSS" fieldcaption="WebPartCSS" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fcbd9e97-6a62-4582-bf5d-01e52f9784f1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textareacontrol</controlname></settings></field><field column="WebPartFileName" fieldcaption="WebPartFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="db339e77-7066-4eb0-ac2a-fa9333dbb45c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartProperties" fieldcaption="WebPartProperties" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5ff2bc7b-8079-4b4d-ba30-7b2fa1aa5269" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebPartCategoryID" fieldcaption="WebPartCategoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7f34b123-3e8d-4fa9-814d-5385d397efd8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartParentID" fieldcaption="WebPartParentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="84c73734-f65c-488a-a169-fd563cab6d40" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartDocumentation" fieldcaption="WebPartDocumentation" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43aa4a09-b2ea-4685-9edc-fb0ac2179df4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebPartGUID" fieldcaption="WebPartGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b55f8e33-b63c-4fe0-98ad-416d6d6b05e5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="WebPartLastModified" fieldcaption="WebPartLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c1ab9701-42aa-4cf1-aa79-70952c79c9a6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="WebPartType" fieldcaption="WebPartType" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a01db9d-bc2e-4924-a8b9-48d5a0b88f08" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLoadGeneration" fieldcaption="WebPartLoadGeneration" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a650ce19-a7e6-4e79-8700-dc5405904902" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLastSelection" fieldcaption="WebPartLastSelection" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a881f02b-f419-43b9-addd-9e1bb636f31b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="WebPartDefaultValues" fieldcaption="Web part default values" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dcb81f77-1f3f-48f8-8a63-915cbe552552" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebPartResourceID" fieldcaption="Web part module ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="970542bb-05ab-4808-a7b0-7a5fe3d49220" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_WebPart', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110324 14:25:53', '347a3154-07b4-4fd6-8b70-ce0391f76007', 0, 1, 0, N'', 0, N'WebPartDisplayName', N'WebPartDescription', N'', N'WebPartLastSelection', N'<search><item searchable="True" name="WebPartID" tokenized="False" content="False" id="5401474f-2fab-470b-a5fe-5bdd413cfcc9" /><item searchable="False" name="WebPartName" tokenized="True" content="True" id="1de1f98e-f057-4ab6-9dd4-33dd243b3dd0" /><item searchable="False" name="WebPartDisplayName" tokenized="True" content="True" id="6179fa91-aac1-482d-9d5b-f8384b1f5cc7" /><item searchable="False" name="WebPartDescription" tokenized="True" content="True" id="b3a1cff6-661c-40a6-b7a1-a23ef27f4127" /><item searchable="False" name="WebPartFileName" tokenized="True" content="True" id="3fefb063-ecf9-4dcc-88cd-8a644bbef9fd" /><item searchable="False" name="WebPartProperties" tokenized="True" content="True" id="28715c52-1284-4a00-85af-e2dfcc94e76e" /><item searchable="True" name="WebPartCategoryID" tokenized="False" content="False" id="adb0ff03-0522-4ad9-98e7-5b6768159e75" /><item searchable="True" name="WebPartParentID" tokenized="False" content="False" id="16d23d81-df6b-4adb-8e1f-a0250897a22b" /><item searchable="False" name="WebPartDocumentation" tokenized="True" content="True" id="f727d5e4-c286-450c-83be-33df1669d41b" /><item searchable="False" name="WebPartGUID" tokenized="False" content="False" id="b2d9c1e9-1a4d-4fa1-a034-fd985ef43ccf" /><item searchable="True" name="WebPartLastModified" tokenized="False" content="False" id="954dc1f0-0d98-4693-a996-f9bc4461963a" /><item searchable="True" name="WebPartType" tokenized="False" content="False" id="01b7d8ec-7386-4ab8-af2c-701d5340da51" /><item searchable="True" name="WebPartLoadGeneration" tokenized="False" content="False" id="fa36d0b6-af23-43a8-ab7c-de720fd50971" /><item searchable="True" name="WebPartLastSelection" tokenized="False" content="False" id="221ce8a3-b66d-4a2e-9bdd-103d9ad5acbc" /><item searchable="False" name="WebPartDefaultValues" tokenized="True" content="True" id="0afbf476-9c48-490c-b710-d6b70ec2a94e" /><item searchable="True" name="WebPartResourceID" tokenized="False" content="False" id="92313116-02a1-4426-9562-196800fb433c" /></search>', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (490, N'UI culture', N'cms.uiculture', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_UICulture">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UICultureID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UICultureName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UICultureCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UICultureGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="UICultureLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_UICulture" />
      <xs:field xpath="UICultureID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UICultureID" fieldcaption="UICultureID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0e71eca6-a095-4fcb-838c-4bc9a10947f7" /><field column="UICultureName" fieldcaption="UICultureName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f845dfbb-a645-4f55-a68f-dc317f842802" /><field column="UICultureCode" fieldcaption="UICultureCode" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ef053470-1986-4676-9bf3-6b384d37d127" /><field column="UICultureGUID" fieldcaption="UICultureGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="597bcede-5b8e-43b5-b80a-125c049c671a" /><field column="UICultureLastModified" fieldcaption="UICultureLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3d7274f5-c67d-4819-8cf6-fb30c450c1f6" /></form>', N'', N'', N'', N'CMS_UICulture', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:53:00', 'c5ac2fce-1d32-49b6-b36d-533610a50988', 0, 1, 0, N'', 1, N'UICultureName', N'0', N'', N'UICultureLastModified', N'<search><item searchable="True" name="UICultureID" tokenized="False" content="False" id="6c8294d5-3de1-4845-8c27-1c2eb23bb411" /><item searchable="False" name="UICultureName" tokenized="True" content="True" id="b6374db2-053c-4105-8ccb-f1648645860a" /><item searchable="False" name="UICultureCode" tokenized="True" content="True" id="ce441586-026b-4bd8-82c7-99a233ddeda4" /><item searchable="False" name="UICultureGUID" tokenized="False" content="False" id="617185fa-6e0b-4442-84ed-cb16224eb758" /><item searchable="True" name="UICultureLastModified" tokenized="False" content="False" id="b3e377df-f321-4c18-b776-98d487b969a9" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (500, N'Web template', N'cms.webtemplate', 1, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebTemplate">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WebTemplateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WebTemplateDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebTemplateFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebTemplateDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebTemplateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WebTemplateLastModified" type="xs:dateTime" />
              <xs:element name="WebTemplateName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebTemplateOrder" type="xs:int" />
              <xs:element name="WebTemplateLicenses">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebTemplatePackages" minOccurs="0">
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
      <xs:selector xpath=".//CMS_WebTemplate" />
      <xs:field xpath="WebTemplateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WebTemplateID" fieldcaption="WebTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="bedd2cc2-8f3d-4693-9d70-7a79b1b559d4" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="WebTemplateDisplayName" fieldcaption="WebTemplateDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1640e39-619a-46d7-ba7c-84dfe8c6e544" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebTemplateFileName" fieldcaption="WebTemplateFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="015f84fe-a23d-43c8-8f18-58a912bda714" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebTemplateDescription" fieldcaption="WebTemplateDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c94c476d-f821-4d18-a625-44b1845b7f48" ismacro="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebTemplateGUID" fieldcaption="WebTemplateGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8ce11f28-e4f7-4139-9ffc-6f069a8c4bc6" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="WebTemplateLastModified" fieldcaption="WebTemplateLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="28416deb-c52c-477c-a27c-c4a29efe0811" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="WebTemplateName" fieldcaption="WebTemplateName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ead12402-8ffc-413b-8d53-4ce290b7556a" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebTemplateOrder" fieldcaption="WebTemplateOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c285f89e-b479-4bec-8bd8-f24ffdeb42e1" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebTemplateLicenses" fieldcaption="WebTemplateLicenses" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b5634cf7-4ad7-4dde-b37c-df2975d0a04c" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebTemplatePackages" fieldcaption="WebTemplatePackages" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" columnsize="200" publicfield="false" spellcheck="false" guid="211b54cf-02dc-4ef1-806f-f988e4bb8bd7" visibility="none" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_WebTemplate', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:14:06', '710b58bd-d39a-4f5c-93a8-42c8d886da2c', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (504, N'Attachment', N'cms.attachment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Attachment">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AttachmentID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AttachmentName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="255" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentSize" type="xs:int" />
              <xs:element name="AttachmentMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentBinary" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="AttachmentImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentDocumentID" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AttachmentLastHistoryID" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentLastModified" type="xs:dateTime" />
              <xs:element name="AttachmentIsUnsorted" type="xs:boolean" minOccurs="0" />
              <xs:element name="AttachmentOrder" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentGroupGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="AttachmentFormGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="AttachmentHash" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="32" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentCustomData" minOccurs="0">
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
      <xs:selector xpath=".//CMS_Attachment" />
      <xs:field xpath="AttachmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AttachmentID" fieldcaption="AttachmentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b985a545-89fe-4ff3-8dd2-337300735cc0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentName" fieldcaption="AttachmentName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="255" publicfield="false" spellcheck="true" guid="2a47c2d0-d90b-4f7d-aff6-f372a9912c74" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentExtension" fieldcaption="AttachmentExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="4c3bd725-76e9-445c-96cf-80f750bfefd8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentSize" fieldcaption="AttachmentSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e1e4069a-1cce-4d77-bba8-bee75d823cd4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentMimeType" fieldcaption="AttachmentMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="656c281f-de55-4ae4-94ff-7fcdda523c6c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentBinary" fieldcaption="AttachmentBinary" visible="true" columntype="binary" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fffa4cb8-cda7-4e8d-9f18-b713a6fda407" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentImageWidth" fieldcaption="AttachmentImageWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42f78c33-8aff-442c-8d86-be4150e2927e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentImageHeight" fieldcaption="AttachmentImageHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="52a66a03-154a-44e3-84ba-1328bc6329b0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentDocumentID" fieldcaption="AttachmentDocumentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c05d18d4-cd4f-4855-998c-cec7d5a60fc6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentGUID" fieldcaption="AttachmentGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7f35eb41-f171-41b8-83f9-9d6fe00cf407" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentLastHistoryID" fieldcaption="AttachmentLastHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="247ac0af-3dd1-4fae-b8b1-c43ff9589d6a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentSiteID" fieldcaption="AttachmentSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3c5f0fd7-34c9-45d3-ba14-8b8e14b8f42a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentLastModified" fieldcaption="AttachmentLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="91858b60-a074-4fe4-836c-36170849e389" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="AttachmentIsUnsorted" fieldcaption="AttachmentIsUnsorted" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f07f10b2-bbab-46d0-ac1e-a5eb71e69579" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AttachmentOrder" fieldcaption="AttachmentOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29d985ab-ad73-4a2e-b3cf-4d328a2921f0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentGroupGUID" fieldcaption="AttachmentGroupGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1d321bde-cea0-45cf-a4a3-0ef2c412b0b8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentFormGUID" fieldcaption="AttachmentFormGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b43b925b-a809-4697-9a18-a771af70e08c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentHash" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="32" publicfield="false" spellcheck="true" guid="8fcd51df-1b8b-4086-9bab-812d2c1d2908" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentTitle" fieldcaption="AttachmentTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="b27e61f4-cbd7-4990-b372-c368bf5a1f9f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentDescription" fieldcaption="AttachmentDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="06dcf746-1d89-4b15-90c5-fc4ec6aabaec" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field column="AttachmentCustomData" fieldcaption="AttachmentCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c2d9e94c-0164-4bd3-9721-e42a4595de4e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field></form>', N'', N'', N'', N'CMS_Attachment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111006 02:13:44', 'af2b10c3-0037-42e9-8b5a-e4e1a90b81e6', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (516, N'Attachment history', N'cms.attachmenthistory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_AttachmentHistory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AttachmentHistoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AttachmentName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="255" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentSize" type="xs:int" />
              <xs:element name="AttachmentMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentBinary" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="AttachmentImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentDocumentID" type="xs:int" />
              <xs:element name="AttachmentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AttachmentIsUnsorted" type="xs:boolean" minOccurs="0" />
              <xs:element name="AttachmentOrder" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentGroupGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="AttachmentHash" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="32" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_AttachmentHistory" />
      <xs:field xpath="AttachmentHistoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AttachmentHistoryID" fieldcaption="AttachmentHistoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="86c5ec11-c8d9-4563-9aa6-c34c7e309859" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentName" fieldcaption="AttachmentName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="255" publicfield="false" spellcheck="true" guid="632cc328-92ac-4f9b-8f8e-055fc684d60a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentExtension" fieldcaption="AttachmentExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="88804def-9871-4dd3-947e-36d91ed97682" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentSize" fieldcaption="AttachmentSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="00919c3c-e110-4eda-9669-85cb1e2c3a41" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentMimeType" fieldcaption="AttachmentMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="38063b41-d910-4166-bc14-fdfe6d003c28" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentBinary" fieldcaption="AttachmentBinary" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="53774aec-ef12-4e40-93b2-dcb2b57cf8ce" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentImageWidth" fieldcaption="AttachmentImageWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1993f67a-0004-4365-ba55-320445401f91" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentImageHeight" fieldcaption="AttachmentImageHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dde1218e-27d0-4e6c-82c0-b79f7c9017be" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentDocumentID" fieldcaption="AttachmentDocumentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c93b7183-23aa-4418-bf5e-2e064945c7ba" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentGUID" fieldcaption="AttachmentGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6da57418-94ce-4af9-a981-82fb58214236" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentIsUnsorted" fieldcaption="AttachmentIsUnsorted" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4fca9cf7-2b4a-48bc-90d7-13c93d20c3b7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AttachmentOrder" fieldcaption="AttachmentOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02b696d5-7eaf-4f54-a96a-a22d4ecb4851" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentGroupGUID" fieldcaption="AttachmentGroupGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a9ec6492-5ccb-44a8-be38-edb173e968f9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentHash" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="32" publicfield="false" spellcheck="true" guid="fd7e4017-edff-4c89-968b-5d1122b426ea" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentTitle" fieldcaption="AttachmentTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="4ab635f0-735d-4e5d-877d-daba1af1f7b6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentDescription" fieldcaption="AttachmentDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb0fe5a8-65e8-4c89-83bc-e9afa790c00f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field column="AttachmentCustomData" fieldcaption="AttachmentCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aefb4cae-fc4b-469f-bcbb-72f0fa4b9797" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field column="AttachmentLastModified" fieldcaption="AttachmentLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e2d6e0b8-26fd-49bc-bfa7-147591120e79" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field></form>', N'', N'', N'', N'CMS_AttachmentHistory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111006 06:41:29', '5d723ee7-8df3-4253-a0de-8cb8534a1e17', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (530, N'Relationship name', N'cms.relationshipname', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_RelationshipName">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="RelationshipNameID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="RelationshipDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="RelationshipName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="RelationshipAllowedObjects" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="RelationshipGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="RelationshipLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_RelationshipName" />
      <xs:field xpath="RelationshipNameID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="RelationshipNameID" fieldcaption="RelationshipNameID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="47839bd6-f19c-4cfd-b67f-1ca754694d46" /><field column="RelationshipDisplayName" fieldcaption="RelationshipDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6515b190-003a-44b6-b541-8814760de218" /><field column="RelationshipName" fieldcaption="RelationshipName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42221f4a-30fa-47a6-bc80-3f99ee81f8a5" /><field column="RelationshipAllowedObjects" fieldcaption="RelationshipAllowedObjects" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2a02c9d5-f0f9-4a19-be8d-9a007f4464ac" /><field column="RelationshipGUID" fieldcaption="RelationshipGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="03ad948a-2bb7-44b2-b580-b05abf3a2a8b" /><field column="RelationshipLastModified" fieldcaption="RelationshipLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea7edf35-ed86-4cef-91c5-7bfdde27c389" /></form>', N'', N'', N'', N'CMS_RelationshipName', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:55:11', 'bcf36881-2644-4955-a591-7bc93157bf96', 0, 1, 0, N'', 1, N'RelationshipDisplayName', N'0', N'', N'RelationshipLastModified', N'<search><item searchable="True" name="RelationshipNameID" tokenized="False" content="False" id="89896f5c-8388-4940-bad6-ba6229d90522" /><item searchable="False" name="RelationshipDisplayName" tokenized="True" content="True" id="8b8ed02d-bc42-48b0-a9e3-3e8dca31c195" /><item searchable="False" name="RelationshipName" tokenized="True" content="True" id="c133394d-5c34-4812-94cb-8553f159b448" /><item searchable="False" name="RelationshipAllowedObjects" tokenized="True" content="True" id="681668ba-e6ee-420e-8ebc-cc9e023b384b" /><item searchable="False" name="RelationshipGUID" tokenized="False" content="False" id="afee414a-6a57-413b-8224-036820b879c8" /><item searchable="True" name="RelationshipLastModified" tokenized="False" content="False" id="a7dcb781-133b-4eb1-8373-262929633d5b" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (536, N'RelationshipNameSite', N'CMS.RelationshipNameSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_RelationshipNameSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="RelationshipNameID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_RelationshipNameSite" />
      <xs:field xpath="RelationshipNameID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="RelationshipNameID" fieldcaption="RelationshipNameID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e1f7d35d-e4b0-4df9-b63f-c855b306247c" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="556cd134-f2b3-4ff4-8bbe-d7a7dce76095" /></form>', N'', N'', N'', N'CMS_RelationshipNameSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:21', 'b9bc174e-8753-4d6d-bac1-f3ebb2b77ba8', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (537, N'Relationship', N'CMS.Relationship', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Relationship">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="LeftNodeID" type="xs:int" />
              <xs:element name="RightNodeID" type="xs:int" />
              <xs:element name="RelationshipNameID" type="xs:int" />
              <xs:element name="RelationshipCustomData" minOccurs="0">
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
      <xs:selector xpath=".//CMS_Relationship" />
      <xs:field xpath="LeftNodeID" />
      <xs:field xpath="RightNodeID" />
      <xs:field xpath="RelationshipNameID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="LeftNodeID" fieldcaption="LeftNodeID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ace8cd3a-d954-4db1-8adb-895330e7f6b2" /><field column="RightNodeID" fieldcaption="RightNodeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6ef2e0ec-a0bc-451e-ade1-1f1b17d10d79" /><field column="RelationshipNameID" fieldcaption="RelationshipNameID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f0ba6614-31ed-4544-8378-855c69df9e56" /><field column="RelationshipCustomData" fieldcaption="RelationshipCustomData" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f3f9e7ed-c3e0-4765-8c91-577db743b687" /></form>', N'', N'', N'', N'CMS_Relationship', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:55:18', '784809db-885c-4619-89fd-ef1c59066839', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (555, N'CSS stylesheet', N'cms.cssstylesheet', 1, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_CssStyleSheet">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StylesheetID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="StylesheetDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StylesheetName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StylesheetText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StylesheetCheckedOutByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="StylesheetCheckedOutMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StylesheetCheckedOutFileName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StylesheetVersionGUID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StylesheetGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="StylesheetLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_CssStyleSheet" />
      <xs:field xpath="StylesheetID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StylesheetID" fieldcaption="StylesheetID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e4954d9f-c7b4-4aed-900d-b4a41baad967" /><field column="StylesheetDisplayName" fieldcaption="StylesheetDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f19d3a0-ff81-4fd7-818e-ca9c964e2c1d" /><field column="StylesheetName" fieldcaption="StylesheetName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f0a6eecf-5ba4-40de-8c0b-3a17dee2484f" /><field column="StylesheetText" fieldcaption="StylesheetText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7d418e04-4553-4ed3-9658-61f86e99abd7" /><field column="StylesheetCheckedOutByUserID" fieldcaption="StylesheetCheckedOutByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="782d505d-1193-4850-99dd-f424669819fa" /><field column="StylesheetCheckedOutMachineName" fieldcaption="StylesheetCheckedOutMachineName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="18ee9d8c-942f-48fe-ab7a-e0472de39615" /><field column="StylesheetCheckedOutFileName" fieldcaption="StylesheetCheckedOutFileName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb22239d-2f29-41c3-b55a-c3ac8ac9212c" /><field column="StylesheetVersionGUID" fieldcaption="StylesheetVersionGUID" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a8b4a916-8414-4932-81bb-93200820f30b" /><field column="StylesheetGUID" fieldcaption="StylesheetGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="919f9210-e1c5-4722-a0b1-efffba6f4fcc" /><field column="StylesheetLastModified" fieldcaption="StylesheetLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f02b9848-d685-435e-b51b-9ad5896ea338" /></form>', N'', N'', N'', N'CMS_CssStyleSheet', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:06:42', 'e6b9c8bb-e87b-48ec-949e-9158f60ff91c', 0, 1, 0, N'', 0, N'StylesheetDisplayName', N'StylesheetText', N'', N'StylesheetLastModified', N'<search><item searchable="True" name="StylesheetID" tokenized="False" content="False" id="860b5f61-0b3c-4581-b41a-4cf124a15065" /><item searchable="False" name="StylesheetDisplayName" tokenized="True" content="True" id="4715fb39-da6d-4a07-a559-3c0b761e202d" /><item searchable="False" name="StylesheetName" tokenized="True" content="True" id="29a956f0-231d-4648-b90c-d16ff771b792" /><item searchable="False" name="StylesheetText" tokenized="True" content="True" id="6669fc43-3af2-4302-a42a-fa0e3d4a98d9" /><item searchable="True" name="StylesheetCheckedOutByUserID" tokenized="False" content="False" id="ec9b90fa-b633-4e93-aff9-a206c377b164" /><item searchable="False" name="StylesheetCheckedOutMachineName" tokenized="True" content="True" id="cec0a5b7-70b0-4ba8-822a-b0f7921ffa0a" /><item searchable="False" name="StylesheetCheckedOutFileName" tokenized="True" content="True" id="f4f97a68-13db-4adf-9bab-2b3aa1499746" /><item searchable="False" name="StylesheetVersionGUID" tokenized="True" content="True" id="84a3d120-3942-45c2-b1ee-c5b03c309c0d" /><item searchable="False" name="StylesheetGUID" tokenized="False" content="False" id="ef38bbd2-b61e-426b-bfac-63c97081b1a3" /><item searchable="True" name="StylesheetLastModified" tokenized="False" content="False" id="a470451a-13ea-4dc3-bc09-5d35a724bade" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (661, N'Site domain alias', N'cms.sitedomainalias', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SiteDomainAlias">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SiteDomainAliasID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SiteDomainAliasName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteID" type="xs:int" />
              <xs:element name="SiteDefaultVisitorCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDomainGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SiteDomainLastModified" type="xs:dateTime" />
              <xs:element name="SiteDomainDefaultAliasPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteDomainRedirectUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SiteDomainAlias" />
      <xs:field xpath="SiteDomainAliasID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SiteDomainAliasID" fieldcaption="SiteDomainAliasID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="97dd2de6-e021-4be6-afc0-98bb257c2697" /><field column="SiteDomainAliasName" fieldcaption="SiteDomainAliasName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0128ef2c-a751-404e-a4ca-e635da780b90" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6bcc14c5-26c8-4219-95fa-b17014252d3a" /><field column="SiteDefaultVisitorCulture" fieldcaption="SiteDefaultVisitorCulture" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a69982ab-2f9e-4c9d-a11a-caa1bee222aa" /><field column="SiteDomainGUID" fieldcaption="SiteDomainGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f8ec7b9-bd18-4c2f-aa37-86cc2e848fa7" /><field column="SiteDomainLastModified" fieldcaption="SiteDomainLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9ddf6eaa-3970-4615-9000-e40513eaa8c3" /><field column="SiteDomainDefaultAliasPath" fieldcaption="SiteDomainDefaultAliasPath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="8552c789-8c92-4639-8b27-22e5c07706ae" /><field column="SiteDomainRedirectUrl" fieldcaption="SiteDomainRedirectUrl" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="f8cfa857-2f5c-465d-aa72-8d23841f0334" /></form>', N'', N'', N'', N'CMS_SiteDomainAlias', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:08:54', 'a72f9763-8c28-4314-981e-6247776f8134', 0, 1, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (846, N'Form user control', N'cms.formusercontrol', 1, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_FormUserControl">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserControlID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UserControlDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserControlCodeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserControlFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserControlForText" type="xs:boolean" />
              <xs:element name="UserControlForLongText" type="xs:boolean" />
              <xs:element name="UserControlForInteger" type="xs:boolean" />
              <xs:element name="UserControlForDecimal" type="xs:boolean" />
              <xs:element name="UserControlForDateTime" type="xs:boolean" />
              <xs:element name="UserControlForBoolean" type="xs:boolean" />
              <xs:element name="UserControlForFile" type="xs:boolean" />
              <xs:element name="UserControlShowInBizForms" type="xs:boolean" />
              <xs:element name="UserControlDefaultDataType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserControlDefaultDataTypeSize" type="xs:int" minOccurs="0" />
              <xs:element name="UserControlShowInDocumentTypes" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlShowInSystemTables" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlShowInWebParts" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlShowInReports" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="UserControlLastModified" type="xs:dateTime" />
              <xs:element name="UserControlForGuid" type="xs:boolean" />
              <xs:element name="UserControlShowInCustomTables" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlForVisibility" type="xs:boolean" />
              <xs:element name="UserControlParameters" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserControlForDocAttachments" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlForLongInteger" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserControlResourceID" type="xs:int" minOccurs="0" />
              <xs:element name="UserControlType" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_FormUserControl" />
      <xs:field xpath="UserControlID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserControlID" fieldcaption="UserControlID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="12a7dbac-256a-4620-8c83-ebef77554f29" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserControlDisplayName" fieldcaption="UserControlDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="58a7d57f-08b3-4357-bf7c-1681268bf702" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserControlCodeName" fieldcaption="UserControlCodeName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="c78acd92-a019-4830-8b20-56129d0d056f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserControlFileName" fieldcaption="UserControlFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="400" publicfield="false" spellcheck="true" guid="6928eca4-3189-4172-b8a5-53d28a4e6966" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserControlForText" fieldcaption="UserControlForText" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="083e22af-aa8e-43ee-ba88-652dc8f24ba8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForLongText" fieldcaption="UserControlForLongText" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ed4af8dc-0bc5-4628-a3ac-da94198f24d6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForInteger" fieldcaption="UserControlForInteger" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a3b8e554-24f2-46fd-a547-6d31bf223066" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForLongInteger" fieldcaption="UserControlForLongInteger" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6219a136-b2b3-4878-a4d7-aa063f3c2af1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForDecimal" fieldcaption="UserControlForDecimal" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="58ce5d88-bd20-4c62-be7f-5174c98c20e3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForDateTime" fieldcaption="UserControlForDateTime" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0969400a-4930-4389-b6dd-3e70aa97ee91" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForBoolean" fieldcaption="UserControlForBoolean" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d60d1960-04a9-4857-8bdf-6fb82b425a8a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForFile" fieldcaption="UserControlForFile" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7b7c90e8-a739-4f82-baf9-6cb912f7fcb1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForDocAttachments" fieldcaption="User Control For Document Attachments" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="63c34a62-59b3-44dc-a33b-dbfcf832d3fe" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForGuid" fieldcaption="UserControlForGuid" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5145d64a-c176-463a-89bb-19af77bb8e4d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlForVisibility" fieldcaption="UserControlForVisibility" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9e466fcd-eae2-45f9-bef2-f9ea11a18ea1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlShowInBizForms" fieldcaption="UserControlShowInBizForms" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="06591de6-0995-4cad-8931-2bb863c326f3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlDefaultDataType" fieldcaption="UserControlDefaultDataType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="9ba27907-21f5-4b91-bef7-7bc775e5a17c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserControlDefaultDataTypeSize" fieldcaption="UserControlDefaultDataTypeSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="15fd80cc-e8e3-47ff-9892-cf8719ba1c2c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserControlShowInDocumentTypes" fieldcaption="UserControlShowInDocumentTypes" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db50df09-ceff-498e-adf5-22f59ae02711" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlShowInSystemTables" fieldcaption="UserControlShowInSystemTables" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="95dd83fb-ec56-41cb-908b-3ef96d173c98" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlShowInWebParts" fieldcaption="UserControlShowInWebParts" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="597a5ea1-9775-4cb1-baa5-ce9c66d21d33" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlShowInReports" fieldcaption="UserControlShowInReports" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="336640d5-2347-45f8-afcb-6b2a97d59dd0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlGUID" fieldcaption="UserControlGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="94342765-2117-4427-9fe9-ba8e89c657e4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserControlLastModified" fieldcaption="UserControlLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a74a847c-8357-434e-9096-7666c50bb870" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="UserControlShowInCustomTables" fieldcaption="UserControlShowInCustomTables" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d3a2ed0b-5b90-48f2-8fe8-6e4aef81f626" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="UserControlParameters" fieldcaption="UserControlParameters" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d0857b52-d00b-466f-a48c-fc0842f619d7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="UserControlResourceID" fieldcaption="Form control module ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f6afb12a-6482-42d5-9452-9a130f7d1d9a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserControlType" fieldcaption="UserControlType" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="4d61220d-909f-499b-96dc-b8728c12f5aa" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_FormUserControl', N'Null', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:49:31', 'ee83afe0-b6e8-442c-a643-13f670672466', 0, 1, 0, N'', 1, N'UserControlDisplayName', N'0', N'', N'UserControlLastModified', N'<search><item searchable="True" name="UserControlID" tokenized="False" content="False" id="c26fa479-5f12-4798-96b0-88dbe9160abd" /><item searchable="False" name="UserControlDisplayName" tokenized="True" content="True" id="a8dec2d7-fba9-4f56-baf6-dc77536e658b" /><item searchable="False" name="UserControlCodeName" tokenized="True" content="True" id="8d059f95-77a0-4a7d-a167-1dd591f8204e" /><item searchable="False" name="UserControlFileName" tokenized="True" content="True" id="b57cdb92-569f-4354-a89f-a45dc537f337" /><item searchable="True" name="UserControlForText" tokenized="False" content="False" id="5cf6d5b2-bda2-446e-8cd7-7ad701a0c952" /><item searchable="True" name="UserControlForLongText" tokenized="False" content="False" id="1dd77a9d-cacd-4c41-b93d-5dc0ec228a2e" /><item searchable="True" name="UserControlForInteger" tokenized="False" content="False" id="a72c3499-210a-4490-9203-092a0b1274a4" /><item searchable="True" name="UserControlForLongInteger" tokenized="False" content="False" id="096b5a47-3456-4d4a-ae54-6687da1d229b" /><item searchable="True" name="UserControlForDecimal" tokenized="False" content="False" id="cbe13ec2-b3a7-4027-a6ef-4e4b57f02e81" /><item searchable="True" name="UserControlForDateTime" tokenized="False" content="False" id="1234b7c4-17f1-40d7-bff4-6fda6bf47331" /><item searchable="True" name="UserControlForBoolean" tokenized="False" content="False" id="549c27ef-8c19-4637-936f-70da999acaeb" /><item searchable="True" name="UserControlForFile" tokenized="False" content="False" id="4fe4529a-0731-4b89-bc47-47bbe7c52475" /><item searchable="True" name="UserControlForDocAttachments" tokenized="False" content="False" id="3f392968-7575-42a1-b07c-7b52699a552b" /><item searchable="True" name="UserControlForGuid" tokenized="False" content="False" id="79c079d7-ade6-4982-a766-03e59247fe83" /><item searchable="True" name="UserControlForVisibility" tokenized="False" content="False" id="f2e1996b-f72c-4d89-b6c9-0c2f4cd4aa98" /><item searchable="True" name="UserControlShowInBizForms" tokenized="False" content="False" id="265bf9b3-fa41-49a2-ab49-266d1bb8a776" /><item searchable="False" name="UserControlDefaultDataType" tokenized="True" content="True" id="3c79c63b-986d-47ee-b919-7d6961fb3437" /><item searchable="True" name="UserControlDefaultDataTypeSize" tokenized="False" content="False" id="491917ba-84ff-41f7-adca-fd240b2c5c55" /><item searchable="True" name="UserControlShowInDocumentTypes" tokenized="False" content="False" id="3f8125fe-e739-452f-81fc-c5ba5cd8f099" /><item searchable="True" name="UserControlShowInSystemTables" tokenized="False" content="False" id="8cfcc9c9-f7d3-419c-a644-39dc66bb9c8d" /><item searchable="True" name="UserControlShowInWebParts" tokenized="False" content="False" id="430a41af-ecf5-4ef0-bfea-3977fbe49490" /><item searchable="True" name="UserControlShowInReports" tokenized="False" content="False" id="0699a0de-62d7-48b0-86f0-26a8b0c4d1db" /><item searchable="False" name="UserControlGUID" tokenized="False" content="False" id="7f99e978-cd10-48b7-979f-ee355adfa2ef" /><item searchable="True" name="UserControlLastModified" tokenized="False" content="False" id="171a5ec7-12d8-40e4-a1e6-f08664752630" /><item searchable="True" name="UserControlShowInCustomTables" tokenized="False" content="False" id="b94c255c-d4ed-4cdf-b480-e1c80efd326e" /><item searchable="False" name="UserControlParameters" tokenized="True" content="True" id="5c1d78bf-6e18-4b11-ab28-97f85e13d077" /><item searchable="True" name="UserControlResourceID" tokenized="False" content="False" id="2c37e967-05da-4344-9f6d-017bc2a2e0cc" /><item searchable="True" name="UserControlType" tokenized="False" content="False" id="45cf696a-462c-4508-b7dd-1534a657f845" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (877, N'Inline control', N'cms.inlinecontrol', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_InlineControl">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ControlID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ControlDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ControlName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ControlParameterName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ControlDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ControlGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ControlLastModified" type="xs:dateTime" />
              <xs:element name="ControlFileName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ControlProperties" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ControlResourceID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_InlineControl" />
      <xs:field xpath="ControlID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ControlID" fieldcaption="ControlID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e85912e4-5a1a-4002-ad46-7ad5f2009a05" ismacro="false" /><field column="ControlDisplayName" fieldcaption="ControlDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd811489-c34a-412e-868c-a5c07bf43734" ismacro="false" /><field column="ControlName" fieldcaption="ControlName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="564e6a7f-b643-4061-b218-b1c43633b7c0" ismacro="false" /><field column="ControlParameterName" fieldcaption="ControlParameterName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02e9c06f-9187-44a6-9bab-72f9b9210b77" ismacro="false" /><field column="ControlDescription" fieldcaption="ControlDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b6697c07-7d62-4961-a809-d410953ab0f9" ismacro="false" /><field column="ControlGUID" fieldcaption="ControlGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f3a30a4-7d73-411e-a758-a1a72a8ad018" ismacro="false" /><field column="ControlLastModified" fieldcaption="ControlLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db669571-0da4-4607-8e9e-9d2c7787e88b" ismacro="false" /><field column="ControlFileName" fieldcaption="ControlFileName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f0369391-048d-4717-8020-fc15e8fce2b3" ismacro="false" /><field column="ControlProperties" fieldcaption="Control properties" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ead75d5b-0189-4836-8daa-6a6ea9dbf7d3" ismacro="false" /><field column="ControlResourceID" fieldcaption="Control module ID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="afb6f63d-5951-432a-88c3-75c07499da1a" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_InlineControl', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:49:37', 'e2be9ac2-a390-4619-99ae-b3ff811d11d1', 0, 1, 0, N'', 1, N'ControlDisplayName', N'ControlDescription', N'', N'ControlLastModified', N'<search><item searchable="True" name="ControlID" tokenized="False" content="False" id="13542dec-1023-41e5-b176-925abf511962" /><item searchable="False" name="ControlDisplayName" tokenized="True" content="True" id="5e3848ec-5f4c-41bb-a439-ec7205020fc8" /><item searchable="False" name="ControlName" tokenized="True" content="True" id="a3a02099-4d86-4481-8b04-40f216235cc6" /><item searchable="False" name="ControlParameterName" tokenized="True" content="True" id="a49517fc-b949-4744-ae66-8ebe47ed2cda" /><item searchable="False" name="ControlDescription" tokenized="True" content="True" id="3edaa259-5b66-4828-8eae-f0621087d2c3" /><item searchable="False" name="ControlGUID" tokenized="False" content="False" id="a6fdf108-8a47-4fce-a628-42616ddb9bb7" /><item searchable="True" name="ControlLastModified" tokenized="False" content="False" id="58f09b1c-a773-4d16-a31e-2a1b93b948b2" /><item searchable="False" name="ControlFileName" tokenized="True" content="True" id="2b2f5d28-a1a7-4dc9-9135-0fd438db0369" /><item searchable="False" name="ControlProperties" tokenized="True" content="True" id="d077b281-5592-4424-b5d1-53b26a6d1af7" /><item searchable="True" name="ControlResourceID" tokenized="False" content="False" id="ced03442-b530-4c49-8449-ca9e42e2138e" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (880, N'Form', N'cms.form', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Form">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FormID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="FormDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormSendToEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormSendFromEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormEmailSubject" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormEmailTemplate" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormEmailAttachUploadedDocs" type="xs:boolean" minOccurs="0" />
              <xs:element name="FormClassID" type="xs:int" />
              <xs:element name="FormItems" type="xs:int" />
              <xs:element name="FormReportFields" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormRedirectToUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormDisplayText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormClearAfterSave" type="xs:boolean" />
              <xs:element name="FormSubmitButtonText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormSiteID" type="xs:int" />
              <xs:element name="FormConfirmationEmailField" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormConfirmationTemplate" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormConfirmationSendFromEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormConfirmationEmailSubject" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormAccess" type="xs:int" minOccurs="0" />
              <xs:element name="FormSubmitButtonImage" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="255" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FormGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FormLastModified" type="xs:dateTime" />
              <xs:element name="FormLogActivity" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Form" />
      <xs:field xpath="FormID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FormID" fieldcaption="FormID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="7e4045d0-83fe-48e2-8dff-17ba63371c98" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FormDisplayName" fieldcaption="FormDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a2f80fe3-4db8-4108-a1ba-72d87b300acd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormName" fieldcaption="FormName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9923899a-7913-4138-bc54-00b2d9d75826" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormSendToEmail" fieldcaption="FormSendToEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e39d82b9-a055-4816-9a7d-ae091ad1b533" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormSendFromEmail" fieldcaption="FormSendFromEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4233f725-e939-4a30-9f80-6f662b35c87a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormEmailSubject" fieldcaption="FormEmailSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d5a09ea2-cd3f-43cd-9e16-34aae72e4693" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormEmailTemplate" fieldcaption="FormEmailTemplate" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="70361f86-4cfa-4728-aa67-c130eb29cc53" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="FormEmailAttachUploadedDocs" fieldcaption="FormEmailAttachUploadedDocs" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ee271037-5c60-4196-8220-0bf7fd4f0b24" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="FormClassID" fieldcaption="FormClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4a6f6a49-d057-4c80-9b20-2b8e760c246f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormItems" fieldcaption="FormItems" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72c682ed-264a-4bc2-9ce2-9aab8203dd61" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormReportFields" fieldcaption="FormReportFields" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f20ee9b5-041b-44fe-a2f9-c9723a959201" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="FormRedirectToUrl" fieldcaption="FormRedirectToUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="15988158-84b2-4f52-8474-3b512d8c4e43" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormDisplayText" fieldcaption="FormDisplayText" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="97864ebf-e58c-4ccd-b1f7-c49801379509" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="FormClearAfterSave" fieldcaption="FormClearAfterSave" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="414bfd02-1c2e-4a6c-80df-70158de74370" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="FormSubmitButtonText" fieldcaption="FormSubmitButtonText" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db2765b7-a1b6-464d-924b-7b7f7231d4a3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormSiteID" fieldcaption="FormSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a695a92d-3d29-4aaf-b326-20db0876d681" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormConfirmationEmailField" fieldcaption="FormConfirmationEmailField" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6b822565-28bf-4881-b75a-86565bd3f6a4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormConfirmationTemplate" fieldcaption="FormConfirmationTemplate" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b305f971-5cde-4f05-bc7e-f5f122432c89" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="FormConfirmationSendFromEmail" fieldcaption="FormConfirmationSendFromEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="38dfb147-f65e-4e82-b098-4b3cc09e7c7a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormConfirmationEmailSubject" fieldcaption="FormConfirmationEmailSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="47e8f8d3-c8ba-4060-9d3b-ac44883febd8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormAccess" fieldcaption="FormAccess" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6655864e-d1c5-44e7-bdb9-d115fd50309b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormSubmitButtonImage" fieldcaption="FormSubmitButtonImage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="839193cc-6353-4cd8-9b70-2315dce1f7de" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FormGUID" fieldcaption="FormGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9e7c52e0-e181-4e28-a140-2c49aaf5640a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="FormLastModified" fieldcaption="FormLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f50a1f91-63ea-4a7c-a69a-8fb4574a93ba" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="FormLogActivity" fieldcaption="Log on-line marketing activity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6a4f0e2f-e7e7-49f1-b130-8da263c531cd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Form', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110628 12:30:53', 'be2a91e3-b191-4a74-8888-a212b9a88c6d', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (905, N'Newsletter - Email template', N'newsletter.emailtemplate', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_EmailTemplate">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TemplateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TemplateDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateBody">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateSiteID" type="xs:int" />
              <xs:element name="TemplateHeader">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateFooter">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateStylesheetText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TemplateLastModified" type="xs:dateTime" />
              <xs:element name="TemplateSubject" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_EmailTemplate" />
      <xs:field xpath="TemplateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TemplateID" fieldcaption="TemplateID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="1fc8d3e4-5b6f-4a8b-9266-42cfdd739d2d" /><field column="TemplateDisplayName" fieldcaption="TemplateDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d3505546-d312-4dda-aacc-cf1dfcb7bab3" /><field column="TemplateName" fieldcaption="TemplateName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d138a78c-5095-4778-a5a1-6c0e9d9c226e" /><field column="TemplateBody" fieldcaption="TemplateBody" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b06319a3-ed5a-4e28-b0d0-76e8db86ea6c" /><field column="TemplateSiteID" fieldcaption="TemplateSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e8548e32-9ba1-4093-b00e-b6ab09d374e2" /><field column="TemplateHeader" fieldcaption="TemplateHeader" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d5753a72-1424-4110-8557-1b54f68169c1" /><field column="TemplateFooter" fieldcaption="TemplateFooter" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e8476b60-2e2f-48b6-87f6-5b6fe3ef426e" /><field column="TemplateType" fieldcaption="TemplateType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="721c9b4a-60e2-4e9e-beef-8ca0e5612a89" /><field column="TemplateStylesheetText" fieldcaption="TemplateStylesheetText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b9c74440-beca-4189-99a8-9bb431b3c20a" /><field column="TemplateGUID" fieldcaption="TemplateGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4bf16188-eaa3-4b2a-bc44-897706226bae" /><field column="TemplateLastModified" fieldcaption="TemplateLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="951b6ddb-4c52-4d94-a7dd-601f67b3658c" /><field column="TemplateSubject" fieldcaption="TemplateSubject" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="0eaddd8f-5a59-4873-afb1-80a7156918a6" /></form>', N'', N'', N'', N'Newsletter_EmailTemplate', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:58:17', '5c6c85d4-63bc-4ca6-8b93-160d574716e4', 0, 1, 0, N'', 2, N'TemplateDisplayName', N'TemplateBody', N'', N'TemplateLastModified', N'<search><item searchable="True" name="TemplateID" tokenized="False" content="False" id="f3278be6-944b-47b3-b919-09f3a8afcefa" /><item searchable="False" name="TemplateDisplayName" tokenized="True" content="True" id="3ea61a69-b0ad-48ef-866a-b30acea0458c" /><item searchable="False" name="TemplateName" tokenized="True" content="True" id="e11becb2-b767-4e9c-9a2c-c39c65eca5be" /><item searchable="False" name="TemplateBody" tokenized="True" content="True" id="460b40dd-9584-4567-b36c-59dfc1f82a4a" /><item searchable="True" name="TemplateSiteID" tokenized="False" content="False" id="272e12f1-f294-4d4f-a3b2-a237d5c54c71" /><item searchable="False" name="TemplateHeader" tokenized="True" content="True" id="d4d9f7e1-5aab-4943-b5a7-fc22c805c4ad" /><item searchable="False" name="TemplateFooter" tokenized="True" content="True" id="07ee285a-5283-41f9-a97d-827d837569c1" /><item searchable="False" name="TemplateType" tokenized="True" content="True" id="1f058dc2-5fc7-4681-b8de-64c260ef85a5" /><item searchable="False" name="TemplateStylesheetText" tokenized="True" content="True" id="8d6e32f6-1d01-454e-a19b-38aac4be2ade" /><item searchable="False" name="TemplateGUID" tokenized="False" content="False" id="8658df22-5ee9-4704-9840-a3a0fd6b0a7a" /><item searchable="True" name="TemplateLastModified" tokenized="False" content="False" id="6ad72bed-b56f-436d-bc63-91e351ed8b7f" /><item searchable="False" name="TemplateSubject" tokenized="True" content="True" id="cde0bcb7-eb12-40f1-8d41-8762b6d8bf5c" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (907, N'Newsletter - Issue', N'newsletter.issue', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_NewsletterIssue">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="IssueID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="IssueSubject">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IssueText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IssueUnsubscribed" type="xs:int" />
              <xs:element name="IssueNewsletterID" type="xs:int" />
              <xs:element name="IssueTemplateID" type="xs:int" minOccurs="0" />
              <xs:element name="IssueSentEmails" type="xs:int" />
              <xs:element name="IssueMailoutTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="IssueShowInNewsletterArchive" type="xs:boolean" minOccurs="0" />
              <xs:element name="IssueGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="IssueLastModified" type="xs:dateTime" />
              <xs:element name="IssueSiteID" type="xs:int" />
              <xs:element name="IssueOpenedEmails" type="xs:int" minOccurs="0" />
              <xs:element name="IssueBounces" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_NewsletterIssue" />
      <xs:field xpath="IssueID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="IssueID" fieldcaption="IssueID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="15b88ba7-8d58-48c4-9292-ea5f7784fb04" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IssueSubject" fieldcaption="IssueSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="00223c6b-e89b-4dcc-b489-5bbc48cd4a24" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IssueText" fieldcaption="IssueText" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="afc050d5-1b24-411f-a3dc-24453f493022" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="IssueUnsubscribed" fieldcaption="IssueUnsubscribed" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f9a9455-3975-4ced-9400-13b2f8b0ef44" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IssueNewsletterID" fieldcaption="IssueNewsletterID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="54d28cf3-3e2e-4b3b-a3f6-419cb217db55" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IssueTemplateID" fieldcaption="IssueTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f3101643-6599-4374-a7a6-c666834af21d" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IssueSentEmails" fieldcaption="IssueSentEmails" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4e86e99f-0715-4f0e-b6eb-d69f2f84363e" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IssueMailoutTime" fieldcaption="IssueMailoutTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c176fdf-07c7-4dff-b21c-dfe85684160c" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="IssueShowInNewsletterArchive" fieldcaption="IssueShowInNewsletterArchive" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="39607d2e-7a55-4331-a9e8-68b83d320a44" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="IssueGUID" fieldcaption="IssueGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea71f981-a9e2-4019-a97d-e65dac96c9a1" ismacro="false" hasdependingfields="false"><settings><controlname>unknown</controlname></settings></field><field column="IssueLastModified" fieldcaption="IssueLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cfdcc588-60b7-4909-9f2e-46f62e7d7615" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="IssueSiteID" fieldcaption="IssueSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c3fd7ac6-621d-4e3d-b79d-4f493b053c21" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IssueOpenedEmails" fieldcaption="IssueOpenedEmails" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cf8feffc-13e9-47e8-9c14-b07484e74b5d" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IssueBounces" fieldcaption="IssueBounces" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="bdd9e7ab-1038-40f8-b72d-50d6c8e96be2" visibility="none" ismacro="false" hasdependingfields="false"><settings><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Newsletter_NewsletterIssue', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110301 10:12:27', '41f64b98-ae6d-4ba4-867a-0079ef839a1a', 0, 1, 0, N'', 2, N'IssueSubject', N'IssueText', N'', N'IssueLastModified', N'<search><item searchable="True" name="IssueID" tokenized="False" content="False" id="6aa3baf2-1a77-4e01-ab02-8e787c7fc748" /><item searchable="False" name="IssueSubject" tokenized="True" content="True" id="659771fd-4191-4547-9b09-95283884d14d" /><item searchable="False" name="IssueText" tokenized="True" content="True" id="eeb7da76-cfbe-458f-85bc-505688173678" /><item searchable="True" name="IssueUnsubscribed" tokenized="False" content="False" id="ef262da9-20f7-4b67-9d1f-f9ee7a53875d" /><item searchable="True" name="IssueNewsletterID" tokenized="False" content="False" id="f85d0360-a2e2-4f3c-a527-4c2bf724827d" /><item searchable="True" name="IssueTemplateID" tokenized="False" content="False" id="8d2fbe28-d331-48f7-9ea2-a4ac1075b9ac" /><item searchable="True" name="IssueSentEmails" tokenized="False" content="False" id="ef5cc431-7dea-4a30-af75-dc2f126ea344" /><item searchable="True" name="IssueMailoutTime" tokenized="False" content="False" id="dcab45aa-ee4c-4465-b6a9-4873b9b1250d" /><item searchable="True" name="IssueShowInNewsletterArchive" tokenized="False" content="False" id="a89a11a2-178f-45de-a8ec-298c6533ac1a" /><item searchable="False" name="IssueGUID" tokenized="False" content="False" id="568cfd91-bbf4-4ec5-ae80-1793c1c73b4b" /><item searchable="True" name="IssueLastModified" tokenized="False" content="False" id="b3c4c359-4f5c-44d3-9da6-a0d21856a628" /><item searchable="True" name="IssueSiteID" tokenized="False" content="False" id="882be111-d8fe-41c9-abdd-73e94c75b867" /><item searchable="True" name="IssueOpenedEmails" tokenized="False" content="False" id="c7d9ca7f-b4a8-407c-958d-254b30d0cb5c" /><item searchable="True" name="IssueBounces" tokenized="False" content="False" id="d375a286-b2bf-48a6-944d-4950531d18d5" /></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (908, N'Newsletter - Subscriber', N'newsletter.subscriber', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_Subscriber">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriberID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SubscriberEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriberFirstName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriberLastName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriberSiteID" type="xs:int" />
              <xs:element name="SubscriberGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SubscriberCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriberType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriberRelatedID" type="xs:int" minOccurs="0" />
              <xs:element name="SubscriberLastModified" type="xs:dateTime" />
              <xs:element name="SubscriberFullName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriberBounces" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_Subscriber" />
      <xs:field xpath="SubscriberID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriberID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="83c8689b-22e0-4b81-a1f0-ae6ff8808fba" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SubscriberEmail" fieldcaption="{$General.Email$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="400" regularexpression="^[a-zA-Z0-9_\-\+]+(\.[a-zA-Z0-9_\-\+]+)*@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$" validationerrormessage="{$NewsletterSubscription.ErrorInvalidEmail$}" publicfield="false" spellcheck="true" guid="c13eb8fa-f65d-4052-8f91-92fc00c53679" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SubscriberFirstName" fieldcaption="{$SubscribeForm.FirstName$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="e02a73e0-3722-47ba-be59-aae58cae2c49" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SubscriberLastName" fieldcaption="{$SubscribeForm.LastName$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="d3020a22-0412-443c-8925-0d0936813c55" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SubscriberSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2bfe7ce0-e15c-4cef-bc53-c0cecabbcf52" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SubscriberGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4e52e79d-6de4-4fda-86cd-e22150b496c0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SubscriberRelatedID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6a33bca3-a330-4bd4-8e35-bffa9a14d941" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SubscriberLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0b576d02-188a-4e93-8900-7197bf863eb5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SubscriberType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="6d688ffb-acac-4b92-89f8-35689d3da781" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SubscriberBounces" fieldcaption="SubscriberBounces" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="915657d3-4337-45ce-ba34-2ba0442f0a1b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field></form>', N'', N'', N'', N'Newsletter_Subscriber', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110816 10:35:35', '01c9a7a3-beb3-48ef-a2b2-2cd56a0f074f', 0, 1, 0, N'', 2, N'SubscriberEmail', N'0', N'', N'SubscriberLastModified', N'<search><item searchable="True" name="SubscriberID" tokenized="False" content="False" id="3123062f-7551-4c73-89e1-00f7312ce71c" /><item searchable="False" name="SubscriberEmail" tokenized="True" content="True" id="6d43d810-568c-49da-a03d-37cfd673f550" /><item searchable="False" name="SubscriberFirstName" tokenized="True" content="True" id="b6139f0d-4386-4718-83c6-2a3b9d8d9db3" /><item searchable="False" name="SubscriberLastName" tokenized="True" content="True" id="cd021fcf-0506-494c-b5a2-b88e49527120" /><item searchable="True" name="SubscriberSiteID" tokenized="False" content="False" id="c37df813-5eac-4fdb-a495-6bfac601cd4f" /><item searchable="False" name="SubscriberGUID" tokenized="False" content="False" id="a02ca0df-8c53-4946-99c7-fda955a05c9a" /><item searchable="True" name="SubscriberRelatedID" tokenized="False" content="False" id="4f88e23e-5d40-4139-916a-8d3f1e20a76d" /><item searchable="True" name="SubscriberLastModified" tokenized="False" content="False" id="b878f668-9590-492e-99ed-352e3cae4146" /><item searchable="False" name="SubscriberType" tokenized="True" content="True" id="a134b19f-dc92-40f6-a160-c9f031ad42ee" /><item searchable="True" name="SubscriberBounces" tokenized="False" content="False" id="ec5f815a-c130-4168-bce9-0bb8cc1d0765" /></search>', NULL, 1, N'', NULL, N'<form><field column="ContactEmail" mappedtofield="SubscriberEmail" /><field column="ContactFirstName" mappedtofield="SubscriberFirstName" /><field column="ContactLastName" mappedtofield="SubscriberLastName" /></form>', 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (909, N'Scheduled task', N'cms.ScheduledTask', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ScheduledTask">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaskID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaskName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskAssemblyName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskClass" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskInterval">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskData">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskLastRunTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="TaskNextRunTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="TaskProgress" type="xs:int" minOccurs="0" />
              <xs:element name="TaskLastResult" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskEnabled" type="xs:boolean" />
              <xs:element name="TaskSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="TaskDeleteAfterLastRun" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaskServerName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TaskLastModified" type="xs:dateTime" />
              <xs:element name="TaskExecutions" type="xs:int" minOccurs="0" />
              <xs:element name="TaskResourceID" type="xs:int" minOccurs="0" />
              <xs:element name="TaskRunInSeparateThread" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaskUseExternalService" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaskAllowExternalService" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ScheduledTask" />
      <xs:field xpath="TaskID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaskID" fieldcaption="TaskID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4419f729-75ac-4eb7-a4d4-bdcdb8d03884" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskName" fieldcaption="TaskName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e9eabd43-9133-4cd9-8012-aefa513e87b2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskDisplayName" fieldcaption="TaskDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a3f95b42-bd02-4db6-b1aa-0d340952a27e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskAssemblyName" fieldcaption="TaskAssemblyName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c567e00-bf7a-42e8-878b-a5befde1faed" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskClass" fieldcaption="TaskClass" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c7a260a0-d909-4e9e-b1d3-bcbda7eb9076" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskInterval" fieldcaption="TaskInterval" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02900d9c-c032-4081-9532-a48c339dbbce" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskData" fieldcaption="TaskData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a8fd0828-3456-483d-9845-f93c6d9fc9fd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="TaskLastRunTime" fieldcaption="TaskLastRunTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c1c5fbe3-cf9b-4055-b663-18898c5cae18" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="TaskNextRunTime" fieldcaption="TaskNextRunTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72b48b10-53ca-4934-8be4-c3044af2be7d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="TaskProgress" fieldcaption="TaskProgress" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b541b89c-ea55-4085-9a0d-9c9c33cf64e5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskLastResult" fieldcaption="TaskLastResult" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="feb262dd-3a25-4301-8b31-6d068f0a75a1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="TaskEnabled" fieldcaption="TaskEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dbd5d77f-f7f4-4da4-87f8-9027cbcca5e1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskSiteID" fieldcaption="TaskSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e4bc2c60-9893-4c41-a3c9-45395365d776" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskDeleteAfterLastRun" fieldcaption="TaskDeleteAfterLastRun" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="564b2be3-fa3f-4517-8cf3-f36bff312e6b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskServerName" fieldcaption="TaskServerName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a86cf79a-156b-4075-a378-58875eb715ef" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskGUID" fieldcaption="TaskGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6db955f1-124a-4a2b-ad66-596dd53936ea" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="TaskLastModified" fieldcaption="TaskLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a02bac7-ee29-497a-a42d-22f1201e1b1c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="TaskExecutions" fieldcaption="TaskExecutions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5f1cbc53-2a56-415b-b194-fdf91a48a8ad" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskResourceID" fieldcaption="Task module ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6f837b40-1492-4558-b16f-85622aa7d1ce" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskRunInSeparateThread" fieldcaption="Run in separate thread" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="false" guid="56747e3e-3b17-459a-a828-353093ae98fb" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskAllowExternalService" fieldcaption="Allow external service" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" fielddescription="Indicates whether the task can be processed by an external service." publicfield="false" spellcheck="true" guid="9b235d42-586a-4509-a81d-258d40d62690" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskUseExternalService" fieldcaption="Use external service" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" fielddescription="Indicates whether the task is processed by an external service." publicfield="false" spellcheck="true" guid="464d4d85-2746-46f8-aa67-f8d936c0d25c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_ScheduledTask', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110801 18:03:09', '57b78754-4137-4220-b736-8c16cbbe6c87', 0, 1, 0, N'', 1, N'TaskDisplayName', N'TaskLastResult', N'', N'TaskLastModified', N'<search><item searchable="True" name="TaskID" tokenized="False" content="False" id="a0dd8f64-9aec-48c2-88db-5b1280e5df16" /><item searchable="False" name="TaskName" tokenized="True" content="True" id="c9fe6ee6-fb5d-4777-a51a-e6b2aa04fbca" /><item searchable="False" name="TaskDisplayName" tokenized="True" content="True" id="62617973-7fb4-4ab2-bddc-a6278bf9ec17" /><item searchable="False" name="TaskAssemblyName" tokenized="True" content="True" id="2a03db10-cf81-4a98-beed-09cfa7326f4a" /><item searchable="False" name="TaskClass" tokenized="True" content="True" id="b5add5c9-e188-4957-b02b-30f4cf526ad1" /><item searchable="False" name="TaskInterval" tokenized="True" content="True" id="c13b1a3b-d832-4f00-b2e0-47ce7b6e907f" /><item searchable="False" name="TaskData" tokenized="True" content="True" id="b5c34645-676b-468e-83bf-d388c7018c7d" /><item searchable="True" name="TaskLastRunTime" tokenized="False" content="False" id="76b69056-d0ef-4d75-9ff8-0c75f2750e31" /><item searchable="True" name="TaskNextRunTime" tokenized="False" content="False" id="2e454b83-3c7b-40f2-b45a-a2b25c1cc668" /><item searchable="True" name="TaskProgress" tokenized="False" content="False" id="24285fde-73b8-4dfd-9994-f1afb3abc4bd" /><item searchable="False" name="TaskLastResult" tokenized="True" content="True" id="d744b40d-ec9c-469b-b7e7-283155fa51a1" /><item searchable="True" name="TaskEnabled" tokenized="False" content="False" id="109e16b6-9526-4fc6-9ae4-583f31a17274" /><item searchable="True" name="TaskSiteID" tokenized="False" content="False" id="d856e820-b3dd-432c-94cd-47ff810670c5" /><item searchable="True" name="TaskDeleteAfterLastRun" tokenized="False" content="False" id="9a015d0a-6757-4929-97b9-e570caa80bf3" /><item searchable="False" name="TaskServerName" tokenized="True" content="True" id="5fc63baa-40a0-4002-9b7b-7904a46c2b45" /><item searchable="False" name="TaskGUID" tokenized="False" content="False" id="a6468b94-cd2f-4046-b5a0-36a9d69ea768" /><item searchable="True" name="TaskLastModified" tokenized="False" content="False" id="1946ce8b-0b08-462f-a862-4d199ea83c56" /><item searchable="True" name="TaskExecutions" tokenized="False" content="False" id="66084509-9776-4df6-8bf7-97eb148454d1" /><item searchable="True" name="TaskResourceID" tokenized="False" content="False" id="3c91f106-984c-40b9-bb4d-d475753b3035" /><item searchable="True" name="TaskRunInSeparateThread" tokenized="False" content="False" id="523a9bba-eade-4eaa-806c-68a8a2b73eae" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (912, N'Newsletter - SubscriberNewsletter', N'Newsletter.SubscriberNewsletter', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_SubscriberNewsletter">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriberID" type="xs:int" />
              <xs:element name="NewsletterID" type="xs:int" />
              <xs:element name="SubscribedWhen" type="xs:dateTime" />
              <xs:element name="SubscriptionApproved" type="xs:boolean" minOccurs="0" />
              <xs:element name="SubscriptionApprovedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="SubscriptionApprovalHash" minOccurs="0">
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
      <xs:selector xpath=".//Newsletter_SubscriberNewsletter" />
      <xs:field xpath="SubscriberID" />
      <xs:field xpath="NewsletterID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriberID" fieldcaption="SubscriberID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="812e73dc-fd64-4e4d-8edc-b83ee7991a33" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="NewsletterID" fieldcaption="NewsletterID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b19b8699-2c2f-4001-b802-04cdaa87e01e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SubscribedWhen" fieldcaption="SubscribedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="882d41c1-10a3-4c89-9b11-19cb98f88807" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SubscriptionApproved" fieldcaption="SubscriptionApproved" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="23308913-8f3f-4d45-b5f9-63e5aa6bab32" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SubscriptionApprovedWhen" fieldcaption="SubscriptionApprovedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6e124236-cdb0-4b0f-8a06-3c1d1cb162cd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><controlname>calendarcontrol</controlname><editTime>true</editTime></settings></field><field column="SubscriptionApprovalHash" fieldcaption="SubscriptionApprovalHash" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="77673da4-a9e1-4776-b77c-8cf72b8c8132" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Newsletter_SubscriberNewsletter', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110803 17:26:47', 'e73eba16-4a8d-445d-a1f1-ac3d610b855e', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (913, N'Newsletter - Newsletter', N'newsletter.newsletter', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_Newsletter">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="NewsletterID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="NewsletterDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="5" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterSubscriptionTemplateID" type="xs:int" />
              <xs:element name="NewsletterUnsubscriptionTemplateID" type="xs:int" />
              <xs:element name="NewsletterSenderName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterSenderEmail">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterDynamicSubject" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterDynamicURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterDynamicScheduledTaskID" type="xs:int" minOccurs="0" />
              <xs:element name="NewsletterTemplateID" type="xs:int" minOccurs="0" />
              <xs:element name="NewsletterSiteID" type="xs:int" />
              <xs:element name="NewsletterGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="NewsletterUnsubscribeUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterBaseUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterLastModified" type="xs:dateTime" />
              <xs:element name="NewsletterUseEmailQueue" type="xs:boolean" minOccurs="0" />
              <xs:element name="NewsletterEnableOptIn" type="xs:boolean" minOccurs="0" />
              <xs:element name="NewsletterOptInTemplateID" type="xs:int" minOccurs="0" />
              <xs:element name="NewsletterSendOptInConfirmation" type="xs:boolean" minOccurs="0" />
              <xs:element name="NewsletterOptInApprovalURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterTrackOpenEmails" type="xs:boolean" minOccurs="0" />
              <xs:element name="NewsletterTrackClickedLinks" type="xs:boolean" minOccurs="0" />
              <xs:element name="NewsletterDraftEmails" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="NewsletterLogActivity" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_Newsletter" />
      <xs:field xpath="NewsletterID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="NewsletterID" fieldcaption="NewsletterID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f015e086-c0c6-4c72-bbaa-7a7461f4e5ff" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="NewsletterDisplayName" fieldcaption="NewsletterDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7af312ef-f89f-4480-bafd-22f924b7517f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterName" fieldcaption="NewsletterName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ab201dec-dd17-45e1-8057-306ed113b8ed" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterType" fieldcaption="NewsletterType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5d898e33-0f0d-462e-b051-91228c9b4729" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterSubscriptionTemplateID" fieldcaption="NewsletterSubscriptionTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="902c9fb3-08fd-453d-9cad-7f2b1ef40201" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterUnsubscriptionTemplateID" fieldcaption="NewsletterUnsubscriptionTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0fceb734-921a-4434-bede-0aed0ed35823" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterSenderName" fieldcaption="NewsletterSenderName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2eaf8900-a508-4c38-a013-c8d243d2f96c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterSenderEmail" fieldcaption="NewsletterSenderEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8499a3ed-40b3-4ea7-bbf7-4ac62e2a835c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterDynamicSubject" fieldcaption="NewsletterDynamicSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b489b91a-31a1-4c1a-9208-255f78324403" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterDynamicURL" fieldcaption="NewsletterDynamicURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="58bc4a29-4c40-4b2d-acba-197115902e38" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterDynamicScheduledTaskID" fieldcaption="NewsletterDynamicScheduledTaskID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="21c10b0f-e449-4c7b-b5ad-7f9fba1c9705" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterTemplateID" fieldcaption="NewsletterTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="95bd18af-6e22-4c94-baaa-7f20254b60c2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterSiteID" fieldcaption="NewsletterSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9048b776-a74a-4551-b7c5-cee34128b9ee" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterGUID" fieldcaption="NewsletterGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5c08f195-4b00-46b3-a733-a629c8206380" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="NewsletterUnsubscribeUrl" fieldcaption="NewsletterUnsubscribeUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4b99c0d8-704a-4d1f-ba23-9924ffdb0f3e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterBaseUrl" fieldcaption="NewsletterBaseUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e08adfd-1256-4946-abb1-7bc104bf95c3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterLastModified" fieldcaption="NewsletterLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2dd1c825-0c1c-4ea8-84d0-64a3bff43f32" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="NewsletterUseEmailQueue" fieldcaption="NewsletterUseEmailQueue" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1dfa055c-6214-44b1-ac7d-92ed36d0eb68" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="NewsletterEnableOptIn" fieldcaption="NewsletterEnableOptIn" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c7d18589-6ba9-48d1-976f-ab3d8fd2422d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="NewsletterOptInTemplateID" fieldcaption="NewsletterOptInTemplateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="54459197-3e55-4346-a286-d6c65757f3a6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterSendOptInConfirmation" fieldcaption="NewsletterSendOptInConfirmation" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="23c053fb-9c65-415f-8fc4-1972b54072f4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="NewsletterOptInApprovalURL" fieldcaption="NewsletterOptInApprovalURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="6b65b50e-2e44-467a-9c69-d0a13e376a9e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="NewsletterTrackOpenEmails" fieldcaption="NewsletterTrackOpenEmails" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b257aa96-f2d7-4e9f-80dd-5d8d60b1ba09" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="NewsletterTrackClickedLinks" fieldcaption="NewsletterTrackClickedLinks" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" fielddescription="Indicates if link tracking is enabled" publicfield="false" spellcheck="false" guid="d1ec21b6-7668-4b4d-855d-3b439dcdc173" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="NewsletterDraftEmails" fieldcaption="NewsletterDraftEmails" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" fielddescription="Contains testing e-mails" publicfield="false" spellcheck="false" guid="439d9668-e22c-4e40-baf8-f1cda5530254" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="NewsletterLogActivity" fieldcaption="NewsletterLogActivity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" fielddescription="Indicates if on-line marketing activities are logged" publicfield="false" spellcheck="true" guid="988fb2d4-b2f9-4b0b-a03d-7a09886d5f0b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Newsletter_Newsletter', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110603 15:41:52', '22ecf2f7-865a-4a1e-bbaa-5da680489b39', 0, 1, 0, N'', 2, N'NewsletterDisplayName', N'0', N'', N'NewsletterLastModified', N'<search><item searchable="True" name="NewsletterID" tokenized="False" content="False" id="f40084cf-cb0a-4990-bfd0-6ea05f944c53" /><item searchable="False" name="NewsletterDisplayName" tokenized="True" content="True" id="65c8b3bb-b6be-441b-a264-7ad39149f8aa" /><item searchable="False" name="NewsletterName" tokenized="True" content="True" id="473003c0-fe02-4c64-b208-7e940c5fa32e" /><item searchable="False" name="NewsletterType" tokenized="True" content="True" id="8b43555c-7a6c-4a62-b71f-d5784d9cafe3" /><item searchable="True" name="NewsletterSubscriptionTemplateID" tokenized="False" content="False" id="05bb59cf-ced3-4f88-959a-3182a015048f" /><item searchable="True" name="NewsletterUnsubscriptionTemplateID" tokenized="False" content="False" id="01ce53d8-d0cd-4258-b536-86447f008b91" /><item searchable="False" name="NewsletterSenderName" tokenized="True" content="True" id="d2785e76-4b09-4bc3-a662-ac4f2e4998f0" /><item searchable="False" name="NewsletterSenderEmail" tokenized="True" content="True" id="41e477e4-0d62-44c6-884c-57f029d065dd" /><item searchable="False" name="NewsletterDynamicSubject" tokenized="True" content="True" id="0808f807-d088-4904-9855-0cdd2b07a997" /><item searchable="False" name="NewsletterDynamicURL" tokenized="True" content="True" id="acdee0aa-769c-43c4-9517-895328e4a1ec" /><item searchable="True" name="NewsletterDynamicScheduledTaskID" tokenized="False" content="False" id="9cf2fe80-52ea-4896-90a2-cae10d7e5336" /><item searchable="True" name="NewsletterTemplateID" tokenized="False" content="False" id="cc6e6c11-d324-4f14-8ca7-80d22fe874ec" /><item searchable="True" name="NewsletterSiteID" tokenized="False" content="False" id="ce878e5e-6697-43c9-87c4-648c83b28d3c" /><item searchable="False" name="NewsletterGUID" tokenized="False" content="False" id="b4a60c8d-b8bb-47fb-92b0-141aa8d4ceb6" /><item searchable="False" name="NewsletterUnsubscribeUrl" tokenized="True" content="True" id="65cd328d-9459-4ede-8e7e-cda5cf5401a5" /><item searchable="False" name="NewsletterBaseUrl" tokenized="True" content="True" id="859eb990-8bb8-48e6-a657-10e96bae4db4" /><item searchable="True" name="NewsletterLastModified" tokenized="False" content="False" id="abca14c9-c8c5-487a-b74b-648802694258" /><item searchable="True" name="NewsletterUseEmailQueue" tokenized="False" content="False" id="d9be6022-6f62-47ff-abdc-32587dc90117" /><item searchable="True" name="NewsletterEnableOptIn" tokenized="False" content="False" id="ca81ef34-addb-4de2-9033-4cbac9e724b2" /><item searchable="True" name="NewsletterOptInTemplateID" tokenized="False" content="False" id="277c5283-a19a-44d6-9005-d7a306ccc8af" /><item searchable="True" name="NewsletterSendOptInConfirmation" tokenized="False" content="False" id="82c1d248-105b-42f1-af50-ae2b7d3139c8" /><item searchable="False" name="NewsletterOptInApprovalURL" tokenized="True" content="True" id="4adfce04-80b7-45ac-8b86-c3bed345e9a1" /><item searchable="True" name="NewsletterTrackOpenEmails" tokenized="False" content="False" id="033802a3-b420-4dbc-a634-9dc7f85d5ccc" /><item searchable="True" name="NewsletterTrackClickedLinks" tokenized="False" content="False" id="3a07d96e-e2fa-44fd-ab6a-9c96a073a9ed" /><item searchable="False" name="NewsletterDraftEmails" tokenized="True" content="True" id="8467ff20-9bf0-4cbd-9681-cbe25447a3fa" /></search>', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (919, N'Newsletter - Emails', N'newsletter.emails', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_Emails">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EmailID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="EmailNewsletterIssueID" type="xs:int" />
              <xs:element name="EmailSubscriberID" type="xs:int" />
              <xs:element name="EmailSiteID" type="xs:int" />
              <xs:element name="EmailLastSendResult" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailLastSendAttempt" type="xs:dateTime" minOccurs="0" />
              <xs:element name="EmailSending" type="xs:boolean" minOccurs="0" />
              <xs:element name="EmailGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="EmailLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_Emails" />
      <xs:field xpath="EmailID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EmailID" fieldcaption="EmailID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8a400d2b-7570-430f-b380-39bef865cbd4" /><field column="EmailNewsletterIssueID" fieldcaption="EmailNewsletterIssueID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="578a62a2-28aa-4e74-ae40-e3bbe1ff2445" /><field column="EmailSubscriberID" fieldcaption="EmailSubscriberID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="56aebfe8-f488-43e1-baf1-7b42e5935fb7" /><field column="EmailSiteID" fieldcaption="EmailSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c07c5d6c-4898-4e70-8c41-112da8032f06" /><field column="EmailLastSendResult" fieldcaption="EmailLastSendResult" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="27b30e71-7cb9-482c-b6b3-efdc45109bf3" /><field column="EmailLastSendAttempt" fieldcaption="EmailLastSendAttempt" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bfbe1ae6-2430-41ab-90ed-a648c0db3946" /><field column="EmailSending" fieldcaption="EmailSending" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6dcfdb16-3b71-4a22-94d6-27fb374a537f" /><field column="EmailGUID" fieldcaption="EmailGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a35bf542-8ab5-4e7a-a000-6e84be3b4265" /><field column="EmailLastModified" fieldcaption="EmailLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="30cefcfa-1ae9-45dc-ac26-5e57b4427de3" /></form>', N'', N'', N'', N'Newsletter_Emails', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:57:52', '551d4df2-d429-4a32-906b-c5821a04f7da', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (929, N'Web part container', N'cms.WebPartContainer', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebPartContainer">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContainerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ContainerDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContainerName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContainerTextBefore" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContainerTextAfter" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ContainerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ContainerLastModified" type="xs:dateTime" />
              <xs:element name="ContainerCSS" minOccurs="0">
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
      <xs:selector xpath=".//CMS_WebPartContainer" />
      <xs:field xpath="ContainerID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContainerID" fieldcaption="ContainerID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b2c2b404-fd6f-4fb1-bb93-4f6926ac3e37" ismacro="false" /><field column="ContainerDisplayName" fieldcaption="ContainerDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="790388db-652f-4b65-9740-65ec50bb1f35" ismacro="false" /><field column="ContainerName" fieldcaption="ContainerName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="62939e60-f1b3-4164-b587-94aa5b9a1c5f" ismacro="false" /><field column="ContainerTextBefore" fieldcaption="ContainerTextBefore" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f2c087fc-b7ea-4513-a14e-89b5769d2b09" ismacro="false" /><field column="ContainerTextAfter" fieldcaption="ContainerTextAfter" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b8c67f27-9765-4371-a48d-8281e220c530" ismacro="false" /><field column="ContainerCSS" fieldcaption="Container CSS styles" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6f206cc4-d767-4c84-959a-8dfa5181608e" visibility="none" ismacro="false" /><field column="ContainerGUID" fieldcaption="ContainerGUID" visible="true" columntype="file" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ba1c27b5-3f48-45d4-bdfc-62471238b7f7" ismacro="false" /><field column="ContainerLastModified" fieldcaption="ContainerLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c35ecd48-2d39-4daa-ba7e-5087dfee2c1f" ismacro="false" /></form>', N'', N'', N'', N'CMS_WebPartContainer', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:14:24', 'efe592b2-9b49-4997-8572-8419674cad0c', 0, 1, 0, N'', 1, N'ContainerDisplayName', N'0', N'', N'ContainerLastModified', N'<search><item searchable="True" name="ContainerID" tokenized="False" content="False" id="774b3e9b-ab15-44b9-b1c9-74919a6dce8a" /><item searchable="False" name="ContainerDisplayName" tokenized="True" content="True" id="835d1872-5e73-4b92-aa84-f5d4c57d2609" /><item searchable="False" name="ContainerName" tokenized="True" content="True" id="f8726091-5340-4167-8e71-87797b9bcd7c" /><item searchable="False" name="ContainerTextBefore" tokenized="True" content="True" id="7ac9b825-7506-40af-9579-a1f97017a1db" /><item searchable="False" name="ContainerTextAfter" tokenized="True" content="True" id="b0f394cf-5ce3-42a0-92ec-17ad2d72c8da" /><item searchable="False" name="ContainerCSS" tokenized="True" content="True" id="e4981ba1-68e6-48e6-8561-f50e1db4184e" /><item searchable="False" name="ContainerGUID" tokenized="False" content="False" id="2336e440-c38f-45b6-b0ad-487057b58348" /><item searchable="True" name="ContainerLastModified" tokenized="False" content="False" id="b76e558c-6e17-4503-8c39-d37cbff419cf" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (930, N'LicenseKey', N'cms.LicenseKey', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_LicenseKey">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="LicenseKeyID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="LicenseDomain">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LicenseKey">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LicenseEdition" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LicenseExpiration" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LicensePackages" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LicenseServers" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_LicenseKey" />
      <xs:field xpath="LicenseKeyID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="LicenseKeyID" fieldcaption="LicenseKeyID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d2e30d80-9086-4d4b-a508-842eefa2aa1a" /><field column="LicenseDomain" fieldcaption="LicenseDomain" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cc354cd7-2b35-4a58-9ab4-a7f4d4428881" /><field column="LicenseKey" fieldcaption="LicenseKey" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="13e758e9-06e7-4074-8805-13aa3acd6d02" /><field column="LicenseEdition" fieldcaption="LicenseEdition" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d7884a96-2d2f-4e1b-9e06-2063cf08bff0" /><field column="LicenseExpiration" fieldcaption="LicenseExpiration" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b50fdb22-682e-4f36-abe6-2962376c45a0" /><field column="LicensePackages" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="993a19f5-15b7-4b89-8844-0b5520a1c57c" visibility="none" ismacro="false" fieldcaption="LicensePackages" /><field column="LicenseServers" fieldcaption="LicenseServers" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="135c8059-2619-431b-8fd1-682b5cf5c332" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_LicenseKey', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110228 17:26:49', '029aa7db-a6e2-4996-b1c7-38f7bbd838ee', 0, 1, 0, N'', 0, N'LicenseDomain', N'0', N'', N'LicenseExpiration', N'<search><item searchable="True" name="LicenseKeyID" tokenized="False" content="False" id="dedd1dbd-7e60-428e-976f-c927eafa3997" /><item searchable="False" name="LicenseDomain" tokenized="True" content="True" id="ac26eaaa-af54-4ce3-880e-f803face2389" /><item searchable="False" name="LicenseKey" tokenized="True" content="True" id="856168ab-c9f6-4bd4-9c53-13ca84e3e9e1" /><item searchable="False" name="LicenseEdition" tokenized="True" content="True" id="8240bc6f-cb81-4a4e-97b0-fa04f186c8ed" /><item searchable="False" name="LicenseExpiration" tokenized="True" content="True" id="7b49ff56-29b4-403e-b7b8-495f4dadf634" /><item searchable="False" name="LicensePackages" tokenized="True" content="True" id="884dc00d-a4bd-4b15-bd2b-8c36599785be" /><item searchable="True" name="LicenseServers" tokenized="False" content="False" id="8859afc0-c8d6-4a76-9971-f277001de398" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1093, N'Web farm server', N'cms.WebFarmServer', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>  <xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">    <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">      <xs:complexType>        <xs:choice minOccurs="0" maxOccurs="unbounded">          <xs:element name="CMS_WebFarmServer">            <xs:complexType>              <xs:sequence>                <xs:element name="ServerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />                <xs:element name="ServerDisplayName">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ServerName">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ServerURL">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2000" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ServerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />                <xs:element name="ServerLastModified" type="xs:dateTime" />                <xs:element name="ServerEnabled" type="xs:boolean" />                <xs:element name="ServerLastUpdated" type="xs:dateTime" minOccurs="0" />              </xs:sequence>            </xs:complexType>          </xs:element>        </xs:choice>      </xs:complexType>      <xs:unique name="Constraint1" msdata:PrimaryKey="true">        <xs:selector xpath=".//CMS_WebFarmServer" />        <xs:field xpath="ServerID" />      </xs:unique>    </xs:element>  </xs:schema>', N'<form><field column="ServerID" fieldcaption="ServerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="aa45d15a-369b-4d52-b349-50a6dd88bf99" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ServerDisplayName" fieldcaption="ServerDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0faeba3a-902c-498b-86f1-adc10aecc480" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="true"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ServerName" fieldcaption="ServerName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b918771-1f4f-4b7e-8079-a9bc77580211" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ServerURL" fieldcaption="ServerURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="46b24382-3b88-46b8-b1f8-9a04ddbf09d3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ServerGUID" fieldcaption="ServerGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4a6c0fb1-566d-4a9f-b780-a6777aab8ac5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>unknown</controlname></settings></field><field column="ServerLastModified" fieldcaption="ServerLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="08311544-ceb1-4df6-b40a-c7c7eb4b8048" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ServerEnabled" fieldcaption="ServerEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a7318ab5-b31f-4c39-a7e5-6f3fe9dff9c1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ServerLastUpdated" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bb3c64d7-9e54-42b6-84ad-23d64182f136" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_WebFarmServer', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20120308 10:47:08', 'bb652b81-0792-4191-9d32-e7b8f2bd6e4f', 0, 1, 0, N'', 1, N'ServerDisplayName', N'0', N'', N'ServerLastModified', N'<search><item searchable="True" name="ServerID" tokenized="False" content="False" id="71b26851-b68f-43b1-a327-252253c37fd0" /><item searchable="False" name="ServerDisplayName" tokenized="True" content="True" id="dbe591f8-e7ff-4df4-b838-c0b8bc332fd1" /><item searchable="False" name="ServerName" tokenized="True" content="True" id="001c185f-bd67-4114-b6d0-28cd04ae2fca" /><item searchable="False" name="ServerURL" tokenized="True" content="True" id="3ae8604d-4296-4e68-9f89-79b3c6e078c4" /><item searchable="False" name="ServerGUID" tokenized="False" content="False" id="dd0e9a1e-d279-4464-b614-fd678eb2cf67" /><item searchable="True" name="ServerLastModified" tokenized="False" content="False" id="d3495bd1-2ecc-4088-91fc-bc02b6d00956" /><item searchable="True" name="ServerEnabled" tokenized="False" content="False" id="fe4321ee-ad8b-420a-87a0-6d7285500dba" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1094, N'Web farm task', N'cms.WebFarmTask', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebFarmTask">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaskID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaskType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskTextData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskBinaryData" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="TaskCreated" type="xs:dateTime" minOccurs="0" />
              <xs:element name="TaskEnabled" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaskTarget" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="TaskIsAnonymous" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaskErrorMessage" minOccurs="0">
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
      <xs:selector xpath=".//CMS_WebFarmTask" />
      <xs:field xpath="TaskID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaskID" fieldcaption="TaskID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5e8df106-ca79-494f-bcf1-d0b016afbad9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskType" fieldcaption="TaskType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8f272cff-24c4-48d6-8199-d96f0b3a4c49" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskTextData" fieldcaption="TaskTextData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="35ac3782-f563-4df0-af7e-989db67a7ec7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="TaskBinaryData" fieldcaption="TaskBinaryData" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="93186034-dfa3-49ac-8d6c-3c7c503e088c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskCreated" fieldcaption="TaskCreated" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2468f2df-6070-4941-82f0-04c1f9aa91d0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="TaskEnabled" fieldcaption="TaskEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e53d2ff4-2a03-41d3-95b9-8808a4a4e4af" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskTarget" fieldcaption="TaskTarget" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="92590b46-2f18-4de4-8a83-3e670dc72cce" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskMachineName" fieldcaption="TaskMachineName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0febed42-1c4f-4abe-86f8-b2cb7cf9a25e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaskGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9783e58c-39eb-43c8-ae5a-90f11a6797e5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaskIsAnonymous" fieldcaption="TaskIsAnonymous" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b90733f1-350f-4c90-a7f9-6ff237618cc2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaskErrorMessage" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0ba49fe4-479a-4821-9785-dc1376e8a388" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><controlname>bbeditorcontrol</controlname><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><Dialogs_Web_Hide>False</Dialogs_Web_Hide><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><Autoresize_Hashtable>True</Autoresize_Hashtable><Dialogs_Content_Hide>False</Dialogs_Content_Hide></settings></field></form>', N'', N'', N'', N'CMS_WebFarmTask', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110727 15:53:51', '44576ef8-a9b8-44e6-ba9d-4643f7de17cf', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1095, N'Root', N'CMS.Root', 0, 1, 0, N'', N'<form></form>', N'', N'', N'', N'', N'', N'', N'', N'', 0, 0, 0, N'', 1, N'', NULL, '20120216 08:50:56', 'a585aea3-10b5-4b74-9aad-747fcce72493', 0, 0, 0, N'', 0, N'DocumentName', N'DocumentContent', N'', N'DocumentCreatedWhen', N'<search></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
SET IDENTITY_INSERT [CMS_Class] OFF
