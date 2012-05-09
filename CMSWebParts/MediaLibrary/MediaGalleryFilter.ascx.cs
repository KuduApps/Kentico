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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.MediaLibrary;

public partial class CMSWebParts_MediaLibrary_MediaGalleryFilter : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), String.Empty);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.libSort.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the file id querysting parameter.
    /// </summary>
    public string FileIDQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FileIDQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("FileIDQueryStringKey", value);
            this.libSort.FileIDQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the sort querysting parameter.
    /// </summary>
    public string SortQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SortQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("SortQueryStringKey", value);
            this.libSort.SortQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the filter method.
    /// </summary>
    public int FilterMethod
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("FilterMethod"), 0);
        }
        set
        {
            this.SetValue("FilterMethod", value);
            this.libSort.FilterMethod = value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        // Add sort to the filter collection
        CMSControlsHelper.SetFilter(ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ID), this.libSort);

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int fid = QueryHelper.GetInteger("fid", 0);
        if (fid > 0)
        {
            this.libSort.StopProcessing = true;
            this.libSort.Visible = false;
        }
        this.libSort.SourceFilterName = this.FilterName;
        this.libSort.FileIDQueryStringKey = this.FileIDQueryStringKey;
        this.libSort.SortQueryStringKey = this.SortQueryStringKey;
        this.libSort.FilterMethod = this.FilterMethod;
    }
}
