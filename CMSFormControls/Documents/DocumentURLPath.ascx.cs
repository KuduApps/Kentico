using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.TreeEngine;

public partial class CMSFormControls_Documents_DocumentURLPath : FormEngineUserControl
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets whether the custom option is available for the URL path
    /// </summary>
    public bool HideCustom 
    { 
        get; 
        set; 
    }


    /// <summary>
    /// Gets or sets stylesheet ID.
    /// </summary>
    public override object Value
    {
        get
        {
            return URLPath;
        }
        set
        {
            URLPath = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// Gets or sets the automatic URL path that will be used in case custom is not set
    /// </summary>
    public string AutomaticURLPath 
    { 
        get; 
        set; 
    }


    /// <summary>
    /// Gets or sets the URL path
    /// </summary>
    public string URLPath
    {
        get
        {
            return GetURLPath();
        }
        set
        {
            SetURLPath(value);
        }
    }


    /// <summary>
    /// Plain URL path without the path prefix
    /// </summary>
    public string PlainURLPath
    {
        get
        {
            return txtUrlPath.Text.Trim();
        }
        set
        {
            txtUrlPath.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets whether the given URL is custom or not
    /// </summary>
    public bool IsCustom 
    {
        get
        {
            if (HideCustom)
            {
                return true;
            }

            return chkCustomUrl.Checked;
        }
        set
        {
            chkCustomUrl.Checked = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// PreRender event handler
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.plcMVC.Visible = radMVC.Checked;

        // Set controls according the custom setting
        txtUrlPath.Enabled = IsCustom;
        pnlType.Enabled = IsCustom;

        plcCustom.Visible = !HideCustom;
    }
    

    /// <summary>
    /// Gets the current URL path
    /// </summary>
    protected string GetURLPath()
    {
        // Process the URL path
        string urlPath = txtUrlPath.Text.Trim();
        if (String.IsNullOrEmpty(urlPath))
        {
            return null;
        }
        urlPath = "/" + urlPath.TrimStart('/'); 

        if (radMVC.Checked)
        {
            string prefix = TreePathUtils.URL_PREFIX_MVC;

            // Add default controller and action
            if (!String.IsNullOrEmpty(txtController.Text))
            {
                prefix += "[Controller=" + txtController.Text.Trim() + "]:";
            }

            if (!String.IsNullOrEmpty(txtAction.Text))
            {
                prefix += "[Action=" + txtAction.Text.Trim() + "]:";
            }

            urlPath = prefix + urlPath;
        }
        else if (radRoute.Checked)
        {
            urlPath = TreePathUtils.URL_PREFIX_ROUTE + urlPath;
        }

        return urlPath;
    }


    /// <summary>
    /// Sets the control with a new URL path
    /// </summary>
    /// <param name="urlPath">URL path to set</param>
    protected void SetURLPath(string urlPath)
    {
        radMVC.Checked = false;
        radRoute.Checked = false;
        radPage.Checked = false;

        // Process the URL path
        if (String.IsNullOrEmpty(urlPath))
        {
            radPage.Checked = true;
            txtUrlPath.Text = "";
            return;
        }

        // Parse the path
        string prefix;
        Hashtable values = new Hashtable();

        TreePathUtils.ParseUrlPath(ref urlPath, out prefix, values);

        // Examine the prefix
        if (prefix.StartsWith(TreePathUtils.URL_PREFIX_MVC, StringComparison.InvariantCultureIgnoreCase))
        {
            radMVC.Checked = true;
        }
        else if (prefix.StartsWith(TreePathUtils.URL_PREFIX_ROUTE, StringComparison.InvariantCultureIgnoreCase))
        {
            radRoute.Checked = true;
        }
        else
        {
            radPage.Checked = true;
        }

        txtUrlPath.Text = urlPath;

        txtController.Text = ValidationHelper.GetString(values["controller"], "");
        txtAction.Text = ValidationHelper.GetString(values["action"], "");
    }


    protected void chkCustomUrl_CheckedChanged(object sender, EventArgs e)
    {
        if (!IsCustom)
        {
            SetURLPath(AutomaticURLPath);
        }
    }

    #endregion
}

