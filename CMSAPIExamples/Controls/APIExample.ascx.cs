using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.EventLog;

public partial class CMSAPIExamples_Controls_APIExample : CMSUserControl
{
    #region "Enumeration"

    /// <summary>
    /// Type of API example.
    /// </summary>
    public enum APIExampleTypeEnum
    {
        /// <summary>
        /// Main API example.
        /// </summary>
        ManageMain = 0,

        /// <summary>
        /// Additional API example.
        /// </summary>
        ManageAdditional = 1,

        /// <summary>
        /// Main cleanup API example.
        /// </summary>
        CleanUpMain = 2,

        /// <summary>
        /// Additional cleanup API example.
        /// </summary>
        CleanUpAdditional = 3
    }

    #endregion


    #region "Variables"

    private int? mExampleOrder = null;
    private string mButtonText = null;
    private string mButtonClass = null;
    private string mInfoMessage = "The API example ran successfully.";
    private string mErrorMessage = "Something went wrong.";
    private string mMethodName = null;
    private APIExampleTypeEnum mAPIExampleType = APIExampleTypeEnum.ManageMain;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Text which is set as inner button's text.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return this.mButtonText;
        }
        set
        {
            this.mButtonText = value;
        }
    }


    /// <summary>
    /// Css class which is applied to inner button.
    /// </summary>
    public string ButtonClass
    {
        get
        {
            if (string.IsNullOrEmpty(this.mButtonClass))
            {
                switch (this.APIExampleType)
                {
                    case APIExampleTypeEnum.ManageAdditional:
                    case APIExampleTypeEnum.CleanUpAdditional:
                        // Blue button
                        this.mButtonClass = "XXLongButton";
                        break;

                    case APIExampleTypeEnum.ManageMain:
                    case APIExampleTypeEnum.CleanUpMain:
                    default:
                        // Green button
                        this.mButtonClass = "XXLongSubmitButton";
                        break;
                }
            }

            return this.mButtonClass;
        }
        set
        {
            this.mButtonClass = value;
        }
    }


    /// <summary>
    /// Text which is applied when API example ran successfully.
    /// </summary>
    public string InfoMessage
    {
        get
        {
            return this.mInfoMessage;
        }
        set
        {
            this.mInfoMessage = value;
        }
    }


    /// <summary>
    /// Text which is displayed when API example failed.
    /// </summary>
    public string ErrorMessage
    {
        get
        {
            return this.mErrorMessage;
        }
        set
        {
            this.mErrorMessage = value;
        }
    }


    /// <summary>
    /// Type of the example.
    /// </summary>
    public APIExampleTypeEnum APIExampleType
    {
        get
        {
            return mAPIExampleType;
        }
        set
        {
            mAPIExampleType = value;
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Specifies order within all module's examples from same container.
    /// </summary>
    private int ExampleOrder
    {
        get
        {
            if (!mExampleOrder.HasValue)
            {
                if (IsCleanupExample)
                {
                    mExampleOrder = ValidationHelper.GetInteger(RequestStockHelper.GetItem("CMSRightAPIExampleOrder"), 0) + 1;
                    RequestStockHelper.Add("CMSRightAPIExampleOrder", mExampleOrder, false);
                }
                else
                {
                    mExampleOrder = ValidationHelper.GetInteger(RequestStockHelper.GetItem("CMSLeftAPIExampleOrder"), 0) + 1;
                    RequestStockHelper.Add("CMSLeftAPIExampleOrder", mExampleOrder, false);
                }
            }

            return (int)this.mExampleOrder;
        }
    }


    /// <summary>
    /// Indicates if controls is used for cleanup example.
    /// </summary>
    private bool IsCleanupExample
    {
        get
        {
            return (APIExampleType == APIExampleTypeEnum.CleanUpMain) || (APIExampleType == APIExampleTypeEnum.CleanUpAdditional);
        }
    }


    /// <summary>
    /// Name of the method handling this example.
    /// </summary>
    private string MethodName
    {
        get
        {
            if (string.IsNullOrEmpty(mMethodName))
            {
                // Skip leading "api" substring
                mMethodName = this.ID.Substring(3);
            }
            return mMethodName;
        }
    }


    /// <summary>
    /// Path to file where this control is used.
    /// </summary>
    private string CurrentFilePath
    {
        get
        {
            string path = RequestStockHelper.GetItem("CurrentFilePath", true) as string;
            if (string.IsNullOrEmpty(path))
            {
                path = Request.PhysicalPath + ".cs";
                RequestStockHelper.Add("CurrentFilePath", path);
            }

            return path;
        }
    }

    #endregion


    #region "Events"

    public delegate bool OnRunExample();

    /// <summary>
    /// Event raised on button click.
    /// </summary>
    public event OnRunExample RunExample;


    public delegate void OnDisplayCode(string filePath, string method);

    /// <summary>
    /// Event raised on "Disaplay Code" button click.
    /// </summary>
    public event OnDisplayCode DisplayCode;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set up displaying of code
        CMSAPIExamplePage examplePage = Page as CMSAPIExamplePage;
        if (examplePage != null)
        {
            this.DisplayCode += new OnDisplayCode(examplePage.ReadCode);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        btnAction.CssClass = ButtonClass;
        btnAction.Text = ButtonText;

        if (DisplayCode != null)
        {
            btnShowCode.Visible = true;
            btnShowCode.ImageUrl = GetImageUrl("/Others/APIExamples/details.png");
        }

        lblNumber.Text = ExampleOrder + ".";
    }


    protected void btnAction_Click(object sender, EventArgs e)
    {
        Run();
    }


    /// <summary>
    /// Runs example.
    /// </summary>
    public void Run()
    {
        try
        {
            if (RunExample != null)
            {
                bool success = RunExample();

                if (!success)
                {
                    // Display error message
                    lblError.Text = ErrorMessage;
                    lblError.Visible = true;

                    return;
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent("APIExample", "EXCEPTION", ex);

            string  msg = "";

            // Try to find id of last interesting log
            string where = ep.GetSiteWhereCondition(0) + " AND (Source = 'APIExample') AND (IPAddress = '" + HTTPHelper.UserHostAddress + "')";
            string orderBy = "EventTime DESC, EventID DESC";
            DataSet ds = ep.GetAllEvents(where, orderBy, 1, "EventID");

            // Get id
            int eventId = 0;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                eventId = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["EventID"], 0);
            }

            if(eventId != 0)
            {
                string identificator = Guid.NewGuid().ToString();
                Hashtable mParameters = new Hashtable();
                mParameters["where"] = where;
                mParameters["orderby"] = orderBy;

                WindowHelper.Add(identificator, mParameters);

                string queryString = "?params=" + identificator;
                queryString = URLHelper.AddParameterToUrl(queryString, "hash", QueryHelper.GetHash(queryString));
                queryString = URLHelper.AddParameterToUrl(queryString, "eventid", eventId.ToString());

                // Add link to event details in event log
                msg = String.Format("The API example failed. See event log for <a href=\"\" onclick=\"modalDialog('" + ResolveUrl("~/CMSModules/EventLog/EventLog_Details.aspx") + queryString + "', 'eventdetails', 920, 700); return false;\">more details</a>.");
            }
            else
            {
                // Add link to Event log
                msg = String.Format("The API example failed. See <a href=\"" + ResolveUrl("~/CMSModules/EventLog/EventLog.aspx") + "\" target=\"_blank\">event log</a> for more details.");
            }

            // Display error message
            lblError.Text = msg;
            lblError.ToolTip = ex.Message;
            lblError.Visible = true;

            return;
        }

        //Display info message
        lblInfo.Text = InfoMessage;
        lblInfo.Visible = true;
    }


    protected void btnShowCode_Click(object sender, EventArgs e)
    {
        if (DisplayCode != null)
        {
            DisplayCode(CurrentFilePath, MethodName);
        }
    }

    #endregion
}
