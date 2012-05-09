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
using CMS.Forums;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Subscriptions_ForumSubscription_Edit : CMSGroupForumPage
{
    protected int subscriptionId = 0;
    protected int forumId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Forums - Edit subscription";
        
        string currentForumSubscription = "";
        ForumSubscriptionInfo forumSubscriptionObj = new ForumSubscriptionInfo();
        int subscriptionId = QueryHelper.GetInteger("subscriptionid", 0);
        this.subscriptionEdit.SubscriptionID = subscriptionId;
        this.subscriptionEdit.OnSaved += new EventHandler(subscriptionEdit_OnSaved);
        this.subscriptionEdit.IsLiveSite = false;

        // get forumSubscription id from querystring
        if (subscriptionId > 0)
        {            
            forumSubscriptionObj = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(subscriptionId);
            currentForumSubscription = HTMLHelper.HTMLEncode(forumSubscriptionObj.SubscriptionEmail);

            // initializes page title control		
            string[,] tabs = new string[2, 3];
            tabs[0, 0] = GetString("ForumSubscription_Edit.ItemListLink");
            tabs[0, 1] = "~/CMSModules/Groups/Tools/Forums/Subscriptions/ForumSubscription_List.aspx?forumid=" + forumSubscriptionObj.SubscriptionForumID;
            tabs[0, 2] = "";
            tabs[1, 0] = (string.IsNullOrEmpty(currentForumSubscription)) ? GetString("ForumSubscription_List.NewItemCaption") : currentForumSubscription;
            tabs[1, 1] = "";
            tabs[1, 2] = "";

            this.CurrentMaster.Title.Breadcrumbs = tabs;
        }
        else
        {
            forumId = ValidationHelper.GetInteger(Request.QueryString["forumid"], 0);
            if (forumId == 0)
            {
                return;
            }

            this.subscriptionEdit.ForumID = forumId;

            // initializes page title control		
            string[,] tabs = new string[2, 3];
            tabs[0, 0] = GetString("ForumSubscription_Edit.ItemListLink");
            tabs[0, 1] = "~/CMSModules/Groups/Tools/Forums/Subscriptions/ForumSubscription_List.aspx?forumid=" + forumId.ToString();
            tabs[0, 2] = "";
            tabs[1, 0] = (string.IsNullOrEmpty(currentForumSubscription)) ? GetString("ForumSubscription_List.NewItemCaption") : currentForumSubscription;
            tabs[1, 1] = "";
            tabs[1, 2] = "";
            this.CurrentMaster.Title.Breadcrumbs = tabs;
            this.CurrentMaster.Title.TitleText = GetString("forumsubscription_edit.newitemcaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Forums_ForumSubscription/new.png");
        }

        this.subscriptionEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(subscriptionEdit_OnCheckPermissions);
    }


    void subscriptionEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        ForumInfo fi = ForumInfoProvider.GetForumInfo(ValidationHelper.GetInteger(Request.QueryString["forumid"], 0));
        if (fi != null)
        {
            ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
            if (fgi != null)
            {
                groupId = fgi.GroupGroupID;
            }
        }

        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    void subscriptionEdit_OnSaved(object sender, EventArgs e)
    {
        if (this.subscriptionEdit.SubscriptionID != 0)
        {
            URLHelper.Redirect("ForumSubscription_Edit.aspx?subscriptionid=" + Convert.ToString(this.subscriptionEdit.SubscriptionID) + "&saved=1");
        }
    }
}
