using System;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.WorkflowEngine;

public partial class CMSFormControls_Basic_DocumentAttachmentsControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return documentAttachments.Enabled;
        }
        set
        {
            documentAttachments.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return documentAttachments.Value;
        }
        set
        {
            documentAttachments.Value = value;
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
            documentAttachments.FieldInfo = value;
        }
    }


    /// <summary>
    /// Indicates if control is placed on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return documentAttachments.IsLiveSite;
        }
        set
        {
            documentAttachments.IsLiveSite = value;
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
        documentAttachments.Form = this.Form;
        documentAttachments.CheckPermissions = false;
        documentAttachments.OnUploadFile += new EventHandler(this.Form.RaiseOnUploadFile);
        documentAttachments.OnDeleteFile += new EventHandler(this.Form.RaiseOnDeleteFile);
        documentAttachments.AllowChangeOrder = ValidationHelper.GetBoolean(GetValue("changeorder"), true);
        documentAttachments.AllowPaging = ValidationHelper.GetBoolean(GetValue("paging"), true);
        documentAttachments.PageSize = ValidationHelper.GetString(GetValue("pagingsize"), "5,10,25,50,100,##ALL##");
        documentAttachments.DefaultPageSize = ValidationHelper.GetInteger(GetValue("defaultpagesize"), 5);

        if (this.FieldInfo != null)
        {
            documentAttachments.GroupGUID = this.FieldInfo.Guid;
        }

        // Set allowed extensions
        string extensions = ValidationHelper.GetString(GetValue("extensions"), null);
        string allowedExtensions = ValidationHelper.GetString(GetValue("allowed_extensions"), null);
        if ((extensions == "custom") && (!String.IsNullOrEmpty(allowedExtensions)))
        {
            documentAttachments.AllowedExtensions = allowedExtensions;
        }

        // Set image auto resize dimensions
        if (this.FieldInfo != null)
        {
            int attachmentsWidth = 0;
            int attachmentsHeight = 0;
            int attachmentsMaxSideSize = 0;
            ImageHelper.GetAutoResizeDimensions(FieldInfo.Settings, CMSContext.CurrentSiteName, out attachmentsWidth, out attachmentsHeight, out attachmentsMaxSideSize);
            documentAttachments.ResizeToWidth = attachmentsWidth;
            documentAttachments.ResizeToHeight = attachmentsHeight;
            documentAttachments.ResizeToMaxSideSize = attachmentsMaxSideSize;
        }

        // Get node
        CMS.TreeEngine.TreeNode node = (CMS.TreeEngine.TreeNode)this.Form.EditedObject;

        if ((Form.Mode == FormModeEnum.Insert) || (Form.Mode == FormModeEnum.InsertNewCultureVersion))
        {
            documentAttachments.FormGUID = Form.FormGUID;
            if ((Form.ParentObject != null) && (Form.ParentObject is CMS.TreeEngine.TreeNode))
            {
                documentAttachments.NodeParentNodeID = ((CMS.TreeEngine.TreeNode)Form.ParentObject).NodeID;
            }
            else if (node != null)
            {
                documentAttachments.NodeParentNodeID = node.NodeParentID;
            }

            if (node != null)
            {
                documentAttachments.NodeClassName = node.NodeClassName;
            }
        }
        else if (node != null)
        {
            // Set appropriate control settings
            documentAttachments.DocumentID = node.DocumentID;
            documentAttachments.NodeParentNodeID = node.NodeParentID;
            documentAttachments.NodeClassName = node.NodeClassName;
            documentAttachments.ActualNode = node;

            // Get the node workflow
            WorkflowManager wm = new WorkflowManager(node.TreeProvider);
            WorkflowInfo wi = wm.GetNodeWorkflow(node);
            if (wi != null)
            {
                // Ensure the document version
                documentAttachments.VersionHistoryID = node.DocumentCheckedOutVersionHistoryID;
            }
        }
        if ((node != null) && !string.IsNullOrEmpty(node.NodeSiteName))
        {
            documentAttachments.SiteName = node.NodeSiteName;
        }

        // Set control styles
        if (!String.IsNullOrEmpty(ControlStyle))
        {
            documentAttachments.Attributes.Add("style", ControlStyle);
            this.ControlStyle = null;
        }
        if (!String.IsNullOrEmpty(CssClass))
        {
            documentAttachments.CssClass = CssClass;
            this.CssClass = null;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (this.FieldInfo != null)
        {
            documentAttachments.ID = this.FieldInfo.Name;
        }
    }

    #endregion
}
