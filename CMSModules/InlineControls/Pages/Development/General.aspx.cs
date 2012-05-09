using System;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_InlineControls_Pages_Development_General : SiteManagerPage
{
    #region "Variables"

    protected int controlId;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Controls initializations
        rfvDisplayName.ErrorMessage = GetString("InlineControl_Edit.ErrorDisplayName");
        rfvName.ErrorMessage = GetString("InlineControl_Edit.ErrorName");

        plcDevelopment.Visible = SettingsKeyProvider.DevelopmentMode;

        // Get inline control ID from querystring
        controlId = QueryHelper.GetInteger("inlinecontrolid", 0);
        InlineControlInfo inlineControlObj = InlineControlInfoProvider.GetInlineControlInfo(controlId);
        EditedObject = inlineControlObj;

        // Fill editing form
        if (!RequestHelper.IsPostBack())
        {
            LoadData(inlineControlObj);

            if (QueryHelper.GetBoolean("saved", false))
            {
                ShowInformation(GetString("general.changessaved"));
            }
        }

        FileSystemDialogConfiguration config = new FileSystemDialogConfiguration
        {
            DefaultPath = "CMSInlineControls",
            AllowedExtensions = "ascx",
            ShowFolders = false
        };

        FileSystemSelector.DialogConfig = config;
        FileSystemSelector.AllowEmptyValue = false;
        FileSystemSelector.SelectedPathPrefix = "~/CMSInlineControls/";
        FileSystemSelector.ValidationError = GetString("InlineControl_Edit.ErrorFileName");
    }


    /// <summary>
    /// Load data of editing inlineControl.
    /// </summary>
    /// <param name="inlineControlObj">InlineControl object</param>
    protected void LoadData(InlineControlInfo inlineControlObj)
    {
        txtControlDescription.Text = inlineControlObj.ControlDescription;
        txtControlDisplayName.Text = inlineControlObj.ControlDisplayName;
        txtControlParameterName.Text = inlineControlObj.ControlParameterName;
        FileSystemSelector.Value = inlineControlObj.ControlFileName;
        txtControlName.Text = inlineControlObj.ControlName;
        drpModule.Value = inlineControlObj.ControlResourceID;
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = new Validator()
            .NotEmpty(txtControlDisplayName.Text.Trim(), rfvDisplayName.ErrorMessage)
            .NotEmpty(txtControlName.Text.Trim(), rfvName.ErrorMessage)
            .IsCodeName(txtControlName.Text.Trim(), GetString("general.errorcodenameinidentificatorformat"))
            .Result;

        if ((string.IsNullOrEmpty(errorMessage)) && (!FileSystemSelector.IsValid()))
        {
            errorMessage = FileSystemSelector.ValidationError;
        }

        if (string.IsNullOrEmpty(errorMessage))
        {
            // Get object
            InlineControlInfo inlineControlObj = InlineControlInfoProvider.GetInlineControlInfo(controlId);
            EditedObject = inlineControlObj;

            // Update properties
            inlineControlObj.ControlDescription = txtControlDescription.Text.Trim();
            inlineControlObj.ControlDisplayName = txtControlDisplayName.Text.Trim();
            inlineControlObj.ControlParameterName = txtControlParameterName.Text.Trim();
            inlineControlObj.ControlName = txtControlName.Text.Trim();
            inlineControlObj.ControlFileName = FileSystemSelector.Value.ToString().Trim();
            inlineControlObj.ControlResourceID = ValidationHelper.GetInteger(drpModule.Value, 0);

            try
            {
                // Save changes
                InlineControlInfoProvider.SetInlineControlInfo(inlineControlObj);
                ShowInformation(GetString("general.changessaved"));
                
                // Refresh header with display name
                ScriptHelper.RefreshTabHeader(Page, null);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message.Replace("%%name%%", txtControlName.Text));
            }
        }
        else
        {
            ShowError(errorMessage);
        }
    }

    #endregion
}