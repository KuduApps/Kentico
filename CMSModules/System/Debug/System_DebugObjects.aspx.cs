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
using System.Net;
using System.Net.Mail;
using System.Threading;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.EmailEngine;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.UIControls;

public partial class CMSModules_System_Debug_System_DebugObjects : CMSDebugPage
{
    protected string cmsVersion = null;
    protected int index = 0;

    protected int totalObjects = 0;
    protected int totalTableObjects = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        cmsVersion = GetString("Footer.Version") + "&nbsp;" + CMSContext.GetFriendlySystemVersion(true);

        this.gridObjects.Columns[0].HeaderText = GetString("General.ObjectType");
        this.gridObjects.Columns[1].HeaderText = GetString("General.Count");

        this.gridHashtables.Columns[0].HeaderText = GetString("Administration-System.CacheName");
        this.gridHashtables.Columns[1].HeaderText = GetString("General.Count");

        this.btnClear.Text = GetString("Administration-System.ClearHashtables");

        ReloadData();
    }


    protected void ReloadData()
    {
        // Hashtables
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("TableName", typeof(string)));
        dt.Columns.Add(new DataColumn("ObjectCount", typeof(int)));

        lock (ProviderStringDictionary.Dictionaries)
        {
            // Hashtables
            foreach (DictionaryEntry item in ProviderStringDictionary.Dictionaries)
            {
                if (item.Value is IProviderDictionary)
                {
                    IProviderDictionary dict = (IProviderDictionary)item.Value;

                    DataRow dr = dt.NewRow();
                    string resStringKey = "HashTableName." + ValidationHelper.GetIdentifier(item.Key);
                    if (resStringKey.Length > 100)
                    {
                        resStringKey = resStringKey.Substring(0, 100);
                    }

                    dr["TableName"] = GetString(resStringKey);
                    dr["ObjectCount"] = dict.Count;

                    dt.Rows.Add(dr);
                }
            }
        }

        dt.DefaultView.Sort = "TableName ASC";

        this.gridHashtables.DataSource = dt.DefaultView;
        this.gridHashtables.DataBind();

        // Objects
        if (TypeInfo.TrackObjectInstances)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("ObjectType", typeof(string)));
            dt.Columns.Add(new DataColumn("ObjectCount", typeof(int)));

            foreach (TypeInfo info in TypeInfo.TypeInfos.Values)
            {
                DataRow dr = dt.NewRow();
                dr["ObjectType"] = info.ObjectType;

                // Get the instances
                IList<BaseInfo> instances = info.GetInstances();
                dr["ObjectCount"] = instances.Count;

                dt.Rows.Add(dr);
            }

            dt.DefaultView.Sort = "ObjectType ASC";

            this.gridObjects.DataSource = dt.DefaultView;
            this.gridObjects.DataBind();
        }
    }


    protected string GetCount(object count)
    {
        int cnt = ValidationHelper.GetInteger(count, 0);
        totalObjects += cnt;

        return cnt.ToString();
    }


    protected string GetTableCount(object count)
    {
        int cnt = ValidationHelper.GetInteger(count, 0);
        totalTableObjects += cnt;

        return cnt.ToString();
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        Functions.ClearHashtables();

        // Collect the memory
        GC.Collect();
        GC.WaitForPendingFinalizers();

        ReloadData();
    }
}
