using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Content_CMSDesk_Validation_Header : CMSValidationPage
{
    #region "Variables"

    private string selected = null;
    private int selectedTabIndex = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        selected = DataHelper.GetNotEmpty(QueryHelper.GetString("tab", String.Empty).ToLower(), ValidationTabCode.ToString(UIContext.ValidationTab).ToLower());

        CurrentMaster.Tabs.OnTabCreated += tabElem_OnTabCreated;
        CurrentMaster.Tabs.ModuleName = "CMS.Content";
        CurrentMaster.Tabs.ElementName = "Validation";
        CurrentMaster.Tabs.UrlTarget = "validationedit";
        CurrentMaster.SetRTL();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        CurrentMaster.Tabs.SelectedTab = selectedTabIndex;
        CurrentMaster.Tabs.DoTabSelection();
    }


    protected string[] tabElem_OnTabCreated(UIElementInfo element, string[] parameters, int tabIndex)
    {
        string elementName = element.ElementName.ToLower();

        if (elementName.StartsWith("validation.") && (elementName.Substring("validation.".Length) == selected))
        {
            selectedTabIndex = tabIndex;
        }

        if (parameters[2] != null)
        {
            parameters[2] = URLHelper.AppendQuery(URLHelper.RemoveQuery(parameters[2]), URLHelper.Url.Query);
        }

        return parameters;
    }
}
