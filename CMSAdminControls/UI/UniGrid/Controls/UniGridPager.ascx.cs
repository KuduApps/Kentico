using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_UniGrid_Controls_UniGridPager : UniGridPager
{
    #region "Properties"

    /// <summary>
    /// Indicates if pager was already loaded.
    /// </summary>
    private bool PagerLoaded
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["PagerLoaded"], false);
        }
        set
        {
            ViewState["PagerLoaded"] = value;
        }
    }


    /// <summary>
    /// UniPager control.
    /// </summary>
    public override UniPager UniPager
    {
        get
        {
            return pagerElem;
        }
    }


    /// <summary>
    /// PageSize dropdown control.
    /// </summary>
    public override DropDownList PageSizeDropdown
    {
        get
        {
            return drpPageSize;
        }
    }


    /// <summary>
    /// Default page size at first load.
    /// </summary>
    public override int DefaultPageSize
    {
        get
        {
            return base.DefaultPageSize;
        }
        set
        {
            base.DefaultPageSize = value;
            SetupControls(true);
        }
    }


    /// <summary>
    /// Gets or sets current page size.
    /// </summary>
    public override int CurrentPageSize
    {
        get
        {
            return base.CurrentPageSize;
        }
        set
        {
            base.CurrentPageSize = value;
            SetupControls(true);
        }
    }


    /// <summary>
    /// Page size values separates with comma. 
    /// Macro ##ALL## indicates that all the results will be displayed at one page.
    /// </summary>
    public override string PageSizeOptions
    {
        get
        {
            return base.PageSizeOptions;
        }
        set
        {
            base.PageSizeOptions = value;
            SetupControls(true);
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        SetupControls();

        base.OnInit(e);
    }


    protected override void OnLoad(EventArgs e)
    {
        SetupControls();

        drpPageSize.SelectedIndexChanged += new EventHandler(drpPageSize_SelectedIndexChanged);

        base.OnLoad(e);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        plcPageSize.Visible = (DisplayPager && ShowPageSize && (drpPageSize.Items.Count > 1));

        // Handle pager only if visible
        if (pagerElem.Visible)
        {
            if (UniPager.PageCount > UniPager.GroupSize)
            {
                LocalizedLabel lblPage = ControlsHelper.GetChildControl(UniPager, typeof(LocalizedLabel), "lblPage") as LocalizedLabel;
                using (Control drpPage = ControlsHelper.GetChildControl(UniPager, typeof(DropDownList), "drpPage"))
                {
                    using (Control txtPage = ControlsHelper.GetChildControl(UniPager, typeof(TextBox), "txtPage"))
                    {
                        if ((lblPage != null) && (drpPage != null) && (txtPage != null))
                        {
                            if (UniPager.PageCount > 20)
                            {
                                drpPage.Visible = false;
                                // Set labels associated control for US Section 508 validation
                                lblPage.AssociatedControlClientID = txtPage.ClientID;
                            }
                            else
                            {
                                txtPage.Visible = false;
                                // Set labels associated control for US Section 508 validation
                                lblPage.AssociatedControlClientID = drpPage.ClientID;
                            }
                        }
                    }
                }
            }
            else
            {
                // Remove direct page control if only one group of pages is  shown
                using (Control plcDirectPage = ControlsHelper.GetChildControl(UniPager, typeof(PlaceHolder), "plcDirectPage"))
                {
                    if (plcDirectPage != null)
                    {
                        plcDirectPage.Controls.Clear();
                    }
                }
            }
        }

        plcSpace.Visible = !pagerElem.Visible;

        // Hide entire control if pager and page size drodown is hidden
        if (!plcPageSize.Visible && !pagerElem.Visible)
        {
            Visible = false;
            UniPager.Enabled = false;
        }

        PagerLoaded = true;
    }


    protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        UniPager.CurrentPage = 1;
        UniPager.PageSize = ValidationHelper.GetInteger(drpPageSize.SelectedValue, -1);

        if (UniPager.PagedControl != null)
        {
            UniPager.PagedControl.ReBind();
        }
    }

    #endregion


    #region "Private methods"

    private void SetupControls()
    {
        SetupControls(false);
    }


    private void SetupControls(bool forceReload)
    {
        SetPageSize(forceReload);

        UniPager.HidePagerForSinglePage = true;
        UniPager.PagerMode = UniPagerMode.PostBack;
        UniPager.PageSize = ValidationHelper.GetInteger(drpPageSize.SelectedValue, -1);
        UniPager.DirectPageControlID = DirectPageControlID;
    }


    /// <summary>
    /// Sets page size dropdown list according to PageSize property.
    /// </summary>
    private void SetPageSize(bool forceReload)
    {
        if ((drpPageSize.Items.Count == 0) || forceReload)
        {
            string currentPagesize = CurrentPageSize.ToString();
            if (!PagerLoaded)
            {
                currentPagesize = DefaultPageSize.ToString();
            }

            drpPageSize.Items.Clear();

            string[] sizes = PageSizeOptions.Split(',');
            if (sizes.Length > 0)
            {
                List<int> sizesInt = new List<int>();

                // Indicates if contains 'Select ALL' macro
                bool containsAll = false;
                foreach (string size in sizes)
                {
                    if (size.ToUpper() == UniGrid.ALL)
                    {
                        containsAll = true;
                    }
                    else
                    {
                        sizesInt.Add(ValidationHelper.GetInteger(size.Trim(), 0));
                    }
                }

                // Add default page size if not pressents
                if ((DefaultPageSize > 0) && !sizesInt.Contains(DefaultPageSize))
                {
                    sizesInt.Add(DefaultPageSize);
                }

                // Sort list of page sizes
                sizesInt.Sort();

                ListItem item = null;

                foreach (int size in sizesInt)
                {
                    // Skip zero values
                    if (size != 0)
                    {
                        item = new ListItem(size.ToString());
                        if (item.Value == currentPagesize)
                        {
                            item.Selected = true;
                        }
                        drpPageSize.Items.Add(item);
                    }
                }

                // Add 'Select ALL' macro at the end of list
                if (containsAll)
                {
                    item = new ListItem(GetString("general.selectall"), "-1");
                    if (currentPagesize == "-1")
                    {
                        item.Selected = true;
                    }
                    drpPageSize.Items.Add(item);
                }
            }
        }
    }

    #endregion
}
