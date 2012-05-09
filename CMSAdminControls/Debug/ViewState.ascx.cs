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
using System.Reflection;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;

public partial class CMSAdminControls_Debug_ViewState : ViewStateLog
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableViewState = false;
        this.Visible = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = false;

        try
        {
            if (this.Log != null)
            {
                // Get the log table
                DataTable dt = this.Log.LogTable;
                DataView dv = new DataView(dt);

                if (!DataHelper.DataSourceIsEmpty(dv))
                {
                    this.Visible = true;

                    gridStates.Columns[1].HeaderText = GetString("ViewStateLog.ID");
                    gridStates.Columns[2].HeaderText = GetString("ViewStateLog.IsDirty");
                    gridStates.Columns[3].HeaderText = GetString("ViewStateLog.ViewState");
                    gridStates.Columns[4].HeaderText = GetString("ViewStateLog.Size");

                    if (LogStyle != "")
                    {
                        this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("ViewStateLog.Info") + "</div>";
                    }

                    // Bind to the grid
                    if (this.DisplayOnlyDirty)
                    {
                        dv.RowFilter = "HasDirty = 1";
                    }

                    this.MaxSize = DataHelper.GetMaximumValue<int>(dv, "ViewStateSize");

                    this.gridStates.DataSource = dv;
                    this.gridStates.DataBind();
                }
            }
        }
        catch (Exception)
        {
            this.ltlInfo.Text = "Unable to acquire ViewState from the controls collection.";
            this.Visible = true;
        }
    }


    protected int GetIndex()
    {
        return ++index;
    }


    protected string ColourYesNo(object value)
    {
        string str = ValidationHelper.GetString(value, String.Empty);
        string[] values = str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        str = String.Empty;

        foreach (string val in values)
        {
            str += UniGridFunctions.ColoredSpanYesNoReversed(val) + "<br />";
        }

        return str;
    }
}
