using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.OnlineMarketing;
using CMS.GlobalHelper;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_FormControls_SelectMVTest : FormEngineUserControl
{
    #region "Variables"

    private int mNodeID = 0;
    private bool mPostbackOnChange = false;
    private bool dataLoaded = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns or sets MVTestID value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.Value;
        }
        set
        {
            EnsureChildControls();
            ucUniSelector.Value = value;
        }
    }


    /// <summary>
    /// If true, full postback is raised when item changed.
    /// </summary>
    public bool PostbackOnChange
    {
        get
        {
            return mPostbackOnChange;
        }
        set
        {
            mPostbackOnChange = value;
        }
    }


    /// <summary>
    /// Enables/disables control
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            ucUniSelector.Enabled = value;
            base.Enabled = value;
        }
    }


    /// <summary>
    /// Node ID.
    /// </summary>
    public int NodeID
    {
        get
        {
            return mNodeID;
        }
        set
        {
            mNodeID = value;
        }
    }


    /// <summary>
    /// If true (all) option is enabled.
    /// </summary>
    public bool AllowAll
    {
        get
        {
            return ucUniSelector.AllowAll;
        }
        set
        {
            ucUniSelector.AllowAll = value;
        }
    }


    /// <summary>
    /// If true empty record is enabled.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return ucUniSelector.AllowEmpty;
        }
        set
        {
            ucUniSelector.AllowEmpty = value;
        }
    }


    /// <summary>
    /// Returns column name of uniselector.
    /// </summary>
    public string ReturnColumnName
    {
        get
        {
            return ucUniSelector.ReturnColumnName;
        }
        set
        {
            ucUniSelector.ReturnColumnName = value;
        }
    }


    /// <summary>
    /// Gets or sets all record value for uni selector.
    /// </summary>
    public string AllRecordValue
    {
        get
        {
            return ucUniSelector.AllRecordValue;
        }
        set
        {
            ucUniSelector.AllRecordValue = value;
        }
    }


    /// <summary>
    /// Gets Uniselector object
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return ucUniSelector;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData(false);

        if (PostbackOnChange)
        {
            ucUniSelector.DropDownSingleSelect.AutoPostBack = true;
            ScriptManager scr = ScriptManager.GetCurrent(Page);
            scr.RegisterPostBackControl(ucUniSelector.DropDownSingleSelect);            
        }

        if (!URLHelper.IsPostback())
        {
            // If some test belongs to node give by NodeID - preselect it in MVTest selector
            if (NodeID != 0)
            {
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                TreeNode node = tree.SelectSingleNode(NodeID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture);

                if (node != null)
                {
                    DataSet ds = ABTestInfoProvider.GetABTests("MVTestSiteID = " + CMSContext.CurrentSiteID + " AND MVTestPage = '" + SqlHelperClass.GetSafeQueryString(node.NodeAliasPath, false) + "'", String.Empty, 1, "MVTestID", null);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        int abTestID = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["MVTestID"], 0);
                        if (abTestID != 0)
                        {
                            ucUniSelector.Value = abTestID;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    /// <param name="forceReload">If true, data are loaded in all cases</param>
    public void ReloadData(bool forceReload)
    {
        if (forceReload || !dataLoaded)
        {
            ucUniSelector.IsLiveSite = IsLiveSite;
            ucUniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(ucUniSelector.WhereCondition, "MVTestSiteID =" + CMSContext.CurrentSiteID);
            ucUniSelector.Reload(forceReload);
            dataLoaded = true;
        }
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (ucUniSelector == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Returns the value of the given property.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    public override object GetValue(string propertyName)
    {
        switch (propertyName.ToLower())
        {
            case "allrecordvalue":
                return AllRecordValue;
        }

        return base.GetValue(propertyName);
    }


    /// <summary>
    /// Sets the property value of the control, setting the value affects only local property value.
    /// </summary>
    /// <param name="propertyName">Property name to set</param>
    /// <param name="value">New property value</param>
    public override void SetValue(string propertyName, object value)
    {
        switch (propertyName.ToLower())
        {
            case "specialfields":
                UniSelector.SpecialFields = (string[,])value;
                break;

            case "allowempty":
                AllowEmpty = ValidationHelper.GetBoolean(value, false);
                break;

            case "returncolumnname":
                ReturnColumnName = ValidationHelper.GetString(value, String.Empty);
                break;
        }
        base.SetValue(propertyName, value);
    }

    #endregion
}
