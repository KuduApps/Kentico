using System;

using CMS.FormControls;
using CMS.ExtendedControls;

public partial class CMSFormControls_MediaSelector : FormEngineUserControl, IDialogControl
{
    #region "Properties"

  
    /// <summary>
    /// Indicates if the Clear button should be displayed.
    /// </summary>
    public bool ShowClearButton
    {
        get
        {
            return this.selectMediaElement.ShowClearButton;
        }
        set
        {
            this.selectMediaElement.ShowClearButton = value;
        }
    }


    /// <summary>
    /// Indicates if the image preview be displayed.
    /// </summary>
    public bool ShowPreview
    {
        get
        {
            return this.selectMediaElement.ShowPreview;
        }
        set
        {
            this.selectMediaElement.ShowPreview = value;
        }
    }


    /// <summary>
    /// Indicates if the path textbox should be displayed.
    /// </summary>
    public bool ShowTextBox
    {
        get
        {
            return this.selectMediaElement.ShowTextBox;
        }
        set
        {
            this.selectMediaElement.ShowTextBox = value;
        }
    }


    /// <summary>
    /// Selector value: URL of the Media.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.selectMediaElement.Value;
        }
        set
        {
            if (value != null)
            {
                this.selectMediaElement.Value = value.ToString();
            }
            else
            {
                this.selectMediaElement.Value = String.Empty;
            }
        }
    }


    ///<summary>
    /// Width of the image preview.
    ///</summary>
    public int ImageWidth
    {
        get
        {
            return this.selectMediaElement.ImageWidth;
        }
        set
        {
            this.selectMediaElement.ImageWidth = value;
        }
    }


    /// <summary>
    /// Height of the image preview.
    /// </summary>
    public int ImageHeight
    {
        get
        {
            return this.selectMediaElement.ImageHeight;
        }
        set
        {
            this.selectMediaElement.ImageHeight = value;
        }
    }


    /// <summary>
    /// Image max side size.
    /// </summary>
    public int ImageMaxSideSize
    {
        get
        {
            return this.selectMediaElement.ImageMaxSideSize;
        }
        set
        {
            this.selectMediaElement.ImageMaxSideSize = value;
        }
    }


    /// <summary>
    /// CSS class of the image preview.
    /// </summary>
    public string ImageCssClass
    {
        get
        {
            return this.selectMediaElement.ImageCssClass;
        }
        set
        {
            this.selectMediaElement.ImageCssClass = value;
        }
    }


    /// <summary>
    /// CSS style of the image preview.
    /// </summary>
    public string ImageStyle
    {
        get
        {
            return this.selectMediaElement.ImageStyle;
        }
        set
        {
            this.selectMediaElement.ImageStyle = value;
        }
    }


    /// <summary>
    /// Enable open in full size behavior.
    /// </summary>
    public bool EnableOpenInFull
    {
        get
        {
            return this.selectMediaElement.EnableOpenInFull;
        }
        set
        {
            this.selectMediaElement.EnableOpenInFull = value;
        }
    }


    /// <summary>
    /// Interface culture of the control.
    /// </summary>
    public string Culture
    {
        get
        {
            return this.selectMediaElement.Culture;
        }
        set
        {
            this.selectMediaElement.Culture = value;
        }
    }


    /// <summary>
    /// Enabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return this.selectMediaElement.Enabled;
        }
        set
        {
            this.selectMediaElement.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates if control is used in live site mode.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return this.selectMediaElement.IsLiveSite;
        }
        set
        {
            this.selectMediaElement.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Configuration of the dialog for inserting Images.
    /// </summary>
    public DialogConfiguration DialogConfig
    {
        get
        {
            return this.selectMediaElement.DialogConfig;
        }
        set
        {
            this.selectMediaElement.DialogConfig = value;
        }
    }

    #endregion
}
