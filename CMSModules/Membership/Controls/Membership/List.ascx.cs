using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Membership_Controls_Membership_List : CMSAdminListControl
{
    #region "Variables"

    private int mSiteID = 0;
    protected string mSiteQueryUrl = String.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// Inner grid.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            return this.gridElem;
        }
    }


    /// <summary>
    /// Gets or sets site specific url for item edit.
    /// </summary>
    public string SiteQueryUrl
    {
        get
        {
            return mSiteQueryUrl;
        }
        set
        {
            mSiteQueryUrl = value;
        }
    }


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
    /// SiteID filter - if 0 global membership is used.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;
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

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        gridElem.WhereCondition = (SiteID == 0) ? "MembershipSiteID IS NULL" : "MembershipSiteID =" + SiteID;

        // Grid initialization                
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "EditItem", ScriptHelper.GetScript(
            "function EditMembership(id) { this.window.location = 'Frameset.aspx?membershipId=' + id+ '" + mSiteQueryUrl + "'}"));

    }


    /// <summary>
    /// Handles UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of the action which should be performed</param>
    /// <param name="actionArgument">ID of the item the action should be performed with</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        int membershipId = ValidationHelper.GetInteger(actionArgument, 0);
        if (membershipId > 0)
        {
            switch (actionName.ToLower())
            {
                case "edit":
                    this.SelectedItemID = membershipId;
                    this.RaiseOnEdit();
                    break;

                case "delete":
                    if (CheckPermissions("CMS.Membership", CMSAdminControl.PERMISSION_MODIFY))
                    {
                        // Check dependencies
                        if (MembershipInfoProvider.CheckDependencies(membershipId))
                        {
                            lblError.Visible = true;
                            lblError.Text = GetString("membership.dependencies");
                            return;
                        }

                        // Delete the object
                        MembershipInfoProvider.DeleteMembershipInfo(membershipId);
                        this.RaiseOnDelete();

                        // Reload data
                        gridElem.ReloadData();
                    }
                    break;
            }
        }
    }

    #endregion
}