SET IDENTITY_INSERT [CMS_Class] ON
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1158, N'Ecommerce - Order', N'ecommerce.order', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_Order">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="OrderID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="OrderBillingAddressID" type="xs:int" />
              <xs:element name="OrderShippingAddressID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderShippingOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderTotalShipping" type="xs:double" minOccurs="0" />
              <xs:element name="OrderTotalPrice" type="xs:double" />
              <xs:element name="OrderTotalTax" type="xs:double" />
              <xs:element name="OrderDate" type="xs:dateTime" />
              <xs:element name="OrderStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderCurrencyID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderCustomerID" type="xs:int" />
              <xs:element name="OrderCreatedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderNote" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderSiteID" type="xs:int" />
              <xs:element name="OrderPaymentOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderInvoice" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderInvoiceNumber" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderDiscountCouponID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderCompanyAddressID" type="xs:int" minOccurs="0" />
              <xs:element name="OrderTrackingNumber" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderPaymentResult" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="OrderLastModified" type="xs:dateTime" />
              <xs:element name="OrderTotalPriceInMainCurrency" type="xs:double" minOccurs="0" />
              <xs:element name="OrderIsPaid" type="xs:boolean" minOccurs="0" />
              <xs:element name="OrderCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_Order" />
      <xs:field xpath="OrderID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="OrderID" fieldcaption="OrderID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f28fb94f-e177-48e6-aaf6-acfcd856ac3e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="OrderBillingAddressID" fieldcaption="OrderBillingAddressID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="27697294-392b-4a76-8a1d-e9a1cb05eb58" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderShippingAddressID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="26b8aeb8-fade-40be-9f3d-3ce906583670" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="OrderShippingOptionID" fieldcaption="OrderShippingOptionID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0eccbbc8-1bb0-4ef9-9c34-6d30c87ea79c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderTotalShipping" fieldcaption="OrderTotalShipping" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="075b59b9-762d-46f7-8e08-e0008ad055ea" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderTotalPrice" fieldcaption="OrderTotalPrice" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fef6bb8a-2c7d-4ea3-8335-920498657802" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderTotalPriceInMainCurrency" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a62bd496-944e-4f31-9f2d-530b849cd644" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="OrderTotalTax" fieldcaption="OrderTotalTax" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="786aede8-95e6-4d3a-a285-1627661b9ca7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderDate" fieldcaption="OrderDate" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fc38316b-50bd-49f2-b469-2a8649440ed8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname><editTime>false</editTime></settings></field><field column="OrderStatusID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="11250533-9320-4950-b72c-f44d48c6903c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="OrderCurrencyID" fieldcaption="OrderCurrencyID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="efeadf73-e9a5-4283-b015-0702a04163e9" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderCustomerID" fieldcaption="OrderCustomerID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ae5f163d-d345-459c-97e2-3adb7150195f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderCreatedByUserID" fieldcaption="OrderCreatedByUserID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1fda4f50-5db3-4803-aeb5-7c1ddfbf85ab" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderNote" fieldcaption="OrderNote" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ad9e3376-04c8-42b3-b405-d0f36bebb606" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="OrderSiteID" fieldcaption="OrderSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72c87c0f-a61b-4ff4-8c2b-4f6b510e03a1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderPaymentOptionID" fieldcaption="OrderPaymentOptionID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d7942b91-97c7-46d0-a4b5-770a006065e1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderInvoice" fieldcaption="OrderInvoice" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="da660e03-1666-44e4-b05c-15b52a9e2f44" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="OrderInvoiceNumber" fieldcaption="OrderInvoiceNumber" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="2e515b8c-5186-488f-8836-84ae1a3a73bb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderDiscountCouponID" fieldcaption="OrderDiscountCouponID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dceb6698-1196-4cd4-803e-bdc73f5c3413" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderCompanyAddressID" fieldcaption="OrderCompanyAddressID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43aac489-4194-4d9e-898c-bdbad5dc4f0d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderTrackingNumber" fieldcaption="OrderTrackingNumber" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="a8e47954-ef84-46cc-a2e7-c8ae6515b4c5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderCustomData" fieldcaption="OrderCustomData" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0162e231-adda-4951-b014-fde264ec2103" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="OrderPaymentResult" fieldcaption="OrderPaymentResult" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="59784e17-5d9d-48e0-93a1-ae6d97564121" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="OrderGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cfcbbfde-eda8-4fd8-b94c-7d92bac0b907" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="OrderLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0554677d-56cc-4b16-8992-7e02ae6c46f7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="OrderIsPaid" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4f3c7a42-baa6-44bc-8c52-2f18dc8e20b9" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="OrderCulture" fieldcaption="Culture" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="10" publicfield="false" spellcheck="true" guid="d4c36d0d-073d-4b64-a99c-5213b120cffc" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field></form>', N'', N'', N'', N'COM_Order', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110729 12:29:26', '58eb48fc-83f4-41f7-add2-bc3ce5de8909', 0, 1, 0, N'', 2, N'OrderID', N'0', N'', N'OrderLastModified', N'<search><item searchable="True" name="OrderID" tokenized="False" content="False" id="eeabfd04-b3dd-4d99-b656-f0abef3d468c" /><item searchable="True" name="OrderBillingAddressID" tokenized="False" content="False" id="1a5f4c67-3815-4f24-942b-66d517388c86" /><item searchable="True" name="OrderShippingAddressID" tokenized="False" content="False" id="ed4b6001-d65a-42b1-aed4-e8db4601dc45" /><item searchable="True" name="OrderShippingOptionID" tokenized="False" content="False" id="caa2b3dd-38b2-4041-8edb-d0cedcb68152" /><item searchable="True" name="OrderTotalShipping" tokenized="False" content="False" id="e59df6f0-a275-4e29-a67d-505a030900dd" /><item searchable="True" name="OrderTotalPrice" tokenized="False" content="False" id="91181b5c-6e21-4854-8c73-bc432f74093e" /><item searchable="True" name="OrderTotalTax" tokenized="False" content="False" id="1afe29be-f598-4b42-9062-f0dcb44b2714" /><item searchable="True" name="OrderDate" tokenized="False" content="False" id="01ae8919-418d-43c2-aba2-b8b30c0111b2" /><item searchable="True" name="OrderStatusID" tokenized="False" content="False" id="af522e39-0f42-41e3-a64f-5a91bd7a1ebb" /><item searchable="True" name="OrderCurrencyID" tokenized="False" content="False" id="8ce766ae-df06-464b-a120-93dbe99e55b9" /><item searchable="True" name="OrderCustomerID" tokenized="False" content="False" id="378a81fb-e51a-4288-bc88-def7e7921244" /><item searchable="True" name="OrderCreatedByUserID" tokenized="False" content="False" id="a0b7f51f-1f7e-4a50-85e0-0ef7abadd4ba" /><item searchable="False" name="OrderNote" tokenized="True" content="True" id="c8c5f815-714c-43b1-8809-af12e657494a" /><item searchable="True" name="OrderSiteID" tokenized="False" content="False" id="5009e672-8dfc-4941-a7de-8ca0705bc1b7" /><item searchable="True" name="OrderPaymentOptionID" tokenized="False" content="False" id="55f2f36f-7f03-4521-8ff4-44155d93d9f1" /><item searchable="False" name="OrderInvoice" tokenized="True" content="True" id="6770ca0f-0bc0-4cf0-85b6-d35c4b56f303" /><item searchable="False" name="OrderInvoiceNumber" tokenized="True" content="True" id="48cd47a4-b745-4aa6-8cc3-8fed99f67975" /><item searchable="True" name="OrderDiscountCouponID" tokenized="False" content="False" id="b8b082e8-3326-4b46-bee6-0f2b4760dd0c" /><item searchable="True" name="OrderCompanyAddressID" tokenized="False" content="False" id="8c6455e2-c515-426b-a889-d421ef210e1a" /><item searchable="False" name="OrderTrackingNumber" tokenized="True" content="True" id="464cfc17-c570-473b-af60-6be86fd4d032" /><item searchable="False" name="OrderCustomData" tokenized="True" content="True" id="8e09b135-71d7-4c71-928a-40dc07b0fe0f" /><item searchable="False" name="OrderPaymentResult" tokenized="True" content="True" id="b629e211-20aa-41b1-984c-4188d5c79728" /><item searchable="False" name="OrderGUID" tokenized="False" content="False" id="c0a5dbf1-a5ae-4a56-b75e-1b97b8ac9299" /><item searchable="True" name="OrderLastModified" tokenized="False" content="False" id="c25620f7-d5d0-4041-ade5-46c9a86091b6" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1159, N'Ecommerce - SKU', N'ecommerce.sku', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_SKU">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SKUID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SKUNumber" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUDescription" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUPrice" type="xs:double" />
              <xs:element name="SKUEnabled" type="xs:boolean" />
              <xs:element name="SKUDepartmentID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUManufacturerID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUInternalStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUPublicStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUSupplierID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUAvailableInDays" type="xs:int" minOccurs="0" />
              <xs:element name="SKUGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SKUImagePath" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUWeight" type="xs:double" minOccurs="0" />
              <xs:element name="SKUWidth" type="xs:double" minOccurs="0" />
              <xs:element name="SKUDepth" type="xs:double" minOccurs="0" />
              <xs:element name="SKUHeight" type="xs:double" minOccurs="0" />
              <xs:element name="SKUAvailableItems" type="xs:int" minOccurs="0" />
              <xs:element name="SKUSellOnlyAvailable" type="xs:boolean" minOccurs="0" />
              <xs:element name="SKUCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUOptionCategoryID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUOrder" type="xs:int" minOccurs="0" />
              <xs:element name="SKULastModified" type="xs:dateTime" />
              <xs:element name="SKUCreated" type="xs:dateTime" minOccurs="0" />
              <xs:element name="SKUSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="SKUPrivateDonation" type="xs:boolean" minOccurs="0" />
              <xs:element name="SKUNeedsShipping" type="xs:boolean" minOccurs="0" />
              <xs:element name="SKUMaxDownloads" type="xs:int" minOccurs="0" />
              <xs:element name="SKUValidUntil" type="xs:dateTime" minOccurs="0" />
              <xs:element name="SKUProductType" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUMaxItemsInOrder" type="xs:int" minOccurs="0" />
              <xs:element name="SKUMaxPrice" type="xs:double" minOccurs="0" />
              <xs:element name="SKUValidity" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUValidFor" type="xs:int" minOccurs="0" />
              <xs:element name="SKUMinPrice" type="xs:double" minOccurs="0" />
              <xs:element name="SKUMembershipGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="SKUConversionName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUConversionValue" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SKUBundleInventoryType" minOccurs="0">
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
      <xs:selector xpath=".//COM_SKU" />
      <xs:field xpath="SKUID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SKUID" fieldcaption="SKUID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="95abe990-8663-4a8d-8db4-a4d104579424" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SKUNumber" fieldcaption="SKUNumber" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="c37e01b4-9477-4ccd-86f8-b6f000743184" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUName" fieldcaption="SKUName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="440" publicfield="false" spellcheck="true" guid="161e6482-fbd6-41df-b251-5ef9603f8576" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUDescription" fieldcaption="SKUDescription" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b13a8a29-f8a9-49ed-8cb4-4db417e174ab" external="true" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="SKUPrice" fieldcaption="SKUPrice" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a3c3478f-0507-44ef-a8be-26f7fd3833c5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUEnabled" fieldcaption="SKUEnabled" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bedcaf24-ad52-4293-9418-f7fcecbb9811" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SKUDepartmentID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f97d35fc-6b5a-4c23-a666-15f8cfe7faa6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="SKUManufacturerID" fieldcaption="SKUManufacturerID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="808244ef-6595-42f2-90a0-9bf0717645e1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUInternalStatusID" fieldcaption="SKUInternalStatusID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e6053672-514c-4f66-8c2e-87da9dd3f310" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUPublicStatusID" fieldcaption="SKUPublicStatusID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9e5eaa25-43b3-40af-b651-94387a5b77ef" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUSupplierID" fieldcaption="SKUSupplierID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="57cc81c5-1f0c-45d4-9741-e37d7f35db34" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUAvailableInDays" fieldcaption="SKUAvailableInDays" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7d56b109-15d2-41ed-ab41-c505b50b8386" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="99228497-3209-44bd-8e5c-cd9de56e7fbd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="SKUImagePath" fieldcaption="SKUImagePath" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="6174e45a-ca37-4946-a8b9-a53bd4fb76d2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUWeight" fieldcaption="SKUWeight" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="433ec6bf-2a24-46f7-ab64-413723f8d2d4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUWidth" fieldcaption="SKUWidth" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cada95d5-f56f-47b8-8ec5-2311f6420e8f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUDepth" fieldcaption="SKUDepth" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01b371cb-8598-4409-82a5-955e5178dbfa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUHeight" fieldcaption="SKUHeight" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="960b12da-a78a-47af-b30a-dd5d4c963c7f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUAvailableItems" fieldcaption="SKUAvailableItems" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db263b45-b660-45e4-b02e-fa0819e75472" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SKUSellOnlyAvailable" fieldcaption="SKUSellOnlyAvailable" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ca3ef874-bccc-401d-ae31-f5b40c99f900" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SKUCustomData" fieldcaption="SKUCustomData" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="29e8c9c5-d3c7-4846-a18f-057d5cd0a352" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="SKUOptionCategoryID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5781a44f-28d4-4ac6-b393-55c9fb2b1f3a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SKUOrder" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3e41eb69-f9f4-42ea-a51b-f47ebb489b6e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SKULastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="87e7a912-8806-4971-9912-af8711f71707" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="SKUCreated" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="046115eb-09d5-4c9f-b4ae-e5da3761b436" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><timezonetype>inherit</timezonetype><controlname>labelcontrol</controlname></settings></field><field column="SKUSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="69b49087-e19b-4b94-990c-f174f146b7db" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SKUProductType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="33da4205-7ff2-46f9-ae88-3f28ae663ff8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field column="SKUMembershipGUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1bdbe0d4-4142-4aac-9eff-51709cfa8b06" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SKUValidity" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="24a8c5b1-f065-4a4b-a608-836e06d07083" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SKUValidFor" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e708cdc4-502c-46a3-b8a7-120167863a7e" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field column="SKUValidUntil" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fea9d168-8b89-4b05-8243-2cd26c6b9773" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SKUMaxDownloads" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5e1d81cf-b3bc-43cf-96eb-42e343da9eb8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SKUBundleInventoryType" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="50" publicfield="false" spellcheck="true" guid="4f095aab-f4fe-409b-9990-fd491bdfa7f8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>logiccaptcha</controlname></settings></field><field column="SKUPrivateDonation" fieldcaption="Private donation" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="72fe9474-dd53-40e2-b9c3-7501baded3bd" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SKUMinPrice" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f6464371-2fc6-42bb-a2f2-3021efb4c455" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Dialogs_Web_Hide>False</Dialogs_Web_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><controlname>bbeditorcontrol</controlname><Dialogs_Content_Hide>False</Dialogs_Content_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="SKUMaxPrice" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5fd253dd-6dfa-4a2b-a106-055fe2ab90a0" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="SKUNeedsShipping" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0fd1ef55-4dd6-4c0e-8030-244dda90ffa3" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="SKUMaxItemsInOrder" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cf170044-35ac-4710-b82f-4c0162d805ff" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" /><field column="SKUConversionName" visible="false" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="100" publicfield="false" spellcheck="true" guid="0132fdb3-0cfc-4966-99ce-a7d8cfd4bf9a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field><field column="SKUConversionValue" visible="false" defaultvalue="0" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="870a3279-5120-4bc6-817c-318360a7fbb3" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_SKU', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110916 16:47:27', 'f1349c42-bae7-4614-a2ec-a7e61d8867c5', 0, 1, 0, N'', 2, N'SKUName', N'SKUDescription', N'SKUImagePath', N'SKUCreated', N'<search><item searchable="True" name="SKUID" tokenized="False" content="False" id="e8cd0a02-e97f-4d13-8727-9c87168da965" /><item searchable="True" name="SKUNumber" tokenized="False" content="False" id="6f9ed819-2c5f-408e-ac0e-5fa3de4a98c6" /><item searchable="False" name="SKUName" tokenized="True" content="True" id="78d3186a-a4bb-4fe4-9a8c-ff128972b78e" /><item searchable="False" name="SKUDescription" tokenized="True" content="True" id="737d866d-27dc-4d56-8316-c852bf812a2b" /><item searchable="True" name="SKUPrice" tokenized="False" content="False" id="b5e2e9af-ccef-4d43-91d0-bb7abd484a83" /><item searchable="True" name="SKUEnabled" tokenized="False" content="False" id="8670d0c2-83c9-4fe8-b221-0185c629eb4e" /><item searchable="False" name="SKUDepartmentID" tokenized="False" content="False" id="194545f0-8728-4d82-8650-69565bdc9856" /><item searchable="False" name="SKUManufacturerID" tokenized="False" content="False" id="af463007-b6ca-45e5-ab43-0551f6c34a27" /><item searchable="False" name="SKUInternalStatusID" tokenized="False" content="False" id="bfb4a226-e397-4118-b636-0d4b6aea695e" /><item searchable="False" name="SKUPublicStatusID" tokenized="False" content="False" id="7b0a23bd-fbbe-4def-9060-2aada1f4ab18" /><item searchable="False" name="SKUSupplierID" tokenized="False" content="False" id="d9207d8d-d20e-4e66-a7f1-fac36e6411af" /><item searchable="True" name="SKUAvailableInDays" tokenized="False" content="False" id="1d207c82-a526-4511-842b-c866729be003" /><item searchable="False" name="SKUGUID" tokenized="False" content="False" id="9731a63e-2eca-4356-9fbc-2deecc75979f" /><item searchable="True" name="SKUImagePath" tokenized="False" content="False" id="52ee4669-65c2-4ddb-adf2-bd67fc9485ab" /><item searchable="True" name="SKUWeight" tokenized="False" content="False" id="d5ca4182-4574-4b97-b7be-8e25f978e8a3" /><item searchable="True" name="SKUWidth" tokenized="False" content="False" id="ba59754d-0bc6-4c97-ad9d-7c358976fa8b" /><item searchable="True" name="SKUDepth" tokenized="False" content="False" id="bda40c57-2349-4eb6-b75c-93adaae238a8" /><item searchable="True" name="SKUHeight" tokenized="False" content="False" id="ff6712e4-91e6-49f5-bff4-24e2c20e047d" /><item searchable="True" name="SKUAvailableItems" tokenized="False" content="False" id="b60e79ac-3f77-49de-ba09-3d310e1cafe3" /><item searchable="True" name="SKUSellOnlyAvailable" tokenized="False" content="False" id="48761ecf-ca94-4603-bdc4-aa8727dd20d8" /><item searchable="False" name="SKUCustomData" tokenized="False" content="False" id="61697694-47b9-4a21-86cb-84be932f7eb6" /><item searchable="False" name="SKUOptionCategoryID" tokenized="False" content="False" id="12b8007f-fc6f-4164-8f79-3b2a68d4c847" /><item searchable="True" name="SKUOrder" tokenized="False" content="False" id="efe0ee15-67f7-4fbb-8b5c-b807ef556762" /><item searchable="False" name="SKULastModified" tokenized="False" content="False" id="4443e4c0-6732-492b-b92d-321d9750e1e5" /><item searchable="True" name="SKUCreated" tokenized="False" content="False" id="99542edc-d460-4478-8328-9aa9daa94ed6" /><item searchable="False" name="SKUSiteID" tokenized="False" content="False" id="700b6bda-1784-45e1-9390-689daac73088" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1161, N'Ecommerce - Order item', N'ecommerce.orderitem', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_OrderItem">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="OrderItemID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="OrderItemOrderID" type="xs:int" />
              <xs:element name="OrderItemSKUID" type="xs:int" />
              <xs:element name="OrderItemSKUName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderItemUnitPrice" type="xs:double" />
              <xs:element name="OrderItemUnitCount" type="xs:int" />
              <xs:element name="OrderItemCustomData" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderItemGuid" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="OrderItemParentGuid" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="OrderItemLastModified" type="xs:dateTime" />
              <xs:element name="OrderItemIsPrivate" type="xs:boolean" minOccurs="0" />
              <xs:element name="OrderItemSKU" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderItemValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="OrderItemBundleGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="OrderItemTotalPriceInMainCurrency" type="xs:double" minOccurs="0" />
              <xs:element name="OrderItemSendNotification" type="xs:boolean" minOccurs="0" />
              <xs:element name="OrderItemText" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="OrderItemPrice" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_OrderItem" />
      <xs:field xpath="OrderItemID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="OrderItemID" fieldcaption="OrderItemID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d4f766eb-288a-4b3e-87ea-d0929b2cd93f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="OrderItemOrderID" fieldcaption="OrderItemOrderID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6ff5cd02-cfdf-418e-b6df-dfb477ed8819" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemSKUID" fieldcaption="OrderItemSKUID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="94d1bc70-53c7-4753-9652-5f6929c372c8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemSKUName" fieldcaption="OrderItemSKUName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="4cfc2089-cbc3-4e95-9135-ff18a4602836" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemUnitPrice" fieldcaption="OrderItemUnitPrice" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="76c993a3-e9c5-416f-96d7-7a0a8985cd42" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemUnitCount" fieldcaption="OrderItemUnitCount" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aa23435f-dc7b-4836-9056-79e473c56153" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemCustomData" fieldcaption="OrderItemCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="274c6fda-298f-4dcd-84e8-2fdb1ec00f45" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="OrderItemGuid" fieldcaption="OrderItemSKUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8d32219d-22fd-4305-8ad8-fcc9053e35c5" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemParentGuid" fieldcaption="OrderItemSKUID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="581d726c-a02b-43f1-aa1d-1976a35aa14a" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="OrderItemBundleGUID" fieldcaption="OrderItemID" visible="false" columntype="guid" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="61e5d45b-8b47-4183-bf31-db5342f38738" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="OrderItemLastModified" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="599a7c93-bc13-497e-8f08-8da4a23e093a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="OrderItemIsPrivate" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b4cd1d3c-39c6-4aff-a866-d66ba0587a7c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="OrderItemValidTo" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1e3612e9-db5a-4158-a63b-a2afeaa9de56" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="OrderItemSendNotification" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2426d80e-7a3a-44fe-8c54-033ea0cfd892" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="OrderItemSKU" fieldcaption="OrderItemID" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="488da930-0ccb-4b81-bca1-05d073755331" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="OrderItemTotalPriceInMainCurrency" fieldcaption="OrderItemID" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="32aaf914-dd2f-4240-91b3-1417125a40de" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="OrderItemText" visible="false" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6d24de89-1a8d-43eb-b4a4-4c32a4420b30" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><Dialogs_Web_Hide>False</Dialogs_Web_Hide><Dialogs_Attachments_Hide>False</Dialogs_Attachments_Hide><ShowAdvancedImage>False</ShowAdvancedImage><ShowAdvancedUrl>False</ShowAdvancedUrl><Dialogs_Anchor_Hide>False</Dialogs_Anchor_Hide><Dialogs_Libraries_Hide>False</Dialogs_Libraries_Hide><controlname>bbeditorcontrol</controlname><Dialogs_Content_Hide>False</Dialogs_Content_Hide><Dialogs_Email_Hide>False</Dialogs_Email_Hide><Autoresize_Hashtable>True</Autoresize_Hashtable></settings></field><field column="OrderItemPrice" visible="false" columntype="double" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5ff2a596-54db-45dc-aebc-22c7f973489d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_OrderItem', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110825 09:35:05', '201d37f4-8961-45d1-9b34-05303f8df065', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1162, N'Ecommerce - Shopping cart', N'ecommerce.shoppingcart', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_ShoppingCart">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ShoppingCartID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ShoppingCartGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ShoppingCartUserID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartSiteID" type="xs:int" />
              <xs:element name="ShoppingCartLastUpdate" type="xs:dateTime" />
              <xs:element name="ShoppingCartCurrencyID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartPaymentOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartShippingOptionID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartDiscountCouponID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartBillingAddressID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartShippingAddressID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartCustomerID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartNote" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ShoppingCartCompanyAddressID" type="xs:int" minOccurs="0" />
              <xs:element name="ShoppingCartCustomData" minOccurs="0">
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
      <xs:selector xpath=".//COM_ShoppingCart" />
      <xs:field xpath="ShoppingCartID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ShoppingCartID" fieldcaption="ShoppingCartID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="c46cbaf5-3ff2-4503-a206-d6f9c7967de6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ShoppingCartGUID" fieldcaption="ShoppingCartGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="595611ae-e9d0-48bc-8dc4-c03f1aedaca6" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ShoppingCartUserID" fieldcaption="ShoppingCartUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4d7e8b85-0d95-45b8-8667-2149bbd30656" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartSiteID" fieldcaption="ShoppingCartSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="970e2c1d-f283-4280-8f58-3032e55473c2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartLastUpdate" fieldcaption="ShoppingCartLastUpdate" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fc89a87d-246c-41da-b28d-ce9577e7d705" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ShoppingCartCurrencyID" fieldcaption="ShoppingCartCurrencyID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fa403d6a-27e8-4c85-af97-4e2653f9823b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartPaymentOptionID" fieldcaption="ShoppingCartPaymentOptionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b4157cc5-c600-45af-b243-c4caea0d2b50" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartShippingOptionID" fieldcaption="ShoppingCartShippingOptionID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="58c0aa4e-e24e-4776-bb4b-e3adc1a67e63" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartDiscountCouponID" fieldcaption="ShoppingCartDiscountCouponID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82f0b1e8-f3ac-4918-b414-c056744c346f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartBillingAddressID" fieldcaption="ShoppingCartBillingAddressID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="00ab0979-20f9-452c-8cb0-09cf2ef776b6" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartShippingAddressID" fieldcaption="ShoppingCartShippingAddressID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7014e4e5-6067-416b-bd27-2815fad98bea" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartCustomerID" fieldcaption="ShoppingCartCustomerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="746649e0-c4fd-4b06-a05a-908cb3b3b875" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartNote" fieldcaption="ShoppingCartNote" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0fcd34b7-3982-44ef-914d-922ed3f99395" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="ShoppingCartCompanyAddressID" fieldcaption="ShoppingCartCompanyAddressID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="40009fbf-c9b9-41c2-b717-491de0cfb316" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ShoppingCartCustomData" fieldcaption="ShoppingCartCustomData" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43d0e526-c14a-4406-a7d1-eaa5ebc6636a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_ShoppingCart', N'', N'', N'', N'', 1, 0, 0, N'', 0, N'', NULL, '20110331 10:04:23', 'c8a865c2-df9e-4f10-9b9e-f78bc0926f15', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1163, N'Ecommerce - Order status - user', N'ecommerce.OrderStatusUser', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_OrderStatusUser">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="OrderStatusUserID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="OrderID" type="xs:int" />
              <xs:element name="FromStatusID" type="xs:int" minOccurs="0" />
              <xs:element name="ToStatusID" type="xs:int" />
              <xs:element name="ChangedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="Date" type="xs:dateTime" />
              <xs:element name="Note" minOccurs="0">
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
      <xs:selector xpath=".//COM_OrderStatusUser" />
      <xs:field xpath="OrderStatusUserID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="OrderStatusUserID" fieldcaption="OrderStatusUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="32fe6606-1f77-42bc-8835-b4b8d09b1770" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="OrderID" fieldcaption="OrderID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="89c15ea5-4d0a-485a-8648-bf16a26608c8" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="FromStatusID" fieldcaption="FromStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4c7ca64c-b914-422b-95d5-aa390862dace" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ToStatusID" fieldcaption="ToStatusID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="02f7c0cb-be80-4f2a-a4d3-83a4f834a921" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ChangedByUserID" fieldcaption="ChangedByUserID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a01e87ea-ab09-40fa-87cd-d0a76b2dc3cf" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="Date" fieldcaption="Date" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ddb5c3f2-4e45-4a6c-b9bc-bdb20510d49d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="Note" fieldcaption="Note" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cb010f29-a80d-4232-b876-2c6a4fc48ad2" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_OrderStatusUser', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110615 10:50:36', 'd196077d-d956-4c09-9ffd-f8239ac3d81c', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1202, N'Ecommerce - Exchange rate', N'ecommerce.exchangerate', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_CurrencyExchangeRate">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ExchagneRateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ExchangeRateToCurrencyID" type="xs:int" />
              <xs:element name="ExchangeRateValue" type="xs:double" />
              <xs:element name="ExchangeTableID" type="xs:int" />
              <xs:element name="ExchangeRateGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="ExchangeRateLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_CurrencyExchangeRate" />
      <xs:field xpath="ExchagneRateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ExchagneRateID" fieldcaption="ExchagneRateID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="52028791-7170-424b-b587-3f0f3892084a" /><field column="ExchangeRateToCurrencyID" fieldcaption="ExchangeRateToCurrencyID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ae3af6c9-5fd8-44a7-b93c-9e42115aeff4" /><field column="ExchangeRateValue" fieldcaption="ExchangeRateValue" visible="true" columntype="double" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c68f77b3-fdb2-4f52-bb6f-848e143a836e" /><field column="ExchangeTableID" fieldcaption="ExchangeTableID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="008e3a69-256a-498e-9333-88d7c3d9e02e" /><field column="ExchangeRateGUID" fieldcaption="ExchangeRateGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a94dd2fb-461a-4433-b3f0-554ce6da1f20" /><field column="ExchangeRateLastModified" fieldcaption="ExchangeRateLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="147c537e-88d2-4a8e-9fd0-35ccf666780f" /></form>', N'', N'', N'', N'COM_CurrencyExchangeRate', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110401 14:03:52', '8cae6e0b-2da2-46a7-9a0b-fa73aed5e96b', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1330, N'Polls - Poll', N'polls.poll', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Polls_Poll">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="PollID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="PollCodeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PollDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PollTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PollOpenFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="PollOpenTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="PollAllowMultipleAnswers" type="xs:boolean" />
              <xs:element name="PollQuestion">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PollAccess" type="xs:int" />
              <xs:element name="PollResponseMessage" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="PollGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="PollLastModified" type="xs:dateTime" />
              <xs:element name="PollGroupID" type="xs:int" minOccurs="0" />
              <xs:element name="PollSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="PollLogActivity" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Polls_Poll" />
      <xs:field xpath="PollID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="PollID" fieldcaption="PollID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="2017eced-1133-44d2-be9e-0271eaa136f0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PollCodeName" fieldcaption="PollCodeName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="07bdbe7e-259c-4b7a-81ce-e215b3900278" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollDisplayName" fieldcaption="PollDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4d725c85-2113-4963-a259-2f3f71cb929b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollTitle" fieldcaption="PollTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="66317519-d26b-4dae-8c23-6a7e2a8ff1e7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollOpenFrom" fieldcaption="PollOpenFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="70f42b69-fb44-4801-98af-4b9d7bfe4422" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PollOpenTo" fieldcaption="PollOpenTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="136186d2-5e14-4878-bca1-a24d6e0b3659" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PollAllowMultipleAnswers" fieldcaption="PollAllowMultipleAnswers" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ae1ce3ca-db21-493a-9180-12cf78b0b701" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="PollQuestion" fieldcaption="PollQuestion" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="972d4368-36b0-4fef-b44d-574f7cf18cec" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollAccess" fieldcaption="PollAccess" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9b4f22c7-7702-40f7-a86e-219118b55527" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollResponseMessage" fieldcaption="PollResponseMessage" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cd01bedf-1c05-42c4-9e3e-272e157c56ba" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollGUID" fieldcaption="PollGUID" visible="true" columntype="guid" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0cf99ca0-4a0a-4121-ab1c-4a083feb3b94" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="PollLastModified" fieldcaption="PollLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="61add291-4cc4-43b8-b72e-eb9fa0a408ef" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="PollGroupID" fieldcaption="PollGroupID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="16f8e1a1-7af7-4c7e-bfcb-afb2ba399af4" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollSiteID" fieldcaption="PollSiteID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c6bf0202-7332-4176-8a24-efc1712f1e34" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="PollLogActivity" fieldcaption="Log on-line marketing activity" visible="true" defaultvalue="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a9a31dc6-516f-442d-b75a-07b820128e29" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Polls_Poll', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20111020 16:39:33', '35aefb4e-1944-48af-8725-0aedf4bb17be', 0, 1, 0, N'', 2, N'PollTitle', N'PollQuestion', N'', N'PollLastModified', N'<search><item searchable="True" name="PollID" tokenized="False" content="False" id="ff0639ab-5b37-4043-b9f5-2ac57c37b1fc" /><item searchable="False" name="PollCodeName" tokenized="True" content="True" id="0613dbac-1c14-4b79-9011-2fdb2b936907" /><item searchable="False" name="PollDisplayName" tokenized="True" content="True" id="2faf7297-72ba-4fe2-94a1-180f0bb18c29" /><item searchable="False" name="PollTitle" tokenized="True" content="True" id="77df3c20-e914-4c98-8eea-758ad5654617" /><item searchable="True" name="PollOpenFrom" tokenized="False" content="False" id="a525135d-dacf-4691-bcdb-dc1bba5721e3" /><item searchable="True" name="PollOpenTo" tokenized="False" content="False" id="0e2e0fb6-1616-4181-86f5-8c6618b50f5f" /><item searchable="True" name="PollAllowMultipleAnswers" tokenized="False" content="False" id="aa4e3aae-54ff-4051-be2d-ffe1b5bfdb17" /><item searchable="False" name="PollQuestion" tokenized="True" content="True" id="dd30deeb-aaac-46fa-bb48-2d0268dfbfb0" /><item searchable="True" name="PollAccess" tokenized="False" content="False" id="2a4d84aa-7631-419b-bf53-2c35c7ec9ee9" /><item searchable="False" name="PollResponseMessage" tokenized="True" content="True" id="950ab094-3989-4e4a-b474-9a9be28e9068" /><item searchable="False" name="PollGUID" tokenized="False" content="False" id="541c8c9e-fc0f-4ef7-98ce-ff18459c05d9" /><item searchable="True" name="PollLastModified" tokenized="False" content="False" id="788f10c4-a787-407e-aeda-f36f372d235d" /><item searchable="True" name="PollGroupID" tokenized="False" content="False" id="42db84c9-9d30-4494-b824-e4e98a4fe494" /><item searchable="True" name="PollSiteID" tokenized="False" content="False" id="e627d62d-f9fb-49c3-919c-8c9c99c8d998" /></search>', NULL, 1, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1331, N'Polls - Poll answer', N'polls.pollanswer', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Polls_PollAnswer">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="AnswerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="AnswerText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="AnswerOrder" type="xs:int" minOccurs="0" />
              <xs:element name="AnswerCount" type="xs:int" minOccurs="0" />
              <xs:element name="AnswerEnabled" type="xs:boolean" minOccurs="0" />
              <xs:element name="AnswerPollID" type="xs:int" />
              <xs:element name="AnswerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="AnswerLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Polls_PollAnswer" />
      <xs:field xpath="AnswerID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="AnswerID" fieldcaption="AnswerID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="85aa83fe-c59b-4848-808b-e4933cc873f0" /><field column="AnswerText" fieldcaption="AnswerText" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="462e0935-c9a8-42ef-a8b4-8c9de332491d" /><field column="AnswerOrder" fieldcaption="AnswerOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="641e9f4c-7afb-45e5-8e74-dbf316fa5fe2" /><field column="AnswerCount" fieldcaption="AnswerCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ea95f511-6c0f-431c-a3ac-b6f7824bc5ab" /><field column="AnswerEnabled" fieldcaption="AnswerEnabled" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="75e6c0d8-b0ca-41d5-b960-a3c3d25ef4a7" /><field column="AnswerPollID" fieldcaption="AnswerPollID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="18311baa-1a61-4aaf-9256-56d7b3be5afd" /><field column="AnswerGUID" fieldcaption="AnswerGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a0d3438-6e13-4ebf-b306-fae1d444f5f3" /><field column="AnswerLastModified" fieldcaption="AnswerLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8c7645ba-c9da-4440-af55-9765b8e8f056" /></form>', N'', N'', N'', N'Polls_PollAnswer', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:56:29', '6f77a516-ab75-412e-a75f-2602e0dcb293', 0, 1, 0, N'', 2, N'AnswerText', N'0', N'', N'AnswerLastModified', N'<search><item searchable="True" name="AnswerID" tokenized="False" content="False" id="372c8a91-54a3-442e-ba42-af17e4673177" /><item searchable="False" name="AnswerText" tokenized="True" content="True" id="c09abdc5-488d-4275-ad9f-03bbfffaea7d" /><item searchable="True" name="AnswerOrder" tokenized="False" content="False" id="78ab655c-19c5-4805-8768-1e9afebd10ee" /><item searchable="True" name="AnswerCount" tokenized="False" content="False" id="19ac3383-bb83-45ea-a004-7e73db6c81a3" /><item searchable="True" name="AnswerEnabled" tokenized="False" content="False" id="08d99bf8-d596-4780-b478-9a79502efa5c" /><item searchable="True" name="AnswerPollID" tokenized="False" content="False" id="2cd5c9bc-ba2d-47da-b17b-ae53b0cfe33e" /><item searchable="False" name="AnswerGUID" tokenized="False" content="False" id="ee8fb2fa-2914-4d48-ad58-394cd234a95e" /><item searchable="True" name="AnswerLastModified" tokenized="False" content="False" id="cd5ea8ee-2fc3-48fc-b9b5-95db432f7b00" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1338, N'Report', N'Reporting.Report', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_Report">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ReportID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ReportName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="440" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportLayout">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportParameters">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ReportCategoryID" type="xs:int" />
              <xs:element name="ReportAccess" type="xs:int" />
              <xs:element name="ReportGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="ReportLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_Report" />
      <xs:field xpath="ReportID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ReportID" fieldcaption="ReportID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="54774329-35c0-4d7e-8e70-107db2efcc01" /><field column="ReportName" fieldcaption="ReportName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fcb403e6-9944-4782-a409-d6b0e7845ce9" /><field column="ReportDisplayName" fieldcaption="ReportDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9b994700-6a2d-451e-9ef8-8013fd3365ef" columnsize="440" visibility="none" ismacro="false" /><field column="ReportLayout" fieldcaption="ReportLayout" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fd743157-93fe-4dbc-8db1-6b0218205c2a" /><field column="ReportParameters" fieldcaption="ReportParameters" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1c648af7-cf8d-49ff-8d2d-d152aaff301e" /><field column="ReportCategoryID" fieldcaption="ReportCategoryID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8c8f484d-4f01-4359-8cb6-1a5ae63e1d78" /><field column="ReportAccess" fieldcaption="ReportAccess" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3b798284-728b-46da-a669-069988e7f440" /><field column="ReportGUID" fieldcaption="ReportGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bd7e8bd6-a0fe-4a0e-bce4-ab6d84aef49c" /><field column="ReportLastModified" fieldcaption="ReportLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="639711b2-c908-449d-8946-ce9b2ef87ac8" /></form>', N'', N'', N'', N'Reporting_Report', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110530 13:44:05', '8e5b3d49-5cf9-487a-afc3-164d26fdaa2e', 0, 1, 0, N'', 2, N'ReportDisplayName', N'0', N'', N'ReportLastModified', N'<search><item searchable="True" name="ReportID" tokenized="False" content="False" id="168cada2-ebef-40b2-84fd-80b95c2dbe87" /><item searchable="False" name="ReportName" tokenized="True" content="True" id="06c43aca-7018-4c80-b711-c3475e180129" /><item searchable="False" name="ReportDisplayName" tokenized="True" content="True" id="bca6a536-52e5-4c4b-a05d-1a72e422ed09" /><item searchable="False" name="ReportLayout" tokenized="True" content="True" id="6c19874f-e58e-4e6c-91b9-12be269a2562" /><item searchable="False" name="ReportParameters" tokenized="True" content="True" id="2c8b6757-2c4c-4563-b126-f819d6d4ebfc" /><item searchable="True" name="ReportCategoryID" tokenized="False" content="False" id="5ebf65be-4e97-469f-83cf-dfffe6561dea" /><item searchable="True" name="ReportAccess" tokenized="False" content="False" id="8a5886c4-9d11-4da6-83e5-ea5ffe5e8991" /><item searchable="False" name="ReportGUID" tokenized="False" content="False" id="b97f59b9-9e97-44ae-b430-ea8f8c823d65" /><item searchable="True" name="ReportLastModified" tokenized="False" content="False" id="3a7dbdc6-ada3-4886-9204-7ce33c5453bd" /></search>', NULL, 1, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1339, N'Report graph', N'Reporting.ReportGraph', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_ReportGraph">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="GraphID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="GraphName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphQuery">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphQueryIsStoredProcedure" type="xs:boolean" />
              <xs:element name="GraphType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="50" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphReportID" type="xs:int" />
              <xs:element name="GraphTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphXAxisTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphYAxisTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphWidth" type="xs:int" minOccurs="0" />
              <xs:element name="GraphHeight" type="xs:int" minOccurs="0" />
              <xs:element name="GraphLegendPosition" type="xs:int" minOccurs="0" />
              <xs:element name="GraphSettings" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="2147483647" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="GraphGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="GraphLastModified" type="xs:dateTime" />
              <xs:element name="GraphIsHtml" type="xs:boolean" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_ReportGraph" />
      <xs:field xpath="GraphID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="GraphID" fieldcaption="GraphID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="eed4199f-210d-4306-b919-28477fee1a6f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="GraphName" fieldcaption="GraphName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="cfdc850b-9ac0-4671-98e8-2bd0ebe42b5e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphDisplayName" fieldcaption="GraphDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="92eec597-ebd4-44c8-ab5a-39f72b089a2e" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphQuery" fieldcaption="GraphQuery" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e201f69e-2c44-457b-b944-21c4958d588a" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="GraphQueryIsStoredProcedure" fieldcaption="GraphQueryIsStoredProcedure" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="95801c24-5c35-435d-889d-a39f003ebd1c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="GraphType" fieldcaption="GraphType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="075c428a-5a6e-4922-a490-23c5d36e77c0" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphReportID" fieldcaption="GraphReportID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ebb98f15-b8e5-49df-9730-d96f455ab409" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphTitle" fieldcaption="GraphTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="60cc5183-b44c-44f2-9c65-83e667e4b0e3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphXAxisTitle" fieldcaption="GraphXAxisTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dc023e32-ae9a-4564-9d1e-f04659ea61fa" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphYAxisTitle" fieldcaption="GraphYAxisTitle" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c0370641-69cb-43c0-ad3d-a5322d9791b7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphWidth" fieldcaption="GraphWidth" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="938fbf3b-145e-4577-baca-146528b53315" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphHeight" fieldcaption="GraphHeight" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f076698-3fa5-4b0e-aec0-5e27ec801ac7" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphLegendPosition" fieldcaption="GraphLegendPosition" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9b38f89d-4d0d-4dcc-ac84-e39d6e906e92" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="GraphSettings" fieldcaption="GraphSettings" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c8ddc397-c8bc-4909-b715-4db752b69d0c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textareacontrol</controlname></settings></field><field column="GraphGUID" fieldcaption="GraphGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6d952912-2be9-4b73-885b-53159198072d" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="GraphLastModified" fieldcaption="GraphLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4cd06d92-b729-47f2-8b52-4a034e80cedb" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="GraphIsHtml" visible="false" defaultvalue="false" columntype="boolean" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="951adf33-476b-45b1-b1b2-5ab37254104b" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'Reporting_ReportGraph', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110428 02:49:16', 'f4e23a8d-9b99-4fdc-8e82-8ceb69fa5cd7', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1341, N'Report table', N'Reporting.ReportTable', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_ReportTable">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TableID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TableName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TableDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TableQuery">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TableQueryIsStoredProcedure" type="xs:boolean" />
              <xs:element name="TableReportID" type="xs:int" />
              <xs:element name="TableSettings" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="TableGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="TableLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_ReportTable" />
      <xs:field xpath="TableID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TableID" fieldcaption="TableID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="01ec14c3-a29d-4d45-8991-00c753d51f1b" /><field column="TableName" fieldcaption="TableName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b5bc8fb2-4fac-4de3-8417-893b1c570458" /><field column="TableDisplayName" fieldcaption="TableDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="db0bbc1e-9682-4ba2-b02e-cb94f28bcfe2" /><field column="TableQuery" fieldcaption="TableQuery" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5a207185-1804-4523-83f0-53e232b487fd" /><field column="TableQueryIsStoredProcedure" fieldcaption="TableQueryIsStoredProcedure" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="da5e0111-43ae-445e-89aa-5cef08f0da2e" /><field column="TableReportID" fieldcaption="TableReportID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e1b60a01-8bcc-4982-be5d-b0c36c91ef99" /><field column="TableSettings" fieldcaption="TableSettings" visible="true" columntype="longtext" fieldtype="textarea" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4d6d2a3d-b807-4eeb-896d-137fde611c10" /><field column="TableGUID" fieldcaption="TableGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c25fbaa2-44aa-4d61-826f-33eb597ad524" /><field column="TableLastModified" fieldcaption="TableLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5ab4d460-8534-41ab-a4da-79940336ee0c" /></form>', N'', N'', N'', N'Reporting_ReportTable', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110428 02:28:26', 'dc3c643e-3f7b-455a-88e5-4b95cf964756', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1342, N'Report value', N'Reporting.ReportValue', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_ReportValue">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="ValueID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="ValueName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ValueDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ValueQuery">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ValueQueryIsStoredProcedure" type="xs:boolean" />
              <xs:element name="ValueFormatString" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="ValueReportID" type="xs:int" />
              <xs:element name="ValueGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="ValueLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_ReportValue" />
      <xs:field xpath="ValueID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="ValueID" fieldcaption="ValueID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="f1e02451-4522-48b3-9ae4-afdfd9b4ead8" /><field column="ValueName" fieldcaption="ValueName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d72af990-71b1-4cb4-a94c-f9285a75bd53" /><field column="ValueDisplayName" fieldcaption="ValueDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="641aeca8-f17c-4bd1-bbfe-ddb77396d1a9" /><field column="ValueQuery" fieldcaption="ValueQuery" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="82b749ae-b6c5-41ff-90fd-9f5965ab70f0" /><field column="ValueQueryIsStoredProcedure" fieldcaption="ValueQueryIsStoredProcedure" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4709fd4f-4fda-4fb3-bd45-f08a3c6024d8" /><field column="ValueFormatString" fieldcaption="ValueFormatString" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3ba99cb4-40ae-4e91-947d-d280be7a825b" /><field column="ValueReportID" fieldcaption="ValueReportID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="aabffc3a-2a0a-4321-9540-351f16510842" /><field column="ValueGUID" fieldcaption="ValueGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8601ebe4-5f0a-4e76-9b96-e2a9fe92a0c3" /><field column="ValueLastModified" fieldcaption="ValueLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="933bc040-9b40-4524-91e2-9535d339d7e3" /></form>', N'', N'', N'', N'Reporting_ReportValue', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110428 00:17:28', 'a131b916-b2e3-4a9a-ae29-29d12bb6bbbf', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1349, N'Reporting saved graph', N'Reporting.SavedGraph', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_SavedGraph">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SavedGraphID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SavedGraphSavedReportID" type="xs:int" />
              <xs:element name="SavedGraphGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SavedGraphBinary" type="xs:base64Binary" />
              <xs:element name="SavedGraphMimeType">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SavedGraphLastModified" type="xs:dateTime" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_SavedGraph" />
      <xs:field xpath="SavedGraphID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SavedGraphID" fieldcaption="SavedGraphID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="efd03c4f-4299-4abb-b589-af20b0b74a9b" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="SavedGraphSavedReportID" fieldcaption="SavedGraphSavedReportID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d2562dcd-50e6-4a23-aacc-c34c931d84ae" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SavedGraphGUID" fieldcaption="SavedGraphGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c453fcc2-d4b0-40c2-ba6c-071f1da64242" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>unknown</controlname></settings></field><field column="SavedGraphBinary" fieldcaption="SavedGraphBinary" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="8d10643c-ed3d-4eea-8603-34b058fb0a87" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SavedGraphMimeType" fieldcaption="SavedGraphMimeType" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7e1dcef8-bf43-4300-819b-cf4aca678884" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="SavedGraphLastModified" fieldcaption="SavedGraphLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3b124fbb-878f-4586-8cdb-5adf09eb571d" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><DisplayNow>True</DisplayNow><TimeZoneType>inherit</TimeZoneType><controlname>calendarcontrol</controlname><EditTime>True</EditTime></settings></field></form>', N'', N'', N'', N'Reporting_SavedGraph', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110607 08:54:38', 'a9b8cccf-a725-4101-8ba3-41cada06c8d4', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1350, N'Reporting saved report', N'Reporting.SavedReport', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_SavedReport">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="SavedReportID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="SavedReportReportID" type="xs:int" />
              <xs:element name="SavedReportGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="SavedReportTitle" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SavedReportDate" type="xs:dateTime" />
              <xs:element name="SavedReportHTML">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SavedReportParameters">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="SavedReportCreatedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="SavedReportLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_SavedReport" />
      <xs:field xpath="SavedReportID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="SavedReportID" fieldcaption="SavedReportID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0575d5aa-5090-47cd-ab7e-a09f906d11f4" /><field column="SavedReportReportID" fieldcaption="SavedReportReportID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="65296e8c-a796-475f-96c8-7e37d7ad4bf1" /><field column="SavedReportGUID" fieldcaption="SavedReportGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1423e2f0-1274-40f7-ad2a-4599e0a8d6a1" /><field column="SavedReportTitle" fieldcaption="SavedReportTitle" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="64328433-acbc-40de-a208-cadeb5a4d4ca" /><field column="SavedReportDate" fieldcaption="SavedReportDate" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="935d44a6-b553-4e7c-ab84-16db554c92a9" /><field column="SavedReportHTML" fieldcaption="SavedReportHTML" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e9416b58-29ee-4fa4-9bf9-4cdc2adefa4a" /><field column="SavedReportParameters" fieldcaption="SavedReportParameters" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2acae724-da8e-42a9-84b1-7ec1e546ea61" /><field column="SavedReportCreatedByUserID" fieldcaption="SavedReportCreatedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c12cd366-c374-4538-9e2d-29d6b57b287d" /><field column="SavedReportLastModified" fieldcaption="SavedReportLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="66f9efe1-0ac0-43bc-a6af-010b9d0e564b" /></form>', N'', N'', N'', N'Reporting_SavedReport', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110331 09:54:25', '0d57ce56-d0bf-49f3-8a94-240268deec66', 0, 1, 0, N'', 2, N'SavedReportTitle', N'0', N'', N'SavedReportDate', N'<search><item searchable="True" name="SavedReportID" tokenized="False" content="False" id="f7458b88-366e-4bca-92a0-3d57c1ee1451" /><item searchable="True" name="SavedReportReportID" tokenized="False" content="False" id="09e62568-1ee5-41e1-b156-0f167271afda" /><item searchable="False" name="SavedReportGUID" tokenized="False" content="False" id="4cee3552-b618-4c7f-aa6e-2e4ff124ae2b" /><item searchable="False" name="SavedReportTitle" tokenized="True" content="True" id="fc0a1143-6a9c-44e6-a29c-6bdab86c49d0" /><item searchable="True" name="SavedReportDate" tokenized="False" content="False" id="68344dc3-a019-4c4b-ae4a-61c27362846d" /><item searchable="False" name="SavedReportHTML" tokenized="True" content="True" id="a7bb6e4b-5a1a-4c39-acc5-3ee5d9219050" /><item searchable="False" name="SavedReportParameters" tokenized="True" content="True" id="15d63080-2df3-4106-9681-37e9c3fb703c" /><item searchable="True" name="SavedReportCreatedByUserID" tokenized="False" content="False" id="4a358047-e4af-4933-921c-31b386c34d84" /><item searchable="True" name="SavedReportLastModified" tokenized="False" content="False" id="50f87b34-d881-49d4-9bf7-345209d1c0ad" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1360, N'Blog comment', N'blog.comment', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Blog_Comment">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="CommentID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="CommentUserName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CommentUserID" type="xs:int" minOccurs="0" />
              <xs:element name="CommentUrl" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CommentText">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="1073741823" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CommentApprovedByUserID" type="xs:int" minOccurs="0" />
              <xs:element name="CommentPostDocumentID" type="xs:int" />
              <xs:element name="CommentDate" type="xs:dateTime" />
              <xs:element name="CommentIsSpam" type="xs:boolean" minOccurs="0" />
              <xs:element name="CommentApproved" type="xs:boolean" minOccurs="0" />
              <xs:element name="CommentIsTrackBack" type="xs:boolean" minOccurs="0" />
              <xs:element name="CommentEmail" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="250" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Blog_Comment" />
      <xs:field xpath="CommentID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CommentID" fieldcaption="CommentID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="8ab031c9-df3d-4c65-9016-289e5c8469e7" /><field column="CommentUserName" fieldcaption="CommentUserName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="200" publicfield="false" spellcheck="true" guid="6a4c697c-0881-4e38-a2e6-b1a619af658c" /><field column="CommentUserID" fieldcaption="CommentUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c9d0fc02-d016-4f5a-870c-5a607c338a6b" /><field column="CommentUrl" fieldcaption="CommentUrl" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="d828d315-4a6d-4919-80ab-f080ac4507bb" /><field column="CommentText" fieldcaption="CommentText" visible="true" columntype="longtext" fieldtype="textarea" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1189c1f2-8391-4dc0-8019-05cfa2bdd97e" /><field column="CommentEmail" fieldcaption="E-mail" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="250" publicfield="false" spellcheck="true" guid="973875a2-3429-4562-a631-eb41f6baa8d1" /><field column="CommentApprovedByUserID" fieldcaption="CommentApprovedByUserID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f275bdd1-6b44-489e-87d8-f362f6fc9583" /><field column="CommentPostDocumentID" fieldcaption="CommentPostDocumentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fac813e1-53c1-4ded-8f1e-11940b812653" /><field column="CommentDate" fieldcaption="CommentDate" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="5b67362f-b86f-4f6e-bbdd-ad6d7b425600" /><field column="CommentIsSpam" fieldcaption="CommentIsSpam" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b66f6add-2e5f-412c-b386-56b5f677b89b" /><field column="CommentApproved" fieldcaption="CommentApproved" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7f112078-00c8-4747-b218-ca779f0a3fb2" /><field column="CommentIsTrackBack" fieldcaption="CommentIsTrackBack" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2d213593-81e7-4d70-9fff-6623294b09d2" defaultvalue="false" /></form>', N'', N'', N'', N'Blog_Comment', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100303 13:48:22', 'ff247c39-574a-4807-bbdb-e42b512f9898', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'<search><item tokenized="False" name="CommentApproved" content="False" searchable="True" id="dae64ba0-89b3-4cd9-a726-56cbc30f90e8"></item><item tokenized="True" name="CommentEmail" content="True" searchable="True" id="1075417d-2313-4cdd-a057-ccf33164a191"></item><item tokenized="False" name="CommentIsTrackBack" content="False" searchable="True" id="e4062876-5ca1-4c34-a6b7-ca3165464444"></item><item tokenized="False" name="CommentPostDocumentID" content="False" searchable="True" id="9d9ab8c6-5de6-4745-a1b1-f09d45c5c0ba"></item><item tokenized="False" name="CommentDate" content="False" searchable="True" id="50f07302-cf70-49eb-a07b-0d67d43231ad"></item><item tokenized="False" name="CommentApprovedByUserID" content="False" searchable="True" id="490785a7-7059-40ee-9903-5ffb6894f3b6"></item><item tokenized="True" name="CommentUrl" content="True" searchable="True" id="d60a2aeb-cb62-488a-b85e-3696d2818fba"></item><item tokenized="True" name="CommentText" content="True" searchable="True" id="21e30c19-dd1b-4ba8-8cc5-510fdc9f6dfc"></item><item tokenized="False" name="CommentIsSpam" content="False" searchable="True" id="96ff6705-0ea6-4dba-8ea8-d02d5713c489"></item><item tokenized="True" name="CommentUserName" content="True" searchable="True" id="7f2528b2-9109-4d45-9d89-e165edadb225"></item><item tokenized="False" name="CommentID" content="False" searchable="True" id="549a65eb-3852-4b89-b003-c3ad117187fa"></item><item tokenized="False" name="CommentUserID" content="False" searchable="True" id="30c8c336-1cd0-4454-8f4e-7389c71195b7"></item></search>', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1366, N'Analytics statistics', N'analytics.statistics', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Analytics_Statistics">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="StatisticsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="StatisticsSiteID" type="xs:int" minOccurs="0" />
              <xs:element name="StatisticsCode">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="400" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatisticsObjectName" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="450" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="StatisticsObjectID" type="xs:int" minOccurs="0" />
              <xs:element name="StatisticsObjectCulture" minOccurs="0">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="10" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Analytics_Statistics" />
      <xs:field xpath="StatisticsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="StatisticsID" fieldcaption="StatisticsID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="304c2ad5-41fd-4954-920b-1b8d9876e48a" ismacro="false" /><field column="StatisticsSiteID" fieldcaption="StatisticsSiteID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="43c6424f-8930-4abb-82c7-139aff98871d" ismacro="false" /><field column="StatisticsCode" fieldcaption="StatisticsCode" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="400" publicfield="false" spellcheck="true" guid="b58d2acd-05c6-48d3-a19e-8b720bcf18b1" visibility="none" ismacro="false" /><field column="StatisticsObjectName" fieldcaption="StatisticsObjectName" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="03afa08a-bfdf-41de-ac95-03da490da18b" ismacro="false" /><field column="StatisticsObjectID" fieldcaption="StatisticsObjectID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="effde454-0f22-4a16-9a9c-4510b8c9fe16" ismacro="false" /><field column="StatisticsObjectCulture" fieldcaption="StatisticsObjectCulture" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f9ab8359-7261-47c6-b368-21d3c3439b71" ismacro="false" /></form>', N'', N'', N'', N'Analytics_Statistics', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20120207 11:20:16', '75b95c91-bc3e-45a7-b40d-27581d8b67bc', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, 0, N'PRODUCT')
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1367, N'Analytics hour hits', N'analytics.hitshour', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Analytics_HourHits">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="HitsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="HitsStatisticsID" type="xs:int" />
              <xs:element name="HitsStartTime" type="xs:dateTime" />
              <xs:element name="HitsEndTime" type="xs:dateTime" />
              <xs:element name="HitsCount" type="xs:int" />
              <xs:element name="HitsValue" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Analytics_HourHits" />
      <xs:field xpath="HitsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="HitsID" fieldcaption="HitsID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="2b4c7f1f-b65e-49e3-b9e3-1b5f6cec367a" ismacro="false" /><field column="HitsStatisticsID" fieldcaption="HitsStatisticsID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dab8b16a-40a5-46a1-b9b6-ef48a36b665f" ismacro="false" /><field column="HitsStartTime" fieldcaption="HitsStartTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="01b9f2d8-7c62-42e3-9137-4c65fc20dcf5" ismacro="false" /><field column="HitsEndTime" fieldcaption="HitsEndTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4767e166-32e9-4630-afcc-78711f95960a" ismacro="false" /><field column="HitsCount" fieldcaption="HitsCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7500e99c-be1c-4ef3-8626-4dd97c1f9c08" ismacro="false" /><field column="HitsValue" fieldcaption="Hits value" visible="true" columntype="double" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="84340044-f625-499b-91b1-4284cdf0cfd0" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Analytics_HourHits', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100831 12:58:09', 'b97a7c1e-5193-4388-938b-a212e8f2c28a', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1368, N'Analytics day hits', N'analytics.hitsday', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Analytics_DayHits">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="HitsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="HitsStatisticsID" type="xs:int" />
              <xs:element name="HitsStartTime" type="xs:dateTime" />
              <xs:element name="HitsEndTime" type="xs:dateTime" />
              <xs:element name="HitsCount" type="xs:int" />
              <xs:element name="HitsValue" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Analytics_DayHits" />
      <xs:field xpath="HitsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="HitsID" fieldcaption="HitsID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="2b1d184c-b95c-407e-b253-cd81d0a1a5a9" ismacro="false" /><field column="HitsStatisticsID" fieldcaption="HitsStatisticsID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4301af00-35b2-4f73-90c2-dfc987809658" ismacro="false" /><field column="HitsStartTime" fieldcaption="HitsStartTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dc4a60fc-3b3c-44a5-b42e-50e14f3da88a" ismacro="false" /><field column="HitsEndTime" fieldcaption="HitsEndTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="89b49a8b-e2ff-4c3a-a1f4-eff3f623aaa7" ismacro="false" /><field column="HitsCount" fieldcaption="HitsCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="41d7b63a-80d2-45ff-b9bf-0fb3c173980e" ismacro="false" /><field column="HitsValue" fieldcaption="Hits value" visible="true" columntype="double" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a692f94d-f623-40ec-9cd5-552e558995b2" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Analytics_DayHits', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100831 12:57:38', '97040785-3faf-4d17-9b77-7436f7b10844', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1369, N'Analytics month hits', N'analytics.hitsmonth', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Analytics_MonthHits">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="HitsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="HitsStatisticsID" type="xs:int" />
              <xs:element name="HitsStartTime" type="xs:dateTime" />
              <xs:element name="HitsEndTime" type="xs:dateTime" />
              <xs:element name="HitsCount" type="xs:int" />
              <xs:element name="HitsValue" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Analytics_MonthHits" />
      <xs:field xpath="HitsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="HitsID" fieldcaption="HitsID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="d191d9b3-52ef-43c1-ae3d-7779125897a0" ismacro="false" /><field column="HitsStatisticsID" fieldcaption="HitsStatisticsID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9ad191da-6058-4916-a7d4-1b70b0f6b66d" ismacro="false" /><field column="HitsStartTime" fieldcaption="HitsStartTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="290752d5-924a-441f-8450-f057d653ac70" ismacro="false" /><field column="HitsEndTime" fieldcaption="HitsEndTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6c89aad0-8284-4ec7-96e7-d4ee954a5de3" ismacro="false" /><field column="HitsCount" fieldcaption="HitsCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b28a32d6-ac63-4e3f-9747-1061714809ec" ismacro="false" /><field column="HitsValue" fieldcaption="Hits value" visible="true" columntype="double" fieldtype="label" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="00331abd-6a14-4178-9f25-d6b446578051" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Analytics_MonthHits', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100831 12:59:38', '7528e30b-8884-4e8f-8a49-085f8069ffea', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1370, N'Analytics week hits', N'analytics.hitsweek', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Analytics_WeekHits">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="HitsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="HitsStatisticsID" type="xs:int" />
              <xs:element name="HitsStartTime" type="xs:dateTime" />
              <xs:element name="HitsEndTime" type="xs:dateTime" />
              <xs:element name="HitsCount" type="xs:int" />
              <xs:element name="HitsValue" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Analytics_WeekHits" />
      <xs:field xpath="HitsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="HitsID" fieldcaption="HitsID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="a2831edb-3d81-4a7e-82ca-bcb63d676364" ismacro="false" /><field column="HitsStatisticsID" fieldcaption="HitsStatisticsID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="f10ff8a2-e7d7-4713-bfc4-bbb7459b89f0" ismacro="false" /><field column="HitsStartTime" fieldcaption="HitsStartTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="816e8b8b-f28e-4152-860f-2141cdf6a88b" ismacro="false" /><field column="HitsEndTime" fieldcaption="HitsEndTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="de5eb681-92ea-49cf-939a-615b856c7dae" ismacro="false" /><field column="HitsCount" fieldcaption="HitsCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="25dc690e-9106-44d5-b81b-bab6e271cf50" ismacro="false" /><field column="HitsValue" fieldcaption="Hits value" visible="true" columntype="double" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="ecfaff0d-0c7f-4b68-b568-2b22209d0bf1" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Analytics_WeekHits', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100831 13:01:51', '32eb47b8-2e30-4094-8c20-be3b355b31d1', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1371, N'Analytics year hits', N'analytics.hitsyear', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Analytics_YearHits">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="HitsID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="HitsStatisticsID" type="xs:int" />
              <xs:element name="HitsStartTime" type="xs:dateTime" />
              <xs:element name="HitsEndTime" type="xs:dateTime" />
              <xs:element name="HitsCount" type="xs:int" />
              <xs:element name="HitsValue" type="xs:double" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Analytics_YearHits" />
      <xs:field xpath="HitsID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="HitsID" fieldcaption="HitsID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e424b08c-9d5c-4fde-98f9-fa5df680ba40" ismacro="false" /><field column="HitsStatisticsID" fieldcaption="HitsStatisticsID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="12a87691-0b02-4676-93c6-6949fbf5a7dd" ismacro="false" /><field column="HitsStartTime" fieldcaption="HitsStartTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4360ade8-e25f-4466-91d5-5da81683e6e3" ismacro="false" /><field column="HitsEndTime" fieldcaption="HitsEndTime" visible="true" columntype="datetime" fieldtype="calendar" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69ebdbff-6467-465a-a329-5b1ee5ccdda4" ismacro="false" /><field column="HitsCount" fieldcaption="HitsCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1c3d328b-4ac0-41b5-968c-f54fa796cf5d" ismacro="false" /><field column="HitsValue" fieldcaption="Hits value" visible="true" columntype="double" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="6e5f74de-c9c2-453e-a0a7-64294340b059" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Analytics_YearHits', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20100831 13:03:29', '3f74ab7f-4d2e-40cd-8a94-78ba0a84f366', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1424, N'Ecommerce - Volume discount', N'ecommerce.volumediscount', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_VolumeDiscount">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="VolumeDiscountID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="VolumeDiscountSKUID" type="xs:int" />
              <xs:element name="VolumeDiscountMinCount" type="xs:int" />
              <xs:element name="VolumeDiscountValue" type="xs:double" />
              <xs:element name="VolumeDiscountIsFlatValue" type="xs:boolean" />
              <xs:element name="VolumeDiscountGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" minOccurs="0" />
              <xs:element name="VolumeDiscountLastModified" type="xs:dateTime" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_VolumeDiscount" />
      <xs:field xpath="VolumeDiscountID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="VolumeDiscountID" fieldcaption="VolumeDiscountID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="0c00f4b8-c620-47c9-b210-560ccad5de07" /><field column="VolumeDiscountSKUID" fieldcaption="VolumeDiscountSKUID" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="40cf73c1-36e7-4ba1-a430-99edc9882288" /><field column="VolumeDiscountMinCount" fieldcaption="VolumeDiscountMinCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="fec331ea-76d5-4b7b-9d07-712a73f010df" /><field column="VolumeDiscountValue" fieldcaption="VolumeDiscountValue" visible="true" columntype="double" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="85872dc6-b413-4a5c-b534-193c93175e94" /><field column="VolumeDiscountIsFlatValue" fieldcaption="VolumeDiscountIsFlatValue" visible="true" columntype="boolean" fieldtype="checkbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="87ded51f-d9b2-4650-88a4-53c3e09c4735" /><field column="VolumeDiscountGUID" fieldcaption="VolumeDiscountGUID" visible="true" columntype="file" fieldtype="uploadfile" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="51cb9858-7105-4c5e-8f23-58b9299b37de" /><field column="VolumeDiscountLastModified" fieldcaption="VolumeDiscountLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1709e8fd-993e-44a3-9e74-eab513d45a78" /></form>', N'', N'', N'', N'COM_VolumeDiscount', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110405 17:33:09', 'c363a8ef-9b91-4e8b-860f-1e058dfc66ec', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1429, N'Ecommerce - Discount level', N'ecommerce.discountlevel', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_DiscountLevel">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DiscountLevelID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="DiscountLevelDisplayName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DiscountLevelName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="100" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="DiscountLevelValue" type="xs:double" />
              <xs:element name="DiscountLevelEnabled" type="xs:boolean" />
              <xs:element name="DiscountLevelValidFrom" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DiscountLevelValidTo" type="xs:dateTime" minOccurs="0" />
              <xs:element name="DiscountLevelGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="DiscountLevelLastModified" type="xs:dateTime" />
              <xs:element name="DiscountLevelSiteID" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_DiscountLevel" />
      <xs:field xpath="DiscountLevelID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="DiscountLevelID" fieldcaption="DiscountLevelID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="7ad32772-5c73-4ff7-ba95-1ffb0166a2d3" ismacro="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="DiscountLevelDisplayName" fieldcaption="DiscountLevelDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="83d2a289-1cf3-4d43-9b15-869f31f647f1" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DiscountLevelName" fieldcaption="DiscountLevelName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="564457b5-62b5-4e2d-a34f-3b3f96a2d601" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DiscountLevelValue" fieldcaption="DiscountLevelValue" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1882af8-6ca2-411e-b3f4-c0ed83448af8" ismacro="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="DiscountLevelEnabled" fieldcaption="DiscountLevelEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="7d41d785-b1b3-4faa-83b9-8d5be49569f1" ismacro="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="DiscountLevelValidFrom" fieldcaption="DiscountLevelValidFrom" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bc2fca5f-aa50-43be-8306-f172d826ad86" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DiscountLevelValidTo" fieldcaption="DiscountLevelValidTo" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="69a5c64e-e5f1-4a2d-85da-58a83eaaa9b9" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DiscountLevelGUID" fieldcaption="DiscountLevelGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d0e040df-1faa-4e45-b7dc-183b5f3e742a" ismacro="false"><settings><controlname>unknown</controlname></settings></field><field column="DiscountLevelLastModified" fieldcaption="DiscountLevelLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a54ed583-3013-484d-b2c6-768c2d6f3ea0" ismacro="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="DiscountLevelSiteID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="false" guid="0e5d9fc7-4197-416f-b487-9b435ce9ec5b" visibility="none" ismacro="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_DiscountLevel', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110404 11:23:03', 'bffc1dfe-82c4-4612-a3dc-e7a40cb26985', 0, 1, 0, N'', 2, N'DiscountLevelDisplayName', N'0', N'', N'DiscountLevelLastModified', N'<search><item searchable="True" name="DiscountLevelID" tokenized="False" content="False" id="ffcad1ea-97b4-484f-a019-201d00f67dcd" /><item searchable="False" name="DiscountLevelDisplayName" tokenized="True" content="True" id="ec24b287-afb9-46fc-8310-13ac743e730e" /><item searchable="False" name="DiscountLevelName" tokenized="True" content="True" id="358658f4-6ae5-4456-b4a4-13d16d6c132a" /><item searchable="True" name="DiscountLevelValue" tokenized="False" content="False" id="8f16cfbb-ed78-46eb-9949-44f1287efa92" /><item searchable="True" name="DiscountLevelEnabled" tokenized="False" content="False" id="83e8e9f6-6797-4bfd-819f-bee6f8377774" /><item searchable="True" name="DiscountLevelValidFrom" tokenized="False" content="False" id="0f4bcf6c-671d-4a61-a12f-b439fd9e85a7" /><item searchable="True" name="DiscountLevelValidTo" tokenized="False" content="False" id="5a5ac991-18e2-489e-b27e-c786731d6aff" /><item searchable="False" name="DiscountLevelGUID" tokenized="False" content="False" id="cbbb25df-4aad-40f2-9aac-a65f4ab532c5" /><item searchable="True" name="DiscountLevelLastModified" tokenized="False" content="False" id="bad8f366-8ebc-4480-bc22-f073314c7393" /><item searchable="True" name="DiscountLevelSiteID" tokenized="False" content="False" id="c5e5d185-51df-4009-8a26-4b413f00676c" /></search>', NULL, 0, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1431, N'Report category', N'Reporting.ReportCategory', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="Reporting_ReportCategory">
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
              <xs:element name="CategoryCodeName">
                <xs:simpleType>
                  <xs:restriction base="xs:string">
                    <xs:maxLength value="200" />
                  </xs:restriction>
                </xs:simpleType>
              </xs:element>
              <xs:element name="CategoryGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />
              <xs:element name="CategoryLastModified" type="xs:dateTime" />
              <xs:element name="CategoryParentID" type="xs:int" minOccurs="0" />
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
              <xs:element name="CategoryReportChildCount" type="xs:int" minOccurs="0" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//Reporting_ReportCategory" />
      <xs:field xpath="CategoryID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="CategoryID" fieldcaption="CategoryID" visible="true" columntype="integer" fieldtype="label" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="2d4af13c-4087-468e-8971-5e63c038e867" ismacro="false" /><field column="CategoryDisplayName" fieldcaption="CategoryDisplayName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="e0fcb4bf-d573-4e0e-8eb5-9a47e810b13a" ismacro="false" /><field column="CategoryCodeName" fieldcaption="CategoryCodeName" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="dc0ecf7d-0953-447f-a8eb-8876d759d0e5" ismacro="false" /><field column="CategoryGUID" fieldcaption="CategoryGUID" visible="true" columntype="file" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c843471e-1877-4fcb-ba2c-ee55c3413248" ismacro="false" /><field column="CategoryLastModified" fieldcaption="CategoryLastModified" visible="true" columntype="datetime" fieldtype="calendar" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="b917a7d8-8d58-4dbf-83f1-9f5e32eed1a2" ismacro="false" /><field column="CategoryParentID" fieldcaption="CategoryParentID" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="af475f5f-6dd0-4800-bad6-cf0fa1d9f52f" visibility="none" ismacro="false" /><field column="CategoryImagePath" fieldcaption="CategoryImagePath" visible="true" columntype="text" fieldtype="textbox" allowempty="true" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="821fec6d-6c4b-4840-b53c-95fc2108b721" visibility="none" ismacro="false" /><field column="CategoryPath" fieldcaption="CategoryPath" visible="true" columntype="text" fieldtype="textbox" allowempty="false" isPK="false" system="true" columnsize="450" publicfield="false" spellcheck="true" guid="46928e89-1623-46f3-8b34-a349b01fde68" visibility="none" ismacro="false" /><field column="CategoryOrder" fieldcaption="CategoryOrder" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4d861f94-1de7-44c6-a319-b103265d5685" visibility="none" ismacro="false" /><field column="CategoryLevel" fieldcaption="CategoryLevel" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="d33fee17-cba7-4bd6-a0f7-35d607d17389" visibility="none" ismacro="false" /><field column="CategoryChildCount" fieldcaption="CategoryChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c50b873d-5585-434d-8ba1-7ae79273ef9d" visibility="none" ismacro="false" /><field column="CategoryReportChildCount" fieldcaption="CategoryReportChildCount" visible="true" columntype="integer" fieldtype="textbox" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4ba1c430-7d9a-4bc0-85e7-83991a760a64" visibility="none" ismacro="false" /></form>', N'', N'', N'', N'Reporting_ReportCategory', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110531 17:09:34', 'f2763c6c-d3d8-4e39-89f9-8f71c21f9368', 0, 1, 0, N'', 2, N'CategoryDisplayName', N'0', N'CategoryImagePath', N'CategoryLastModified', N'<search><item searchable="True" name="CategoryID" tokenized="False" content="False" id="3e5b7802-1ad2-4157-9723-34d022b4bce1" /><item searchable="False" name="CategoryDisplayName" tokenized="True" content="True" id="bbfb331e-5952-4111-9cfd-2acc4c679817" /><item searchable="False" name="CategoryCodeName" tokenized="True" content="True" id="df23e4aa-2980-4e7a-a33b-7e10bf698834" /><item searchable="False" name="CategoryGUID" tokenized="False" content="False" id="c009ddfb-a65f-4ed5-bd34-96e74e484f7a" /><item searchable="True" name="CategoryLastModified" tokenized="False" content="False" id="aa58f112-2d77-4e6c-acee-a8a2e981bb93" /><item searchable="True" name="CategoryParentID" tokenized="False" content="False" id="6827cd62-f0b9-4a8d-a69b-6df82f822285" /><item searchable="False" name="CategoryImagePath" tokenized="True" content="True" id="1f271a4e-62b7-400f-b1d5-10459e9ff419" /><item searchable="False" name="CategoryPath" tokenized="True" content="True" id="7c8fc550-e894-4a2a-b023-a3b2f8d3d2c4" /><item searchable="True" name="CategoryOrder" tokenized="False" content="False" id="c79f1147-e93f-4380-a5ea-aa4c53e772ac" /><item searchable="True" name="CategoryLevel" tokenized="False" content="False" id="d91d6684-2212-41e6-9b20-fb03eeb9c87a" /><item searchable="True" name="CategoryChildCount" tokenized="False" content="False" id="3d793cd7-6f3e-4642-97ba-05f952231a61" /><item searchable="True" name="CategoryReportChildCount" tokenized="False" content="False" id="c11df1de-43fe-45d2-8ed0-e3494dcb33d8" /></search>', NULL, 0, N'', NULL, NULL, NULL, NULL)
