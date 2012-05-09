using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.PortalControls ;
using CMS.GlobalHelper;
using CMS.ISearchEngine;

public partial class CMSWebParts_SmartSearch_SearchDialog : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the label search for text.
    /// </summary>
    public string SearchForLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchForLabel"), this.srchDialog.SearchForLabel);
        }
        set
        {
            this.SetValue("SearchForLabel", value);
            this.srchDialog.SearchForLabel = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether search mode settings should be displayed.
    /// </summary>
    public bool ShowSearchMode
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSearchMode"), this.srchDialog.ShowSearchMode);
        }
        set
        {
            this.SetValue("ShowSearchMode", value);
            this.srchDialog.ShowSearchMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the search button text.
    /// </summary>
    public string SearchButton
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchButton"), this.srchDialog.SearchButton);
        }
        set
        {
            this.SetValue("SearchButton", value);
            this.srchDialog.SearchButton = value;
        }
    }


    /// <summary>
    ///  Gets or sets the search mode.
    /// </summary>
    public SearchModeEnum SearchMode
    {
        get
        {
            return SearchHelper.GetSearchModeEnum(DataHelper.GetNotEmpty(this.GetValue("SearchMode"), SearchHelper.GetSearchModeString(this.srchDialog.SearchMode)));
        }
        set
        {
            this.SetValue("SearchMode", SearchHelper.GetSearchModeString(value) );
            this.srchDialog.SearchMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the search mode label text.
    /// </summary>
    public string SearchModeLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchModeLabel"), this.srchDialog.SearchModeLabel);
        }
        set
        {
            this.SetValue("SearchModeLabel", value);
            this.srchDialog.SearchModeLabel = value;
        }
    }


    /// <summary>
    /// Gets or sets the result webpart id.
    /// </summary>
    public string ResultWebpartID
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ResultWebpartID"), "");
        }
        set
        {
            this.SetValue("ResultWebpartID", value);
            this.srchDialog.ResultWebpartID = value;
            
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// On init event.
    /// </summary>
    /// <param name="e">Params</param>
    protected override void OnInit(EventArgs e)
    {
        srchDialog.FilterID = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
        srchDialog.LoadData();
    }


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
        // Check stop processing
        if (this.StopProcessing)
        {
            srchDialog.StopProcessing = true;
            return;
        }
        else
        {            
            // Set settings to search dialog
            srchDialog.SearchForLabel = this.SearchForLabel;
            srchDialog.SearchModeLabel = this.SearchModeLabel;
            srchDialog.SearchButton = this.SearchButton;
            srchDialog.SearchMode = this.SearchMode;
            srchDialog.ShowSearchMode = this.ShowSearchMode;
            srchDialog.ResultWebpartID = this.ResultWebpartID;           
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        base.ReloadData();
    }

    #endregion

}
