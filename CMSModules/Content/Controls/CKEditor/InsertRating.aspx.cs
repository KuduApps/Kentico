using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.IO;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.PortalEngine;

public partial class CMSModules_Content_Controls_CKEditor_InsertRating : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetBrowserClass();
        bodyElem.Attributes["class"] = mBodyClass;

        // Prepare dataset
        DataSet ds = new DataSet();
        ds.Tables.Add();
        ds.Tables[0].Columns.Add("UserControlDisplayName");
        ds.Tables[0].Columns.Add("UserControlCodeName");

        // Get file names of rating controls
        string[] fileList = Directory.GetFiles(Server.MapPath(AbstractRatingControl.GetRatingControlUrl("")), "*.ascx");
        string fileName = null;
        foreach (string file in fileList)
        {
            fileName = Path.GetFileNameWithoutExtension(file);
            // Initialize dataset
            ds.Tables[0].Rows.Add(GetString("contentrating." + fileName), fileName);
        }

        // Initialize grid
        gridForms.Columns[0].HeaderText = "<strong>" + GetString("SelectRatingDialog.FormName") + "</strong>";
        gridForms.DataSource = ds;
        gridForms.DataBind();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
        {
            // Register custom css if exists
            RegisterDialogCSSLink();
            SetLiveDialogClass();
        }
    }
}
