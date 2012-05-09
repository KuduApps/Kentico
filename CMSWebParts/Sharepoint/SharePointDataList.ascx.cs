using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.SiteProvider;


public partial class CMSWebParts_SharePoint_SharePointDataList : CMSAbstractWebPart
{
    #region "SharePoint data source properties"

    /// <summary>
    /// Gets or sets value of username.
    /// </summary>
    public string Username
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Username"), SPDataSource.Username);
        }
        set
        {
            this.SetValue("Username", value);
            SPDataSource.Username = value;
        }
    }


    /// <summary>
    /// Gets or sets value of password.
    /// </summary>
    public string Password
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Password"), SPDataSource.Password);
        }
        set
        {
            this.SetValue("Password", value);
            SPDataSource.Password = value;
        }
    }


    /// <summary>
    /// Gets or sets value of SharePoint list name.
    /// </summary>
    public string ListName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ListName"), SPDataSource.ListName);
        }
        set
        {
            this.SetValue("ListName", value);
            SPDataSource.ListName = value;
        }
    }


    /// <summary>
    /// Gets or sets URL of SharePoint service (Eg. Lists.asmx, Imaging.asmx).
    /// </summary>
    public string SPServiceURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SPServiceURL"), SPDataSource.SPServiceURL);
        }
        set
        {
            this.SetValue("SPServiceURL", value);
            SPDataSource.SPServiceURL = value;
        }
    }


    /// <summary>
    /// Enables or disables showing CAML on output.
    /// </summary>
    public bool ShowReturnedCAML
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowReturnedCAML"), SPDataSource.ShowReturnedCAML);
        }
        set
        {
            this.SetValue("ShowReturnedCAML", value);
            SPDataSource.ShowReturnedCAML = value;
        }
    }


    /// <summary>
    /// Gets or set the row limit.
    /// </summary>
    public int RowLimit
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RowLimit"), SPDataSource.RowLimit);
        }
        set
        {
            this.SetValue("RowLimit", value);
            SPDataSource.RowLimit = value;
        }
    }


    /// <summary>
    /// Gets or sets query to specify which document should be retrieved (like where condition).
    /// </summary>
    public string Query
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Query"), SPDataSource.Query);
        }
        set
        {
            this.SetValue("Query", value);
            SPDataSource.Query = value;
        }
    }


    /// <summary>
    /// Gets or sets document fields which should be retrieved.
    /// </summary>
    public string ViewFields
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ViewFields"), SPDataSource.ViewFields);
        }
        set
        {
            this.SetValue("ViewFields", value);
            SPDataSource.ViewFields = value;
        }
    }


    /// <summary>
    /// Enables or disables using of classic dataset as data source for ASCX transformation.
    /// </summary>
    public bool UseClassicDataset
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseClassicDataset"), SPDataSource.UseClassicDataset);
        }
        set
        {
            this.SetValue("UseClassicDataset", value);
            SPDataSource.UseClassicDataset = value;
        }
    }


    /// <summary>
    /// Gets or sets fields which should be included in dataset
    /// Note: Only if UseClassicDataset is enabled
    /// </summary>
    public string Fields
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Fields"), SPDataSource.Fields);
        }
        set
        {
            this.SetValue("Fields", value);
            SPDataSource.Fields = value;
        }
    }


    /// <summary>
    /// Gets or sets the mode which specifies what this webpart exactly do.
    /// </summary>
    public string Mode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Mode"), SPDataSource.Mode);
        }
        set
        {
            this.SetValue("Mode", value);
            SPDataSource.Mode = value;
        }
    }


    /// <summary>
    /// Gets or sets query string key name. Presence of the key in query string indicates, 
    /// that some item should be selected. The item is determined by query string value.        
    /// </summary>
    public string ItemIDField
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemIDField"), SPDataSource.ItemIDField);
        }
        set
        {
            this.SetValue("ItemIDField", value);
            SPDataSource.ItemIDField = value;
        }
    }


    /// <summary>
    /// Gets or sets the field name which is used for selecting item. Case sensitive!
    /// </summary>
    public string SelectedItemFieldName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemFieldName"), SPDataSource.SelectedItemFieldName);
        }
        set
        {
            this.SetValue("SelectedItemFieldName", value);
            SPDataSource.SelectedItemFieldName = value;
        }
    }


    /// <summary>
    /// Gets or sets the type of field for selecting item.
    /// </summary>
    public string ItemIDFieldType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemIDFieldType"), SPDataSource.ItemIDFieldType);
        }
        set
        {
            this.SetValue("ItemIDFieldType", value);
            SPDataSource.ItemIDFieldType = value;
        }
    }

    #endregion


    #region "BasicDataList properties"

    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.BasicDataList.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.BasicDataList.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets ZeroRowsText property.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.BasicDataList.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.BasicDataList.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the count of repeat columns.
    /// </summary>
    public int RepeatColumns
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RepeatColumns"), this.BasicDataList.RepeatColumns);
        }
        set
        {
            this.SetValue("RepeatColumns", value);
            this.BasicDataList.RepeatColumns = value;
        }
    }


    /// <summary>
    /// Gets or sets whether control is displayed in a table or flow layout.
    /// </summary>
    public RepeatLayout RepeatLayout
    {
        get
        {
            return CMSDataList.GetRepeatLayout(DataHelper.GetNotEmpty(this.GetValue("RepeatLayout"), this.BasicDataList.RepeatLayout.ToString()));
        }
        set
        {
            this.SetValue("RepeatLayout", value.ToString());
            this.BasicDataList.RepeatLayout = value;
        }
    }


    /// <summary>
    /// Gets or sets whether DataList control displays vertically or horizontally.
    /// </summary>
    public RepeatDirection RepeatDirection
    {
        get
        {
            return CMSDataList.GetRepeatDirection(DataHelper.GetNotEmpty(this.GetValue("RepeatDirection"), this.BasicDataList.RepeatDirection.ToString()));
        }
        set
        {
            this.SetValue("RepeatDirection", value.ToString());
            this.BasicDataList.RepeatDirection = value;
        }
    }

    #endregion


    #region "BasicDataList template properties"

    /// <summary>
    /// Gets or sets ItemTemplate property.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), "");
        }
        set
        {
            this.SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets AlternatingItemTemplate property.
    /// </summary>
    public string AlternatingItemTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AlternatingItemTransformationName"), "");
        }
        set
        {
            this.SetValue("AlternatingItemTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets FooterTemplate property.
    /// </summary>
    public string FooterTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FooterTransformationName"), "");
        }
        set
        {
            this.SetValue("FooterTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HeaderTemplate property.
    /// </summary>
    public string HeaderTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("HeaderTransformationName"), "");
        }
        set
        {
            this.SetValue("HeaderTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets SeparatorTemplate property.
    /// </summary>
    public string SeparatorTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SeparatorTransformationName"), "");
        }
        set
        {
            this.SetValue("SeparatorTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets SelectedItemStyle property.
    /// </summary>
    public string SelectedItemTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemTransformationName"), "");
        }
        set
        {
            this.SetValue("SelectedItemTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets FooterTemplate for selected item.
    /// </summary>
    public string SelectedItemFooterTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemFooterTransformationName"), "");
        }
        set
        {
            this.SetValue("SelectedItemFooterTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HeaderTemplate for selected item.
    /// </summary>
    public string SelectedItemHeaderTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemHeaderTransformationName"), "");
        }
        set
        {
            this.SetValue("SelectedItemHeaderTransformationName", value);
        }
    }

    #endregion


    #region "UniPager properties"

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
            return ValidationHelper.GetString(GetValue("PagingMode"), pagerElem.PagerMode.ToString());
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

    #endregion


    #region "UniPager Template properties"

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


    #region "Overridden properties"

    /// <summary>
    /// Gets or sets the cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            SPDataSource.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, SPDataSource.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            SPDataSource.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            SPDataSource.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.SPDataSource.StopProcessing = value;
        }
    }

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
    protected void SetupControl()
    {
        // Set stop processing to inner controls trough property setter
        StopProcessing = StopProcessing;

        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            BasicDataList.DataBindByDefault = false;
            pagerElem.PageControl = BasicDataList.ID;

            if (!String.IsNullOrEmpty(TransformationName))
            {
                // Set SharePoint datasource properties
                SPDataSource.Username = this.Username;
                SPDataSource.Password = EncryptionHelper.DecryptData(this.Password);
                SPDataSource.ListName = this.ListName;
                SPDataSource.SPServiceURL = this.SPServiceURL;
                SPDataSource.ShowReturnedCAML = this.ShowReturnedCAML;
                SPDataSource.RowLimit = this.RowLimit;
                SPDataSource.Query = this.Query;
                SPDataSource.ViewFields = this.ViewFields;
                SPDataSource.UseClassicDataset = this.UseClassicDataset;
                SPDataSource.Fields = this.Fields;
                SPDataSource.Mode = this.Mode;
                SPDataSource.ItemIDField = this.ItemIDField;
                SPDataSource.SelectedItemFieldName = this.SelectedItemFieldName;
                SPDataSource.ItemIDFieldType = this.ItemIDFieldType;
                SPDataSource.CacheItemName = this.CacheItemName;
                SPDataSource.CacheDependencies = this.CacheDependencies;
                SPDataSource.CacheMinutes = this.CacheMinutes;


                // Classic dataset OR XSLT transformation
                if (this.UseClassicDataset)
                {
                    // Basic datalist properties
                    BasicDataList.RepeatColumns = RepeatColumns;
                    BasicDataList.RepeatLayout = RepeatLayout;
                    BasicDataList.RepeatDirection = RepeatDirection;
                    BasicDataList.HideControlForZeroRows = HideControlForZeroRows;
                    BasicDataList.ZeroRowsText = ZeroRowsText;

                    // UniPager properties                    
                    pagerElem.PageSize = PageSize;
                    pagerElem.GroupSize = GroupSize;
                    pagerElem.QueryStringKey = QueryStringKey;
                    pagerElem.DisplayFirstLastAutomatically = DisplayFirstLastAutomatically;
                    pagerElem.DisplayPreviousNextAutomatically = DisplayPreviousNextAutomatically;
                    pagerElem.HidePagerForSinglePage = HidePagerForSinglePage;

                    switch (PagingMode.ToLower())
                    {
                        case "postback":
                            pagerElem.PagerMode = UniPagerMode.PostBack;
                            break;
                        default:
                            pagerElem.PagerMode = UniPagerMode.Querystring;
                            break;
                    }


                    #region "UniPager template properties"

                    // UniPager template properties
                    if (!string.IsNullOrEmpty(PagesTemplate))
                    {
                        pagerElem.PageNumbersTemplate = CMSDataProperties.LoadTransformation(pagerElem, PagesTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(CurrentPageTemplate))
                    {
                        pagerElem.CurrentPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, CurrentPageTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(SeparatorTemplate))
                    {
                        pagerElem.PageNumbersSeparatorTemplate = CMSDataProperties.LoadTransformation(pagerElem, SeparatorTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(FirstPageTemplate))
                    {
                        pagerElem.FirstPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, FirstPageTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(LastPageTemplate))
                    {
                        pagerElem.LastPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, LastPageTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(PreviousPageTemplate))
                    {
                        pagerElem.PreviousPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, PreviousPageTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(NextPageTemplate))
                    {
                        pagerElem.NextPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, NextPageTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(PreviousGroupTemplate))
                    {
                        pagerElem.PreviousGroupTemplate = CMSDataProperties.LoadTransformation(pagerElem, PreviousGroupTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(NextGroupTemplate))
                    {
                        pagerElem.NextGroupTemplate = CMSDataProperties.LoadTransformation(pagerElem, NextGroupTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(DirectPageTemplate))
                    {
                        pagerElem.DirectPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, DirectPageTemplate, false);
                    }

                    if (!string.IsNullOrEmpty(LayoutTemplate))
                    {
                        pagerElem.LayoutTemplate = CMSDataProperties.LoadTransformation(pagerElem, LayoutTemplate, false);
                    }

                    #endregion
                }
                else
                {
                    plcDataset.Visible = false;
                    plcXSLT.Visible = true;
                }

                // Load and bind data
                BindData();
            }
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if(!string.IsNullOrEmpty(SPDataSource.ErrorMessage))
        {
            ShowError(SPDataSource.ErrorMessage) ;
            plcDataset.Visible = false;
            plcXSLT.Visible = false;
            pagerElem.Visible = false;
        }
        else
        {
            //DataSet
            if (this.UseClassicDataset)
            {
                if (HideControlForZeroRows && !BasicDataList.HasData())
                {
                    this.Visible = false;
                }
            }
            // XSLT
            else
            {
                if (!SPDataSource.HasData)
                {
                    // Hide or display zero rows message
                    if (HideControlForZeroRows)
                    {
                        this.Visible = false;
                    }
                    else
                    {
                        Label lblZeroRowsText = new Label();
                        lblZeroRowsText.Text = this.ZeroRowsText;
                        Controls.Add(lblZeroRowsText);
                    }
                }
            }
        }
        base.OnPreRender(e);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        BasicDataList.ReloadData(true);
    }


    /// <summary>
    /// Binds data to basic data list.
    /// </summary>
    protected void BindData()
    {
        // Dataset = connects datalist with data source 
        if (this.UseClassicDataset)
        {
            // Connect data list with data source                    
            BasicDataList.DataSource = SPDataSource.DataSource;

            if (!DataHelper.DataSourceIsEmpty(BasicDataList.DataSource))
            {
                // Set proper transformations
                LoadTransformations();

                BasicDataList.DataBind();
            }
        }
        // XSLT
        else
        {
            XmlNode caml = SPDataSource.DataSource as XmlNode;
            string transName = this.TransformationName;

            // If selected item is set
            if (SPDataSource.IsSelected && !String.IsNullOrEmpty(this.SelectedItemTransformationName))
            {
                transName = this.SelectedItemTransformationName;
            }

            TransformationInfo ti = TransformationInfoProvider.GetTransformation(transName);
            if ((caml != null) && (ti != null))
            {
                // Check it is XSLT transformation
                if (ti.TransformationType != TransformationTypeEnum.Xslt)
                {
                    ShowError(string.Format(GetString("sharepoint.XSL"), ti.TransformationFullName));
                    return;
                }

                try
                {
                    ltlTransformedOutput.Text = SharePointFunctions.TransformCAML(caml, ti);
                }
                catch (Exception ex)
                {
                    ShowError(string.Format(GetString("sharepoint.XSLTError") + ResHelper.Colon + " " + ex.Message ));                    
                }
            }
        }
    }


    /// <summary>
    /// Load transformations with dependence on current datasource type and datasource type.
    /// </summary>
    protected void LoadTransformations()
    {
        CMSBaseDataSource dataSource = this.SPDataSource as CMSBaseDataSource;
        if ((dataSource != null) && (dataSource.IsSelected) && (!String.IsNullOrEmpty(this.SelectedItemTransformationName)))
        {
            this.BasicDataList.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.SelectedItemTransformationName, false);

            if (!String.IsNullOrEmpty(this.SelectedItemFooterTransformationName))
            {
                this.BasicDataList.FooterTemplate = CMSDataProperties.LoadTransformation(this, this.SelectedItemFooterTransformationName, false);
            }
            else
            {
                this.BasicDataList.FooterTemplate = null;
            }

            if (!String.IsNullOrEmpty(this.SelectedItemHeaderTransformationName))
            {
                this.BasicDataList.HeaderTemplate = CMSDataProperties.LoadTransformation(this, this.SelectedItemHeaderTransformationName, false);
            }
            else
            {
                this.BasicDataList.HeaderTemplate = null;
            }
        }
        else
        {
            // Apply transformations if they exist
            if (!String.IsNullOrEmpty(this.AlternatingItemTransformationName))
            {
                this.BasicDataList.AlternatingItemTemplate = CMSDataProperties.LoadTransformation(this, this.AlternatingItemTransformationName, false);
            }

            if (!String.IsNullOrEmpty(this.FooterTransformationName))
            {
                this.BasicDataList.FooterTemplate = CMSDataProperties.LoadTransformation(this, this.FooterTransformationName, false);
            }

            if (!String.IsNullOrEmpty(this.HeaderTransformationName))
            {
                this.BasicDataList.HeaderTemplate = CMSDataProperties.LoadTransformation(this, this.HeaderTransformationName, false);
            }

            if (!String.IsNullOrEmpty(this.TransformationName))
            {
                this.BasicDataList.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.TransformationName, false);
            }

            if (!String.IsNullOrEmpty(this.SeparatorTransformationName))
            {
                this.BasicDataList.SeparatorTemplate = CMSDataProperties.LoadTransformation(this, this.SeparatorTransformationName, false);
            }
        }
    }


    /// <summary>
    /// Shows error with message.
    /// </summary>
    /// <param name="message">Message</param>
    private void ShowError(string message)
    {
        Label lblError = new Label();
        lblError.Text = message;
        lblError.CssClass = "ErrorLabel";

        Controls.Add(lblError);
    }

    #endregion
}
