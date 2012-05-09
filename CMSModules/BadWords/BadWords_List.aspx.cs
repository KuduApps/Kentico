using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_BadWords_BadWords_List : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize masterpage properties
        CurrentMaster.Title.TitleText = GetString("BadWords_List.HeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Badwords_Word/object.png");
        CurrentMaster.Title.HelpTopicName = "general_badwords";
        CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("BadWords_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("BadWords_Edit_General.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Badwords_Word/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        // Initialize unigrid
        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
        UniGrid.OnBeforeDataReload += UniGrid_OnBeforeDataReload;
        UniGrid.ZeroRowsText = GetString("general.nodatafound");
        ucBadWordAction.NoSelectionText = GetString("general.allactions");

        btnShow.Click += btnShow_Click;

        if (!RequestHelper.IsPostBack())
        {
            UniGrid.OrderBy = "WordExpression ASC";
            ucBadWordAction.ReloadData();
        }
    }


    /// <summary>
    /// Button Show event handler.
    /// </summary>
    protected void btnShow_Click(object sender, EventArgs e)
    {
    }

    #endregion


    #region "UniGrid behaviour"

    protected void UniGrid_OnBeforeDataReload()
    {
        string where = null;

        // Create WHERE condition with 'Expression'
        string txt = txtExpression.Text.Trim().Replace("'", "''");
        if (!string.IsNullOrEmpty(txt))
        {
            where = "(WordExpression LIKE N'%" + txt + "%')";
        }

        // Create WHERE condition with 'Action'
        int action = ValidationHelper.GetInteger(ucBadWordAction.Value, -1);
        if (action != -1)
        {
            if (!String.IsNullOrEmpty(where))
            {
                where += " AND ";
            }

            // Select also bad words that ihnerit action from settings
            if (action == (int)BadWordsHelper.BadWordsAction(CMSContext.CurrentSiteName))
            {
                where += "(WordAction = " + action + " OR WordAction IS NULL)";
            }
            else
            {
                where += "(WordAction = " + action + ")";
            }
        }
        UniGrid.WhereCondition = where;
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        bool inherited = false;
        BadWordActionEnum action = BadWordActionEnum.None;
        string siteName = CMSContext.CurrentSiteName;

        switch (sourceName.ToLower())
        {
            case "wordaction":
                if (!string.IsNullOrEmpty(parameter.ToString()))
                {
                    action = (BadWordActionEnum)Enum.Parse(typeof(BadWordActionEnum), parameter.ToString());
                }
                else
                {
                    action = BadWordsHelper.BadWordsAction(siteName);
                    inherited = true;
                }
                // Ensure displaying text labels instead of numbers
                switch (action)
                {
                    case BadWordActionEnum.Remove:
                        parameter = GetString("general.remove");
                        break;

                    case BadWordActionEnum.Replace:
                        parameter = GetString("general.replace");
                        break;

                    case BadWordActionEnum.ReportAbuse:
                        parameter = GetString("BadWords_Edit.ReportAbuse");
                        break;

                    case BadWordActionEnum.RequestModeration:
                        parameter = GetString("BadWords_Edit.RequestModeration");
                        break;

                    case BadWordActionEnum.Deny:
                        parameter = GetString("Security.Deny");
                        break;
                }
                if (inherited)
                {
                    parameter += " " + GetString("BadWords_Edit.Inherited");
                }
                break;

            case "wordreplacement":
                // Get DataRowView
                DataRowView drv = parameter as DataRowView;
                if (drv != null)
                {
                    string replacement = drv.Row["WordReplacement"].ToString();
                    string toReturn = replacement;
                    // Set 'inherited' only if WordReplacement is empty
                    if (string.IsNullOrEmpty(replacement))
                    {
                        // Get action from cell
                        string actionText = drv.Row["WordAction"].ToString();
                        // Get action enum value
                        if (string.IsNullOrEmpty(actionText))
                        {
                            action = BadWordsHelper.BadWordsAction(siteName);
                        }
                        else
                        {
                            action = (BadWordActionEnum)Convert.ToInt32(actionText);
                        }

                        // Set replacement only if action is replace
                        if (action == BadWordActionEnum.Replace)
                        {
                            // Get inherited replacement from settings
                            if (string.IsNullOrEmpty(toReturn))
                            {
                                string inheritedSetting = SettingsKeyProvider.GetStringValue(siteName + ".CMSBadWordsReplacement");
                                toReturn += inheritedSetting + " " + GetString("BadWords_Edit.Inherited");
                            }
                        }
                        else
                        {
                            toReturn = string.Empty;
                        }
                    }
                    return HTMLHelper.HTMLEncode(toReturn);
                }
                return null;

            case "global":
                bool global = ValidationHelper.GetBoolean(parameter, false);
                return UniGridFunctions.ColoredSpanYesNo(global);
        }
        return HTMLHelper.HTMLEncode(parameter.ToString());
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("BadWords_Edit.aspx?badwordid=" + Convert.ToString(actionArgument));
                break;

            case "delete":
                BadWordInfoProvider.DeleteBadWordInfo(Convert.ToInt32(actionArgument));
                break;
        }
    }

    #endregion
}
