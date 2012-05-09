SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1121, N'ForumGroup', N'Forums.ForumGroup', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_ForumGroup">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="GroupID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="GroupSiteID" type="xs:int" />
              <xs:element name="GroupName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupOrder" type="xs:int" minOccurs="0" />
              <xs:element name="GroupDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="GroupLastModified" type="xs:dateTime" />
              <xs:element name="GroupBaseUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupUnsubscriptionUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GroupGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="GroupAuthorEdit" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupAuthorDelete" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupType" type="xs:int" minOccurs="0" />
              <xs:element name="GroupIsAnswerLimit" type="xs:int" minOccurs="0" />
              <xs:element name="GroupImageMaxSideSize" type="xs:int" minOccurs="0" />
              <xs:element name="GroupDisplayEmails" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupRequireEmail" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupHTMLEditor" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupUseCAPTCHA" type="xs:boolean" minOccurs="0" />
              <xs:element name="GroupAttachmentMaxFileSize" type="xs:int" minOccurs="0" />
              <xs:element name="GroupDiscussionActions" type="xs:int" minOccurs="0" />
              <xs:element name="GroupLogActivity" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_ForumGroup" />
      <xs:field xpath="GroupID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="GroupID" fieldcaption="GroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="50672b5d-28e7-4921-bd0e-531d708afbaa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GroupSiteID" fieldcaption="GroupSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43e2efce-a5a9-4a0d-b6b4-64e44431bb89" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupName" fieldcaption="GroupName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="39177fe2-d6d5-47ef-80b4-f40c81543e7f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupDisplayName" fieldcaption="GroupDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="992f03b3-2866-48b1-ad27-7f4d60346a4c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupOrder" fieldcaption="GroupOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c72aa204-d1e2-452f-b210-6b5abadbe7e0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupDescription" fieldcaption="GroupDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eb32d397-4c3c-4dc6-a2a5-613980598049" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="GroupGUID" fieldcaption="GroupGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e752f0f3-3a4a-401a-b10f-077c2ae9baa8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GroupLastModified" fieldcaption="GroupLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3fe0188c-52cc-44aa-8eee-a152d8ae2a0b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="GroupBaseUrl" fieldcaption="GroupBaseUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="c244605b-0c2b-4c2e-b9dc-cd07122c911b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupUnsubscriptionUrl" fieldcaption="GroupUnsubscriptionUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="026dd06a-ec26-425d-be05-ec62ca2d63f3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupGroupID" fieldcaption="GroupGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ddb4b979-4274-4305-95b5-c2f17aff74a9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupAuthorEdit" fieldcaption="GroupAuthorEdit" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="90fff474-5d5f-4c9b-a373-86aa3e3179c8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupAuthorDelete" fieldcaption="GroupAuthorDelete" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="52355416-f982-40e5-b0df-d335d75527d0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupType" fieldcaption="GroupType" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e3ec2d3d-65f4-430a-8447-3503ce4a9423" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupIsAnswerLimit" fieldcaption="GroupIsAnswerLimit" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="16dd655d-8417-45fa-82d2-15ffc7166c73" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupImageMaxSideSize" fieldcaption="GroupImageMaxSideSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f8629a16-3009-4650-9a5a-3bcc3503f9cd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupDisplayEmails" fieldcaption="GroupDisplayEmails" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="68f84af3-38cf-40e7-93a3-69d95945aafa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupRequireEmail" fieldcaption="GroupRequireEmail" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="79eaf33a-37e4-498c-8691-b3af44f0d0dc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupHTMLEditor" fieldcaption="GroupHTMLEditor" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f4bddb6-ef29-4a93-b7c0-1e9e3b504245" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupUseCAPTCHA" fieldcaption="GroupUseCAPTCHA" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2300337e-0edc-4adf-94c7-5867e9b39c02" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GroupAttachmentMaxFileSize" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e00eb00-2368-45ab-bd0e-e9af4652d095" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GroupDiscussionActions" fieldcaption="GroupDiscussionActions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a156aa0a-4df4-4efb-b7c6-69e5307d9985" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GroupLogActivity" fieldcaption="GroupLogActivity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69823a96-a2f9-4882-bf65-17ec73836376" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Forums_ForumGroup', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110607 09:08:36', 'ae01ac82-45ae-441f-85e3-fda3e45ef85f', 0, 1, 0, N'', 2, N'GroupDisplayName', N'GroupDescription', N'', N'GroupLastModified', N'<search><item searchable="True" name="GroupID" tokenized="False" content="False" id="adb087db-a484-4eb0-afb6-96c2b5e2800d" /><item searchable="True" name="GroupSiteID" tokenized="False" content="False" id="6da975be-8426-46c2-a639-662fd1fcfac1" /><item searchable="False" name="GroupName" tokenized="True" content="True" id="2cde2415-8942-4857-911c-082188d300d1" /><item searchable="False" name="GroupDisplayName" tokenized="True" content="True" id="47b7b8fd-a647-4870-8371-4bcbc6d324ac" /><item searchable="True" name="GroupOrder" tokenized="False" content="False" id="41b4b806-5640-4cdc-8435-1902d104c3e4" /><item searchable="False" name="GroupDescription" tokenized="True" content="True" id="794311ad-4af9-4238-beda-7e63f29964ad" /><item searchable="False" name="GroupGUID" tokenized="False" content="False" id="175331c7-3250-4ae9-87b8-96f7009a419c" /><item searchable="True" name="GroupLastModified" tokenized="False" content="False" id="1f70d6fc-8a66-4267-98b4-da2940a18007" /><item searchable="False" name="GroupBaseUrl" tokenized="True" content="True" id="5c1a7151-58dd-4c8b-9dfa-fedb5cb12d57" /><item searchable="False" name="GroupUnsubscriptionUrl" tokenized="True" content="True" id="33569da7-af6a-474c-833c-f89b62b72308" /><item searchable="True" name="GroupGroupID" tokenized="False" content="False" id="a150cd73-13ac-4e37-b19e-5df198daa6d1" /><item searchable="True" name="GroupAuthorEdit" tokenized="False" content="False" id="50354451-7029-4df0-895f-80eaa5065e7e" /><item searchable="True" name="GroupAuthorDelete" tokenized="False" content="False" id="dc1316b7-48a3-444e-871b-df868b43ca88" /><item searchable="True" name="GroupType" tokenized="False" content="False" id="88f7d958-d7e6-4a87-8519-bdc1f63f7490" /><item searchable="True" name="GroupIsAnswerLimit" tokenized="False" content="False" id="259533ec-7b8d-4139-9b35-99a832d0f7aa" /><item searchable="True" name="GroupImageMaxSideSize" tokenized="False" content="False" id="2e1ea674-dfeb-42fc-88fb-4e21b0237385" /><item searchable="True" name="GroupDisplayEmails" tokenized="False" content="False" id="6379f97c-7003-4c22-b054-fca0aeebed34" /><item searchable="True" name="GroupRequireEmail" tokenized="False" content="False" id="b6991f63-7d91-484a-9e90-26bc5446c818" /><item searchable="True" name="GroupHTMLEditor" tokenized="False" content="False" id="a8df2d5b-3555-409a-b1bc-9fed8f1d3ef3" /><item searchable="True" name="GroupUseCAPTCHA" tokenized="False" content="False" id="594bd775-eb56-4f97-bdf8-cdb6918e5067" /><item searchable="True" name="GroupAttachmentMaxFileSize" tokenized="False" content="False" id="7c4132ee-9145-4ac8-a9d9-50d308c16b95" /><item searchable="True" name="GroupDiscussionActions" tokenized="False" content="False" id="aaab860f-f50c-4dcc-b548-6590effbd2c6" /></search>', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1122, N'Forum', N'Forums.Forum', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_Forum">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ForumID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ForumGroupID" type="xs:int" />
              <xs:element name="ForumName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumOrder" type="xs:int" minOccurs="0" />
              <xs:element name="ForumDocumentID" type="xs:int" minOccurs="0" />
              <xs:element name="ForumOpen" type="xs:boolean" />
              <xs:element name="ForumModerated" type="xs:boolean" />
              <xs:element name="ForumDisplayEmails" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumRequireEmail" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumAccess" type="xs:int" />
              <xs:element name="ForumThreads" type="xs:int" />
              <xs:element name="ForumPosts" type="xs:int" />
              <xs:element name="ForumLastPostTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ForumLastPostUserName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumBaseUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumAllowChangeName" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumHTMLEditor" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumUseCAPTCHA" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ForumLastModified" type="xs:dateTime" />
              <xs:element name="ForumUnsubscriptionUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumIsLocked" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumSettings" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumAuthorEdit" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumAuthorDelete" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumType" type="xs:int" minOccurs="0" />
              <xs:element name="ForumIsAnswerLimit" type="xs:int" minOccurs="0" />
              <xs:element name="ForumImageMaxSideSize" type="xs:int" minOccurs="0" />
              <xs:element name="ForumLastPostTimeAbsolute" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ForumLastPostUserNameAbsolute" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ForumPostsAbsolute" type="xs:int" minOccurs="0" />
              <xs:element name="ForumThreadsAbsolute" type="xs:int" minOccurs="0" />
              <xs:element name="ForumAttachmentMaxFileSize" type="xs:int" minOccurs="0" />
              <xs:element name="ForumDiscussionActions" type="xs:int" minOccurs="0" />
              <xs:element name="ForumSiteID" type="xs:int" />
              <xs:element name="ForumLogActivity" type="xs:boolean" minOccurs="0" />
              <xs:element name="ForumCommunityGroupID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_Forum" />
      <xs:field xpath="ForumID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ForumID" fieldcaption="ForumID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a5a72580-8e59-4c28-9ed1-68260005d5ec" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ForumGroupID" fieldcaption="ForumGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="da47afe9-4cee-4122-a356-fdf161909434" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumName" fieldcaption="ForumName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="d44035ca-f757-4b21-ab86-2496940def23" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumDisplayName" fieldcaption="ForumDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="cbcfba2f-7033-418e-a983-207674c0b40c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumDescription" fieldcaption="ForumDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="80033e42-d650-4b5e-87fe-1ee020efc671" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ForumOrder" fieldcaption="ForumOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1ace9535-c2d2-4bc9-90cc-a777926a7190" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumDocumentID" fieldcaption="ForumDocumentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f5d483c-a9d4-4be0-b997-3cf8172c434a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumOpen" fieldcaption="ForumOpen" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29d695a8-f165-44c0-977c-bfe1952e4864" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumModerated" fieldcaption="ForumModerated" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2a84972b-9988-4560-8fd9-bd60fcd05e76" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumDisplayEmails" fieldcaption="ForumDisplayEmails" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="401fe8d8-bd7a-4671-aa42-9188b881b9af" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumRequireEmail" fieldcaption="ForumRequireEmail" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="036c72c8-c97b-41d8-9f15-a3550a2c8dd7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumAccess" fieldcaption="ForumAccess" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4e1fe9df-0be3-41ab-8418-45481e35d136" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumThreads" fieldcaption="ForumThreads" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="56fa4909-066d-4135-b56f-4c2e751d57c3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumPosts" fieldcaption="ForumPosts" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e866e09f-15cc-43e2-b16c-31c643ddf8e6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumLastPostTime" fieldcaption="ForumLastPostTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="50bf7d4f-258a-4f24-8bed-9272753c7b5a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ForumLastPostUserName" fieldcaption="ForumLastPostUserName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="68e87a3c-eb53-4f1e-aa59-0dde3dba94b0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumBaseUrl" fieldcaption="ForumBaseUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="f2789b2b-0187-45a3-b51d-ebd2b01b58fa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumAllowChangeName" fieldcaption="ForumAllowChangeName" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1b4cee6-4eff-41a6-9925-8167361bfbb5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumHTMLEditor" fieldcaption="ForumHTMLEditor" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fa99d5b2-81da-4d20-988a-84fd8dfeb08f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumUseCAPTCHA" fieldcaption="ForumUseCAPTCHA" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="efc0612a-7161-42e5-b4e9-77fbd182b2e8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumGUID" fieldcaption="ForumGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="626e3139-da8d-4d78-8d0b-217a5ba4c66d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ForumLastModified" fieldcaption="ForumLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e6af3cf1-871c-434d-a8ea-ca33c21bb372" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ForumUnsubscriptionUrl" fieldcaption="ForumUnsubscriptionUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="0a317c54-44e3-4f35-8762-9b83d2bb64dc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumIsLocked" fieldcaption="ForumIsLocked" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1e79145e-05e7-45f6-bddb-d3040108b386" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumSettings" fieldcaption="ForumSettings" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6460cdfc-8e5c-4286-b5ba-5b40f1ca6e6e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ForumAuthorEdit" fieldcaption="ForumAuthorEdit" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7128e991-75dd-4730-9a81-ab995016d9b7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumAuthorDelete" fieldcaption="ForumAuthorDelete" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0c03904c-0369-4ff4-ae26-603433572553" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumType" fieldcaption="ForumType" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="65c15999-91c6-48ed-9b85-58fbd76e2168" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumIsAnswerLimit" fieldcaption="ForumIsAnswerLimit" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="71f079d1-33ad-43ba-9803-f05d121ce042" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumImageMaxSideSize" fieldcaption="ForumImageMaxSideSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d9dd0e96-655b-4bdd-9358-92bbb755eeb8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumLastPostTimeAbsolute" fieldcaption="ForumLastPostTimeAbsolute" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ff990f14-94d5-4a78-ae1a-e0a462d689d7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ForumLastPostUserNameAbsolute" fieldcaption="ForumLastPostUserNameAbsolute" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="10473eee-e635-4929-9a58-d06f4940114f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumPostsAbsolute" fieldcaption="ForumPostsAbsolute" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4bd75ccf-9dfc-426d-8ce8-bcbe4f7638a0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumThreadsAbsolute" fieldcaption="ForumThreadsAbsolute" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b86bebb0-db09-421e-92d3-fe5acb10838f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumAttachmentMaxFileSize" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d4909e25-1604-4dd6-89f5-3a44f697b9c3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumDiscussionActions" fieldcaption="ForumDiscussionActions" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b7fa1080-90f2-4f08-a7d2-75417dea8550" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ForumSiteID" fieldcaption="Forum site ID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9dd53caf-bf89-4ec7-92a9-3414595e9649" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ForumLogActivity" fieldcaption="Log on-line marketing activity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0959e7dd-7f2b-41e8-8036-38d64a70837f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ForumCommunityGroupID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b6cce314-15bf-43c9-b1f6-51a8323c46e2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'Forums_Forum', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110915 14:36:15', '32a7f4fb-dfa5-4394-a120-6af690f0f2e2', 0, 1, 0, N'', 2, N'ForumDisplayName', N'ForumDescription', N'', N'ForumLastModified', N'<search><item searchable="True" name="ForumID" tokenized="False" content="False" id="2a8eeb34-9411-4e4e-909f-19924c223e62" /><item searchable="True" name="ForumGroupID" tokenized="False" content="False" id="5c34e78b-a11b-43c5-beac-4349a11030f2" /><item searchable="False" name="ForumName" tokenized="True" content="True" id="eaad5cb4-c866-4586-b9f1-d2b3e9ab4225" /><item searchable="False" name="ForumDisplayName" tokenized="True" content="True" id="0e3ef579-0fdc-4789-9972-041f6f686d8f" /><item searchable="False" name="ForumDescription" tokenized="True" content="True" id="bdb0affe-16a1-4861-bb99-cc9c77906ba1" /><item searchable="True" name="ForumOrder" tokenized="False" content="False" id="6e0b1d37-80cf-412f-a785-5a1281a99c54" /><item searchable="True" name="ForumDocumentID" tokenized="False" content="False" id="373803d4-e307-4d23-9df7-df8893d3e31e" /><item searchable="True" name="ForumOpen" tokenized="False" content="False" id="06e3295f-3304-4e79-bde0-89b3ff86cae0" /><item searchable="True" name="ForumModerated" tokenized="False" content="False" id="42917067-5437-4e3c-879b-53125822ee6b" /><item searchable="True" name="ForumDisplayEmails" tokenized="False" content="False" id="bd34fef9-191f-4043-bb4c-823460f19521" /><item searchable="True" name="ForumRequireEmail" tokenized="False" content="False" id="db26ca93-2a0a-46de-ab41-8d53e2cfb290" /><item searchable="True" name="ForumAccess" tokenized="False" content="False" id="79a6c12c-656a-4e22-9d38-a38dff198d4d" /><item searchable="True" name="ForumThreads" tokenized="False" content="False" id="54d3fe2a-e6f7-42c0-8c5e-a645fcb9d0c2" /><item searchable="True" name="ForumPosts" tokenized="False" content="False" id="e599c192-6c97-454e-b247-8190c0bb757b" /><item searchable="True" name="ForumLastPostTime" tokenized="False" content="False" id="82cecd2f-15de-4f60-b494-089f1046f0ec" /><item searchable="False" name="ForumLastPostUserName" tokenized="True" content="True" id="5e1ea68c-12dc-412a-9d9d-3456b166ec7f" /><item searchable="False" name="ForumBaseUrl" tokenized="True" content="True" id="810d7caf-6e36-45f6-b94c-b2cd3330c2a1" /><item searchable="True" name="ForumAllowChangeName" tokenized="False" content="False" id="4ba5650a-6442-40e3-a668-0f18c7cf3f58" /><item searchable="True" name="ForumHTMLEditor" tokenized="False" content="False" id="1814e147-5a0d-41cd-bab6-cc30ccb64877" /><item searchable="True" name="ForumUseCAPTCHA" tokenized="False" content="False" id="08e41aa7-2286-448c-8c5d-502caac3b2f6" /><item searchable="False" name="ForumGUID" tokenized="False" content="False" id="18c52c56-07e7-4e56-bf6d-797f049303a5" /><item searchable="True" name="ForumLastModified" tokenized="False" content="False" id="73b80f4c-8c41-456d-a9a2-7653e8e5af34" /><item searchable="False" name="ForumUnsubscriptionUrl" tokenized="True" content="True" id="412d894b-7039-4134-be2d-cd4216b6cdab" /><item searchable="True" name="ForumIsLocked" tokenized="False" content="False" id="b5943b0b-cb0e-4e37-a36e-caa34172cf03" /><item searchable="False" name="ForumSettings" tokenized="True" content="True" id="c998773b-6985-48b1-9540-49ea02831c9e" /><item searchable="True" name="ForumAuthorEdit" tokenized="False" content="False" id="2824b85f-99a7-46fa-a559-3f39ee74c5dd" /><item searchable="True" name="ForumAuthorDelete" tokenized="False" content="False" id="02966967-03d1-410a-8897-c899ba7c97a8" /><item searchable="True" name="ForumType" tokenized="False" content="False" id="c99a5ca2-8099-49e5-af59-d1315c939cea" /><item searchable="True" name="ForumIsAnswerLimit" tokenized="False" content="False" id="80534f06-2c4d-47f5-9320-ebcafdce5bf4" /><item searchable="True" name="ForumImageMaxSideSize" tokenized="False" content="False" id="2dfba041-494d-4251-ada3-001a118c92e0" /><item searchable="True" name="ForumLastPostTimeAbsolute" tokenized="False" content="False" id="daa22402-b847-4072-9050-4a7fc8a25ed4" /><item searchable="False" name="ForumLastPostUserNameAbsolute" tokenized="True" content="True" id="a4e466c6-1d7c-42a0-99af-f5697bd680c2" /><item searchable="True" name="ForumPostsAbsolute" tokenized="False" content="False" id="67059b63-3058-4bb6-bfd3-35a10b62db14" /><item searchable="True" name="ForumThreadsAbsolute" tokenized="False" content="False" id="680f2610-8aa1-4881-aca8-1eadb0e54b12" /><item searchable="True" name="ForumAttachmentMaxFileSize" tokenized="False" content="False" id="a97bb594-3c61-4643-acc1-7612da025506" /><item searchable="True" name="ForumDiscussionActions" tokenized="False" content="False" id="28ffaa81-482b-41f6-acf4-0731de663cca" /><item searchable="True" name="ForumSiteID" tokenized="False" content="False" id="ce6f2f08-3100-4062-910c-4cdf4940fdec" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1123, N'ForumPost', N'Forums.ForumPost', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_ForumPost">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PostId" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PostForumID" type="xs:int" />
              <xs:element name="PostParentID" type="xs:int" minOccurs="0" />
              <xs:element name="PostIDPath">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostLevel" type="xs:int" />
              <xs:element name="PostSubject">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostUserID" type="xs:int" minOccurs="0" />
              <xs:element name="PostUserName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostUserMail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostTime" type="xs:dateTime" />
              <xs:element name="PostApprovedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="PostThreadPosts" type="xs:int" minOccurs="0" />
              <xs:element name="PostThreadLastPostUserName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostThreadLastPostTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="PostUserSignature" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PostLastModified" type="xs:dateTime" />
              <xs:element name="PostApproved" type="xs:boolean" minOccurs="0" />
              <xs:element name="PostIsLocked" type="xs:boolean" minOccurs="0" />
              <xs:element name="PostIsAnswer" type="xs:int" minOccurs="0" />
              <xs:element name="PostStickOrder" type="xs:int" />
              <xs:element name="PostViews" type="xs:int" minOccurs="0" />
              <xs:element name="PostLastEdit" type="xs:dateTime" minOccurs="0" />
              <xs:element name="PostInfo" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostAttachmentCount" type="xs:int" minOccurs="0" />
              <xs:element name="PostType" type="xs:int" minOccurs="0" />
              <xs:element name="PostThreadPostsAbsolute" type="xs:int" minOccurs="0" />
              <xs:element name="PostThreadLastPostUserNameAbsolute" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PostThreadLastPostTimeAbsolute" type="xs:dateTime" minOccurs="0" />
              <xs:element name="PostQuestionSolved" type="xs:boolean" minOccurs="0" />
              <xs:element name="PostIsNotAnswer" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_ForumPost" />
      <xs:field xpath="PostId" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PostId" fieldcaption="PostId" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="00a5f4ac-2e67-465b-bd5c-e6848c10559a" /><field column="PostForumID" fieldcaption="PostForumID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="924d3b8c-3110-48b4-961a-08bae5b5adc0" /><field column="PostParentID" fieldcaption="PostParentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7ec61a89-dd67-4694-8734-6887ad02b162" /><field column="PostIDPath" fieldcaption="PostIDPath" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="3f10250c-688d-4f5e-8b6d-68e5055872e3" /><field column="PostLevel" fieldcaption="PostLevel" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a5f89ef5-0988-402e-996e-e93409d0b22a" /><field column="PostSubject" fieldcaption="PostSubject" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="fc8be85a-2904-4098-a7c0-35ea946ae16d" /><field column="PostUserID" fieldcaption="PostUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6e8e10ba-6ff2-4393-b6cc-37d4e3a3687b" /><field column="PostUserName" fieldcaption="PostUserName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="8373dd18-fe7b-4ead-a3b6-9c295605385f" visibility="none" ismacro="false" /><field column="PostUserMail" fieldcaption="PostUserMail" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="c2213d50-2c8f-41b0-9151-41f0b84f37db" /><field column="PostText" fieldcaption="PostText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="33c6eb92-51f3-420d-82cd-5e3b4f54361a" /><field column="PostTime" fieldcaption="PostTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="95aa7b00-47d2-4119-af78-6ba0003eede2" /><field column="PostApprovedByUserID" fieldcaption="PostApprovedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ac69c3a5-53a2-4113-b78c-31d41d34868a" /><field column="PostThreadPosts" fieldcaption="PostThreadPosts" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6290317c-5293-44e1-81fb-b85c85f69f60" /><field column="PostThreadLastPostUserName" fieldcaption="PostThreadLastPostUserName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="17985059-abff-4992-a38d-0ce54ff426dd" visibility="none" ismacro="false" /><field column="PostThreadLastPostTime" fieldcaption="PostThreadLastPostTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="989327c7-6997-4a50-a234-10ec7a5ad132" /><field column="PostUserSignature" fieldcaption="PostUserSignature" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="47a93b85-74db-447b-a10f-2df627566203" /><field column="PostGUID" fieldcaption="PostGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b98f1f62-a46e-450b-b61d-68b13e0bff63" /><field column="PostLastModified" fieldcaption="PostLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b0d89e7c-6427-4f90-9ef9-1cd819cc3669" /><field column="PostApproved" fieldcaption="PostApproved" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0bb49d7c-346d-4da4-ac4e-70cd971fec5d" /><field column="PostIsLocked" fieldcaption="PostIsLocked" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2560b6ed-9156-4b51-81d6-8b44165c8334" /><field column="PostIsAnswer" fieldcaption="PostIsAnswer" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ac84a32c-33eb-4404-88cd-843cd5f4475a" /><field column="PostStickOrder" fieldcaption="PostStickOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5dcc65fd-dba4-4e7f-b899-b5cfcbbe23e6" /><field column="PostViews" fieldcaption="PostViews" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c586bf90-5b68-488d-a6a6-6bda0f1a6e27" /><field column="PostLastEdit" fieldcaption="PostLastEdit" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a00788a0-53ec-4f73-bf9a-47f7c1fa1940" /><field column="PostInfo" fieldcaption="PostInfo" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a879820c-e3cd-4467-82a6-c4d28f15e637" /><field column="PostAttachmentCount" fieldcaption="PostAttachmentCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="71cc9ff2-e54e-4536-8248-3205e7320931" defaultvalue="0" /><field column="PostType" fieldcaption="PostType" visible="true" columntype="integer" fieldtype="radiobuttons" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9157042f-693a-4e03-9b2c-dcc84404f34a"><settings><repeatdirection>vertical</repeatdirection><options><item value="0" text="Normal" /><item value="1" text="Answer" /></options></settings></field><field column="PostThreadPostsAbsolute" fieldcaption="PostThreadPostsAbsolute" visible="false" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7470dc26-3ee4-4027-bdc1-aedd64b35715" /><field column="PostThreadLastPostUserNameAbsolute" fieldcaption="PostThreadLastPostUserNameAbsolute" visible="false" columntype="text" fieldtype="label" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="17951f5f-5f61-4cc8-b63b-82f58815c826" /><field column="PostThreadLastPostTimeAbsolute" fieldcaption="PostThreadLastPostTimeAbsolute" visible="false" columntype="datetime" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0af83503-acaf-47a1-b6f6-19c85308a9fb" /><field column="PostQuestionSolved" fieldcaption="PostQuestionSolved" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6e4af8f2-aae7-4df1-b977-2cd341ada9f0" /><field column="PostIsNotAnswer" fieldcaption="PostIsNotAnswer" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="34fe9c34-2c65-456b-a285-fcbc84b7650a" /></form>', N'', N'', N'', N'Forums_ForumPost', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110113 10:57:10', 'f40c961c-3d47-4e6a-997e-b127db2520c1', 0, 1, 0, N'', 2, N'PostSubject', N'PostText', N'', N'PostTime', N'<search><item tokenized="False" name="PostId" content="False" searchable="True" id="e4795a52-a0c3-4040-b4d9-07a57294e05b" /><item tokenized="False" name="PostForumID" content="False" searchable="True" id="075d7293-802f-476d-aac6-af0abae45816" /><item tokenized="False" name="PostParentID" content="False" searchable="True" id="4bac1bd7-1885-497a-8f15-0fc6015dfa93" /><item tokenized="False" name="PostIDPath" content="False" searchable="True" id="76d587b1-0711-419e-ac79-a4305a3e1c38" /><item tokenized="False" name="PostLevel" content="False" searchable="True" id="c515659a-41e8-4e0d-9bfd-a8706a4cec54" /><item tokenized="True" name="PostSubject" content="True" searchable="False" id="d8fdc784-f5d5-412c-8885-b4c4ff0c5974" /><item tokenized="False" name="PostUserID" content="False" searchable="True" id="0f9a0b1e-731c-4432-bd1b-d9bc1bdddae2" /><item tokenized="True" name="PostUserName" content="True" searchable="False" id="c5939d48-d9cb-4d74-bfe3-5e5d4e4b200f" /><item tokenized="True" name="PostUserMail" content="True" searchable="False" id="32dde000-eed5-4091-8bc9-8acca85ac1b3" /><item tokenized="True" name="PostText" content="True" searchable="False" id="20650fce-9586-4dac-87e2-042eabd757f8" /><item tokenized="False" name="PostTime" content="False" searchable="True" id="e3c5b2c8-00cc-43b0-b1cd-16cae14e6102" /><item tokenized="False" name="PostApprovedByUserID" content="False" searchable="True" id="781b680e-5ee9-485a-a260-2652245919df" /><item tokenized="False" name="PostThreadPosts" content="False" searchable="True" id="75949b92-d698-4053-99e4-43a277a9ec1c" /><item tokenized="False" name="PostThreadLastPostUserName" content="False" searchable="False" id="c55984d7-8f15-40ca-bd4b-8b152eb3ed7a" /><item tokenized="False" name="PostThreadLastPostTime" content="False" searchable="False" id="acd19d37-9350-4661-b2b8-fdd16cde9050" /><item tokenized="False" name="PostUserSignature" content="False" searchable="False" id="6235fc61-99c0-4b32-8759-e87c981e9a65" /><item tokenized="False" name="PostGUID" content="False" searchable="False" id="c16877d3-a7d2-48e4-86d2-c39014f324db" /><item tokenized="False" name="PostLastModified" content="False" searchable="True" id="4270f74e-7c3b-405c-b953-4eb710cdb86f" /><item tokenized="False" name="PostApproved" content="False" searchable="True" id="ef64c5f6-6b82-4070-b814-4dbd575b29e2" /><item tokenized="False" name="PostIsLocked" content="False" searchable="True" id="84eb64c7-849d-4621-bc08-84d693b98cab" /><item tokenized="False" name="PostIsAnswer" content="False" searchable="True" id="88c1b2a0-4a86-42c4-ac35-54955c146b83" /><item tokenized="False" name="PostStickOrder" content="False" searchable="True" id="c610c446-b9bb-4e09-a9b9-c9cf0cf47bb3" /><item tokenized="False" name="PostViews" content="False" searchable="True" id="3133a944-d966-472a-8c87-9e28865eb634" /><item tokenized="False" name="PostLastEdit" content="False" searchable="True" id="5b7c2424-423a-4367-9053-1fb5751e4be8" /><item tokenized="False" name="PostInfo" content="False" searchable="False" id="a6d1e097-2511-4e4f-8a65-a18c9c7f0701" /><item tokenized="False" name="PostAttachmentCount" content="False" searchable="True" id="fb76529e-3521-4987-b1f5-9c2b22cf2afe" /><item tokenized="False" name="PostType" content="False" searchable="True" id="6fbf9437-e4ce-4636-88d3-5a5225d5da58" /><item tokenized="False" name="PostThreadPostsAbsolute" content="False" searchable="False" id="e1e78549-804d-486d-808d-0d9088dd4c9f" /><item tokenized="False" name="PostThreadLastPostUserNameAbsolute" content="False" searchable="False" id="703bb080-71c4-41ab-bcca-ad88374bc5ef" /><item tokenized="False" name="PostThreadLastPostTimeAbsolute" content="False" searchable="False" id="34738a31-7dbb-45b8-af3d-7249fb54857b" /><item tokenized="False" name="PostQuestionSolved" content="False" searchable="True" id="abacdb5a-597a-4e0d-882c-b4f86c15adb4" /><item tokenized="False" name="PostIsNotAnswer" content="False" searchable="True" id="ef75eb43-e6da-448d-b7bc-714b44e6f5ea" /></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1124, N'ForumSubscription', N'Forums.ForumSubscription', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_ForumSubscription">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SubscriptionUserID" type="xs:int" minOccurs="0" />
              <xs:element name="SubscriptionEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionForumID" type="xs:int" />
              <xs:element name="SubscriptionPostID" type="xs:int" minOccurs="0" />
              <xs:element name="SubscriptionGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="SubscriptionLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_ForumSubscription" />
      <xs:field xpath="SubscriptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriptionID" fieldcaption="SubscriptionID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="3fc5afd4-d80f-4c8e-bff1-203798e7142a" /><field column="SubscriptionUserID" fieldcaption="SubscriptionUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dd5652e2-0d24-4e79-bbca-41508456c966" /><field column="SubscriptionEmail" fieldcaption="SubscriptionEmail" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01531690-2ab8-4ef5-97a3-db33bf517da4" /><field column="SubscriptionForumID" fieldcaption="SubscriptionForumID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1f53c06d-dfed-46f0-b77e-7ce173d0f2e5" /><field column="SubscriptionPostID" fieldcaption="SubscriptionPostID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aafad6cf-698b-41f7-97b7-21d4988bd61a" /><field column="SubscriptionGUID" fieldcaption="SubscriptionGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="446281b6-cf2d-4f6a-95a1-bda6552a2b74" /><field column="SubscriptionLastModified" fieldcaption="SubscriptionLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f417d26b-a105-4dbd-848a-7451dcb6d6b7" /></form>', N'', N'', N'', N'Forums_ForumSubscription', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110113 10:59:38', 'c1ddbb88-8e9a-4b77-b7c1-cb331ed94083', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1125, N'Country', N'cms.country', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="cms_country">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CountryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CountryDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CountryName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CountryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CountryLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//cms_country" />
      <xs:field xpath="CountryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CountryID" fieldcaption="CountryID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="1c2b75ec-e2ec-4c27-b9f3-b1ad4c86747b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CountryDisplayName" fieldcaption="{$Country_Edit.CountryDisplayNameLabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="714fce0f-86fb-4b48-84bd-7e028eecf62d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>localizabletextbox</controlname></settings></field><field column="CountryName" fieldcaption="{$Country_Edit.CountryNameLabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="7360fc6f-444b-4293-a60e-678920a9635f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="CountryGUID" fieldcaption="CountryGUID" visible="false" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="eafdf9a4-1213-4ab7-ad24-e67684cfa34e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>directuploadcontrol</controlname><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="CountryLastModified" fieldcaption="CountryLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="28acb0c7-a1e6-4b19-9037-b53147d9cf02" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'cms_country', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110811 13:58:56', '7e651b6d-e59d-4d72-93c3-2d3adb2a6c6b', 0, 1, 0, N'', 1, N'CountryDisplayName', N'0', N'', N'CountryLastModified', N'<search><item searchable="True" name="CountryID" tokenized="False" content="False" id="c08c858f-ac1c-4f8c-a8ed-aa91d8f6a11f" /><item searchable="False" name="CountryDisplayName" tokenized="True" content="True" id="64ded574-2084-420d-9348-18be85becc4c" /><item searchable="False" name="CountryName" tokenized="True" content="True" id="84980a47-1ee7-4a79-ae61-0487f25db40e" /><item searchable="False" name="CountryGUID" tokenized="False" content="False" id="a021a032-aba1-4edf-8f18-4ec0e1972374" /><item searchable="True" name="CountryLastModified" tokenized="False" content="False" id="ebdbe945-da05-4c45-a31f-e7a6be2ec9b0" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1126, N'State', N'cms.state', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_State">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="StateDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StateName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StateCode" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CountryID" type="xs:int" />
              <xs:element name="StateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="StateLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_State" />
      <xs:field xpath="StateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StateID" fieldcaption="StateID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="9d25f72b-8b2f-4635-a0c0-49f1a46369ce" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="StateDisplayName" fieldcaption="{$Country_State_Edit.StateDisplayNameLabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="153f4f19-df99-4cb5-8089-cdfc25260a4f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>localizabletextbox</controlname></settings></field><field column="StateName" fieldcaption="{$Country_State_Edit.StateNameLabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="f773552d-012b-4d94-9935-dab01da9b535" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="StateCode" fieldcaption="{$Country_State_Edit.StateCodeLabel$}" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="33e36f31-a6ad-4e19-a83c-b4d900cc07db" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="CountryID" fieldcaption="CountryID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c76ec985-2879-4e92-aebd-edf82909e58b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StateGUID" fieldcaption="StateGUID" visible="false" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="216850cd-1080-4bc7-a794-2c0f2fa848a0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>directuploadcontrol</controlname><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="StateLastModified" fieldcaption="StateLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="014db439-c6cd-4774-9a1b-d2243fdff59c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_State', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111214 09:00:03', '6d8f4d1f-3ac1-4388-94d6-14d68e972f5d', 0, 1, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1131, N'Staging - synchronization', N'staging.synchronization', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Staging_Synchronization">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SynchronizationID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SynchronizationTaskID" type="xs:int" />
              <xs:element name="SynchronizationServerID" type="xs:int" />
              <xs:element name="SynchronizationLastRun" type="xs:dateTime" />
              <xs:element name="SynchronizationErrorMessage" minOccurs="0">
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
      <xs:selector xpath=".//Staging_Synchronization" />
      <xs:field xpath="SynchronizationID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SynchronizationID" fieldcaption="SynchronizationID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a98996fd-850a-48a3-862e-ed43833d3780" /><field column="SynchronizationTaskID" fieldcaption="SynchronizationTaskID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82d4f3ab-07fb-42ff-9a7c-5f398e1c52cd" /><field column="SynchronizationServerID" fieldcaption="SynchronizationServerID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5ed8bcb4-d204-46e7-b6e3-9a282fc888ea" /><field column="SynchronizationLastRun" fieldcaption="SynchronizationLastRun" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f85de1a4-a651-4150-97f5-8af2c850964c" /><field column="SynchronizationErrorMessage" fieldcaption="SynchronizationErrorMessage" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0a3c3f13-ce60-45e4-8276-539e56d79972" /></form>', N'', N'', N'', N'Staging_Synchronization', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100415 14:32:30', '235368da-5b9f-4a38-b0f6-b8baf22ecd0f', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1132, N'Staging - server', N'staging.server', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="staging_server">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ServerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ServerName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerSiteID" type="xs:int" />
              <xs:element name="ServerURL">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerEnabled" type="xs:boolean" />
              <xs:element name="ServerAuthentication">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerUsername" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerPassword" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerX509ClientKeyID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerX509ServerKeyID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ServerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ServerLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//staging_server" />
      <xs:field xpath="ServerID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ServerID" fieldcaption="ServerID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="adc25c7d-4b9d-4272-9bb5-83577cf832d3" /><field column="ServerName" fieldcaption="ServerName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="56e84c75-7ef5-46ac-b851-792532748a56" /><field column="ServerDisplayName" fieldcaption="ServerDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ccbfd081-f598-4bcd-86c5-0b0659d1d823" columnsize="440" visibility="none" ismacro="false" /><field column="ServerSiteID" fieldcaption="ServerSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3a5aac85-f6a7-462c-995c-f87088fccdc7" /><field column="ServerURL" fieldcaption="ServerURL" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5e032c90-c2ab-43d8-bb9c-9c43ebfcddc8" /><field column="ServerEnabled" fieldcaption="ServerEnabled" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4726b612-34b9-464b-8a35-17ef4acc9c26" /><field column="ServerAuthentication" fieldcaption="ServerAuthentication" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="df28276a-53b0-4899-b35e-3bce1bf0cfb8" /><field column="ServerUsername" fieldcaption="ServerUsername" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7d0a229d-7ac4-4377-aaf4-953e995ed9a8" /><field column="ServerPassword" fieldcaption="ServerPassword" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b2bb8d87-584d-4e88-bff1-ebc6e96eff4a" /><field column="ServerX509ClientKeyID" fieldcaption="ServerX509ClientKeyID" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2c24dc63-7eed-4fc9-a343-24c89827ce81" /><field column="ServerX509ServerKeyID" fieldcaption="ServerX509ServerKeyID" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8b2d88a3-111f-4a03-8128-e31abaff562f" /><field column="ServerGUID" fieldcaption="ServerGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="85c57a10-f71b-4bcc-903a-8e55bd1038a2" /><field column="ServerLastModified" fieldcaption="ServerLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="40a0bfef-1603-4a94-90c0-d48e769f3fe7" /></form>', N'', N'', N'', N'Staging_Server', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 12:36:30', 'ae6f2aaa-9dbc-47f4-b365-91167e71bbd0', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1133, N'Staging - synclog', N'staging.synclog', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Staging_Synclog">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SyncLogID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SyncLogTaskID" type="xs:int" />
              <xs:element name="SyncLogServerID" type="xs:int" />
              <xs:element name="SyncLogTime" type="xs:dateTime" />
              <xs:element name="SyncLogError" minOccurs="0">
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
      <xs:selector xpath=".//Staging_Synclog" />
      <xs:field xpath="SyncLogID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SyncLogID" fieldcaption="SyncLogID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8bdf61c0-cae5-4bbb-a8a3-ed8fff630027" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SyncLogTaskID" fieldcaption="SyncLogTaskID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="47ab8296-3ebb-42a4-84e0-427938f02836" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SyncLogServerID" fieldcaption="SyncLogServerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bfd29008-13d1-46f0-a828-7d475504278f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SyncLogTime" fieldcaption="SyncLogTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f06b57e-9ceb-4a12-a92d-c9fed5686c61" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SyncLogError" fieldcaption="SyncLogError" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5cca9fdd-e704-42df-8121-172a42e3c8be" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field></form>', N'', N'', N'', N'Staging_Synclog', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110613 10:45:10', 'abf6512d-23fe-4657-a767-0417f41cf96b', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1134, N'Staging - task', N'staging.task', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Staging_Task">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaskID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaskSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="TaskDocumentID" type="xs:int" minOccurs="0" />
              <xs:element name="TaskNodeAliasPath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
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
                    <xs:maxLength value="2147483647" />
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
              <xs:element name="TaskRunning" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaskNodeID" type="xs:int" minOccurs="0" />
              <xs:element name="TaskServers" minOccurs="0">
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
      <xs:selector xpath=".//Staging_Task" />
      <xs:field xpath="TaskID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaskID" fieldcaption="TaskID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="5a76cc96-89cf-4a50-94e6-7514e2d49fdc" ismacro="false" /><field column="TaskSiteID" fieldcaption="TaskSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fa037649-546c-4e8c-b6ba-b58233ac4cfb" ismacro="false" /><field column="TaskDocumentID" fieldcaption="TaskDocumentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="229ffc3b-f78f-4982-a159-fd0789c9875e" ismacro="false" /><field column="TaskNodeID" fieldcaption="TaskNodeID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="df505950-031a-4d7d-bfa7-0f0dbea0a732" visibility="none" ismacro="false" /><field column="TaskNodeAliasPath" fieldcaption="TaskNodeAliasPath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="44f99d9f-cf85-4f4e-9f6a-b38466e32e4c" ismacro="false" /><field column="TaskTitle" fieldcaption="TaskTitle" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ec039fe8-3da7-4c0f-a8f7-4f8d8c501eea" ismacro="false" /><field column="TaskData" fieldcaption="TaskData" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="582a8ed2-17d6-46e7-af57-d1552a5905c4" ismacro="false" /><field column="TaskTime" fieldcaption="TaskTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fe5c0b3e-381b-4813-896f-8a24119c8912" ismacro="false" /><field column="TaskType" fieldcaption="TaskType" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c934e50-ede0-429b-9243-7951ae0c7022" ismacro="false" /><field column="TaskObjectType" fieldcaption="TaskObjectType" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca7447f8-e178-4ef1-8a8a-d373c2364422" ismacro="false" /><field column="TaskObjectID" fieldcaption="TaskObjectID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c27c108d-ecda-45ce-a085-fce64d313eed" ismacro="false" /><field column="TaskRunning" fieldcaption="TaskRunning" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="17eafbfa-0395-4cc3-9ad3-0afdbea9c19e" ismacro="false" /><field column="TaskServers" visible="false" defaultvalue="null" columntype="longtext" fieldtype="label" allowempty="true" isPK="false" system="true" fielddescription="List of server names separated by ?;?(;server1;server2;server3;)" publicfield="false" spellcheck="true" guid="2a3a4d63-e6d8-4821-bb81-d0574331824d" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Staging_Task', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110228 19:36:44', 'a79db106-b9f8-44ee-a172-f8c77403aebb', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1141, N'Ecommerce - Supplier', N'ecommerce.supplier', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Supplier">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SupplierID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SupplierDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SupplierPhone">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SupplierEmail">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SupplierFax">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SupplierEnabled" type="xs:boolean" />
              <xs:element name="SupplierGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SupplierLastModified" type="xs:dateTime" />
              <xs:element name="SupplierSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Supplier" />
      <xs:field xpath="SupplierID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SupplierID" fieldcaption="SupplierID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="7197f910-b4b5-4c81-be4c-b24f7c38e87c" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SupplierDisplayName" fieldcaption="SupplierDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fe7d0925-6fda-47bc-9783-d8f27867590a" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SupplierPhone" fieldcaption="SupplierPhone" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f321f915-0958-4c10-9bc3-b8ab505b1f2a" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SupplierEmail" fieldcaption="SupplierEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01565a13-6ee4-482d-b9f6-d6a9d03c2fca" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SupplierFax" fieldcaption="SupplierFax" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8bc06426-f54b-4cd7-9b59-64fd0d84b1c3" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SupplierEnabled" fieldcaption="SupplierEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fb64cad7-bec2-4e50-8c90-502de0f21738" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SupplierGUID" fieldcaption="SupplierGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="27e25a04-f570-44cf-9358-89869d3a33e1" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="SupplierLastModified" fieldcaption="SupplierLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d0e00d08-4f82-4a61-a2fd-b6a796758c0d" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SupplierSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="f858d835-cc53-491c-afc7-5700c53c60e0" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_Supplier', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110405 17:27:44', 'd30e1123-ea76-42db-8f74-a20da0c54d03', 0, 1, 0, N'', 2, N'SupplierDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="SupplierID" tokenized="False" content="False" id="2a1901da-71f5-490f-8835-c5a41b6d41fd" /><item searchable="False" name="SupplierDisplayName" tokenized="True" content="True" id="717de3e9-0940-41ab-9a4c-ce2b4238c26d" /><item searchable="False" name="SupplierPhone" tokenized="True" content="True" id="5aa79877-ff3d-4f28-a46b-86513db4bfb9" /><item searchable="False" name="SupplierEmail" tokenized="True" content="True" id="0f09391c-5849-4704-820d-8cfce8f536fa" /><item searchable="False" name="SupplierFax" tokenized="True" content="True" id="5ddb4255-e23f-43ab-9043-97e4029fb16a" /><item searchable="True" name="SupplierEnabled" tokenized="False" content="False" id="0f1bc837-6ca5-4806-bd85-1edc0ba20240" /><item searchable="False" name="SupplierGUID" tokenized="False" content="False" id="fde172e5-f58b-4714-b1d6-b36db9f735f5" /><item searchable="True" name="SupplierLastModified" tokenized="False" content="False" id="91e5c40b-dd09-4820-824e-9070e61eaf8e" /><item searchable="True" name="SupplierSiteID" tokenized="False" content="False" id="e2b44de0-ba33-4a2b-8e8d-95936b8ed114" /></search>', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1142, N'Ecommerce - Manufacturer', N'ecommerce.manufacturer', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Manufacturer">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ManufacturerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ManufacturerDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ManufactureHomepage" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ManufacturerEnabled" type="xs:boolean" />
              <xs:element name="ManufacturerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ManufacturerLastModified" type="xs:dateTime" />
              <xs:element name="ManufacturerSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Manufacturer" />
      <xs:field xpath="ManufacturerID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ManufacturerID" fieldcaption="ManufacturerID" visible="true" columntype="integer" fieldtype="TextBoxControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="53e6de27-2519-46b8-bf4a-9b9b6b29089b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="ManufacturerDisplayName" fieldcaption="ManufacturerDisplayName" visible="true" columntype="text" fieldtype="TextBoxControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="51d162fa-6588-4109-b837-0d1e88903be6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="ManufactureHomepage" fieldcaption="ManufactureHomepage" visible="true" columntype="text" fieldtype="TextBoxControl" allowempty="true" isPK="false" system="true" columnsize="400" publicfield="false" spellcheck="true" guid="24da0868-e426-4381-9141-8a0c20b2fffa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="ManufacturerEnabled" fieldcaption="ManufacturerEnabled" visible="true" columntype="boolean" fieldtype="TextBoxControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="26bc66e3-00c2-49c4-9713-1dc8a2237c2c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="ManufacturerGUID" fieldcaption="ManufacturerGUID" visible="true" columntype="guid" fieldtype="TextBoxControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d4586504-a3d4-4f8e-9624-ce18f6849fab" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="ManufacturerLastModified" fieldcaption="ManufacturerLastModified" visible="true" columntype="datetime" fieldtype="TextBoxControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2c25aa69-3c12-4d67-90d5-337e8722abf9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="ManufacturerSiteID" fieldcaption="ManufacturerSiteID" visible="true" columntype="integer" fieldtype="TextBoxControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="84b6048d-6430-4bf8-a75e-8062f2aae429" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /></form>', N'', N'', N'', N'COM_Manufacturer', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110404 11:33:16', 'dcbcbc29-fa72-404c-bcaf-7eea0dac144e', 0, 1, 0, N'', 2, N'ManufacturerDisplayName', N'ManufactureHomepage', N'', N'ManufacturerLastModified', N'<search><item searchable="True" name="ManufacturerID" tokenized="False" content="False" id="eeefda16-bcd6-42ee-8237-4161be12e7b7" /><item searchable="False" name="ManufacturerDisplayName" tokenized="True" content="True" id="63610929-09cc-41f8-b6da-c976b4f47376" /><item searchable="False" name="ManufactureHomepage" tokenized="True" content="True" id="c8a08a6c-73d1-4e45-9612-a5dd66041550" /><item searchable="True" name="ManufacturerEnabled" tokenized="False" content="False" id="3cbc1302-b6b9-4a7d-a962-68d0f00c6e82" /><item searchable="False" name="ManufacturerGUID" tokenized="False" content="False" id="f8662843-ef3f-418b-89c6-b66fcbd0cf4a" /><item searchable="True" name="ManufacturerLastModified" tokenized="False" content="False" id="a26d17d3-a332-4b96-8c41-c20f721cfd8b" /><item searchable="True" name="ManufacturerSiteID" tokenized="False" content="False" id="bffd60ac-5d3f-4af9-8eca-7ef6dbabe4f9" /></search>', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1143, N'Ecommerce - Currency', N'ecommerce.currency', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Currency">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CurrencyID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CurrencyName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CurrencyDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CurrencyCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CurrencyRoundTo" type="xs:int" minOccurs="0" />
              <xs:element name="CurrencyEnabled" type="xs:boolean" />
              <xs:element name="CurrencyFormatString">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CurrencyIsMain" type="xs:boolean" />
              <xs:element name="CurrencyGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="CurrencyLastModified" type="xs:dateTime" />
              <xs:element name="CurrencySiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Currency" />
      <xs:field xpath="CurrencyID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CurrencyID" fieldcaption="CurrencyID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e5c6c212-da7d-441e-a7ea-0a4ad9878485" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CurrencyName" fieldcaption="CurrencyName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="90342e29-3669-4e5e-a523-cf604230d80d" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CurrencyDisplayName" fieldcaption="CurrencyDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="194beb72-436a-452c-88b5-ed2dcaa4794d" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CurrencyCode" fieldcaption="CurrencyCode" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="088b32fc-7f67-4730-9419-b81aa71c1e48" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CurrencyRoundTo" fieldcaption="CurrencyRoundTo" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3ed60614-90b5-43ba-be0d-e774e8868f41" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CurrencyEnabled" fieldcaption="CurrencyEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="305137cc-6b7a-48b9-9c61-1c56cd21ff0b" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CurrencyFormatString" fieldcaption="CurrencyFormatString" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="32757520-9db9-4153-937d-bc8e3b0ee5b8" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CurrencyIsMain" fieldcaption="CurrencyIsMain" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="727b2700-d8e8-47e1-be66-b6a9977db89c" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CurrencyGUID" fieldcaption="CurrencyGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="edfd61fa-607c-4bb0-9166-2a7a5c2c2dd3" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CurrencyLastModified" fieldcaption="CurrencyLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="13efe629-afaf-4943-b3b7-0a3e0d0ec1d9" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="CurrencySiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e07b4e06-f8f7-4f2a-ae13-4294ef5ae714" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_Currency', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110407 14:03:03', '456a1f44-3c71-446f-8a24-509b74037abd', 0, 1, 0, N'', 2, N'CurrencyDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="CurrencyID" tokenized="False" content="False" id="0599d565-bd44-41e3-a9c5-a135157faacb" /><item searchable="False" name="CurrencyName" tokenized="True" content="True" id="12529362-e413-4781-bd33-05a35f5fa671" /><item searchable="False" name="CurrencyDisplayName" tokenized="True" content="True" id="1dc78b36-663d-4eb4-ae97-87b2531ce1fd" /><item searchable="False" name="CurrencyCode" tokenized="True" content="True" id="eea11d9f-8f84-4f87-a35c-30bfa757cc8d" /><item searchable="True" name="CurrencyRoundTo" tokenized="False" content="False" id="4736ccb9-8576-4031-8cc4-a31ef53b5e37" /><item searchable="True" name="CurrencyEnabled" tokenized="False" content="False" id="49dcfb00-f2d4-4663-8c4f-2e5fb1513025" /><item searchable="False" name="CurrencyFormatString" tokenized="True" content="True" id="1d0b9777-1965-443d-b0ef-3ccd854dda22" /><item searchable="True" name="CurrencyIsMain" tokenized="False" content="False" id="0769b2d8-ef53-4464-aadb-2fc5adddecdc" /><item searchable="False" name="CurrencyGUID" tokenized="False" content="False" id="06f31704-4856-4eff-bd41-cdd453429418" /><item searchable="True" name="CurrencyLastModified" tokenized="False" content="False" id="77dbdea8-49e9-4143-822b-929e21460d19" /><item searchable="True" name="CurrencySiteID" tokenized="False" content="False" id="76ea84e2-6eed-4709-a917-443b2c720dee" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1144, N'Ecommerce - Payment option', N'ecommerce.paymentoption', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_PaymentOption">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PaymentOptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PaymentOptionName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PaymentOptionDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PaymentOptionEnabled" type="xs:boolean" />
              <xs:element name="PaymentOptionSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="PaymentOptionPaymentGateUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="500" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PaymentOptionAssemblyName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PaymentOptionClassName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PaymentOptionSucceededOrderStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="PaymentOptionFailedOrderStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="PaymentOptionGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PaymentOptionLastModified" type="xs:dateTime" />
              <xs:element name="PaymentOptionAllowIfNoShipping" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_PaymentOption" />
      <xs:field xpath="PaymentOptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PaymentOptionID" fieldcaption="PaymentOptionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0c8db08f-1219-47e6-9ec0-2c7347e6dfb4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PaymentOptionName" fieldcaption="PaymentOptionName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b941eea2-edcd-44af-8370-63df85298d7a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionDisplayName" fieldcaption="PaymentOptionDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c81b117e-07e6-4665-8852-e07ee74e30ee" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionEnabled" fieldcaption="PaymentOptionEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6dcaf75a-f970-4cc9-8119-ba5d68b6a41f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PaymentOptionSiteID" fieldcaption="PaymentOptionSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="012bd445-e8a8-4c27-8d04-9e3867e7c3a7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionPaymentGateUrl" fieldcaption="PaymentOptionPaymentGateUrl" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e9508d42-44c5-4241-a83f-e74ec498eeb9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionAssemblyName" fieldcaption="PaymentOptionAssemblyName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c32782d-b052-4ff6-a7ba-0f8936788f8b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionClassName" fieldcaption="PaymentOptionClassName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="abff12f4-f681-4245-88c3-61b27d1a9794" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionSucceededOrderStatusID" fieldcaption="PaymentOptionSucceededOrderStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f183ff2d-e653-4d8f-b167-4ea7468efb7c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionFailedOrderStatusID" fieldcaption="PaymentOptionFailedOrderStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b6c2060-7184-410e-9c14-60636824d725" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PaymentOptionGUID" fieldcaption="PaymentOptionGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2683e510-dc55-4bf1-8367-845ec45dbb05" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="PaymentOptionLastModified" fieldcaption="PaymentOptionLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4e2fdbb1-b20c-4165-84b3-d1b4e55c90c0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PaymentOptionAllowIfNoShipping" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a3b1072d-0f77-4404-b04e-a26456b1e602" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_PaymentOption', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110823 20:50:00', 'f4d1e038-f33e-4ff6-99e8-a075a94a964b', 0, 1, 0, N'', 2, N'PaymentOptionDisplayName', N'0', N'', N'PaymentOptionLastModified', N'<search><item searchable="True" name="PaymentOptionID" tokenized="False" content="False" id="620d385c-8edc-4fb8-9e31-552157c944d2" /><item searchable="False" name="PaymentOptionName" tokenized="True" content="True" id="6a7824a8-ef73-4e6a-8563-afdf283b3e73" /><item searchable="False" name="PaymentOptionDisplayName" tokenized="True" content="True" id="e5381762-9fa3-4424-a477-90e51013b26b" /><item searchable="True" name="PaymentOptionEnabled" tokenized="False" content="False" id="2704897d-58bd-47df-8a48-37c9ad2ddd4c" /><item searchable="True" name="PaymentOptionSiteID" tokenized="False" content="False" id="a6bbd8ce-e3d1-475a-a64f-f9d4f326ae8e" /><item searchable="False" name="PaymentOptionPaymentGateUrl" tokenized="True" content="True" id="ea90d96e-1953-4b45-a79d-e29ccb237194" /><item searchable="False" name="PaymentOptionAssemblyName" tokenized="True" content="True" id="ff11cbb0-488b-4250-b1b9-5035bfce72d3" /><item searchable="False" name="PaymentOptionClassName" tokenized="True" content="True" id="ae7cc207-7193-431e-a652-7a67d493fbaa" /><item searchable="True" name="PaymentOptionSucceededOrderStatusID" tokenized="False" content="False" id="269597e9-164d-4225-a918-4f740b80b007" /><item searchable="True" name="PaymentOptionFailedOrderStatusID" tokenized="False" content="False" id="a22001d2-d183-4ada-9436-285fb00ed6d2" /><item searchable="False" name="PaymentOptionGUID" tokenized="False" content="False" id="cabc21a9-6f9f-4076-a883-154f9d4def19" /><item searchable="True" name="PaymentOptionLastModified" tokenized="False" content="False" id="9bfe226f-b85a-4bd1-8601-47cc99a48bb7" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1145, N'Ecommerce - Public status', N'ecommerce.publicstatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_PublicStatus">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PublicStatusID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PublicStatusName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PublicStatusDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PublicStatusEnabled" type="xs:boolean" />
              <xs:element name="PublicStatusGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PublicStatusLastModified" type="xs:dateTime" />
              <xs:element name="PublicStatusSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_PublicStatus" />
      <xs:field xpath="PublicStatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PublicStatusID" fieldcaption="PublicStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="18356b40-9b5e-448b-8e05-944cacae39ca" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PublicStatusName" fieldcaption="PublicStatusName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad426156-5ac2-4260-99bb-4cba01b0eaee" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PublicStatusDisplayName" fieldcaption="PublicStatusDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b12464a8-94d8-437d-95a5-76e1a3b7c22f" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PublicStatusEnabled" fieldcaption="PublicStatusEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43ca7794-8f4d-4384-8b99-6e168baec910" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PublicStatusGUID" fieldcaption="PublicStatusGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4ac4a4f9-5e50-4de6-a042-4d2aa3afdb53" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="PublicStatusLastModified" fieldcaption="PublicStatusLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="94bf7a36-2b43-4b77-97b7-37448a4a80be" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PublicStatusSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="6913b813-4f9d-45fd-bbd2-5c191c2fe4a9" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_PublicStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110405 17:19:10', 'ae8706a2-9a38-47be-ad1b-251862821fad', 0, 1, 0, N'', 2, N'PublicStatusDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="PublicStatusID" tokenized="False" content="False" id="d5c72829-bfaf-4ae2-9626-50697800d800" /><item searchable="False" name="PublicStatusName" tokenized="True" content="True" id="6d7f584e-b7a9-4700-b98f-88007906530f" /><item searchable="False" name="PublicStatusDisplayName" tokenized="True" content="True" id="ac847cb3-19bc-4cf7-b6a0-467148f03904" /><item searchable="True" name="PublicStatusEnabled" tokenized="False" content="False" id="897b5fa5-3853-4760-86a8-0a18e1e8f0d3" /><item searchable="False" name="PublicStatusGUID" tokenized="False" content="False" id="563b6218-1dc8-469d-a6bd-76ce82669e17" /><item searchable="True" name="PublicStatusLastModified" tokenized="False" content="False" id="a7d3e48c-3af0-4cae-ae8e-d4fbe82ba64e" /><item searchable="True" name="PublicStatusSiteID" tokenized="False" content="False" id="40c18e66-d40f-4c44-a1a9-8925cce9f6df" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1146, N'Ecommerce - Order status', N'ecommerce.orderstatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_OrderStatus">
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
              <xs:element name="StatusOrder" type="xs:int" minOccurs="0" />
              <xs:element name="StatusEnabled" type="xs:boolean" />
              <xs:element name="StatusColor" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="7" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatusGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="StatusLastModified" type="xs:dateTime" />
              <xs:element name="StatusSendNotification" type="xs:boolean" minOccurs="0" />
              <xs:element name="StatusSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="StatusOrderIsPaid" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_OrderStatus" />
      <xs:field xpath="StatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StatusID" fieldcaption="StatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0054dfd5-d032-4146-b5a1-b611202c552c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="StatusName" fieldcaption="StatusName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="44fa0a6c-5818-45ae-b066-e2526e5754e7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusDisplayName" fieldcaption="StatusDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fef2499a-8e05-4f98-aedc-4b6103d46383" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusOrder" fieldcaption="StatusOrder" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ecb76c5e-b8a5-488a-a57b-ee00951c578e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusEnabled" fieldcaption="StatusEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3eddb801-ea97-430d-a5f8-97a5e06cef87" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="StatusColor" fieldcaption="StatusColor" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a23641f6-08d0-40df-aa21-4dad99eff0ff" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="StatusGUID" fieldcaption="StatusGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="03ecad4a-071c-4810-85d9-a42e792c12b0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="StatusLastModified" fieldcaption="StatusLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c01407a-db06-418a-baf0-16a1c398ef57" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="StatusSendNotification" fieldcaption="StatusSendNotification" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9bc726eb-df8d-409e-82ac-7038abc35d6b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="StatusSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="5ca22366-8cd8-4ade-bcd0-50aab476506c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="StatusOrderIsPaid" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="941b9582-f90b-4216-9534-648c129725ad" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_OrderStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110516 14:54:41', 'da3a2138-096a-4554-9a64-afd4ef3e0b30', 0, 1, 0, N'', 2, N'StatusDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="StatusID" tokenized="False" content="False" id="ad82e6ba-847a-40e9-9a28-7228af1db2b9" /><item searchable="False" name="StatusName" tokenized="True" content="True" id="799aaa7f-4e38-4c21-a81b-5a296c000fc0" /><item searchable="False" name="StatusDisplayName" tokenized="True" content="True" id="783ffe0d-fc01-44b4-b744-33c62b2a6945" /><item searchable="True" name="StatusOrder" tokenized="False" content="False" id="62bbfe67-8a14-4900-8f7c-a432d4762866" /><item searchable="True" name="StatusEnabled" tokenized="False" content="False" id="27f4c7d9-a1f5-4dd9-9fe7-dd13dec5d9c0" /><item searchable="False" name="StatusColor" tokenized="True" content="True" id="862f3d6d-0e92-4a96-bb9f-baf5ba65fadc" /><item searchable="False" name="StatusGUID" tokenized="False" content="False" id="11be0615-e649-4b0a-b7ea-64d2bf18f870" /><item searchable="True" name="StatusLastModified" tokenized="False" content="False" id="6a41c6ae-11c6-4c81-91ae-427e4aa7c658" /><item searchable="True" name="StatusSendNotification" tokenized="False" content="False" id="bdb98f5a-b8f2-4fd3-a236-57765c3f48ec" /><item searchable="True" name="StatusSiteID" tokenized="False" content="False" id="76d07ebe-70f0-4a37-94b8-6bdb2b911897" /></search>', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1147, N'Ecommerce - Internal status', N'ecommerce.internalstatus', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_InternalStatus">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="InternalStatusID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="InternalStatusName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="InternalStatusDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="InternalStatusEnabled" type="xs:boolean" />
              <xs:element name="InternalStatusGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="InternalStatusLastModified" type="xs:dateTime" />
              <xs:element name="InternalStatusSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_InternalStatus" />
      <xs:field xpath="InternalStatusID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="InternalStatusID" fieldcaption="InternalStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4d9bc5f0-be40-458a-b037-cb2f25c812b8" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="InternalStatusName" fieldcaption="InternalStatusName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d9e2dd64-1886-40af-bbea-f0d722372dde" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="InternalStatusDisplayName" fieldcaption="InternalStatusDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c1a9a41-562d-4d26-a189-7356ef7608d0" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="InternalStatusEnabled" fieldcaption="InternalStatusEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f45be238-8cf2-4ddc-8bea-9be2914679a7" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="InternalStatusGUID" fieldcaption="InternalStatusGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d73f5d80-96da-4fa4-a642-220c585a6e15" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="InternalStatusLastModified" fieldcaption="InternalStatusLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad337396-1b43-44d4-ac68-cb6114e158d6" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="InternalStatusSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="fdbc49fb-8441-4da8-97e5-d24be3f91506" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_InternalStatus', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:03:38', '65ac5e46-2fbe-4c21-b123-687d3b54ac67', 0, 1, 0, N'', 2, N'InternalStatusDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="InternalStatusID" tokenized="False" content="False" id="3dc0ecad-2719-4738-b5b1-284a18997fbc" /><item searchable="False" name="InternalStatusName" tokenized="True" content="True" id="28628865-fb13-4556-bf5b-518f6014ffca" /><item searchable="False" name="InternalStatusDisplayName" tokenized="True" content="True" id="00c71b4a-b532-444b-b233-c75d7b5dc1b1" /><item searchable="True" name="InternalStatusEnabled" tokenized="False" content="False" id="c850fc9c-7cee-4aa4-9a55-304d6a1cbb72" /><item searchable="False" name="InternalStatusGUID" tokenized="False" content="False" id="28e5bdcc-f923-4ec0-b785-34a8b5a13663" /><item searchable="True" name="InternalStatusLastModified" tokenized="False" content="False" id="765e034b-d622-49eb-85e9-324fe8784c65" /><item searchable="True" name="InternalStatusSiteID" tokenized="False" content="False" id="7d428bde-c39c-4848-a375-e5eccbd4e353" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1148, N'Ecommerce - Department', N'ecommerce.department', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Department">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DepartmentID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="DepartmentName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DepartmentDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DepartmentDefaultTaxClassID" type="xs:int" minOccurs="0" />
              <xs:element name="DepartmentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="DepartmentLastModified" type="xs:dateTime" />
              <xs:element name="DepartmentSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Department" />
      <xs:field xpath="DepartmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DepartmentID" fieldcaption="DepartmentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="3d7b9c80-4c03-4312-a523-d86e3c03102d" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="DepartmentName" fieldcaption="DepartmentName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b5e1a6b9-cfcd-48ef-bcf8-0a7fa4a5e1b8" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DepartmentDisplayName" fieldcaption="DepartmentDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1a96bf6c-baaa-49c4-8a1f-8d34f80cbbf6" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DepartmentDefaultTaxClassID" fieldcaption="DepartmentDefaultTaxClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="addb3f11-2bfa-4747-8e86-3aaa8905658e" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DepartmentGUID" fieldcaption="DepartmentGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b7726ed9-96c7-4b3d-b36e-71200f682a89" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="DepartmentLastModified" fieldcaption="DepartmentLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="029ca9e8-e182-43bd-b944-d3bd4f8d5bc9" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DepartmentSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="af9978d0-baf3-4b26-af41-b825ff7cc908" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_Department', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:00:54', '1c05d8cc-e5b6-4477-b04c-2bcdf7f2ac84', 0, 1, 0, N'', 2, N'DepartmentDisplayName', N'0', N'', N'DepartmentLastModified', N'<search><item searchable="True" name="DepartmentID" tokenized="False" content="False" id="a4fe3b23-bb06-4993-9e1a-efac91f6b1c4" /><item searchable="False" name="DepartmentName" tokenized="True" content="True" id="7ac6d7d4-e984-4627-b52e-4fe4cbd2dadd" /><item searchable="False" name="DepartmentDisplayName" tokenized="True" content="True" id="c0caf40d-3af5-4b61-be9c-0df21e019206" /><item searchable="True" name="DepartmentDefaultTaxClassID" tokenized="False" content="False" id="1935167e-6bac-4816-8bcd-b5bf77ae0692" /><item searchable="False" name="DepartmentGUID" tokenized="False" content="False" id="da3a72ab-ffe3-4ca6-8d27-b114b2b2cb8e" /><item searchable="True" name="DepartmentLastModified" tokenized="False" content="False" id="5733a2e1-66f1-44ee-8c6c-616c82a70339" /><item searchable="True" name="DepartmentSiteID" tokenized="False" content="False" id="608d5eb4-2725-46da-99ff-43dff7a8082c" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1149, N'Ecommerce - Discount coupon', N'ecommerce.discountcoupon', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_DiscountCoupon">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DiscountCouponID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="DiscountCouponDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DiscountCouponIsExcluded" type="xs:boolean" />
              <xs:element name="DiscountCouponValidFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DiscountCouponValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DiscountCouponValue" type="xs:double" minOccurs="0" />
              <xs:element name="DiscountCouponIsFlatValue" type="xs:boolean" />
              <xs:element name="DiscountCouponCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DiscountCouponGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="DiscountCouponLastModified" type="xs:dateTime" />
              <xs:element name="DiscountCouponSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_DiscountCoupon" />
      <xs:field xpath="DiscountCouponID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DiscountCouponID" fieldcaption="DiscountCouponID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f325b7a0-e6e0-427e-ba5a-6e7e1197a7ba" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="DiscountCouponDisplayName" fieldcaption="DiscountCouponDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="30072228-a0df-40dc-8e75-2df26d8b4fc1" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DiscountCouponIsExcluded" fieldcaption="DiscountCouponIsExcluded" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d2d7e4b6-2225-4bac-9b51-b681ecc71f6b" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DiscountCouponValidFrom" fieldcaption="DiscountCouponValidFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="869851d3-7ba1-4373-97bf-edd987dcb27a" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DiscountCouponValidTo" fieldcaption="DiscountCouponValidTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2d107d85-ff21-46f7-95f5-600c5f07924b" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DiscountCouponValue" fieldcaption="DiscountCouponValue" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="76a9cd38-0d59-43b7-bdcc-5086f8856f7e" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DiscountCouponIsFlatValue" fieldcaption="DiscountCouponIsFlatValue" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2dfbc1f5-331a-4c71-b85b-c89ab250c55b" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DiscountCouponCode" fieldcaption="DiscountCouponCode" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="52d454b1-53da-425c-b24b-93e114ffc1d9" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DiscountCouponGUID" fieldcaption="DiscountCouponGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2e7733de-f0a1-4b82-ad47-c7f93ca0bc0a" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="DiscountCouponLastModified" fieldcaption="DiscountCouponLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b737ffd1-10c2-4059-a59f-31eae0aac802" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DiscountCouponSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="2e5a32ea-9669-49bf-8c21-7d5a5c1ede77" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_DiscountCoupon', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110405 17:37:11', 'ae9dba35-b6b5-45f4-bb1c-5125d15b3bb4', 0, 1, 0, N'', 2, N'DiscountCouponDisplayName', N'0', N'', N'DiscountCouponLastModified', N'<search><item searchable="True" name="DiscountCouponID" tokenized="False" content="False" id="2b14f092-1979-4546-8ea4-dec698433766" /><item searchable="False" name="DiscountCouponDisplayName" tokenized="True" content="True" id="204eca6a-69a5-4fc0-9c9d-28362b1050ed" /><item searchable="True" name="DiscountCouponIsExcluded" tokenized="False" content="False" id="8ac7badc-db21-431d-9467-6f6a5009539d" /><item searchable="True" name="DiscountCouponValidFrom" tokenized="False" content="False" id="56df5e1e-ffce-451e-8d0f-6bdef6fafe7c" /><item searchable="True" name="DiscountCouponValidTo" tokenized="False" content="False" id="2bd649ef-b921-4959-9e44-143892751405" /><item searchable="True" name="DiscountCouponValue" tokenized="False" content="False" id="599306e0-6dcd-432c-9027-fbc46198c417" /><item searchable="True" name="DiscountCouponIsFlatValue" tokenized="False" content="False" id="c5e7829f-b95b-49cd-8d8d-d215bc6e00d0" /><item searchable="False" name="DiscountCouponCode" tokenized="True" content="True" id="36efe5e7-4e36-4771-8d3e-87c00d0bba54" /><item searchable="False" name="DiscountCouponGUID" tokenized="False" content="False" id="54419afe-6151-4ba0-a1a9-03039da6055d" /><item searchable="True" name="DiscountCouponLastModified" tokenized="False" content="False" id="3dc0d4c2-1d3e-46f8-8ac7-cd6b931bbbc7" /><item searchable="True" name="DiscountCouponSiteID" tokenized="False" content="False" id="270a42ab-f237-4ac1-85e0-a75f33872380" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1150, N'Ecommerce - Shipping option', N'Ecommerce.ShippingOption', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_ShippingOption">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ShippingOptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ShippingOptionName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ShippingOptionDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ShippingOptionCharge" type="xs:double" />
              <xs:element name="ShippingOptionEnabled" type="xs:boolean" />
              <xs:element name="ShippingOptionSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="ShippingOptionGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ShippingOptionLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_ShippingOption" />
      <xs:field xpath="ShippingOptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ShippingOptionID" fieldcaption="ShippingOptionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="dc7a8f97-f1cb-4517-8031-489213a2445d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ShippingOptionName" fieldcaption="ShippingOptionName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9804163e-683a-4f71-91ed-d2b81643f483" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShippingOptionDisplayName" fieldcaption="ShippingOptionDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="765b2b6e-52b1-41c9-ab12-ec94b3ef6ad7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShippingOptionCharge" fieldcaption="ShippingOptionCharge" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a22dba60-33bc-4d6a-8ad3-88c1f3459f49" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShippingOptionEnabled" fieldcaption="ShippingOptionEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0fe5eaa7-6d54-4c76-90c5-376b35a11f9d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ShippingOptionSiteID" fieldcaption="ShippingOptionSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd1414cc-26d1-4d35-8dfe-6d6c0edf3a92" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShippingOptionGUID" fieldcaption="ShippingOptionGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="18dd26bf-7cc3-4d53-9cd4-b3cb5df860da" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="ShippingOptionLastModified" fieldcaption="ShippingOptionLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="949f7f46-fd67-4cc3-8942-77064b1924cf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_ShippingOption', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110405 17:19:56', 'b556c066-57d3-4400-b601-78bb4f527447', 0, 1, 0, N'', 2, N'ShippingOptionDisplayName', N'0', N'', N'ShippingOptionLastModified', N'<search><item searchable="True" name="ShippingOptionID" tokenized="False" content="False" id="37bba34d-ce23-44b0-8dd3-49a2a73fbf52" /><item searchable="False" name="ShippingOptionName" tokenized="True" content="True" id="bb8ce5a0-8ee2-4b00-98c1-ac488b108a73" /><item searchable="False" name="ShippingOptionDisplayName" tokenized="True" content="True" id="a6d290f6-04fb-4fb0-a1fa-b90218c01a7a" /><item searchable="True" name="ShippingOptionCharge" tokenized="False" content="False" id="58b7ead9-2038-4b2d-9731-1968cc7bf09d" /><item searchable="True" name="ShippingOptionEnabled" tokenized="False" content="False" id="601e8ce1-c959-45f7-816a-e0996264ddd6" /><item searchable="True" name="ShippingOptionSiteID" tokenized="False" content="False" id="3097d44e-cc79-427e-bbe6-229e0d592933" /><item searchable="False" name="ShippingOptionGUID" tokenized="False" content="False" id="299f630d-910c-4070-aa4c-2206fdab632a" /><item searchable="True" name="ShippingOptionLastModified" tokenized="False" content="False" id="3cf17e19-481d-47a2-bc1a-f8c834f6bd46" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1151, N'Ecommerce - Exchange table', N'ecommerce.exchangetable', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_ExchangeTable">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ExchangeTableID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ExchangeTableDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ExchangeTableValidFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ExchangeTableValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="ExchangeTableGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ExchangeTableLastModified" type="xs:dateTime" />
              <xs:element name="ExchangeTableSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="ExchangeTableRateFromGlobalCurrency" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_ExchangeTable" />
      <xs:field xpath="ExchangeTableID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ExchangeTableID" fieldcaption="ExchangeTableID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0598477f-1a96-4453-a8bc-5d4a4d374a47" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ExchangeTableDisplayName" fieldcaption="ExchangeTableDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b9524920-c9cd-4861-b338-b6a6c91cf70e" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ExchangeTableValidFrom" fieldcaption="ExchangeTableValidFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad6205d5-899a-49f8-8760-8ac47c5c48c3" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ExchangeTableValidTo" fieldcaption="ExchangeTableValidTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bff8a62c-8e74-413e-b319-d22e2490a4e6" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ExchangeTableGUID" fieldcaption="ExchangeTableGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1a96357c-6911-4b5b-bb22-ec5cc29a7476" ismacro="false" hasdependingfields="false"><settings><controlname>unknown</controlname></settings></field><field column="ExchangeTableLastModified" fieldcaption="ExchangeTableLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1c59db57-7b91-4cc5-aac3-8d6a69dd3e58" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ExchangeTableSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="0d5d6f91-4d30-468e-b5d0-6dd6278b027d" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="ExchangeTableRateFromGlobalCurrency" fieldcaption="ExchangeTableRateFromGlobalCurrency" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="977e8a84-1be7-4c6c-a9fe-92d382bd8b38" visibility="none" ismacro="false" hasdependingfields="false"><settings><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_ExchangeTable', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110401 15:28:35', 'f13d168a-10fd-4202-a9f6-cc0c2c61b4ce', 0, 1, 0, N'', 2, N'ExchangeTableDisplayName', N'0', N'', N'ExchangeTableValidFrom', N'<search><item searchable="True" name="ExchangeTableID" tokenized="False" content="False" id="bc868cc3-1a55-4d15-87ba-d5e4aa9e47e8" /><item searchable="False" name="ExchangeTableDisplayName" tokenized="True" content="True" id="128cd09c-6e71-4e1b-b7af-f2aa7f7bdd5e" /><item searchable="True" name="ExchangeTableValidFrom" tokenized="False" content="False" id="3748dd5a-031b-4be2-a16c-3269a1a9eb0f" /><item searchable="True" name="ExchangeTableValidTo" tokenized="False" content="False" id="1d855c2c-4de9-412f-a2b8-29a09bd7df66" /><item searchable="False" name="ExchangeTableGUID" tokenized="False" content="False" id="5e69cfde-8cff-4120-9779-ccb92e4ba979" /><item searchable="True" name="ExchangeTableLastModified" tokenized="False" content="False" id="eaf5fd39-6886-4341-a5a5-99304733d460" /><item searchable="True" name="ExchangeTableSiteID" tokenized="False" content="False" id="4b772b43-9877-41b5-9021-0466a9c8b0c4" /><item searchable="True" name="ExchangeTableRateFromGlobalCurrency" tokenized="False" content="False" id="7f80fc3b-c906-4856-820a-4bfb05fa4845" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1152, N'Ecommerce - Tax class', N'ecommerce.taxclass', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_TaxClass">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaxClassID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaxClassName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaxClassDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TaxClassZeroIfIDSupplied" type="xs:boolean" minOccurs="0" />
              <xs:element name="TaxClassGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TaxClassLastModified" type="xs:dateTime" />
              <xs:element name="TaxClassSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_TaxClass" />
      <xs:field xpath="TaxClassID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaxClassID" fieldcaption="TaxClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="120dba86-26f2-4062-87b9-c2df858fea8d" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="TaxClassName" fieldcaption="TaxClassName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3a64b5fd-d188-4bb7-83f5-8644669e296a" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaxClassDisplayName" fieldcaption="TaxClassDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="170b4fb7-d206-4bb8-87fa-c132a3dd3193" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaxClassZeroIfIDSupplied" fieldcaption="TaxClassZeroIfIDSupplied" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3d8003fd-ab1c-4f33-a7f8-5b060cecfad8" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="TaxClassGUID" fieldcaption="TaxClassGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3627e0db-1184-4d5d-a27f-1e11eff3ffe3" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="TaxClassLastModified" fieldcaption="TaxClassLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e5339651-4379-490c-9fea-c380ed074213" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="TaxClassSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="9a74e039-c6a3-41bd-9c4b-e705e7534109" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_TaxClass', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110909 16:05:59', 'b4fa4dd9-d561-463e-8aa9-1c3cc4550ac2', 0, 1, 0, N'', 2, N'TaxClassDisplayName', N'0', N'', N'0', N'<search><item searchable="True" name="TaxClassID" tokenized="False" content="False" id="abd55122-4c05-43f9-a1ef-979bbc8ed2ad" /><item searchable="False" name="TaxClassName" tokenized="True" content="True" id="169d5965-16b3-4c0e-ba81-2bb474ac6612" /><item searchable="False" name="TaxClassDisplayName" tokenized="True" content="True" id="e38edc2a-72b9-4fde-b486-fbdf4085ed5d" /><item searchable="True" name="TaxClassZeroIfIDSupplied" tokenized="False" content="False" id="50eddfb5-6e21-4801-88fc-6155c76f19b4" /><item searchable="False" name="TaxClassGUID" tokenized="False" content="False" id="70e7f750-205e-4799-b58f-6f90bebe750d" /><item searchable="True" name="TaxClassLastModified" tokenized="False" content="False" id="e2e4c202-6409-4735-bf6c-935432f0908e" /><item searchable="True" name="TaxClassSiteID" tokenized="False" content="False" id="ad778182-320b-4010-b88d-88aab477b6d9" /></search>', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1154, N'Ecommerce - Tax class country', N'Ecommerce.TaxClassCountry', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_TaxClassCountry">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaxClassCountryID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaxClassID" type="xs:int" />
              <xs:element name="CountryID" type="xs:int" />
              <xs:element name="TaxValue" type="xs:double" />
              <xs:element name="IsFlatValue" type="xs:boolean" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_TaxClassCountry" />
      <xs:field xpath="TaxClassCountryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaxClassCountryID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="59560247-2e15-4714-9444-2e694cef028f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="TaxClassID" fieldcaption="TaxClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9867db8a-cff3-4795-a695-3cb52f1bff4e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CountryID" fieldcaption="CountryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6304fe0c-face-498e-a72e-4a1bc2eccd24" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaxValue" fieldcaption="TaxValue" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7671e12b-8ab1-4438-b1f9-947bba3cfb48" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IsFlatValue" fieldcaption="IsFlatValue" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dcd43c73-4dc1-47cd-9391-11349a5aa35c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_TaxClassCountry', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110428 13:56:33', '87c1d77d-21ea-4629-ad2a-eaab556602f7', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1156, N'Ecommerce - Address', N'Ecommerce.Address', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Address">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AddressID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AddressName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressLine1">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressLine2">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressCity">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressZip">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="20" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressPhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressCustomerID" type="xs:int" />
              <xs:element name="AddressCountryID" type="xs:int" />
              <xs:element name="AddressStateID" type="xs:int" minOccurs="0" />
              <xs:element name="AddressIsBilling" type="xs:boolean" />
              <xs:element name="AddressEnabled" type="xs:boolean" />
              <xs:element name="AddressPersonalName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AddressIsShipping" type="xs:boolean" />
              <xs:element name="AddressIsCompany" type="xs:boolean" minOccurs="0" />
              <xs:element name="AddressGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="AddressLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Address" />
      <xs:field xpath="AddressID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AddressID" fieldcaption="AddressID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="45acf5f0-2a7d-407b-996b-438274529c9a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AddressName" fieldcaption="AddressName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5e42b159-00f3-4933-85ab-73f3de18a8ee" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressLine1" fieldcaption="AddressLine1" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="650bc95e-0923-4cbe-b83c-4a250bfe5505" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressLine2" fieldcaption="AddressLine2" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6e25541d-4592-426d-9ace-eb8c4f9903aa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressCity" fieldcaption="AddressCity" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0633aba6-62bd-4184-ae5f-5ad85e562358" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressZip" fieldcaption="AddressZip" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d036387c-702f-437f-9785-218dac48ad3c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressPhone" fieldcaption="AddressPhone" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5e86531-d896-4784-a7ce-46e4e7563fa2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressCustomerID" fieldcaption="AddressCustomerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="26fe2b8e-a2c2-40de-8fe6-a2f97dd5e171" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressCountryID" fieldcaption="AddressCountryID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8a967728-36e1-4eb9-964c-a91c8a557594" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressStateID" fieldcaption="AddressStateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ac508421-fa90-4e76-9387-06b5af8df17b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressIsBilling" fieldcaption="AddressIsBilling" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b4dbab58-4ce0-46cd-936e-889a5dc45540" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AddressEnabled" fieldcaption="AddressEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4dcca810-6afe-46ca-8d38-9633fcf374b4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AddressPersonalName" fieldcaption="AddressPersonalName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8879c6fb-f4ae-4802-9d82-82fd3d074124" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AddressIsShipping" fieldcaption="AddressIsShipping" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cfe91c20-eda4-4e6a-86b3-6579d973ef2b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AddressIsCompany" fieldcaption="AddressIsCompany" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="558c8a0d-7328-4cd8-bed2-82a76514c24f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AddressGUID" fieldcaption="AddressGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f038c0a7-59ae-477e-b9c6-5dfc9daedf3e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AddressLastModified" fieldcaption="AddressLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="33264671-3b18-4eb9-b7fc-d96c007adb37" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_Address', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110401 08:04:34', '3cc96754-268c-4eeb-94cf-44204a27431a', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 1, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1157, N'Ecommerce - Customer', N'ecommerce.customer', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Customer">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CustomerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CustomerFirstName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerLastName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerPhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerFax" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerCompany" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerUserID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerPreferredCurrencyID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerPreferredShippingOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerCountryID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerEnabled" type="xs:boolean" />
              <xs:element name="CustomerPrefferedPaymentOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerStateID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CustomerTaxRegistrationID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerOrganizationID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CustomerDiscountLevelID" type="xs:int" minOccurs="0" />
              <xs:element name="CustomerCreated" type="xs:dateTime" minOccurs="0" />
              <xs:element name="CustomerLastModified" type="xs:dateTime" />
              <xs:element name="CustomerSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Customer" />
      <xs:field xpath="CustomerID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CustomerID" fieldcaption="CustomerID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="96c3731e-c8dc-4bb7-b692-120b5e92e67d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CustomerFirstName" fieldcaption="CustomerFirstName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="b2b44dc5-ae13-4180-9fad-7a47431b9b36" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerLastName" fieldcaption="CustomerLastName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="f1470954-5eed-49a9-91ae-de257fc8cc6b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerEmail" fieldcaption="CustomerEmail" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ed57af72-8df1-456f-b369-041c7faf0085" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerPhone" fieldcaption="CustomerPhone" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="ec61f54e-1202-4e42-8646-30f067f38eb2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerFax" fieldcaption="CustomerFax" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="044422b8-46d8-4222-b0f7-9466783c2274" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerCompany" fieldcaption="CustomerCompany" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="0b263893-c00f-44f7-a36d-ca0449ac9f0a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerUserID" fieldcaption="CustomerUserID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="54062fca-65ff-4995-9b7a-404ca2e04f58" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerPreferredCurrencyID" fieldcaption="CustomerPreferredCurrencyID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3142d83c-af78-484f-9867-946339d66fba" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerPreferredShippingOptionID" fieldcaption="CustomerPreferredShippingOptionID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8217b461-c402-41b2-8923-1364b6468ba2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerCountryID" fieldcaption="CustomerCountryID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="80182aa0-ae56-432a-8f66-51ea1ec857ac" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerEnabled" fieldcaption="CustomerEnabled" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="86754dcb-f60a-490c-916e-c3e51d63cc38" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="CustomerPrefferedPaymentOptionID" fieldcaption="CustomerPrefferedPaymentOptionID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0bc8e195-ea84-4474-bede-ed7bbdd1ad04" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerStateID" fieldcaption="CustomerStateID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e2feb737-5110-40db-b535-12079051379e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="56206b17-fdf9-4953-9136-6e91e18d6d4d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="CustomerTaxRegistrationID" fieldcaption="CustomerTaxRegistrationID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="873dce9d-ee95-4793-a1ed-963a3feeee42" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerOrganizationID" fieldcaption="CustomerOrganizationID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="e7df891f-ea59-4add-876b-69d4a619ac12" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerDiscountLevelID" fieldcaption="CustomerDiscountLevelID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7fbca01a-61af-4362-a273-e2aa8a3a454c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="CustomerLastModified" fieldcaption="CustomerLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="87a1a570-2285-457b-9508-d541995a7769" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="CustomerSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="b986d250-cc3e-404f-86f2-987f25770524" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_Customer', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20120208 09:50:33', 'cd867311-743a-4599-ba72-5fe29b1c4a9c', 0, 1, 0, N'', 2, N'CustomerID', N'0', N'', N'0', N'<search><item searchable="True" name="CustomerID" tokenized="False" content="False" id="5ec636e6-a399-4970-9a67-6a8069198ca1" /><item searchable="False" name="CustomerFirstName" tokenized="True" content="True" id="77c67bcd-9c66-4a8e-82c5-64c42cf7b169" /><item searchable="False" name="CustomerLastName" tokenized="True" content="True" id="5bf27f2b-6785-4ff3-9787-4b97116b7bbd" /><item searchable="False" name="CustomerEmail" tokenized="True" content="True" id="111c4a7f-292c-4a7c-9ef1-e4874870af2f" /><item searchable="False" name="CustomerPhone" tokenized="True" content="True" id="8633ad64-b248-40bd-9727-654cd45ce65c" /><item searchable="False" name="CustomerFax" tokenized="True" content="True" id="4f34fc10-3289-48e9-8547-8d63682ecb4b" /><item searchable="False" name="CustomerCompany" tokenized="True" content="True" id="db833152-c2de-4c69-afff-56fc46c4fcfe" /><item searchable="True" name="CustomerUserID" tokenized="False" content="False" id="e3a09419-04c5-4970-8d22-3a4d22c914e8" /><item searchable="True" name="CustomerPreferredCurrencyID" tokenized="False" content="False" id="c755f1e1-6e94-48e7-b872-91185e6489ce" /><item searchable="True" name="CustomerPreferredShippingOptionID" tokenized="False" content="False" id="e2979431-6cd8-4861-8795-f92d16b53e4d" /><item searchable="True" name="CustomerCountryID" tokenized="False" content="False" id="bb4fe818-52ee-4ddb-992a-69c44d567286" /><item searchable="True" name="CustomerEnabled" tokenized="False" content="False" id="fe1dd2d8-b80c-46f8-a363-55f10a4d39cf" /><item searchable="True" name="CustomerPrefferedPaymentOptionID" tokenized="False" content="False" id="0eb76742-d24d-4818-825e-1bd8b5163f3a" /><item searchable="True" name="CustomerStateID" tokenized="False" content="False" id="d60fdec0-da6e-466b-9b39-eaa055d90164" /><item searchable="False" name="CustomerGUID" tokenized="True" content="True" id="1c3ee168-d69e-4dd0-891a-e712d5b5d41e" /><item searchable="False" name="CustomerTaxRegistrationID" tokenized="True" content="True" id="7b02b7b2-5207-4753-9e09-8312e6fd448b" /><item searchable="False" name="CustomerOrganizationID" tokenized="True" content="True" id="37206195-2d56-4856-8557-9ca836c02fb7" /><item searchable="True" name="CustomerDiscountLevelID" tokenized="False" content="False" id="a6135c81-5917-42be-a89b-53b9d80b1688" /><item searchable="True" name="CustomerLastModified" tokenized="False" content="False" id="23f0d85c-678b-4ac0-a69e-d8ad7e4e1058" /><item searchable="True" name="CustomerSiteID" tokenized="False" content="False" id="10f52642-f19e-4eb5-ae5c-7f54791e9987" /></search>', NULL, 1, N'', NULL, N'<form><field column="ContactCountryID" mappedtofield="CustomerCountryID" /><field column="ContactEmail" mappedtofield="CustomerEmail" /><field column="ContactFirstName" mappedtofield="CustomerFirstName" /><field column="ContactLastName" mappedtofield="CustomerLastName" /><field column="ContactMobilePhone" mappedtofield="CustomerPhone" /><field column="ContactStateID" mappedtofield="CustomerStateID" /></form>', 0, N'PRODUCT')
SET IDENTITY_INSERT [CMS_Class] OFF
