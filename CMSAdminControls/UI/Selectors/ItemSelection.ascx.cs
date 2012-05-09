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
using System.ComponentModel;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Selectors_ItemSelection : CMSUserControl
{
    #region "Variables"

    private string[,] mLeftItems = null;
    private string[,] mRightItems = null;

    private bool mShowUpDownButtons = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Left column label.
    /// </summary>
    public Label LeftColumnLabel
    {
        get
        {
        	 return lblLeftColumn; 
        }
        set
        {
        	 lblLeftColumn = value; 
        }
    }


    /// <summary>
    /// Right column label.
    /// </summary>
    public Label RightColumnLabel
    {
        get
        {
        	 return lblRightColumn; 
        }
        set
        {
        	 lblRightColumn = value; 
        }
    }


    /// <summary>
    /// Right column list box.
    /// </summary>
    public ListBox RightColumListBox
    {
        get
        {
        	 return lstRightColumn; 
        }
        set
        {
        	 lstRightColumn = value; 
        }
    }


    /// <summary>
    /// Left column list box.
    /// </summary>
    public ListBox LeftColumListBox
    {
        get
        {
        	 return lstLeftColumn; 
        }
        set
        {
        	 lstLeftColumn = value; 
        }
    }


    /// <summary>
    /// Moves right button.
    /// </summary>
    public CMSButton MoveRightButton
    {
        get
        {
        	 return btnMoveRight; 
        }
        set
        {
        	 btnMoveRight = value; 
        }
    }


    /// <summary>
    /// Moves left button.
    /// </summary>
    public CMSButton MoveLeftButton
    {
        get
        {
        	 return btnMoveLeft; 
        }
        set
        {
        	 btnMoveLeft = value; 
        }
    }


    /// <summary>
    /// Left column items.
    /// </summary>
    [Category("Data"), Description("Left column items.")]
    public string[,] LeftItems
    {
        get
        {
        	 return mLeftItems; 
        }
        set
        {
        	 mLeftItems = value; 
        }
    }


    /// <summary>
    /// Right column items.
    /// </summary>
    [Category("Data"), Description("Right column items.")]
    public string[,] RightItems
    {
        get
        {
        	 return mRightItems; 
        }
        set
        {
        	 mRightItems = value; 
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show up / down buttons.
    /// </summary>
    public bool ShowUpDownButtons
    {
        get
        {
            return this.mShowUpDownButtons;
        }
        set
        {
            this.mShowUpDownButtons = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.mShowUpDownButtons)
        {
            this.btnDown.Visible = false;
            this.btnUp.Visible = false;
        }
        else
        {
            btnUp.Text = GetString("ItemSelection.btnUp");
            btnDown.Text = GetString("ItemSelection.btnDown");

            btnDown.Enabled = false;
            btnUp.Enabled = false;
        }

        plcButtons.Visible = this.mShowUpDownButtons;
        if (!RequestHelper.IsPostBack())
        {
            fill();
        }
        else
        {
            ChangeProperties();
        }
    }


    /// <summary>
    /// Fill Column list boxs.
    /// </summary>
    public void fill()
    {
        this.lstLeftColumn.Items.Clear();
        this.lstRightColumn.Items.Clear();
        // Fill left column
        if (mLeftItems != null)
        {
            for (int i = LeftItems.GetLowerBound(0); i <= LeftItems.GetUpperBound(0); i++)
            {
                ListItem li = new ListItem(LeftItems[i, 1], LeftItems[i, 0]);

                this.lstLeftColumn.Items.Add(li);
            }
        }

        // Fill right column
        if (mRightItems != null)
        {
            for (int i = RightItems.GetLowerBound(0); i <= RightItems.GetUpperBound(0); i++)
            {
                ListItem li = new ListItem(RightItems[i, 1], RightItems[i, 0]);
                this.lstRightColumn.Items.Add(li);
            }
        }
    }


    /// <summary>
    /// Moves selected items from left column to right column.
    /// </summary>
    protected void MoveRightButton_Click(object sender, EventArgs e)
    {
        if ((this.LeftItems != null) && (this.LeftItems.Length > 0))
        {
            string args = "";

            for (int i = 0; i < this.lstLeftColumn.Items.Count; i++)
            {
                if (lstLeftColumn.Items[i].Selected == true)
                {
                    args += ";" + lstLeftColumn.Items[i].Value;
                    lstLeftColumn.Items[i].Selected = false;
                    this.lstRightColumn.Items.Add(lstLeftColumn.Items[i]);
                    this.lstLeftColumn.Items.Remove(lstLeftColumn.Items[i]);
                    i--;
                }
            }
            ChangeProperties();

            // Raise the event
            if (OnMoveRight != null)
            {
                OnMoveRight(sender, new CommandEventArgs("roleid", args));
            }
        }
    }


    /// <summary>
    /// Moves Selected items from right column to left column.
    /// </summary>
    protected void MoveLeftButton_Click(object sender, EventArgs e)
    {
        if ((this.RightItems != null) && (this.RightItems.Length > 0))
        {
            string args = "";

            for (int i = 0; i < lstRightColumn.Items.Count; i++)
            {
                if (lstRightColumn.Items[i].Selected == true)
                {
                    args += ";" + lstRightColumn.Items[i].Value;
                    lstRightColumn.Items[i].Selected = false;
                    this.lstLeftColumn.Items.Add(lstRightColumn.Items[i]);
                    this.lstRightColumn.Items.Remove(lstRightColumn.Items[i]);
                    i--;
                }
            }
            btnDown.Enabled = false;
            btnUp.Enabled = false;
            ChangeProperties();

            // Raise the event
            if (OnMoveLeft != null)
            {
                OnMoveLeft(sender, new CommandEventArgs("roleid", args));
            }
        }
    }


    /// <summary>
    /// Change data in LeftItems and RightItems.
    /// </summary>
    public void ChangeProperties()
    {
        string[,] mLeft = new string[lstLeftColumn.Items.Count, 2];
        string[,] mRight = new string[lstRightColumn.Items.Count, 2];

        // LeftItems properties
        for (int i = 0; i < lstLeftColumn.Items.Count; i++)
        {
            mLeft[i, 0] = lstLeftColumn.Items[i].Value;
            mLeft[i, 1] = lstLeftColumn.Items[i].Text;
        }

        // RightItems properties
        for (int i = 0; i < lstRightColumn.Items.Count; i++)
        {
            mRight[i, 0] = lstRightColumn.Items[i].Value;
            mRight[i, 1] = lstRightColumn.Items[i].Text;
        }

        // Save it
        if (lstLeftColumn.Items.Count > 0)
            LeftItems = mLeft;
        else
        {
        	LeftItems = null;
        }

        if (lstRightColumn.Items.Count > 0)
            RightItems = mRight;
        else
        {
        	RightItems = null;
        }

        if (lstRightColumn.SelectedItem != null)
        {
            btnDown.Enabled = true;
            btnUp.Enabled = true;
        }
    }

    #region "Handle event"

    public event CommandEventHandler OnMoveLeft;
    public event CommandEventHandler OnMoveRight;
    public event EventHandler OnPostBack;


    public void lstRightColumn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnPostBack != null)
        {
            OnPostBack(sender, e);
        }
    }
    #endregion

    /// <summary>
    /// MoveUp selected column.
    /// </summary>
    protected void btnUp_Click(object sender, EventArgs e)
    {
        if (lstRightColumn.SelectedItem != null && lstRightColumn.SelectedIndex > 0)
        {
            int index = lstRightColumn.SelectedIndex;

            lstRightColumn.Items[index].Selected = false;
            lstRightColumn.Items[index - 1].Selected = false;

            ListItem mDown = new ListItem(lstRightColumn.Items[index - 1].Text, lstRightColumn.Items[index - 1].Value);
            ListItem mUp = new ListItem(lstRightColumn.Items[index].Text, lstRightColumn.Items[index].Value);

            lstRightColumn.Items[index].Text = mDown.Text;
            lstRightColumn.Items[index].Value = mDown.Value;
            lstRightColumn.Items[index - 1].Text = mUp.Text;
            lstRightColumn.Items[index - 1].Value = mUp.Value;


            string mItem = "";

            mItem = RightItems[index, 0];
            RightItems[index, 0] = RightItems[index - 1, 0];
            RightItems[index - 1, 0] = mItem;
            mItem = RightItems[index, 1];
            RightItems[index, 1] = RightItems[index - 1, 1];
            RightItems[index - 1, 1] = mItem;

            lstRightColumn.Items[index - 1].Selected = true;
        }
    }


    /// <summary>
    /// MoveDown selected column.
    /// </summary>
    protected void btnDown_Click(object sender, EventArgs e)
    {
        if (lstRightColumn.SelectedItem != null && lstRightColumn.SelectedIndex < lstRightColumn.Items.Count - 1)
        {
            int index = lstRightColumn.SelectedIndex;

            lstRightColumn.Items[index].Selected = false;
            lstRightColumn.Items[index + 1].Selected = false;

            ListItem mDown = new ListItem(lstRightColumn.Items[index + 1].Text, lstRightColumn.Items[index + 1].Value);
            ListItem mUp = new ListItem(lstRightColumn.Items[index].Text, lstRightColumn.Items[index].Value);

            lstRightColumn.Items[index].Text = mDown.Text;
            lstRightColumn.Items[index].Value = mDown.Value;
            lstRightColumn.Items[index + 1].Text = mUp.Text;
            lstRightColumn.Items[index + 1].Value = mUp.Value;

            string mItem = "";

            mItem = RightItems[index, 0];
            RightItems[index, 0] = RightItems[index + 1, 0];
            RightItems[index + 1, 0] = mItem;
            mItem = RightItems[index, 1];
            RightItems[index, 1] = RightItems[index + 1, 1];
            RightItems[index + 1, 1] = mItem;

            lstRightColumn.Items[index + 1].Selected = true;
        }
    }
}
