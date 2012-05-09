using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.ExtendedControls;

public partial class CMSWebParts_Viewers_Basic_BasicDataList : CMSAbstractWebPart
{
    #region "Variables"

    // Base datasource instance
    private CMSBaseDataSource mDataSourceControl = null;

    // Indicates whether control was binded
    private bool binded = false;

    // BasicDatalist instance
    BasicDataList BasicDataList = new BasicDataList();

    // Indicates whether current control was added to the filter collection
    private bool mFilterControlAdded = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets name of source.
    /// </summary>
    public string DataSourceName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DataSourceName"), "");
        }
        set
        {
            this.SetValue("DataSourceName", value);
        }
    }


    /// <summary>
    /// Control with data source.
    /// </summary>
    public CMSBaseDataSource DataSourceControl
    {
        get
        {
            // Check if control is empty and load it with the data
            if (this.mDataSourceControl == null)
            {
                if (!String.IsNullOrEmpty(this.DataSourceName))
                {
                    this.mDataSourceControl = CMSControlsHelper.GetFilter(this.DataSourceName) as CMSBaseDataSource;
                }
            }

            return this.mDataSourceControl;
        }
        set
        {
            this.mDataSourceControl = value;
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


    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), true);
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
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), "");
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


    #region "Methods"

    /// <summary>
    /// On content loaded override.
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
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Set properties
            this.BasicDataList.HideControlForZeroRows = this.HideControlForZeroRows;
            this.BasicDataList.DataBindByDefault = false;
            this.BasicDataList.RepeatColumns = this.RepeatColumns;
            this.BasicDataList.RepeatDirection = this.RepeatDirection;
            this.BasicDataList.RepeatLayout = this.RepeatLayout;
            this.BasicDataList.OnPageChanged += new EventHandler<EventArgs>(BasicDataList_OnPageChanged);

            EnsureFilterControl();

            if (!String.IsNullOrEmpty(this.ZeroRowsText))
            {
                this.BasicDataList.ZeroRowsText = this.ZeroRowsText;
            }
        }
    }


    /// <summary>
    /// Ensures current control in the filters collection.
    /// </summary>
    protected void EnsureFilterControl()
    {
        if (!mFilterControlAdded)
        {
            // Add basic repeater to the filter collection
            CMSControlsHelper.SetFilter(ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID), this.BasicDataList);
            mFilterControlAdded = true;
        }
    }


    /// <summary>
    /// OnPageChanged event handler.
    /// </summary>
    void BasicDataList_OnPageChanged(object sender, EventArgs e)
    {
        // Reload data
        if (this.DataSourceControl != null)
        {
            this.BasicDataList.DataSource = this.DataSourceControl.DataSource;
            this.LoadTransformations();
            this.BasicDataList.DataBind();
            binded = true;
        }
    }


    /// <summary>
    /// Loads and setups web part.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        // Add control to the control collection
        plcBasicDataList.Controls.Add(this.BasicDataList);
        
        // Check whether postback was executed from current transformation item
        if (RequestHelper.IsPostBack())
        {
            // Indicates whether postback was fired from current control
            bool bindControl = false;

            // Check event target value and callback parameter value
            string eventTarget = ValidationHelper.GetString(this.Request.Form["__EVENTTARGET"], String.Empty);
            string callbackParam = ValidationHelper.GetString(this.Request.Form["__CALLBACKPARAM"], String.Empty);
            if (eventTarget.StartsWith(this.UniqueID) || callbackParam.StartsWith(this.UniqueID) || eventTarget.EndsWith(ContextMenu.CONTEXT_MENU_SUFFIX))
            {
                bindControl = true;
            }
            // Check whether request key contains some control assigned to current control
            else
            {
                foreach (string key in this.Request.Form.Keys)
                {
                    if ((key != null) && key.StartsWith(this.UniqueID))
                    {
                        bindControl = true;
                        break;
                    }
                }
            }

            if (bindControl)
            {
                // Reload data
                if (this.DataSourceControl != null)
                {
                    // DataSource must be accessed first before LoadTransformations() because it sets the IsSelected property in the get{} method
                    this.BasicDataList.DataSource = this.DataSourceControl.DataSource;
                    this.LoadTransformations();
                    this.BasicDataList.DataBind();
                    binded = true;
                }
            }
        }

        //Handle filter change event
        if (this.DataSourceControl != null)
        {
            this.DataSourceControl.OnFilterChanged += new ActionEventHandler(DataSourceControl_OnFilterChanged);
        }

        base.OnLoad(e);
    }


    /// <summary>
    /// Load transformations with dependence on current datasource type and datasource type.
    /// </summary>
    protected void LoadTransformations()
    {
        CMSBaseDataSource docDataSource = this.DataSourceControl as CMSBaseDataSource;
        if ((docDataSource != null) && (docDataSource.IsSelected) && (!String.IsNullOrEmpty(this.SelectedItemTransformationName)))
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
    /// OnFilter change event handler.
    /// </summary>
    void DataSourceControl_OnFilterChanged()
    {
        // Set forcibly visibility
        this.Visible = true;

        // Reload data
        if (this.DataSourceControl != null)
        {
            this.BasicDataList.DataSource = this.DataSourceControl.DataSource;
            this.LoadTransformations();
            this.BasicDataList.DataBind();
            binded = true;
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Datasource data
        object ds = null;

        // Set transformations if data source is not empty
        if (this.DataSourceControl != null)
        {
            // Get data from datasource
            ds = this.DataSourceControl.DataSource;

            // Check whether data exist
            if ((!DataHelper.DataSourceIsEmpty(ds)) && (!binded))
            {
                // Initilaize related data if provided
                if (this.DataSourceControl.RelatedData != null)
                {
                    this.RelatedData = this.DataSourceControl.RelatedData;
                }

                this.BasicDataList.DataSource = this.DataSourceControl.DataSource;
                this.LoadTransformations();
                this.BasicDataList.DataBind();
            }
        }

        base.OnPreRender(e);

        // Hide control for zero rows
        if (((this.DataSourceControl == null) || (DataHelper.DataSourceIsEmpty(ds))) && (this.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        EnsureFilterControl();

        base.ReloadData();
    }

    #endregion;
}
