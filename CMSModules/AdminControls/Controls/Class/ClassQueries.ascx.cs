using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_ClassQueries : CMSUserControl
{
    #region "Private fields"

    private int mClassID = 0;
    private string mEditPageUrl = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the class to edit queries.
    /// </summary>
    public int ClassID
    {
        get
        {
            return this.mClassID;
        }
        set
        {
            this.mClassID = value;
        }
    }


    /// <summary>
    /// URL of the page holding the editing tasks.
    /// </summary>
    public string EditPageUrl
    {
        get
        {
            return this.mEditPageUrl;
        }
        set
        {
            this.mEditPageUrl = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            uniGrid.IsLiveSite = value;
            base.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {

        }
        else
        {
            // Initialize the controls
            uniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
            uniGrid.GridName = "~/CMSModules/AdminControls/Controls/Class/ClassQueries.xml";
            uniGrid.IsLiveSite = IsLiveSite;
            uniGrid.ZeroRowsText = GetString("general.nodatafound");

            // If the ClassID was specified
            if (ClassID > 0)
            {
                uniGrid.WhereCondition = "ClassID=" + ClassID;
            }
            else
            {
                // Otherwise hide the UniGrid to avoid unexpected behaviour
                uniGrid.Visible = false;
            }
        }
    }

    #endregion


    #region "UniGrid handling"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (string.Equals(actionName, "edit", StringComparison.InvariantCultureIgnoreCase))
        {
            RedirectToEditUrl(actionArgument);
        }
        else if (string.Equals(actionName, "delete", StringComparison.InvariantCultureIgnoreCase))
        {
            int queryId = ValidationHelper.GetInteger(actionArgument, -1);
            if (queryId > 0)
            {
                QueryProvider.DeleteQuery(queryId);
            }
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Redirects to page where user can edit a selected query.
    /// </summary>
    /// <param name="actionArgument">ID of the query selected in UniGrid</param>
    private void RedirectToEditUrl(object actionArgument)
    {
        string actionArg = ValidationHelper.GetString(actionArgument, string.Empty);
        if (actionArg == string.Empty)
        {
            return;
        }

        string editUrl = string.Format("{0}?queryid={1}&classid={2}", this.EditPageUrl, actionArg, this.ClassID);
        URLHelper.Redirect(editUrl);
    }

    #endregion
}