INSERT INTO [CMS_Class] ([ClassID], [ClassDisplayName], [ClassName], [ClassUsesVersioning], [ClassIsDocumentType], [ClassIsCoupledClass], [ClassXmlSchema], [ClassFormDefinition], [ClassEditingPageUrl], [ClassListPageUrl], [ClassNodeNameSource], [ClassTableName], [ClassViewPageUrl], [ClassPreviewPageUrl], [ClassFormLayout], [ClassNewPageUrl], [ClassShowAsSystemTable], [ClassUsePublishFromTo], [ClassShowTemplateSelection], [ClassSKUMappings], [ClassIsMenuItemType], [ClassNodeAliasSource], [ClassDefaultPageTemplateID], [ClassLastModified], [ClassGUID], [ClassCreateSKU], [ClassIsProduct], [ClassIsCustomTable], [ClassShowColumns], [ClassLoadGeneration], [ClassSearchTitleColumn], [ClassSearchContentColumn], [ClassSearchImageColumn], [ClassSearchCreationDateColumn], [ClassSearchSettings], [ClassInheritsFromClassID], [ClassSearchEnabled], [ClassSKUDefaultDepartmentName], [ClassSKUDefaultDepartmentID], [ClassContactMapping], [ClassContactOverwriteEnabled], [ClassSKUDefaultProductType]) VALUES (1448, N'Ecommerce - Tax class state', N'ecommerce.taxclassstate', 0, 0, 1, N'<?xml version="1.0" encoding="utf-8"?>
<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
  <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
    <xs:complexType>
      <xs:choice minOccurs="0" maxOccurs="unbounded">
        <xs:element name="COM_TaxClassState">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="TaxClassStateID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />
              <xs:element name="TaxClassID" type="xs:int" />
              <xs:element name="StateID" type="xs:int" />
              <xs:element name="TaxValue" type="xs:double" />
              <xs:element name="IsFlatValue" type="xs:boolean" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
    <xs:unique name="Constraint1" msdata:PrimaryKey="true">
      <xs:selector xpath=".//COM_TaxClassState" />
      <xs:field xpath="TaxClassStateID" />
    </xs:unique>
  </xs:element>
</xs:schema>', N'<form><field column="TaxClassStateID" visible="false" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="58115b3b-9197-4132-a412-c8410cb6d453" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field><field column="TaxClassID" fieldcaption="TaxClassID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c3503fa7-38e1-4450-b245-cece59d8b765" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="StateID" fieldcaption="StateID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="c3ac6edb-e895-4f76-8469-68a3b02d2e37" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="TaxValue" fieldcaption="TaxValue" visible="true" columntype="double" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="3f9b4b7c-a74c-4a29-acff-f5e4359b5206" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="IsFlatValue" fieldcaption="IsFlatValue" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="1318a545-531e-4d9a-b121-ca3f4867895f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false"><settings><controlname>checkboxcontrol</controlname></settings></field></form>', N'', N'', N'', N'COM_TaxClassState', N'', N'', N'', N'', 0, 0, 0, N'', 0, N'', NULL, '20110428 13:36:02', '25c47e7c-7eb0-4edb-8509-960cddbda2e1', 0, 1, 0, N'', 2, N'', N'', N'', N'', N'', NULL, 0, N'', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [CMS_Class] OFF
