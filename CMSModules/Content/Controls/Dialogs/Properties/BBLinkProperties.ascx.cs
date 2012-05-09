using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_Properties_BBLinkProperties : ItemProperties
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            this.tabGeneral.HeaderText = GetString("general.general");
            this.lblEmpty.Text = this.NoSelectionText;

            this.btnHidden.Click += new EventHandler(btnHidden_Click);
            string postBackRef = ControlsHelper.GetPostBackEventReference(this.btnHidden, "");
            string postBackKeyDownRef = "var keynum;if(window.event){keynum = event.keyCode;}else if(event.which){keynum = event.which;}if(keynum == 13){" + postBackRef + "; return false;}";

            this.urlSelectElem.TextBoxLinkText.Attributes["onchange"] = postBackRef;
            this.urlSelectElem.TextBoxLinkText.Attributes["onkeydown"] = postBackKeyDownRef;
            this.urlSelectElem.TextBoxUrl.Attributes["onchange"] = postBackRef;
            this.urlSelectElem.TextBoxUrl.Attributes["onkeydown"] = postBackKeyDownRef;
            this.urlSelectElem.DropDownProtocol.Attributes["onchange"] = postBackRef;
            this.urlSelectElem.DropDownProtocol.Attributes["onkeydown"] = postBackKeyDownRef;
        }
    }


    void btnHidden_Click(object sender, EventArgs e)
    {
        SaveSession();
    }


    #region "Private methods"

    /// <summary>
    /// Save current properties into session.
    /// </summary>
    private void SaveSession()
    {
        Hashtable savedProperties = SessionHelper.GetValue("DialogSelectedParameters") as Hashtable;
        if (savedProperties == null)
        {
            savedProperties = new Hashtable();
        }
        Hashtable properties = GetItemProperties();
        foreach (DictionaryEntry entry in properties)
        {
            savedProperties[entry.Key] = entry.Value;
        }
        SessionHelper.SetValue("DialogSelectedParameters", savedProperties);
    }

    #endregion


    #region "Overriden methods"

    /// <summary>
    /// Loads given link parameters.
    /// </summary>
    /// <param name="item"></param>
    public override void LoadSelectedItems(MediaItem item, Hashtable properties)
    {
        LoadProperties(properties);
        if (item.Url != null)
        {
            string shortUrl = URLHelper.RemoveProtocol(item.Url);
            string protocol = item.Url.Substring(0, item.Url.Length - shortUrl.Length);
            if ((protocol == "http://") || (protocol == "https://") ||
                (protocol == "ftp://") || (protocol == "news://"))
            {
                this.urlSelectElem.LinkProtocol = protocol;
                this.urlSelectElem.LinkURL = shortUrl;
            }
            else
            {
                this.urlSelectElem.LinkProtocol = "other";
                this.urlSelectElem.LinkURL = item.Url;
            }
            this.urlSelectElem.LinkText = item.Name;

            if (item.MediaType == MediaTypeEnum.Flash)
            {
                this.urlSelectElem.LinkURL = URLHelper.UpdateParameterInUrl(this.urlSelectElem.LinkURL, "ext", "." + item.Extension.TrimStart('.'));
            }
        }
        SaveSession();
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection of properties</param>
    public override void LoadItemProperties(Hashtable properties)
    {
        LoadProperties(properties);
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection of properties</param>
    public override void LoadProperties(Hashtable properties)
    {
        if (properties != null)
        {
            // Display the properties
            this.pnlEmpty.Visible = false;
            this.pnlTabs.CssClass = "Dialog_Tabs";

            #region "General tab"

            string linkText = ValidationHelper.GetString(properties[DialogParameters.LINK_TEXT], "");
            string linkProtocol = ValidationHelper.GetString(properties[DialogParameters.LINK_PROTOCOL], "other");
            string linkUrl = ValidationHelper.GetString(properties[DialogParameters.LINK_URL], "");

            this.urlSelectElem.LinkText = linkText;
            this.urlSelectElem.LinkURL = linkUrl;
            this.urlSelectElem.LinkProtocol = linkProtocol;

            #endregion

            #region "General items"

            this.EditorClientID = ValidationHelper.GetString(properties[DialogParameters.EDITOR_CLIENTID], "");

            #endregion
        }

        SaveSession();
    }


    /// <summary>
    /// Returns all parameters of the selected item as name – value collection.
    /// </summary>
    public override Hashtable GetItemProperties()
    {
        Hashtable retval = new Hashtable();

        #region "General tab"

        string url = this.urlSelectElem.LinkURL.Trim();
        retval[DialogParameters.LINK_TEXT] = HttpUtility.HtmlEncode(this.urlSelectElem.LinkText);
        retval[DialogParameters.LINK_URL] = url.StartsWith("~/") ? URLHelper.ResolveUrl(url) : url;
        retval[DialogParameters.LINK_PROTOCOL] = this.urlSelectElem.LinkProtocol;

        #endregion

        #region "General items"

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

        this.urlSelectElem.LinkText = "";
        this.urlSelectElem.LinkURL = "";
        this.urlSelectElem.LinkProtocol = "other";

    }

    #endregion
}
