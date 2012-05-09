using System.Collections;
using System.Data;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;
using CMS.SiteProvider;

/// <summary>
/// Class containing resolvers for specific types of e-mail templates.
/// </summary>
public static class EmailTemplateMacros
{
    #region "Enumeration"

    /// <summary>
    /// Email type enumeration. Determines the type of a resolver used in the e-mail template.
    /// </summary>
    public enum EmailType
    {
        General,
        Blog,
        Boards,
        BookingEvent,
        Ecommerce,
        EcommerceEproductExpiration,
        Forum,
        ForumSubscribtion,
        Friends,
        GroupMember,
        GroupInvitation,
        GroupMemberInvitation,
        Password,
        ProjectManagement,
        Messaging,
        Registration,
        RegistrationApproval,
        MembershipRegistration,
        MembershipExpiration,
        MembershipChangePassword,
        ForgottenPassword,
        Newsletter,
        Workflow,
        Scoring
    }

    #endregion


    #region "Variables"

    static ContextResolver mEmailTemplateResolver = null;
    static ContextResolver mShoppingCartResolver = null;
    static ContextResolver mNewslettersResolver = null;
    static ContextResolver mEcommerceResolver = null;
    static ContextResolver mEcommerceEproductExpirationResolver = null;
    static ContextResolver mWorkflowResolver = null;
    static ContextResolver mBlogResolver = null;
    static ContextResolver mBookingResolver = null;
    static ContextResolver mForumsResolver = null;
    static ContextResolver mForumsSubscribeResolver = null;
    static ContextResolver mFriendsResolver = null;
    static ContextResolver mMessagingResolver = null;
    static ContextResolver mRegistrationResolver = null;
    static ContextResolver mRegistrationApprovedResolver = null;
    static ContextResolver mMembershipRegistrationResolver = null;
    static ContextResolver mMembershipExpirationResolver = null;
    static ContextResolver mPasswordResolver = null;
    static ContextResolver mForgottenPasswordResolver = null;
    static ContextResolver mGroupMemberResolver = null;
    static ContextResolver mBoardsResolver = null;
    static ContextResolver mGroupsInvitationResolver = null;
    static ContextResolver mGroupsAcceptedInvitationResolver = null;
    static ContextResolver mProjectManagementResolver = null;
    static ContextResolver mScoringResolver = null;
    static ContextResolver mContactResolver = null;
    static ContextResolver mMembershipChangePasswordResolver = null;

    #endregion


    #region "Methods"

    /// <summary>
    /// Sets additional context values to resolver.
    /// </summary>
    /// <param name="resolver">Context resolver</param>
    private static void SetContext(ContextResolver resolver)
    {
        resolver.CurrentDocument = TreeNode.New("CMS.root");
        resolver.CurrentPageInfo = new PageInfo();
        resolver.CurrentPageInfo.PageTemplateInfo = new PageTemplateInfo();
    }


    /// <summary>
    /// Returns the EmailType enum constant from string representation.
    /// </summary>
    /// <param name="emailType">String representation of email type</param>
    public static EmailType GetEmailTypeEnum(string emailType)
    {
        // Get a enum representation and return it as result
        switch (emailType.ToLower())
        {
            case "general":
                return EmailType.General;

            case "blog":
                return EmailType.Blog;

            case "boards":
                return EmailType.Boards;

            case "bookingevent":
                return EmailType.BookingEvent;

            case "ecommerce":
                return EmailType.Ecommerce;

            case "ecommerceeproductexpiration":
                return EmailType.EcommerceEproductExpiration;

            case "forum":
                return EmailType.Forum;

            case "forumsubscribtion":
                return EmailType.ForumSubscribtion;

            case "friends":
                return EmailType.Friends;

            case "groupmember":
                return EmailType.GroupMember;

            case "groupinvitation":
                return EmailType.GroupInvitation;

            case "groupmemberinvitation":
                return EmailType.GroupMemberInvitation;

            case "password":
                return EmailType.Password;

            case "projectmanagement":
                return EmailType.ProjectManagement;

            case "messaging":
                return EmailType.Messaging;

            case "registration":
                return EmailType.Registration;

            case "registrationapproval":
                return EmailType.RegistrationApproval;

            case "membershipregistration":
                return EmailType.MembershipRegistration;

            case "membershipexpiration":
                return EmailType.MembershipExpiration;

            case "forgottenpassword":
                return EmailType.ForgottenPassword;

            case "membershipchangepassword":
                return EmailType.MembershipChangePassword;

            case "newsletter":
                return EmailType.Newsletter;

            case "workflow":
                return EmailType.Workflow;

            case "scoring":
                return EmailType.Scoring;

            default:
                return EmailType.General;
        }
    }


