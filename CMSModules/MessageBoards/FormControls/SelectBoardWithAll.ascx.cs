using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.FormControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;


public partial class CMSModules_MessageBoards_FormControls_SelectBoardWithAll : FormEngineUserControl
{
    #region "Variables"

    private bool siteNameIsAll = false;
    private bool siteNameAvailable = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return this.selectBoard.Value;
        }
        set
        {
            EnsureChildControls();
            this.selectBoard.Value = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        SetFormSiteName();
        selectBoard.AddAllItemsRecord = true;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        bool reloaded = false;

        this.selectBoard.Enabled = true;

        if (siteNameIsAll)
        {
            this.selectBoard.Enabled = false;

            if (!URLHelper.IsPostback())
            {
                // Load the uniselector in order to select the ##all## value
                selectBoard.ReloadData(true);
                reloaded = true;
            }

            if (this.selectBoard.DropDownSingleSelect.Items.Count > 0)
            {
                this.selectBoard.DropDownSingleSelect.SelectedIndex = 0;
            }
        }

        if (URLHelper.IsPostback()
            && (siteNameAvailable || siteNameIsAll))
        {
            if (!reloaded)
            {
                selectBoard.ReloadData(true);
            }

            pnlUpdate.Update();
        }
    }


    /// <summary>
    /// Creates child controls and loads update panel container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (selectBoard == null)
        {
            this.pnlUpdate.LoadContainer();
        }

        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Sets the selectedSiteId if the SiteName field is available in the form.
    /// </summary>
    private void SetFormSiteName()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), "");
            if (String.IsNullOrEmpty(siteName) || siteName.Equals("##all##", StringComparison.InvariantCultureIgnoreCase))
            {
                siteNameIsAll = true;
                return;
            }
            else if (siteName.Equals("##currentsite##", StringComparison.InvariantCultureIgnoreCase))
            {
                siteName = CMSContext.CurrentSiteName;
            }

            if (!String.IsNullOrEmpty(siteName))
            {
                SiteInfo siteObj = SiteInfoProvider.GetSiteInfo(siteName);
                if (siteObj != null)
                {
                    selectBoard.SiteID = siteObj.SiteID;
                    siteNameAvailable = true;
                    return;
                }
            }
        }

        selectBoard.SiteID = CMSContext.CurrentSiteID;
    }

    #endregion

}
