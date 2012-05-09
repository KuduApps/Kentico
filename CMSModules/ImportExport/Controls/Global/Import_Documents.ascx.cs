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

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ImportExport_Controls_Global_Import_Documents : CMSUserControl
{
    /// <summary>
    /// If the document should be imported.
    /// </summary>
    public bool ImportDocuments
    {
        get
        {
        	 return this.radImportDoc.Checked; 
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.radImportDoc.Text = GetString("ImportSite.wzdStepDocuments.radImportDoc");
        this.radDoNotImportDoc.Text = GetString("ImportSite.wzdStepDocuments.radDoNotImportDoc");
    }
}
