using System;
using System.Data;
using System.Collections;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSFormControls_LiveSelectors_TagSelector : CMSLiveModalPage
{
    #region "Variables"

    private int groupId = 0;
    private string textBoxId = null;
    private string oldTags = null;
    private Hashtable selectedTags = null;

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register jQuery
        ScriptHelper.RegisterJQuery(Page);

        // Get group Id
        groupId = QueryHelper.GetInteger("group", 0);

        // Get id of the base selector textbox
        textBoxId = QueryHelper.GetText("textbox", "");

        // Get selected tags
        oldTags = QueryHelper.GetText("tags", "");
        selectedTags = TagHelper.GetTags(oldTags);

        // Setup UniGrid
        gridElem.ZeroRowsText = GetString("tags.tagselector.noold");
        gridElem.GridView.ShowHeader = false;
        gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
        gridElem.OnAfterDataReload += new OnAfterDataReload(gridElem_OnAfterDataReload);
        gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;

        // Page title
        CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("Objects/CMS_TagGroup/object.png", true));
        CurrentMaster.Title.TitleText = GetString("tags.tagselector.title");

        btnOk.Click += btnOk_Click;
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        string retval = "";

        // Append selected tags which are already in DB
        if (gridElem.SelectedItems.Count > 0)
        {
            ArrayList tags = gridElem.SelectedItems;
            tags.Sort();
            foreach (string tagName in tags)
            {
                if (tagName.Contains(" "))
                {
                    retval = (retval + ", \"" + tagName.Trim('"') + "\"");
                }
                else
                {
                    retval = (retval + ", " + tagName);
                }
            }
        }

        // Remove
        if (retval != "")
        {
            retval = retval.Substring(2);
        }

        ltlScript.Text = ScriptHelper.GetScript("wopener.setTagsToTextBox(" + ScriptHelper.GetString(textBoxId) + ", " + ScriptHelper.GetString(retval) + "); window.close();");
    }

    #endregion


    #region "UniGrid Events"

    protected void gridElem_OnBeforeDataReload()
    {
        string where = "(TagGroupID = " + groupId + ")";
        if (!String.IsNullOrEmpty(gridElem.CompleteWhereCondition))
        {
            where += " AND (" + gridElem.CompleteWhereCondition + ")";
        }
        gridElem.WhereCondition = where;
    }


    protected void gridElem_OnAfterDataReload()
    {
        if (!DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource))
        {
            ArrayList selection = new ArrayList();
            foreach (string tag in selectedTags.Values)
            {
                selection.Add(tag.Trim('"'));
            }

            if (!URLHelper.IsPostback())
            {
                gridElem.SelectedItems = selection;
            }
        }
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (sourceName.ToLower() == "tagname")
        {
            DataRowView drv = (DataRowView)parameter;
            string tagName = ValidationHelper.GetString(drv["TagName"], "");
            string tagId = ValidationHelper.GetString(drv["TagID"], "");
            if ((tagName != "") && (tagName != tagId))
            {
                string tagCount = ValidationHelper.GetString(drv["TagCount"], "");
                string tagText = HTMLHelper.HTMLEncode(tagName) + " (" + tagCount + ")";

                // Create link with onclick event which call onclick event of checkbox in the same row
                return "<a href=\"#\" onclick=\"var c=$j(this).parents('tr:first').find('input:checkbox'); c.attr('checked', !c.attr('checked')).get(0).onclick(); return false;\">" + tagText + "</a>";
            }
        }
        return "";
    }

    #endregion
}