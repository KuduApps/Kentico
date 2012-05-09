using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_EmailQueue_Controls_EmailQueue : CMSAdminControl, ICallbackEventHandler
{
    #region "Variables"

    private Hashtable mParameters;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the UniGrid.
    /// </summary>
    public UniGrid EmailGrid
    {
        get
        {
            return gridElem;
        }
    }


    /// <summary>
    /// Dialog control identifier.
    /// </summary>
    private string Identifier
    {
        get
        {
            string identifier = hdnIdentificator.Value;
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = Guid.NewGuid().ToString();
                hdnIdentificator.Value = identifier;
            }

            return identifier;
        }
    }


    /// <summary>
    /// Gets or sets the email id.
    /// </summary>
    private int EmailID 
    { 
        get; 
        set; 
    }

    
    /// <summary>
    /// Returns true if the control processing should be stopped.
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
            gridElem.StopProcessing = value;
        }
    }    

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions
        RaiseOnCheckPermissions("READ", this);

        if (StopProcessing)
        {
            return;
        }

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(Page);

        string openEmailDetailScript = string.Format("function OpenEmailDetail(queryParameters) {{\n modalDialog({0} + queryParameters, 'emaildetails', 920, 700);\n}}", 
                                                     ScriptHelper.GetString(ResolveUrl("~/CMSModules/EmailQueue/EmailQueue_Details.aspx")));

        // Register the dialog script
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "Email_OpenDetail", ScriptHelper.GetScript(openEmailDetailScript));
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "Email_" + ClientID, ScriptHelper.GetScript("var emailDialogParams_" + ClientID + " = '';"));

        gridElem.OnAction += gridElem_OnAction;
        gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;
    }

    #endregion


    #region "Public Methods"

    public override void ReloadData()
    {
        if (!StopProcessing)
        {
            gridElem.ReloadData();
        }
        base.ReloadData();
    }


    /// <summary>
    /// Returns the IDs of e-mail messages that were selected.
    /// </summary>
    /// <returns>An array of email IDs</returns>
    public int[] GetSelectedEmailIDs()
    {
        int[] emailIds = new int[EmailGrid.SelectedItems.Count];

        for (int i = 0; i < EmailGrid.SelectedItems.Count; i++)
        {
            emailIds[i] = ValidationHelper.GetInteger(EmailGrid.SelectedItems[i], 0);
        }

        return emailIds;
    }

    #endregion


    #region "Unigrid events"

    /// <summary>
    /// Handles Unigrid's OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        sourceName = sourceName.ToLower();

        switch (sourceName)
        {
            case "priority":
                return GetEmailPriority(parameter);                

            case "status":
                return GetEmailStatus(parameter);

            case "subject":
                return TextHelper.LimitLength(HTMLHelper.HTMLEncode(parameter.ToString()), 40);

            case "result":
                return TextHelper.LimitLength(HTMLHelper.HTMLEncode(parameter.ToString()), 40);

            case "subjecttooltip":

            case "resulttooltip":
                return parameter.ToString().Replace("\r\n", "<br />").Replace("\n", "<br />");

            case "resend":
            case "delete":
                ImageButton imageButton = sender as ImageButton;
                if (imageButton != null)
                {
                    DisableActionButtons(imageButton, sourceName, parameter);
                }
                break;

            case "emailto":
                return GetEmailRecipients(parameter);

            case "edit":
                ImageButton viewBtn = (ImageButton)sender;
                viewBtn.OnClientClick = string.Format("emailDialogParams_{0} = '{1}';{2};return false;", 
                                                      ClientID, 
                                                      viewBtn.CommandArgument, Page.ClientScript.GetCallbackEventReference(this, "emailDialogParams_" + ClientID, "OpenEmailDetail", null));

                break;
        }

        return null;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (StopProcessing)
        {
            return;
        }

        switch (actionName.ToLower())
        {
            case "delete":
                // Delete an email
                int deleteEmailId = ValidationHelper.GetInteger(actionArgument, 0);
                EmailHelper.Queue.Delete(deleteEmailId);                
                break;

            case "resend":
                // Resend email info object from queue
                int sendEmailId = ValidationHelper.GetInteger(actionArgument, -1);
                if (sendEmailId > 0)
                {
                    EmailHelper.Queue.Send(sendEmailId);                    
                }
                break;
        }
    }

    /// <summary>
    /// Gets the e-mail priority.
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>E-mail priority</returns>
    private string GetEmailPriority(object parameter)
    {        
        switch ((EmailPriorityEnum)parameter)
        {
            case EmailPriorityEnum.Low:
                return GetString("emailpriority.low");

            case EmailPriorityEnum.Normal:
                return GetString("emailpriority.normal");

            case EmailPriorityEnum.High:
                return GetString("emailpriority.high");

            default:
                return string.Empty;
        }        
    }


    /// <summary>
    /// Gets the e-mail status.
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>E-mail status</returns>
    private string GetEmailStatus(object parameter)
    {        
        switch ((EmailStatusEnum)parameter)
        {
            case EmailStatusEnum.Created:
                return GetString("emailstatus.created");

            case EmailStatusEnum.Waiting:
                return GetString("emailstatus.waiting");

            case EmailStatusEnum.Sending:
                return GetString("emailstatus.sending");

            default:
                return string.Empty;
        }        
    }


    /// <summary>
    /// Gets the e-mail recipient(s).
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>E-mail recipients</returns>
    private string GetEmailRecipients(object parameter)
    {        
        DataRowView dr = (DataRowView)parameter;

        if (ValidationHelper.GetBoolean(dr["EmailIsMass"], false))
        {            
            return string.Format("<a href=\"#\" onclick=\"javascript: DisplayRecipients({0}); return false; \">{1}</a>",
                                 ValidationHelper.GetInteger(dr["EmailID"], 0),
                                 GetString("emailqueue.queue.massdetails"));
        }
        else
        {            
            return HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["EmailTo"], string.Empty));
        }
    }


    /// <summary>
    /// Disables the action buttons.
    /// </summary>
    /// <param name="imageButton">The image button</param>
    /// <param name="sourceName">Name of the source</param>
    /// <param name="parameter">The parameter</param>
    private void DisableActionButtons(ImageButton imageButton, string sourceName, object parameter)
    {        
        int status = ValidationHelper.GetInteger((object)((DataRowView)((GridViewRow)parameter).DataItem).Row["EmailStatus"], -1);
        bool sending = EmailHelper.Queue.SendingInProgess;

        // Disable action buttons (and image) if e-mail status is 'created' or 'sending'
        if (sending || (status == (int)EmailStatusEnum.Created) || (status == (int)EmailStatusEnum.Sending))
        {
            imageButton.OnClientClick = null;
            imageButton.Enabled = false;

            imageButton.ImageUrl = sourceName == "resend" ?
                GetImageUrl("Design/Controls/UniGrid/Actions/resendemaildisabled.png") :
                GetImageUrl("Design/Controls/UniGrid/Actions/deletedisabled.png");
        }
    }


    #endregion


    #region "ICallbackEventHandler Members"

    /// <summary>
    /// Gets callback result.
    /// </summary>
    public string GetCallbackResult()
    {
        mParameters = new Hashtable();
        mParameters["where"] = gridElem.WhereCondition;
        mParameters["orderby"] = gridElem.SortDirect;

        WindowHelper.Add(Identifier, mParameters);

        string queryString = "?params=" + Identifier;

        queryString = URLHelper.AddParameterToUrl(queryString, "hash", QueryHelper.GetHash(queryString));
        queryString = URLHelper.AddParameterToUrl(queryString, "emailid", EmailID.ToString());

        return queryString;
    }


    /// <summary>
    /// Raise callback method.
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        EmailID = ValidationHelper.GetInteger(eventArgument, 0);
    }

    #endregion
}