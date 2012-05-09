using System;
using System.Web;
using System.Web.UI.WebControls;

using CMS.Blogs;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SettingsProvider;

public partial class CMSModules_Blogs_FormControls_SelectBlogName : FormEngineUserControl
{
    private string selectedSiteName = null;
    private bool siteNameIsAll = false;

    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return usBlogs.Value;
        }
        set
        {
            EnsureChildControls();
            usBlogs.Value = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SetFormSiteName();
        string siteName = String.IsNullOrEmpty(selectedSiteName) ? CMSContext.CurrentSiteName : selectedSiteName;

        usBlogs.WhereCondition = SqlHelperClass.AddWhereCondition(String.Empty, "SiteName = N'" + SqlHelperClass.GetSafeQueryString(siteName, false)+ "'");
        bool isAdmin = CMSContext.CurrentUser.IsGlobalAdministrator;
        string[,] items = new string[1 + Convert.ToInt32(isAdmin),2];
        items[0, 0] = GetString("blogselector.myblogs");
        items[0, 1] = "##myblogs##";
        
        if (isAdmin)
        {
            items[1, 0] = GetString("general.selectall"); 
            items[1, 1] = "##all##";
        }
        usBlogs.SpecialFields = items;

   }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        bool reloaded = false;
        this.usBlogs.Enabled = true;

        if (siteNameIsAll)
        {
            this.usBlogs.Enabled = false;

            if (!URLHelper.IsPostback())
            {
                // Load the uniselector data in order to select the ##all## value
                usBlogs.Reload(true);
                reloaded = true;
            }

            if (this.usBlogs.DropDownSingleSelect.Items.Count > 0)
            {
                this.usBlogs.DropDownSingleSelect.SelectedValue = "##all##";
            }
        }

        // Update the update panel
        if (URLHelper.IsPostback()
            && this.DependsOnAnotherField)
        {
            if (!String.IsNullOrEmpty(selectedSiteName)
                && (!reloaded))
            {
                usBlogs.Reload(true);
            }

            pnlUpdate.Update();
        }
    }


    /// <summary>
    /// Sets the site name if the SiteName field is available in the form.
    /// </summary>
    private void SetFormSiteName()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), "");
            if (siteName.Equals(string.Empty) || siteName.Equals("##all##", StringComparison.InvariantCultureIgnoreCase))
            {
                siteNameIsAll = true;
                return;
            }
            else if (siteName.Equals("##currentsite##", StringComparison.InvariantCultureIgnoreCase))
            {
                siteName = CMSContext.CurrentSiteName;
            }

            if (!String.IsNullOrEmpty(siteName))
            {
                selectedSiteName = siteName;
                return;
            }
        }

        selectedSiteName = null;
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If the selector is not defined load the update panel container
        if (usBlogs == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }
}
