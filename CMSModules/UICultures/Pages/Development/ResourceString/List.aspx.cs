using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.ResourceManager;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_UICultures_Pages_Development_ResourceString_List : SiteManagerPage
{
    #region "Variables"

    private int uiCultureID;


    private UICultureInfo ui;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get culture ID from query string
        uiCultureID = QueryHelper.GetInteger("UIcultureID", 0);

        // Get requested culture
        ui = UICultureInfoProvider.GetSafeUICulture(uiCultureID);
        EditedObject = ui;

        UICultureInfo dui = null;
        if (string.Equals(ui.UICultureCode, CultureHelper.DefaultUICulture, StringComparison.InvariantCultureIgnoreCase))
        {
            dui = ui;
            gridStrings.GridName = "List_defaultculture.xml";
        }
        else
        {
            // Ty to get default UI culture
            try
            {
                dui = UICultureInfoProvider.GetUICultureInfo(CultureHelper.DefaultUICulture);
            }
            catch
            { }
        }

        if (dui != null)
        {
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@Culture", ui.UICultureID);
            parameters.AddId("@DefaultUICultureID", dui.UICultureID);

            // Setup the grid
            gridStrings.QueryParameters = parameters;
            gridStrings.OnAction += UniGridUICultures_OnAction;
            gridStrings.OnExternalDataBound += UniGridStrings_OnExternalDataBound;

            InitializeMasterPage();
        }
        else
        {
            // Default UI culture does not exist - hide the grid and display error message
            gridStrings.StopProcessing = true;
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        // Init the header for the default culture
        if (gridStrings.GridView.HeaderRow != null)
        {
            try
            {
                string text = string.Format(GetString("unigrid.uiculture_strings_list.columns.english"), CultureHelper.DefaultUICulture);
                // Change column name "Default" to "Default (<default_culture>)"
                foreach (Control cntrl in gridStrings.GridView.HeaderRow.Cells[2].Controls)
                {
                    if (cntrl is LinkButton)
                    {
                        if (cntrl.Controls.Count == 0)
                        {
                            // Link button contains one control only => it's just text
                            LinkButton lbl = (LinkButton)cntrl;
                            lbl.Text = text;
                        }
                        else
                        {
                            // Link button contains several controls => text + space (&nbsp;) + image (up arrow/down arrow)
                            foreach (Control c in cntrl.Controls)
                            {
                                if (c is LiteralControl)
                                {
                                    // Change text only
                                    LiteralControl ltl = (LiteralControl)c;
                                    if (!ltl.Text.Contains("&"))
                                    {
                                        ltl.Text = text;
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        base.Render(writer);
    }


    protected void InitializeMasterPage()
    {
        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Development-UICulture_Strings_List.NewString");
        actions[0, 3] = ResolveUrl("New.aspx?uicultureid=" + uiCultureID);
        actions[0, 5] = GetImageUrl("CMSModules/CMS_UICultures/addstring.png");

        CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Handles the UniGrid's OnExternalDataBound event.
    /// </summary>
    protected object UniGridStrings_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "edititem":
                ImageButton ib = sender as ImageButton;
                if (ib != null)
                {
                    GridViewRow gvr = parameter as GridViewRow;
                    if (gvr != null)
                    {
                        DataView dv = gvr.DataItem as DataView;
                        if (dv != null)
                        {
                            if (ui != null)
                            {
                                ResourceStringInfo ri = SqlResourceManager.GetResourceStringInfo(ValidationHelper.GetString(dv[0], ""), ui.UICultureCode);
                                if (ri != null)
                                {
                                    ib.OnClientClick = String.Format("location.href='Edit.aspx?stringid={0}&uicultureid={1}'; return false;", ri.StringId, ui.UICultureID);
                                }
                            }
                        }
                    }
                }
                break;

            case "stringiscustom":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "culturetext":
            case "defaulttext":
                return MacroResolver.RemoveSecurityParameters(parameter.ToString(), true, null);
        }

        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridUICultures_OnAction(string actionName, object actionArgument)
    {
        ResourceStringInfo ri = SqlResourceManager.GetResourceStringInfo(actionArgument.ToString(), ui.UICultureCode);

        switch (actionName)
        {
            case "edit":
                URLHelper.Redirect(string.Format("Edit.aspx?stringid={0}&uicultureid={1}", ri.StringId, ui.UICultureID));
                break;

            case "delete":
                SqlResourceManager.DeleteResourceStringInfo(actionArgument.ToString(), ui.UICultureCode);
                break;

            case "deleteResource":
                SqlResourceManager.DeleteResourceStringInfo(actionArgument.ToString(), CultureHelper.DefaultUICulture);
                break;
        }
    }

    #endregion
}