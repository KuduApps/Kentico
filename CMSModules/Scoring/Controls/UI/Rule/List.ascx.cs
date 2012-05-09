using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_Scoring_Controls_UI_Rule_List : CMSAdminListControl
{
    #region "Properties"

    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int scoreId = QueryHelper.GetInteger("scoreid", 0);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.EditActionUrl = "Tab_Rules_Edit.aspx?scoreid=" + scoreId + "&ruleId={0}";
        gridElem.WhereCondition = "RuleScoreID = " + scoreId;
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "validity":
                DataRowView row = (DataRowView)parameter;

                // Valid until specific date
                DateTime validUntil = ValidationHelper.GetDateTime(row["RuleValidUntil"], DateTime.MinValue);
                if (validUntil != DateTime.MinValue)
                {
                    return validUntil.ToShortDateString();
                }

                // Valid for specified time period
                if (!String.IsNullOrEmpty(ValidationHelper.GetString(row["RuleValidity"], null)))
                {
                    string strValidity = ValidationHelper.GetString(row["RuleValidity"], null);
                    ValidityEnum validity = (ValidityEnum)Enum.Parse(typeof(ValidityEnum), strValidity);
                    if (validity != ValidityEnum.Until)
                    {
                        return ValidationHelper.GetString(row["RuleValidFor"], "1") + " " + Enum.GetName(typeof(ValidityEnum), validity);
                    }
                }
                break;

            case "ruletype":
                string name = "om.score.";
                name += Enum.GetName(typeof(RuleTypeEnum), parameter);
                return GetString(name);
        }
        return null;
    }

    #endregion
}
