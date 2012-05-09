using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.FormControls;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactid")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Customers : CMSContactManagementContactsPage
{
    #region "Variables"

    private int contactId = 0;
    private bool globalContact = false;
    private bool mergedContact = false;
    private bool modifyAllowed = false;
    private ContactInfo ci = null;

    FormEngineUserControl ucSelectCustomer = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        ci = (ContactInfo)EditedObject;
        this.CheckReadPermission(ci.ContactSiteID);

        globalContact = (ci.ContactSiteID <= 0);
        mergedContact = (ci.ContactMergedWithContactID > 0);
        modifyAllowed = ContactHelper.AuthorizedModifyContact(ci.ContactSiteID, false);

        contactId = QueryHelper.GetInteger("contactid", 0);

        string where = null;
        // Filter only site members in CMSDesk (for global contacts)
        if (!this.IsSiteManager && globalContact && AuthorizedForSiteContacts)
        {
            where += " (ContactSiteID IS NULL OR ContactSiteID=" + CMSContext.CurrentSiteID + ")";
        }

        fltElem.ShowContactNameFilter = globalContact;
        fltElem.ShowSiteFilter = this.IsSiteManager && globalContact;

        // Choose correct object ("query") according to type of contact
        if (globalContact)
        {
            gridElem.ObjectType = OnlineMarketingObjectType.MEMBERSHIPGLOBALCUSTOMERLIST;
        }
        else if (mergedContact)
        {
            gridElem.ObjectType = OnlineMarketingObjectType.MEMBERSHIPMERGEDCUSTOMERLIST;
        }
        else
        {
            gridElem.ObjectType = OnlineMarketingObjectType.MEMBERSHIPCUSTOMERLIST;
        }

        // Query parameters
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@ContactId", contactId);

        where = SqlHelperClass.AddWhereCondition(where, fltElem.WhereCondition);
        gridElem.WhereCondition = where;
        gridElem.QueryParameters = parameters;
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);

        // Hide header actions for global contact
        this.CurrentMaster.HeaderActionsPlaceHolder.Visible = modifyAllowed && !globalContact && !mergedContact;

        // Setup customer selector
        try
        {
            ucSelectCustomer = (FormEngineUserControl)this.Page.LoadControl("~/CMSModules/Ecommerce/FormControls/CustomerSelector.ascx");
            ucSelectCustomer.SetValue("Mode", "OnlineMarketing");
            ucSelectCustomer.SetValue("SiteID", ci.ContactSiteID);
            ucSelectCustomer.Changed += new EventHandler(ucSelectCustomer_Changed);
            ucSelectCustomer.IsLiveSite = false;
            pnlSelectCustomer.Controls.Clear();
            pnlSelectCustomer.Controls.Add(ucSelectCustomer);
        }
        catch
        {
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        gridElem.NamedColumns["sitename"].Visible = globalContact;

        // Display contact full name if some merged contacts point to this contact or if required
        bool showFullName = globalContact;
        if (!showFullName)
        {
            object dataSrc = gridElem.GridView.DataSource;
            if (!DataHelper.DataSourceIsEmpty(dataSrc))
            {
                DataRow[] dr = null;
                if (dataSrc is DataSet)
                {
                    DataSet ds = (DataSet)dataSrc;
                    dr = ds.Tables[0].Select("ContactMergedWithContactID > 0");
                }
                if (dataSrc is DataView)
                {
                    DataView dv = ((DataView)dataSrc);
                    dr = dv.Table.Select("ContactMergedWithContactID > 0");
                }
                showFullName = (dr != null) && (dr.Length > 0);
            }
        }
        gridElem.NamedColumns["contactname"].Visible = showFullName;
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "delete":
                if (globalContact || mergedContact || !modifyAllowed)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png"));
                    button.Enabled = false;
                }
                break;
        }

        return parameter;
    }


    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "delete":
                int membershipId = ValidationHelper.GetInteger(actionArgument, 0);
                if (membershipId > 0)
                {
                    // Check permissions
                    if (ContactHelper.AuthorizedModifyContact(ci.ContactSiteID, true))
                    {
                        MembershipInfoProvider.DeleteRelationship(membershipId);
                    }
                }
                break;
        }
    }


    void ucSelectCustomer_Changed(object sender, EventArgs e)
    {
        // Check permissions
        if (ContactHelper.AuthorizedModifyContact(ci.ContactSiteID, true))
        {
            // Load value form dynamic control
            string values = null;
            if (ucSelectCustomer != null)
            {
                values = ValidationHelper.GetString(ucSelectCustomer.GetValue("OnlineMarketingValue"), null);
            }

            if (!String.IsNullOrEmpty(values))
            {
                // Store users one by one
                string[] customerIds = values.Split(';');
                foreach (string customerId in customerIds)
                {
                    // Check if user ID is valid
                    int customerIdInt = ValidationHelper.GetInteger(customerId, 0);
                    if (customerIdInt <= 0)
                    {
                        continue;
                    }
                    // Add new relation
                    int parentId = (ci.ContactMergedWithContactID == 0) ? ci.ContactID : ci.ContactMergedWithContactID;
                    MembershipInfoProvider.SetRelationship(customerIdInt, MemberTypeEnum.EcommerceCustomer, ci.ContactID, parentId, true);
                    ci = ContactInfoProvider.GetContactInfo(contactId);
                }

                // When contact was merged then refresh complete page
                if ((ci != null) && (ci.ContactMergedWithContactID > 0))
                {
                    Page.Response.Redirect(URLHelper.Url.ToString(), true);
                }
                else
                {
                    gridElem.ReloadData();
                }
            }
        }
    }

    #endregion
}