    /// <summary>
    /// Returns string representation of the EmailType enum constant.
    /// </summary>
    /// <param name="emailType">EmailType enum constant</param>
    public static string GetEmailTypeString(EmailType emailType)
    {
        // Get a enum representation and return it as result
        switch (emailType)
        {
            case EmailType.General:
                return "general";

            case EmailType.Blog:
                return "blog";

            case EmailType.Boards:
                return "boards";

            case EmailType.BookingEvent:
                return "bookingevent";

            case EmailType.Ecommerce:
                return "ecommerce";

            case EmailType.EcommerceEproductExpiration:
                return "ecommerceeproductexpiration";

            case EmailType.Forum:
                return "forum";

            case EmailType.ForumSubscribtion:
                return "forumsubscribtion";

            case EmailType.Friends:
                return "friends";

            case EmailType.GroupMember:
                return "groupmember";

            case EmailType.GroupInvitation:
                return "groupinvitation";

            case EmailType.GroupMemberInvitation:
                return "groupmemberinvitation";

            case EmailType.Password:
                return "password";

            case EmailType.ProjectManagement:
                return "projectmanagement";

            case EmailType.Messaging:
                return "messaging";

            case EmailType.Registration:
                return "registration";

            case EmailType.RegistrationApproval:
                return "registrationapproval";

            case EmailType.MembershipRegistration:
                return "membershipregistration";

            case EmailType.MembershipExpiration:
                return "membershipexpiration";

            case EmailType.ForgottenPassword:
                return "forgottenpassword";

            case EmailType.Newsletter:
                return "newsletter";

            case EmailType.Workflow:
                return "workflow";

            case EmailType.MembershipChangePassword:
                return "membershipchangepassword";

            case EmailType.Scoring:
                return "scoring";

            default:
                return "general";
        }
    }


    /// <summary>
    /// Returns appropriate resolver for given e-mail type.
    /// </summary>
    /// <param name="type">E-mail type</param>
    public static ContextResolver GetEmailTemplateResolver(EmailType type)
    {
        switch (type)
        {
            case EmailType.Blog:
                return BlogsResolver;

            case EmailType.Boards:
                return BoardsResolver;

            case EmailType.BookingEvent:
                return BookingResolver;

            case EmailType.Ecommerce:
                return EcommerceResolver;

            case EmailType.EcommerceEproductExpiration:
                return EcommerceEproductExpirationResolver;

            case EmailType.Forum:
                return ForumsResolver;

            case EmailType.ForumSubscribtion:
                return ForumsSubscribtionResolver;

            case EmailType.Friends:
                return FriendsResolver;

            case EmailType.GroupMember:
                return GroupMemberResolver;

            case EmailType.GroupInvitation:
                return GroupsInvitationResolver;

            case EmailType.GroupMemberInvitation:
                return GroupsAcceptedInvitationResolver;

            case EmailType.Password:
                return PasswordResolver;

            case EmailType.ProjectManagement:
                return ProjectManagement;

            case EmailType.Messaging:
                return MessagingResolver;

            case EmailType.Registration:
                return RegistrationResolver;

            case EmailType.RegistrationApproval:
                return RegistrationApprovedResolver;

            case EmailType.MembershipRegistration:
                return MembershipRegistrationResolver;

            case EmailType.MembershipExpiration:
                return MembershipExpirationResolver;

            case EmailType.ForgottenPassword:
                return ForgottenPasswordResolver;

            case EmailType.Newsletter:
                return NewsletterResolver;

            case EmailType.Workflow:
                return WorkflowResolver;

            case EmailType.Scoring:
                return ScoringResolver;

            case EmailType.MembershipChangePassword:
                return MembershipChangePasswordResolver;

            default:
                return EmailTemplateResolver;
        }
    }


