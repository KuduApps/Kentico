using System;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Security : CMSBizFormPage
{
    protected int formId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        // Get form id from url
        formId = QueryHelper.GetInteger("formid", 0);

        // Control initialization
        addRoles.FormID = formId;
        addRoles.CurrentSelector.IsLiveSite = false;
        addRoles.Changed += addRoles_Changed;
        addRoles.ShowSiteFilter = false;

        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (!RequestHelper.IsPostBack())
        {
            // Load data
            if (bfi != null)
            {
                radAllUsers.Checked = (bfi.FormAccess == FormAccessEnum.AllBizFormUsers);
                radOnlyRoles.Checked = !radAllUsers.Checked;

                // Load list with allowed roles
                LoadRoles();
            }
        }
        else
        {
            if (addRoles.CurrentSelector.Enabled)
            {
                DataSet ds = BizFormInfoProvider.GetFormAuthorizedRoles(formId);
                addRoles.CurrentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "RoleID"));
            }
        }
    }


    /// <summary>
    /// On Add roles changed event.
    /// </summary>
    void addRoles_Changed(object sender, EventArgs e)
    {        
        LoadRoles();        
        pnlUpdate.Update();
    }


    protected void btnRemoveRole_Click(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }

        foreach (ListItem item in lstRoles.Items)
        {
            if (item.Selected)
            {
                // Remove role-form association from database
                BizFormInfoProvider.RemoveRoleFromForm(Convert.ToInt32(item.Value), formId);
            }
        }

        LoadRoles();
    }   


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }

        BizFormInfo form = BizFormInfoProvider.GetBizFormInfo(formId);
        if (form != null)
        {
            if (radAllUsers.Checked)
            {
                form.FormAccess = FormAccessEnum.AllBizFormUsers;                
                BizFormInfoProvider.RemoveAllRolesFromForm(formId);
                form.ClearAuthorizedRoles();
                lstRoles.Items.Clear();   
            }
            else
            {
                form.FormAccess = FormAccessEnum.OnlyAuthorizedRoles;
            }
            BizFormInfoProvider.SetBizFormInfo(form);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.Changessaved");
        }
    }


    protected void radOnlyRoles_CheckedChanged(object sender, EventArgs e)
    {
        LoadRoles();
    }


    private void SetFormControls()
    {
        // Disable add/remove for unauthorized users
        bool authorized = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm");
        addRoles.CurrentSelector.Enabled = radOnlyRoles.Checked && authorized;
        btnRemoveRole.Enabled = radOnlyRoles.Checked && authorized;
        lstRoles.Enabled = radOnlyRoles.Checked;
    }


    /// <summary>
    /// Loads list of roles authorized for form access.
    /// </summary>
    protected void LoadRoles()
    {
        DataSet ds = BizFormInfoProvider.GetFormAuthorizedRoles(formId);
        addRoles.CurrentSelector.Value = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "RoleID"));

        lstRoles.Items.Clear();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string name = Convert.ToString(dr["RoleDisplayName"]);
            if (ValidationHelper.GetInteger(dr["SiteID"], 0) == 0)
            {
                name += " " + GetString("general.global");
            }
            lstRoles.Items.Add(new ListItem(name, Convert.ToString(dr["RoleID"])));
        }
        
    }

   


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Disable add/remove for unauthorized users
        bool authorized = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm");

        addRoles.CurrentSelector.Enabled = radOnlyRoles.Checked && authorized;
        btnRemoveRole.Enabled = radOnlyRoles.Checked && authorized ;
        lstRoles.Enabled = radOnlyRoles.Checked;
    }
}
