using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Threading;
using System.Net;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.DataEngine;

public partial class CMSModules_System_Debug_System_DebugLoad : CMSDebugPage
{
    #region "Variables"

    private static bool mCancel = true;
    private static bool mRun = false;
    private static int mSuccessRequests = 0;
    private static int mErrors = 0;
    private static int mCurrentThreads = 0;

    private static string mUserName = "";
    private static string mDuration = "";
    private static string mIterations = "1000";
    private static string mInterval = "";
    private static string mThreads = "10";
    private static string mUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; MS-RTC LM 8)";
    private static string mURLs = "~/Home.aspx";
    private static bool mSplitURLs = false;

    protected static string mLastError = null;

    #endregion


    #region "Request loader"

    /// <summary>
    /// Request loader class.
    /// </summary>
    protected class RequestLoader
    {
        public string[] URLs = null;
        public DateTime RunUntil = DataHelper.DATETIME_NOT_SELECTED;
        public int NumberOfIterations = -1;
        public int WaitInterval = 0;
        public string UserAgent = null;
        public string UserName = null;


        /// <summary>
        /// Returns true if the loader is canceled (exceeds the execution time, exceeds an allowed number of iterations or is forcibly canceled).
        /// </summary>
        protected bool IsCanceled()
        {
            return mCancel || ((RunUntil != DataHelper.DATETIME_NOT_SELECTED) && (DateTime.Now > RunUntil)) || ((NumberOfIterations != -1) && (NumberOfIterations == 0));
        }


        /// <summary>
        /// Runs the load to the URLs.
        /// </summary>
        public void Run()
        {
            mCurrentThreads++;

            // Prepare the client
            WebClient client = new WebClient();

            // Authenticate specified user
            if (!string.IsNullOrEmpty(UserName))
            {
                client.Headers.Add("Cookie", ".ASPXFORMSAUTH=" + FormsAuthentication.GetAuthCookie(UserName, false).Value);
            }

            // Add user agent header
            if (!string.IsNullOrEmpty(UserAgent))
            {
                client.Headers.Add("user-agent", UserAgent);
            }

            while (!IsCanceled())
            {
                // Run the list of URLs
                foreach (string url in URLs)
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        if (IsCanceled())
                        {
                            break;
                        }

                        // Wait if some interval specified
                        if (WaitInterval > 0)
                        {
                            Thread.Sleep(WaitInterval);
                        }

                        try
                        {
                            // Get the page
                            client.DownloadData(url);

                            mSuccessRequests++;
                        }
                        catch (Exception ex)
                        {
                            mLastError = ex.Message;
                            mErrors++;
                        }
                    }
                }

                // Decrease number of iterations
                if (NumberOfIterations > 0)
                {
                    NumberOfIterations--;
                }
            }

            // Dispose the client
            client.Dispose();

            mCurrentThreads--;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!URLHelper.IsPostback())
        {
            if ((mCurrentThreads > 0) || (mSuccessRequests > 0) || (mErrors > 0))
            {
                this.txtDuration.Text = mDuration;
                this.txtInterval.Text = mInterval;
                this.txtIterations.Text = mIterations;
                this.txtThreads.Text = mThreads;
                this.txtURLs.Text = mURLs;
                this.txtUserAgent.Text = mUserAgent;
                this.userElem.Value = mUserName;
                this.chkSplitUrls.Checked = mSplitURLs;
            }
        }
        else
        {
            if (mRun && (mCurrentThreads == 0))
            {
                // Enable the form when the load finished
                mRun = false;
                EnableAll();
                this.pnlBody.Update();
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.lblInfo.Text = String.Format(GetString("DebugLoad.Info"), mCurrentThreads, mSuccessRequests, mErrors);
        this.lblError.Text = mLastError;

        this.btnStart.Text = GetString("DebugLoad.Generate");
        this.btnStop.Text = GetString("DebugLoad.Stop");
        this.btnReset.Text = GetString("DebugLoad.Reset");

        if (mCurrentThreads > 0)
        {
            DisableAll();
        }
        this.btnStop.Enabled = (mCurrentThreads > 0);
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        mSuccessRequests = 0;
        mErrors = 0;
        mLastError = "";
    }


    protected void btnStop_Click(object sender, EventArgs e)
    {
        mRun = false;
        mCancel = true;
        while (mCurrentThreads > 0)
        {
            Thread.Sleep(100);
        }
        mCurrentThreads = 0;

        EnableAll();
        this.btnStop.Enabled = false;

        mLastError = "";
        this.lblError.Text = "";
    }


    protected void btnStart_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        mLastError = "";
        mCancel = false;
        mSuccessRequests = 0;
        mErrors = 0;
        mRun = true;

        mDuration = this.txtDuration.Text.Trim();
        mDuration = this.txtInterval.Text.Trim();
        mIterations = this.txtIterations.Text.Trim();
        mThreads = this.txtThreads.Text.Trim();
        mURLs = this.txtURLs.Text.Trim();
        mUserAgent = this.txtUserAgent.Text.Trim();
        mUserName = ValidationHelper.GetString(this.userElem.Value, "");
        mSplitURLs = this.chkSplitUrls.Checked;

        if (!String.IsNullOrEmpty(txtURLs.Text))
        {
            // Prepare the parameters
            string[] urls = txtURLs.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < urls.Length; i++)
            {
                urls[i] = URLHelper.GetAbsoluteUrl(urls[i]);
            }

            int newThreads = ValidationHelper.GetInteger(txtThreads.Text, 0);
            if (newThreads > 0)
            {
                int duration = ValidationHelper.GetInteger(txtDuration.Text, 0);
                int interval = ValidationHelper.GetInteger(txtInterval.Text, 0);
                int iterations = ValidationHelper.GetInteger(txtIterations.Text, 0);
                bool splitUrls = ValidationHelper.GetBoolean(chkSplitUrls.Checked, false);

                DateTime runUntil = DateTime.Now.AddSeconds(duration);

                // Divide URLs between threads
                string[][] partUrls = null;
                if (splitUrls)
                {
                    // Do not run more threads than URLs
                    newThreads = Math.Min(urls.Length, newThreads);

                    partUrls = new string[newThreads][];

                    int size = (int)Math.Ceiling((double)urls.Length / newThreads);
                    for (int i = 0; i < newThreads; i++)
                    {
                        size = Math.Min(size, urls.Length - i * size);
                        partUrls[i] = new string[size];
                        for (int j = 0; j < size; j++)
                        {
                            partUrls[i][j] = urls[i * size + j];
                        }
                    }
                }

                // Run specified number of threads
                for (int i = 0; i < newThreads; i++)
                {
                    // Prepare the loader object
                    RequestLoader loader = new RequestLoader();
                    loader.URLs = (splitUrls ? partUrls[i] : urls);
                    loader.WaitInterval = interval;
                    loader.UserAgent = txtUserAgent.Text.Trim();
                    loader.UserName = ValidationHelper.GetString(userElem.Value, "").Trim();
                    if (duration > 0)
                    {
                        loader.RunUntil = runUntil;
                    }
                    if (iterations > 0)
                    {
                        loader.NumberOfIterations = iterations;
                    }

                    // Start new thread
                    CMSThread newThread = new CMSThread(loader.Run);
                    newThread.Start();
                }

                DisableAll();
                this.btnStop.Enabled = true;
                this.btnReset.Enabled = true;
            }
        }
    }


    private void EnableAll()
    {
        this.txtDuration.Enabled = true;
        this.txtInterval.Enabled = true;
        this.txtIterations.Enabled = true;
        this.txtThreads.Enabled = true;
        this.txtURLs.Enabled = true;
        this.txtUserAgent.Enabled = true;
        this.userElem.Enabled = true;
        this.chkSplitUrls.Enabled = true;
        this.btnStart.Enabled = true;
    }


    private void DisableAll()
    {
        this.txtDuration.Enabled = false;
        this.txtInterval.Enabled = false;
        this.txtIterations.Enabled = false;
        this.txtThreads.Enabled = false;
        this.txtURLs.Enabled = false;
        this.txtUserAgent.Enabled = false;
        this.userElem.Enabled = false;
        this.chkSplitUrls.Enabled = false;
        this.btnStart.Enabled = false;
    }
}