    /// <summary>
    /// Reigsters flat values in the resolver (adds them to values which are already registered).
    /// </summary>
    /// <param name="resolver">Resolver object</param>
    /// <param name="names">Names of the macros - values will be accessible by this names in the resolver</param>
    private static void RegisterValues(this MacroResolver resolver, string[] names)
    {
        if ((resolver != null) && (names != null))
        {
            object[,] sourceParameters = null;
            int i = 0;

            if (resolver.SourceParameters == null)
            {
                // Create new parameters
                sourceParameters = new string[names.Length, 2];
            }
            else
            {
                // Add parameters to current resolver
                int currentSize = resolver.SourceParameters.GetUpperBound(0);
                sourceParameters = new string[names.Length + currentSize + 1, 2];

                // Copy current values
                for (int j = 0; j <= currentSize; j++)
                {
                    sourceParameters[j, 0] = resolver.SourceParameters[j, 0];
                    i++;
                }
            }

            foreach (string name in names)
            {
                sourceParameters[i++, 0] = name;
            }

            resolver.SourceParameters = sourceParameters;
        }
    }


    /// <summary>
    /// Registers NamedDataSource to a given resolver under the specified name.
    /// </summary>
    /// <param name="resolver">Resolver object</param>
    /// <param name="name">Name of the macro - object will be accessible by this name in the resolver</param>
    /// <param name="classObject">Class object to register</param>
    private static void RegisterObject(this MacroResolver resolver, string name, object classObject)
    {
        resolver.SetNamedSourceData(name, classObject);
    }


    /// <summary>
    /// Registers NamedDataSource to a given resolver under the specified name.
    /// </summary>
    /// <param name="resolver">Resolver object</param>
    /// <param name="name">Name of the macro - object will be accessible by this name in the resolver</param>
    /// <param name="className">Class name of the object (for the AutoCompletion to work properly)</param>
    private static void RegisterObject(this MacroResolver resolver, string name, string className)
    {
        BaseInfo obj = CMSObjectHelper.GetReadOnlyObject(className);
        if (obj != null)
        {
            resolver.SetNamedSourceData(name, obj);
        }
        else
        {
            resolver.SetNamedSourceData(name, new SimpleDataClass(className));
        }
    }

    #endregion


    #region "Resolvers"

    /// <summary>
    /// Newsletter resolver.
    /// </summary>
    public static ContextResolver NewsletterResolver
    {
        get
        {
            if (mNewslettersResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("NewsletterIssue", PredefinedObjectType.NEWSLETTERISSUE);
                resolver.RegisterObject("Newsletter", PredefinedObjectType.NEWSLETTER);
                resolver.RegisterObject("Subscriber", PredefinedObjectType.NEWSLETTERSUBSCRIBER);

                resolver.RegisterValues(new string[] { "Email", "FirstName", "LastName", "UnsubscribeLink" });

                mNewslettersResolver = resolver;
            }

            return mNewslettersResolver;
        }
    }


