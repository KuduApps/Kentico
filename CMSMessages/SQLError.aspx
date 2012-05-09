<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSMessages_SQLError"
    Theme="Default" CodeFile="SQLError.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>You may encounter problems when entering the database connection details in the
        first step of database setup</title>
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
        
        .border
        {
            border: 1px solid black;
            padding: 5px;
        }
        
        img
        {
            padding: 10px 0px 10px 0px;
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
                    You may encounter problems when entering the database connection details in the
                    first step of database setup:
                </p>
                <p>
                    <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image001.gif" alt="Screen" />
                </p>
                <h1>
                    Error 1: Establishing connection to the server</h1>
                <p>
                    Error message:
                </p>
                <p class="border">
                    An error has occurred while establishing a connection to the server. When connecting
                    to SQL Server 2005, this failure may be caused by the fact that under the default
                    settings SQL Server does not allow remote connections. (provider: Named Pipes Provider,
                    error: 40 - Could not open a connection to SQL Server)
                </p>
                <p>
                    <strong>Troubleshooting:</strong></p>
                <ol>
                    <li>Make sure the SQL Server name or IP address is correct. In some cases, using one
                        of the following values may help:
                        <ul>
                            <li>your computer name</li>
                            <li>localhost</li>
                            <li>127.0.0.1</li>
                            <li>(local)</li>
                        </ul>
                    </li>
                    <li>Make sure the server has Microsoft SQL Server 2000 or 2005 installed and running.
                    </li>
                    <li>Make sure you are using the appropriate instance of the SQL Server in case you are
                        using different instances of SQL Server. The instance name must be entered as myserver\myinstance
                        (please note there's a backslash \). </li>
                    <li>If you're using Microsoft SQL Server Express 2005 with default installation settings,
                        the correct server name is <strong>.\sqlexpress</strong> or <strong>computername\sqlexpress</strong>.
                    </li>
                    <li>Make sure the access to the database server is not blocked by some firewall (the
                        default port number for TCP/IP protocol is 1423). 6. If you're using <strong>SQL Server 2005</strong>
                        (especially the Express Edition), some protocols are disabled by default. You may
                        need to go to <strong>Start menu -&gt; All Programs -&gt; Microsoft SQL Server 2005 -&gt;
                            Configuration Tools</strong> on the computer where the SQL Server is installed and
                        start SQL Server Configuration Manager. Then, go to <strong>SQL Server 2005 Network Configuration</strong>
                        and enable the TCP/IP protocol:
                        <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image003.gif" alt="Configuration manager" />
                    </li>
                    <li>You may also need to enable the TCP/IP protocol in the <strong>SQL Native Client Configuration
                        -> Client Protocols</strong> section:
                        <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image005.gif" alt="Configuration manager" />
                    </li>
                </ol>
                <h1>
                    Error 2: Login failed for user 'xy'</h1>
                <p>
                    Error message:</p>
                <p class="border">
                    Login failed for user 'xy'
                </p>
                <p>
                    <strong>Troubleshooting for SQL Server account</strong></p>
                <p>
                    If you're using SQL Server account with password, make sure you are using a valid
                    user name and password. The login must be created on the server, it must be enabled
                    and granted with permission to connect to the server. You can check the user account
                    in Enterprise Manager/SQL Server Management Studio -&gt; Server -&gt; Security -&gt;
                    Logins:</p>
                <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image007.gif" alt="Logins" />
                <p>
                    Also, check the <strong>Server Properties -&gt; Security</strong> dialog in Enterprise Manager/SQL
                    Server Management Studio and make sure your server supports <strong>SQL Server and Windows
                        Authentication mode</strong>:</p>
                <ul>
                    <li>SQL Server 2000:<br />
                        <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image009.gif" alt="sql2000" />
                    </li>
                    <li>SQL Server 2005:<br />
                        <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image011.gif" alt="sql2005" />
                    </li>
                </ul>
                <p>
                    <strong>Troubleshooting for Windows Authentication account</strong></p>
                <p>
                    If you're using Windows Authentication account, the situation may be a little more
                    complex and may require you to contact your network administrator. The ASP.NET applications
                    run under some particular local or domain account. This current account is displayed
                    on the screen:
                </p>
                <img src="../App_Themes/Default/Images/Others/Messages/Screens/sql/image013.gif" alt="Windows authentication" />
                <p>
                    This account must have its own login with Windows authentication in the SQL Server.
                    You can create the login in <strong>Enterprise Manager\SQL Server Management Studio -&gt;
                        Security -&gt; Logins</strong> and grant it with appropriate permissions on the server.
                    If you SQL Server is located on a different machine than your web server, you may
                    need to configure your web application so that it runs under some domain account,
                    rather than local account so that you can the login in the remote SQL Server.
                </p>
                <p>
                    <strong>If you do not succeed to configure Windows authentication</strong>, you may want to
                    enable Windows and SQL Server Authentication on your SQL Server and use SQL Server
                    account instead. You can learn more about SQL Server authentication in the <strong>Troubleshooting
                        for SQL Server account</strong> section of this chapter.</p>
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
