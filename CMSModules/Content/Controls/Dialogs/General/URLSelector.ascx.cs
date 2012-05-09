using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_General_URLSelector : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the link URL.
    /// </summary>
    public string LinkURL
    {
        get
        {
            return this.txtUrl.Text.Trim();
        }
        set
        {
            this.txtUrl.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the link text.
    /// </summary>
    public string LinkText
    {
        get
        {
            return this.txtLinkText.Text.Trim();
        }
        set
        {
            this.txtLinkText.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether the link text is enabled.
    /// </summary>
    public bool LinkTextEnabled
    {
        get
        {
            return this.plcLinkText.Visible;
        }
        set
        {
            this.plcLinkText.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the link protocol.
    /// </summary>
    public string LinkProtocol
    {
        get
        {
            if (this.drpProtocol.SelectedValue != "other")
            {
                return this.drpProtocol.SelectedValue;
            }
            else
            {
                return "";
            }
        }
        set
        {
            string protocol = value.TrimEnd('/') + "//";
            this.drpProtocol.ClearSelection();
            ListItem li = this.drpProtocol.Items.FindByValue(protocol);
            if (li != null)
            {
                li.Selected = true;
            }
            else
            {
                this.drpProtocol.SelectedIndex = 4;
            }
        }
    }


    /// <summary>
    /// Url textbox.
    /// </summary>
    public CMSTextBox TextBoxUrl
    {
        get
        {
            return this.txtUrl;
        }
        set
        {
            this.txtUrl = value;
        }
    }


    /// <summary>
    /// Link text textbox.
    /// </summary>
    public CMSTextBox TextBoxLinkText
    {
        get
        {
            return this.txtLinkText;
        }
        set
        {
            this.txtLinkText = value;
        }
    }


    /// <summary>
    /// Protocol dropdown list.
    /// </summary>
    public DropDownList DropDownProtocol
    {
        get
        {
            return this.drpProtocol;
        }
        set
        {
            this.drpProtocol = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!this.StopProcessing)
        {
            // Load protocol dropdown with values
            this.drpProtocol.Items.Add(new ListItem(GetString("dialogs.protocol.http"), "http://"));
            this.drpProtocol.Items.Add(new ListItem(GetString("dialogs.protocol.https"), "https://"));
            this.drpProtocol.Items.Add(new ListItem(GetString("dialogs.protocol.ftp"), "ftp://"));
            this.drpProtocol.Items.Add(new ListItem(GetString("dialogs.protocol.news"), "news://"));
            this.drpProtocol.Items.Add(new ListItem(GetString("dialogs.protocol.other"), "other"));
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {

            // Detect URL JavaScript
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "URLProtocolDetection", ScriptHelper.GetScript(
                "function detectUrl (urlId, protocolId) { \n" +
                "    var re = new RegExp('(http|https|ftp|news)://(.*)'); \n" +
                "    var urlElem = document.getElementById(urlId); \n" +
                "    var protocolElem = document.getElementById(protocolId); \n" +
                "    if ((urlElem != null) && (protocolElem != null)) { \n" +
                "        var url = urlElem.value.replace(re, '$2'); \n" +
                "        var protocol = urlElem.value.replace(re, '$1'); \n" +
                "        if (protocol != urlElem.value) { \n" +
                "            protocol += '://'; \n" +
                "            urlElem.value = url; \n" +
                "            protocolElem.value = protocol; \n" +
                "        } \n" +
                "    } \n" +
                "} \n"));

            this.txtUrl.Attributes["onchange"] = "detectUrl('" + this.txtUrl.ClientID + "','" + this.drpProtocol.ClientID + "');";

        }
    }

    #endregion
}
