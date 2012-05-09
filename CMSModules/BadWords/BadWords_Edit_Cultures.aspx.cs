using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_BadWords_BadWords_Edit_Cultures : SiteManagerPage
{
    #region "Protected variables"

    protected int badWordId = 0;
    protected BadWordInfo badWordObj = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ID of bad word
        badWordId = QueryHelper.GetInteger("badwordid", 0);

        if (badWordId > 0)
        {
            radAll.CheckedChanged += isGlobal_CheckedChanged;
            radSelected.CheckedChanged += isGlobal_CheckedChanged;
            badWordObj = BadWordInfoProvider.GetBadWordInfo(badWordId);
            // Set edited object
            EditedObject = badWordObj;

            if (badWordObj != null)
            {
                if (!RequestHelper.IsPostBack())
                {
                    // Initialize radiobuttons
                    radAll.Checked = badWordObj.WordIsGlobal;
                    radSelected.Checked = !badWordObj.WordIsGlobal;
                }
            }

            // Show / hide selector
            SetSelectorVisibility();
            
            // Get the word cultures
            if (!RequestHelper.IsPostBack() || (radSelected == ControlsHelper.GetPostBackControl(Page)))
            {
                usWordCultures.Value = GetCurrentValues();
            }
        }

        // Initialize selector properties
        usWordCultures.OnSelectionChanged += usWordCultures_OnSelectionChanged;
        usWordCultures.ButtonRemoveSelected.CssClass = "XLongButton";
        usWordCultures.ButtonAddItems.CssClass = "XLongButton";
    }


    protected void isGlobal_CheckedChanged(object sender, EventArgs e)
    {
        if (badWordObj != null)
        {
            // Set whether the word is global
            badWordObj.WordIsGlobal = radAll.Checked;
            // Save badword
            BadWordInfoProvider.SetBadWordInfo(badWordObj);
            // Display message
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
            // Show / hide selector
            SetSelectorVisibility();
        }
    }

    #endregion


    #region "Control events"

    protected void usWordCultures_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveCultures();
    }

    #endregion


    #region "Protected methods"

    protected string GetCurrentValues()
    {
        string currentValues = null;
        DataSet ds = BadWordCultureInfoProvider.GetBadWordCultures("WordID=" + badWordId, null);
        // Initialize selector
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "CultureID"));
        }
        return currentValues;
    }


    protected void SaveCultures()
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(usWordCultures.Value, null);
        string currentValues = GetCurrentValues();
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new cultures to word
                foreach (string item in newItems)
                {
                    int cultureId = ValidationHelper.GetInteger(item, 0);
                    BadWordCultureInfoProvider.RemoveBadWordFromCulture(badWordId, cultureId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new cultures to word
                foreach (string item in newItems)
                {
                    int cultureId = ValidationHelper.GetInteger(item, 0);
                    BadWordCultureInfoProvider.AddBadWordToCulture(badWordId, cultureId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }


    protected void SetSelectorVisibility()
    {
        usWordCultures.StopProcessing = !radSelected.Checked;
        usWordCultures.Visible = radSelected.Checked;
    }

    #endregion
}
