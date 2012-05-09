using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_Controls_UI_MVTVariant_Edit : CMSAdminEditControl
{
    #region "Properties"

    /// <summary>
    /// UIForm control used for editing objects properties.
    /// </summary>
    public UIForm UIFormControl
    {
        get
        {
            return this.EditForm;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.EditForm.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            EditForm.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        EditForm.OnAfterSave += new EventHandler(EditForm_OnAfterSave);
    }


    /// <summary>
    /// Handles the OnAfterSave event of the EditForm control.
    /// </summary>
    void EditForm_OnAfterSave(object sender, EventArgs e)
    {
        if (UIFormControl.EditedObject != null)
        {
            // Clear cache
            CacheHelper.TouchKey("om.mvtvariant|bytemplateid|" + (UIFormControl.EditedObject as MVTVariantInfo).MVTVariantPageTemplateID);

            // Log widget variant synchronization
            MVTVariantInfo variantInfo = (MVTVariantInfo)UIFormControl.EditedObject;
            if (variantInfo.MVTVariantDocumentID > 0)
            {
                // Log synchronization
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                TreeNode node = tree.SelectSingleDocument(variantInfo.MVTVariantDocumentID);
                DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
            }
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Validates the form data. Checks the code name format and if the code name is unique.
    /// </summary>
    public bool ValidateData()
    {
        // Check the required fields for emptiness
        bool isValid = EditForm.ValidateData();

        if (isValid)
        {
            if ((EditForm.EditedObject == null) || (EditForm.ParentObject == null))
            {
                UIFormControl.ErrorLabel.Text = GetString("general.saveerror");
                return false;
            }

            string codeName = ValidationHelper.GetString(((FormEngineUserControl)UIFormControl.FieldControls["MVTVariantName"]).Value, string.Empty);

            // Create a temporary varaint object in order to check the code name format rules and uniqueness
            MVTVariantInfo variant = new MVTVariantInfo();
            variant.MVTVariantName = codeName;
            variant.MVTVariantID = EditForm.EditedObject.GetIntegerValue("MVTVariantID", 0);
            variant.MVTVariantPageTemplateID = EditForm.ParentObject.GetIntegerValue("PageTemplateID", 0);

            // Validate the codename format
            if (!ValidationHelper.IsCodeName(codeName))
            {
                isValid = false;
                UIFormControl.ErrorLabel.Text = String.Format(GetString("general.codenamenotvalid"), codeName);
            }
            // Check if the code name already exists
            else if (!variant.CheckUniqueCodeName())
            {
                isValid = false;
                UIFormControl.ErrorLabel.Text = String.Format(GetString("general.codenamenotunique"), codeName);
            }
        }

        return isValid;
    }

    #endregion
}

