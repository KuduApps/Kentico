using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.FormControls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.FormEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Departments_FormControls_DepartmentRolesSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAllowEmpty = true;
    private NodePermissionsEnum mPermission = NodePermissionsEnum.Read;
    private bool mInheritParentPermissions = false;
    private TreeNode mEditedNode = null;
    private AclProvider mProvider = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if role selector allow empty selection.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return this.mAllowEmpty;
        }
        set
        {
            this.mAllowEmpty = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            EnsureChildControls();
            base.Enabled = value;
            this.usRoles.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets role name.
    /// </summary>
    public override object Value
    {
        get
        {
            return 0;
        }
    }


    /// <summary>
    /// Gets or sets if live iste property.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            EnsureChildControls();
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            usRoles.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates if node should inherit parent permissions or not.
    /// </summary>
    public bool InheritParentPermissions
    {
        get
        {
            return mInheritParentPermissions;
        }
        set
        {
            mInheritParentPermissions = value;
        }
    }


    /// <summary>
    /// Required document permission.
    /// </summary>
    public NodePermissionsEnum Permission
    {
        get
        {
            return mPermission;
        }
        set
        {
            mPermission = value;
        }
    }


    /// <summary>
    /// Gets document TreeNode.
    /// </summary>
    private TreeNode EditedNode
    {
        get
        {
            if (mEditedNode == null)
            {
                mEditedNode = Form.EditedObject as TreeNode;
            }
            return mEditedNode;
        }
    }


    /// <summary>
    /// Gets instance of ACLProvider.
    /// </summary>
    private AclProvider Provider
    {
        get
        {
            if (mProvider == null)
            {
                mProvider = new AclProvider(EditedNode.TreeProvider);
            }
            return mProvider;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Page_Load event.
    /// </summary>    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if node updated and role permissions should be loaded
        if (!usRoles.HasValue && (Form.Mode != FormModeEnum.Insert))
        {
            // Check if node has own ACL
            if (Provider.HasOwnACL(EditedNode))
            {
                DataSet dsRoles = Provider.GetAllowedRoles(ValidationHelper.GetInteger(EditedNode.GetValue("NodeACLID"), 0), Permission, "RoleID");
                if (!DataHelper.DataSourceIsEmpty(dsRoles))
                {
                    IList<string> roles = SqlHelperClass.GetStringValues(dsRoles.Tables[0], "RoleID");
                    usRoles.Value = TextHelper.Join(";", roles);
                }
            }
        }

        // Set after save operation
        Form.OnAfterSave += AddRoles;

        // Initialize UniSelector
        Reload(false);
    }


    /// <summary>
    /// Reloads the selector's data.
    /// </summary>
    /// <param name="forceReload">Indicates whether data should be forcibly reloaded</param>
    public void Reload(bool forceReload)
    {
        // Set allow empty
        usRoles.AllowEmpty = this.AllowEmpty;

        // Set uniselector properties
        usRoles.ReturnColumnName = "RoleID";
        usRoles.WhereCondition = "(SiteID = " + CMSContext.CurrentSiteID.ToString() + ") AND (RoleGroupID IS NULL)";

        if (forceReload)
        {
            usRoles.Reload(forceReload);
        }
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load update panel container
        if (usRoles == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// After node created, solver role permissions.
    /// </summary>
    private void AddRoles(object sender, EventArgs e)
    {
        string roleIds = ";" + usRoles.Value + ";";

        // Check if ACL should inherit from parent
        if (InheritParentPermissions)
        {
            Provider.EnsureOwnAcl(EditedNode);
        }
        else
        {
            // If node has already own ACL don't leave permissions, otherwise break inheritance
            if (!Provider.HasOwnACL(EditedNode))
            {
                Provider.BreakInherintance(EditedNode, false);
            }
        }

        int aclId = ValidationHelper.GetInteger(EditedNode.GetValue("NodeACLID"), 0);

        // Get orginal ACLItems
        DataSet ds = Provider.GetACLItems(EditedNode.NodeID, "Operator LIKE N'R%' AND ACLID = " + aclId, null, 0, "Operator, Allowed, Denied");

        // Change original values
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string op = DataHelper.GetNotEmpty(dr["Operator"], "R");
                int allowed = ValidationHelper.GetInteger(dr["Allowed"], 0);
                int denied = ValidationHelper.GetInteger(dr["Denied"], 0);
                int aclRoleId = ValidationHelper.GetInteger(op.Substring(1), 0);

                if (aclRoleId != 0)
                {
                    // Check if read permission should be set or removed
                    if (roleIds.Contains(";" + aclRoleId + ";"))
                    {
                        // Remove role from processed role and adjust permissions in database
                        roleIds = roleIds.Replace(";" + aclRoleId + ";", ";");
                        allowed |= 1;
                    }
                    else
                    {
                        allowed &= 126;
                    }
                    Provider.SetRolePermissions(EditedNode, allowed, denied, aclRoleId);
                }
            }
        }

        // Create ACL items for new roles
        if (roleIds.Trim(';') != "")
        {
            // Process rest of the roles
            string[] roles = roleIds.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string role in roles)
            {
                Provider.SetRolePermissions(EditedNode, 1, 0, int.Parse(role));
            }
        }
    }


    /// <summary>
    /// Returns true if entered data is valid. If data is invalid, it returns false and displays an error message.
    /// </summary>
    public override bool IsValid()
    {
        if ((FieldInfo != null) && !FieldInfo.AllowEmpty && String.IsNullOrEmpty(usRoles.Value.ToString()))
        {
            this.ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }
        return true;
    }

    #endregion
}
