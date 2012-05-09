using System;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.WorkflowEngine;
using CMS.CMSHelper;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSFormControls_Basic_DirectUploadControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return directUpload.Enabled;
        }
        set
        {
            directUpload.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return directUpload.Value;
        }
        set
        {
            directUpload.Value = value;
        }
    }


    /// <summary>
    /// Field info object.
    /// </summary>
    public override FormFieldInfo FieldInfo
    {
        get
        {
            return base.FieldInfo;
        }
        set
        {
            base.FieldInfo = value;
            directUpload.FieldInfo = value;
        }
    }


    /// <summary>
    /// Indicates if control is placed on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return directUpload.IsLiveSite;
        }
        set
        {
            directUpload.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize control
        directUpload.Form = Form;

        directUpload.OnUploadFile += Form.RaiseOnUploadFile;
        directUpload.OnDeleteFile += Form.RaiseOnDeleteFile;
        directUpload.CheckPermissions = false;
        if (this.FieldInfo != null)
        {
            directUpload.GUIDColumnName = this.FieldInfo.Name;
            directUpload.AllowDelete = this.FieldInfo.AllowEmpty;
        }

        // Set allowed extensions
        string extensions = ValidationHelper.GetString(GetValue("extensions"), null);
        string allowedExtensions = ValidationHelper.GetString(GetValue("allowed_extensions"), null);
        if (extensions == "custom")
        {
            directUpload.AllowedExtensions = !String.IsNullOrEmpty(allowedExtensions) ? allowedExtensions : "";
        }

        // Set image auto resize configuration
        if (this.FieldInfo != null)
        {
            int width = 0;
            int height = 0;
            int maxSideSize = 0;
            ImageHelper.GetAutoResizeDimensions(FieldInfo.Settings, CMSContext.CurrentSiteName, out width, out height, out maxSideSize);
            directUpload.ResizeToWidth = width;
            directUpload.ResizeToHeight = height;
            directUpload.ResizeToMaxSideSize = maxSideSize;
        }

        // Set control properties from parent Form
        if (Form != null)
        {
            // Get node
            TreeNode node = (TreeNode)Form.EditedObject;

            // Insert mode
            if ((Form.Mode == FormModeEnum.Insert) || (Form.Mode == FormModeEnum.InsertNewCultureVersion))
            {
                directUpload.FormGUID = Form.FormGUID;
                if ((Form.ParentObject != null) && (Form.ParentObject is TreeNode))
                {
                    directUpload.NodeParentNodeID = ((TreeNode)Form.ParentObject).NodeID;
                }
                else if (node != null)
                {
                    directUpload.NodeParentNodeID = node.NodeParentID;
                }

                if (node != null)
                {
                    directUpload.NodeClassName = node.NodeClassName;
                    directUpload.SiteName = node.NodeSiteName;

                    // Set document version history
                    if (Form.Mode == FormModeEnum.InsertNewCultureVersion)
                    {
                        // Set document version history ID
                        directUpload.VersionHistoryID = node.DocumentCheckedOutVersionHistoryID;
                        directUpload.SiteName = node.NodeSiteName;
                    }
                }
            }
            // Editing existing node
            else if (node != null)
            {
                // Set appropriate control settings
                directUpload.DocumentID = node.DocumentID;
                directUpload.NodeParentNodeID = node.NodeParentID;
                directUpload.NodeClassName = node.NodeClassName;
                directUpload.SiteName = node.NodeSiteName;

                // Get the node workflow
                WorkflowManager wm = new WorkflowManager(node.TreeProvider);
                WorkflowInfo wi = wm.GetNodeWorkflow(node);
                if (wi != null)
                {
                    // Set document version history ID
                    directUpload.VersionHistoryID = node.DocumentCheckedOutVersionHistoryID;
                }
            }
        }

        // Set style properties of control
        if (!String.IsNullOrEmpty(ControlStyle))
        {
            directUpload.Attributes.Add("style", ControlStyle);
            this.ControlStyle = null;
        }
        if (!String.IsNullOrEmpty(CssClass))
        {
            directUpload.CssClass = CssClass;
            this.CssClass = null;
        }

        CheckFieldEmptiness = false;
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (this.FieldInfo != null)
        {
            directUpload.ID = FieldInfo.Name;
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Check empty field
        if ((this.FieldInfo != null) && !FieldInfo.AllowEmpty)
        {
            string value = ValidationHelper.GetString(directUpload.Value, string.Empty).Trim();
            if ((String.IsNullOrEmpty(value)) || (ValidationHelper.GetGuid(value, Guid.Empty) == Guid.Empty))
            {
                ValidationError += ResHelper.GetString("BasicForm.ErrorEmptyValue");
                return false;
            }
        }
        return true;
    }

    #endregion
}
