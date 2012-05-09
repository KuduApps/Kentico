using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_InvoiceTemplate_InvoiceTemplate_Edit : CMSEcommerceSharedConfigurationPage
{
    #region "Variables"

    private SiteInfo mConfiguredSiteInfo = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// SiteInfo object of currently configured site.
    /// </summary>
    private SiteInfo ConfiguredSiteInfo
    {
        get
        {
            if (mConfiguredSiteInfo == null)
            {
                mConfiguredSiteInfo = SiteInfoProvider.GetSiteInfo(ConfiguredSiteID);
            }

            return mConfiguredSiteInfo;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        GlobalObjectsKeyName = ECommerceSettings.USE_GLOBAL_INVOICE;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.Invoice"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.Invoice");
        }

        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("InvoiceTemplate.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Ecommerce/invoice.png");
        this.CurrentMaster.Title.HelpTopicName = "invoice";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[2, 9];
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("general.save");
        actions[0, 2] = null;
        actions[0, 3] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "lnksave_click";
        actions[0, 7] = null;
        actions[0, 8] = "true";

        // Show "Copy from global" link when not configuring global invoice.
        if (ConfiguredSiteID != 0)
        {
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("com.InvoiceFromGlobal");
            actions[1, 2] = "return ConfirmCopyFromGlobal();";
            actions[1, 3] = null;
            actions[1, 4] = null;
            actions[1, 5] = GetImageUrl("CMSModules/CMS_Ecommerce/invoicefromglobal.png");
            actions[1, 6] = "copyFromGlobal";
            actions[1, 7] = String.Empty;
            actions[1, 8] = true.ToString();

            // Register javascript to confirm generate 
            string script = "function ConfirmCopyFromGlobal() {return confirm(" + ScriptHelper.GetString(GetString("com.ConfirmInvoiceFromGlobal")) + ");}";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmCopyFromGlobal", ScriptHelper.GetScript(script));
        }

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);

        lblInsertMacro.Text = GetString("macroselector.insertmacro") + ":";
        AttachmentTitle.TitleText = GetString("general.attachments");

        macroSelectorElm.Resolver = EmailTemplateMacros.EcommerceResolver;
        macroSelectorElm.CKEditorID = htmlInvoiceTemplate.ClientID;

        // Init attachment storage
        if (ConfiguredSiteInfo != null)
        {
            AttachmentList.ObjectID = ConfiguredSiteID;
            AttachmentList.ObjectType = SiteObjectType.SITE;
            AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_INVOICE;
            AttachmentList.SiteID = ConfiguredSiteID;

            // Permissions
            AttachmentList.AllowEdit = ECommerceContext.IsUserAuthorizedForPermission("ConfigurationModify");
            AttachmentList.CheckObjectPermissions = false;
        }
        else
        {
            plcAttachments.Visible = false;
        }

        DisplayHelperTable();

        if (!RequestHelper.IsPostBack())
        {
            htmlInvoiceTemplate.ResolvedValue = "";
            // Configuring global invoice
            if (ConfiguredSiteID == 0)
            {
                // Show global invoice
                htmlInvoiceTemplate.ResolvedValue = ECommerceSettings.InvoiceTemplate(null);
            }
            else
            {
                // Show site-specific invoice
                if (ConfiguredSiteInfo != null)
                {
                    htmlInvoiceTemplate.ResolvedValue = ECommerceSettings.InvoiceTemplate(ConfiguredSiteInfo.SiteName);
                }
            }

            InitHTMLEditor();
        }

        // Show "using global settings" info message only if showing global store settings
        if ((ConfiguredSiteID == 0) && (SiteID != 0))
        {
            lblGlobalInfo.Visible = true;
            lblGlobalInfo.Text = GetString("com.UsingGlobalInvoice");
        }
    }


    /// <summary>
    /// Save button action.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "lnksave_click":
                // Check permissions
                CheckConfigurationModification();

                // Check if template doesn't contains more editable regions with same name
                Hashtable eRegions = new Hashtable();

                int count = 0;
                int textStart = 0;
                int editRegStart = htmlInvoiceTemplate.ResolvedValue.Trim().IndexOf("$$", textStart);

                while (editRegStart >= 0)
                {
                    count++;

                    // End of region
                    editRegStart += 2;
                    textStart = editRegStart;
                    if (editRegStart < htmlInvoiceTemplate.ResolvedValue.Trim().Length - 1)
                    {
                        int editRegEnd = htmlInvoiceTemplate.ResolvedValue.Trim().IndexOf("$$", editRegStart);
                        if (editRegEnd >= 0)
                        {
                            string region = htmlInvoiceTemplate.ResolvedValue.Trim().Substring(editRegStart, editRegEnd - editRegStart);
                            string[] parts = (region + ":" + ":").Split(':');

                            textStart = editRegEnd + 2;
                            try
                            {
                                string name = parts[0];
                                if (name.Trim() != "")
                                {
                                    if (eRegions[name.ToLower()] != null)
                                    {
                                        break;
                                    }

                                    if (!ValidationHelper.IsCodeName(name))
                                    {
                                        break;
                                    }
                                    eRegions[name.ToLower()] = 1;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    editRegStart = htmlInvoiceTemplate.ResolvedValue.Trim().IndexOf("$$", textStart);
                }

                if (IsValid)
                {
                    if (ConfiguredSiteInfo != null)
                    {
                        SettingsKeyProvider.SetValue(ConfiguredSiteInfo.SiteName + "." + ECommerceSettings.INVOICE_TEMPLATE, htmlInvoiceTemplate.ResolvedValue.Trim());
                    }
                    else
                    {
                        SettingsKeyProvider.SetValue(ECommerceSettings.INVOICE_TEMPLATE, htmlInvoiceTemplate.ResolvedValue.Trim());
                    }
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
                break;

            case "copyfromglobal":
                // Read global invoice
                htmlInvoiceTemplate.ResolvedValue = ECommerceSettings.InvoiceTemplate(null);

                break;
        }
    }


    /// <summary>
    /// Initializes HTML editor's settings.
    /// </summary>
    protected void InitHTMLEditor()
    {
        htmlInvoiceTemplate.AutoDetectLanguage = false;
        htmlInvoiceTemplate.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlInvoiceTemplate.MediaDialogConfig.UseFullURL = true;
        htmlInvoiceTemplate.LinkDialogConfig.UseFullURL = true;
        htmlInvoiceTemplate.QuickInsertConfig.UseFullURL = true;
    }


    protected void FillHelperTable(Table tblHelp, string[,] tableColumns)
    {
        for (int i = 0; i < tableColumns.GetUpperBound(0) + 1; i++)
        {
            TableRow tRow = new TableRow();
            TableCell leftCell = new TableCell();
            leftCell.Wrap = false;

            TableCell rightCell = new TableCell();

            Label lblExample = new Label();
            Label lblExplanation = new Label();

            // init labels
            lblExample.Text = tableColumns[i, 0];
            lblExplanation.Text = tableColumns[i, 1];

            // add labels to the cells
            leftCell.Controls.Add(lblExample);
            rightCell.Controls.Add(lblExplanation);

            leftCell.Width = new Unit(250);

            // add cells to the row
            tRow.Cells.Add(leftCell);
            tRow.Cells.Add(rightCell);

            // add row to the table
            tblHelp.Rows.Add(tRow);
        }
    }


    /// <summary>
    /// Displays helper table with makro examples.
    /// </summary>
    protected void DisplayHelperTable()
    {
        string[,] tableColumns = new string[28, 2];
        int i = 0;

        tableColumns[i, 0] = "{%ShoppingCart.ShoppingCartCurrencyID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShoppingCart");

        tableColumns[i, 0] = "{%Order.OrderID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ColumnName");

        tableColumns[i, 0] = "{%Order.OrderInvoiceNumber%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_INVOICENUMBER");

        tableColumns[i, 0] = "{%Order.OrderNote|(encode)%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ORDERNOTE");

        tableColumns[i, 0] = "{%OrderStatus.StatusID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_OrderStatus");

        tableColumns[i, 0] = "{%BillingAddress.AddressID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_BillingAddressColumn");

        tableColumns[i, 0] = "{%BillingAddress.Country.CountryID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_BillingAddressCountry");

        tableColumns[i, 0] = "{%BillingAddress.State.StateID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_BillingAddressState");

        tableColumns[i, 0] = "{%ShippingAddress.AddressID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShippingAddress");

        tableColumns[i, 0] = "{%ShippingAddress.ApplyTransformation()%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShippingAddressTransformation");

        tableColumns[i, 0] = "{%ShippingAddress.Country.CountryID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShippingAddressCountry");

        tableColumns[i, 0] = "{%ShippingAddress.State.StateID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShippingAddressState");

        tableColumns[i, 0] = "{%CompanyAddress.AddressID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_CompanyAddress");

        tableColumns[i, 0] = "{%CompanyAddress.Country.CountryID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_CompanyAddressCountry");

        tableColumns[i, 0] = "{%CompanyAddress.State.StateID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_CompanyAddressState");

        tableColumns[i, 0] = "{%ShippingOption.ShippingOptionID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShippingOptionColumn");

        tableColumns[i, 0] = "{%PaymentOption.PaymentOptionID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_PaymentOptionColumn");

        tableColumns[i, 0] = "{%Currency.CurrencyID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_Currency");

        tableColumns[i, 0] = "{%Customer.CustomerID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_Customer");

        tableColumns[i, 0] = "{%Customer.CustomerOrganizationID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_OrganizationID");

        tableColumns[i, 0] = "{%Customer.CustomerTaxRegistrationID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_TaxregistRationID");

        tableColumns[i, 0] = "{%DiscountCoupon.DiscountCouponID%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_DiscountCoupon");

        tableColumns[i, 0] = "{%ContentTable.ApplyTransformation()%}";
        tableColumns[i++, 1] = GetString("order_edit_invoice.help_productlist");

        tableColumns[i, 0] = "{%ContentTaxesTable.ApplyTransformation()%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_TaxRecapitulation");

        tableColumns[i, 0] = "{%ShippingTaxesTable.ApplyTransformation()%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ShippingTaxRecapitulation");

        tableColumns[i, 0] = "{%TotalPrice.Format(Currency.CurrencyFormatString)%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_PriceFormatting");

        tableColumns[i, 0] = "{%Format(Order.OrderDate, \"{0:d}\")%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_ORDERDATE");

        tableColumns[i, 0] = "{%TotalShipping.Format(Currency.CurrencyFormatString)%}";
        tableColumns[i++, 1] = GetString("Order_Edit_Invoice.Help_TOTALSHIPPING");

        FillHelperTable(tblMore, tableColumns);

        lnkMoreMacros.Text = GetString("Order_Edit_Invoice.MoreMacros");
        lblMoreInfo.Text = GetString("Order_Edit_Invoice.MoreInfo");
    }
}