    /// <summary>
    /// Default e-mail template resolver.
    /// </summary>
    public static ContextResolver EmailTemplateResolver
    {
        get
        {
            if (mEmailTemplateResolver == null)
            {
                // Use data from ShoppingCart resolver
                ContextResolver resolver = ShoppingCartResolver.CreateContextChild();

                // Add newsletter data
                resolver.RegisterObject("Newsletter_Subscriber", PredefinedObjectType.NEWSLETTERSUBSCRIBER);
                resolver.RegisterObject("Newsletter_Newsletter", PredefinedObjectType.NEWSLETTER);
                resolver.RegisterObject("Newsletter_NewsletterIssue", PredefinedObjectType.NEWSLETTERISSUE);

                // Set additional context macros
                resolver.CurrentDocument = TreeNode.New("CMS.root");
                resolver.CurrentPageInfo = new PageInfo();
                resolver.CurrentPageInfo.PageTemplateInfo = new PageTemplateInfo();

                mEmailTemplateResolver = resolver;
            }

            return mEmailTemplateResolver;
        }
    }


    /// <summary>
    /// Returns membership change password e-mail template macro resolver.
    /// </summary>
    public static ContextResolver MembershipChangePasswordResolver
    {
        get
        {
            if (mMembershipChangePasswordResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterValues(new string[] { "ResetPasswordUrl", "CancelUrl" });
                mMembershipChangePasswordResolver = resolver;
            }

            return mMembershipChangePasswordResolver;
        }
    }


    /// <summary>
    /// Blog e-mail template macro resolver.
    /// </summary>
    public static ContextResolver BlogsResolver
    {
        get
        {
            if (mBlogResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                // Blog document type doesn't exists, fill with the root doc. type
                if (DataHelper.DataSourceIsEmpty(DataClassInfoProvider.GetDataClass("cms.blog")))
                {
                    resolver.RegisterObject("Blog", TreeNode.New("CMS.Root"));
                }
                else
                {
                    resolver.RegisterObject("Blog", TreeNode.New("CMS.Blog"));
                }

                // BlogPost document type doesn't exists, fill with the root doc. type
                if (DataHelper.DataSourceIsEmpty(DataClassInfoProvider.GetDataClass("cms.blogpost")))
                {
                    resolver.RegisterObject("BlogPost", TreeNode.New("CMS.Root"));
                }
                else
                {
                    resolver.RegisterObject("BlogPost", TreeNode.New("CMS.BlogPost"));
                }
                resolver.RegisterObject("Comment", PredefinedObjectType.BLOGCOMMENT);
                resolver.RegisterObject("CommentUser", PredefinedObjectType.USER);
                resolver.RegisterObject("CommentUserSettings", SiteObjectType.USERSETTINGS);

                // Register flat values
                resolver.RegisterValues(new string[] { "UserFullName", "CommentUrl", "Comments", "CommentDate", "BlogPostTitle", "BlogLink", "BlogPostLink", "UnsubscriptionLink" });

                // Set BlogResolver
                mBlogResolver = resolver;
            }

            return mBlogResolver;
        }
    }


    /// <summary>
    /// Project management e-mail template macro resolver.
    /// </summary>
    public static ContextResolver ProjectManagement
    {
        get
        {
            if (mProjectManagementResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("ProjectTask", PredefinedObjectType.PROJECTTASK);
                resolver.RegisterObject("Project", PredefinedObjectType.PROJECT);
                resolver.RegisterObject("ProjectTaskStatus", PredefinedObjectType.PROJECTTASKSTATUS);

                resolver.SetNamedSourceData("Owner", PredefinedObjectType.USER);
                resolver.SetNamedSourceData("Assignee", PredefinedObjectType.USER);

                resolver.RegisterValues(new string[] { "TaskURL", "ProjectTaskDescriptionPlain", "ReminderMessage", "ReminderMessagePlain" });

                mProjectManagementResolver = resolver;
            }

            return mProjectManagementResolver;
        }
    }


    /// <summary>
    /// Boards e-mail template macro resolver.
    /// </summary>
    public static ContextResolver BoardsResolver
    {
        get
        {
            if (mBoardsResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Board", "board.board");
                resolver.RegisterObject("Message", "board.message");
                resolver.RegisterObject("MessageUser", PredefinedObjectType.USER);
                resolver.RegisterObject("MessageUserSettings", SiteObjectType.USERSETTINGS);

                // Register flat values
                resolver.RegisterValues(new string[] { "UnsubscriptionLink", "DocumentLink" });

                mBoardsResolver = resolver;
            }

            return mBoardsResolver;
        }
    }


