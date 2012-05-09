using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_UIProfiles_UIToolbar : CMSUserControl
{
    #region "Variables"

    protected string preselectedItem = null;

    protected string mElementName = null;
    protected string mModuleName = null;
    protected string mTargetFrameset = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns the UIElementInfo representing the first button of first group displayed.
    /// </summary>
    public UIElementInfo FirstUIElement
    {
        get
        {
            return this.uniMenu.FirstUIElement;
        }
    }


    /// <summary>
    /// Returns the UIElementInfo representing the explicitly highlighted UI element.
    /// </summary>
    public UIElementInfo HighlightedUIElement
    {
        get
        {
            return this.uniMenu.HighlightedUIElement;
        }
    }


    /// <summary>
    /// Returns the UIElementInfo representing the selected (either explicitly highlighted or first) UI element.
    /// </summary>
    public UIElementInfo SelectedUIElement
    {
        get
        {
            return this.uniMenu.SelectedUIElement;
        }
    }


    /// <summary>
    /// Indicates whether at least one group with at least one button is rendered in the menu.
    /// </summary>
    public bool MenuEmpty
    {
        get
        {
            return this.uniMenu.MenuEmpty;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first item should be highligted.
    /// </summary>
    public bool HighlightFirstItem
    {
        get
        {
            return this.uniMenu.HighlightFirstItem;
        }
        set
        {
            this.uniMenu.HighlightFirstItem = value;
        }
    }


    /// <summary>
    /// Indicates whether to remember item which was last selected and select it again.
    /// </summary>
    public bool RememberSelectedItem
    {
        get
        {
            return this.uniMenu.RememberSelectedItem;
        }
        set
        {
            this.uniMenu.RememberSelectedItem = value;
        }
    }


    /// <summary>
    /// Code name of the UI element.
    /// </summary>
    public string ElementName
    {
        get
        {
            return this.mElementName;
        }
        set
        {
            this.mElementName = value;
        }
    }


    /// <summary>
    /// Code name of the module.
    /// </summary>
    public string ModuleName
    {
        get
        {
            return this.mModuleName;
        }
        set
        {
            this.mModuleName = value;
            this.uniMenu.ModuleName = value;
        }
    }


    /// <summary>
    /// Target frameset in which the links are opened.
    /// </summary>
    public string TargetFrameset
    {
        get
        {
            return this.mTargetFrameset;
        }
        set
        {
            this.mTargetFrameset = value;
            this.uniMenu.TargetFrameset = value;
        }
    }


    /// <summary>
    /// Query parameter name for the preselection of the item.
    /// </summary>
    public string QueryParameterName
    {
        get;
        set;
    }

    #endregion


    #region "Custom events"

    /// <summary>
    /// Button filtered delegate.
    /// </summary>
    public delegate bool ButtonFilterEventHandler(UIElementInfo uiElement);


    /// <summary>
    /// Button created delegate.
    /// </summary>
    public delegate void ButtonCreatedEventHandler(UIElementInfo uiElement, string url);


    /// <summary>
    /// Button filtered event handler.
    /// </summary>
    public event ButtonFilterEventHandler OnButtonFiltered;


    /// <summary>
    /// Button created event handler.
    /// </summary>
    public event ButtonCreatedEventHandler OnButtonCreated;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Handle the preselection
        preselectedItem = QueryHelper.GetString(this.QueryParameterName, "");
        if (preselectedItem.StartsWith("cms.", StringComparison.InvariantCultureIgnoreCase))
        {
            preselectedItem = preselectedItem.Substring(4);
        }

        uniMenu.HighlightItem = preselectedItem;

        // If element name is not set, use root module element
        string elemName = this.ElementName;
        if (String.IsNullOrEmpty(elemName))
        {
            elemName = this.ModuleName.Replace(".", "");
        }

        // Get the UI elements
        DataSet ds = UIElementInfoProvider.GetChildUIElements(this.ModuleName, elemName);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            FilterElements(ds);

            int count = ds.Tables[0].Rows.Count;
            string[,] groups = new string[count, 4];

            // Prepare the list of elements
            int i = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string url = ValidationHelper.GetString(dr["ElementTargetURL"], "");

                if (url.EndsWith("ascx"))
                {
                    groups[i, 1] = url;
                }
                else
                {
                    groups[i, 3] = ValidationHelper.GetString(dr["ElementID"], "");
                }

                groups[i, 2] = "ContentMenuGroup";
                groups[i, 0] = ResHelper.LocalizeString(ValidationHelper.GetString(dr["ElementCaption"], ""));

                i++;
            }

            uniMenu.Groups = groups;

            // Button created & filtered event handler
            if (OnButtonCreated != null)
            {
                uniMenu.OnButtonCreated += new CMSAdminControls_UI_UniMenu_UniMenu.ButtonCreatedEventHandler(uniMenu_OnButtonCreated);
            }
            if (OnButtonFiltered != null)
            {
                uniMenu.OnButtonFiltered += new CMSAdminControls_UI_UniMenu_UniMenu.ButtonFilterEventHandler(uniMenu_OnButtonFiltered);
            }
        }

        // Add editing icon in development mode
        if (SettingsKeyProvider.DevelopmentMode && CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(this.ModuleName);
            if (ri != null)
            {
                ltlAfter.Text += UIHelper.GetResourceUIElementsLink(this.Page, ri.ResourceId);
            }
        }
    }


    /// <summary>
    /// Filters the dataset with UI Elements according to UI Profile of current user by default and according to custom event (if defined).
    /// </summary>
    protected void FilterElements(DataSet dsElements)
    {
        // For all tables in dataset
        foreach (DataTable dt in dsElements.Tables)
        {
            ArrayList deleteRows = new ArrayList();

            // Find rows to filter out
            foreach (DataRow dr in dt.Rows)
            {
                UIElementInfo uiElement = new UIElementInfo(dr);
                bool allowed = CMSContext.CurrentUser.IsAuthorizedPerUIElement(this.ModuleName, uiElement.ElementName);

                if (!allowed)
                {
                    deleteRows.Add(dr);
                }
            }

            // Delete the filtered rows
            foreach (DataRow dr in deleteRows)
            {
                dt.Rows.Remove(dr);
            }
        }
    }


    protected void uniMenu_OnButtonCreated(UIElementInfo uiElement, string url)
    {
        if (OnButtonCreated != null)
        {
            OnButtonCreated(uiElement, url);
        }
    }


    protected bool uniMenu_OnButtonFiltered(UIElementInfo uiElement)
    {
        if (OnButtonFiltered != null)
        {
            return OnButtonFiltered(uiElement);
        }
        return false;
    }

    #endregion
}
