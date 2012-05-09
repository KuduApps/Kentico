using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.IO;
using CMS.SettingsProvider;
using CMS.TreeEngine;

public partial class CMSFormControls_Basic_UploadControl : FormEngineUserControl
{
    #region "Variables"

    private string mValue = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return uploader.Enabled;
        }
        set
        {
            uploader.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (String.IsNullOrEmpty(mValue) || (mValue == Guid.Empty.ToString()))
            {
                return null;
            }
            return mValue;
        }
        set
        {
            mValue = ValidationHelper.GetString(value, "");
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.UploaderInUse = true;
        uploader.OnUploadFile += new EventHandler(this.Form.RaiseOnUploadFile);
        uploader.OnDeleteFile += new EventHandler(this.Form.RaiseOnDeleteFile);

        // Apply styles
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            uploader.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            uploader.CssClass = this.CssClass;
            this.CssClass = null;
        }

        // Set image auto resize configuration
        if (this.FieldInfo != null)
        {
            int uploaderWidth = 0;
            int uploaderHeight = 0;
            int uploaderMaxSideSize = 0;
            ImageHelper.GetAutoResizeDimensions(this.FieldInfo.Settings, CMSContext.CurrentSiteName, out uploaderWidth, out uploaderHeight, out uploaderMaxSideSize);
            uploader.ResizeToWidth = uploaderWidth;
            uploader.ResizeToHeight = uploaderHeight;
            uploader.ResizeToMaxSideSize = uploaderMaxSideSize;
        }
        this.CheckFieldEmptiness = false;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // ItemValue is GUID or file name (GUID + extension) when working with bizforms
        if (mValue.LastIndexOf(".") == -1)
        {
            Guid fileGuid = ValidationHelper.GetGuid(Value, Guid.Empty);
            if (fileGuid != Guid.Empty)
            {
                // Get the file record
                AttachmentInfo ai = this.Form.GetAttachment(this, fileGuid);
                if (ai != null)
                {
                    uploader.CurrentFileName = ai.AttachmentName;
                    if (string.IsNullOrEmpty(ai.AttachmentUrl))
                    {
                        uploader.CurrentFileUrl = "~/CMSPages/GetFile.aspx?guid=" + ai.AttachmentGUID;
                    }
                    else
                    {
                        uploader.CurrentFileUrl = ai.AttachmentUrl;
                    }

                    // Register dialog script
                    ScriptHelper.RegisterDialogScript(this.Page);

                    string jsFuncName = null;
                    string baseUrl = null;
                    int width = 0;
                    int height = 0;
                    string tooltip = null;
                    if (ImageHelper.IsSupportedByImageEditor(ai.AttachmentExtension))
                    {
                        // Dialog URL for image editing
                        width = 905;
                        height = 670;
                        jsFuncName = "OpenImageEditor";
                        tooltip = ResHelper.GetString("general.editimage");
                        baseUrl = string.Format(IsLiveSite ? "{0}/CMSModules/Content/CMSDesk/Edit/ImageEditor.aspx" : "{0}/CMSFormControls/LiveSelectors/ImageEditor.aspx", URLHelper.GetFullApplicationUrl());
                    }
                    else
                    {
                        // Dialog URL for editing metadata
                        width = 500;
                        height = 350;
                        jsFuncName = "OpenMetaEditor";
                        tooltip = ResHelper.GetString("general.edit");
                        baseUrl = string.Format(IsLiveSite ? "{0}/CMSModules/Content/Attachments/CMSPages/MetaDataEditor.aspx" : "{0}/CMSModules/Content/Attachments/Dialogs/MetaDataEditor.aspx", URLHelper.GetFullApplicationUrl());
                    }

                    string script = "function " + jsFuncName + "(attachmentGuid, versionHistoryId, siteId, hash) {\n modalDialog('" + baseUrl + "?attachmentguid=' + attachmentGuid + (versionHistoryId > 0 ? '&versionhistoryid=' + versionHistoryId : '' ) + '&siteid=' + siteId + '&hash='+hash, 'imageEditorDialog', " + width + ", " + height + ");\n return false;\n}";

                    // Dialog for attachment editing
                    ScriptHelper.RegisterClientScriptBlock(this, typeof(string), jsFuncName,
                        ScriptHelper.GetScript(script));

                    if (this.Form.Mode != FormModeEnum.InsertNewCultureVersion)
                    {
                        // Create security hash
                        string parameters = "?attachmentGUID=" + ai.AttachmentGUID.ToString();
                        if (ai.AttachmentVersionHistoryID > 0)
                        {
                            parameters += "&versionhistoryid=" + ai.AttachmentVersionHistoryID;
                        }
                        parameters += "&siteid=" + ai.AttachmentSiteID;

                        string validationHash = QueryHelper.GetHash(parameters);

                        // Setup uploader's action button - it opens image editor when clicked
                        uploader.ActionButton.Attributes.Add("onclick", string.Format("{0}('{1}', {2}, {3}, '{4}'); return false;", jsFuncName, ai.AttachmentGUID, ai.AttachmentVersionHistoryID, ai.AttachmentSiteID, validationHash));

                        uploader.ActionButton.ToolTip = tooltip;
                        uploader.ShowActionButton = true;
                    }
                    else
                    {
                        uploader.ShowActionButton = false;
                    }
                }
            }
        }
        else
        {
            uploader.CurrentFileName = this.Form.GetFileNameForUploader(mValue);
            uploader.CurrentFileUrl = "~/CMSModules/BizForms/CMSPages/GetBizFormFile.aspx?filename=" + this.Form.GetGuidFileName(mValue) + "&sitename=" + this.Form.SiteName;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (this.FieldInfo != null)
        {
            uploader.ID = this.FieldInfo.Name;
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Check allow empty
        if ((this.FieldInfo != null) && !this.FieldInfo.AllowEmpty)
        {
            if (String.IsNullOrEmpty(uploader.CurrentFileName) && (uploader.PostedFile == null))
            {
                // Error empty
                ValidationError += ResHelper.GetString("BasicForm.ErrorEmptyValue");
                return false;
            }
        }

        // Test if file has allowed file-type
        if ((uploader.PostedFile != null) && (!String.IsNullOrEmpty(uploader.PostedFile.FileName.Trim())))
        {
            string customExtension = ValidationHelper.GetString(this.GetValue("extensions"), "");
            string extensions = null;

            if (String.Compare(customExtension, "custom", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                extensions = ValidationHelper.GetString(this.GetValue("allowed_extensions"), "");
            }

            string ext = Path.GetExtension(uploader.PostedFile.FileName);
            if (!IsFileTypeAllowed(ext, extensions))
            {
                // Add global allowed file extensions from Settings
                if (extensions == null)
                {
                    extensions += ";" + SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSUploadExtensions");
                }
                extensions = (extensions.TrimStart(';')).TrimEnd(';');

                ValidationError += string.Format(ResHelper.GetString("BasicForm.ErrorWrongFileType"), ext.TrimStart('.'), extensions.Replace(";", ", "));
                return false;
            }
        }
        return true;
    }

    #endregion
}
