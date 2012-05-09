using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.Controls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_IntranetPortal_DepartmentMembersDataSource : CMSAbstractWebPart
{
    #region "Constants"

    private const string DEPARTMENT_CLASS_NAME = "intranetportal.department";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets path to department document.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Path"), "");
        }
        set
        {
            SetValue("Path", value);
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WhereCondition"), "");
        }
        set
        {
            SetValue("WhereCondition", value);
            srcUsers.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only approved property.
    /// </summary>
    public bool SelectOnlyApproved
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectOnlyApproved"), true);
        }
        set
        {
            SetValue("SelectOnlyApproved", value);
            srcUsers.SelectOnlyApproved = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only hidden property.
    /// </summary>
    public bool SelectHidden
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectHidden"), false);
        }
        set
        {
            SetValue("SelectHidden", value);
            srcUsers.SelectHidden = value;
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OrderBy"), "");
        }
        set
        {
            SetValue("OrderBy", value);
            srcUsers.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets top N selected documents.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SelectTopN"), 0);
        }
        set
        {
            SetValue("SelectTopN", value);
            srcUsers.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FilterName"), "");
        }
        set
        {
            SetValue("FilterName", value);
            srcUsers.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SiteName"), CMSContext.CurrentSiteName);
        }
        set
        {
            SetValue("SiteName", value);
            srcUsers.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            srcUsers.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, srcUsers.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            srcUsers.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            srcUsers.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets selected columns.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Columns"), "");
        }
        set
        {
            SetValue("Columns", value);
            srcUsers.SelectedColumns = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
            srcUsers.StopProcessing = true;
        }
        else
        {
            TreeNode node = null;
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Check if path is set
            if (String.IsNullOrEmpty(Path))
            {
                TreeNode curDoc = CMSContext.CurrentDocument;
                // Check if current document is department
                if ((curDoc != null) && (curDoc.NodeClassName.ToLower() == DEPARTMENT_CLASS_NAME))
                {
                    node = CMSContext.CurrentDocument;
                }
            }
            else
            {
                // Obtain document from specified path
                node = tree.SelectSingleNode(SiteName, Path, CMSContext.PreferredCultureCode, true, DEPARTMENT_CLASS_NAME, false, false, false);
            }

            AclProvider aclProv = new AclProvider(tree);
            // If department document exists and has own ACL continue with initializing controls
            if ((node != null) && aclProv.HasOwnACL(node))
            {
                // Get users and roles with read permission for department document
                int aclId = ValidationHelper.GetInteger(node.GetValue("NodeACLID"), 0);
                DataSet dsRoles = aclProv.GetAllowedRoles(aclId, NodePermissionsEnum.Read, "RoleID");
                DataSet dsUsers = aclProv.GetAllowedUsers(aclId, NodePermissionsEnum.Read, "UserID");

                string where = null;

                // Process users dataset to where condition
                if (!DataHelper.DataSourceIsEmpty(dsUsers))
                {
                    // Get allowed users ids
                    IList<string> users = SqlHelperClass.GetStringValues(dsUsers.Tables[0], "UserID");
                    string userIds = TextHelper.Join(", ", users);

                    // Populate where condition with user condition                
                    where = SqlHelperClass.AddWhereCondition("UserID IN (" + userIds + ")", where);
                }

                // Process roles dataset to where condition
                if (!DataHelper.DataSourceIsEmpty(dsRoles))
                {
                    // Get allowed roles ids
                    IList<string> roles = SqlHelperClass.GetStringValues(dsRoles.Tables[0], "RoleID");
                    string roleIds = TextHelper.Join(", ", roles);

                    // Populate where condition with role condition
                    where = SqlHelperClass.AddWhereCondition("UserID IN (SELECT UserID FROM View_CMS_UserRole_MembershipRole_ValidOnly_Joined WHERE RoleID IN (" + roleIds + "))", where, "OR");
                }


                if (!String.IsNullOrEmpty(where))
                {
                    // Check if exist where condition and add it to current where condition
                    where = SqlHelperClass.AddWhereCondition(WhereCondition, where);

                    // Set datasource properties
                    srcUsers.WhereCondition = where;
                    srcUsers.OrderBy = OrderBy;
                    srcUsers.TopN = SelectTopN;
                    srcUsers.FilterName = ValidationHelper.GetString(GetValue("WebPartControlID"), ClientID);
                    srcUsers.SourceFilterName = FilterName;
                    srcUsers.SiteName = SiteName;
                    srcUsers.CacheItemName = CacheItemName;
                    srcUsers.CacheDependencies = CacheDependencies;
                    srcUsers.CacheMinutes = CacheMinutes;
                    srcUsers.SelectOnlyApproved = SelectOnlyApproved;
                    srcUsers.SelectHidden = SelectHidden;
                    srcUsers.SelectedColumns = Columns;
                }
                else
                {
                    srcUsers.StopProcessing = true;
                }
            }
            else
            {
                srcUsers.StopProcessing = true;
            }

        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        srcUsers.ClearCache();
    }

    #endregion

}
