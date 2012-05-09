using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.UIControls;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_TimeZones_Pages_TimeZone_Edit : SiteManagerPage
{
    protected int zoneid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Page.Title = "Time Zone - Edit";

        // Set context help
        this.CurrentMaster.Title.HelpTopicName = "time_zones_edit";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Control initialization				
        rfvName.ErrorMessage = GetString("TimeZ.Edit.rfvName");
        rfvDisplayName.ErrorMessage = GetString("TimeZ.Edit.rfvDisplayName");
        rfvGMT.ErrorMessage = GetString("TimeZ.Edit.rfvGMT");
        rvGMTDouble.ErrorMessage = GetString("TimeZ.Edit.rvGMTDouble");
        rvGMTDouble.MinimumValue = (-12.0).ToString();
        rvGMTDouble.MaximumValue = (13.0).ToString();

        string currentTimeZone = GetString("TimeZ.Edit.NewItem");

        // Get timeZone id from querystring		
        zoneid = QueryHelper.GetInteger("zoneid", 0);
        if (zoneid > 0)
        {
            TimeZoneInfo timeZoneObj = TimeZoneInfoProvider.GetTimeZoneInfo(zoneid);
            if (timeZoneObj != null)
            {
                currentTimeZone = timeZoneObj.TimeZoneDisplayName;

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(timeZoneObj);

                    // Show that the timeZone was created or updated successfully
                    if (ValidationHelper.GetString(Request.QueryString["saved"], "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }
        else
        {
            this.plcDSTInfo.Visible = false;
        }

        // Initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("TimeZ.Edit.ItemList");
        breadcrumbs[0, 1] = "~/CMSModules/TimeZones/Pages/TimeZone_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = currentTimeZone;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        if (QueryHelper.GetString("zoneid", "") != "")
        {
            this.CurrentMaster.Title.TitleText = GetString("timez.edit.properties");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_TimeZone/object.png");
        }
        else
        {
            this.CurrentMaster.Title.TitleText = GetString("timez.edit.newtimezone");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_TimeZone/new.png");
        }


        startRuleEditor.Enabled = this.chkTimeZoneDaylight.Checked;
        endRuleEditor.Enabled = this.chkTimeZoneDaylight.Checked;
    }


    /// <summary>
    /// Load data of editing timeZone.
    /// </summary>
    /// <param name="timeZoneObj">TimeZone object</param>
    protected void LoadData(TimeZoneInfo timeZoneObj)
    {

        txtTimeZoneName.Text = timeZoneObj.TimeZoneName;
        txtTimeZoneDisplayName.Text = timeZoneObj.TimeZoneDisplayName;
        txtTimeZoneGMT.Text = Convert.ToString(timeZoneObj.TimeZoneGMT);
        chkTimeZoneDaylight.Checked = timeZoneObj.TimeZoneDaylight;
        if (timeZoneObj.TimeZoneDaylight)
        {
            lblTimeZoneRuleStart.Text = timeZoneObj.TimeZoneRuleStartIn.ToString();
            lblTimeZoneRuleEnd.Text = timeZoneObj.TimeZoneRuleEndIn.ToString();
            startRuleEditor.Rule = timeZoneObj.TimeZoneRuleStartRule;
            endRuleEditor.Rule = timeZoneObj.TimeZoneRuleEndRule;
        }

        if (lblTimeZoneRuleStart.Text.Trim() == String.Empty)
        {
            lblTimeZoneRuleStart.Text = GetString("general.na");
        }

        if (lblTimeZoneRuleEnd.Text.Trim() == String.Empty)
        {
            lblTimeZoneRuleEnd.Text = GetString("general.na");
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = new Validator()
            .NotEmpty(txtTimeZoneName.Text, rfvName.ErrorMessage)
            .NotEmpty(txtTimeZoneDisplayName.Text, rfvDisplayName.ErrorMessage)
            .NotEmpty(txtTimeZoneGMT.Text, rfvGMT.ErrorMessage)
            .IsCodeName(txtTimeZoneName.Text, GetString("general.invalidcodename"))
            .IsDouble(txtTimeZoneGMT.Text, rvGMTDouble.ErrorMessage)
            .Result;
        if (this.chkTimeZoneDaylight.Checked)
        {
            if ((!this.startRuleEditor.IsValid()) || (!this.endRuleEditor.IsValid()))
            {
                errorMessage = GetString("TimeZ.RuleEditor.NotValid");
            }
        }

        if (errorMessage == "")
        {

            // timeZoneName must to be unique
            TimeZoneInfo timeZoneObj = TimeZoneInfoProvider.GetTimeZoneInfo(txtTimeZoneName.Text.Trim());

            // if timeZoneName value is unique														
            if ((timeZoneObj == null) || (timeZoneObj.TimeZoneID == zoneid))
            {
                // if timeZoneName value is unique -> determine whether it is update or insert 
                if ((timeZoneObj == null))
                {
                    // get TimeZoneInfo object by primary key
                    timeZoneObj = TimeZoneInfoProvider.GetTimeZoneInfo(zoneid);
                    if (timeZoneObj == null)
                    {
                        // create new item -> insert
                        timeZoneObj = new TimeZoneInfo();
                    }
                }

                timeZoneObj.TimeZoneName = txtTimeZoneName.Text.Trim();
                timeZoneObj.TimeZoneDaylight = chkTimeZoneDaylight.Checked;
                timeZoneObj.TimeZoneDisplayName = txtTimeZoneDisplayName.Text.Trim();
                timeZoneObj.TimeZoneRuleStartIn = ValidationHelper.GetDateTime(TimeZoneInfoProvider.CreateRuleDateTime(this.startRuleEditor.Rule), DateTime.Now);
                timeZoneObj.TimeZoneRuleEndIn = ValidationHelper.GetDateTime(TimeZoneInfoProvider.CreateRuleDateTime(this.endRuleEditor.Rule), DateTime.Now);
                timeZoneObj.TimeZoneRuleStartRule = this.startRuleEditor.Rule;
                timeZoneObj.TimeZoneRuleEndRule = this.endRuleEditor.Rule;
                timeZoneObj.TimeZoneGMT = Convert.ToDouble(txtTimeZoneGMT.Text.Trim());

                TimeZoneInfoProvider.SetTimeZoneInfo(timeZoneObj);

                URLHelper.Redirect("TimeZone_Edit.aspx?zoneid=" + Convert.ToString(timeZoneObj.TimeZoneID) + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("TimeZ.Edit.TimeZoneNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
