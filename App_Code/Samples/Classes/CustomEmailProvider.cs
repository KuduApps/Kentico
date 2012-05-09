using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.EventLog;
using CMS.EmailEngine;
using System.Net.Mail;
using System.ComponentModel;

/// <summary>
/// Sample customized e-mail provider.
/// </summary>
public class CustomEmailProvider : EmailProvider
{
    /// <summary>
    /// Synchronously sends an e-mail through the SMTP server.
    /// </summary>
    /// <param name="siteName">Site name</param>
    /// <param name="message">E-mail message</param>
    /// <param name="smtpServer">SMTP server</param>
    protected override void SendEmailInternal(string siteName, MailMessage message, SMTPServerInfo smtpServer)
    {
        base.SendEmailInternal(siteName, message, smtpServer);

        string detail = string.Format("E-mail from {0} through {1} was sent (synchronously)", message.From.Address, smtpServer.ServerName);

        EventLogProvider.LogInformation("CMSCustom", "MyCustomEmailProvider", detail);
    }


    /// <summary>
    /// Asynchronously sends an e-mail through the SMTP server.
    /// </summary>
    /// <param name="siteName">Site name</param>
    /// <param name="message">E-mail message</param>
    /// <param name="smtpServer">SMTP server</param>
    /// <param name="emailToken">E-mail token that represents the message being sent</param>
    protected override void SendEmailAsyncInternal(string siteName, MailMessage message, SMTPServerInfo smtpServer, EmailToken emailToken)
    {
        base.SendEmailAsyncInternal(siteName, message, smtpServer, emailToken);

        string detail = string.Format("E-mail from {0} through {1} was dispatched (asynchronously)", message.From.Address, smtpServer.ServerName);

        EventLogProvider.LogInformation("CMSCustom", "MyCustomEmailProvider", detail);
    }


    /// <summary>
    /// Raises the SendCompleted event after the send is completed.
    /// </summary>
    /// <param name="e">Provides data for async SendCompleted event</param>
    protected override void OnSendCompleted(AsyncCompletedEventArgs e)
    {
        base.OnSendCompleted(e);

        string detail = "Received callback from asynchronosu dispatch";

        EventLogProvider.LogInformation("CMSCustom", "MyCustomEmailProvider", detail);
    }
}