    /// <summary>
    /// Booking e-mail template macro resolver.
    /// </summary>
    public static ContextResolver BookingResolver
    {
        get
        {
            if (mBookingResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Event", "cms.bookingevent");
                resolver.RegisterObject("Attendee", "cms.eventattendee");

                mBookingResolver = resolver;
            }

            return mBookingResolver;
        }
    }


    /// <summary>
    /// Forums e-mail template macro resolver.
    /// </summary>
    public static ContextResolver ForumsResolver
    {
        get
        {
            if (mForumsResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("ForumPost", PredefinedObjectType.FORUMPOST);
                resolver.RegisterObject("Forum", PredefinedObjectType.FORUM);
                resolver.RegisterObject("ForumGroup", PredefinedObjectType.FORUMGROUP);
                resolver.RegisterObject("Subscriber", "forums.forumsubscription");

                resolver.RegisterValues(new string[] { "ForumDisplayName", "PostSubject", "Link", "ForumName", "PostText", "PostUsername", 
                    "PostTime", "GroupDisplayname", "GroupName", "GroupDescription", "ForumDescription", "UnsubscribeLink" });

                mForumsResolver = resolver;
            }

            return mForumsResolver;
        }
    }


    /// <summary>
    /// Forums subscribtion e-mail template macro resolver.
    /// </summary>
    public static ContextResolver ForumsSubscribtionResolver
    {
        get
        {
            if (mForumsSubscribeResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                // Register objects
                resolver.RegisterObject("ForumPost", PredefinedObjectType.FORUMPOST);
                resolver.RegisterObject("Forum", PredefinedObjectType.FORUM);
                resolver.RegisterObject("ForumGroup", PredefinedObjectType.FORUMGROUP);

                // Register flat values
                resolver.RegisterValues(new string[] { "ForumDisplayName", "Subject", "Link", "UnsubscribeLink", "Separator" });

                // Save the resolver for future use
                mForumsSubscribeResolver = resolver;
            }
            return mForumsSubscribeResolver;
        }
    }


    /// <summary>
    /// Friends e-mail template macro resolver.
    /// </summary>
    public static ContextResolver FriendsResolver
    {
        get
        {
            if (mFriendsResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Sender", PredefinedObjectType.USER);
                resolver.RegisterObject("Recipient", PredefinedObjectType.USER);
                resolver.RegisterObject("Friendship", PredefinedObjectType.FRIEND);

                resolver.RegisterValues(new string[] { "ManagementUrl", "ProfileUrl", "FormatedSenderName", "FormatedRecipientName" });

                mFriendsResolver = resolver;
            }

            return mFriendsResolver;
        }
    }


    /// <summary>
    /// Groups e-mail template macro resolver.
    /// </summary>
    public static ContextResolver GroupMemberResolver
    {
        get
        {
            if (mGroupMemberResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Group", PredefinedObjectType.GROUP);
                resolver.RegisterObject("MemberUser", PredefinedObjectType.USER);

                mGroupMemberResolver = resolver;
            }

            return mGroupMemberResolver;
        }
    }


    /// <summary>
    /// Groups invitation e-mail template macro resolver.
    /// </summary>
    public static ContextResolver GroupsInvitationResolver
    {
        get
        {
            if (mGroupsInvitationResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Invitation", "community.invitation");
                resolver.RegisterObject("Group", PredefinedObjectType.GROUP);

                resolver.RegisterValues(new string[] { "AcceptionURL", "InvitedBy" });

                mGroupsInvitationResolver = resolver;
            }

            return mGroupsInvitationResolver;
        }
    }


