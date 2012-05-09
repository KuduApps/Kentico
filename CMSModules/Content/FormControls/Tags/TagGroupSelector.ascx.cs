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

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.PortalControls;
using CMS.CMSHelper;

public partial class CMSModules_Content_FormControls_Tags_TagGroupSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mUseDropdownList = false;
    private int mSiteId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (UseGroupNameForSelection)
            {
                return TagGroupName;
            }
            else
            {
                return TagGroupID;
            }
        }
        set
        {
            if (UseGroupNameForSelection)
            {
                TagGroupName = ValidationHelper.GetString(value, "");
            }
            else
            {
                TagGroupID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the TagGroup ID.
    /// </summary>
    public int TagGroupID
    {
        get
        {
            if (UseGroupNameForSelection)
            {
                string name = ValidationHelper.GetString(UniSelector.Value, "");
                TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(name, CMSContext.CurrentSite.SiteID);
                if (tgi != null)
                {
                    return tgi.TagGroupID;
                }
                return 0;
            }
            else
            {
                return ValidationHelper.GetInteger(UniSelector.Value, 0);
            }
        }
        set
        {
            if (UseGroupNameForSelection)
            {
                TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(value);
                if (tgi != null)
                {
                    UniSelector.Value = tgi.TagGroupName;
                }
            }
            else
            {
                UniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the TagGroup code name.
    /// </summary>
    public string TagGroupName
    {
        get
        {
            if (UseGroupNameForSelection)
            {
                return ValidationHelper.GetString(UniSelector.Value, "");
            }
            else
            {
                int id = ValidationHelper.GetInteger(UniSelector.Value, 0);
                TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(id);
                if (tgi != null)
                {
                    return tgi.TagGroupName;
                }
                return "";
            }
        }
        set
        {
            if (UseGroupNameForSelection)
            {
                UniSelector.Value = value;
            }
            else
            {
                TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(value, CMSContext.CurrentSite.SiteID);
                if (tgi != null)
                {
                    UniSelector.Value = tgi.TagGroupID;
                }
            }
        }
    }


    /// <summary>
    ///  If true, selected value is TagGroupName, if false, selected value is TagGroupID.
    /// </summary>
    public bool UseGroupNameForSelection
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("UseGroupNameForSelection"), true);
        }
        set
        {
            SetValue("UseGroupNameForSelection", value);
            UniSelector.ReturnColumnName = (value ? "TagGroupName" : "TagGroupID");
        }
    }


    /// <summary>
    /// Indicates whether to use dropdownlist or textbox selection mode.
    /// </summary>
    public bool UseDropdownList
    {
        get
        {
            return mUseDropdownList;
        }
        set
        {
            mUseDropdownList = value;

            if (value)
            {
                UniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
            }
            else
            {
                UniSelector.SelectionMode = SelectionModeEnum.SingleTextBox;
            }
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
            UniSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
    /// </summary>
    public bool AddNoneItemsRecord
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AddNoneItemsRecord"), true);
        }
        set
        {
            SetValue("AddNoneItemsRecord", value);
            UniSelector.AllowEmpty = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether use autopostback or not.
    /// </summary>
    public bool UseAutoPostback
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("UseAutoPostback"), false);
        }
        set
        {
            SetValue("UseAutoPostback", value);
            if (uniSelector != null)
            {
                uniSelector.DropDownSingleSelect.AutoPostBack = value;
            }
        }
    }


    /// <summary>
    /// Gets the inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }
            return uniSelector;
        }
    }


    /// <summary>
    /// ID of the site which tag groups are to be displayed.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId <= 0)
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

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            UniSelector.StopProcessing = true;
        }
        else
        {
            SetFormSiteID();

            UniSelector.DisplayNameFormat = "{%TagGroupDisplayName%}";
            if (UseDropdownList)
            {
                UniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
            }
            else
            {
                UniSelector.SelectionMode = SelectionModeEnum.SingleTextBox;
            }
            UniSelector.AllowEditTextBox = true;
            UniSelector.WhereCondition = "TagGroupSiteID = " + SiteID;
            UniSelector.ReturnColumnName = (UseGroupNameForSelection ? "TagGroupName" : "TagGroupID");
            UniSelector.OrderBy = "TagGroupDisplayName";
            UniSelector.ObjectType = SiteObjectType.TAGGROUP;
            UniSelector.ResourcePrefix = "taggroupselect";
            uniSelector.DropDownSingleSelect.AutoPostBack = UseAutoPostback;
            UniSelector.AllowEmpty = AddNoneItemsRecord;
            UniSelector.IsLiveSite = IsLiveSite;

            if (UseGroupNameForSelection)
            {
                uniSelector.AllRecordValue = "";
                uniSelector.NoneRecordValue = "";
            }
        }
    }


    /// <summary>
    /// Loads public status according to the control settings.
    /// </summary>
    public void ReloadData()
    {
        uniSelector.Reload(true);
    }


    /// <summary>
    /// Sets the SiteID if the SiteName field is available in the form.
    /// </summary>
    private void SetFormSiteID()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), null);
            if (!String.IsNullOrEmpty(siteName))
            {
                SiteInfo siteObj = SiteInfoProvider.GetSiteInfo(siteName);
                if (siteObj != null)
                {
                    SiteID = siteObj.SiteID;
                }
            }
            else
            {
                SiteID = -1;
            }
        }
    }
}
