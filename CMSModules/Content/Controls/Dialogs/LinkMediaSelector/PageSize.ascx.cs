using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;


public partial class CMSModules_Content_Controls_Dialogs_LinkMediaSelector_PageSize : CMSUserControl
{
    #region "Event & delegate"

    public delegate void OnSelectedItemChanged(int pageSize);
    public event OnSelectedItemChanged SelectedItemChanged;

    #endregion


    #region "Private variables"

    private string[] mPageSizeItems = null;

    #endregion


    #region "Public propeties"

    /// <summary>
    /// Gets or sets an array holding available size items ("10", "20", etc).
    /// </summary>
    public string[] Items 
    {
        get 
        {
            return this.mPageSizeItems;
        }        
        set 
        {
            this.mPageSizeItems = value;
        }
    }


    /// <summary>
    /// Gets or sets currently selected value.
    /// </summary>
    public string SelectedValue 
    {
        get 
        {
            return this.drpPageSize.SelectedValue;
        }
        set 
        {
            try 
            {
                this.drpPageSize.SelectedValue = value;
            }
            catch { }
        }
    }

    #endregion


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!this.StopProcessing)
        {
            SetupControl();
        }
        else
        {
            this.Visible = false;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControl()
    {
        this.lblPageSize.Text = GetString("unigrid.itemsperpage");

        if ((this.drpPageSize.Items == null) || (this.drpPageSize.Items.Count == 0))
        {
            // Add custom items
            if ((this.Items != null) && (this.Items.Length > 0))
            {
                foreach (string itemName in this.Items)
                {
                    this.drpPageSize.Items.Add(new ListItem(itemName, itemName));
                }
            }

            // Add default item
            this.drpPageSize.Items.Insert(this.drpPageSize.Items.Count, new ListItem(GetString("general.selectall"), "-1"));

            this.drpPageSize.DataBind();
        }
    }

    
    protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(this.SelectedItemChanged!=null)
        {
            // Raise event
            this.SelectedItemChanged(ValidationHelper.GetInteger(this.drpPageSize.SelectedValue, 0));
        }
    }
}