    /// <summary>
    /// Groups accepted invitation e-mail template macro resolver.
    /// </summary>
    public static ContextResolver GroupsAcceptedInvitationResolver
    {
        get
        {
            if (mGroupsAcceptedInvitationResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Sender", PredefinedObjectType.USER);
                resolver.RegisterObject("Recipient", PredefinedObjectType.USER);
                resolver.RegisterObject("GroupMember", PredefinedObjectType.GROUPMEMBER);
                resolver.RegisterObject("Group", PredefinedObjectType.GROUP);

                mGroupsAcceptedInvitationResolver = resolver;
            }

            return mGroupsAcceptedInvitationResolver;
        }
    }


    /// <summary>
    /// Password e-mail template macro resolver.
    /// </summary>
    public static ContextResolver PasswordResolver
    {
        get
        {
            if (mPasswordResolver == null)
            {
                ContextResolver resolver = RegistrationResolver.CreateContextChild();

                resolver.RegisterValues(new string[] { "Password" });

                mPasswordResolver = resolver;
            }

            return mPasswordResolver;
        }
    }


    /// <summary>
    /// Forgotten password e-mail template macro resolver.
    /// </summary>
    public static ContextResolver ForgottenPasswordResolver
    {
        get
        {
            if (mForgottenPasswordResolver == null)
            {
                ContextResolver resolver = PasswordResolver.CreateContextChild();

                resolver.RegisterValues(new string[] { "LogonURL" });

                mForgottenPasswordResolver = resolver;
            }

            return mForgottenPasswordResolver;
        }
    }


    /// <summary>
    /// Registration e-mail template macro resolver.
    /// </summary>
    public static ContextResolver RegistrationResolver
    {
        get
        {
            if (mRegistrationResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterValues(new string[] { "FirstName", "LastName", "Email", "UserName" });

                mRegistrationResolver = resolver;
            }

            return mRegistrationResolver;
        }
    }


    /// <summary>
    /// Registration approved e-mail template macro resolver.
    /// </summary>
    public static ContextResolver RegistrationApprovedResolver
    {
        get
        {
            if (mRegistrationApprovedResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterValues(new string[] { "HomePageURL" });

                mRegistrationApprovedResolver = resolver;
            }

            return mRegistrationApprovedResolver;
        }
    }


    /// <summary>
    /// Returns membership registration e-mail template macro resolver.
    /// </summary>
    public static ContextResolver MembershipRegistrationResolver
    {
        get
        {
            if (mMembershipRegistrationResolver == null)
            {
                ContextResolver resolver = PasswordResolver.CreateContextChild();

                resolver.RegisterValues(new string[] { "ConfirmAddress" });

                mMembershipRegistrationResolver = resolver;

            }

            return mMembershipRegistrationResolver;
        }
    }


    /// <summary>
    /// Returns membership expiration e-mail template macro resolver.
    /// </summary>
    public static ContextResolver MembershipExpirationResolver
    {
        get
        {
            if (mMembershipExpirationResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                // Expiring memberships
                resolver.SetNamedSourceData("MembershipsTable", (new DataTable()).Rows);

                mMembershipExpirationResolver = resolver;
            }

            return mMembershipExpirationResolver;
        }
    }


    /// <summary>
    /// Returns messaging e-mail template macro resolver.
    /// </summary>
    public static ContextResolver MessagingResolver
    {
        get
        {
            if (mMessagingResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Sender", PredefinedObjectType.USER);
                resolver.RegisterObject("Recipient", PredefinedObjectType.USER);
                resolver.RegisterObject("Message", "messaging.message");

                mMessagingResolver = resolver;
            }

            return mMessagingResolver;
        }
    }


    /// <summary>
    /// Returns workflow e-mail template macro resolver.
    /// </summary>
    public static ContextResolver WorkflowResolver
    {
        get
        {
            if (mWorkflowResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Document", TreeNode.New("CMS.root"));
                resolver.RegisterObject("User", PredefinedObjectType.USER);
                resolver.RegisterObject("OriginalStep", PredefinedObjectType.WORKFLOWSTEP);
                resolver.RegisterObject("CurrentStep", PredefinedObjectType.WORKFLOWSTEP);

                resolver.RegisterValues(new string[] { "ApplicationURL", "ApprovedBy", "ApprovedWhen", "OriginalStepName", "CurrentStepName", "Comment", "FirstName", "LastName", "UserName", "Email", "FullName", "DocumentPreviewUrl", "DocumentEditUrl" });

                mWorkflowResolver = resolver;
            }

            return mWorkflowResolver;
        }
    }


