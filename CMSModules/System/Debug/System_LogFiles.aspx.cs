using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.IO;

public partial class CMSModules_System_Debug_System_LogFiles : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gridFiles.OnAction += new OnActionEventHandler(gridFiles_OnAction);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("Name");
        dt.Columns.Add("Size");
        ds.Tables.Add(dt);

        DirectoryInfo dir = DirectoryInfo.New(Server.MapPath("~/App_Data/"));
        FileInfo[] files = dir.GetFiles("*.log");

        // Fill the datatable with data
        foreach (FileInfo file in files)
        {
            DataRow dr = dt.NewRow();
            dr["Name"] = file.Name;
            dr["Size"] = DataHelper.GetSizeString(file.Length);
            dt.Rows.Add(dr);
        }

        // Bind the data to a grid
        gridFiles.DataSource = ds;
        gridFiles.ReloadData();
    }

    
    protected void gridFiles_OnAction(string actionName, object actionArgument)
    {
        if (actionName.ToLower() == "delete")
        {
            File.Delete(Server.MapPath("~/App_Data/") + actionArgument.ToString());
        }
    }
}
