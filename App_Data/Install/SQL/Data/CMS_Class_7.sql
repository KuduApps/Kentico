SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1934, N'Media library', N'media.library', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Media_Library">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="LibraryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="LibraryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LibraryDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LibraryDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LibraryFolder">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LibraryAccess" type="xs:int" />
              <xs:element name="LibraryGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="LibrarySiteID" type="xs:int" />
              <xs:element name="LibraryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="LibraryLastModified" type="xs:dateTime" />
              <xs:element name="LibraryTeaserPath" minOccurs="0">
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
      <xs:selector xpath=".//Media_Library" />
      <xs:field xpath="LibraryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="LibraryID" fieldcaption="LibraryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="00fdb6b0-5f2e-4ef9-8648-1d3c7af8b721" /><field column="LibraryName" fieldcaption="LibraryName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="e4081abf-652e-47bd-82d0-313752f01873" /><field column="LibraryDisplayName" fieldcaption="LibraryDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="9b4aa82c-02df-4712-a5b9-3b9dee377b45" /><field column="LibraryDescription" fieldcaption="LibraryDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ac965989-ec7a-446f-81bb-ccb5043abf0b" /><field column="LibraryFolder" fieldcaption="LibraryFolder" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="06f6b6a9-08ca-4735-8732-20cc75d11802" /><field column="LibraryAccess" fieldcaption="LibraryAccess" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="720d2865-0be4-43d3-8ed8-412b269b1d00" /><field column="LibraryGroupID" fieldcaption="LibraryGroupID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="27889b22-33b2-48e6-b273-0d18c250676c" /><field column="LibrarySiteID" fieldcaption="LibrarySiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="99746a1e-3514-4c13-b878-7bb7b39ddb3d" /><field column="LibraryGUID" fieldcaption="LibraryGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="512c11dd-a325-4bf2-b1be-4f7c54397132" /><field column="LibraryLastModified" fieldcaption="LibraryLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="54f9c352-5ac2-4e4b-ab98-65d41240e9e4" /><field column="LibraryTeaserPath" fieldcaption="LibraryTeaserPath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="5de9f8d9-3e2d-4d83-8a63-6c2bdfb76629" /></form>', N'', N'', N'', N'Media_Library', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 16:46:35', 'dead7673-d566-4f83-87e3-e9a235b70e4f', 0, 0, 0, N'', 2, N'LibraryDisplayName', N'LibraryDescription', N'LibraryTeaserPath', N'LibraryLastModified', N'<search><item searchable="True" name="LibraryID" tokenized="False" content="False" id="62f643ab-fb54-47c8-bac6-c76b2c8fae4b" /><item searchable="False" name="LibraryName" tokenized="True" content="True" id="4a9038c6-c30d-411d-85a2-82e44861aa09" /><item searchable="False" name="LibraryDisplayName" tokenized="True" content="True" id="61fc89d0-264b-4b13-b329-6c51a65f262c" /><item searchable="False" name="LibraryDescription" tokenized="True" content="True" id="ca0eceb9-3678-43bb-a372-e820e0aca68f" /><item searchable="False" name="LibraryFolder" tokenized="True" content="True" id="09c6435a-5dbb-458c-acea-60eb51e0fb64" /><item searchable="True" name="LibraryAccess" tokenized="False" content="False" id="248545b9-04ea-41dd-be81-0e67b356893b" /><item searchable="True" name="LibraryGroupID" tokenized="False" content="False" id="cf9a7075-d5bb-4649-92ac-efb845df37d7" /><item searchable="True" name="LibrarySiteID" tokenized="False" content="False" id="fa214b77-24ed-4e41-a32d-d879b02b5e0c" /><item searchable="False" name="LibraryGUID" tokenized="False" content="False" id="3164e87f-eb64-4b29-b1e8-1fdd451344be" /><item searchable="True" name="LibraryLastModified" tokenized="False" content="False" id="0cc88a04-062d-4d77-adde-c91e88f20565" /><item searchable="False" name="LibraryTeaserPath" tokenized="True" content="True" id="c671060b-3381-4023-bfbd-cc0702dadb5f" /></search>', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1935, N'Media file', N'media.file', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Media_File">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FileID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="FileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileTitle">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FilePath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileSize" type="xs:long" />
              <xs:element name="FileImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="FileImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="FileGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FileLibraryID" type="xs:int" />
              <xs:element name="FileSiteID" type="xs:int" />
              <xs:element name="FileCreatedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="FileCreatedWhen" type="xs:dateTime" />
              <xs:element name="FileModifiedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="FileModifiedWhen" type="xs:dateTime" />
              <xs:element name="FileCustomData" minOccurs="0">
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
      <xs:selector xpath=".//Media_File" />
      <xs:field xpath="FileID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FileID" fieldcaption="FileID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="be274203-89f9-46c6-8430-686c3f4ed2ee" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileName" fieldcaption="FileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="e5922646-9755-419f-b97c-4da13c11e537" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileTitle" fieldcaption="File title" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="69be119f-9a88-4ee5-8d15-7f3fca93abc1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileDescription" fieldcaption="FileDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="61714ed7-0b1a-40ee-9dcf-a03c3ecff77e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="FileExtension" fieldcaption="FileExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="2fa8bf43-d045-4ad6-805d-22adcd05cddf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileMimeType" fieldcaption="FileMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="75fbd642-1882-4463-b813-00feddb929b7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FilePath" fieldcaption="FilePath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="6d1230c9-478c-4b29-b2e1-d055d0173bc7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileSize" fieldcaption="FileSize" visible="true" columntype="longinteger" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e230f41a-6ff1-4975-af9e-546c35cf50a2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileImageWidth" fieldcaption="FileImageWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e106b686-4bcc-467c-8c9e-08554ed47551" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileImageHeight" fieldcaption="FileImageHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ddc0242f-cd85-42e1-aaf9-baa98c0fcba2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileGUID" fieldcaption="FileGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e146f69c-1f65-433c-a091-616d9ac28aa7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileLibraryID" fieldcaption="FileLibraryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5b7f4a0a-386a-4575-beea-126f94381abb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileSiteID" fieldcaption="FileSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5657aab6-21f0-42aa-a901-c4396fb82a98" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileCreatedByUserID" fieldcaption="FileCreatedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2c37082f-cc5f-4ef1-b272-d28166037df1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileCreatedWhen" fieldcaption="FileCreatedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f4c0a650-7f2d-43d3-b56a-a5ae117c1f01" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="FileModifiedByUserID" fieldcaption="FileModifiedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8a3a9510-9f16-4760-937e-ff733dfe39ed" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileModifiedWhen" fieldcaption="FileModifiedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="63d7a235-fae6-4383-b251-7269b71f648d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="FileCustomData" fieldcaption="FileCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea859358-cb3e-4fd6-a16c-bfca37c7484c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'Media_File', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110913 13:46:29', 'd511179e-7fd5-42ee-8b51-048f58e45f6f', 0, 0, 0, N'', 2, N'FileTitle', N'FileDescription', N'', N'FileCreatedWhen', N'<search><item searchable="True" name="FileID" tokenized="False" content="False" id="30cf9e00-29b0-412b-a7f0-878c54aadba1" /><item searchable="False" name="FileName" tokenized="True" content="True" id="95a95607-3e85-4aba-9bac-4d57125012bc" /><item searchable="False" name="FileTitle" tokenized="True" content="True" id="10a81c75-c1ab-4275-ab11-2a0905e5dcdf" /><item searchable="False" name="FileDescription" tokenized="True" content="True" id="61acbd8a-a32f-4de5-9c55-6e1e526481f0" /><item searchable="False" name="FileExtension" tokenized="True" content="True" id="83cb9a4c-33c8-4391-974d-b1712ff882e4" /><item searchable="False" name="FileMimeType" tokenized="True" content="True" id="22f17d3d-2b83-4438-b5c3-4c4da9dd6f9d" /><item searchable="False" name="FilePath" tokenized="True" content="True" id="204aa287-55e1-43fb-8a58-97dfaf0461b3" /><item searchable="True" name="FileSize" tokenized="False" content="False" id="f77bb2a9-294d-4842-aa28-e680c0a3c188" /><item searchable="True" name="FileImageWidth" tokenized="False" content="False" id="25616859-c0b1-4704-85d7-9e3918cd3017" /><item searchable="True" name="FileImageHeight" tokenized="False" content="False" id="af8e03e0-b158-40c6-9620-20b640caeb13" /><item searchable="False" name="FileGUID" tokenized="False" content="False" id="b4e29687-7622-44ae-92db-c278f67d699d" /><item searchable="True" name="FileLibraryID" tokenized="False" content="False" id="ceefb90b-a5a4-4a53-96f7-265d73287703" /><item searchable="True" name="FileSiteID" tokenized="False" content="False" id="40f7e129-ec1e-4291-97ea-60f0b8478e4d" /><item searchable="True" name="FileCreatedByUserID" tokenized="False" content="False" id="5e9ec9d8-0dd3-4cbc-ab47-f6bf56e282dc" /><item searchable="True" name="FileCreatedWhen" tokenized="False" content="False" id="5efe5dc2-07e0-4640-b198-1b05d87c027a" /><item searchable="True" name="FileModifiedByUserID" tokenized="False" content="False" id="08e3f3dc-0642-4fda-93db-de6d9bbacf20" /><item searchable="True" name="FileModifiedWhen" tokenized="False" content="False" id="40ed2224-8a56-4c6c-ac09-00c1ba90bfcf" /><item searchable="False" name="FileCustomData" tokenized="True" content="True" id="55f83d30-0258-4cc2-b39c-6b6b82525391" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1937, N'Media library role permission', N'media.libraryrolepermission', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Media_LibraryRolePermission">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="LibraryID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="PermissionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Media_LibraryRolePermission" />
      <xs:field xpath="LibraryID" />
      <xs:field xpath="RoleID" />
      <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="LibraryID" fieldcaption="LibraryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="38f76421-d982-45b1-8cc6-7c1b8f6c6655" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e48a6d51-7ef0-4059-bd5e-013c8e401606" /><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fb93207d-e013-4f0d-bdac-0d4182519e0e" /></form>', N'', N'', N'', N'Media_LibraryRolePermission', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100827 12:40:40', '7707e68b-a76e-42ad-b07b-93f26d529778', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1962, N'Invitation', N'Community.Invitation', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Community_Invitation">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="InvitationID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="InvitedUserID" type="xs:int" minOccurs="0" />
              <xs:element name="InvitedByUserID" type="xs:int" />
              <xs:element name="InvitationGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="InvitationCreated" type="xs:dateTime" minOccurs="0" />
              <xs:element name="InvitationValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="InvitationComment" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="InvitationGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="InvitationLastModified" type="xs:dateTime" />
              <xs:element name="InvitationUserEmail" minOccurs="0">
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
      <xs:selector xpath=".//Community_Invitation" />
      <xs:field xpath="InvitationID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="InvitationID" fieldcaption="InvitationID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a07dad62-5624-4db6-8cbe-c8e10672fdc9" /><field column="InvitedUserID" fieldcaption="InvitedUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4cc438c8-8e41-406e-a500-4f9e1145cb06" /><field column="InvitedByUserID" fieldcaption="InvitedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="94945103-4e87-489a-9230-f30cff5fa957" /><field column="InvitationGroupID" fieldcaption="InvitationGroupID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d4e30801-fd08-4f41-b7a3-32da2ee7a32e" /><field column="InvitationCreated" fieldcaption="InvitationCreated" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="19e7bd69-0588-4d86-a766-6b98adde8a05" /><field column="InvitationValidTo" fieldcaption="InvitationValidTo" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0b47e1d8-37cc-4fe7-b8b0-ccda4122597b" /><field column="InvitationComment" fieldcaption="InvitationComment" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8a54a0bc-67e0-454a-abd7-2f33e74ad2bb" /><field column="InvitationGUID" fieldcaption="InvitationGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="66fbf8be-0aed-43e8-bd59-4b7eddbb62c2" /><field column="InvitationLastModified" fieldcaption="InvitationLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0d726f6e-93c0-42dd-8767-4720376a5528" /><field column="InvitationUserEmail" fieldcaption="InvitationUserEmail" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="5caa5bab-b7ef-4bb8-b582-41c8948ed0c5" /></form>', N'', N'', N'', N'Community_Invitation', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:59:35', 'a6d0fba0-8420-4a6a-a4e5-a94babe73387', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1995, N'Group Role Permission', N'Community.GroupRolePermission', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Community_GroupRolePermission">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="GroupID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="PermissionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Community_GroupRolePermission" />
      <xs:field xpath="GroupID" />
      <xs:field xpath="RoleID" />
      <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="GroupID" fieldcaption="GroupID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f04ba8b6-2c34-45b8-a9a9-f7c71e5a14ee" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4e68ead1-29d8-4b6c-9c82-b07e2d3c38f0" /><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b22650fa-12f1-4751-a852-b918ef48c6e2" /></form>', N'', N'', N'', N'Community_GroupRolePermission', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100201 08:33:26', 'a0b09036-ae6a-41f2-bee9-58fd49710360', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2018, N'Blog post subscription', N'blog.postsubscription', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Blog_PostSubscription">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SubscriptionPostDocumentID" type="xs:int" />
              <xs:element name="SubscriptionUserID" type="xs:int" minOccurs="0" />
              <xs:element name="SubscriptionEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionLastModified" type="xs:dateTime" />
              <xs:element name="SubscriptionGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Blog_PostSubscription" />
      <xs:field xpath="SubscriptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriptionID" fieldcaption="SubscriptionID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="60d7d9ed-561d-4e2c-a1dc-a1b813e216df" /><field column="SubscriptionPostDocumentID" fieldcaption="SubscriptionPostDocumentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3922393b-dc76-4466-a2f7-ebff75d9ed91" /><field column="SubscriptionUserID" fieldcaption="SubscriptionUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="710adf87-4854-41a1-bb31-82c1a7013848" /><field column="SubscriptionEmail" fieldcaption="SubscriptionEmail" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="0b82f5f2-019b-431f-b618-e75d43a83788" /><field column="SubscriptionLastModified" fieldcaption="SubscriptionLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e797244c-52a6-4165-a765-313b63a84e9c" /><field column="SubscriptionGUID" fieldcaption="SubscriptionGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c34dfa10-582d-4dc3-816f-eb2f00823b6f" /></form>', N'', N'', N'', N'Blog_PostSubscription', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:48:45', 'b396f281-260f-458f-94a7-edc7c78588ea', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2043, N'Search index', N'cms.SearchIndex', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SearchIndex">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="IndexID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="IndexName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexAnalyzerType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexIsCommunityGroup" type="xs:boolean" />
              <xs:element name="IndexSettings" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="IndexLastModified" type="xs:dateTime" />
              <xs:element name="IndexLastRebuildTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="IndexType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexStopWordsFile" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexCustomAnalyzerAssemblyName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexCustomAnalyzerClassName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexBatchSize" type="xs:int" minOccurs="0" />
              <xs:element name="IndexStatus" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="IndexLastUpdate" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SearchIndex" />
      <xs:field xpath="IndexID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="IndexID" fieldcaption="IndexID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="11a12bb8-8daa-4d81-a93d-18b6ee6dd7fe" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IndexName" fieldcaption="IndexName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="1e61111e-be57-4ed5-bf42-5a831ef74e02" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IndexDisplayName" fieldcaption="IndexDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="f6062f9f-ddcb-432b-aa26-3eb19bec2136" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IndexType" fieldcaption="IndexType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="e196f557-21ab-4b65-8977-d5b14d33d333" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IndexAnalyzerType" fieldcaption="IndexAnalyzerType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="38f0655c-0b92-4436-915f-8a2545ee548a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IndexIsCommunityGroup" fieldcaption="IndexIsCommunityGroup" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9779c4b1-155e-473f-a19d-a54afbf99f8a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="IndexSettings" fieldcaption="IndexSettings" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6baa44c0-892e-4136-be2e-2e625347981f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="IndexGUID" fieldcaption="IndexGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7b121874-5f4d-44e1-a652-26273df5f674" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IndexLastModified" fieldcaption="IndexLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f52606a9-543c-4554-98c5-c1a3680ef7ee" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><controlname>calendarcontrol</controlname><editTime>false</editTime></settings></field><field column="IndexLastRebuildTime" fieldcaption="IndexLastRebuildTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a4a4036-c437-41ce-b2f7-2c1e5bf7eff1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><displayNow>true</displayNow><timezonetype>inherit</timezonetype><controlname>calendarcontrol</controlname><editTime>false</editTime></settings></field><field column="IndexStopWordsFile" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="16b06aff-63dc-4a32-8f0e-99bff88a0cf3" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IndexCustomAnalyzerAssemblyName" fieldcaption="IndexID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ee4cca86-033f-4e43-aa34-b969f8114ade" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="IndexCustomAnalyzerClassName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="false" guid="0cee35a9-e0bc-4ab8-98b5-391e67e1ee16" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="IndexBatchSize" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="869750fe-25c4-476c-b614-cb3aa1c6f5cc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="IndexStatus" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="4dee1752-a984-4385-b1e0-0187a0289441" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field column="IndexLastUpdate" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c83cc5f6-0453-47da-9550-9b147156d1fd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_SearchIndex', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110601 15:00:06', 'd81f1c1e-da26-43c5-9962-0e663c448629', 0, 0, 0, N'', 0, N'IndexDisplayName', N'0', N'', N'IndexLastRebuildTime', N'<search><item searchable="True" name="IndexID" tokenized="False" content="False" id="0dbf4800-8b11-434f-8c37-cd281ea96590" /><item searchable="False" name="IndexName" tokenized="True" content="True" id="d0764a1a-7f11-4ff3-8c72-277e879e4f5b" /><item searchable="False" name="IndexDisplayName" tokenized="True" content="True" id="3a539047-30da-4a1a-8139-abeb89399955" /><item searchable="False" name="IndexType" tokenized="True" content="True" id="2c317f8f-590f-4e2c-adcd-85de714070bf" /><item searchable="False" name="IndexAnalyzerType" tokenized="True" content="True" id="3a5d3f22-2a8e-49a9-afe6-0584d9a28a8e" /><item searchable="True" name="IndexIsCommunityGroup" tokenized="False" content="False" id="9d43ee2c-4c5c-42d6-8bea-4225bf3f7465" /><item searchable="False" name="IndexSettings" tokenized="True" content="True" id="d2d01c4b-7e7f-4149-b3bf-b1dd8144ee26" /><item searchable="False" name="IndexGUID" tokenized="False" content="False" id="f1ad4237-93ed-4ed0-b0e3-9aa2846e3406" /><item searchable="True" name="IndexLastModified" tokenized="False" content="False" id="7fb10fb9-a15d-4a3c-bd72-d130ae02aab0" /><item searchable="True" name="IndexLastRebuildTime" tokenized="False" content="False" id="65995830-12a4-4b64-9253-5873405f0c83" /><item searchable="False" name="IndexStopWordsFile" tokenized="True" content="True" id="3ee3e1de-9a06-4e31-9b3c-79a4b96cad1f" /><item searchable="False" name="IndexCustomAnalyzerAssemblyName" tokenized="True" content="True" id="360f2e5e-28f7-4f96-af8d-8f21108344ff" /><item searchable="False" name="IndexCustomAnalyzerClassName" tokenized="True" content="True" id="2b7a465b-5925-495e-9dda-eb86ea4a88b1" /><item searchable="True" name="IndexBatchSize" tokenized="False" content="False" id="0811c61f-cf8d-4f91-a9b9-a45cafd339ee" /></search>', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2044, N'Search index site', N'cms.SearchIndexSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="cms_SearchIndexSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="IndexID" type="xs:int" />
              <xs:element name="IndexSiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//cms_SearchIndexSite" />
      <xs:field xpath="IndexID" />
      <xs:field xpath="IndexSiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="IndexID" fieldcaption="IndexID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="92bfdd7c-ff31-494f-aeb8-40bc60b27098" visibility="none" /><field column="IndexSiteID" fieldcaption="IndexSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f6c6fe0a-45e8-45cf-9266-5a214dbdd6ae" /></form>', N'', N'', N'', N'CMS_SearchIndexSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20101005 09:22:08', 'edd838a8-13e8-4195-9ba8-092da8b0680f', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2045, N'Search index culture', N'cms.SearchIndexCulture', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="cms_SearchIndexCulture">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="IndexID" type="xs:int" />
              <xs:element name="IndexCultureID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//cms_SearchIndexCulture" />
      <xs:field xpath="IndexID" />
      <xs:field xpath="IndexCultureID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="IndexID" fieldcaption="IndexID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8e994fd3-5f76-40cd-9dba-20c5ac7ff343" visibility="none" /><field column="IndexCultureID" fieldcaption="IndexCultureID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1a35e0e0-f57a-4e2e-96f5-6df781c65214" /></form>', N'', N'', N'', N'CMS_SearchIndexCulture', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20101005 09:20:59', '3c8d501e-1051-41e4-b0a5-0acac7c7d065', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2046, N'Search task', N'CMS.SearchTask', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="cms_SearchTask">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SearchTaskID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SearchTaskType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SearchTaskObjectType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SearchTaskField" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SearchTaskValue">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="600" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SearchTaskServerName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SearchTaskStatus">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SearchTaskPriority" type="xs:int" />
              <xs:element name="SearchTaskCreated" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//cms_SearchTask" />
      <xs:field xpath="SearchTaskID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SearchTaskID" fieldcaption="SearchTaskID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="2d7ca691-6e80-4ab6-88ac-7609f9fc216f" /><field column="SearchTaskType" fieldcaption="SearchTaskType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="851b8b0b-5aad-49d3-9d01-12dadd8c2bd4" /><field column="SearchTaskObjectType" fieldcaption="SearchTaskObjectType" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="db90abf1-a656-4c80-a617-48ebc9f056b5" /><field column="SearchTaskField" fieldcaption="SearchTaskField" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2deab677-b15a-4540-94d5-3a35d17d66ef" /><field column="SearchTaskValue" fieldcaption="SearchTaskValue" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="600" publicfield="false" spellcheck="true" guid="91d12388-cfbb-42ec-9504-d46641293e97" visibility="none" ismacro="false" /><field column="SearchTaskServerName" fieldcaption="SearchTaskServerName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="178cb9b9-eb79-471e-ab7e-22ce39b8d02d" /><field column="SearchTaskStatus" fieldcaption="SearchTaskStatus" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="fada5670-c89f-4799-b985-f918efeb9531" /><field column="SearchTaskCreated" visible="false" columntype="datetime" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1d560e5a-d4bb-4aa1-9a9a-3766244f444a" visibility="none"><settings><timezonetype>inherit</timezonetype></settings></field></form>', N'', N'', N'', N'CMS_SearchTask', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:53:27', '3ff254fe-d4de-4ad9-a6c9-cb71fc96c684', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2132, N'User culture', N'cms.userculture', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_UserCulture">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="CultureID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_UserCulture" />
      <xs:field xpath="UserID" />
      <xs:field xpath="CultureID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="bb23cacf-feb8-46f8-8d64-e5c6007d54ab" /><field column="CultureID" fieldcaption="CultureID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="558b122d-669e-486c-981a-a64f786f1fd7" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b263018-f23a-4640-84bc-674a6292c321" /></form>', N'', N'', N'', N'CMS_UserCulture', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20090609 13:51:22', '04792d69-14ca-4e35-a851-461c0fe2502a', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2195, N'UI element', N'CMS.UIElement', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_UIElement">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ElementID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ElementDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementCaption" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementTargetURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="650" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementResourceID" type="xs:int" />
              <xs:element name="ElementParentID" type="xs:int" minOccurs="0" />
              <xs:element name="ElementChildCount" type="xs:int" />
              <xs:element name="ElementOrder" type="xs:int" minOccurs="0" />
              <xs:element name="ElementLevel" type="xs:int" />
              <xs:element name="ElementIDPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementIconPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementIsCustom" type="xs:boolean" />
              <xs:element name="ElementLastModified" type="xs:dateTime" />
              <xs:element name="ElementGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ElementSize" type="xs:int" minOccurs="0" />
              <xs:element name="ElementDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ElementFromVersion" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_UIElement" />
      <xs:field xpath="ElementID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ElementID" fieldcaption="ElementID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="645c0c6d-7049-4e6b-b82c-4e955281a6ab" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ElementDisplayName" fieldcaption="ElementDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="b4631bcc-8acc-48ed-b44a-6db67e7a9a70" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementName" fieldcaption="ElementName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="e3267f50-9cd8-462c-b6a4-8e682c0a7a2b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementCaption" fieldcaption="ElementCaption" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="44ebc475-fa8e-4b62-bdb3-facec141da14" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementTargetURL" fieldcaption="ElementTargetURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="650" publicfield="false" spellcheck="true" guid="7dd2f1ba-80b1-4295-b937-029afdc2d584" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ElementResourceID" fieldcaption="ElementResourceID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cddb57a0-5500-4011-a414-2e6e6e214f90" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementParentID" fieldcaption="ElementParentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c1534088-c759-4c87-9c2c-e39f75cdfa87" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementChildCount" fieldcaption="ElementChildCount" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ee66fff4-f1c0-4f20-81e0-cbb00de1c95a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementOrder" fieldcaption="ElementOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="53486043-468b-4937-a6e0-f9fe28adb638" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementLevel" fieldcaption="ElementLevel" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d3a88b9f-adbb-4f04-a212-2d391b5ea5d6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementIDPath" fieldcaption="ElementIDPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="0cbf65ee-21c4-447e-92c9-139b9ccf882d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementIconPath" fieldcaption="ElementIconPath" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="4aa48ec6-c813-439d-9a5b-4c94a92ee26d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ElementIsCustom" fieldcaption="ElementIsCustom" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ab307308-5525-4044-96ea-a2596417b87e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ElementLastModified" fieldcaption="ElementLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0a3574c2-c3f2-4434-bf1a-7bd043b58a76" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ElementGUID" fieldcaption="ElementGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ec245fac-abd8-488a-8b9a-780afeca2e15" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ElementSize" fieldcaption="ElementSize" visible="true" defaultvalue="0" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b0fbdae8-2dcf-480b-9214-8ba738aa8fed" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ElementDescription" fieldcaption="ElementDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5b75437-8e04-489b-a36b-fc65c12666cc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ElementFromVersion" fieldcaption="ElementFromVersion" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" columnsize="20" publicfield="false" spellcheck="false" guid="090113df-a8b8-40de-8812-6dc1b89e63c0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>countryselector</controlname></settings></field></form>', N'', N'', N'', N'CMS_UIElement', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110914 13:54:42', '756292a7-ea43-4b8c-a343-6e88dbc3d38e', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2196, N'Role UI Element', N'CMS.RoleUIElement', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_RoleUIElement">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="ElementID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_RoleUIElement" />
      <xs:field xpath="RoleID" />
      <xs:field xpath="ElementID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4ace5a61-c739-4478-a14b-e66f7cdd093b" /><field column="ElementID" fieldcaption="ElementID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="39275590-79b9-41e4-81b9-2c026730c5d7" /></form>', N'', N'', N'', N'CMS_RoleUIElement', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20090903 12:16:18', 'ea08b2b5-89d6-4011-be2c-b4eb47283f6e', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2197, N'Widget', N'cms.Widget', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Widget">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WidgetID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WidgetWebPartID" type="xs:int" />
              <xs:element name="WidgetDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetCategoryID" type="xs:int" />
              <xs:element name="WidgetProperties" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetSecurity" type="xs:int" />
              <xs:element name="WidgetGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WidgetLastModified" type="xs:dateTime" />
              <xs:element name="WidgetIsEnabled" type="xs:boolean" />
              <xs:element name="WidgetForGroup" type="xs:boolean" />
              <xs:element name="WidgetForEditor" type="xs:boolean" />
              <xs:element name="WidgetForUser" type="xs:boolean" />
              <xs:element name="WidgetForDashboard" type="xs:boolean" />
              <xs:element name="WidgetForInline" type="xs:boolean" />
              <xs:element name="WidgetDocumentation" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetDefaultValues" minOccurs="0">
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
      <xs:selector xpath=".//CMS_Widget" />
      <xs:field xpath="WidgetID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WidgetID" fieldcaption="WidgetID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4f7cba83-cc62-4cad-8b6a-f35cd55f4b5f" visibility="none" ismacro="false" /><field column="WidgetWebPartID" fieldcaption="WidgetWebPartID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f4ac38f-9358-45be-9a5a-3e0000716c7b" ismacro="false" /><field column="WidgetDisplayName" fieldcaption="WidgetDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="576b3117-e2b8-4377-9143-e792e48e32cc" ismacro="false" /><field column="WidgetName" fieldcaption="WidgetName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="16fa68bb-c899-410a-aef7-08527c4dacaa" ismacro="false" /><field column="WidgetDescription" fieldcaption="WidgetDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fc5bf0ad-5c38-4857-b953-c03f2918d4c1" ismacro="false" /><field column="WidgetCategoryID" fieldcaption="WidgetCategoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="35c79638-a901-43c3-982f-561d48567d4f" ismacro="false" /><field column="WidgetProperties" fieldcaption="WidgetProperties" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1c7e4924-a259-4d69-bcb6-e24e05845158" ismacro="false" /><field column="WidgetDocumentation" fieldcaption="WidgetDocumentation" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="68793616-f2af-4ef4-9773-9ff2253bf342" visibility="none" ismacro="false" /><field column="WidgetSecurity" fieldcaption="WidgetSecurity" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6027bc44-27d1-490d-af69-45b586a6c823" ismacro="false" /><field column="WidgetForGroup" fieldcaption="WidgetForGroup" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d7e8eedf-c6a6-4976-84e2-d3edadde5bf6" visibility="none" ismacro="false" /><field column="WidgetForEditor" fieldcaption="WidgetForEditor" visible="true" defaultvalue="false" columntype="boolean" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e8ff4627-d131-401e-9da3-ea6b97b2d443" visibility="none" ismacro="false" /><field column="WidgetForUser" fieldcaption="WidgetForUser" visible="true" defaultvalue="false" columntype="boolean" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="48cfed61-17fc-4985-80ad-ad1b4fb585bc" visibility="none" ismacro="false" /><field column="WidgetForInline" fieldcaption="WidgetForInline" visible="true" defaultvalue="false" columntype="boolean" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b5c0c231-e1d2-41a0-81e9-3ca6a17aac27" visibility="none" ismacro="false" /><field column="WidgetGUID" fieldcaption="WidgetGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9a1bd07b-a623-49af-a5ce-f7ed5959136c" ismacro="false" /><field column="WidgetLastModified" fieldcaption="WidgetLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aca51648-3b54-4e8f-82c5-5748e0d72ae2" ismacro="false" /><field column="WidgetIsEnabled" fieldcaption="WidgetIsEnabled" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c7106140-fec2-43f7-8794-4e929bd14f6b" visibility="none" ismacro="false" /><field column="WidgetForDashboard" visible="false" defaultvalue="false" columntype="boolean" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aa9c09e1-4e0e-43c8-a868-25b642576157" visibility="none" ismacro="false" /><field column="WidgetDefaultValues" fieldcaption="Widget default values" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6a96742b-9d64-48ef-82b8-89401ef4a67a" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_Widget', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110228 17:25:22', '3abbd36b-5e12-410a-a6d9-fa22d4007970', 0, 0, 0, N'', 0, N'WidgetDisplayName', N'WidgetDescription', N'', N'WidgetLastModified', N'<search><item searchable="True" name="WidgetID" tokenized="False" content="False" id="f959d376-1be4-4afc-962b-a414ebad5695" /><item searchable="True" name="WidgetWebPartID" tokenized="False" content="False" id="26fd9d90-33c9-41df-acdd-0aa6fdc75b10" /><item searchable="False" name="WidgetDisplayName" tokenized="True" content="True" id="00002148-77f8-4dea-84e9-fcfd8baf5ea0" /><item searchable="False" name="WidgetName" tokenized="True" content="True" id="9470a61f-635f-4c44-93a8-83ba1f7e5f1c" /><item searchable="False" name="WidgetDescription" tokenized="True" content="True" id="4d166dee-f874-403f-bd34-a213e73084ec" /><item searchable="True" name="WidgetCategoryID" tokenized="False" content="False" id="9056338a-6ec7-4fee-9d21-1459acebd91e" /><item searchable="False" name="WidgetProperties" tokenized="True" content="True" id="6acedb51-fe0d-4381-bf67-9331914f4d1d" /><item searchable="False" name="WidgetDocumentation" tokenized="True" content="True" id="e7b814ed-cdbc-47d1-a257-7d7dfa90e466" /><item searchable="True" name="WidgetSecurity" tokenized="False" content="False" id="deed685e-de6c-458d-877a-f3686e5c79e4" /><item searchable="True" name="WidgetForGroup" tokenized="False" content="False" id="014a981a-6576-493f-87fe-cd60cfd9eaa6" /><item searchable="True" name="WidgetForEditor" tokenized="False" content="False" id="ab0e7d61-7962-4167-b4a4-9bf9d2240e5a" /><item searchable="True" name="WidgetForUser" tokenized="False" content="False" id="43f1e2b9-1bc5-40e2-b99d-0b32ef55fecd" /><item searchable="True" name="WidgetForInline" tokenized="False" content="False" id="8699812c-94f8-4e64-8e73-76010cb2ddab" /><item searchable="False" name="WidgetGUID" tokenized="False" content="False" id="2b524eeb-060d-41f0-b237-b9dc8917ffd9" /><item searchable="True" name="WidgetLastModified" tokenized="False" content="False" id="d244d818-b366-4e97-b6a2-0d1aac284d33" /><item searchable="True" name="WidgetIsEnabled" tokenized="False" content="False" id="7ab74ecd-f267-4168-b1c1-7155a170dffe" /><item searchable="True" name="WidgetForDashboard" tokenized="False" content="False" id="0581cea6-cf78-4048-b89d-652a0bb8983b" /><item searchable="False" name="WidgetDefaultValues" tokenized="True" content="True" id="ca371bdd-9334-4cd9-b431-4da642b21123" /></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2198, N'Widget category', N'CMS.WidgetCategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WidgetCategory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WidgetCategoryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WidgetCategoryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetCategoryDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetCategoryParentID" type="xs:int" minOccurs="0" />
              <xs:element name="WidgetCategoryPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetCategoryLevel" type="xs:int" />
              <xs:element name="WidgetCategoryOrder" type="xs:int" minOccurs="0" />
              <xs:element name="WidgetCategoryChildCount" type="xs:int" minOccurs="0" />
              <xs:element name="WidgetCategoryWidgetChildCount" type="xs:int" minOccurs="0" />
              <xs:element name="WidgetCategoryImagePath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WidgetCategoryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WidgetCategoryLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WidgetCategory" />
      <xs:field xpath="WidgetCategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WidgetCategoryID" fieldcaption="WidgetCategoryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="28fe3f1b-f233-41f5-95aa-b31f6cfbe7c7" visibility="none" ismacro="false" /><field column="WidgetCategoryName" fieldcaption="WidgetCategoryName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="f8ee7f18-26fc-46e4-82fd-c64479e3989f" /><field column="WidgetCategoryDisplayName" fieldcaption="WidgetCategoryDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="31e41178-316e-4ee0-9b70-3e0a970ac2d8" /><field column="WidgetCategoryParentID" fieldcaption="WidgetCategoryParentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f848c87-8f3d-44c9-ab7c-9f4ae008ad28" /><field column="WidgetCategoryPath" fieldcaption="WidgetCategoryPath" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="c7708ea9-a750-4e5e-92af-7079316e0a86" /><field column="WidgetCategoryLevel" fieldcaption="WidgetCategoryLevel" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c4224d1f-13c0-442f-9b75-521dea57fbd3" /><field column="WidgetCategoryOrder" fieldcaption="WidgetCategoryOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f0af481c-07e0-48e8-ae90-aac6d353f7cd" /><field column="WidgetCategoryChildCount" fieldcaption="WidgetCategoryChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="70aa81f0-9142-49f4-a9c2-6621f448f573" /><field column="WidgetCategoryWidgetChildCount" fieldcaption="WidgetCategoryWidgetChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a127d34b-ec04-40ab-bdb2-4d46697268f5" /><field column="WidgetCategoryImagePath" fieldcaption="WidgetCategoryImagePath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="977af818-688b-43eb-89f1-fd720b8553d5" /><field column="WidgetCategoryGUID" fieldcaption="WidgetCategoryGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cedcae5c-aaff-41da-ba76-d120e5b9ed39" /><field column="WidgetCategoryLastModified" fieldcaption="WidgetCategoryLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f614a167-5ad4-4507-b05b-6b06afe26102" /></form>', N'', N'', N'', N'CMS_WidgetCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:13:55', '3e6a29e9-75e0-423c-b989-58b44e689a66', 0, 0, 0, N'', 0, N'WidgetCategoryDisplayName', N'0', N'WidgetCategoryImagePath', N'0', N'<search><item searchable="True" name="WidgetCategoryID" tokenized="False" content="False" id="8d059536-f2bc-48cb-b04d-2ef5c3bde804" /><item searchable="False" name="WidgetCategoryName" tokenized="True" content="True" id="e0f7f419-f84c-44d9-a632-58dedf2c2b7b" /><item searchable="False" name="WidgetCategoryDisplayName" tokenized="True" content="True" id="6d3fde69-bc3b-4884-af57-95f004e20e00" /><item searchable="True" name="WidgetCategoryParentID" tokenized="False" content="False" id="5440a5c3-24a8-46fc-ae80-65cf437566d3" /><item searchable="False" name="WidgetCategoryPath" tokenized="True" content="True" id="6ab8d164-0502-4721-a139-f04b737991e4" /><item searchable="True" name="WidgetCategoryLevel" tokenized="False" content="False" id="0e5ef0eb-53a3-499c-bc0a-be2086cf05ac" /><item searchable="True" name="WidgetCategoryOrder" tokenized="False" content="False" id="141f76c0-efe2-447c-89a0-a91364456a50" /><item searchable="True" name="WidgetCategoryChildCount" tokenized="False" content="False" id="e8674a11-f937-4247-8dfc-339f96a9ceb1" /><item searchable="True" name="WidgetCategoryWidgetChildCount" tokenized="False" content="False" id="cb19a206-70af-46cf-80e0-1de02b2aa2f6" /><item searchable="False" name="WidgetCategoryImagePath" tokenized="True" content="True" id="a9fd4638-c5c5-40ce-838b-240171d73df0" /><item searchable="False" name="WidgetCategoryGUID" tokenized="False" content="False" id="f4d352ed-4749-4e4d-b979-c41baa3f5a17" /><item searchable="True" name="WidgetCategoryLastModified" tokenized="False" content="False" id="ce8a3879-ae66-462b-948a-e0a03b8e17f9" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2199, N'WidgetRole', N'CMS.WidgetRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WidgetRole">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WidgetID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
              <xs:element name="PermissionID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WidgetRole" />
      <xs:field xpath="WidgetID" />
      <xs:field xpath="RoleID" />
      <xs:field xpath="PermissionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WidgetID" fieldcaption="WidgetID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="7a693a1c-2362-41ed-8d71-efe36276ac49" visibility="none" ismacro="false" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d32ed50f-96d8-4874-81f7-f9b46c5699a1" /><field column="PermissionID" fieldcaption="PermissionID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c62e9caf-e4d2-4627-86df-91650e9fa1fa" visibility="none" /></form>', N'', N'', N'', N'CMS_WidgetRole', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110530 12:05:23', '6bd0e677-683e-4875-8dcb-686295c0320b', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2337, N'Page template scope', N'cms.pagetemplatescope', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_PageTemplateScope">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PageTemplateScopeID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PageTemplateScopePath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateScopeLevels" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PageTemplateScopeCultureID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateScopeClassID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateScopeTemplateID" type="xs:int" />
              <xs:element name="PageTemplateScopeSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="PageTemplateScopeLastModified" type="xs:dateTime" />
              <xs:element name="PageTemplateScopeGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_PageTemplateScope" />
      <xs:field xpath="PageTemplateScopeID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PageTemplateScopeID" fieldcaption="PageTemplateScopeID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="2198e351-37d1-413b-8478-96c340d4fa87" ismacro="false" /><field column="PageTemplateScopePath" visible="false" columntype="text" fieldtype="label" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="9546eb9d-d482-4df7-81d3-7269cf644ff8" visibility="none" ismacro="false" /><field column="PageTemplateScopeClassID" visible="false" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6dce0208-c825-49b2-809f-c945a3abe9ff" visibility="none" ismacro="false" /><field column="PageTemplateScopeCultureID" visible="false" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aab07400-a230-4de9-81a1-9db8be770830" visibility="none" ismacro="false" /><field column="PageTemplateScopeLevels" visible="false" columntype="text" fieldtype="label" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="31e04b8d-0afe-4828-9b19-ea18be43c132" visibility="none" ismacro="false" /><field column="PageTemplateScopeSiteID" fieldcaption="PageTemplateScopeSiteID" visible="false" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2ba9fc4f-2ec2-400e-9676-d077214eba69" visibility="none" ismacro="false" /><field column="PageTemplateScopeTemplateID" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a86754fa-ce7c-4997-9082-f44adf46b012" visibility="none" ismacro="false" /><field column="PageTemplateScopeLastModified" visible="false" columntype="datetime" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca965801-5580-4dc5-88cf-59601e7f5a2a" visibility="none" ismacro="false"><settings><timezonetype>inherit</timezonetype></settings></field><field column="PageTemplateScopeGUID" visible="false" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4be17f8e-dcca-452c-927a-e7c94c4a4187" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_PageTemplateScope', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:56:59', '9bd4c694-369e-444e-842e-947383f8da43', 0, 0, 0, N'', 0, N'PageTemplateScopePath', N'0', N'', N'PageTemplateScopeLastModified', N'<search><item searchable="True" name="PageTemplateScopeID" tokenized="False" content="False" id="284e6dbb-8d03-48be-a006-133a8c060edd" /><item searchable="False" name="PageTemplateScopePath" tokenized="True" content="True" id="a71bcaf5-baed-43fa-8347-b07b3a80691f" /><item searchable="True" name="PageTemplateScopeClassID" tokenized="False" content="False" id="4cb30fb9-a997-4bc8-9e00-b7b88738f8bc" /><item searchable="True" name="PageTemplateScopeCultureID" tokenized="False" content="False" id="5619696c-544a-4ebb-8f69-1e6ae8927c1a" /><item searchable="False" name="PageTemplateScopeLevels" tokenized="True" content="True" id="7d85b506-d979-402f-9eb7-fa67f9bc3c98" /><item searchable="True" name="PageTemplateScopeSiteID" tokenized="False" content="False" id="c353b86d-6e5b-435c-b97f-add4493ade9c" /><item searchable="True" name="PageTemplateScopeTemplateID" tokenized="False" content="False" id="5b86993d-46ae-4d09-a8ff-8d8857733202" /><item searchable="True" name="PageTemplateScopeLastModified" tokenized="False" content="False" id="e4957792-478f-4a46-a0e9-a69bac8e4d7f" /><item searchable="False" name="PageTemplateScopeGUID" tokenized="False" content="False" id="327a582f-4b1c-44e9-840a-073a2a46bf8a" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2371, N'OpenIDUser', N'cms.OpenIDUser', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="cms_OpenIDUser">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="OpenIDUserID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="OpenID">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OpenIDProviderURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//cms_OpenIDUser" />
      <xs:field xpath="OpenIDUserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="OpenIDUserID" fieldcaption="OpenIDUserID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e41dcedb-534b-4546-8822-7b22a484340b" ismacro="false" visibility="none" /><field column="OpenID" fieldcaption="OpenID" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="4dfa8c83-42e4-4856-ba2b-d1b789f18ff0" ismacro="false" /><field column="OpenIDProviderURL" fieldcaption="OpenIDProviderURL" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="29626164-9916-4631-926f-fa32554566c5" ismacro="false" visibility="none" /><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="98e15d62-ce90-4fda-9736-02444c4470a9" ismacro="false" visibility="none" /></form>', N'', N'', N'', N'CMS_OpenIDUser', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100826 18:05:19', '1d4b85e6-4780-4f85-a0ab-894ac18d261a', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2426, N'Temporary file', N'Temp.File', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Temp_File">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FileID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="FileParentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FileNumber" type="xs:int" />
              <xs:element name="FileExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileSize" type="xs:long" />
              <xs:element name="FileMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="FileImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="FileBinary" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="FileGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FileLastModified" type="xs:dateTime" />
              <xs:element name="FileDirectory">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FileDescription" minOccurs="0">
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
      <xs:selector xpath=".//Temp_File" />
      <xs:field xpath="FileID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FileID" fieldcaption="FileID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="5abf2688-3875-4ce7-a83d-0aafd362b52b" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileParentGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="215a1e76-6d11-491e-8c1b-6f0d011462bc" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileNumber" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd151d86-81f3-47f7-afee-a1a556324fa1" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileDirectory" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="5b00c2b8-265b-488b-9b2c-a1eaa70108ea" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="362ec0be-77ad-49da-806c-3765079e6f31" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileExtension" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="11b03f83-aaa2-4b72-8656-c2bbc4ad1976" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileSize" visible="false" columntype="longinteger" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42e44002-4ccd-44bc-9384-e7c752a6132c" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileMimeType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="51025316-9427-4f31-8560-cbe539662ea1" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileImageWidth" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0dff9216-c2ff-4d19-98b2-a1ad2345dafa" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileImageHeight" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="16008d49-dd76-4ca6-a37e-8b88c670e427" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileBinary" visible="false" columntype="binary" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b0088401-e9e3-4385-96f4-e8c8791aba42" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bdfcbbd5-0147-4bf9-8869-2985e5d422b2" visibility="none" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FileLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ffe61141-5b36-485e-b69c-4c3f7866dac5" visibility="none" ismacro="false"><settings><timezonetype>inherit</timezonetype><controlname>labelcontrol</controlname></settings></field><field column="FileTitle" fieldcaption="FileTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="bd81da9a-4501-4350-a20e-5c781143fa16" visibility="none" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FileDescription" fieldcaption="FileDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a91f1701-e840-4e49-8544-bb5b8e3a554e" visibility="none" ismacro="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field></form>', N'', N'', N'', N'Temp_File', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20101103 09:43:22', '0b6b1848-09a8-4564-9853-6d11b2bf621b', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2429, N'Newsletter - Opened emails', N'newsletter.openedemail', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_OpenedEmail">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriberID" type="xs:int" />
              <xs:element name="IssueID" type="xs:int" />
              <xs:element name="OpenedWhen" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_OpenedEmail" />
      <xs:field xpath="SubscriberID" />
      <xs:field xpath="IssueID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriberID" fieldcaption="SubscriberID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="211b5600-fb5a-4889-93ce-0355c1647814" ismacro="false" /><field column="IssueID" fieldcaption="IssueID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e9e04e5-6c7c-4728-ba5f-f2ff4a6cdef3" ismacro="false" /><field column="OpenedWhen" fieldcaption="OpenedWhen" visible="true" columntype="datetime" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f371025-2be9-4f41-b5c3-13775735c393" ismacro="false" /></form>', N'', N'', N'', N'Newsletter_OpenedEmail', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111117 11:27:13', 'bae1e405-c859-4444-b9ce-daca14f5e1d7', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2461, N'Ecommerce - Shipping cost', N'Ecommerce.ShippingCost', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_ShippingCost">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ShippingCostID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ShippingCostShippingOptionID" type="xs:int" />
              <xs:element name="ShippingCostMinWeight" type="xs:double" />
              <xs:element name="ShippingCostValue" type="xs:double" />
              <xs:element name="ShippingCostGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ShippingCostLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_ShippingCost" />
      <xs:field xpath="ShippingCostID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ShippingCostID" fieldcaption="ShippingCostID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="19136248-cd5f-4f04-a9d6-52290fccf00e" ismacro="false" /><field column="ShippingCostShippingOptionID" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d6f69e5f-8722-4a7b-a243-f05bd97922e8" visibility="none" ismacro="false" /><field column="ShippingCostMinWeight" visible="false" columntype="double" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="242b5242-87b0-4be3-a43a-80d3d08c98e6" visibility="none" ismacro="false" /><field column="ShippingCostValue" visible="false" columntype="double" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43532083-fa4d-4d5d-b48d-100e3e29ca33" visibility="none" ismacro="false" /><field column="ShippingCostGUID" visible="false" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e31ce320-3f74-465e-85bc-491c6752d6f0" visibility="none" ismacro="false" /><field column="ShippingCostLastModified" visible="false" columntype="datetime" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="54cf481f-eb80-4efc-a273-5502a8b4e856" visibility="none" ismacro="false"><settings><timezonetype>inherit</timezonetype></settings></field></form>', N'', N'', N'', N'COM_ShippingCost', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110824 14:48:27', 'ea53e677-0d87-4873-a279-b51b06d9c56e', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2463, N'Ecommerce - Shipping option tax class', N'Ecommerce.ShippingOptionTaxClass', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_ShippingOptionTaxClass">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ShippingOptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaxClassID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_ShippingOptionTaxClass" />
      <xs:field xpath="ShippingOptionID" />
      <xs:field xpath="TaxClassID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ShippingOptionID" fieldcaption="ShippingOptionID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="8b539200-c027-4059-a2fc-ae3d602c9320" ismacro="false" /><field column="TaxClassID" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a7004d42-86eb-4564-9f77-1eaca3541b87" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'COM_ShippingOptionTaxClass', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100809 10:14:32', '3f9ca6be-0b1e-4494-b1d1-08e3842634d6', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2491, N'A/B test', N'OM.ABTest', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_ABTest">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ABTestID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ABTestName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABTestDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABTestCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABTestOriginalPage">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABTestOpenFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ABTestOpenTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ABTestEnabled" type="xs:boolean" />
              <xs:element name="ABTestSiteID" type="xs:int" />
              <xs:element name="ABTestMaxConversions" type="xs:int" minOccurs="0" />
              <xs:element name="ABTestConversions" type="xs:int" minOccurs="0" />
              <xs:element name="ABTestGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ABTestLastModified" type="xs:dateTime" />
              <xs:element name="ABTestTargetConversionType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABTestDisplayName">
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
      <xs:selector xpath=".//OM_ABTest" />
      <xs:field xpath="ABTestID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ABTestID" fieldcaption="ABTestID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="86655528-dafb-45db-83d4-dd0b75569349" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestName" fieldcaption="ABTestName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="1d1b3ae7-1a63-48f7-ae8c-5984d7063c8f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ABTestDisplayName" fieldcaption="ABTestDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="19a599a7-6256-476a-8d37-9faf5ac33dbf" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ABTestDescription" fieldcaption="ABTestDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="559f6e45-2cc3-4326-9f5c-f66cb4e45cce" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestCulture" fieldcaption="ABTestCulture" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="0e01436b-dc1c-4bc0-a57b-a2e6a55dfac1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestOriginalPage" fieldcaption="ABTestOriginalPage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="68d756ae-ca0e-4cc0-b903-f79d10f2f701" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestOpenFrom" fieldcaption="ABTestOpenFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fab79e13-0177-4616-bd9b-a1a48ba32fc1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestOpenTo" fieldcaption="ABTestOpenTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a99b03df-10da-4804-ae9c-dd8924733318" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestEnabled" fieldcaption="ABTestEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eeb22c23-7a35-4fc0-a2c1-252d7fafcbb6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestSiteID" fieldcaption="ABTestSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b8c25b1d-6ab4-46e7-994a-5d844cbb5625" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestMaxConversions" fieldcaption="ABTestMaxConversions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="34ef7893-959e-47cc-96b9-c9882fc9f642" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestConversions" fieldcaption="ABTestConversions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c0c5841f-842b-47db-aae8-7722087ac588" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestTargetConversionType" fieldcaption="AB test target cinversuin type" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="false" guid="3037f5ef-52bb-4ebc-abf0-006c1d7d6e60" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ABTestGUID" fieldcaption="ABTestGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eab35f06-804c-4193-8830-dee55dae8deb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ABTestLastModified" fieldcaption="ABTestLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="80955796-c133-49d8-a7eb-1f9b10399e25" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field></form>', N'', N'', N'', N'OM_ABTest', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111006 14:55:29', '71a1d617-6161-4ad6-aee2-be5756048223', 0, 0, 0, N'', 0, N'ABTestName', N'ABTestDescription', N'', N'ABTestLastModified', N'<search><item searchable="True" name="ABTestID" tokenized="False" content="False" id="932b7100-3798-4971-aba2-6f093923bdf2" /><item searchable="False" name="ABTestName" tokenized="True" content="True" id="b661d86c-3511-4c5c-9384-62baa82d05b5" /><item searchable="False" name="ABTestDescription" tokenized="True" content="True" id="9ee9eace-eb48-40e2-8730-e13d45602156" /><item searchable="False" name="ABTestCulture" tokenized="True" content="True" id="ddcc0a5e-8cf4-4145-b252-4fc394d3f1fb" /><item searchable="False" name="ABTestOriginalPage" tokenized="True" content="True" id="0899d7ad-8e06-4a8e-b777-59efbae24fef" /><item searchable="True" name="ABTestOpenFrom" tokenized="False" content="False" id="1120c699-df7e-4111-9896-ef6721a8fb60" /><item searchable="True" name="ABTestOpenTo" tokenized="False" content="False" id="46726b7a-439b-4eb3-9046-dc7fb951f2d9" /><item searchable="True" name="ABTestEnabled" tokenized="False" content="False" id="a4483b08-c0d7-4543-aaf3-e8d3a75ad8c5" /><item searchable="True" name="ABTestSiteID" tokenized="False" content="False" id="98ab51c7-57b4-46d4-8584-6bee9e28aa82" /><item searchable="True" name="ABTestMaxConversions" tokenized="False" content="False" id="d7532e94-c431-465f-a079-1a33d911b0d2" /><item searchable="True" name="ABTestConversions" tokenized="False" content="False" id="ccff2f28-5d51-401a-b7ed-af3530557270" /><item searchable="False" name="ABTestTargetConversionType" tokenized="True" content="True" id="7a76102e-4825-4d45-a130-1cbb544256e9" /><item searchable="False" name="ABTestGUID" tokenized="False" content="False" id="e5c64709-2c08-4b4d-b7f4-89eed22d1867" /><item searchable="True" name="ABTestLastModified" tokenized="False" content="False" id="155b919e-ff98-4f61-8071-f283bbb8641b" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2492, N'A/B variant', N'OM.ABVariant', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="OM_ABVariant">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ABVariantID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ABVariantDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="110" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABVariantName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABVariantTestID" type="xs:int" />
              <xs:element name="ABVariantPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ABVariantViews" type="xs:int" minOccurs="0" />
              <xs:element name="ABVariantConversions" type="xs:int" />
              <xs:element name="ABVariantGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ABVariantLastModified" type="xs:dateTime" />
              <xs:element name="ABVariantSiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//OM_ABVariant" />
      <xs:field xpath="ABVariantID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ABVariantID" fieldcaption="ABVariantID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f98434ef-e6d2-47b4-961c-cfc77732156a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ABVariantDisplayName" fieldcaption="Variant display name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="110" publicfield="false" spellcheck="true" guid="b312cf05-fbed-454c-8089-7068ef028c78" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="ABVariantName" fieldcaption="Allow empty value" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="9c8e730b-99e7-4a82-8e01-c806034b978f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="ABVariantTestID" fieldcaption="Variant test ID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="05f4478c-afca-4080-a1ed-0b57f7ced483" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ABVariantPath" fieldcaption="Variant path" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="7a30dbee-81e1-4e00-9f85-9f440474ccc9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ABVariantViews" fieldcaption="Variant views" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0c01f577-d3ba-4ecd-b1e1-7e2568efe769" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ABVariantConversions" fieldcaption="Variant conversions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="34aed6d7-753e-4080-b69c-087bc8f491a4" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ABVariantGUID" fieldcaption="Variant GUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4eddca74-5003-459d-8efb-03c804c19938" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ABVariantLastModified" fieldcaption="Last modified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c55c2642-3533-479b-a3fa-056327dead2e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><timezonetype>inherit</timezonetype><controlname>labelcontrol</controlname></settings></field><field column="ABVariantSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9859dbe7-c398-4b76-bdea-d39c28f1934e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'OM_ABVariant', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110914 16:37:15', 'fb969b83-36b9-4c02-9700-948f4e7892ea', 0, 0, 0, N'', 0, N'ABVariantDisplayName', N'0', N'', N'ABVariantLastModified', N'<search><item searchable="True" name="ABVariantID" tokenized="False" content="False" id="a0549892-c891-4947-adc0-9acc9f140ae9" /><item searchable="False" name="ABVariantDisplayName" tokenized="True" content="True" id="13407213-05bb-421c-bb7c-e5719808f7b7" /><item searchable="False" name="ABVariantName" tokenized="True" content="True" id="c2c1ef5e-4c9a-4e24-ad35-f8625a216269" /><item searchable="True" name="ABVariantTestID" tokenized="False" content="False" id="00076ce0-2762-437a-b354-8918a5aee63e" /><item searchable="False" name="ABVariantPath" tokenized="True" content="True" id="edc2d92b-f1c4-4025-bfbe-164080f4442d" /><item searchable="True" name="ABVariantViews" tokenized="False" content="False" id="c0ca7806-21bb-4adb-98cf-209fa4ba817b" /><item searchable="True" name="ABVariantConversions" tokenized="False" content="False" id="4266cd97-9c39-4ac5-8460-42fb598cfe56" /><item searchable="False" name="ABVariantGUID" tokenized="False" content="False" id="02aeffbf-359f-42b4-b89c-60296a2fea89" /><item searchable="True" name="ABVariantLastModified" tokenized="False" content="False" id="3f4b8773-43cf-40e8-914a-4b1835b8e556" /><item searchable="True" name="ABVariantSiteID" tokenized="False" content="False" id="eea97969-85d1-44f7-a44e-7cf1d4325ba2" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (2694, N'Newsletter - Clicked links', N'newsletter.link', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Newsletter_Link">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="LinkID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="LinkIssueID" type="xs:int" />
              <xs:element name="LinkTarget">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LinkDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LinkOutdated" type="xs:boolean" />
              <xs:element name="LinkTotalClicks" type="xs:int" minOccurs="0" />
              <xs:element name="LinkGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Newsletter_Link" />
      <xs:field xpath="LinkID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="LinkID" fieldcaption="LinkID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b3f5d8ab-b3f1-4e4d-88f9-bbdf6ae31ad9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="LinkIssueID" fieldcaption="LinkIssueID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="6af9c1c0-c90c-4c33-8dcc-db7bfbe41cfc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="LinkTarget" fieldcaption="LinkTarget" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="705a2c0f-3cc4-4a61-ac4b-22f28fea3129" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="LinkDescription" fieldcaption="LinkDescription" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="false" guid="f44debd1-8885-4117-928e-f5fd290bb32c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="LinkOutdated" fieldcaption="LinkOutdated" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="a7115405-3299-478f-b944-61c5f94adc0d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="LinkTotalClicks" fieldcaption="LinkTotalClicks" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="ea6827cc-91d1-4ed5-89a1-bd9f205de5e2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="LinkGUID" fieldcaption="LinkGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="7ef15a7f-5377-4320-b79e-3aa664641a11" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'Newsletter_Link', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111129 14:18:53', '1c15bf83-7266-4f4c-97ce-a180b2c0b41c', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
SET IDENTITY_INSERT [CMS_Class] OFF