    /// <summary>
    /// Returns scoring e-mail template macro resolver.
    /// </summary>
    public static ContextResolver ScoringResolver
    {
        get
        {
            if (mScoringResolver == null)
            {
                ContextResolver resolver = ContactResolver.CreateContextChild();

                resolver.RegisterObject("Score", "om.score");

                resolver.RegisterValues(new string[] { "ScoreValue" });

                mScoringResolver = resolver;
            }

            return mScoringResolver;
        }
    }


    /// <summary>
    /// Returns contact resolver.
    /// </summary>
    public static ContextResolver ContactResolver
    {
        get
        {
            if (mContactResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                resolver.RegisterObject("Contact", "OM.Contact");

                mContactResolver = resolver;
            }

            return mContactResolver;
        }
    }

    #endregion


    #region "E-commerce resolvers"

    /// <summary>
    /// Shopping cart resolver.
    /// </summary>
    public static ContextResolver ShoppingCartResolver
    {
        get
        {
            if (mShoppingCartResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                // Data
                resolver.RegisterObject("Order", PredefinedObjectType.ORDER);
                resolver.RegisterObject("OrderStatus", PredefinedObjectType.ORDERSTATUS);
                resolver.RegisterObject("BillingAddress", "ecommerce.address");
                resolver.RegisterObject("ShippingAddress", "ecommerce.address");
                resolver.RegisterObject("CompanyAddress", "ecommerce.address");
                resolver.RegisterObject("ShippingOption", PredefinedObjectType.SHIPPINGOPTION);
                resolver.RegisterObject("PaymentOption", PredefinedObjectType.PAYMENTOPTION);
                resolver.RegisterObject("Currency", PredefinedObjectType.CURRENCY);
                resolver.RegisterObject("Customer", PredefinedObjectType.CUSTOMER);
                resolver.RegisterObject("DiscountCoupon", PredefinedObjectType.DISCOUNTCOUPON);
                resolver.RegisterObject("ShoppingCart", "ecommerce.shoppingcart");

                // Content tables                
                DataTable table = new DataTable();
                resolver.SetNamedSourceData("ContentTable", table.Rows);
                resolver.SetNamedSourceData("ContentTaxesTable", table.Rows);
                resolver.SetNamedSourceData("ShippingTaxesTable", table.Rows);

                // E-product table
                resolver.SetNamedSourceData("EproductsTable", table.Rows);

                mShoppingCartResolver = resolver;
            }

            return mShoppingCartResolver;
        }
    }



    /// <summary>
    /// E-commerce e-mail template macro resolver.
    /// </summary>
    public static ContextResolver EcommerceResolver
    {
        get
        {
            if (mEcommerceResolver == null)
            {
                // Copy all the data from ShoppingCart resolver
                ContextResolver resolver = ShoppingCartResolver.CreateContextChild();

                resolver.RegisterValues(new string[] { "TotalPrice", "TotalShipping", "NewOrderLink" });

                mEcommerceResolver = resolver;
            }

            return mEcommerceResolver;
        }
    }


    /// <summary>
    /// E-commerce expiring e-product e-mail template macro resolver.
    /// </summary>
    public static ContextResolver EcommerceEproductExpirationResolver
    {
        get
        {
            if (mEcommerceEproductExpirationResolver == null)
            {
                ContextResolver resolver = new ContextResolver();

                // Expriring e-products
                resolver.SetNamedSourceData("EproductsTable", (new DataTable()).Rows);

                mEcommerceEproductExpirationResolver = resolver;
            }

            return mEcommerceEproductExpirationResolver;
        }
    }

    #endregion
}
