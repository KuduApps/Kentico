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
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.UIControls;
using CMS.MessageBoard;

public partial class CMSModules_MessageBoards_Controls_Messages_BoardSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAddAllItemsRecord = false;
    private int mGroupId = 0;
    private int mSiteId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the current group.
    /// </summary>
    public int GroupID
    {
        get
        {
            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
        }
    }


    /// <summary>
    /// ID of the current site.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
    /// </summary>
    public bool AddAllItemsRecord
    {
        get
        {
            return mAddAllItemsRecord;
        }
        set
        {
            mAddAllItemsRecord = value;
        }
    }


    /// <summary>
    /// Gets the inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return this.uniSelector;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        this.uniSelector.WhereCondition = "BoardSiteID = " + this.SiteID + " AND " + (this.GroupID > 0 ? "BoardGroupID = " + this.GroupID : "((BoardGroupID = 0) OR (BoardGroupID IS NULL))");
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        this.uniSelector.AllowAll = false;
        this.uniSelector.AllowEmpty = false;
        if (this.AddAllItemsRecord)
        {
            this.uniSelector.SpecialFields = new string[,] { { GetString("general.selectall"), "ALL" } };
        }
    }
}
