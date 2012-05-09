SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1750, N'Avatar', N'cms.avatar', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Avatar">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AvatarID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AvatarName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AvatarFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AvatarFileExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AvatarBinary" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="AvatarType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AvatarIsCustom" type="xs:boolean" />
              <xs:element name="AvatarGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AvatarLastModified" type="xs:dateTime" />
              <xs:element name="AvatarMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AvatarFileSize" type="xs:int" />
              <xs:element name="AvatarImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="AvatarImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="DefaultMaleUserAvatar" type="xs:boolean" minOccurs="0" />
              <xs:element name="DefaultFemaleUserAvatar" type="xs:boolean" minOccurs="0" />
              <xs:element name="DefaultGroupAvatar" type="xs:boolean" minOccurs="0" />
              <xs:element name="DefaultUserAvatar" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Avatar" />
      <xs:field xpath="AvatarID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AvatarID" fieldcaption="AvatarID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="070e07c2-613b-4ec3-b253-85490b5c2123" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AvatarName" fieldcaption="AvatarName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="fe64d27a-a61c-4f3f-af58-d61e35faa192" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarFileName" fieldcaption="AvatarFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="c57fbf46-2904-4329-b081-7be3ff62e105" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarFileExtension" fieldcaption="AvatarFileExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="44852334-d234-4cb6-84db-d8f96b5a0411" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarBinary" fieldcaption="AvatarBinary" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d1455dfc-5c3c-4e66-a012-b567b76d20b1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarType" fieldcaption="AvatarType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="eb09494b-aa8f-455f-8282-0f305be2c4a0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarIsCustom" fieldcaption="AvatarIsCustom" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="824bb523-a40b-4c36-9b61-55cbd38e1572" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="AvatarGUID" fieldcaption="AvatarGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="76a41f68-0412-46ef-9a71-f3b0b23f3c14" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AvatarLastModified" fieldcaption="AvatarLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a0df33d7-2ea5-4ab1-8204-0b97d6b4bd40" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="AvatarMimeType" fieldcaption="AvatarMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="f54b64b6-e474-48f8-82bf-d774a84a0134" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarFileSize" fieldcaption="AvatarFileSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cc46dcfc-ffb2-4901-9d99-1a066d74113c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarImageHeight" fieldcaption="AvatarImageHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1c43a226-2790-4c55-9bd1-69a5d738f3fb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AvatarImageWidth" fieldcaption="AvatarImageWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6df10f1e-1218-4cda-a594-1f991be5806c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DefaultMaleUserAvatar" fieldcaption="DefaultMaleUserAvatar" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4904d149-1611-444c-8306-2f2d3dcecd7d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DefaultFemaleUserAvatar" fieldcaption="DefaultFemaleUserAvatar" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="70d2f645-011d-49d3-8740-bf9da38746bc" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DefaultGroupAvatar" fieldcaption="DefaultGroupAvatar" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="673ae3c3-2bfd-41c8-9747-58da08d6ca7b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DefaultUserAvatar" fieldcaption="DefaultUserAvatar" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5462d085-166f-4a76-a5e7-c5f912bf7538" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_Avatar', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110607 08:54:16', 'ee73a58a-898b-4ca4-9abf-442380daae84', 0, 0, 0, N'', 0, N'AvatarName', N'0', N'', N'AvatarLastModified', N'<search><item searchable="True" name="AvatarID" tokenized="False" content="False" id="00432b34-0b00-4b37-8701-cb058e8a432e" /><item searchable="False" name="AvatarName" tokenized="True" content="True" id="9f150fe2-1832-4c9f-b8b7-2b5a4c1c7f4b" /><item searchable="False" name="AvatarFileName" tokenized="True" content="True" id="2d5942b9-0105-4a75-ad8f-c9fefcaba874" /><item searchable="False" name="AvatarFileExtension" tokenized="True" content="True" id="a0e6d59d-cad3-484c-8d2b-9aa05e6e4e97" /><item searchable="False" name="AvatarBinary" tokenized="True" content="True" id="ac287cf5-9ee9-4896-8409-0178295c4114" /><item searchable="False" name="AvatarType" tokenized="True" content="True" id="8f73cc29-e538-434e-ad1e-79252ccba2b3" /><item searchable="True" name="AvatarIsCustom" tokenized="False" content="False" id="568a4839-9697-401b-81ab-ac76052a0796" /><item searchable="False" name="AvatarGUID" tokenized="False" content="False" id="28ec954d-2010-4bbc-8da6-4b190e35817c" /><item searchable="True" name="AvatarLastModified" tokenized="False" content="False" id="6f727a3c-c1b5-4be4-ab5a-160f61854d77" /><item searchable="False" name="AvatarMimeType" tokenized="True" content="True" id="01b20a16-e68c-4e1c-a803-83a6b9caa939" /><item searchable="True" name="AvatarFileSize" tokenized="False" content="False" id="665a938a-970c-4839-979d-9ea908f4697d" /><item searchable="True" name="AvatarImageHeight" tokenized="False" content="False" id="39a7cf34-ab16-45cb-b600-87ab484ebf7e" /><item searchable="True" name="AvatarImageWidth" tokenized="False" content="False" id="59351866-a463-4ef4-b7cd-79f0fbff6af5" /><item searchable="True" name="DefaultMaleUserAvatar" tokenized="False" content="False" id="d86e0021-c746-48d1-8743-d509bf6c5889" /><item searchable="True" name="DefaultFemaleUserAvatar" tokenized="False" content="False" id="84ff4121-a709-42c8-bd3a-b9853398662f" /><item searchable="True" name="DefaultGroupAvatar" tokenized="False" content="False" id="39f2242c-6f7e-4004-bce5-be5c7d80a3cf" /><item searchable="True" name="DefaultUserAvatar" tokenized="False" content="False" id="08f0b2d5-2267-4822-9de0-e0f9203fb25c" /></search>', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1768, N'User - Settings', N'cms.usersettings', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_UserSettings">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="UserSettingsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UserNickName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserPicture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserSignature" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserURLReferrer" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserCampaign" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserMessagingNotificationEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserRegistrationInfo" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserPreferences" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserActivationDate" type="xs:dateTime" minOccurs="0" />
              <xs:element name="UserActivatedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="UserTimeZoneID" type="xs:int" minOccurs="0" />
              <xs:element name="UserAvatarID" type="xs:int" minOccurs="0" />
              <xs:element name="UserBadgeID" type="xs:int" minOccurs="0" />
              <xs:element name="UserShowSplashScreen" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserActivityPoints" type="xs:int" minOccurs="0" />
              <xs:element name="UserForumPosts" type="xs:int" minOccurs="0" />
              <xs:element name="UserBlogComments" type="xs:int" minOccurs="0" />
              <xs:element name="UserGender" type="xs:int" minOccurs="0" />
              <xs:element name="UserDateOfBirth" type="xs:dateTime" minOccurs="0" />
              <xs:element name="UserMessageBoardPosts" type="xs:int" minOccurs="0" />
              <xs:element name="UserSettingsUserGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="UserSettingsUserID" type="xs:int" />
              <xs:element name="WindowsLiveID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserBlogPosts" type="xs:int" minOccurs="0" />
              <xs:element name="UserWaitingForApproval" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserDialogsConfiguration" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserUsedWebParts" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserUsedWidgets" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserFacebookID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserAuthenticationGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="UserSkype" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserIM" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserPhone" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="26" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserPosition" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserBounces" type="xs:int" minOccurs="0" />
              <xs:element name="UserLinkedInID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="UserLogActivities" type="xs:boolean" minOccurs="0" />
              <xs:element name="UserPasswordRequestHash" minOccurs="0">
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
      <xs:selector xpath=".//CMS_UserSettings" />
      <xs:field xpath="UserSettingsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="UserSettingsID" fieldcaption="UserSettingsID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f88177e0-3b3e-4f62-9ceb-1338a5a0722a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserNickName" fieldcaption="UserNickName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="efb1a8cb-a54b-4362-ba8e-f759e1ee834f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserPicture" fieldcaption="UserPicture" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="ea2ec595-2e19-441e-88cf-f7c1181867ff" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserSignature" fieldcaption="UserSignature" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="028e7124-d2eb-4ee9-becf-60b6a2281521" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="UserURLReferrer" fieldcaption="UserURLReferrer" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="df3044e3-28fe-4b46-bb07-37428f5d833f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserCampaign" fieldcaption="UserCampaign" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="c4e77a91-8804-4956-a27d-dd89ddf30b3c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserMessagingNotificationEmail" fieldcaption="UserMessagingNotificationEmail" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="8968e8bd-9223-41dd-831f-586632285df1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserCustomData" fieldcaption="UserCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="34e7ab20-5aab-4305-96da-ba828c4eafd0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserRegistrationInfo" fieldcaption="UserRegistrationInfo" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b254202a-cd95-49cf-8002-422c37fbb311" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserPreferences" fieldcaption="UserPreferences" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8e07e797-b695-4ddf-86d1-bfff4e3028f8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserActivationDate" fieldcaption="UserActivationDate" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4b9bf86d-4b85-4d32-9599-c49a3ca30687" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserActivatedByUserID" fieldcaption="UserActivatedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02ee7d84-59b9-4c6d-9505-23ef5620f70e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserTimeZoneID" fieldcaption="UserTimeZoneID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b6c65099-1e1d-4ad7-a540-d872aae4a3bf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserAvatarID" fieldcaption="UserAvatarID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7df4af3f-bd8e-49d2-814c-0a00613120bb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserBadgeID" fieldcaption="UserBadgeID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cf437fae-27be-4b53-af62-55975f3b0785" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserShowSplashScreen" fieldcaption="UserShowSplashScreen" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="375bae6f-885c-455e-bd12-a254b525a48c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserActivityPoints" fieldcaption="UserActivityPoints" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fc8a6be4-44ae-42a8-b6c0-be43147e2d33" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserForumPosts" fieldcaption="UserForumPosts" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="448e7d8f-a886-46a6-9be5-74b22fda0eb2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserBlogComments" fieldcaption="UserBlogComments" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69691713-f8ee-4104-a0d1-db52fddec111" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserGender" fieldcaption="UserGender" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bb81189b-8090-4d61-8e71-bb2a40e97ef6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserDateOfBirth" fieldcaption="UserDateOfBirth" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0000a8ba-40fb-4e91-8a1f-90e4913f0aec" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="UserMessageBoardPosts" fieldcaption="UserMessageBoardPosts" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4107fe8b-72a5-43bb-91c2-c5850262c303" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserSettingsUserGUID" fieldcaption="UserSettingsUserGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca94536c-9d77-4451-9cad-e0b13f672e09" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserSettingsUserID" fieldcaption="UserSettingsUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="821e0a34-28a4-4b59-8b3a-d3afcf53ed96" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WindowsLiveID" fieldcaption="WindowsLiveID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="6cfd72b6-214c-4018-a970-284d411341ca" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserBlogPosts" fieldcaption="UserBlogPosts" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bfb792b5-40a1-491b-9c83-255e04b31f68" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserWaitingForApproval" fieldcaption="UserWaitingForApproval" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea1dde05-b660-4fee-bac7-3475cb559340" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserDialogsConfiguration" fieldcaption="UserDialogsConfiguration" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="11e15a34-e0de-43cd-9cbe-74e26a1bd367" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserDescription" fieldcaption="UserDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01309c8c-b144-4662-8534-ca68fcdda40d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserUsedWebParts" fieldcaption="UserUsedWebParts" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="1000" publicfield="false" spellcheck="true" guid="f1a33942-073f-4e54-bccc-2b13f05d1a80" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserUsedWidgets" fieldcaption="UserUsedWidgets" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="1000" publicfield="false" spellcheck="true" guid="9e0b3fb8-d9f1-40a3-adf9-460dd19ab044" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserFacebookID" fieldcaption="UserFacebookID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="4971b905-286a-4e79-a1e8-454763b91a3d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserLinkedInID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="72604a87-5d1d-4d70-a9c7-b16596f0416c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field column="UserAuthenticationGUID" fieldcaption="UserAuthenticationGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="75360e04-4c1f-44e0-b8f4-bc154e395e3d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserSkype" fieldcaption="UserBounces" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="false" guid="882a11c9-192a-4b38-81dd-3ee5b5e2477e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserIM" fieldcaption="UserBounces" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="false" guid="9634d746-a4db-44ce-930c-9c37ff5097a1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserPhone" fieldcaption="UserBounces" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="26" publicfield="false" spellcheck="false" guid="f79c705a-da88-43d5-93a7-3d7d5ab0f281" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserPosition" fieldcaption="UserBounces" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="false" guid="dbf94b10-7262-4d2c-8f6d-887bd8e7e7bc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserBounces" fieldcaption="UserBounces" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="3f06ef91-12eb-4d65-b25f-62d27b6bdd70" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field><field column="UserLogActivities" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aa46b973-7036-4b71-aa50-b351a5658965" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="UserPasswordRequestHash" fieldcaption="UserSettingsID" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="734de788-ae15-4c25-88c6-1ec0c8cbfa87" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_UserSettings', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110713 09:32:36', 'a8203aeb-7709-4079-ad7e-3f9fccadc929', 0, 0, 0, N'', 0, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, 0, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1771, N'Bad word', N'badwords.word', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="BadWords_Word">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WordID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="WordGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="WordLastModified" type="xs:dateTime" />
              <xs:element name="WordExpression">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WordReplacement" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="WordAction" type="xs:int" minOccurs="0" />
              <xs:element name="WordIsGlobal" type="xs:boolean" />
              <xs:element name="WordIsRegularExpression" type="xs:boolean" />
              <xs:element name="WordMatchWholeWord" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//BadWords_Word" />
      <xs:field xpath="WordID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WordID" fieldcaption="WordID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e55fab30-93b1-4726-ac24-70db1744bd52" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="WordGUID" fieldcaption="WordGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bfab8943-80c4-4a8f-9edd-4058567f964f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="WordLastModified" fieldcaption="WordLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="66a1e9e5-27e1-4807-9138-edf7f19543f8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="WordExpression" fieldcaption="WordExpression" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="06c844fc-a206-4ce9-8475-46235c72007b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WordReplacement" fieldcaption="WordReplacement" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="43afe0be-68dd-4e9d-8b52-196416a9743f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WordAction" fieldcaption="WordAction" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="362cfe9c-423a-40f0-8918-2529c1bc7d02" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="WordIsGlobal" fieldcaption="WordIsGlobal" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4043c1a4-0979-4cb4-9956-3210e752b9f3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="WordIsRegularExpression" fieldcaption="Word is regular expression" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9c891be9-e869-4041-8e50-fb10c51d25ce" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="WordMatchWholeWord" fieldcaption="WordMatchWholeWord" visible="true" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="085a4c0c-7a6f-42f1-949e-7735f013eb95" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'BadWords_Word', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:07:40', '9ff65ce7-ed48-48ea-97d6-cf88e644a10e', 0, 0, 0, N'', 2, N'WordExpression', N'0', N'', N'WordLastModified', N'<search><item searchable="True" name="WordID" tokenized="False" content="False" id="699331a8-6ece-42a2-8e0f-5a9ceff6f0e8" /><item searchable="False" name="WordGUID" tokenized="False" content="False" id="0ed6f07d-c57d-4969-a963-0c8fd011e4f1" /><item searchable="True" name="WordLastModified" tokenized="False" content="False" id="72928fad-8dd8-4ae8-b629-ac662c2a93ef" /><item searchable="False" name="WordExpression" tokenized="True" content="True" id="cfb63391-a9cb-4355-9b41-4e85d37bea38" /><item searchable="False" name="WordReplacement" tokenized="True" content="True" id="c2c5c0ac-a8b8-444f-8932-a1d34950f0a1" /><item searchable="True" name="WordAction" tokenized="False" content="False" id="5f3a3e59-4d41-43eb-ab8e-9f0b23e5bba3" /><item searchable="True" name="WordIsGlobal" tokenized="False" content="False" id="0495906d-23b4-4c91-b14c-fde05fbfcafb" /><item searchable="True" name="WordIsRegularExpression" tokenized="False" content="False" id="57beaafb-b275-4f1b-884f-a91df7efd4c7" /><item searchable="True" name="WordMatchWholeWord" tokenized="False" content="False" id="7ca71a34-76a3-4615-a9c1-64df53c84056" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1787, N'Badge', N'CMS.Badge', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Badge">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="BadgeID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="BadgeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BadgeDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BadgeImageURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BadgeIsAutomatic" type="xs:boolean" />
              <xs:element name="BadgeTopLimit" type="xs:int" minOccurs="0" />
              <xs:element name="BadgeGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="BadgeLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Badge" />
      <xs:field xpath="BadgeID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="BadgeID" fieldcaption="BadgeID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e6c959c1-3f56-4b98-9ba1-56df31742a3c" visibility="none" /><field column="BadgeName" fieldcaption="BadgeName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="8f7f989e-3115-43d5-afce-2c382577893f" /><field column="BadgeDisplayName" fieldcaption="BadgeDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="8b346663-f678-419d-9c5e-3de2bb247f33" /><field column="BadgeImageURL" fieldcaption="BadgeImageURL" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="be04c9a0-4afe-4ad6-8a71-d17ef454968b" /><field column="BadgeIsAutomatic" fieldcaption="BadgeIsAutomatic" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3c65408f-702f-4556-badd-c2d0f813cf36" /><field column="BadgeTopLimit" fieldcaption="BadgeTopLimit" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="962cbf48-513b-4990-a515-a8466fcfecfc" /><field column="BadgeGUID" fieldcaption="BadgeGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ee6c4e89-b05f-409d-a81e-b49a40e6710e" /><field column="BadgeLastModified" fieldcaption="BadgeLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1e4427e7-5438-4ba4-a757-41e2cc386a23" /></form>', N'', N'', N'', N'CMS_Badge', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110414 17:24:43', 'd2e16806-304a-45d1-8158-d444be21e3a8', 0, 0, 0, N'', 1, N'BadgeName', N'BadgeID', N'', N'BadgeLastModified', N'<search><item searchable="True" name="BadgeID" tokenized="False" content="True" fieldname="qqqw" id="8f752452-7d8d-4fae-ba78-10d1262c9e4e" /><item searchable="True" name="BadgeName" tokenized="True" content="True" id="07bb6afb-a692-4254-a408-2af69b0a4f3e" /><item searchable="True" name="BadgeDisplayName" tokenized="True" content="True" id="4eaf6d55-45d0-46d5-ac31-4391b377f000" /><item searchable="True" name="BadgeImageURL" tokenized="True" content="True" id="0631a1d9-7abf-4ff7-89b4-c172e6c0153d" /><item searchable="True" name="BadgeIsAutomatic" tokenized="True" content="True" id="69ef6851-eecd-4f05-b8d1-8362a839578b" /><item searchable="True" name="BadgeTopLimit" tokenized="True" content="True" id="c05f41e1-80b9-481e-b17f-569cff4edb80" /><item searchable="True" name="BadgeGUID" tokenized="True" content="False" id="48e3b8d8-f8a1-4d9c-acd4-1c6fc3074917" /><item searchable="True" name="BadgeLastModified" tokenized="False" content="False" id="08cf3ea7-8be6-4fa2-a670-7acc3d50d9c2" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1788, N'Message board', N'board.board', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Board_Board">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="BoardID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="BoardName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BoardDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BoardDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BoardOpened" type="xs:boolean" />
              <xs:element name="BoardOpenedFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="BoardOpenedTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="BoardEnabled" type="xs:boolean" />
              <xs:element name="BoardAccess" type="xs:int" />
              <xs:element name="BoardModerated" type="xs:boolean" />
              <xs:element name="BoardUseCaptcha" type="xs:boolean" />
              <xs:element name="BoardMessages" type="xs:int" />
              <xs:element name="BoardLastModified" type="xs:dateTime" />
              <xs:element name="BoardGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="BoardDocumentID" type="xs:int" />
              <xs:element name="BoardUserID" type="xs:int" minOccurs="0" />
              <xs:element name="BoardGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="BoardLastMessageTime" type="xs:dateTime" minOccurs="0" />
              <xs:element name="BoardLastMessageUserName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BoardUnsubscriptionURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BoardRequireEmails" type="xs:boolean" minOccurs="0" />
              <xs:element name="BoardSiteID" type="xs:int" />
              <xs:element name="BoardEnableSubscriptions" type="xs:boolean" />
              <xs:element name="BoardBaseURL" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="BoardLogActivity" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Board_Board" />
      <xs:field xpath="BoardID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="BoardID" fieldcaption="BoardID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="3f5d2bb9-22a8-45ce-ba68-cfada44d509d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="BoardName" fieldcaption="BoardName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="6860b999-d9dd-4a80-ad42-370742f6f1a7" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Trim>False</Trim><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><controlname>textboxcontrol</controlname></settings></field><field column="BoardDisplayName" fieldcaption="BoardDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="e54226a6-bda3-4877-b57e-8c377fbd3df4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardDescription" fieldcaption="BoardDescription" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3366b9f9-096e-4ef5-97be-628a1bd069a0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="BoardOpened" fieldcaption="BoardOpened" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="33127b8a-43bc-46a4-bf30-7e7629e5e9e4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="BoardOpenedFrom" fieldcaption="BoardOpenedFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ce56e685-96cf-41f0-9ea6-7c2befbf63d2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="BoardOpenedTo" fieldcaption="BoardOpenedTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="102e25e0-f65c-42e5-bce1-e039f2488ca8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="BoardEnabled" fieldcaption="BoardEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1b810c74-531d-4257-861d-4fd0841dd252" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="BoardAccess" fieldcaption="BoardAccess" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="45cb27c6-e488-43db-abcb-66cb73d94dd7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardModerated" fieldcaption="BoardModerated" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9249e285-e812-4470-86f9-c7513c2c87a0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="BoardUseCaptcha" fieldcaption="BoardUseCaptcha" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7a75986f-b121-4d04-83d5-16aeb5ec9948" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="BoardMessages" fieldcaption="BoardMessages" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6277f200-4dfe-4a38-92cb-70ed978fb6b1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardLastModified" fieldcaption="BoardLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="891b7bee-2381-41e4-b295-34313a492acf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="BoardGUID" fieldcaption="BoardGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b872e82a-7bc3-4ccb-83e6-6956c039e3af" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="BoardDocumentID" fieldcaption="BoardDocumentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="28a925ed-948f-4b54-b340-1703631f8a0c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardUserID" fieldcaption="BoardUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="50008c74-73f4-4392-82b5-fb56fab1adae" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardGroupID" fieldcaption="BoardGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5b3db77c-1bef-48da-9cd3-f0b003d24c37" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardLastMessageTime" fieldcaption="BoardLastMessageTime" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02e56770-7ff1-47a2-b6d1-85336ac0d1f0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="BoardLastMessageUserName" fieldcaption="BoardLastMessageUserName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="14f779a7-3cf2-4c6f-a47a-bfa569d90771" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardUnsubscriptionURL" fieldcaption="BoardUnsubscriptionURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="e2d08e2a-e814-4172-a186-8dd588299594" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardRequireEmails" fieldcaption="BoardRequireEmails" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="58aa827f-bb57-41df-bd0f-8cf3d205c83a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="BoardSiteID" fieldcaption="BoardSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="14756c64-a27b-4722-b2f9-172fe6fe8628" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardEnableSubscriptions" fieldcaption="BoardEnableSubscriptions" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d3612c9c-913e-4e8e-a2e7-4d7785768062" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="BoardBaseURL" fieldcaption="BoardBaseURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="f46ac2d6-2a75-4463-9298-88ed457b0983" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="BoardLogActivity" fieldcaption="Log on-line marketing activity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8dd91102-c09d-4aff-bd3a-147affa60ab6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Board_Board', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110909 14:37:18', 'c30cef2f-0eb5-4568-a8ab-93bfe91066e8', 0, 0, 0, N'', 2, N'BoardDisplayName', N'BoardDescription', N'', N'BoardLastMessageTime', N'<search><item searchable="True" name="BoardID" tokenized="False" content="False" id="a57f6616-5473-4183-924f-e087f494e2c8" /><item searchable="False" name="BoardName" tokenized="True" content="True" id="f7cfa9c7-99ff-45e5-a959-90ebf671e0ea" /><item searchable="False" name="BoardDisplayName" tokenized="True" content="True" id="703b4c38-ec42-46f5-8bb2-eabc83588c23" /><item searchable="False" name="BoardDescription" tokenized="True" content="True" id="4beadbd6-19b2-4cc7-8608-1eb4ba2a8ea6" /><item searchable="True" name="BoardOpened" tokenized="False" content="False" id="fa251afc-4700-443c-bfbc-72867d5546cd" /><item searchable="True" name="BoardOpenedFrom" tokenized="False" content="False" id="9201a0a6-6229-4a15-8de2-9d92ab7fd1f0" /><item searchable="True" name="BoardOpenedTo" tokenized="False" content="False" id="af916196-ae2c-41c2-8def-c8741ed2c90d" /><item searchable="True" name="BoardEnabled" tokenized="False" content="False" id="daac534a-6aac-44cc-8977-9741b97271f8" /><item searchable="True" name="BoardAccess" tokenized="False" content="False" id="fabd9380-ce93-4068-a422-37c8b3977438" /><item searchable="True" name="BoardModerated" tokenized="False" content="False" id="cb7550b0-796d-4e9c-a01c-bf88b6901837" /><item searchable="True" name="BoardUseCaptcha" tokenized="False" content="False" id="38104b9c-dbe3-45e0-b5d1-a1ce625287ae" /><item searchable="True" name="BoardMessages" tokenized="False" content="False" id="b349859b-1d40-4215-96fc-34a7629b788d" /><item searchable="True" name="BoardLastModified" tokenized="False" content="False" id="e3faae7b-0f89-4cf1-866f-8a3839ca7ab2" /><item searchable="False" name="BoardGUID" tokenized="False" content="False" id="c3522b0d-5ef3-4765-8ef6-158aedc50342" /><item searchable="True" name="BoardDocumentID" tokenized="False" content="False" id="d42230ea-c053-48fa-93d7-4a821523faab" /><item searchable="True" name="BoardUserID" tokenized="False" content="False" id="e1118abd-7aad-44e7-bfb7-74555615fbb0" /><item searchable="True" name="BoardGroupID" tokenized="False" content="False" id="88408e1f-0d33-46d0-a90e-3de2ac27d399" /><item searchable="True" name="BoardLastMessageTime" tokenized="False" content="False" id="159b84b0-5c5c-4e36-b9e3-96afb5135e52" /><item searchable="False" name="BoardLastMessageUserName" tokenized="True" content="True" id="478e5913-1e7f-42fa-b0bd-f95298524bb3" /><item searchable="False" name="BoardUnsubscriptionURL" tokenized="True" content="True" id="06c4cf5c-d366-4de8-a8d4-20e8e906ca31" /><item searchable="True" name="BoardRequireEmails" tokenized="False" content="False" id="3386e0e3-b1d3-4127-ba41-269808763672" /><item searchable="True" name="BoardSiteID" tokenized="False" content="False" id="b7b8a3f8-8070-44b7-b2b7-5d6378956ed2" /><item searchable="True" name="BoardEnableSubscriptions" tokenized="False" content="False" id="621a2340-803c-43f8-b5a5-137f478bd73d" /><item searchable="False" name="BoardBaseURL" tokenized="True" content="True" id="18b96423-147d-4276-8329-60014fc264b4" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1790, N'E-mail attachment', N'cms.EmailAttachment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_EmailAttachment">
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
              <xs:element name="AttachmentBinary" type="xs:base64Binary" />
              <xs:element name="AttachmentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AttachmentLastModified" type="xs:dateTime" />
              <xs:element name="AttachmentContentID" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="255" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_EmailAttachment" />
      <xs:field xpath="AttachmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AttachmentID" fieldcaption="AttachmentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ebf34bd7-e1d2-4488-85a3-9e3127f6a69b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentName" fieldcaption="AttachmentName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="255" publicfield="false" spellcheck="true" guid="5340fb1e-0d50-4f87-aa6a-8029f188e64b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentExtension" fieldcaption="AttachmentExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="4511305d-5456-4bbc-b99c-7eba264d379c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentSize" fieldcaption="AttachmentSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3a4cffe6-51a8-41d6-946c-b35ee0846c9a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentMimeType" fieldcaption="AttachmentMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="8c49f5fb-e8a0-4c7e-bcde-447e370cbb7f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentBinary" fieldcaption="AttachmentBinary" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43c6ec9d-8c01-4b7c-9178-1cea0d2fda61" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentGUID" fieldcaption="AttachmentGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="096f5598-6e57-446f-918d-168beb17e67c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentLastModified" fieldcaption="AttachmentLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9a39bd85-4847-41ee-a7e1-3fb867eb5964" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="AttachmentContentID" fieldcaption="AttachmentContentID" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="255" publicfield="false" spellcheck="true" guid="0a09be9c-cc0c-4342-9549-af1dec53a02d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentSiteID" fieldcaption="Site ID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="701e96b9-634a-4c43-8e95-af75300b2262" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'CMS_EmailAttachment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110607 08:55:51', '3a6a10ed-8426-4240-9f0f-ae054f612c61', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1791, N'Message', N'board.message', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Board_Message">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="MessageID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="MessageUserName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageEmail">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageURL">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageIsSpam" type="xs:boolean" />
              <xs:element name="MessageBoardID" type="xs:int" />
              <xs:element name="MessageApproved" type="xs:boolean" />
              <xs:element name="MessageApprovedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="MessageUserID" type="xs:int" minOccurs="0" />
              <xs:element name="MessageUserInfo">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="MessageAvatarGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="MessageInserted" type="xs:dateTime" />
              <xs:element name="MessageLastModified" type="xs:dateTime" />
              <xs:element name="MessageGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="MessageRatingValue" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Board_Message" />
      <xs:field xpath="MessageID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="MessageID" fieldcaption="MessageID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="false" publicfield="false" spellcheck="true" guid="213451af-1a3a-4540-847a-7c91c1c8185d" /><field column="MessageUserName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="c3a2f1d2-c50e-4626-9196-b92374ae982c" fieldcaption="User name:" /><field column="MessageText" fieldcaption="Text" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="da3e536f-16ff-4ca5-b70a-6d0ddc016b76" /><field column="MessageEmail" fieldcaption="Email" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="1ac6e606-13e7-439e-b51f-8c09fa79984c" /><field column="MessageURL" fieldcaption="URL" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="67503096-e57d-4278-ac92-e6561419c214" /><field column="MessageIsSpam" fieldcaption="Is SPAM" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2f20a317-502b-41df-90e9-023866b017fe" /><field column="MessageBoardID" fieldcaption="Message Board ID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="966041a1-ce21-4cb6-a41f-4141637e5a58" /><field column="MessageApproved" fieldcaption="Approved" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="50d16031-33c1-4ac9-b5e8-f1a7ce0847e0" /><field column="MessageApprovedByUserID" fieldcaption="Approved By User ID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="11bfc429-bab9-4b26-91d9-b4e064b3829b" /><field column="MessageUserID" fieldcaption="User ID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="93218b64-ec61-4b99-89b3-6a576a219fbe" /><field column="MessageUserInfo" fieldcaption="User Info" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="463acf35-fe22-4b96-9a0f-42b6de4c34b9" /><field column="MessageAvatarGUID" fieldcaption="Avatar GUID" visible="true" columntype="guid" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0bac580c-b976-4c63-8cf1-2fbefbaaa148" /><field column="MessageInserted" fieldcaption="Inserted" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7282dfef-a951-44f4-a460-f472c2936692"><settings><editTime>true</editTime></settings></field><field column="MessageLastModified" fieldcaption="Last Modified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c8354000-5a97-4932-899a-caba73315eff"><settings><editTime>true</editTime></settings></field><field column="MessageGUID" fieldcaption="GUID" visible="true" columntype="guid" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f7a909f-3352-47d0-9b55-ed6c9822fcd1" /><field column="MessageRatingValue" fieldcaption="MessageRatingValue" visible="true" columntype="double" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="67f5c140-09e6-42e6-adf9-ca9a2fdf5b84" /></form>', N'', N'', N'', N'Board_Message', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100303 12:11:27', 'a6fc171e-86df-4222-a5cf-c95259deaeb9', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'<search><item tokenized="True" name="MessageUserInfo" content="True" searchable="True" id="94207591-fd30-4260-bda3-2ef62f743c6d"></item><item tokenized="True" name="MessageUserName" content="True" searchable="True" id="f7fbc73c-44d0-4bcf-ad7f-e7f80a056e3f"></item><item tokenized="False" name="MessageLastModified" content="False" searchable="True" id="58405079-5d4f-46ac-baea-b967f02309c1"></item><item tokenized="False" name="MessageID" content="False" searchable="True" id="1ca1c720-4aa6-4a0e-ac6b-ff4c75364044"></item><item tokenized="True" name="MessageEmail" content="True" searchable="True" id="ce3feadb-628b-4014-9854-dc7a306c26e6"></item><item tokenized="False" name="MessageApproved" content="False" searchable="True" id="68f414d2-5d2d-4419-a3ba-5915dad2b9d5"></item><item tokenized="False" name="MessageApprovedByUserID" content="False" searchable="True" id="d3afa628-28cb-4565-ba7f-3d00d3bbb677"></item><item tokenized="False" name="MessageGUID" content="False" searchable="False" id="0fafc405-de48-4170-946f-246a1045907b"></item><item tokenized="False" name="MessageAvatarGUID" content="False" searchable="False" id="86964a7d-983b-44de-8718-0f82607b7682"></item><item tokenized="True" name="MessageText" content="True" searchable="True" id="a6ab08ea-5c71-42ed-8710-12034db98163"></item><item tokenized="False" name="MessageBoardID" content="False" searchable="True" id="e61ca7f3-7965-490f-ae61-1f19b8779c83"></item><item tokenized="False" name="MessageInserted" content="False" searchable="True" id="adbb6d16-049e-455d-9a1a-64a9362c12ac"></item><item tokenized="False" name="MessageRatingValue" content="False" searchable="True" id="7c9b2cf2-7e7f-4d95-b7a2-364e5f9df123"></item><item tokenized="False" name="MessageIsSpam" content="False" searchable="True" id="750551f6-ad21-4cc2-b536-94a9beb00e41"></item><item tokenized="False" name="MessageUserID" content="False" searchable="True" id="90dbeb9e-0091-4d52-97c7-4902c3e3dfcf"></item><item tokenized="True" name="MessageURL" content="True" searchable="True" id="3285dbb0-c1e2-4b39-81bc-27bcb50dd850"></item></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1797, N'Message board role', N'board.boardrole', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Board_Role">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="BoardID" type="xs:int" />
              <xs:element name="RoleID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Board_Role" />
      <xs:field xpath="BoardID" />
      <xs:field xpath="RoleID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="BoardID" fieldcaption="BoardID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="4e0e8f36-218d-4b3b-b133-d45c55bbe586" /><field column="RoleID" fieldcaption="RoleID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="33dfe7c5-a602-4f9b-bb20-4d3f922eea79" /></form>', N'', N'', N'', N'Board_Role', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110324 15:07:48', '7ae09413-b447-4b1f-93e0-58421ca1f6ba', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1803, N'Bad word culture', N'badwords.wordculture', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="BadWords_WordCulture">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="WordID" type="xs:int" />
              <xs:element name="CultureID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//BadWords_WordCulture" />
      <xs:field xpath="WordID" />
      <xs:field xpath="CultureID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="WordID" fieldcaption="WordID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="53b7d596-5a05-4d30-88fa-fdd58c7733d8" /><field column="CultureID" fieldcaption="CultureID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="851d3cb2-dd4d-44c2-8834-36b5091128b4" /></form>', N'', N'', N'', N'BadWords_WordCulture', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110315 08:17:05', 'ec176759-6109-4794-9de9-372f91efb878', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1804, N'Message board moderator', N'board.moderator', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Board_Moderator">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="BoardID" type="xs:int" />
              <xs:element name="UserID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Board_Moderator" />
      <xs:field xpath="BoardID" />
      <xs:field xpath="UserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="BoardID" fieldcaption="BoardID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="18895a79-d9a6-4118-b059-55271cff6483" /><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9451413d-6868-4575-9a2a-697220bf8385" /></form>', N'', N'', N'', N'Board_Moderator', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110315 08:18:22', '4df3a8d5-672c-4ada-b4e4-e5731c2ec5de', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1808, N'Notification gateway', N'notification.gateway', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Notification_Gateway">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="GatewayID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="GatewayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GatewayDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GatewayAssemblyName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GatewayClassName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GatewayDescription">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GatewaySupportsEmail" type="xs:boolean" />
              <xs:element name="GatewaySupportsPlainText" type="xs:boolean" />
              <xs:element name="GatewaySupportsHTMLText" type="xs:boolean" />
              <xs:element name="GatewayLastModified" type="xs:dateTime" />
              <xs:element name="GatewayGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="GatewayEnabled" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Notification_Gateway" />
      <xs:field xpath="GatewayID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="GatewayID" fieldcaption="GatewayID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="998f29e3-f8ff-4cad-848f-7fe56c334eb1" /><field column="GatewayName" fieldcaption="GatewayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="265a729c-4602-4156-a570-dc67c728b9db" /><field column="GatewayDisplayName" fieldcaption="GatewayDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bf9a20e7-1c94-4ce6-aef5-288be9896f8c" /><field column="GatewayAssemblyName" fieldcaption="GatewayAssemblyName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="97622571-8bf3-42b0-be8e-2d04ee6d3305" /><field column="GatewayClassName" fieldcaption="GatewayClassName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="78ed2261-6c6e-4f36-815f-f39c1068dc7d" /><field column="GatewayDescription" fieldcaption="GatewayDescription" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2cc80b89-ef7d-4b6d-908f-4a84ff9c8cd5" /><field column="GatewayEnabled" fieldcaption="Gatewau enabled" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6bb28929-60ca-48c9-bdab-20155e52ddc4" /><field column="GatewaySupportsEmail" fieldcaption="GatewaySupportsEmail" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8961441a-d70a-476b-8686-ac71a619dc6d" /><field column="GatewaySupportsPlainText" fieldcaption="GatewaySupportsPlainText" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01b61ce6-f5ad-41de-b5a7-9343c6c88ed1" /><field column="GatewaySupportsHTMLText" fieldcaption="GatewaySupportsHTMLText" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="394f1ae9-7967-4a9c-a804-0d156a9e7ad6" /><field column="GatewayLastModified" fieldcaption="GatewayLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c320ffe6-2936-4899-ba3c-52e8151313d7" /><field column="GatewayGUID" fieldcaption="GatewayGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0a91dd34-e8fd-46f9-aae5-795d6d7e8d45" /></form>', N'', N'', N'', N'Notification_Gateway', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110527 13:44:26', '3d77378a-a021-4b11-bdce-c8043d45ce4a', 0, 0, 0, N'', 2, N'GatewayDisplayName', N'GatewayDescription', N'', N'GatewayLastModified', N'<search><item searchable="True" name="GatewayID" tokenized="False" content="False" id="15ac0e3d-9af8-4d52-9327-ef36ed527df7" /><item searchable="False" name="GatewayName" tokenized="True" content="True" id="2bd268ab-325b-4cc8-a2d1-3165aa34c419" /><item searchable="False" name="GatewayDisplayName" tokenized="True" content="True" id="f1249902-a8a1-41e3-ac3b-713899884aac" /><item searchable="False" name="GatewayAssemblyName" tokenized="True" content="True" id="665c818d-b9f7-456f-91df-bb42f944fbb0" /><item searchable="False" name="GatewayClassName" tokenized="True" content="True" id="84409909-c34b-42d3-b131-1bb1ddee39cc" /><item searchable="False" name="GatewayDescription" tokenized="True" content="True" id="df7790a3-9bc7-48fc-9608-293fe73fb32e" /><item searchable="True" name="GatewayEnabled" tokenized="False" content="False" id="7d58217a-c0da-4eeb-90db-d2f3bec2c355" /><item searchable="True" name="GatewaySupportsEmail" tokenized="False" content="False" id="8438fee3-03f0-445f-83fa-a9e9886b85d4" /><item searchable="True" name="GatewaySupportsPlainText" tokenized="False" content="False" id="9e408672-a67d-4a40-a6c8-5d703dfd6330" /><item searchable="True" name="GatewaySupportsHTMLText" tokenized="False" content="False" id="a06d27fd-ae56-4738-952c-2f2de538f7af" /><item searchable="True" name="GatewayLastModified" tokenized="False" content="False" id="4fc6a4ef-0ed7-4a2e-91d2-9e59dcdc2f15" /><item searchable="False" name="GatewayGUID" tokenized="False" content="False" id="e6228d0b-1f79-447c-82be-72dcfd109c30" /></search>', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1809, N'Email', N'cms.email', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Email">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EmailID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="EmailFrom">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailTo" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailCc" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailBcc" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailSubject">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailBody" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailPlainTextBody" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailFormat" type="xs:int" />
              <xs:element name="EmailPriority" type="xs:int" />
              <xs:element name="EmailSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="EmailLastSendResult" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailLastSendAttempt" type="xs:dateTime" minOccurs="0" />
              <xs:element name="EmailGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="EmailLastModified" type="xs:dateTime" />
              <xs:element name="EmailStatus" type="xs:int" minOccurs="0" />
              <xs:element name="EmailIsMass" type="xs:boolean" minOccurs="0" />
              <xs:element name="EmailSetName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailSetRelatedID" type="xs:int" minOccurs="0" />
              <xs:element name="EmailReplyTo" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="EmailHeaders" minOccurs="0">
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
      <xs:selector xpath=".//CMS_Email" />
      <xs:field xpath="EmailID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EmailID" fieldcaption="EmailID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="521d5e92-1ffd-4b57-8efe-de86ea04e86b" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="EmailFrom" fieldcaption="EmailFrom" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="c2397191-6419-4c90-8602-438740d5de70" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailTo" fieldcaption="EmailTo" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f47a699d-f3d6-4da7-84e8-a7a54e7de8a9" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailReplyTo" fieldcaption="EmailReplyTo" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="7faf43c9-3882-43fb-9004-520b188a8b5a" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailCc" fieldcaption="EmailCc" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="10e8d6ce-d8b9-4bf8-a84d-8f9688e29981" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailBcc" fieldcaption="EmailBcc" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cedfd4fc-8e1b-4156-960e-552fc1246aeb" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailSubject" fieldcaption="EmailSubject" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="601fdfbe-281e-458d-8cc0-a29306087c87" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailBody" fieldcaption="EmailBody" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad000851-1cdf-41c8-95ee-eff669981220" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailPlainTextBody" fieldcaption="EmailPlainTextBody" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c96fa853-4a36-4f3d-b661-fcdb58434d1c" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailFormat" fieldcaption="EmailFormat" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dcfbdec6-2369-4292-b406-9995bb2889bf" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailPriority" fieldcaption="EmailPriority" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b7feb9c1-7f2e-42e1-9be6-8e7100f9c669" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailSiteID" fieldcaption="EmailSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2aa51d80-c928-42d7-90a7-735dc9ddbc80" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailLastSendResult" fieldcaption="EmailLastSendResult" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="40755371-af41-4836-9e1c-6e512a280e66" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="EmailLastSendAttempt" fieldcaption="EmailLastSendAttempt" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b5be875d-5220-454c-a099-6b936c3485c0" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="EmailGUID" fieldcaption="EmailGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c78068ea-bd20-4004-b3d4-25028ffd07e8" ismacro="false" hasdependingfields="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="EmailLastModified" fieldcaption="EmailLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e94a0a0b-86c9-4207-8710-e86533e755bc" ismacro="false" hasdependingfields="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="EmailStatus" fieldcaption="EmailStatus" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="227556a7-9cc7-4f9b-96ad-2ccd667605b1" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailIsMass" fieldcaption="EmailIsMass" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0a21ac5d-68a2-4306-af4e-7ab7b2aa1693" ismacro="false" hasdependingfields="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="EmailSetName" fieldcaption="EmailSetName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="c3b5fdf7-ed07-4ebf-97fd-9e4a21967507" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailSetRelatedID" fieldcaption="EmailSetRelatedID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="814ac56f-1b7c-4593-b5d4-26e39c3624da" ismacro="false" hasdependingfields="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="EmailHeaders" fieldcaption="EmailHeaders" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="df673a9f-31e4-40c5-9f13-3a5af25dd895" visibility="none" ismacro="false" hasdependingfields="false"><settings><controlname>textareacontrol</controlname><FilterMode>False</FilterMode><FilterEnabled>False</FilterEnabled></settings></field></form>', N'', N'', N'', N'CMS_Email', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110512 10:23:17', '488f275e-7311-4136-a127-4c970cd4060f', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1811, N'Attachment for email', N'cms.attachmentforemail', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_AttachmentForEmail">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EmailID" type="xs:int" />
              <xs:element name="AttachmentID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_AttachmentForEmail" />
      <xs:field xpath="EmailID" />
      <xs:field xpath="AttachmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EmailID" fieldcaption="EmailID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="3a1b56eb-fa26-4225-9342-d8b34d9dbe0f" /><field column="AttachmentID" fieldcaption="AttachmentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="53e20c89-9e25-4528-a839-1f2cf10db117" /></form>', N'', N'', N'', N'CMS_AttachmentForEmail', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081231 14:52:49', '9a4da0dc-ef38-4485-9d15-65f0c4e629d8', 0, 0, 0, N'', 1, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1813, N'Notification template', N'notification.template', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Notification_Template">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TemplateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TemplateName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="TemplateLastModified" type="xs:dateTime" />
              <xs:element name="TemplateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Notification_Template" />
      <xs:field xpath="TemplateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TemplateID" fieldcaption="TemplateID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="649871d8-bbcf-4c0d-9746-388e24871918" /><field column="TemplateName" fieldcaption="TemplateName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6f6f10e9-87e0-4dcc-92ec-bfb547d950d8" /><field column="TemplateDisplayName" fieldcaption="TemplateDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3d31111f-005d-48cb-88f7-e77d0a5fd573" /><field column="TemplateSiteID" fieldcaption="TemplateSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a5f24501-47b5-4328-9210-7c646eb7fb71" /><field column="TemplateCategoryID" fieldcaption="TemplateCategoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fcbbc7ea-61bb-44ae-946b-ff56f0a8e1d1" /><field column="TemplateLastModified" fieldcaption="TemplateLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c00d4d42-ec45-41ff-baa0-dd81fea1dcef" /><field column="TemplateGUID" fieldcaption="TemplateGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="44d412a0-7ee3-435f-9b96-6b9736325f7f" /></form>', N'', N'', N'', N'Notification_Template', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110228 16:20:21', '91058913-11dd-42fa-a9c6-bd00c1b16382', 0, 0, 0, N'', 2, N'TemplateDisplayName', N'0', N'', N'TemplateLastModified', N'<search><item searchable="True" name="TemplateID" tokenized="False" content="False" id="8b947284-db31-422d-811d-ad43976e810b" /><item searchable="False" name="TemplateName" tokenized="True" content="True" id="d6200c44-422f-4025-8e89-7349a90df302" /><item searchable="False" name="TemplateDisplayName" tokenized="True" content="True" id="ee051b8f-e236-4911-9914-107b94edbef2" /><item searchable="True" name="TemplateSiteID" tokenized="False" content="False" id="064df312-2141-44f2-b925-41eeb805058e" /><item searchable="True" name="TemplateCategoryID" tokenized="False" content="False" id="bc1bd276-e4f9-4bca-b332-99b2b104a49b" /><item searchable="True" name="TemplateLastModified" tokenized="False" content="False" id="3afc4ad3-6139-4955-92fd-edf61119afef" /><item searchable="False" name="TemplateGUID" tokenized="False" content="False" id="581cb59d-8c9e-43cb-b2b1-a0bed68e331e" /></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1814, N'Notification template text', N'notification.templatetext', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Notification_TemplateText">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TemplateTextID" type="xs:int" />
              <xs:element name="TemplateID" type="xs:int" />
              <xs:element name="GatewayID" type="xs:int" />
              <xs:element name="TemplateSubject">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateHTMLText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplatePlainText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TemplateTextGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TemplateTextLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Notification_TemplateText" />
      <xs:field xpath="TemplateTextID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TemplateTextID" fieldcaption="TemplateTextID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0fe06997-dfa0-4b7f-96df-cf2b5dba92db" /><field column="TemplateID" fieldcaption="TemplateID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9664235f-533b-40df-9e11-440b7bb620f9" /><field column="GatewayID" fieldcaption="GatewayID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29dd7102-0ca4-4859-a0b6-3585065ac029" /><field column="TemplateSubject" fieldcaption="TemplateSubject" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="8e0bdef7-be8f-4332-aa84-c344a5f57bf8" /><field column="TemplateHTMLText" fieldcaption="TemplateHTMLText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c11474b9-3241-4b64-ac57-0a5f0791bde5" /><field column="TemplatePlainText" fieldcaption="TemplatePlainText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e90f8207-0398-4c60-8287-25f28f1766e1" /><field column="TemplateTextGUID" fieldcaption="TemplateTextGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3a1a1c8c-e009-487a-b610-db0d7deaf57b" /><field column="TemplateTextLastModified" fieldcaption="TemplateTextLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3e883605-907d-46dc-bc7f-8789db61755c"><settings><editTime>false</editTime></settings></field></form>', N'', N'', N'', N'Notification_TemplateText', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20081229 09:07:13', 'e0b0a19f-68db-4784-8134-104a1f574ef8', 0, 0, 0, N'', 2, N'', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1815, N'Notification subscription', N'notification.subscription', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Notification_Subscription">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SubscriptionGatewayID" type="xs:int" />
              <xs:element name="SubscriptionTemplateID" type="xs:int" />
              <xs:element name="SubscriptionEventSource" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionEventCode" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionEventDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionEventObjectID" type="xs:int" minOccurs="0" />
              <xs:element name="SubscriptionTime" type="xs:dateTime" />
              <xs:element name="SubscriptionUserID" type="xs:int" />
              <xs:element name="SubscriptionTarget">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionLastModified" type="xs:dateTime" />
              <xs:element name="SubscriptionGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SubscriptionEventData1" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionEventData2" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SubscriptionUseHTML" type="xs:boolean" minOccurs="0" />
              <xs:element name="SubscriptionSiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Notification_Subscription" />
      <xs:field xpath="SubscriptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriptionID" fieldcaption="SubscriptionID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="ed6ab9e8-4d20-4f5c-9844-b20618195aa1" /><field column="SubscriptionGatewayID" fieldcaption="SubscriptionGatewayID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="adc281f6-26da-4323-b23c-d7857a6c959e" /><field column="SubscriptionTemplateID" fieldcaption="SubscriptionTemplateID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c062c3e5-9ffa-4068-a022-ea1fb70ab26d" /><field column="SubscriptionEventSource" fieldcaption="SubscriptionEventSource" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b27f6ab9-280e-4c6c-b336-d573e7b37916" /><field column="SubscriptionEventCode" fieldcaption="SubscriptionEventCode" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="de9775c1-737b-47bc-bc95-412b64e7c1a4" /><field column="SubscriptionEventDisplayName" fieldcaption="SubscriptionEventDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c6b3dd2f-b815-48d7-a16a-1f4be2735694" /><field column="SubscriptionEventObjectID" fieldcaption="SubscriptionEventObjectID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="58a66744-4360-4ac8-bc3a-34f835403b12" /><field column="SubscriptionEventData1" fieldcaption="Subscription event data 1" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="17b88874-a1f4-4a9c-94c0-742ac3884f11" /><field column="SubscriptionEventData2" fieldcaption="Subscription event data 2" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c26ff987-a95e-4cba-9f6f-65b51c0b9cb0" /><field column="SubscriptionUseHTML" fieldcaption="SubscriptionUseHTML" visible="true" defaultvalue="false" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4b506366-c76a-4e38-ae0e-3610c0c6b387" /><field column="SubscriptionTime" fieldcaption="SubscriptionTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="24332b9b-9cb0-499b-8cf2-e147f681f1a1" /><field column="SubscriptionUserID" fieldcaption="SubscriptionUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9f9b4aa4-19f2-4879-93ab-4675f16704da" /><field column="SubscriptionTarget" fieldcaption="SubscriptionTarget" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="98654069-4985-40fc-bedd-0a0630b81399" /><field column="SubscriptionSiteID" fieldcaption="SubscriptionSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2227c603-af2e-4417-be3d-bf9c07dc856f" /><field column="SubscriptionLastModified" fieldcaption="SubscriptionLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d2f9415b-6a51-4a69-af0e-c24152d77d13" /><field column="SubscriptionGUID" fieldcaption="SubscriptionGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0e034e8a-7d78-4618-b64f-33e978dc94ea" /></form>', N'', N'', N'', N'Notification_Subscription', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:57:18', 'e9e38a33-6bd0-4265-aba3-1babb390b000', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1841, N'Personalization', N'cms.personalization', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_Personalization">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PersonalizationID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PersonalizationGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PersonalizationLastModified" type="xs:dateTime" />
              <xs:element name="PersonalizationUserID" type="xs:int" minOccurs="0" />
              <xs:element name="PersonalizationDocumentID" type="xs:int" minOccurs="0" />
              <xs:element name="PersonalizationWebParts" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PersonalizationDashboardName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PersonalizationSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_Personalization" />
      <xs:field xpath="PersonalizationID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PersonalizationID" fieldcaption="PersonalizationID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="3988441a-55dc-49e7-a66c-e7358beb375b" visibility="none" ismacro="false" /><field column="PersonalizationGUID" fieldcaption="PersonalizationGUID" visible="true" columntype="guid" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="e306eeb6-d3c8-4070-820d-140205886fcf" ismacro="false" /><field column="PersonalizationLastModified" fieldcaption="PersonalizationLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="false" guid="f91bbd1f-9447-4cff-aa85-9f9410c33b01" ismacro="false"><settings><editTime>true</editTime></settings></field><field column="PersonalizationUserID" fieldcaption="PersonalizationUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="5ba5b18b-42c2-4971-a249-8bbc000befd8" ismacro="false" /><field column="PersonalizationDocumentID" fieldcaption="PersonalizationDocumentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="cb5b6418-3eaf-4647-9711-967ad0583179" visibility="none" ismacro="false" /><field column="PersonalizationWebParts" fieldcaption="PersonalizationWebParts" visible="true" columntype="longtext" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="51172fcd-2c07-420d-a30f-42e9e91f4884" ismacro="false" /><field column="PersonalizationDashboardName" visible="false" columntype="text" fieldtype="label" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="false" guid="8b67ad74-9e2c-4a14-959f-2b1f8b7a7b73" visibility="none" ismacro="false" /><field column="PersonalizationSiteID" visible="false" columntype="integer" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="ee05090a-777c-4563-9725-692df5734adf" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'CMS_Personalization', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:15:05', '385d6f75-3d20-42d7-ba0d-6ac36201a4b1', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1887, N'Messaging - Contact list', N'messaging.contactlist', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Messaging_ContactList">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ContactListUserID" type="xs:int" />
              <xs:element name="ContactListContactUserID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Messaging_ContactList" />
      <xs:field xpath="ContactListUserID" />
      <xs:field xpath="ContactListContactUserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ContactListUserID" fieldcaption="ContactListUserID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="c23c4f2b-7c56-447b-a5db-de8062e7596c" /><field column="ContactListContactUserID" fieldcaption="ContactListContactUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd3b6f9d-4f9e-4725-835f-47f7cdb0f412" /></form>', N'', N'', N'', N'Messaging_ContactList', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110324 14:48:54', 'c7118d45-532c-4c43-aa2c-7b1c1d94bf24', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1888, N'Messaging - Ignore list', N'messaging.ignorelist', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Messaging_IgnoreList">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="IgnoreListUserID" type="xs:int" />
              <xs:element name="IgnoreListIgnoredUserID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Messaging_IgnoreList" />
      <xs:field xpath="IgnoreListUserID" />
      <xs:field xpath="IgnoreListIgnoredUserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'', N'', N'', N'', N'Messaging_IgnoreList', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:59:48', '8da9ee4e-4f7b-48f0-8384-1d9636707338', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1895, N'Message board subscription', N'board.subscription', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Board_Subscription">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SubscriptionID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SubscriptionBoardID" type="xs:int" />
              <xs:element name="SubscriptionUserID" type="xs:int" minOccurs="0" />
              <xs:element name="SubscriptionEmail">
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
      <xs:selector xpath=".//Board_Subscription" />
      <xs:field xpath="SubscriptionID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SubscriptionID" fieldcaption="SubscriptionID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="434e2549-3146-4267-b6df-6649b01881b3" /><field column="SubscriptionBoardID" fieldcaption="SubscriptionBoardID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="baef33f3-c2b1-43b7-8b8f-30f3fc74924d" /><field column="SubscriptionUserID" fieldcaption="SubscriptionUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5dbcd520-8f23-40de-8cdb-af13dd26fc5d" /><field column="SubscriptionEmail" fieldcaption="SubscriptionEmail" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c5abf6e8-65a7-4caa-9c7d-bf10c756604a" /><field column="SubscriptionLastModified" fieldcaption="SubscriptionLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="28cb6883-250f-47a2-bb4c-29dd2cfe98e0" /><field column="SubscriptionGUID" fieldcaption="SubscriptionGUID" visible="true" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aff7604e-2934-4ea4-97a8-982cda84f5fd" /></form>', N'', N'', N'', N'Board_Subscription', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 10:09:07', 'ffbbfc2a-fe68-4f1f-8ad7-ee8115499f77', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1896, N'Abuse report', N'CMS.AbuseReport', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_AbuseReport">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ReportID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ReportGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ReportTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportURL">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1000" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportCulture">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportObjectID" type="xs:int" minOccurs="0" />
              <xs:element name="ReportObjectType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportComment">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportUserID" type="xs:int" minOccurs="0" />
              <xs:element name="ReportWhen" type="xs:dateTime" />
              <xs:element name="ReportStatus" type="xs:int" />
              <xs:element name="ReportSiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_AbuseReport" />
      <xs:field xpath="ReportID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ReportID" fieldcaption="ReportID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="892db787-1eca-47ae-b962-5d43f284dfe2" ismacro="false" /><field column="ReportGUID" fieldcaption="ReportGUID" visible="true" columntype="guid" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5025a50d-7931-4b7f-a575-75b48a32851a" ismacro="false" /><field column="ReportTitle" fieldcaption="ReportTitle" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="32e9eb2a-1c55-44e5-a0fd-28001a43211b" ismacro="false" /><field column="ReportURL" fieldcaption="ReportURL" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="1000" publicfield="false" spellcheck="true" guid="b37f7666-cd87-4b00-ba9d-e4df36997db0" ismacro="false" /><field column="ReportCulture" fieldcaption="ReportCulture" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="bc3f0126-2000-4e7e-9415-d714619a49aa" ismacro="false" /><field column="ReportObjectID" fieldcaption="ReportObjectID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6d3e7b93-a903-4a97-9479-460c3d511b5e" ismacro="false" /><field column="ReportObjectType" fieldcaption="ReportObjectType" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="70fb19e9-e74e-46b9-a06a-6728c540430b" ismacro="false" /><field column="ReportComment" fieldcaption="ReportComment" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7ea88b2b-b2d9-4391-8943-588604c80f2b" visibility="none" ismacro="false" /><field column="ReportUserID" fieldcaption="ReportUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9641ceb0-b300-414e-9c2e-3f0a9be6ab54" ismacro="false" /><field column="ReportWhen" fieldcaption="ReportWhen" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="26defb36-5475-4da5-ba6f-7018757cbf3f" ismacro="false"><settings><editTime>true</editTime></settings></field><field column="ReportStatus" fieldcaption="ReportStatus" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="75b84fd4-16c4-45b8-952b-a22c55b50dae" ismacro="false" /><field column="ReportSiteID" fieldcaption="ReportSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1ac21a9b-7391-4a2f-9c84-20992ac78861" ismacro="false" /></form>', N'', N'', N'', N'CMS_AbuseReport', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110225 13:12:19', 'd4b445f8-6f93-412a-8ef2-429ee991473c', 0, 0, 0, N'', 1, N'ReportTitle', N'ReportComment', N'', N'ReportWhen', N'<search><item searchable="True" name="ReportID" tokenized="False" content="False" id="a378e571-c7d9-4412-a345-c6b273e821a4" /><item searchable="False" name="ReportGUID" tokenized="False" content="False" id="b70da8b2-98fb-4f23-85a8-db3830d89beb" /><item searchable="False" name="ReportTitle" tokenized="True" content="True" id="0e8ce569-0dd4-4078-bd00-34941ac49fd5" /><item searchable="False" name="ReportURL" tokenized="True" content="True" id="dd6b3a57-fabf-436e-85bf-9ac599d24047" /><item searchable="False" name="ReportCulture" tokenized="True" content="True" id="d7a12b8d-f3e6-4476-842d-ec6f1877fd76" /><item searchable="True" name="ReportObjectID" tokenized="False" content="False" id="cf1f1bb8-4f6c-4356-a4f4-6cbc248a0d3a" /><item searchable="False" name="ReportObjectType" tokenized="True" content="True" id="117f7e19-94f7-44fa-9990-0fad86e52b6e" /><item searchable="False" name="ReportComment" tokenized="True" content="True" id="84cba4af-0f06-4012-ae23-25e920cd2ded" /><item searchable="True" name="ReportUserID" tokenized="False" content="False" id="2f29a3c9-8696-4a3c-948f-d936adac921d" /><item searchable="True" name="ReportWhen" tokenized="False" content="False" id="cb69db28-143f-4fcc-8a53-bd2396a91a08" /><item searchable="True" name="ReportStatus" tokenized="False" content="False" id="df0fe402-3d4c-4417-8ee5-58d78c7e4147" /><item searchable="True" name="ReportSiteID" tokenized="False" content="False" id="95e82b2f-b930-48ae-b00d-c348f3d21e1b" /></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1898, N'Friends', N'community.friend', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Community_Friend">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FriendID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="FriendRequestedUserID" type="xs:int" />
              <xs:element name="FriendUserID" type="xs:int" />
              <xs:element name="FriendRequestedWhen" type="xs:dateTime" />
              <xs:element name="FriendComment" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="FriendApprovedBy" type="xs:int" minOccurs="0" />
              <xs:element name="FriendApprovedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="FriendRejectedBy" type="xs:int" minOccurs="0" />
              <xs:element name="FriendRejectedWhen" type="xs:dateTime" minOccurs="0" />
              <xs:element name="FriendGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FriendStatus" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Community_Friend" />
      <xs:field xpath="FriendID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FriendID" fieldcaption="FriendID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="c594ffa1-6187-4a76-ad68-4d9f1d4f70c2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FriendRequestedUserID" fieldcaption="FriendRequestedUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="048307ca-6750-4859-8f54-92f9199da826" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FriendUserID" fieldcaption="FriendUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bfb8e040-35f7-4260-99cd-6a1967e7fbdd" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FriendRequestedWhen" fieldcaption="FriendRequestedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f04975c1-fd57-4bd2-8d62-f04a3171781c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="FriendComment" fieldcaption="FriendComment" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="526e3ab2-8d68-42b0-aaeb-5a2ed922e23f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="FriendApprovedBy" fieldcaption="FriendApprovedBy" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="617b041c-4a9b-4f59-bd19-1b52638969a2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FriendApprovedWhen" fieldcaption="FriendApprovedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f2b2dfaa-687d-48b3-b4dc-0d0079290f87" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="FriendRejectedBy" fieldcaption="FriendRejectedBy" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="77d78f42-9506-4eb3-8297-25966319cba2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FriendRejectedWhen" fieldcaption="FriendRejectedWhen" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9b1e26fe-1fb2-47cc-85c0-d7432734ab82" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="FriendGUID" fieldcaption="FriendGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0999b481-bbfc-43de-9ba8-4b3fcaf32a1f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="FriendStatus" fieldcaption="FriendStatus" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="18565d9f-12e6-40b1-9097-8e1ef9866c29" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Community_Friend', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110530 11:40:39', '82c1f83e-19ca-4567-8d02-3d8c271e91bd', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1912, N'ForumAttachment', N'Forums.ForumAttachment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_Attachment">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AttachmentID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AttachmentFileName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentFileExtension">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentBinary" type="xs:base64Binary" minOccurs="0" />
              <xs:element name="AttachmentGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AttachmentLastModified" type="xs:dateTime" />
              <xs:element name="AttachmentMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AttachmentFileSize" type="xs:int" />
              <xs:element name="AttachmentImageHeight" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentImageWidth" type="xs:int" minOccurs="0" />
              <xs:element name="AttachmentPostID" type="xs:int" />
              <xs:element name="AttachmentSiteID" type="xs:int" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_Attachment" />
      <xs:field xpath="AttachmentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AttachmentID" fieldcaption="AttachmentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8f5d85ba-6df6-4eb4-8974-17543631936a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentFileName" fieldcaption="AttachmentFileName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="52e2442a-8e21-448a-ba9f-870a254b50af" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentFileExtension" fieldcaption="AttachmentFileExtension" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="7fadf09b-8dee-489b-8d9b-7d25c6187aae" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentBinary" fieldcaption="AttachmentBinary" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3c04aa2f-f86a-458b-8aea-a75676cf565c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentGUID" fieldcaption="AttachmentGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ee2d7dc9-b1b8-495c-b5d1-72e41529c19b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="AttachmentLastModified" fieldcaption="AttachmentLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e44931f3-dfaa-46d2-9fbc-b2a1384a1d8a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field><field column="AttachmentMimeType" fieldcaption="AttachmentMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="934b7a20-9e7b-472c-80f6-55f06355c1db" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentFileSize" fieldcaption="AttachmentFileSize" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b1cef1bb-75f4-4d55-b915-9b56d3c44547" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentImageHeight" fieldcaption="AttachmentImageHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ed9f7f94-7442-4008-a13d-454059b5792c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentImageWidth" fieldcaption="AttachmentImageWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0dfa0f44-f8d3-4bf7-aa5e-75c02e22fd9f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentPostID" fieldcaption="AttachmentPostID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bcda16be-c192-41d3-8c5f-5711e9a812e6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="AttachmentSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="50fec5e1-a402-475a-a5e9-bdcd6cd24180" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field></form>', N'', N'', N'', N'Forums_Attachment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110607 08:55:36', '604d3bca-4ff8-47d7-9c18-c7637675ee04', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1913, N'ForumUserFavorites', N'Forums.ForumUserFavorites', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Forums_UserFavorites">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="FavoriteID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="PostID" type="xs:int" minOccurs="0" />
              <xs:element name="ForumID" type="xs:int" minOccurs="0" />
              <xs:element name="FavoriteName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SiteID" type="xs:int" />
              <xs:element name="FavoriteGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="FavoriteLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Forums_UserFavorites" />
      <xs:field xpath="FavoriteID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="FavoriteID" fieldcaption="FavoriteID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="b024900c-68e3-4fd8-a6e2-294e6b5aa3a5" /><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="13a08f04-c86f-4cc2-99b5-71ce271ceb0d" /><field column="PostID" fieldcaption="PostID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c88f5e6d-9851-479d-affd-f025eb91b53f" /><field column="ForumID" fieldcaption="ForumID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fb3ded78-3c7f-40fa-9633-4b091dab8f1a" /><field column="FavoriteName" fieldcaption="FavoriteName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="5a832edd-f529-4f92-bcb7-445372cc0cf7" /><field column="SiteID" visible="false" columntype="integer" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e2fc938c-660a-4c63-9200-dfe67bc5103e" /><field column="FavoriteGUID" visible="false" columntype="guid" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="63923c0b-402a-4652-b2f6-094f6f03c1b5" allowusertochangevisibility="false" /><field column="FavoriteLastModified" visible="false" columntype="datetime" fieldtype="label" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="36de5980-e5cd-4961-9b68-c4088f97f84f" allowusertochangevisibility="false"><settings><timezonetype>inherit</timezonetype></settings></field></form>', N'', N'', N'', N'Forums_UserFavorites', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110113 11:01:19', '14c4b710-4504-4c22-83bc-d460e13a7fd0', 0, 0, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1926, N'Email user', N'CMS.EmailUser', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="CMS_EmailRole">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="EmailID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="UserID" type="xs:int" />
              <xs:element name="LastSendResult" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="LastSendAttempt" type="xs:dateTime" minOccurs="0" />
              <xs:element name="Status" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//CMS_EmailRole" />
      <xs:field xpath="EmailID" />
      <xs:field xpath="UserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="EmailID" fieldcaption="EmailID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="46ad051a-403a-4ae1-a5cc-97ce20bc3682" /><field column="UserID" fieldcaption="UserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bd8d5cc3-cbf2-46b1-b187-e2429f53a12c" /><field column="LastSendResult" fieldcaption="LastSendResult" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1b5c35bd-1482-4dbc-b03d-1e603acbd35e" /><field column="LastSendAttempt" fieldcaption="LastSendAttempt" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f484db51-d9f5-41da-b811-42dc6e2d7a6d" /><field column="Status" fieldcaption="Status" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="883bbd0b-e27e-4d94-8e3f-92d2878a9af4" /></form>', N'', N'', N'', N'CMS_EmailUser', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110522 21:19:49', '047de70d-4d44-4530-9bcf-5baafad2d62d', 0, 0, 0, N'', 1, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [CMS_Class] OFF
