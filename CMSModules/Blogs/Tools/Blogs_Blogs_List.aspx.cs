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
using System.Text;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.Blogs;
using CMS.DataEngine;

[Security(Resource = "CMS.Blog", UIElements = "Blogs")]
public partial class CMSModules_Blogs_Tools_Blogs_Blogs_List : CMSBlogsPage
{
    #region "Variables"

    private CurrentUserInfo currentUser = null;
    private bool readBlogs = false;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the current user
        currentUser = CMSContext.CurrentUser;
        if (currentUser == null)
        {
            return;
        }

        // Check 'Read' permission
        if (currentUser.IsAuthorizedPerResource("cms.blog", "Read"))
        {
            readBlogs = true;
        }

        if (!RequestHelper.IsPostBack())
        {
            this.drpBlogs.Items.Add(new ListItem(GetString("general.selectall"), "##ALL##"));
            this.drpBlogs.Items.Add(new ListItem(GetString("blog.selectmyblogs"), "##MYBLOGS##"));
        }

        // No cms.blog doc. type
        if (DataClassInfoProvider.GetDataClass("cms.blog") == null)
        {
            RedirectToInformation(GetString("blog.noblogdoctype"));
        }

        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        gridBlogs.OnDataReload += gridBlogs_OnDataReload;
        gridBlogs.ZeroRowsText = GetString("general.nodatafound");
        gridBlogs.ShowActionsMenu = true;
        gridBlogs.Columns = "BlogID, BlogName, NodeID, DocumentCulture";

        // Get all possible columns to retrieve
        IDataClass nodeClass = DataClassFactory.NewDataClass("CMS.Tree");
        DocumentInfo di = new DocumentInfo();
        BlogInfo bi = new BlogInfo();
        gridBlogs.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(bi.ColumnNames.ToArray()), SqlHelperClass.MergeColumns(di.ColumnNames.ToArray())), SqlHelperClass.MergeColumns(nodeClass.ColumnNames.ToArray()));


        DataClassInfo dci = DataClassInfoProvider.GetDataClass("cms.blogpost");
        string classId = "";
        StringBuilder script = new StringBuilder();

        if (dci != null)
        {
            classId = dci.ClassID.ToString();
        }

        // Get script to redirect to new blog post page        
        script.Append("function NewPost(parentId, culture) {",
                     "  if (parentId != 0) {",
                     "     parent.parent.parent.location.href = \"", ResolveUrl("~/CMSDesk/default.aspx"), "?section=content&action=new&nodeid=\" + parentId + \"&classid=", classId, " &culture=\" + culture;",
                     "}}");

        // Generate javascript code
        ltlScript.Text = ScriptHelper.GetScript(script.ToString());
    }

    #endregion


    #region "UniGrid Events"


    protected DataSet gridBlogs_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        totalRecords = -1;

        if (drpBlogs.SelectedValue == "##MYBLOGS##")
        {
            // Get owned blogs
            return BlogHelper.GetBlogs(CMSContext.CurrentSiteName, currentUser.UserID, null, columns, completeWhere);
        }
        else
        {
            if ((currentUser.IsGlobalAdministrator) || (readBlogs))
            {
                // Get all blogs
                return BlogHelper.GetBlogs(CMSContext.CurrentSiteName, 0, null, columns, completeWhere);
            }
            else
            {
                // Get owned or managed blogs
                return BlogHelper.GetBlogs(CMSContext.CurrentSiteName, currentUser.UserID, currentUser.UserName, columns, completeWhere);
            }
        }
    }

    #endregion
}
