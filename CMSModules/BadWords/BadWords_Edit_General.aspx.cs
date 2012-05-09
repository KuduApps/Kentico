using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_BadWords_BadWords_Edit_General : SiteManagerPage
{
    #region "Protected variables"

    protected int badWordId = 0;
    protected BadWordInfo badWordObj = null;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Get badWord id from querystring		
        badWordId = ValidationHelper.GetInteger(Request.QueryString["badwordid"], 0);
        if (badWordId == 0)
        {
            Page.MasterPageFile = "~/CMSMasterPages/UI/SimplePage.master";
        }
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Initialize resource strings
        rqfWordExpression.ErrorMessage = GetString("general.requiresvalue");

        if (badWordId > 0)
        {
            badWordObj = BadWordInfoProvider.GetBadWordInfo(badWordId);
            // Set edited object
            EditedObject = badWordObj;

            if (badWordObj != null)
            {
                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(badWordObj);

                    // Show that the badWord was created or updated successfully
                    if (QueryHelper.GetString("saved", string.Empty) == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                        // Refresh header
                        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "refreshBadwordsHeader", ScriptHelper.GetScript("parent.frames['badwordsMenu'].location = 'BadWords_Edit_Header.aspx?badwordid=" + badWordObj.WordID + "';"));
                    }
                }
                else
                {
                    // Set selected action
                    SetSelectedAction(badWordObj);
                }
            }
        }
        else
        {
            CurrentMaster.Title.TitleText = GetString("BadWords_Edit.NewItemCaption");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Badwords_Word/new.png");
            CurrentMaster.Title.HelpTopicName = "general_badwords";
            CurrentMaster.Title.HelpName = "helpTopic";

            // Initialize breadcrumbs
            string[,] pageTitleTabs = new string[2, 3];
            pageTitleTabs[0, 0] = GetString("badwords_edit.itemlistlink");
            pageTitleTabs[0, 1] = "~/CMSModules/BadWords/BadWords_List.aspx";
            pageTitleTabs[0, 2] = "_self";
            pageTitleTabs[1, 0] = GetString("badwords_list.newitemcaption");
            pageTitleTabs[1, 1] = "";
            pageTitleTabs[1, 2] = "";
            CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
            SetSelectedAction(null);
        }

        // Enable / disable actions (depending on inheritance)
        SelectBadWordActionControl.Enabled = !chkInheritAction.Checked;
        txtWordReplacement.Enabled = !chkInheritReplacement.Checked;

        // Show / hide replacement textbox depending on action
        plcReplacement.Visible = ((BadWordActionEnum)Enum.Parse(typeof(BadWordActionEnum), SelectBadWordActionControl.Value.ToString())) == BadWordActionEnum.Replace;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = new Validator().NotEmpty(txtWordExpression.Text, GetString("general.requiresvalue")).Result;

        if (errorMessage == string.Empty)
        {
            if (badWordObj == null)
            {
                badWordObj = new BadWordInfo();
            }

            // Set edited object
            EditedObject = badWordObj;

            // If bad word doesn't already exist, create new one
            if (!BadWordInfoProvider.BadWordExists(txtWordExpression.Text.Trim()) || (badWordId != 0))
            {
                badWordObj.WordExpression = txtWordExpression.Text.Trim();
                BadWordActionEnum action =
                    (BadWordActionEnum)Convert.ToInt32(SelectBadWordActionControl.Value.ToString().Trim());
                badWordObj.WordAction = !chkInheritAction.Checked ? action : 0;
                badWordObj.WordReplacement = (!chkInheritReplacement.Checked && (action == BadWordActionEnum.Replace)) ? txtWordReplacement.Text : null;
                badWordObj.WordLastModified = DateTime.Now;
                badWordObj.WordIsRegularExpression = chkIsRegular.Checked;
                badWordObj.WordMatchWholeWord = chkMatchWholeWord.Checked;

                if (badWordId == 0)
                {
                    badWordObj.WordIsGlobal = true;
                }

                BadWordInfoProvider.SetBadWordInfo(badWordObj);
                string redirectTo = badWordId == 0 ? "BadWords_Edit.aspx" : "BadWords_Edit_General.aspx";

                URLHelper.Redirect(redirectTo + "?badwordid=" + Convert.ToString(badWordObj.WordID) + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("badwords_edit.badwordexists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Load data of editing badWord.
    /// </summary>
    /// <param name="badWordObj">BadWord object</param>
    protected void LoadData(BadWordInfo badWordObj)
    {
        // Check inheritance
        chkInheritAction.Checked = (badWordObj.WordAction == 0);
        chkInheritReplacement.Checked = (badWordObj.WordReplacement == null);

        // Set selected action
        SetSelectedAction(badWordObj);

        // Load rest of values
        txtWordExpression.Text = badWordObj.WordExpression;
        chkIsRegular.Checked = badWordObj.WordIsRegularExpression;
        chkMatchWholeWord.Checked = badWordObj.WordMatchWholeWord;
    }


    /// <summary>
    /// Sets selected action.
    /// </summary>
    protected void SetSelectedAction(BadWordInfo badWordObj)
    {
        // Find postback invoker
        string invokerName = Page.Request.Params.Get("__EVENTTARGET");
        Control invokeControl = !string.IsNullOrEmpty(invokerName) ? Page.FindControl(invokerName) : null;

        // Ensure right postback actions
        if ((invokeControl == chkInheritAction) || !RequestHelper.IsPostBack())
        {
            // Deselect all items
            SelectBadWordActionControl.ReloadData();

            // Check inheritance of settings
            if (chkInheritAction != null)
            {
                // Get action
                if ((!chkInheritAction.Checked) && (badWordObj != null))
                {
                    BadWordActionEnum action = badWordObj.WordAction;
                    SelectBadWordActionControl.Value = ((int)action).ToString();
                }
                else
                {
                    SelectBadWordActionControl.Value = ((int)BadWordsHelper.BadWordsAction(CMSContext.CurrentSiteName)).ToString();
                }
            }
        }

        // Get replacement
        if ((invokeControl == chkInheritReplacement) || !RequestHelper.IsPostBack())
        {
            if (chkInheritReplacement != null)
            {
                if (!chkInheritReplacement.Checked && (badWordObj != null))
                {
                    txtWordReplacement.Text = badWordObj.WordReplacement;
                }
                else
                {
                    txtWordReplacement.Text = BadWordsHelper.BadWordsReplacement(CMSContext.CurrentSiteName);
                }
            }
        }
    }

    #endregion
}
