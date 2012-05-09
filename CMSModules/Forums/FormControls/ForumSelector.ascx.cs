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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Forums;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.FormEngine;

public partial class CMSModules_Forums_FormControls_ForumSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Constants"

    const string ADHOCFORUM_VALUE = "ad_hoc_forum";

    #endregion


    #region "Variables"

    private int siteId = 0;

    #endregion


    #region "Properties"

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
            EnsureChildControls();
            base.Enabled = value;
            uniSelector.Enabled = value;
            uniSelector.TextBoxSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            // If field type is integer and selected item is "ad-hoc-forum" return int.MinValue
            if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
            {
                if (ValidationHelper.GetString(uniSelector.Value, null) == ADHOCFORUM_VALUE)
                {
                    return int.MinValue;
                }
            }
            return uniSelector.Value;
        }
        set
        {
            EnsureChildControls();
            // If field type is integer and incoming value is in.MinValue or "ad-hoc-forum" preselect "ad-hoc-forum" in selector
            if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
            {
                if ((ValidationHelper.GetInteger(value, 0) == int.MinValue) || (ValidationHelper.GetString(value, null) == ADHOCFORUM_VALUE))
                {
                    value = ADHOCFORUM_VALUE;
                }
            }
            uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to allow more than one user to select.
    /// </summary>
    public SelectionModeEnum SelectionMode
    {
        get
        {
            EnsureChildControls();
            return uniSelector.SelectionMode;
        }
        set
        {
            EnsureChildControls();
            uniSelector.SelectionMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'Create adHoc forum' option should be displayed.
    /// </summary>
    public bool DisplayAdHocOption
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayAdHocOption"), true);
        }
        set
        {
            this.SetValue("DisplayAdHocOption", value);
        }
    }


    /// <summary>
    /// Gets or sets the sitename. If sitename is defined selector displays forums only from this site. 
    /// If sitename is not defined, selector displays forums for current site
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            uniSelector.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'All forums' option should be displayed in dropdown list.
    /// </summary>
    public bool DisplayAllForumsOption
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayAllForumsOption"), false);
        }
        set
        {
            this.SetValue("DisplayAllForumsOption", value);
        }
    }


    /// <summary>
    /// Gets or sets a value indicating whether to check the AliasPath variable in the URL.
    /// If set to true, use AliasPath variable from URL to decide whether to show or hide the "ad-hoc forum" item in the selector.
    /// </summary>
    public bool CheckAliasPath
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckAliasPath"), false);
        }
        set
        {
            this.SetValue("CheckAliasPath", value);
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether group forums is included in list.
    /// </summary>
    private bool ShowGroupForums
    {
        get;
        set;
    }


    /// <summary>
    /// Sets the property value of control, setting the value affects only local property value.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    /// <param name="value">Value</param>
    public override void SetValue(string propertyName, object value)
    {
        // Add special behavior for selection mode
        if (propertyName.ToLower() == "selectionmode")
        {
            this.SelectionMode = (SelectionModeEnum)Enum.Parse(typeof(SelectionModeEnum), Convert.ToString(value));
        }
        
        base.SetValue(propertyName, value);
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set uniselector
        uniSelector.DisplayNameFormat = "{%ForumDisplayName%}";
        uniSelector.ReturnColumnName = "ForumName";
        uniSelector.AllowEmpty = false;
        uniSelector.AllowAll = false;

        // Return forum name or ID according to type of field (if no field specified forum name is returned)
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            uniSelector.ReturnColumnName = "ForumID";
            uniSelector.AllowEmpty = true;
            ShowGroupForums = true;
        }

        if (this.DependsOnAnotherField)
        {
            CheckAliasPath = true;
        }

        if (this.DisplayAllForumsOption)
        {
            if (ContainsAdHocForum())
            {
                uniSelector.SpecialFields = new string[2, 2] { { GetString("general.selectall"), "" }, 
                                                               { GetString("ForumSelector.AdHocForum"), ADHOCFORUM_VALUE } };
            }
            else
            {
                uniSelector.SpecialFields = new string[1, 2] { { GetString("general.selectall"), "" } };
            }
        }
        else
        {
            if (ContainsAdHocForum())
            {
                uniSelector.SpecialFields = new string[1, 2] { { GetString("ForumSelector.AdHocForum"), ADHOCFORUM_VALUE } };
            }
        }

        // Set resource prefix based on mode
        if ((this.SelectionMode == SelectionModeEnum.Multiple) || (this.SelectionMode == SelectionModeEnum.MultipleButton) || (this.SelectionMode == SelectionModeEnum.MultipleTextBox))
        {
            uniSelector.ResourcePrefix = "forumsselector";
            this.uniSelector.FilterControl = "~/CMSModules/Forums/Filters/ForumGroupFilter.ascx";
        }
        else
        {
            uniSelector.ResourcePrefix = "forumselector";
        }

        SetupWhereCondition();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (URLHelper.IsPostback()
            && this.DependsOnAnotherField)
        {
            SetupWhereCondition();
            uniSelector.Reload(true);
            pnlUpdate.Update();
        }
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (uniSelector == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Generates a where condition for the uniselector.
    /// </summary>
    private void SetupWhereCondition()
    {
        SetFormSiteName();

        SiteInfo si = SiteInfoProvider.GetSiteInfo(this.SiteName);
        if (si != null)
        {
            siteId = si.SiteID;
        }
        else
        {
            siteId = CMSContext.CurrentSiteID;
        }

        // Select non group forum of current site
        uniSelector.WhereCondition = "ForumDocumentID IS NULL AND ForumGroupID IN (SELECT GroupID FROM Forums_ForumGroup WHERE " + (!ShowGroupForums? "GroupGroupID IS NULL AND " : "") + "GroupSiteID = " + siteId + ")";
        uniSelector.SetValue("SiteID", siteId);
    }


    /// <summary>
    /// Sets the site name if the SiteName field is available in the form.
    /// </summary>
    private void SetFormSiteName()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            SiteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), "");
        }
    }


    /// <summary>
    /// Returns true when the "Ah-hoc forum" item should be displayed.
    /// </summary>
    private bool ContainsAdHocForum()
    {
        string aliasPath = QueryHelper.GetString("AliasPath", null);
        return (!CheckAliasPath) || (!String.IsNullOrEmpty(aliasPath));
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
            // Check if 'ad-hoc-forum" has been selected and modify condition accordingly
            else if (ValidationHelper.GetString(uniSelector.Value, null) == ADHOCFORUM_VALUE)
            {
                return String.Format("[{1}] IN (SELECT ForumID FROM Forums_Forum WHERE ForumSiteID={0} AND ForumName LIKE 'AdHoc-%')", CMSContext.CurrentSiteID, FieldInfo.Name);
            }
        }
        return null;
    }

    #endregion
}
