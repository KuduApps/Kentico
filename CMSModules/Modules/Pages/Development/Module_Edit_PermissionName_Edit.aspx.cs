using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_PermissionName_Edit : SiteManagerPage
{
    #region "Variables"

    PermissionNameInfo mCurrentPermission = null;
    protected int mPermissionId = 0;
    protected string mPermissionName = "";
    protected int mResourceId = 0;
    protected bool mHideBreadcrumbs = false;

    #endregion


    #region "Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        mPermissionId = QueryHelper.GetInteger("permissionid", 0);
        mResourceId = QueryHelper.GetInteger("moduleid", 0);
        mHideBreadcrumbs = QueryHelper.GetBoolean("hidebreadcrumbs", false);

        string[,] breadcrumbs = new string[2, 3];

        if (mPermissionId > 0)
        {
            mCurrentPermission = PermissionNameInfoProvider.GetPermissionNameInfo(mPermissionId);
            EditedObject = mCurrentPermission;

            if (!RequestHelper.IsPostBack())
            {
                if (mCurrentPermission != null)
                {
                    tbPermissionCodeName.Text = mCurrentPermission.PermissionName;
                    tbPermissionDisplayName.Text = mCurrentPermission.PermissionDisplayName;
                    txtPermissionDescription.Text = mCurrentPermission.PermissionDescription;
                    chkPermissionDisplayInMatrix.Checked = mCurrentPermission.PermissionDisplayInMatrix;
                    chkGlobalAdmin.Checked = mCurrentPermission.PermissionEditableByGlobalAdmin;
                }

                // shows that the permission was created or updated successfully
                if (QueryHelper.GetBoolean("saved", false))
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
            }

            if (mCurrentPermission != null)
            {
                mPermissionName = mCurrentPermission.PermissionDisplayName;
            }
        }
        else
        {
            mPermissionName = GetString("Module_Edit_PermissionName_Edit.NewPermission");
        }

        if (!mHideBreadcrumbs)
        {
            breadcrumbs[0, 0] = GetString("Administration-Module_Edit.PermissionNames");
            breadcrumbs[0, 1] = "~/CMSModules/Modules/Pages/Development/Module_Edit_PermissionNames.aspx?hidebreadcrumbs=" + (mHideBreadcrumbs ? "1" : "0") + "&moduleID=" + mResourceId;
            breadcrumbs[0, 2] = "";
            breadcrumbs[1, 0] = mPermissionName;
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";
            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        }

        this.CurrentMaster.Title.HelpTopicName = "resource_permission_new";

        rfvPermissionDisplayName.ErrorMessage = GetString("Administration-Module_Edit_PermissionName_Edit.ErrorEmptyPermissionDisplayName");
        rfvPermissionCodeName.ErrorMessage = GetString("Administration-Module_Edit_PermissionName_Edit.ErrorEmptyPermissionCodeName");
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles btnOK's OnClick event - Update or save permission info.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(tbPermissionDisplayName.Text.Trim(), GetString("Administration-Module_Edit_PermissionName_Edit.ErrorEmptyPermissionDisplayName")).NotEmpty(tbPermissionCodeName.Text.Trim(), GetString("Administration-Module_Edit_PermissionName_Edit.ErrorEmptyPermissionCodeName"))
            .IsCodeName(tbPermissionCodeName.Text.Trim(), GetString("general.invalidcodename")).Result;

        if (result == "")
        {
            int resourceId = QueryHelper.GetInteger("moduleid", 0);
            if ((resourceId <= 0) && (mCurrentPermission != null))
            {
                resourceId = mCurrentPermission.ResourceId;
            }

            string resourceName = "";

            ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(resourceId);
            if (ri != null)
            {
                resourceName = ri.ResourceName;
            }

            PermissionNameInfo pni = PermissionNameInfoProvider.GetPermissionNameInfo(tbPermissionCodeName.Text.Trim(), resourceName, null);

            if ((pni == null) || (pni.PermissionId == mPermissionId))
            {
                if (pni == null)
                {
                    pni = PermissionNameInfoProvider.GetPermissionNameInfo(mPermissionId);
                    if (pni == null)
                    {
                        pni = new PermissionNameInfo();
                    }
                }

                pni.PermissionName = tbPermissionCodeName.Text.Trim();
                pni.PermissionDisplayName = tbPermissionDisplayName.Text.Trim();
                pni.PermissionDescription = txtPermissionDescription.Text.Trim();
                pni.PermissionDisplayInMatrix = chkPermissionDisplayInMatrix.Checked;
                pni.ClassId = 0;
                pni.ResourceId = resourceId;
                pni.PermissionEditableByGlobalAdmin = chkGlobalAdmin.Checked;

                if (pni.PermissionOrder == 0)
                {
                    pni.PermissionOrder = PermissionNameInfoProvider.GetLastPermissionOrder(0, resourceId) + 1;
                }

                // Update or save permission info
                PermissionNameInfoProvider.SetPermissionInfo(pni);

                // Redirect to edit page if editing existing permission
                if (mPermissionId > 0)
                {
                    URLHelper.Redirect("Module_Edit_PermissionName_Edit.aspx?moduleID=" + pni.ResourceId + "&permissionID=" + pni.PermissionId + "&saved=1&hidebreadcrumbs=" + (mHideBreadcrumbs ? "1" : "0"));
                }
                // Redirect to whole frameset if creating new
                else
                {
                    URLHelper.Redirect(string.Format(@"Module_Edit_PermissionName_Edit_Frameset.aspx?moduleId={0}&permissionId={1}&saved=1", pni.ResourceId, pni.PermissionId));
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Administration-Module_Edit_PermissionName_Edit.UniqueCodeName");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion
}
