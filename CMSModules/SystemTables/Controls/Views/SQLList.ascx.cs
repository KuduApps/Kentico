using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSModules_SystemTables_Controls_Views_SQLList : CMSUserControl
{
    #region "Private variables"

    private bool mViews = true;

    #endregion


    #region "Public properties"

    public bool Views
    {
        get { return mViews; }
        set { mViews = value; }
    }

    public string EditPage
    {
        get;
        set;
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Views)
        {
            this.gridViews.GridName = "~/CMSModules/SystemTables/Controls/Views/Views_List.xml";
            this.fltViews.Column = "TABLE_NAME";
            this.gridViews.OrderBy = "IsCustom DESC, TABLE_NAME ASC";
        } 
        else 
        {
            this.gridViews.GridName = "~/CMSModules/SystemTables/Controls/Views/Procedures_List.xml";
            this.fltViews.Column = "ROUTINE_NAME";
            this.gridViews.OrderBy = "IsCustom DESC, ROUTINE_NAME ASC";
        }

        this.gridViews.OnAction += new OnActionEventHandler(gridViews_OnAction);
        this.gridViews.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridViews_OnExternalDataBound);

        if (this.drpCustom.Items.Count <= 0)
        {
            drpCustom.Items.Add(new ListItem(GetString("general.any"), ""));
            drpCustom.Items.Add(new ListItem(GetString("general.yes"), "1"));
            drpCustom.Items.Add(new ListItem(GetString("general.no"), "0"));
        }

        ReloadData();
    }


    /// <summary>
    /// Reloads grid.
    /// </summary>
    public void ReloadData()
    {
        string where = null;

        if (!String.IsNullOrEmpty(drpCustom.SelectedValue))
        {
            switch (drpCustom.SelectedValue)
            {
                case "1":
                    where = "{0} LIKE '{1}%'";
                    break;
                case "0":
                    where = "{0} NOT LIKE '{1}%'";
                    break;
            }

            if (this.Views)
            {
                where = String.Format(where, "TABLE_NAME", "View_Custom_");
            }
            else
            {
                where = String.Format(where, "ROUTINE_NAME", "Proc_Custom_");
            }
        }

        where = SqlHelperClass.AddWhereCondition(where, fltViews.GetCondition());
        
        if (this.Views)
        {
            gridViews.DataSource = TableManager.GetList(where, "TABLE_NAME, TABLE_SCHEMA, IsCustom=CASE SUBSTRING(TABLE_NAME,0,13) WHEN 'View_Custom_' THEN 1 ELSE 0 END", true);
        }
        else
        {
            where = SqlHelperClass.AddWhereCondition(where, "ROUTINE_TYPE LIKE 'PROCEDURE'");
            gridViews.DataSource = TableManager.GetList(where, "ROUTINE_NAME, ROUTINE_SCHEMA, IsCustom=CASE SUBSTRING(ROUTINE_NAME,0,13) WHEN 'Proc_Custom_' THEN 1 ELSE 0 END", false);
        }
    }


    #region "Events"

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ReloadData();
    }

    #endregion


    #region "GridView handling"

    private void gridViews_OnAction(string actionName, object actionArgument)
    {
        string objName = null;

        switch (actionName)
        {
            // Editing of the item was fired
            case "edit":
                if (!String.IsNullOrEmpty(this.EditPage))
                {
                    // Get object name
                    objName = ValidationHelper.GetString(actionArgument, null);
                    if (!String.IsNullOrEmpty(objName))
                    {
                        string query = "?objname=" + HTMLHelper.HTMLEncode(objName);
                        query += "&hash=" + QueryHelper.GetHash(query, true);
                        URLHelper.Redirect(this.EditPage + query);
                    }
                }

                break;

            // Deleting of the item was fired
            case "delete":
                objName = ValidationHelper.GetString(actionArgument, null);
                try
                {
                    TableManager.DeleteObject(objName, this.Views);

                    ReloadData();
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("sysdev.views.objectdeleted");
                }
                catch (Exception e)
                {
                    lblError.Visible = true;
                    lblError.Text = e.Message;
                }
                break;

            case "refresh":
                objName = ValidationHelper.GetString(actionArgument, null);
                if (!String.IsNullOrEmpty(objName))
                {
                    TableManager.RefreshView(objName);
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("sysdev.views.viewrefreshed");
                }
                break;

            default:
                return;
        }
    }


    object gridViews_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "iscustom":
                bool isCustom = ValidationHelper.GetBoolean(parameter, false);
                return UniGridFunctions.ColoredSpanYesNo(isCustom);

            case "delete":
                if (!SettingsKeyProvider.DevelopmentMode)
                {
                    // Disable "delete" button for system objects
                    bool delete = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["IsCustom"], false);
                    if (!delete)
                    {
                        ImageButton button = ((ImageButton)sender);
                        button.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png"));
                        button.Enabled = false;
                    }
                }
                break;

        }
        return sender;
    }

    #endregion


    /// <summary>
    /// Refresh all views.
    /// </summary>
    public void RefresViews()
    {
        if (this.Views)
        {
            DataSet ds = (DataSet)gridViews.DataSource;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TableManager.RefreshView(ValidationHelper.GetString(dr["TABLE_NAME"], null));
                }
            }
        }
    }
}
