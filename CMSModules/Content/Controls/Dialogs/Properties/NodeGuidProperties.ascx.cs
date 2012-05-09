using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.IO;


public partial class CMSModules_Content_Controls_Dialogs_Properties_NodeGuidProperties : ItemProperties
{
    #region "Private variables"

    private const int DefaultMaxSideSize = 192;
    private bool mCurrentIsImage = false;
    private bool mCurrentIsMedia = false;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whether the current item is image.
    /// </summary>
    private bool CurrentIsImage
    {
        get
        {
            return this.mCurrentIsImage;
        }
        set
        {
            this.mCurrentIsImage = value;
        }
    }


    /// <summary>
    /// Indicates whether the current item is audio/video/flash.
    /// </summary>
    private bool CurrentIsMedia
    {
        get
        {
            return this.mCurrentIsMedia;
        }
        set
        {
            this.mCurrentIsMedia = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            this.tabImageGeneral.HeaderText = GetString("general.general");
            this.imagePreview.SizeToURL = false;
            this.imagePreview.BorderWidth = 0;
            this.lblEmpty.Text = this.NoSelectionText;

            LoadPreview();
        }
    }


    #region "Private methods"

    /// <summary>
    /// Configures the preview control.
    /// </summary>
    private void LoadPreview()
    {
        this.plcImagePreviewArea.Visible = (this.CurrentIsImage || this.CurrentIsMedia);
        
        if (this.CurrentIsImage)
        {
            string url = ValidationHelper.GetString(ViewState[DialogParameters.URL_URL], "");

            this.imagePreview.Tooltip = GetString("viewfile.openinfull");
            
            // Full size
            this.aFullSize.HRef = URLHelper.ResolveUrl(url);
            this.aFullSize.Target = "parent";

            if (!string.IsNullOrEmpty(url))
            {
                url = URLHelper.UpdateParameterInUrl(url, "maxsidesize", DefaultMaxSideSize.ToString());
            }

            // Add latest version requirement for live site
            if (IsLiveSite)
            {
                // Add requirement for latest version of files for current document
                string newparams = "latestforhistoryid=" + this.HistoryID;
                newparams += "&hash=" + ValidationHelper.GetHashString("h" + this.HistoryID);

                url += "&" + newparams;
            }

            // Preview
            this.imagePreview.URL = url;
        }
        else 
        {
            string ext = ValidationHelper.GetString(ViewState[DialogParameters.URL_EXT], "");

            string url = ValidationHelper.GetString(ViewState[DialogParameters.URL_URL], "");

            this.aFullSize.HRef = URLHelper.ResolveUrl(url);
            this.aFullSize.Target = "parent";

            this.imagePreview.URL = GetFileIconUrl(ext, "");
            this.imagePreview.Tooltip = GetString("general.open");
        }
    }

    #endregion


    #region "Overriden methods"

    public override void LoadSelectedItems(MediaItem item, Hashtable properties)
    {
        if (item != null)
        {
            this.HistoryID = item.HistoryID;

            // Display size selector only if required or image
            this.CurrentIsImage = (item.MediaType == MediaTypeEnum.Image);
            this.CurrentIsMedia = ((this.Config.SelectableContent == SelectableContentEnum.AllFiles) || (item.MediaType == MediaTypeEnum.AudioVideo) || (item.MediaType == MediaTypeEnum.Flash));

            properties[DialogParameters.FILE_NAME] = item.Name;
            properties[DialogParameters.FILE_SIZE] = item.Size;
            properties[DialogParameters.URL_URL] = item.Url;
            properties[DialogParameters.URL_EXT] = item.Extension;
            ViewState[DialogParameters.URL_URL] = item.Url;
            ViewState[DialogParameters.URL_EXT] = item.Extension;

            LoadProperties(properties);

            LoadPreview();
        }
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection with properties</param>
    public override void LoadItemProperties(Hashtable properties)
    {
        LoadProperties(properties);
        if (tabImageGeneral.Visible)
        {
            LoadPreview();
        }
    }

    public override void LoadProperties(Hashtable properties)
    {
        if (properties != null)
        {
            // Display the properties
            this.pnlEmpty.Visible = false;
            this.pnlTabs.CssClass = "Dialog_Tabs";

            #region "Image general tab"

            // Display size selector only if required or image
            string ext = ValidationHelper.GetString(properties[DialogParameters.URL_EXT], ""); 
            
            this.CurrentIsImage = ImageHelper.IsImage(ext);
            this.CurrentIsMedia = !this.CurrentIsImage ? (MediaHelper.IsAudioVideo(ext) || MediaHelper.IsFlash(ext)) : false;

            

            if (tabImageGeneral.Visible)
            {
                string url = ValidationHelper.GetString(properties[DialogParameters.URL_URL], "");
                string fileName = ValidationHelper.GetString(properties[DialogParameters.FILE_NAME], "");
                long fileSize = ValidationHelper.GetLong(properties[DialogParameters.FILE_SIZE], 0);

                this.lblUrlText.Text = url;
                this.lblNameText.Text = AttachmentHelper.GetFullFileName(Path.GetFileNameWithoutExtension(fileName), ext);

                if ((this.plcSizeArea.Visible = this.CurrentIsImage))
                {
                    this.lblSizeText.Text = DataHelper.GetSizeString(fileSize);
                }

                ViewState[DialogParameters.IMG_SIZETOURL] = ValidationHelper.GetBoolean(properties[DialogParameters.IMG_SIZETOURL], false);
            }

            #endregion

            #region "General items"

            ViewState[DialogParameters.URL_EXT] = (properties[DialogParameters.URL_EXT] != null ? ValidationHelper.GetString(properties[DialogParameters.URL_EXT], "") : ValidationHelper.GetString(properties[DialogParameters.IMG_EXT], ""));
            ViewState[DialogParameters.URL_URL] = ValidationHelper.GetString(properties[DialogParameters.URL_URL], "");

            this.EditorClientID = ValidationHelper.GetString(properties[DialogParameters.EDITOR_CLIENTID], "");

            #endregion
        }
    }

    /// <summary>
    /// Returns all parameters of the selected item as name – value collection.
    /// </summary>
    public override Hashtable GetItemProperties()
    {
        Hashtable retval = new Hashtable();

        #region "Image general tab"

        if (tabImageGeneral.Visible)
        {
            string url = this.lblUrlText.Text.Trim();            
            
            retval[DialogParameters.IMG_URL] = URLHelper.ResolveUrl(url);
            retval[DialogParameters.IMG_EXT] = ValidationHelper.GetString(ViewState[DialogParameters.URL_EXT], "");
        }

        #endregion

        #region "General items"

        retval[DialogParameters.URL_EXT] = ValidationHelper.GetString(ViewState[DialogParameters.URL_EXT], "");
        retval[DialogParameters.URL_URL] = URLHelper.ResolveUrl(ValidationHelper.GetString(ViewState[DialogParameters.URL_URL], ""));
        retval[DialogParameters.EDITOR_CLIENTID] = this.EditorClientID;

        #endregion

        return retval;
    }


    /// <summary>
    /// Clears the properties form.
    /// </summary>
    public override void ClearProperties(bool hideProperties)
    {
        // Hide the properties
        this.pnlEmpty.Visible = hideProperties;
        this.pnlTabs.CssClass = (hideProperties ? "DialogElementHidden" : "Dialog_Tabs");
    }

    #endregion
}
