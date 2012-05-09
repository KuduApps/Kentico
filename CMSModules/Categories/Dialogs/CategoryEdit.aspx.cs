using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_Categories_Dialogs_CategoryEdit : CMSModalPage
{
    private int categoryId = 0;
    private int parentCategoryId = -1;
    private bool createPersonal = true;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        categoryId = QueryHelper.GetInteger("categoryId", 0);
        parentCategoryId = QueryHelper.GetInteger("parentId", -1);
        createPersonal = QueryHelper.GetBoolean("personal", true);

        catEdit.OnSaved += new EventHandler(catEdit_OnSaved);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string titleText = GetString("categories.properties");
        if (categoryId == 0)
        {
            catEdit.ParentCategoryID = parentCategoryId;
            catEdit.UserID = createPersonal ? CMSContext.CurrentUser.UserID : 0;
            
            // Prevent creating disabled category
            catEdit.ShowEnabled = false;

            titleText = GetString("multiplecategoriesselector.newcategory");
        }
        else
        {
            catEdit.CategoryID = categoryId;
        }

        // Init buttons
        btnCancel.Attributes.Add("onclick", "window.close();");
        btnOk.Click += new EventHandler(catEdit.btnSaveCategory_Click);

        CurrentMaster.Title.TitleText = titleText;
        CurrentMaster.Title.TitleImage = GetImageUrl("/CMSModules/CMS_Categories/module.png");
        Page.Title = titleText;
    }


    void catEdit_OnSaved(object sender, EventArgs e)
    {
        string param = "edit";
        if (categoryId == 0)
        {
            param = "new|" + catEdit.CategoryID;
        }

        this.ltlScript.Text = ScriptHelper.GetScript("wopener.Refresh('" + param + "'); window.close();");
    }
}

