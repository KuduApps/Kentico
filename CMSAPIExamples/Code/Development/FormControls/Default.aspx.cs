using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

[Title(Text = "Form controls", ImageUrl = "CMSModules/CMS_FormControls/module.png")]
public partial class CMSAPIExamples_Code_Development_FormControls_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Form control
        this.apiCreateFormControl.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateFormControl);
        this.apiGetAndUpdateFormControl.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateFormControl);
        this.apiGetAndBulkUpdateFormControls.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateFormControls);
        this.apiDeleteFormControl.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteFormControl);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Form control
        this.apiCreateFormControl.Run();
        this.apiGetAndUpdateFormControl.Run();
        this.apiGetAndBulkUpdateFormControls.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Form control
        this.apiDeleteFormControl.Run();
    }

    #endregion


    #region "API examples - Form control"

    /// <summary>
    /// Creates form control. Called when the "Create control" button is pressed.
    /// </summary>
    private bool CreateFormControl()
    {
        // Create new form control object
        FormUserControlInfo newControl = new FormUserControlInfo();

        // Set the properties
        newControl.UserControlDisplayName = "My new control";
        newControl.UserControlCodeName = "MyNewControl";
        newControl.UserControlFileName = "~/CMSFormControls/Basic/TextBoxControl.ascx";
        newControl.UserControlType = FormUserControlTypeEnum.Input;
        newControl.UserControlForText = true;
        newControl.UserControlForLongText = false;
        newControl.UserControlForInteger = false;
        newControl.UserControlForLongInteger = false;
        newControl.UserControlForDecimal = false;
        newControl.UserControlForDateTime = false;
        newControl.UserControlForBoolean = false;
        newControl.UserControlForFile = false;
        newControl.UserControlForDocAttachments = false;
        newControl.UserControlForGUID = false;
        newControl.UserControlForVisibility = false;
        newControl.UserControlShowInBizForms = false;
        newControl.UserControlDefaultDataType = "Text";

        // Save the form control
        FormUserControlInfoProvider.SetFormUserControlInfo(newControl);

        return true;
    }


    /// <summary>
    /// Gets and updates form control. Called when the "Get and update control" button is pressed.
    /// Expects the CreateFormControl method to be run first.
    /// </summary>
    private bool GetAndUpdateFormControl()
    {
        // Get the form control
        FormUserControlInfo updateControl = FormUserControlInfoProvider.GetFormUserControlInfo("MyNewControl");
        if (updateControl != null)
        {
            // Update the properties
            updateControl.UserControlDisplayName = updateControl.UserControlDisplayName.ToLower();

            // Save the changes
            FormUserControlInfoProvider.SetFormUserControlInfo(updateControl);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates form controls. Called when the "Get and bulk update controls" button is pressed.
    /// Expects the CreateFormControl method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateFormControls()
    {
        // Prepare the parameters
        string where = "UserControlCodeName LIKE N'MyNewControl%'";

        // Get the data
        DataSet formcontrols = FormUserControlInfoProvider.GetFormUserControls(where, null);
        if (!DataHelper.DataSourceIsEmpty(formcontrols))
        {
            // Loop through the individual items
            foreach (DataRow formcontrolsDr in formcontrols.Tables[0].Rows)
            {
                // Create object from DataRow
                FormUserControlInfo modifyControl = new FormUserControlInfo(formcontrolsDr);

                // Update the properties
                modifyControl.UserControlDisplayName = modifyControl.UserControlDisplayName.ToUpper();

                // Save the changes
                FormUserControlInfoProvider.SetFormUserControlInfo(modifyControl);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes form control. Called when the "Delete control" button is pressed.
    /// Expects the CreateFormControl method to be run first.
    /// </summary>
    private bool DeleteFormControl()
    {
        // Get the form control
        FormUserControlInfo deleteControl = FormUserControlInfoProvider.GetFormUserControlInfo("MyNewControl");

        // Delete the form control
        FormUserControlInfoProvider.DeleteFormUserControlInfo(deleteControl);

        return (deleteControl != null);
    }

    #endregion
}
