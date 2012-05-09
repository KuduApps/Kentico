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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.UIControls;

public partial class CMSModules_System_Debug_System_DebugCacheItems : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnClear.Text = GetString("Administration-System.btnClearCache");
        
        titleDummy.TitleText = GetString("Debug.DummyKeys");
        titleItems.TitleText = GetString("Debug.DataItems");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ReloadData();
    }


    /// <summary>
    /// Loads the data.
    /// </summary>
    protected void ReloadData()
    {
        lock (HttpRuntime.Cache)
        {
            List<string> keyList = new List<string>();
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();

            // Build the items list
            while (CacheEnum.MoveNext())
            {
                string key = CacheEnum.Key.ToString();
                if (!String.IsNullOrEmpty(key))
                {
                    keyList.Add(key);
                }
            }
            keyList.Sort();

            // Load the grids
            this.gridItems.AllItems = keyList;
            this.gridItems.ReloadData();

            this.gridDummy.AllItems = keyList;
            this.gridDummy.ReloadData();
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        CMSFunctions.ClearCache();

        ReloadData();
    }
}
