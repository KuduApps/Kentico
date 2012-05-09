using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

// Title
[Title("Objects/CMS_Country/object.png", "Country_List.HeaderCaption", "countries_list")]

// Actions
[Actions(1)]
[Action(0, "Objects/CMS_Country/add.png", "Country_List.NewItemCaption", "New.aspx")]

public partial class CMSModules_Countries_Pages_Development_Country_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            int countryId = ValidationHelper.GetInteger(actionArgument, 0);

            bool dependent = false;

            // Get country states
            DataSet ds = StateInfoProvider.GetCountryStates(countryId);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // Check dependency of all state at first
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int stateId = ValidationHelper.GetInteger(dr["StateID"], 0);
                    if (StateInfoProvider.CheckDependencies(stateId))
                    {
                        dependent = true;
                        break;
                    }
                }
            }

            // Get country dependency without states
            DataSet dsCountryDependency = CountryInfoProvider.GetCountries(String.Format("(CountryID IN (SELECT AddressCountryID FROM COM_Address WHERE AddressCountryID={0}) OR CountryID IN (SELECT CustomerCountryID FROM COM_Customer WHERE CustomerCountryID={0}) OR CountryID IN (SELECT CountryID FROM COM_TaxClassCountry WHERE CountryID={0}))", countryId), null, 1, "CountryID");

            // Check dependency of country itself
            dependent |= (!DataHelper.DataSourceIsEmpty(dsCountryDependency));

            if (dependent)
            {
                ShowError(GetString("ecommerce.deletedisabledwithoutenable"));
            }
            else
            {

                // Delete all states
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int stateId = ValidationHelper.GetInteger(dr["StateID"], 0);
                    StateInfoProvider.DeleteStateInfo(stateId);
                }

                // Delete CountryInfo object from database
                CountryInfoProvider.DeleteCountryInfo(countryId);
            }
        }
    }
}
