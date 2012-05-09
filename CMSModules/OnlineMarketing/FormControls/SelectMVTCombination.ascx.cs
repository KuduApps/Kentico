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
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_FormControls_SelectMVTCombination : FormEngineUserControl
{
    #region "Variables"

    private int mPageTemplateID = 0;
    private int mDocumentID = 0;
    private bool dataLoaded = false;
    private bool mUseQueryStringOnChange = false;
    private string mQueryStringKey = string.Empty;
    private bool mPostbackOnChange = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the combination id.
    /// </summary>
    /// <value></value>
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
    /// Return colum name for uni selector
    /// </summary>
    public string ReturnColumnName
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.ReturnColumnName;
        }
        set
        {
            EnsureChildControls();
            ucUniSelector.ReturnColumnName = value;
        }
    }


    /// <summary>
    /// All record value for uni selector
    /// </summary>
    public string AllRecordValue
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.AllRecordValue;
        }
        set
        {
            EnsureChildControls();
            ucUniSelector.AllRecordValue = value;
        }
    }


    /// <summary>
    /// Where condition for uniselector
    /// </summary>
    public string WhereCondition
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.WhereCondition;
        }
        set
        {
            EnsureChildControls();
            ucUniSelector.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the page template ID.
    /// </summary>
    public int PageTemplateID
    {
        get
        {
            EnsureChildControls();
            return mPageTemplateID;
        }
        set
        {
            EnsureChildControls();
            mPageTemplateID = value;
        }
    }


    /// <summary>
    /// Gets or sets the document ID.
    /// </summary>
    public int DocumentID
    {
        get
        {
            EnsureChildControls();
            return mDocumentID;
        }
        set
        {
            EnsureChildControls();
            mDocumentID = value;
        }
    }


    /// <summary>
    /// Gets the uni selector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector;
        }
    }


    /// <summary>
    /// Gets the drop down select control.
    /// </summary>
    public DropDownList DropDownSelect
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets a value indicating whether this instance has data.
    /// </summary>
    public bool HasData
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.HasData;
        }
    }


    /// <summary>
    /// Gets or sets a value indicating whether to use query string and redirect when OnChange event is fired or if use a postback.
    /// </summary>
    public bool UseQueryStringOnChange
    {
        get
        {
            return mUseQueryStringOnChange;
        }
        set
        {
            mUseQueryStringOnChange = value;
        }
    }


    /// <summary>
    /// Enables/disables all (all) value
    /// </summary>
    public bool AllowAll
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.AllowAll;
        }
        set
        {
            EnsureChildControls();
            ucUniSelector.AllowAll = value;
        }
    }


    /// <summary>
    /// Enables/disables emtpy (none) value
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            EnsureChildControls();
            return ucUniSelector.AllowEmpty;
        }
        set
        {
            EnsureChildControls();
            ucUniSelector.AllowEmpty = value;
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
    /// Gets or sets the query string key when the UseQueryStringOnChangeis set to true.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return mQueryStringKey;
        }
        set
        {
            mQueryStringKey = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Handles the PreRender event of the Page control.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (PostbackOnChange)
        {
            ucUniSelector.DropDownSingleSelect.AutoPostBack = true;
            ScriptManager scr = ScriptManager.GetCurrent(Page);
            scr.RegisterPostBackControl(ucUniSelector.DropDownSingleSelect);
        }

        if (!mUseQueryStringOnChange)
        {
            ucUniSelector.DropDownSingleSelect.AutoPostBack = true;
        }
        else if (!string.IsNullOrEmpty(mQueryStringKey))
        {
            string script = @"
function ChangeCombination() {
    var ddlEl = document.getElementById('" + ucUniSelector.DropDownSingleSelect.ClientID + @"');
    if (ddlEl != null) {
        var value = ddlEl.options[ddlEl.selectedIndex].value;
        var url = '" + URLHelper.RemoveUrlParameter(URLHelper.RemoveUrlParameter(URLHelper.CurrentURL, "mvtitemupdated"), "mvtiszoneupdated") + @"';
        url = AddUrlParameter(url, 'combinationid', value, true);
        document.location.replace(url);
    }
}";

            ScriptHelper.RegisterStartupScript(this, typeof(string), "mvtSelectCombination", ScriptHelper.GetScript(script));

            // Use query string instead of postback
            ucUniSelector.DropDownSingleSelect.Attributes.Add("onchange", "ChangeCombination()");
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    /// <param name="forceReload">If true, data are loaded in all cases</param>
    public void ReloadData(bool forceReload)
    {
        if (forceReload || !dataLoaded)
        {
            string where = SqlHelperClass.AddWhereCondition(ucUniSelector.WhereCondition, "MVTCombinationPageTemplateID =" + PageTemplateID);
            where = SqlHelperClass.AddWhereCondition(where, "(MVTCombinationDocumentID = " + DocumentID + ") OR (MVTCombinationDocumentID IS NULL)");

            ucUniSelector.WhereCondition = where;
            ucUniSelector.IsLiveSite = IsLiveSite;

            ucUniSelector.Reload(forceReload);
            dataLoaded = true;
        }
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // Call base method
        base.CreateChildControls();

        // If selector is not defined load updat panel container
        if (ucUniSelector == null)
        {
            this.pnlUpdate.LoadContainer();
        }
    }

    #endregion
}
