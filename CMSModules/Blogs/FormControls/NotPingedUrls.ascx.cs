using System;
using System.Web;
using System.Xml;

using CMS.FormControls;
using CMS.TreeEngine;
using CMS.GlobalHelper;

/// <summary>
/// Form control for displaying and storing not pinged trackback URLs.
/// </summary>
public partial class CMSModules_Blogs_FormControls_NotPingedUrls : FormEngineUserControl
{
    private string mValue = string.Empty;
    private bool wasSaved = false;
    private bool allowSave = false;


    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Get XML from controls
            if (!wasSaved)
            {
                SaveToXML();
            }

            return mValue;
        }
        set
        {
            // Set XML for controls
            mValue = Convert.ToString(value);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.OnBeforeSave += Form_OnBeforeSave;
    }


    void Form_OnBeforeSave(object sender, EventArgs e)
    {
        this.allowSave = !this.Form.StopProcessing;
    }


    /// <summary>
    /// Checks that user input is valid.
    /// </summary>
    public override bool IsValid()
    {
        char[] delimiter = Environment.NewLine.ToCharArray();
        string[] split = txtSendTo.Text.Split(delimiter);

        // Validate all URLs
        foreach (string line in split)
        {
            string url = URLHelper.EncodeQueryString(line.Trim());

            // Check URL
            if ((url != string.Empty) && !ValidationHelper.IsURL(url))
            {
                this.ValidationError = GetString("blogs.trackbacks.notvalidurl");
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// Takes values from textbox and stores it in XML.
    /// </summary>
    protected void SaveToXML()
    {
        if (this.IsValid() && this.allowSave)
        {
            // Split URLs by lines from textbox
            char[] delimiter = Environment.NewLine.ToCharArray();
            string[] split = txtSendTo.Text.Split(delimiter);

            // Clear XML
            mValue = "<trackbacks></trackbacks>";

            // Initialize XML variables
            XmlDocument xmlNotPinged = new XmlDocument();
            xmlNotPinged.LoadXml(mValue);

            // Add URLs to be pinged
            foreach (string line in split)
            {
                // Check that each split contains some data
                string url = line.Trim();
                if (!String.IsNullOrEmpty(url))
                {
                    // Try to find value with same URL in XMLs
                    if (xmlNotPinged.DocumentElement != null)
                    {
                        url = URLHelper.GetAbsoluteUrl(url);
                        XmlNodeList urlList = xmlNotPinged.DocumentElement.SelectNodes("url[@value='" + url + "']");

                        // If URL is not present in XML with newly added URLs thed add it
                        if ((urlList == null) || (urlList.Count == 0))
                        {
                            // Add URL into not pinged XML
                            XmlElement newElem = xmlNotPinged.CreateElement("url");
                            newElem.SetAttribute("value", url);
                            newElem.SetAttribute("status", "waiting");
                            newElem.SetAttribute("error", "false");
                            newElem.SetAttribute("message", "");

                            xmlNotPinged.DocumentElement.AppendChild(newElem);
                        }
                    }
                }
            }

            this.Value = xmlNotPinged.OuterXml;
            wasSaved = true;
            txtSendTo.Text = "";
        }
    }

    #endregion
}
