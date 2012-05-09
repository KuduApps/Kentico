SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1511, N'Event attendee', N'cms.EventAttendee', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Events_Attendee">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AttendeeID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AttendeeEmail">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttendeeFirstName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttendeeLastName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttendeePhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttendeeEventNodeID" type="xs:int" />
              <xs:element name="AttendeeGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="AttendeeLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Events_Attendee" />
      <xs:field xpath="AttendeeID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AttendeeID" fieldcaption="AttendeeID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4078b055-4042-41d8-9286-fe954ca4783d" /><field column="AttendeeEmail" fieldcaption="AttendeeEmail" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca9064cb-7f30-43fd-9086-4bcf7452ef97" /><field column="AttendeeFirstName" fieldcaption="AttendeeFirstName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8994e1ec-dc0c-43a2-b745-1e4b1a337681" /><field column="AttendeeLastName" fieldcaption="AttendeeLastName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="62fd46e4-e6c0-4302-98c3-d8bc3f1b0ded" /><field column="AttendeePhone" fieldcaption="AttendeePhone" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="49abfee2-d711-4f27-84e9-73265eeeb1e0" /><field column="AttendeeEventNodeID" fieldcaption="AttendeeEventNodeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ff35fcf0-80d0-4347-9343-f9f957a9a971" /><field column="AttendeeGUID" fieldcaption="AttendeeGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="839cd8b5-afa2-48d6-ac9a-cd8c2eb166d1" /><field column="AttendeeLastModified" fieldcaption="AttendeeLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="42c38270-1b2f-4f8d-ac55-b31b776727a3" /></form>', N'', N'', N'', N'Events_Attendee', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110301 10:10:23', '3eb7dbd1-e72a-4381-817a-789413c477c6', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1514, N'Web part layout', N'cms.WebPartLayout', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WebPartLayout">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WebPartLayoutID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WebPartLayoutCodeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutCode" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutCheckedOutFilename" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutCheckedOutByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="WebPartLayoutCheckedOutMachineName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutVersionGUID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WebPartLayoutWebPartID" type="xs:int" />
              <xs:element name="WebPartLayoutGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WebPartLayoutLastModified" type="xs:dateTime" />
              <xs:element name="WebPartLayoutCSS" minOccurs="0">
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
      <xs:selector xpath=".//CMS_WebPartLayout" />
      <xs:field xpath="WebPartLayoutID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WebPartLayoutID" fieldcaption="WebPartLayoutID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="fb354b46-35b3-4cd9-bbf0-85117bc33359" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="WebPartLayoutCodeName" fieldcaption="WebPartLayoutCodeName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="94d0fbe1-b684-4f7c-a501-0008a0a29e45" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutDisplayName" fieldcaption="WebPartLayoutDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a2407cc-9146-4950-90c4-78549ca7efaf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutDescription" fieldcaption="WebPartLayoutDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3678f082-f1da-4f2b-bd29-3f3dac4e1273" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebPartLayoutCode" fieldcaption="WebPartLayoutCode" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e0430f63-4ce2-4527-81e0-1cdc94ee2e20" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="WebPartLayoutCSS" fieldcaption="WebPartLayoutCSS" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="12930e48-c933-48de-b755-2f41b741da15" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textareacontrol</controlname></settings></field><field column="WebPartLayoutCheckedOutFilename" fieldcaption="WebPartLayoutCheckedOutFilename" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="74793c5c-c957-4acf-bd50-ca6d0eb66c79" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutCheckedOutByUserID" fieldcaption="WebPartLayoutCheckedOutByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f75c8c60-43f9-4dd1-a09d-3ede582c983c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutCheckedOutMachineName" fieldcaption="WebPartLayoutCheckedOutMachineName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="93580f0b-9557-4520-97c3-0879d8a287fa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutVersionGUID" fieldcaption="WebPartLayoutVersionGUID" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eaf51ec6-ab5a-468c-a3c2-b81427650c82" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutWebPartID" fieldcaption="WebPartLayoutWebPartID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e38dfda5-7c17-42b4-bffb-8ddf13bb8680" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WebPartLayoutGUID" fieldcaption="WebPartLayoutGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43c97c99-eb8d-4ef3-b5d2-7805ba29df79" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="WebPartLayoutLastModified" fieldcaption="WebPartLayoutLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f40f5517-7586-425e-aaac-f014ed51ba53" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_WebPartLayout', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:14:18', '05f3851c-9830-4bb4-a717-601e585211d3', 0, 1, 0, N'', 0, N'WebPartLayoutDisplayName', N'WebPartLayoutDescription', N'', N'0', N'<search><item searchable="True" name="WebPartLayoutID" tokenized="False" content="False" id="f2a92617-f906-4fe1-8734-fbf058ac8f6b" /><item searchable="False" name="WebPartLayoutCodeName" tokenized="True" content="True" id="4c7253b9-1e4d-4c19-ba8a-08d106148476" /><item searchable="False" name="WebPartLayoutDisplayName" tokenized="True" content="True" id="44afb786-4aed-4a51-ae14-347dffa36c48" /><item searchable="False" name="WebPartLayoutDescription" tokenized="True" content="True" id="bfae613f-42df-4b90-9f2e-98e4712fee97" /><item searchable="False" name="WebPartLayoutCode" tokenized="True" content="True" id="c108fbce-54a9-4cfb-b7de-d58bac8c7001" /><item searchable="False" name="WebPartLayoutCheckedOutFilename" tokenized="True" content="True" id="b5905df8-7916-4f7f-8f31-be37cb5a4e67" /><item searchable="True" name="WebPartLayoutCheckedOutByUserID" tokenized="False" content="False" id="eddc596a-e465-4b65-9b87-35bb67706d56" /><item searchable="False" name="WebPartLayoutCheckedOutMachineName" tokenized="True" content="True" id="c9a12963-da24-42fe-8817-10ad43428242" /><item searchable="False" name="WebPartLayoutVersionGUID" tokenized="True" content="True" id="239d8670-bbd6-4a23-9fbb-ec2704096392" /><item searchable="True" name="WebPartLayoutWebPartID" tokenized="False" content="False" id="db45543d-e0ae-41a6-9bc0-e1bfda1c9339" /><item searchable="False" name="WebPartLayoutGUID" tokenized="False" content="False" id="82ec7b49-1532-4da5-a188-89e06594bd8e" /><item searchable="True" name="WebPartLayoutLastModified" tokenized="False" content="False" id="a0daf7dc-b5b6-4b50-be60-339fc9fd8741" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1518, N'Ecommerce - Credit event', N'Ecommerce.CreditEvent', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_CustomerCreditHistory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EventID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="EventName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventCreditChange" type="xs:double" />
              <xs:element name="EventDate" type="xs:dateTime" />
              <xs:element name="EventDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EventCustomerID" type="xs:int" />
              <xs:element name="EventCreditGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="EventCreditLastModified" type="xs:dateTime" />
              <xs:element name="EventSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_CustomerCreditHistory" />
      <xs:field xpath="EventID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EventID" fieldcaption="EventID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="632bf090-0a62-4ab4-ad7a-bb5ade15d292" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EventName" fieldcaption="EventName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a24805b1-9e76-48e9-9dd7-f08a536ce86d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EventCreditChange" fieldcaption="EventCreditChange" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="38a6bf91-170b-46fe-b8ec-62cb3f682199" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EventDate" fieldcaption="EventDate" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e839a80a-98e9-4639-badd-24cc7da72eab" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="EventDescription" fieldcaption="EventDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2f7f0148-cf71-4cf0-baae-7d43a0a26f54" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EventCustomerID" fieldcaption="EventCustomerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fec1ab02-ae4e-40d3-8a6f-a6862a2e7e1a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EventCreditGUID" fieldcaption="EventCreditGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a62b7d78-29e6-4ae5-affc-a4b81512272e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EventCreditLastModified" fieldcaption="EventCreditLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="198520a6-2159-459b-88d8-6c02fc8e538e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="EventSiteID" fieldcaption="EventSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b0d1e6bd-ab80-44ba-af2a-3dc6151f5d85" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_CustomerCreditHistory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110412 08:04:09', 'b27d0a43-6e82-4dce-819e-940363aba30e', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1527, N'MetaFile', N'cms.metafile', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_MetaFile">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MetaFileID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MetaFileObjectID" type="xs:int" />
              <xs:element name="MetaFileObjectType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileGroupName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileSize" type="xs:int" />
              <xs:element name="MetaFileMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileBinary" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="MetaFileImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="MetaFileImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="MetaFileGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MetaFileLastModified" type="xs:dateTime" />
              <xs:element name="MetaFileSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="MetaFileTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MetaFileCustomData" minOccurs="0">
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
      <xs:selector xpath=".//CMS_MetaFile" />
      <xs:field xpath="MetaFileID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MetaFileID" fieldcaption="MetaFileID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="35d65b34-c36f-4378-baca-c732dc160c11" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MetaFileObjectID" fieldcaption="MetaFileObjectID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="15e2572b-61cd-4cca-a8fa-f82c11c60272" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileObjectType" fieldcaption="MetaFileObjectType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c6880452-cf2a-4dc7-a35d-17842b554428" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileGroupName" fieldcaption="MetaFileGroupName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4ea15bca-6d35-40b5-b81d-b6fe1f7cb2ec" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileName" fieldcaption="MetaFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d80f5285-6c32-482e-b5c7-e83c60bdd66e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileExtension" fieldcaption="MetaFileExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7cc08f1f-f483-4fb3-84ef-4c0192c6dbe2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileSize" fieldcaption="MetaFileSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="825039bf-d2cc-41dc-b805-52a3935baee7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileMimeType" fieldcaption="MetaFileMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a2a2b992-e52f-4429-a0ab-948b69c2dc24" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileBinary" fieldcaption="MetaFileBinary" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1f0a6168-494b-4a70-b0c0-1903995e5a76" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileImageWidth" fieldcaption="MetaFileImageWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ce204c6c-ffad-495b-a3f7-ac7252863b63" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileImageHeight" fieldcaption="MetaFileImageHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="883493f1-5fbd-42d9-aed7-92438680c0b8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileGUID" fieldcaption="MetaFileGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="101bf156-b48c-4fa2-ad40-30daf0303393" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="MetaFileLastModified" fieldcaption="MetaFileLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c1745a03-c620-4ebc-80b2-21f5ce2a85c4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="MetaFileSiteID" fieldcaption="MetaFileSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="63caffdb-de78-4f1a-aef2-fa565194f36d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileTitle" fieldcaption="MetaFileTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="2be079de-79c4-409c-a45d-c829f3ad4699" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MetaFileDescription" fieldcaption="MetaFileDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb1417e6-576a-4fdf-bd4c-27c48f76e7cf" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field><field column="MetaFileCustomData" fieldcaption="MetaFileCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="92251b55-a189-482d-886c-03613c3d086d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><IsTextArea>True</IsTextArea></settings></field></form>', N'', N'', N'', N'CMS_MetaFile', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110314 10:46:21', '4b42d5a7-a5c9-4804-a25a-0aaee71ba138', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1593, N'Messaging - Message', N'Messaging.Message', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Messaging_Message">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MessageID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MessageSenderUserID" type="xs:int" minOccurs="0" />
              <xs:element name="MessageSenderNickName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageRecipientUserID" type="xs:int" minOccurs="0" />
              <xs:element name="MessageRecipientNickName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageSent" type="xs:dateTime" />
              <xs:element name="MessageSubject" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageBody">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageRead" type="xs:dateTime" minOccurs="0" />
              <xs:element name="MessageSenderDeleted" type="xs:boolean" minOccurs="0" />
              <xs:element name="MessageRecipientDeleted" type="xs:boolean" minOccurs="0" />
              <xs:element name="MessageGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MessageLastModified" type="xs:dateTime" />
              <xs:element name="MessageIsRead" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Messaging_Message" />
      <xs:field xpath="MessageID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MessageID" fieldcaption="MessageID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d10cab69-17aa-460a-9e46-55b1ecc29c63" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="MessageSenderUserID" fieldcaption="MessageSenderUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="09e1e4d8-9f62-4178-8bd1-78499f7a6a1e" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MessageSenderNickName" fieldcaption="MessageSenderNickName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="868f3d19-4631-46b6-85c9-cc2ab3a13e5d" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MessageRecipientUserID" fieldcaption="MessageRecipientUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="713b6789-5e57-4603-9d09-faf5c138437a" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MessageRecipientNickName" fieldcaption="MessageRecipientNickName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8a55702c-e2e2-4682-aa48-6bc39c3a55c5" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MessageSent" fieldcaption="MessageSent" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6921597c-e3f2-4120-880d-5e0f4d5035bf" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="MessageSubject" fieldcaption="MessageSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="35ee1d32-ddc1-4e34-9d00-f5f564b01eec" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="MessageBody" fieldcaption="MessageBody" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d74f1f55-8c02-40a9-9661-744de1dc64b0" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="MessageRead" fieldcaption="MessageRead" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="30db0e2f-1ca1-4d33-a7fd-6bd61e8b42d4" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="MessageSenderDeleted" fieldcaption="MessageSenderDeleted" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3fee4bc2-4467-4951-a6c8-ec886766735d" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="MessageRecipientDeleted" fieldcaption="MessageRecipientDeleted" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01b7b11d-d0d3-4fbe-8683-e710d22a4682" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="MessageGUID" fieldcaption="MessageGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5279f37b-0f30-435d-a1c7-2359dd4966eb" ismacro="false" hasdependingfields="false"><settings><controlname>unknown</controlname></settings></field><field column="MessageLastModified" fieldcaption="MessageLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e9808b50-db94-45ce-a5dd-c0feeae50c38" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="MessageIsRead" fieldcaption="MessageIsRead" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="bbbcb0b7-171d-4be4-800a-e05200ca50dd" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Messaging_Message', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110407 09:50:51', '79e25187-258a-4c42-8849-da3dc3a03ccc', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1594, N'Object relationship', N'CMS.ObjectRelationship', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ObjectRelationship">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="RelationshipLeftObjectID" type="xs:int" />
              <xs:element name="RelationshipLeftObjectType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="RelationshipNameID" type="xs:int" />
              <xs:element name="RelationshipRightObjectID" type="xs:int" />
              <xs:element name="RelationshipRightObjectType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
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
      <xs:selector xpath=".//CMS_ObjectRelationship" />
      <xs:field xpath="RelationshipLeftObjectID" />
      <xs:field xpath="RelationshipLeftObjectType" />
      <xs:field xpath="RelationshipNameID" />
      <xs:field xpath="RelationshipRightObjectID" />
      <xs:field xpath="RelationshipRightObjectType" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="RelationshipLeftObjectID" fieldcaption="RelationshipLeftObjectID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="cd870be4-6274-4a42-9fa6-19a0a4cfef57" /><field column="RelationshipLeftObjectType" fieldcaption="RelationshipLeftObjectType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="71716bc9-57a9-4553-9ffd-51debb299ea2" /><field column="RelationshipNameID" fieldcaption="RelationshipNameID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="696e81f8-3ed5-4ee5-ae5b-11e9d0d2d46c" /><field column="RelationshipRightObjectID" fieldcaption="RelationshipRightObjectID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0e5b653d-88a9-41d1-8d3a-406a9b6ce23d" /><field column="RelationshipRightObjectType" fieldcaption="RelationshipRightObjectType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c11228f-45a6-4cde-8afe-7221a6da741f" /><field column="RelationshipCustomData" fieldcaption="RelationshipCustomData" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="51d01890-8e3c-48c5-b9b0-aec0fe3c550e" /></form>', N'', N'', N'', N'CMS_ObjectRelationship', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:55:23', '6e6c9ed8-b600-48d7-8e71-88886c6a0470', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1596, N'Ecommerce - Option category', N'ecommerce.optioncategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_OptionCategory">
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
              <xs:element name="CategoryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategorySelectionType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryDefaultOptions" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryDefaultRecord">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryEnabled" type="xs:boolean" />
              <xs:element name="CategoryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CategoryLastModified" type="xs:dateTime" />
              <xs:element name="CategoryDisplayPrice" type="xs:boolean" minOccurs="0" />
              <xs:element name="CategorySiteID" type="xs:int" minOccurs="0" />
              <xs:element name="CategoryTextMaxLength" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_OptionCategory" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="2a21007d-55e7-447e-ac5b-891f3d50e815" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CategoryDisplayName" fieldcaption="CategoryDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="23e8b92a-f166-486a-b0df-67da57a305b2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryName" fieldcaption="CategoryName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="d02ea635-6450-4362-89a2-dac7d42da241" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategorySelectionType" fieldcaption="CategorySelectionType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="10aa4111-ca00-4787-8a56-e1723a49cb59" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryDefaultOptions" fieldcaption="CategoryDefaultOptions" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="357d2463-d90c-41d8-86fd-3c9528bb3117" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryDescription" fieldcaption="CategoryDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b20c1bd6-4f86-458a-a3b6-8995aec34f53" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="CategoryDefaultRecord" fieldcaption="CategoryDefaultRecord" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="b97ed292-d585-4d99-976c-67684a61fab7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CategoryEnabled" fieldcaption="CategoryEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a2d20f0e-7c4b-4ce1-bfbd-d8d0f36ebd86" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CategoryGUID" fieldcaption="CategoryGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c942aa89-ccdf-4b24-bfc3-4c38ac651109" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CategoryLastModified" fieldcaption="CategoryLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e68a0390-00d2-41e3-819a-eeb7b65d5f0f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="CategoryDisplayPrice" fieldcaption="CategoryDisplayPrice" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f968f678-e508-4770-ba35-9a57d7997a8d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CategorySiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="13b5bdeb-e1ce-460c-81b7-625c1185f589" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="CategoryTextMaxLength" fieldcaption="CategoryID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="827cd36e-1b85-4251-be1c-ae47e5229e18" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_OptionCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110905 16:29:14', 'dda1df37-bc59-4541-a568-c69aec9d93fc', 0, 1, 0, N'', 2, N'CategoryDisplayName', N'CategoryDescription', N'', N'CategoryLastModified', N'<search><item searchable="True" name="CategoryID" tokenized="False" content="False" id="bd60c81f-35b0-44e5-ac8a-041f267bd52a" /><item searchable="False" name="CategoryDisplayName" tokenized="True" content="True" id="94efc589-fbb2-490d-8578-83f17228b408" /><item searchable="False" name="CategoryName" tokenized="True" content="True" id="626950b4-0844-408e-895c-3250786b7e3a" /><item searchable="False" name="CategorySelectionType" tokenized="True" content="True" id="751e5b6e-075c-4faf-b729-9d0be4afd723" /><item searchable="False" name="CategoryDefaultOptions" tokenized="True" content="True" id="26facdc5-a186-4972-8f46-1f65f12afc71" /><item searchable="False" name="CategoryDescription" tokenized="True" content="True" id="5a7fbe54-1db8-4eb8-b12f-9416cf73011e" /><item searchable="False" name="CategoryDefaultRecord" tokenized="True" content="True" id="0308d01e-ac0a-424e-940d-f8d9fd9940e9" /><item searchable="True" name="CategoryEnabled" tokenized="False" content="False" id="c19f1513-0a4f-480e-8044-aa0eea738473" /><item searchable="False" name="CategoryGUID" tokenized="False" content="False" id="0fd9a5e6-bc76-400c-a415-8e71474a48b5" /><item searchable="True" name="CategoryLastModified" tokenized="False" content="False" id="06f46719-aa44-459a-ba02-5daa37c1866f" /><item searchable="True" name="CategoryDisplayPrice" tokenized="False" content="False" id="1c18c570-0418-4fe4-880c-2b8574e11922" /><item searchable="True" name="CategorySiteID" tokenized="False" content="False" id="93941780-a8c4-44ab-9299-238010a733cd" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1606, N'Ecommerce - Shopping cart item', N'ecommerce.shoppingcartitem', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_ShoppingCartSKU">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CartItemID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ShoppingCartID" type="xs:int" />
              <xs:element name="SKUID" type="xs:int" />
              <xs:element name="SKUUnits" type="xs:int" />
              <xs:element name="CartItemCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CartItemGuid" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="CartItemParentGuid" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="CartItemPrice" type="xs:double" minOccurs="0" />
              <xs:element name="CartItemIsPrivate" type="xs:boolean" minOccurs="0" />
              <xs:element name="CartItemValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="CartItemBundleGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="CartItemText" minOccurs="0">
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
      <xs:selector xpath=".//COM_ShoppingCartSKU" />
      <xs:field xpath="CartItemID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CartItemID" fieldcaption="CartItemID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ddd0b393-ea5e-46aa-89eb-16771b8249ed" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ShoppingCartID" fieldcaption="ShoppingCartID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="109dea76-7132-4052-8d95-8247d6dc94bc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUID" fieldcaption="SKUID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8ccf5472-5f5c-4bab-bf88-9256e8dcd515" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUUnits" fieldcaption="SKUUnits" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="233f8ac2-2329-4188-8812-23aa0c4977f6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CartItemCustomData" fieldcaption="CartItemCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c6af4828-879f-4203-8298-373e12011dca" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="CartItemGuid" fieldcaption="CartItemGuid" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ff1bdf4f-ec66-4d98-9e55-26a5608bcff0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CartItemParentGuid" fieldcaption="CartItemParentGuid" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ed4485db-5dcc-4deb-a1c6-f3749f10a89a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="CartItemBundleGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1e524cb7-2e01-47e0-9777-72b346b9096a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="CartItemPrice" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="59583ea0-5c1f-49b4-a91c-f45617cb7a84" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="CartItemIsPrivate" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="517e0915-5d2a-4bd0-a521-5e888010c25c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CartItemValidTo" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="660e66e6-bdef-4a26-b3bf-8273397d21a5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="CartItemText" fieldcaption="CartItemID" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="04f4bf42-57f4-43ae-ac44-27456e08a838" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_ShoppingCartSKU', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110812 15:34:17', '936fda11-e521-4885-be89-a085f440ba4e', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1611, N'Settings key', N'CMS.SettingsKey', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SettingsKey">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="KeyID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="KeyName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyValue" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyCategoryID" type="xs:int" minOccurs="0" />
              <xs:element name="SiteID" type="xs:int" minOccurs="0" />
              <xs:element name="KeyGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="KeyLastModified" type="xs:dateTime" />
              <xs:element name="KeyOrder" type="xs:int" minOccurs="0" />
              <xs:element name="KeyDefaultValue" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyValidation" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="255" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyEditingControlPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="KeyLoadGeneration" type="xs:int" />
              <xs:element name="KeyIsGlobal" type="xs:boolean" minOccurs="0" />
              <xs:element name="KeyIsCustom" type="xs:boolean" minOccurs="0" />
              <xs:element name="KeyIsHidden" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SettingsKey" />
      <xs:field xpath="KeyID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="KeyID" fieldcaption="KeyID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ac7779ce-7c76-40ad-9c17-b489b895b4a9" visibility="none" ismacro="false" /><field column="KeyIsHidden" fieldcaption="KeyIsHidden" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0a45ca6a-d72e-4f9d-bc12-35cd935049d3" visibility="none" ismacro="false" /><field column="KeyIsCustom" fieldcaption="KeyIsCustom" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3332844e-553a-411f-9840-36533960080e" visibility="none" ismacro="false" /><field column="KeyName" fieldcaption="KeyName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="efbde045-fadd-4730-8fa6-162653f115ae" ismacro="false" /><field column="KeyDisplayName" fieldcaption="KeyDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="93a202e2-0a05-4cbc-bb24-4bb746e0161e" ismacro="false" /><field column="KeyDescription" fieldcaption="KeyDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="831a5e01-d23e-497f-a3b4-abb2716d084f" ismacro="false" /><field column="KeyValue" fieldcaption="KeyValue" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cd572f36-fab6-4ad3-8877-a34305ab3048" ismacro="false" /><field column="KeyType" fieldcaption="KeyType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e32d876e-e786-482c-b263-d2a7dcd4215d" ismacro="false" /><field column="KeyCategoryID" fieldcaption="KeyCategoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5bf162ea-b507-4da2-b6a3-bb47f825c2a5" ismacro="false" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="240d094e-b136-4330-a989-ed12135f49a2" visibility="none" ismacro="false" /><field column="KeyGUID" fieldcaption="KeyGUID" visible="true" columntype="file" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6a1e6aa0-7b6a-4c8e-9292-22d58c69f9c1" ismacro="false" /><field column="KeyLastModified" fieldcaption="KeyLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="713fe666-07c5-446b-b544-48a8205d8b31" ismacro="false" /><field column="KeyOrder" fieldcaption="KeyOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="85d56e5e-9514-448c-8a52-618368567044" ismacro="false" /><field column="KeyDefaultValue" fieldcaption="KeyDefaultValue" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8dbf84ce-f792-4223-a7b7-ca7ff1937384" ismacro="false" /><field column="KeyValidation" fieldcaption="KeyValidation" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8e89cb6f-c568-4622-9d7b-2fd551d121d7" ismacro="false" /><field column="KeyLoadGeneration" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6b4d0589-810b-49d1-8cb1-912b27c85df1" ismacro="false" /><field column="KeyIsGlobal" visible="false" defaultvalue="false" columntype="boolean" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8e638167-3952-467a-b074-3f97e18a6a77" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_SettingsKey', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:13:36', 'ec796166-5adf-43fa-9508-c9791db1b6dd', 0, 1, 0, N'', 1, N'KeyDisplayName', N'KeyDescription', N'', N'KeyLastModified', N'<search><item searchable="True" name="KeyID" tokenized="False" content="False" id="ef5d40ef-6c30-49f0-bec8-0838d434ddfa" /><item searchable="True" name="KeyIsHidden" tokenized="False" content="False" id="235a9a34-aedb-47f8-8908-401ac3bd83a8" /><item searchable="True" name="KeyIsCustom" tokenized="False" content="False" id="c53c7c59-bc80-49ee-ad36-a058c5d9c389" /><item searchable="False" name="KeyName" tokenized="True" content="True" id="e7d07185-3c93-49fc-b37d-be57ee9aa31f" /><item searchable="False" name="KeyDisplayName" tokenized="True" content="True" id="ee5906bb-d968-4236-805d-785ae19189c3" /><item searchable="False" name="KeyDescription" tokenized="True" content="True" id="0282aa5f-f2a9-4378-8188-bf543e02fbaf" /><item searchable="False" name="KeyValue" tokenized="True" content="True" id="d7580b81-3642-4f26-94ce-d7e0eb99c894" /><item searchable="False" name="KeyType" tokenized="True" content="True" id="cc0a89f7-3c30-47b9-ba3e-f40c28e9aac1" /><item searchable="True" name="KeyCategoryID" tokenized="False" content="False" id="bb0d854d-40c5-4ebc-a375-761e7ce155ba" /><item searchable="True" name="SiteID" tokenized="False" content="False" id="c614e4fa-0394-45fa-8b48-fe681adfa7d3" /><item searchable="False" name="KeyGUID" tokenized="False" content="False" id="76d1193f-e3ba-4255-a82b-4f5143864f8f" /><item searchable="True" name="KeyLastModified" tokenized="False" content="False" id="c52b93b9-1406-4381-ba08-4547d078bdc1" /><item searchable="True" name="KeyOrder" tokenized="False" content="False" id="b94c34f5-9a83-4a94-9bdc-082ae7edef2c" /><item searchable="False" name="KeyDefaultValue" tokenized="True" content="True" id="0121b5f4-25ed-4fea-af3d-4f7355e78dc3" /><item searchable="False" name="KeyValidation" tokenized="True" content="True" id="8c1fb729-a0a8-44a3-aec3-c72dad4e653d" /><item searchable="True" name="KeyLoadGeneration" tokenized="False" content="False" id="57625a97-8b27-4663-832a-568fcf1144cc" /><item searchable="True" name="KeyIsGlobal" tokenized="False" content="False" id="85d3e944-8bbf-4dec-83c6-f476dbe6b199" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1616, N'Export - history', N'export.history', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Export_History">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ExportID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ExportDateTime" type="xs:dateTime" />
              <xs:element name="ExportFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ExportSiteID" type="xs:int" />
              <xs:element name="ExportUserID" type="xs:int" />
              <xs:element name="ExportSettings" minOccurs="0">
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
      <xs:selector xpath=".//Export_History" />
      <xs:field xpath="ExportID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ExportID" fieldcaption="ExportID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="149cdd85-2558-4c2a-9ffe-6801a4effe1c" /><field column="ExportDateTime" fieldcaption="ExportDateTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f558feb5-f0f3-4cae-8bca-e0f2af7099f6" /><field column="ExportFileName" fieldcaption="ExportFileName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0377b17d-6321-4169-a4b0-a34a4e683a6c" /><field column="ExportSiteID" fieldcaption="ExportSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="da436881-5b56-45a2-bea4-3e4a8bd2d36c" /><field column="ExportUserID" fieldcaption="ExportUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b78c3e62-84bd-4068-b18c-6b79fdaba365" /><field column="ExportSettings" fieldcaption="ExportSettings" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="78ffe32e-8ea4-4cea-a3ef-e475a55dfc96" /></form>', N'', N'', N'', N'Export_History', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:12:43', 'ab5857eb-879b-422c-82ba-0acc49df79a2', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1617, N'Export - task', N'Export.Task', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Export_Task">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaskID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaskSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="TaskTitle">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskData">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskTime" type="xs:dateTime" />
              <xs:element name="TaskType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskObjectType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaskObjectID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Export_Task" />
      <xs:field xpath="TaskID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaskID" fieldcaption="TaskID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="63d312c3-c5a7-43ff-8574-84125264f844" /><field column="TaskSiteID" fieldcaption="TaskSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8c5930e5-9b1c-45f1-9ad9-c35581eed7c7" /><field column="TaskTitle" fieldcaption="TaskTitle" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="15588e8b-2e40-4481-a023-91d383fc7761" /><field column="TaskData" fieldcaption="TaskData" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fdc52e2c-fadd-4090-b576-5d22596feb75" /><field column="TaskTime" fieldcaption="TaskTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="928995c4-1a63-4615-95b8-18d7ef928ee6" /><field column="TaskType" fieldcaption="TaskType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d533781a-cdde-43b8-aadb-058cf9d3d476" /><field column="TaskObjectType" fieldcaption="TaskObjectType" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9d58620e-cbc1-4db1-a17f-7c4920989286" /><field column="TaskObjectID" fieldcaption="TaskObjectID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e5f956e9-cfbe-4668-8143-afe634d44d76" /></form>', N'', N'', N'', N'Export_Task', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:12:49', '5038ed34-d6c2-4598-b10b-d51cb6f8945b', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1629, N'CSS stylesheet site', N'CMS.CSSStylesheetSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_CssStylesheetSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StylesheetID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_CssStylesheetSite" />
      <xs:field xpath="StylesheetID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StylesheetID" fieldcaption="StylesheetID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="fcd83ef9-440c-4e12-a296-e397203420e5" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="67a1ad4c-a0be-4254-9bb9-ced08b9f7bfc" /></form>', N'', N'', N'', N'CMS_CssStylesheetSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081231 14:54:23', 'f5c63ca9-1ac4-4d41-8977-d129c00d9019', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1630, N'InlineControlSite', N'CMS.InlineControlSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_InlineControlSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ControlID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_InlineControlSite" />
      <xs:field xpath="ControlID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ControlID" fieldcaption="ControlID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="6bd20653-e9ac-448a-8dd8-16b0dd778ae0" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f95ecd72-a16b-4293-adac-3972773154a9" /></form>', N'', N'', N'', N'CMS_InlineControlSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:09', 'abff72a6-3558-4bba-bb86-7117e9ca8525', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1631, N'PageTemplateSite', N'CMS.PageTemplateSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_PageTemplateSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PageTemplateID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_PageTemplateSite" />
      <xs:field xpath="PageTemplateID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PageTemplateID" fieldcaption="PageTemplateID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b03c6f39-3d55-477b-8b7e-dffd2f64fba3" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9c557e72-06ed-423b-aa10-09a543d97976" /></form>', N'', N'', N'', N'CMS_PageTemplateSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:15', '64b959d5-ca01-4aab-a467-ad473346e040', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1632, N'ResourceSite', N'CMS.ResourceSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ResourceSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ResourceID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ResourceSite" />
      <xs:field xpath="ResourceID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ResourceID" fieldcaption="ResourceID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e1cca274-6aa7-4034-86b9-5ee02af2952b" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="436efd78-8b1d-49cd-b073-4024a9c69b0f" /></form>', N'', N'', N'', N'CMS_ResourceSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:22', 'b97b7997-ea51-4c6e-898a-3091106ba5ad', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1633, N'Culture site', N'CMS.CultureSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_SiteCulture">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SiteID" type="xs:int" />
              <xs:element name="CultureID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_SiteCulture" />
      <xs:field xpath="SiteID" />
      <xs:field xpath="CultureID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="759423f2-9e97-4a8d-b32b-657a669f1014" /><field column="CultureID" fieldcaption="CultureID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3e2f0e62-9076-4d68-be90-3898671e094c" /></form>', N'', N'', N'', N'CMS_SiteCulture', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081231 14:52:18', '2b81bbf8-bf34-465b-ae17-32089398d076', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1634, N'UserSite', N'CMS.UserSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_UserSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserSiteID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
              <xs:element name="UserPreferredCurrencyID" type="xs:int" minOccurs="0" />
              <xs:element name="UserPreferredShippingOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="UserPreferredPaymentOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="UserDiscountLevelID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_UserSite" />
      <xs:field xpath="UserSiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserSiteID" fieldcaption="UserSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="82d6b7e6-fac7-4e43-9f7a-4cb8801d7190" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e470c61e-0451-4341-b811-6ef885da1b5a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5e78e5d-f413-4b81-8d7b-d3e1c17125f2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserPreferredCurrencyID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b2234be-3087-48cf-a4dd-e469d016d66b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="UserPreferredShippingOptionID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a7b6b980-c163-49b6-9ea5-e84729a469ca" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="UserPreferredPaymentOptionID" fieldcaption="UserID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7f08afdf-a720-4aa8-a2af-69e947984738" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserDiscountLevelID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="677c6e9a-4fb2-4cda-bab4-fd1639e8a5e1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_UserSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110427 10:01:31', 'f41cb287-9535-4707-a557-0e2806a9f682', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1635, N'WorkflowStepRole', N'CMS.WorkflowStepRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_WorkflowStepRoles">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StepID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_WorkflowStepRoles" />
      <xs:field xpath="StepID" />
      <xs:field xpath="RoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StepID" fieldcaption="StepID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b3b646b4-a9da-47de-9a80-2afa11060e74" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="76e118eb-ac03-40a2-8551-36079bbecde7" /></form>', N'', N'', N'', N'CMS_WorkflowStepRoles', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:41', '6b32cc52-df22-44f5-a0e8-1a227f31689d', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1636, N'Ecommerce - Discount level department', N'Ecommerce.DiscountLevelDepartment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_DiscountLevelDepartment">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DiscountLevelID" type="xs:int" />
              <xs:element name="DepartmentID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_DiscountLevelDepartment" />
      <xs:field xpath="DiscountLevelID" />
      <xs:field xpath="DepartmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DiscountLevelID" fieldcaption="DiscountLevelID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="c3d48dbf-6cfc-4108-8c2a-9b858772c219" /><field column="DepartmentID" fieldcaption="DepartmentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="3366ade1-3d2b-45fb-abb8-f18ff869213a" /></form>', N'', N'', N'', N'COM_DiscountLevelDepartment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110211 12:49:24', '61481a6d-d6ee-4076-acee-a7dea19d36ce', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1637, N'Class site', N'CMS.ClassSite', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_ClassSite">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ClassID" type="xs:int" />
              <xs:element name="SiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_ClassSite" />
      <xs:field xpath="ClassID" />
      <xs:field xpath="SiteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ClassID" fieldcaption="ClassID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="73477d3f-d6cf-4cb5-aba9-dc0b674c7c42" /><field column="SiteID" fieldcaption="SiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1129040b-8547-4e60-9b8c-f7c86ca21ec5" /></form>', N'', N'', N'', N'CMS_ClassSite', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081231 14:53:54', 'e00509ca-65d4-4429-a51b-2db9e8e477cb', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1638, N'FormRole', N'cms.FormRole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_FormRole">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FormID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_FormRole" />
      <xs:field xpath="FormID" />
      <xs:field xpath="RoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FormID" fieldcaption="FormID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ab620239-2fc4-41e5-b46a-bb9737b59d15" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="61315092-d101-4af5-8b07-c712828c49c4" /></form>', N'', N'', N'', N'CMS_FormRole', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:07', 'c6fdeded-624d-46a0-99a7-910cd0eba763', 0, 1, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1639, N'Allowed child class', N'CMS.AllowedChildClass', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_AllowedChildClasses">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ParentClassID" type="xs:int" />
              <xs:element name="ChildClassID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_AllowedChildClasses" />
      <xs:field xpath="ParentClassID" />
      <xs:field xpath="ChildClassID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ParentClassID" fieldcaption="ParentClassID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="577c16d3-07c7-4612-8024-4fb1491ebebf" /><field column="ChildClassID" fieldcaption="ChildClassID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5fe152b9-2296-422d-a674-4ffb00156f1c" /></form>', N'', N'', N'', N'CMS_AllowedChildClasses', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110407 11:04:40', '8ddaca20-37ad-4b93-8230-e38fbde8d935', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1640, N'Ecommerce - SKU discount coupon', N'ecommerce.SKUDiscountCoupon', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_SKUDiscountCoupon">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SKUID" type="xs:int" />
              <xs:element name="DiscountCouponID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_SKUDiscountCoupon" />
      <xs:field xpath="SKUID" />
      <xs:field xpath="DiscountCouponID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SKUID" fieldcaption="SKUID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="bf9798d9-5cad-429f-823b-b4e7b3845cf0" /><field column="DiscountCouponID" fieldcaption="DiscountCouponID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="96c52230-0ef8-4c58-bffb-7ee46bc74eb8" /></form>', N'', N'', N'', N'COM_SKUDiscountCoupon', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110324 14:48:29', 'fafd7adc-496f-4f3c-a4a3-b51e1fc9ab02', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1641, N'Ecommerce - SKU option category', N'Ecommerce.SKUOptionCategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_SKUOptionCategory">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SKUID" type="xs:int" />
              <xs:element name="CategoryID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_SKUOptionCategory" />
      <xs:field xpath="SKUID" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SKUID" fieldcaption="SKUID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f7309204-9d30-4553-adfc-ae733d99bfd7" /><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="eecc79bf-88bb-41b0-a9b6-182a73f7b930" /></form>', N'', N'', N'', N'COM_SKUOptionCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110315 08:33:19', '619ff287-c627-44dd-bf36-786d605e633b', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1642, N'Ecommerce - SKU tax class', N'Ecommerce.SKUTaxClass', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_SKUTaxClasses">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SKUID" type="xs:int" />
              <xs:element name="TaxClassID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_SKUTaxClasses" />
      <xs:field xpath="SKUID" />
      <xs:field xpath="TaxClassID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SKUID" fieldcaption="SKUID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="42f04d31-3a4d-46c5-9336-6b45abc2cd1a" /><field column="TaxClassID" fieldcaption="TaxClassID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ad2e17d3-499c-4aca-82cc-1cd012e64dbc" /></form>', N'', N'', N'', N'COM_SKUTaxClasses', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110315 08:33:30', '624d4c22-1961-44fc-b047-bab2073fa9dc', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [CMS_Class] OFF
