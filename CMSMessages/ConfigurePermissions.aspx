<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSMessages_ConfigurePermissions" Theme="Default" CodeFile="ConfigurePermissions.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Kentico CMS is able to perform most operations without writing to disk</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }

        h1
        {
            font-size:15px;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel ID="PanelBody" runat="server" CssClass="PageBody">
            <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader">
                <cms:PageTitle ID="titleElem" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelContent" runat="server" CssClass="PageContent">
                <p>
                    Kentico CMS is able to perform most operations without writing to disk. However,
                    there are situations when the web application needs to write to the disk for optimal
                    operations or performance, such as importing/exporting a site or storing uploaded
                    files in the files system (which is optional).
                </p>
                <p>
                    If you receive an error message from the system saying that the web application
                    cannot write to disk, you need to grant the appropriate user account with Modify
                    permissions on the whole web site.
                </p>
                <h1>
                    User account of the web application
                </h1>
                <p>
                    The web application runs under user account that depends on your environment:
                </p>
                <ol>
                    <li>In <strong>Windows XP</strong>, the user account is the local <strong>ASPNET</strong> account (aspnet_wp)
                        by default. </li>
                    <li>In <strong>Windows 2000 and 2003</strong>, the user account is the local account <strong>NT Authority\Network
                        Service</strong> by default. </li>
                    <li>If you're using <strong>Visual Studio 2005</strong> built-in web server, it is running under
                        your account. </li>
                </ol>
                <p>
                    You can see the name of the user account under which the application runs in <strong>Site
                        Manager -&gt; Administration -&gt; System</strong> dialog.
                </p>
                <h1>
                    Granting user account with Modify permission on Windows XP
                </h1>
                <p>
                    Open Windows Explorer, locate the folder with your web site, right-click the folder
                    and display its <strong>Properties</strong>. Choose the <strong>Security</strong> tab.
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image001.gif" alt="Local disk properties" />
                </p>
                <table border="1" cellspacing="0" style="width: 400px;">
                    <tr>
                        <td style="vertical-align: top; padding: 15px 5px 0px 5px;">
                            <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image002.gif" alt="Paper sheet" /></td>
                        <td style="vertical-align: top; padding: 15px 5px 10px 5px;">
                            <p>
                                <strong>Missing Security tab in folder properties dialog</strong>
                            </p>
                            <p>
                                If you cannot see the Security tab, click <strong>Tools -> Folder options</strong> in the
                                Windows Explorer main menu, choose the <strong>View</strong> tab and uncheck the <strong>Use simple
                                    file sharing</strong> box. Click OK. Now you should find the Security tab in the
                                folder properties dialog.
                            </p>
                            <p>
                                <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image003.gif" alt="Folder options" />
                            </p>
                        </td>
                    </tr>
                </table>
                <p>
                    Click <strong>Add...</strong> The <strong>Select Users, Computers and Groups</strong> dialog appears.</p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image004.gif" alt="Select users, computers, groups" />
                </p>
                <p>
                    Click <strong>Locations...</strong> and choose your local computer:</p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image005.gif" alt="Locations" />
                </p>
                <p>
                    Click <strong>OK</strong>. Enter <strong>aspnet</strong> into the box and click Check Names. The name
                    should be resolved to <strong>&lt;your computer name&gt;\ASPNET</strong>.
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image006.gif" alt="Select users or groups" />
                </p>
                <p>
                    Click <strong>OK</strong>. The account is added to the list of accounts. Grant the account
                    with <strong>Modify</strong> permissions and click <strong>OK</strong>.
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image007.gif" alt="Select users or groups" />
                </p>
                <h1>
                    Granting user account with Modify permission on Windows 2000/2003</h1>
                <p>
                    Open Windows Explorer, locate the folder with your web site, right-click the folder
                    and display its <strong>Properties</strong>. Choose the <strong>Security</strong> tab.
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image008.gif" alt="Properties" />
                </p>
                <p>
                    Click <strong>Add...</strong> The <strong>Select Users, Computers and Groups</strong> dialog appears.</p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image009.gif" alt="Select users, computers, groups" />
                </p>
                <p>
                    Click <strong>Locations...</strong> and choose your local computer:</p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image010.gif" alt="Locations" />
                </p>
                <p>
                    Click <strong>OK</strong>. Enter network service into the box and click Check Names. The name
                    should be resolved to <strong>NETWORK SERVICE</strong>.
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image011.gif" alt="Select users or groups" />
                </p>
                <p>
                    Click <strong>OK</strong>. The account is added to the list of accounts. Grant the account
                    with <strong>Modify</strong> permissions and click <strong>OK</strong>.
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/Permissions/image012.gif" alt="Select users or groups" />
                </p>
                <h1>
                    Choosing the component for directory operations</h1>
                <p>
                    If you're running Kentico CMS under restricted trust level, you may need to use
                    the managed component for directory operations (create/delete/rename directory).
                    You can configure it by setting the following web.config parameter:
                </p>
                <p>
                    &lt;add key="CMSDirectoryProviderAssembly" value="CMS.DirectoryProviderDotNet" /&gt;
                </p>
                <p>
                    If you're running Kentico CMS on a shared hosting server, some providers require
                    that you use the non-managed methods for directory operations:
                </p>
                <p>
                    &lt;add key="CMSDirectoryProviderAssembly" value="CMS.DirectoryProviderWin32" /&gt;
                </p>
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
