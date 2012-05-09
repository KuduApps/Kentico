using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Dialogs_LinkMediaSelector_NewFolder : CMSAdminControl
{
    #region "Delegates & Events"

    /// <summary>
    /// Delegate of event fired when cancel button is clicked.
    /// </summary>
    public delegate void OnCancelClickEventHandler();


    /// <summary>
    /// Delegate of event fired when folder has been deleted.
    /// </summary>
    public delegate void OnFolderChangeEventHandler(int nodeToSelect);


    /// <summary>
    /// Event raised when cancel button is clicked.
    /// </summary>
    public event OnCancelClickEventHandler CancelClick;


    /// <summary>
    /// Event raised when folder has been deleted.
    /// </summary>
    public event OnFolderChangeEventHandler OnFolderChange;

    #endregion


    #region "Private variables"

    private int mParentNodeId = 0;

    private TreeNode mParentNode = null;
    private TreeProvider mTreeProvider = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets tree provider object for current user.
    /// </summary>
    private TreeProvider TreeProvider
    {
        get
        {
            if (this.mTreeProvider == null)
            {
                this.mTreeProvider = new TreeProvider(CMSContext.CurrentUser);
            }
            return this.mTreeProvider;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the parent of the newly created node.
    /// </summary>
    public int ParentNodeID
    {
        get
        {
            return this.mParentNodeId;
        }
        set
        {
            this.mParentNodeId = value;
        }
    }


    /// <summary>
    /// Gets parent node object.
    /// </summary>
    public TreeNode ParentNode
    {
        get
        {
            if ((this.mParentNode == null) && (this.mParentNodeId > 0))
            {
                this.mParentNode = this.TreeProvider.SelectSingleNode(this.ParentNodeID);
            }
            return this.mParentNode;
        }
        set
        {
            this.mParentNode = value;
        }
    }


    #endregion


    protected override void OnLoad(EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        base.OnLoad(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!this.StopProcessing)
        {

            // Initialize control
            SetupControl();
        }
        else
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Clears form controls content.
    /// </summary>
    public override void ClearForm()
    {
        this.txtFolderName.Text = "";
    }


    #region "Event handlers"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check permissions
        string errMsg = CheckNodePermissions();

        // If validation suceeded
        if (errMsg == "")
        {
            try
            {
                // Validate form entry
                errMsg = ValidateForm();

                if (errMsg == "")
                {
                    if (this.ParentNode != null)
                    {
                        // Initialize and create new folder node
                        TreeNode newFolder = TreeNode.New("CMS.folder", this.TreeProvider);

                        // Set properties
                        newFolder.DocumentName = txtFolderName.Text.Trim();
                        newFolder.DocumentCulture = CMSContext.CurrentUser.PreferredCultureCode;
                        newFolder.SetValue("NodeOwner", CMSContext.CurrentUser.UserID);

                        // Create new folder
                        DocumentHelper.InsertDocument(newFolder, this.ParentNode, this.TreeProvider);

                        // Reload parent content
                        if (OnFolderChange != null)
                        {
                            OnFolderChange(newFolder.NodeID);
                        }
                    }
                }
                else
                {
                    // Display an error to the user
                    this.lblError.Text = errMsg;
                    this.lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Display an error to the user
                this.lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                this.lblError.Visible = true;
            }
        }
        else
        {
            // Display an error to the user
            this.lblError.Text = errMsg;
            this.lblError.Visible = true;
        }
    }


    /// <summary>
    /// Checks user's permissions for the parent node.
    /// </summary>
    private string CheckNodePermissions()
    {
        string errMsg = "";

        if (this.ParentNodeID > 0)
        {
            if (ParentNode != null)
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(ParentNode.NodeSiteID);
                if (si != null)
                {
                    // Check permissions and allowed document type
                    DataClassInfo folderClass = DataClassInfoProvider.GetDataClass("CMS.Folder");
                    if (folderClass != null)
                    {
                        // Check allowed document type
                        if (!DataClassInfoProvider.IsChildClassAllowed(ValidationHelper.GetInteger(this.ParentNode.GetValue("NodeClassID"), 0), folderClass.ClassID))
                        {
                            errMsg = GetString("dialogs.document.classnotallowed");
                        }

                        // Check document permissions
                        if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(this.ParentNode, folderClass.ClassName))
                        {
                            errMsg = GetString("dialogs.document.NotAuthorizedToCreate");
                        }
                    }
                    else
                    {
                        errMsg = GetString("dialogs.document.folderclassnotfound");
                    }
                }
                else
                {
                    errMsg = GetString("dialogs.document.sitemissing");
                }
            }
            else
            {
                errMsg = GetString("dialogs.document.parentmissing");
            }
        }
        else
        {
            errMsg = GetString("dialogs.document.parentmissing");
        }

        return errMsg;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Let the parent control now about user's action
        if (CancelClick != null)
        {
            CancelClick();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the control.
    /// </summary>
    private void SetupControl()
    {
        // Setup labels' and buttons' text
        this.btnOk.Text = GetString("general.ok");
        this.btnCancel.Text = GetString("general.cancel");

        // Register for events
        this.btnCancel.Click += new EventHandler(btnCancel_Click);
        this.rfvFolderName.ErrorMessage = GetString("media.folder.foldernameempty");
        this.ltlScript.Text = GetFocusScript();
    }


    /// <summary>
    /// Validates form entries.
    /// </summary>    
    private string ValidateForm()
    {
        return new Validator().NotEmpty(this.txtFolderName.Text, GetString("dialogs.folder.foldernameempty")).Result;
    }


    /// <summary>
    /// Returns script for focus folder name textbox.
    /// </summary>
    private string GetFocusScript()
    {
        string script = "function FocusFolderName(){\n" +
            "var txtBox = document.getElementById('" + this.txtFolderName.ClientID + "');\n" +
            "if (txtBox != null) { \n" +
            "   try {\n" +
            "       txtBox.focus();\n" +
            "   } catch (e) {\n" +
            "       setTimeout('FocusFolderName()',50);\n" +
            "   }\n" +
            "}\n" +
            "}\n";

        return ScriptHelper.GetScript(script);
    }

    #endregion
}
