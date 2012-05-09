using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.Controls;

public partial class CMSWebParts_Viewers_Basic_UniPager : CMSAbstractWebPart
{
    #region "Public properties"

    #region "Pager properties"

    /// <summary>
    /// Gets or sets the ID of webpart which should be paged. Use this property only for webparts.
    /// </summary>
    public string TargetControlName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TargetControlName"), pagerElem.QueryStringKey);
        }
        set
        {
            SetValue("TargetControlName", value);
            pagerElem.FilterTypePageControl = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of records to display on a page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("PageSize"), pagerElem.PageSize);
        }
        set
        {
            SetValue("PageSize", value);
            pagerElem.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of pages displayed for current page range.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("GroupSize"), pagerElem.GroupSize);
        }
        set
        {
            SetValue("GroupSize", value);
            pagerElem.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager mode ('querystring' or 'postback').
    /// </summary>
    public string PagingMode
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagingMode"), "querystring");
        }
        set
        {
            if (value != null)
            {
                SetValue("PagingMode", value);
                switch (value.ToLower())
                {
                    case "postback": pagerElem.PagerMode = UniPagerMode.PostBack; break;
                    default: pagerElem.PagerMode = UniPagerMode.Querystring; break;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the querysting parameter.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(GetValue("QueryStringKey"), pagerElem.QueryStringKey);
        }
        set
        {
            SetValue("QueryStringKey", value);
            pagerElem.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayFirstLastAutomatically"), pagerElem.DisplayFirstLastAutomatically);
        }
        set
        {
            SetValue("DisplayFirstLastAutomatically", value);
            pagerElem.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayPreviousNextAutomatically"), pagerElem.DisplayPreviousNextAutomatically);
        }
        set
        {
            SetValue("DisplayPreviousNextAutomatically", value);
            pagerElem.DisplayPreviousNextAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HidePagerForSinglePage"), pagerElem.HidePagerForSinglePage);
        }
        set
        {
            SetValue("HidePagerForSinglePage", value);
            pagerElem.HidePagerForSinglePage = value;
        }
    }

    #endregion


    #region "Template properties"

    /// <summary>
    /// Gets or sets the pages template.
    /// </summary>
    public string PagesTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Pages"), "");
        }
        set
        {
            SetValue("Pages", value);
        }
    }


    /// <summary>
    /// Gets or sets the current page template.
    /// </summary>
    public string CurrentPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CurrentPage"), "");
        }
        set
        {
            SetValue("CurrentPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the separator template.
    /// </summary>
    public string SeparatorTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PageSeparator"), "");
        }
        set
        {
            SetValue("PageSeparator", value);
        }
    }


    /// <summary>
    /// Gets or sets the first page template.
    /// </summary>
    public string FirstPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FirstPage"), "");
        }
        set
        {
            SetValue("FirstPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the last page template.
    /// </summary>
    public string LastPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LastPage"), "");
        }
        set
        {
            SetValue("LastPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous page template.
    /// </summary>
    public string PreviousPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousPage"), "");
        }
        set
        {
            SetValue("PreviousPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the next page template.
    /// </summary>
    public string NextPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextPage"), "");
        }
        set
        {
            SetValue("NextPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous group template.
    /// </summary>
    public string PreviousGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousGroup"), "");
        }
        set
        {
            SetValue("PreviousGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the next group template.
    /// </summary>
    public string NextGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextGroup"), "");
        }
        set
        {
            SetValue("NextGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the layout template.
    /// </summary>
    public string LayoutTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagerLayout"), "");
        }
        set
        {
            SetValue("PagerLayout", value);
        }
    }


    /// <summary>
    /// Gets or sets the direct page template.
    /// </summary>
    public string DirectPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DirectPage"), "");
        }
        set
        {
            SetValue("DirectPage", value);
        }
    }

    #endregion

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    public void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // If target control is not specified do notinhg
            if (!String.IsNullOrEmpty(TargetControlName))
            {
                #region "Pager properties"

                pagerElem.FilterTypePageControl = TargetControlName;
                pagerElem.PageSize = PageSize;
                pagerElem.GroupSize = GroupSize;
                pagerElem.QueryStringKey = QueryStringKey;
                pagerElem.DisplayFirstLastAutomatically = DisplayFirstLastAutomatically;
                pagerElem.DisplayPreviousNextAutomatically = DisplayPreviousNextAutomatically;
                pagerElem.HidePagerForSinglePage = HidePagerForSinglePage;

                switch (PagingMode.ToLower())
                {
                    case "postback": pagerElem.PagerMode = UniPagerMode.PostBack; break;
                    default: pagerElem.PagerMode = UniPagerMode.Querystring; break;
                }

                #endregion


                #region "Pager tamplates"

                if (!String.IsNullOrEmpty(PagesTemplate))
                {
                    pagerElem.PageNumbersTemplate = CMSDataProperties.LoadTransformation(pagerElem, PagesTemplate, false);
                }

                if (!String.IsNullOrEmpty(CurrentPageTemplate))
                {
                    pagerElem.CurrentPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, CurrentPageTemplate, false);
                }

                if (!String.IsNullOrEmpty(SeparatorTemplate))
                {
                    pagerElem.PageNumbersSeparatorTemplate = CMSDataProperties.LoadTransformation(pagerElem, SeparatorTemplate, false);
                }

                if (!String.IsNullOrEmpty(FirstPageTemplate))
                {
                    pagerElem.FirstPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, FirstPageTemplate, false);
                }

                if (!String.IsNullOrEmpty(LastPageTemplate))
                {
                    pagerElem.LastPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, LastPageTemplate, false);
                }

                if (!String.IsNullOrEmpty(PreviousPageTemplate))
                {
                    pagerElem.PreviousPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, PreviousPageTemplate, false);
                }

                if (!String.IsNullOrEmpty(NextPageTemplate))
                {
                    pagerElem.NextPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, NextPageTemplate, false);
                }

                if (!String.IsNullOrEmpty(PreviousGroupTemplate))
                {
                    pagerElem.PreviousGroupTemplate = CMSDataProperties.LoadTransformation(pagerElem, PreviousGroupTemplate, false);
                }

                if (!String.IsNullOrEmpty(NextGroupTemplate))
                {
                    pagerElem.NextGroupTemplate = CMSDataProperties.LoadTransformation(pagerElem, NextGroupTemplate, false);
                }

                if (!String.IsNullOrEmpty(DirectPageTemplate))
                {
                    pagerElem.DirectPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, DirectPageTemplate, false);
                }

                if (!String.IsNullOrEmpty(LayoutTemplate))
                {
                    pagerElem.LayoutTemplate = CMSDataProperties.LoadTransformation(pagerElem, LayoutTemplate, false);
                }

                #endregion
            }
        }
    }


    /// <summary>
    /// OnPreRender - check visibility.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Page.SaveStateComplete += new EventHandler(Page_SaveStateComplete);
    }


    /// <summary>
    /// Checks whether current control should be visible.
    /// </summary>
    void Page_SaveStateComplete(object sender, EventArgs e)
    {
        if ((HidePagerForSinglePage && pagerElem.PageCount < 2) || (pagerElem.DataSourceItemsCount == 0))
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        pagerElem.ReloadData(true);

        base.ReloadData();
    }

    #endregion
}
