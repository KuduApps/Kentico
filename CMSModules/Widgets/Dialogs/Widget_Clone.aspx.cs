using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_Widgets_Dialogs_Widget_Clone : CMSModalSiteManagerPage
{
    private int widgetId = 0;
    private WidgetInfo wi = null;

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeComponents();

        // Get the widget ID
        widgetId = QueryHelper.GetInteger("widgetID", 0);

        // Select widget category on dropdown list
        wi = WidgetInfoProvider.GetWidgetInfo(widgetId);

        if (wi != null)
        {
            if (!RequestHelper.IsPostBack())
            {
                int counter = 1;
                string codenameBase = TextHelper.LimitLength(wi.WidgetName, 98, "");

                Regex regexCodename = RegexHelper.GetRegex("^(.*?)_(\\d+)$");
                Regex regexDisplayName = RegexHelper.GetRegex("^(.*?)\\((\\d+)\\)$");

                Match match = regexCodename.Match(wi.WidgetName);
                if (match.Success && (match.Groups.Count == 3))
                {
                    // Incremental codename
                    codenameBase = match.Groups[1].Value;
                    counter = ValidationHelper.GetInteger(match.Groups[2].Value, 1);
                }

                // Find unique widget name                
                while (WidgetInfoProvider.GetWidgetInfo(codenameBase + "_" + counter) != null)
                {
                    counter++;
                }

                // New names
                string newWidgetName = codenameBase + "_" + counter;
                string newWidgetDisplayName = wi.WidgetDisplayName;

                match = regexDisplayName.Match(wi.WidgetDisplayName);
                if (match.Success && (match.Groups.Count == 3))
                {
                    // Incremental display name
                    newWidgetDisplayName = match.Groups[1].Value + "(" + counter + ")";
                }
                else
                {
                    // Full display name
                    newWidgetDisplayName = wi.WidgetDisplayName + "(" + counter + ")";
                }

                txtWidgetDisplayName.Text = newWidgetDisplayName;
                txtWidgetName.Text = newWidgetName;
                categorySelector.Value = wi.WidgetCategoryID;
            }
        }
    }


    /// <summary>
    /// Initialize components - master page, labels.
    /// </summary>
    private void InitializeComponents()
    {
        // Init page title
        this.CurrentMaster.Title.TitleText = GetString("widgets.clone.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Widgets/clonewidget.png");
        this.CurrentMaster.Title.HelpTopicName = "widgets_clone";

        // Init GUI        
        btnOk.Text = GetString("general.ok");
        btnCancel.Text = GetString("General.Cancel");
        btnCancel.OnClientClick = "window.close(); return false;";

        rfvWidgetDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvWidgetName.ErrorMessage = GetString("general.requirescodename");
    }


    /// <summary>
    /// Button OK click handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Trim text values
        txtWidgetName.Text = TextHelper.LimitLength(txtWidgetName.Text.Trim(), 100, "");
        txtWidgetDisplayName.Text = TextHelper.LimitLength(txtWidgetDisplayName.Text.Trim(), 100, "");

        // Validate the text box fields
        string errorMessage = new Validator()
            .NotEmpty(txtWidgetName.Text, rfvWidgetName.ErrorMessage)
            .NotEmpty(txtWidgetDisplayName.Text, rfvWidgetDisplayName.ErrorMessage)
            .IsCodeName(txtWidgetName.Text, GetString("general.InvalidCodeName"))
            .Result;

        // Check if widget with same name exists
        if (WidgetInfoProvider.GetWidgetInfo(txtWidgetName.Text) != null)
        {
            errorMessage = GetString("general.codenameexists");
        }

        if (errorMessage == "")
        {
            // Clone widget info
            WidgetInfo nwi = new WidgetInfo(wi, false);

            // Modify info data
            nwi.WidgetID = 0;
            nwi.WidgetGUID = Guid.NewGuid();
            nwi.WidgetName = txtWidgetName.Text;
            nwi.WidgetDisplayName = txtWidgetDisplayName.Text;
            nwi.WidgetCategoryID = ValidationHelper.GetInteger(categorySelector.Value, 0);

            // Add new web part to database
            WidgetInfoProvider.SetWidgetInfo(nwi);

            // Clone widget security
            DataSet ds = WidgetRoleInfoProvider.GetWidgetRoles("WidgetID = " + wi.WidgetID, null, 0, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WidgetRoleInfo nwri = new WidgetRoleInfo(dr);
                    nwri.WidgetID = nwi.WidgetID;
                    WidgetRoleInfoProvider.SetWidgetRoleInfo(nwri);
                }
            }

            // Update widget category counts
            WidgetCategoryInfoProvider.UpdateCategoryWidgetChildCount(0, nwi.WidgetCategoryID);

            // Duplicate associated thumbnail
            MetaFileInfoProvider.CopyMetaFiles(wi.WidgetID, nwi.WidgetID, PortalObjectType.WIDGET, MetaFileInfoProvider.OBJECT_CATEGORY_THUMBNAIL, null);

            string script = String.Empty;
            string refreshLink = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx?widgetid=" + nwi.WidgetID + "&reload=true");
            if (QueryHelper.GetBoolean("reloadAll", true))
            {
                // Refresh web part tree and select/edit new widget
                script = "wopener.location = '" + refreshLink + "';";
            }
            else
            {
                script += "wopener.parent.parent.frames['widgettree'].location.href ='" + refreshLink + "';";
            }
            script += "window.close();";

            ltlScript.Text = ScriptHelper.GetScript(script);
        }
        else
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
        }
    }
}
