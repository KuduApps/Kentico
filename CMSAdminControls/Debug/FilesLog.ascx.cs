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
using CMS.IO;

public partial class CMSAdminControls_Debug_FilesLog : FilesLog
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

        if (this.Log != null)
        {
            // Get the log table
            DataTable dt = this.Log.LogTable;
            ProcessData(dt);
            DataView dv = new DataView(dt);

            if (this.OperationTypeFilter != IOOperationType.All)
            {
                dv.RowFilter = "OperationType = '" + this.OperationTypeFilter.ToString() + "'";
            }

            if (!DataHelper.DataSourceIsEmpty(dv))
            {
                this.Visible = true;

                gridStates.Columns[1].HeaderText = GetString("FilesLog.Operation");
                gridStates.Columns[2].HeaderText = GetString("FilesLog.FilePath");
                gridStates.Columns[3].HeaderText = GetString("FilesLog.OperationType");
                gridStates.Columns[4].HeaderText = GetString("General.Context");

                // Hide the operation type column if only specific operation type is selected
                if (!StorageHelper.IsExternalStorage || (this.OperationTypeFilter != IOOperationType.All))
                {
                    gridStates.Columns[3].Visible = false;
                }

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("FilesLog.Info") + "</div>";
                }

                this.MaxSize = DataHelper.GetMaximumValue<int>(dt, "FileSize");

                this.gridStates.DataSource = dv;
                this.gridStates.DataBind();
            }
        }
    }


    #region "Helper methods"

    protected int GetIndex()
    {
        return ++index;
    }

    #endregion
}
