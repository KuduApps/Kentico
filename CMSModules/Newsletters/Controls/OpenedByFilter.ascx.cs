using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Controls_OpenedByFilter : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets the where condition created using filtered parameters.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            string whereCond = string.Empty;

            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltSubscriberName.GetCondition());            
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEmail.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltOpenedBetween.GetCondition());       

            return whereCond;
        }
    }

    #endregion
}