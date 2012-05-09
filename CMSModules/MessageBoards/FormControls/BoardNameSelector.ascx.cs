using System;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.FormEngine;

public partial class CMSModules_MessageBoards_FormControls_BoardNameSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAddAllItemsRecord = true;
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
            return mGroupId;
        }
        set
        {
            mGroupId = value;
        }
    }


    /// <summary>
    /// ID of the current site.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId == 0)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }
            return mSiteId;
        }
        set
        {
            mSiteId = value;
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
            if (uniSelector != null)
            {
                uniSelector.Enabled = value;
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
            return uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }

            uniSelector.Value = value;
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
            return uniSelector;
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether group forums is included in list.
    /// </summary>
    private bool ShowGroupBoards
    {
        get;
        set;
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData(false);
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    /// <param name="forced">Indicates whether UniSelector Reload data should be called</param>
    public void ReloadData(bool forced)
    {
        // Return forum name or ID according to type of field (if no field specified forum name is returned)
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            uniSelector.WhereCondition = "BoardSiteID = " + SiteID;
            uniSelector.ReturnColumnName = "BoardID";
            uniSelector.AllowEmpty = true;
            ShowGroupBoards = true;
        }
        else
        {
            uniSelector.WhereCondition = "BoardSiteID = " + SiteID + " AND " + (GroupID > 0 ? "BoardGroupID = " + GroupID : "((BoardGroupID = 0) OR (BoardGroupID IS NULL))");
            uniSelector.AllowEmpty = false;
        }

        uniSelector.IsLiveSite = IsLiveSite;
        uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        uniSelector.AllowAll = false;
        if (AddAllItemsRecord)
        {
            uniSelector.SpecialFields = new string[,] { { GetString("general.selectall"), "" } };
        }

        if (forced)
        {
            uniSelector.Reload(true);
        }
    }


    /// <summary>
    /// Returns WHERE condition for selected form.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Return correct WHERE condition for integer if none value is selected
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            int id = ValidationHelper.GetInteger(uniSelector.Value, 0);
            if (id > 0)
            {
                return base.GetWhereCondition();
            }
        }
        return null;
    }
}
