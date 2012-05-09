using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.Controls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_MediaLibrary_Controls_Filters_MediaLibrarySort : CMSAbstractDataFilterControl
{
    #region "Variables"

    private static List<string> columns = new List<string>() { "filename", "filecreatedwhen", "filesize" };
    private string mFileIDQueryStringKey = null;
    private string mSortQueryStringKey = null;
    private int mFilterMethod = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Sort by value.
    /// </summary>
    public string SortBy
    {
        get
        {
            return ValidationHelper.GetString(this.ViewState["SortBy"], String.Empty);
        }
        set
        {
            this.ViewState["SortBy"] = (object)value;
        }
    }


    /// <summary>
    /// Gets or sets the file id querystring parameter.
    /// </summary>
    public string FileIDQueryStringKey
    {
        get
        {
            return mFileIDQueryStringKey;
        }
        set
        {
            mFileIDQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the sort querystring parameter.
    /// </summary>
    public string SortQueryStringKey
    {
        get
        {
            return mSortQueryStringKey;
        }
        set
        {
            mSortQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the filter method.
    /// </summary>
    public int FilterMethod
    {
        get
        {
            return mFilterMethod;
        }
        set
        {
            mFilterMethod = value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SetupControls();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.GetInteger(this.FileIDQueryStringKey, 0) > 0)
        {
            this.StopProcessing = true;
            this.Visible = false;
        }
        else
        {
            if (this.FilterMethod != 0)
            {
                this.OrderBy = this.SortBy;
            }
            else
            {
                string[] orderBy = QueryHelper.GetString(this.SortQueryStringKey, "").Split(';');
                if ((orderBy.Length == 2) && columns.Contains(orderBy[0].ToLower()) && ((orderBy[1].ToLower() == "asc") || (orderBy[1].ToLower() == "desc")))
                {
                    this.OrderBy = String.Format("{0} {1}", orderBy[0], orderBy[1]);
                }
            }
            RaiseOnFilterChanged();
        }
    }

    #region "Links handlers"

    protected void lnkName_Click(object sender, EventArgs e)
    {
        if (this.FilterMethod == 1)
        {
            if ((this.SortBy != null) && (this.SortBy.EndsWith("ASC")))
            {
                this.SortBy = "FileName DESC";
            }
            else
            {
                this.SortBy = "FileName ASC";
            }
            this.OrderBy = this.SortBy;
            RaiseOnFilterChanged();
        }
        else
        {
            if (!String.IsNullOrEmpty(this.SortQueryStringKey))
            {
                string sort = QueryHelper.GetString(this.SortQueryStringKey, String.Empty);
                if (sort.StartsWith("FileName"))
                {
                    if (sort.EndsWith("ASC"))
                    {
                        RedirectToUpdatedUrl("FileName;DESC");
                    }
                    else
                    {
                        RedirectToUpdatedUrl("FileName;ASC");
                    }
                }
                else
                {
                    RedirectToUpdatedUrl("FileName;ASC");
                }
            }
        }
    }


    protected void lnkDate_Click(object sender, EventArgs e)
    {
        if (this.FilterMethod == 1)
        {
            if ((this.SortBy != null) && (this.SortBy.EndsWith("ASC")))
            {
                this.SortBy = "FileCreatedWhen DESC";
            }
            else
            {
                this.SortBy = "FileCreatedWhen ASC";
            }
            this.OrderBy = this.SortBy;
            RaiseOnFilterChanged();
        }
        else
        {
            if (!String.IsNullOrEmpty(this.SortQueryStringKey))
            {
                string sort = QueryHelper.GetString(this.SortQueryStringKey, String.Empty);
                if (sort.StartsWith("FileCreatedWhen"))
                {
                    if (sort.EndsWith("ASC"))
                    {
                        RedirectToUpdatedUrl("FileCreatedWhen;DESC");
                    }
                    else
                    {
                        RedirectToUpdatedUrl("FileCreatedWhen;ASC");
                    }
                }
                else
                {
                    RedirectToUpdatedUrl("FileCreatedWhen;ASC");
                }
            }
        }
    }


    protected void lnkSize_Click(object sender, EventArgs e)
    {
        if (this.FilterMethod == 1)
        {
            if ((this.SortBy != null) && (this.SortBy.EndsWith("ASC")))
            {
                this.SortBy = "FileSize DESC";
            }
            else
            {
                this.SortBy = "FileSize ASC";
            }
            this.OrderBy = this.SortBy;
            RaiseOnFilterChanged();
        }
        else
        {
            if (!String.IsNullOrEmpty(this.SortQueryStringKey))
            {
                string sort = QueryHelper.GetString(this.SortQueryStringKey, String.Empty);
                if (sort.StartsWith("FileSize"))
                {
                    if (sort.EndsWith("ASC"))
                    {
                        RedirectToUpdatedUrl("FileSize;DESC");
                    }
                    else
                    {
                        RedirectToUpdatedUrl("FileSize;ASC");
                    }
                }
                else
                {
                    RedirectToUpdatedUrl("FileSize;ASC");
                }
            }
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Setup controls.
    /// </summary>
    private void SetupControls()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.lnkDate.Text = ResHelper.GetString("media.library.sort.date");
            this.lnkName.Text = ResHelper.GetString("media.library.sort.name");
            this.lnkSize.Text = ResHelper.GetString("media.library.sort.size");
        }
    }


    /// <summary>
    /// Redirect to updated url.
    /// </summary>
    /// <param name="value">Value</param>
    private void RedirectToUpdatedUrl(string value)
    {
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, this.SortQueryStringKey, value));
    }

    #endregion
}
