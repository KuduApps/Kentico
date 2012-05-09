using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSModules_Content_Controls_SplitView_Documents_DocumentToolbar : CMSUserControl
{
    #region "Variables"

    private string mHorizintalImageUrl = "Design/Controls/SplitView/splitviewhorizontal.png";
    private string mVerticalImageUrl = "Design/Controls/SplitView/splitviewvertical.png";
    private string mCloseImageUrl = "Design/Controls/SplitView/splitviewclose.png";
    private string mSyncCheckedImageUrl = "Design/Controls/SplitView/synccheck.png";
    private string mSyncUncheckedImageUrl = "Design/Controls/SplitView/syncuncheck.png";
    private string buttonClass = "Button";
    private string buttonSelectedClass = "Button Selected";

    #endregion


    #region "Properties"

    /// <summary>
    /// Horizontal image URL.
    /// </summary>
    public string HorizontalImageUrl
    {
        get
        {
            return mHorizintalImageUrl;
        }
        set
        {
            mHorizintalImageUrl = value;
        }
    }


    /// <summary>
    /// Vertical image URL.
    /// </summary>
    public string VerticalImageUrl
    {
        get
        {
            return mVerticalImageUrl;
        }
        set
        {
            mVerticalImageUrl = value;
        }
    }


    /// <summary>
    /// Close image URL.
    /// </summary>
    public string CloseImageUrl
    {
        get
        {
            return mCloseImageUrl;
        }
        set
        {
            mCloseImageUrl = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        if (!RequestHelper.IsPostBack())
        {
            string preferredCultureCode = CMSContext.PreferredCultureCode;
            string currentSiteName = CMSContext.CurrentSiteName;
            string where = "CultureCode IN (SELECT DocumentCulture FROM View_CMS_Tree_Joined WHERE NodeID = " + nodeId + ")";
            DataSet documentCultures = CultureInfoProvider.GetCultures(where, null, 0, "CultureCode");

            // Get site cultures
            DataSet siteCultures = CultureInfoProvider.GetSiteCultures(currentSiteName);
            if (!DataHelper.DataSourceIsEmpty(siteCultures) && !DataHelper.DataSourceIsEmpty(documentCultures))
            {
                string suffixNotTranslated = GetString("SplitMode.NotTranslated");

                foreach (DataRow row in siteCultures.Tables[0].Rows)
                {
                    string cultureCode = ValidationHelper.GetString(row["CultureCode"], null);
                    string cultureName = ResHelper.LocalizeString(ValidationHelper.GetString(row["CultureName"], null));

                    string suffix = string.Empty;

                    // Copmare with preferred culture
                    if (string.Compare(preferredCultureCode, cultureCode, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        suffix = GetString("SplitMode.Current");
                    }
                    else
                    {
                        // Find culture
                        DataRow[] findRows = documentCultures.Tables[0].Select("CultureCode = '" + cultureCode + "'");
                        if (findRows.Length == 0)
                        {
                            suffix = suffixNotTranslated;
                        }
                    }

                    // Add new item
                    ListItem item = new ListItem(cultureName + " " + suffix, cultureCode);
                    drpCultures.Items.Add(item);
                }
            }

            drpCultures.SelectedValue = CMSContext.SplitModeCultureCode;
            drpCultures.Attributes.Add("onchange", "if (parent.CheckChanges('frame2')) { parent.FSP_ChangeCulture(this); }");
        }

        // Image URL and tooltip
        helpElem.IconUrl = GetImageUrl("Design/Controls/SplitView/splitviewhelpicon.png");
        imgHorizontal.ImageUrl = UIHelper.GetImageUrl(Page, HorizontalImageUrl);
        imgVertical.ImageUrl = UIHelper.GetImageUrl(Page, VerticalImageUrl);
        imgClose.ImageUrl = UIHelper.GetImageUrl(Page, CloseImageUrl);
        imgHorizontal.ToolTip = GetString("splitmode.horizontallayout");
        imgVertical.ToolTip = GetString("splitmode.verticallayout");
        imgClose.ToolTip = GetString("splitmode.closesplitmode");


        // Set css class
        switch (CMSContext.SplitMode)
        {
            case SplitModeEnum.Horizontal:
                divHorizontal.Attributes["class"] = buttonSelectedClass;
                divVertical.Attributes["class"] = buttonClass;
                break;

            case SplitModeEnum.Vertical:
                divHorizontal.Attributes["class"] = buttonClass;
                divVertical.Attributes["class"] = buttonSelectedClass;
                break;

            default:
                divHorizontal.Attributes["class"] = buttonClass;
                divVertical.Attributes["class"] = buttonClass;
                break;
        }

        string checkedSyncUrl = UIHelper.GetImageUrl(Page, mSyncCheckedImageUrl);
        string uncheckedSyncUrl = UIHelper.GetImageUrl(Page, mSyncUncheckedImageUrl);

        // Synchonize image
        string tooltip = GetString("splitmode.scrollbarsynchronization");
        imgSync.AlternateText = tooltip;
        imgSync.ToolTip = tooltip;
        imgSync.ImageUrl = CMSContext.SplitModeSyncScrollbars ? checkedSyncUrl : uncheckedSyncUrl;

        StringBuilder script = new StringBuilder();
        script.Append(@"
function FSP_Layout(vertical, frameName, cssClassName)
{
    if ((frameName != null) && parent.CheckChanges(frameName)) {
        if (cssClassName != null) {
            var element = document.getElementById('", pnlMain.ClientID, @"');
            if (element != null) {
                element.setAttribute(""class"", 'SplitToolbar ' + cssClassName);
                element.setAttribute(""className"", 'SplitToolbar ' + cssClassName);
            }
        }
        var divRight = document.getElementById('", divRight.ClientID, @"');
        if (vertical) {
            divRight.setAttribute(""class"", 'RightAlign');
            parent.FSP_VerticalLayout();
        }
        else {
            divRight.setAttribute(""class"", '');
            parent.FSP_HorizontalLayout();
        }
    }
}");

        script.Append(@"
function FSP_Close() { if (parent.CheckChanges()) { parent.FSP_CloseSplitMode(); } }"
);

        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "toolbarScript_" + ClientID, ScriptHelper.GetScript(script.ToString()));

        // Register js events
        imgHorizontal.Attributes.Add("onclick", "javascript:FSP_Layout(false,'frame1Vertical','Horizontal');");
        imgHorizontal.AlternateText = GetString("SplitMode.Horizontal");
        imgVertical.Attributes.Add("onclick", "javascript:FSP_Layout('true','frame1','Vertical');");
        imgVertical.AlternateText = GetString("SplitMode.Vertical");
        imgClose.Attributes.Add("onclick", "javascript:FSP_Close();");
        imgSync.Attributes.Add("onclick", "javascript:parent.FSP_SynchronizeToolbar()");
        imgClose.Style.Add("cursor", "pointer");

        // Set layout
        if (CMSContext.SplitMode == SplitModeEnum.Horizontal)
        {
            pnlMain.CssClass = "SplitToolbar Horizontal";
            divRight.Attributes["class"] = null;
        }
        else if (CMSContext.SplitMode == SplitModeEnum.Vertical)
        {
            pnlMain.CssClass = "SplitToolbar Vertical";
        }

        // Register Init script - FSP_ToolbarInit(selectorId, checkboxId)
        StringBuilder initScript = new StringBuilder();
        initScript.Append("parent.FSP_ToolbarInit('", drpCultures.ClientID, "','", imgSync.ClientID, "','", checkedSyncUrl, "','", uncheckedSyncUrl,
            "','", divHorizontal.ClientID, "','", divVertical.ClientID, "');");

        // Register js scripts
        ScriptHelper.RegisterJQuery(Page);
        ScriptHelper.RegisterStartupScript(Page, typeof(string), "FSP_initToolbar", ScriptHelper.GetScript(initScript.ToString()));
    }

    #endregion
}
