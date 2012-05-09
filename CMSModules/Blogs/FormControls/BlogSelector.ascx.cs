using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.PortalControls;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Blogs_FormControls_BlogSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mIsLiveSite = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets selected items.
    /// </summary>
    public override object Value
    {
        get
        {
            string aliasPath = ValidationHelper.GetString(blogSelector.Value, null);
            if (aliasPath == "0")
            {
                return string.Empty;
            }
            if (!aliasPath.EndsWith("/%"))
            {
                aliasPath = aliasPath.TrimEnd('/') + "/%";
            }

            return aliasPath;
        }
        set
        {
            if (blogSelector == null)
            {
                pnlUpdate.LoadContainer();
            }

            if (blogSelector != null)
            {
                string aliasPath = ValidationHelper.GetString(value, String.Empty);
                if (aliasPath == string.Empty)
                {
                    blogSelector.Value = "0";
                }
                else
                {
                    aliasPath = aliasPath.TrimEnd('%').TrimEnd('/');
                    if (aliasPath == string.Empty)
                    {
                        aliasPath = "/";
                    }
                    blogSelector.Value = aliasPath;
                }
            }
        }
    }


    /// <summary>
    /// Gets the inner blogSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return blogSelector;
        }
    }


    /// <summary>
    /// Gets the single select drop down field.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            EnsureChildControls();
            return blogSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets UpdatePanel of selector.
    /// </summary>
    public CMSUpdatePanel UpdatePanel
    {
        get
        {
            return pnlUpdate;
        }
    }


    /// <summary>
    /// Enables or disables site selector dropdown.
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
            DropDownSingleSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Enables or disables live site mode of selection dialog.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return mIsLiveSite;
        }
        set
        {
            mIsLiveSite = value;
            if (blogSelector != null)
            {
                blogSelector.IsLiveSite = value;
            }
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            blogSelector.StopProcessing = true;
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
        // Generate where condition
        UpdateWhereCondition();

        string[,] fields = new string[,] { { GetString("blogselector.selectblog"), "0" } };
        blogSelector.SpecialFields = fields;

        blogSelector.IsLiveSite = IsLiveSite;
        blogSelector.Reload(false);
    }


    protected override void EnsureChildControls()
    {
        if (blogSelector == null)
        {
            pnlUpdate.LoadContainer();
        }
        base.EnsureChildControls();
    }

    #endregion


    /// <summary>
    /// Updates uni selector where condition based on current properties values.
    /// </summary>
    public void UpdateWhereCondition()
    {
        string where = "NodeOwner=" + CMSContext.CurrentUser.UserID;
        DataClassInfo dci = DataClassInfoProvider.GetDataClass("cms.blog");
        if (dci != null)
        {
            where = SqlHelperClass.AddWhereCondition(where, "NodeClassID=" + dci.ClassID);
        }

        where = SqlHelperClass.AddWhereCondition(where, "NodeSiteID=" + CMSContext.CurrentSiteID);

        blogSelector.WhereCondition = where;
    }


    /// <summary>
    /// Reloads all controls.
    /// </summary>
    /// <param name="forceReload">Indicates if data should be loaded from DB</param>
    public void Reload(bool forceReload)
    {
        blogSelector.Reload(forceReload);
    }
}
