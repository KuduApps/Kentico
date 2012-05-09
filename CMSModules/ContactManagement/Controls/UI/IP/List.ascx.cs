using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_ContactManagement_Controls_UI_Ip_List : CMSAdminListControl
{
    #region "Properties"

    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }



    /// <summary>
    /// True if site name column is visible.
    /// </summary>
    public bool ShowSiteNameColumn
    {
        get;
        set;
    }


    /// <summary>
    /// True if contact name is visible.
    /// </summary>
    public bool ShowContactNameColumn
    {
        get;
        set;
    }


    /// <summary>
    /// True if remove action button is visible.
    /// </summary>
    public bool ShowRemoveButton
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets contact ID.
    /// </summary>
    public int ContactID
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets value that indicates whether contact is merged.
    /// </summary>
    public bool IsMergedContact
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets value that indicates whether contact is global contact.
    /// </summary>
    public bool IsGlobalContact
    {
        get;
        set;
    }


    /// <summary>
    /// Additional WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get;
        set;
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@ContactID", ContactID);

        // Choose correct object type ("query") according to contact type
        if (IsGlobalContact)
        {
            gridElem.ObjectType = OnlineMarketingObjectType.IPGLOBALLIST;
        }
        else if (IsMergedContact)
        {
            gridElem.ObjectType = OnlineMarketingObjectType.IPMERGEDLIST;
        }
        else
        {
            gridElem.ObjectType = OnlineMarketingObjectType.IPLIST;
        }
        gridElem.QueryParameters = parameters;
        gridElem.WhereCondition = WhereCondition;
        gridElem.ZeroRowsText = GetString("om.ip.noips");
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        gridElem.NamedColumns["ContactSiteID"].Visible = this.ShowSiteNameColumn;

        // Display contact full name if some merged contacts point to this contact or if required
        bool showFullName = this.ShowContactNameColumn;
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
        gridElem.NamedColumns["ContactFullName"].Visible = showFullName;
    }


    /// <summary>
    /// UniGrid action handler.
    /// </summary>
    void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            ContactInfo contact = ContactInfoProvider.GetContactInfo(this.ContactID);

            // Check permission
            if ((contact != null) && ContactHelper.AuthorizedModifyContact(contact.ContactSiteID, true))
            {
                int ipId = ValidationHelper.GetInteger(actionArgument, 0);
                IPInfoProvider.DeleteIPInfo(ipId);
            }
        }
    }


    /// <summary>
    /// UniGrid databound handler.
    /// </summary>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "delete":
                if (ShowRemoveButton)
                {
                    ((CMSImageButton)sender).ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png");
                    ((CMSImageButton)sender).Enabled = true;
                }
                else
                {
                    ((CMSImageButton)sender).ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png");
                    ((CMSImageButton)sender).Enabled = false;
                }
                break;
        }
        return null;
    }

    #endregion
}