using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.MessageBoard;
using CMS.TreeEngine;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

[Title(Text = "Message boards", ImageUrl = "CMSModules/CMS_MessageBoards/module.png")]
public partial class CMSAPIExamples_Code_Community_MessageBoards_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.MessageBoards);

        // Message board
        this.apiCreateMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateMessageBoard);
        this.apiGetAndUpdateMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateMessageBoard);
        this.apiGetAndBulkUpdateMessageBoards.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateMessageBoards);
        this.apiDeleteMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteMessageBoard);

        // Message
        this.apiCreateMessage.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateMessage);
        this.apiGetAndUpdateMessage.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateMessage);
        this.apiGetAndBulkUpdateMessages.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateMessages);
        this.apiDeleteMessage.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteMessage);

        // Message board subscription
        this.apiCreateMessageBoardSubscription.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateMessageBoardSubscription);
        this.apiGetAndUpdateMessageBoardSubscription.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateMessageBoardSubscription);
        this.apiGetAndBulkUpdateMessageBoardSubscriptions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateMessageBoardSubscriptions);
        this.apiDeleteMessageBoardSubscription.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteMessageBoardSubscription);

        // Role on message board
        this.apiAddRoleToMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddRoleToMessageBoard);
        this.apiRemoveRoleFromMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveRoleFromMessageBoard);

        // Message board moderator
        this.apiAddModeratorToMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddModeratorToMessageBoard);
        this.apiRemoveModeratorFromMessageBoard.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveModeratorFromMessageBoard);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Message board
        this.apiCreateMessageBoard.Run();
        this.apiGetAndUpdateMessageBoard.Run();
        this.apiGetAndBulkUpdateMessageBoards.Run();

        // Message
        this.apiCreateMessage.Run();
        this.apiGetAndUpdateMessage.Run();
        this.apiGetAndBulkUpdateMessages.Run();

        // Message board subscription
        this.apiCreateMessageBoardSubscription.Run();
        this.apiGetAndUpdateMessageBoardSubscription.Run();
        this.apiGetAndBulkUpdateMessageBoardSubscriptions.Run();

        // Role on message board
        this.apiAddRoleToMessageBoard.Run();

        // Message board moderator
        this.apiAddModeratorToMessageBoard.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Message board moderator
        this.apiRemoveModeratorFromMessageBoard.Run();

        // Role on message board
        this.apiRemoveRoleFromMessageBoard.Run();

        // Message board subscription
        this.apiDeleteMessageBoardSubscription.Run();

        // Message
        this.apiDeleteMessage.Run();

        // Message board
        this.apiDeleteMessageBoard.Run();
    }

    #endregion


    #region "API examples - Message board"

    /// <summary>
    /// Creates message board. Called when the "Create board" button is pressed.
    /// </summary>
    private bool CreateMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Create new message board object
            BoardInfo newBoard = new BoardInfo();

            // Set the properties
            newBoard.BoardDisplayName = "My new board";
            newBoard.BoardName = "MyNewBoard";
            newBoard.BoardDescription = "MyNewBoard";
            newBoard.BoardOpened = true;
            newBoard.BoardEnabled = true;
            newBoard.BoardAccess = 0;
            newBoard.BoardModerated = true;
            newBoard.BoardUseCaptcha = false;
            newBoard.BoardMessages = 0;
            newBoard.BoardEnableSubscriptions = true;
            newBoard.BoardSiteID = CMSContext.CurrentSiteID;
            newBoard.BoardDocumentID = root.DocumentID;

            // Create the message board
            BoardInfoProvider.SetBoardInfo(newBoard);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates message board. Called when the "Get and update board" button is pressed.
    /// Expects the CreateMessageBoard method to be run first.
    /// </summary>
    private bool GetAndUpdateMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo updateBoard = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (updateBoard != null)
            {
                // Update the property
                updateBoard.BoardDisplayName = updateBoard.BoardDisplayName.ToLower();

                // Update the message board
                BoardInfoProvider.SetBoardInfo(updateBoard);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates message boards. Called when the "Get and bulk update boards" button is pressed.
    /// Expects the CreateMessageBoard method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateMessageBoards()
    {
        // Prepare the parameters
        string where = "BoardName LIKE N'MyNewBoard%'";

        // Get the data
        DataSet boards = BoardInfoProvider.GetMessageBoards(where, null);
        if (!DataHelper.DataSourceIsEmpty(boards))
        {
            // Loop through the individual items
            foreach (DataRow boardDr in boards.Tables[0].Rows)
            {
                // Create object from DataRow
                BoardInfo modifyBoard = new BoardInfo(boardDr);

                // Update the property
                modifyBoard.BoardDisplayName = modifyBoard.BoardDisplayName.ToUpper();

                // Update the message board
                BoardInfoProvider.SetBoardInfo(modifyBoard);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes message board. Called when the "Delete board" button is pressed.
    /// Expects the CreateMessageBoard method to be run first.
    /// </summary>
    private bool DeleteMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo deleteBoard = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);

            // Delete the message board
            BoardInfoProvider.DeleteBoardInfo(deleteBoard);

            return (deleteBoard != null);
        }

        return false;
    }

    #endregion


    #region "API examples - Message"

    /// <summary>
    /// Creates message. Called when the "Create message" button is pressed.
    /// </summary>
    private bool CreateMessage()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);

            if (board != null)
            {
                // Create new message object
                BoardMessageInfo newMessage = new BoardMessageInfo();

                // Set the properties
                newMessage.MessageUserName = CMSContext.CurrentUser.UserName;
                newMessage.MessageText = "My new message";
                newMessage.MessageEmail = "administrator@localhost.local";
                newMessage.MessageURL = "";
                newMessage.MessageIsSpam = false;
                newMessage.MessageApproved = true;
                newMessage.MessageInserted = DateTime.Now;
                newMessage.MessageBoardID = board.BoardID;

                // Create the message
                BoardMessageInfoProvider.SetBoardMessageInfo(newMessage);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and updates message. Called when the "Get and update message" button is pressed.
    /// Expects the CreateMessage method to be run first.
    /// </summary>
    private bool GetAndUpdateMessage()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);

            if (board != null)
            {
                // Get the data
                DataSet messages = BoardMessageInfoProvider.GetMessages(board.BoardID);
                if (!DataHelper.DataSourceIsEmpty(messages))
                {
                    // Create object from DataRow
                    BoardMessageInfo updateMessage = new BoardMessageInfo(messages.Tables[0].Rows[0]);

                    // Update the properties
                    updateMessage.MessageText = updateMessage.MessageText.ToLower();

                    // Update the message
                    BoardMessageInfoProvider.SetBoardMessageInfo(updateMessage);

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates messages. Called when the "Get and bulk update messages" button is pressed.
    /// Expects the CreateMessage method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateMessages()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Prepare the parameters
                string where = "MessageBoardID = " + board.BoardID;

                // Get the data
                DataSet messages = BoardMessageInfoProvider.GetMessages(where, null);
                if (!DataHelper.DataSourceIsEmpty(messages))
                {
                    // Loop through the individual items
                    foreach (DataRow messageDr in messages.Tables[0].Rows)
                    {
                        // Create object from DataRow
                        BoardMessageInfo modifyMessage = new BoardMessageInfo(messageDr);

                        // Update the property
                        modifyMessage.MessageText = modifyMessage.MessageText.ToUpper();

                        // Update the message
                        BoardMessageInfoProvider.SetBoardMessageInfo(modifyMessage);
                    }

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes message. Called when the "Delete message" button is pressed.
    /// Expects the CreateMessage method to be run first.
    /// </summary>
    private bool DeleteMessage()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Get the data
                DataSet messages = BoardMessageInfoProvider.GetMessages(board.BoardID);
                if (!DataHelper.DataSourceIsEmpty(messages))
                {
                    // Get the message
                    BoardMessageInfo deleteMessage = new BoardMessageInfo(messages.Tables[0].Rows[0]);

                    // Delete the message
                    BoardMessageInfoProvider.DeleteBoardMessageInfo(deleteMessage);

                    return (deleteMessage != null);
                }
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Message board subscription"

    /// <summary>
    /// Creates message board subscription. Called when the "Create subscription" button is pressed.
    /// Expects the CreateMessageBoard method to be run first.
    /// </summary>
    private bool CreateMessageBoardSubscription()
    {
        // Create new message board subscription object
        BoardSubscriptionInfo newSubscription = new BoardSubscriptionInfo();

        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Set the properties
                newSubscription.SubscriptionBoardID = board.BoardID;
                newSubscription.SubscriptionUserID = CMSContext.CurrentUser.UserID;
                newSubscription.SubscriptionEmail = "Administrator@Localhost.Local";
                newSubscription.SubscriptionLastModified = DateTime.Now;

                // Create the message board subscription
                BoardSubscriptionInfoProvider.SetBoardSubscriptionInfo(newSubscription);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and updates message board subscription. Called when the "Get and update subscription" button is pressed.
    /// Expects the CreateMessageBoardSubscription method to be run first.
    /// </summary>
    private bool GetAndUpdateMessageBoardSubscription()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Get the message board subscription
                BoardSubscriptionInfo updateSubscription = BoardSubscriptionInfoProvider.GetBoardSubscriptionInfo(board.BoardID, CMSContext.CurrentUser.UserID);
                if (updateSubscription != null)
                {
                    // Update the properties
                    updateSubscription.SubscriptionEmail = updateSubscription.SubscriptionEmail.ToLower();

                    // Update the subscription
                    BoardSubscriptionInfoProvider.SetBoardSubscriptionInfo(updateSubscription);

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates message board subscriptions. Called when the "Get and bulk update subscriptions" button is pressed.
    /// Expects the CreateMessageBoardSubscription method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateMessageBoardSubscriptions()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Prepare the parameters
                string where = "SubscriptionBoardID = " + board.BoardID;

                // Get the data
                DataSet subscriptions = BoardSubscriptionInfoProvider.GetSubscriptions(where, null);
                if (!DataHelper.DataSourceIsEmpty(subscriptions))
                {
                    // Loop through the individual items
                    foreach (DataRow subscriptionDr in subscriptions.Tables[0].Rows)
                    {
                        // Create object from DataRow
                        BoardSubscriptionInfo modifySubscription = new BoardSubscriptionInfo(subscriptionDr);

                        // Update the property
                        modifySubscription.SubscriptionEmail = modifySubscription.SubscriptionEmail.ToUpper();

                        // Update the subscription
                        BoardSubscriptionInfoProvider.SetBoardSubscriptionInfo(modifySubscription);
                    }

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes message board subscription. Called when the "Delete subscription" button is pressed.
    /// Expects the CreateMessageBoardSubscription method to be run first.
    /// </summary>
    private bool DeleteMessageBoardSubscription()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Get the message board subscription
                BoardSubscriptionInfo deleteSubscription = BoardSubscriptionInfoProvider.GetBoardSubscriptionInfo(board.BoardID, CMSContext.CurrentUser.UserID);

                // Delete the message board subscription
                BoardSubscriptionInfoProvider.DeleteBoardSubscriptionInfo(deleteSubscription);

                return (deleteSubscription != null);
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Role on message board"

    /// <summary>
    /// Adds role to message board. Called when the button "Add role to board" is pressed.
    /// Expects the method CreateMessageBoard to be run first.
    /// </summary>
    private bool AddRoleToMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);

            // Get the role CMSDeskAdmin
            RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSDeskAdmin", CMSContext.CurrentSite.SiteID);

            if ((board != null) && (role != null))
            {
                // Add role to message board
                BoardRoleInfoProvider.AddRoleToBoard(role.RoleID, board.BoardID);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Removes role from message board. Called when the button "Remove role from board" is pressed.
    /// Expects the method AddRoleToMessageBoard to be run first.
    /// </summary>
    private bool RemoveRoleFromMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);

            // Get the role
            RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSDeskAdmin", CMSContext.CurrentSite.SiteID);

            if ((board != null) && (role != null))
            {
                BoardRoleInfo boardRole = BoardRoleInfoProvider.GetBoardRoleInfo(role.RoleID, board.BoardID);

                if (boardRole != null)
                {
                    // Remove role from message board
                    BoardRoleInfoProvider.DeleteBoardRoleInfo(boardRole);

                    return true;
                }
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Message board moderator"

    /// <summary>
    /// Adds moderator to message board. Called when the button "Add moderator to board" is pressed.
    /// Expects the method CreateMessageBoard to be run first.
    /// </summary>
    private bool AddModeratorToMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                // Add moderator to message board
                BoardModeratorInfoProvider.AddModeratorToBoard(CMSContext.CurrentUser.UserID, board.BoardID);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Removes moderator from message board. Called when the button "Remove moderator from board" is pressed.
    /// Expects the method AddModeratorToMessageBoard to be run first.
    /// </summary>
    private bool RemoveModeratorFromMessageBoard()
    {
        // Get the tree structure
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);
        if (root != null)
        {
            // Get the message board
            BoardInfo board = BoardInfoProvider.GetBoardInfo("MyNewBoard", root.DocumentID);
            if (board != null)
            {
                BoardModeratorInfo boardModerator = BoardModeratorInfoProvider.GetBoardModeratorInfo(CMSContext.CurrentUser.UserID, board.BoardID);

                if (boardModerator != null)
                {
                    // Remove moderator from message board
                    BoardModeratorInfoProvider.DeleteBoardModeratorInfo(boardModerator);

                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}
