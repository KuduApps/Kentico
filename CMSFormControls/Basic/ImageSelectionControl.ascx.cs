using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

using CMS.FormControls;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.ExtendedControls;

public partial class CMSFormControls_Basic_ImageSelectionControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return imageSelector.Enabled;
        }
        set
        {
            imageSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return imageSelector.Value;
        }
        set
        {
            imageSelector.Value = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Indicates if control is placed on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return imageSelector.IsLiveSite;
        }
        set
        {
            imageSelector.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets the value indicating if form control is in mode FileSelectionControl. If FALSE then form control is in mode ImageSelectionControl.
    /// </summary>
    public bool IsFileSelection
    {
        get
        {
            return FormHelper.IsFieldOfType(this.FieldInfo, FormFieldControlTypeEnum.FileSelectionControl);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize control
        imageSelector.ImagePathTextBox.CssClass = "EditingFormImagePathTextBox";
        imageSelector.ImagePreviewControl.CssClass = "EditingFormImagePathPreview";
        imageSelector.SelectImageButton.Attributes.Add("class", "EditingFormImagePathButton");
        imageSelector.ClearPathButton.Attributes.Add("class", "EditingFormImagePathClearButton");
        imageSelector.ShowClearButton = true;
        imageSelector.IsLiveSite = this.IsLiveSite;

        // Setup control
        if (this.GetValue("width") != null)
        {
            imageSelector.ImageWidth = ValidationHelper.GetInteger(this.GetValue("width"), 0);
        }
        if (this.GetValue("height") != null)
        {
            imageSelector.ImageHeight = ValidationHelper.GetInteger(this.GetValue("height"), 0);
        }
        if (this.GetValue("maxsidesize") != null)
        {
            imageSelector.ImageMaxSideSize = ValidationHelper.GetInteger(this.GetValue("maxsidesize"), 0);
        }

        // Apply CSS styles
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            imageSelector.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            imageSelector.CssClass = this.CssClass;
            this.CssClass = null;
        }

        // Set image selector dialog
        DialogConfiguration config = GetDialogConfiguration(this.FieldInfo);
        config.SelectableContent = SelectableContentEnum.OnlyImages;
        imageSelector.DialogConfig = config;
        imageSelector.ShowImagePreview = true;

        // Set properties speicific to File selection control
        if (this.IsFileSelection)
        {
            imageSelector.DialogConfig.SelectableContent = SelectableContentEnum.AllFiles;
            imageSelector.ShowImagePreview = false;
        }

        this.CheckFieldEmptiness = true;
    }

    #endregion
}
