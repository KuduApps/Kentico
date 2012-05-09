using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.SettingsProvider;
using CMS.UIControls;

using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;

public partial class CMSAdminControls_SpellChecker_SpellCheck : CMSUserControl
{
    #region "Variables"

    private bool mDictExists = false;
    private Spelling SpellChecker;
    private WordDictionary WordDictionary;
    private string wordCounterString = null;

    #endregion


    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        string cacheName = "WordDictionary|" + CMSContext.PreferredCultureCode;
        // Get dictionary from cache
        this.WordDictionary = (WordDictionary)HttpContext.Current.Cache[cacheName];
        if (this.WordDictionary == null)
        {
            // If not in cache, create new
            this.WordDictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary();
            this.WordDictionary.EnableUserFile = false;

            // Getting folder for dictionaries
            string folderName = "~/App_Data/Dictionaries";
            folderName = this.MapPath(folderName);

            string culture = CMSContext.PreferredCultureCode;
            string dictFile = culture + ".dic";
            string dictPath = Path.Combine(folderName, dictFile);
            if (!File.Exists(dictPath))
            {
                // Get default culture
                string defaultCulture = ValidationHelper.GetString(SettingsHelper.AppSettings["CMSDefaultSpellCheckerCulture"], "");
                if (defaultCulture != "")
                {
                    culture = defaultCulture;
                    dictFile = defaultCulture + ".dic";
                    dictPath = Path.Combine(folderName, dictFile);
                }
            }

            if (!File.Exists(dictPath))
            {
                this.lblError.Text = string.Format(GetString("SpellCheck.DictionaryNotExists"), culture);
                this.lblError.Visible = true;
                return;
            }

            mDictExists = true;
            this.WordDictionary.DictionaryFolder = folderName;
            this.WordDictionary.DictionaryFile = dictFile;


            // Load and initialize the dictionary
            this.WordDictionary.Initialize();

            // Store the Dictionary in cache
            HttpContext.Current.Cache.Insert(cacheName, this.WordDictionary, new CacheDependency(Path.Combine(folderName, this.WordDictionary.DictionaryFile)));
        }
        else
        {
            mDictExists = true;
        }

        // Create spell checker
        this.SpellChecker = new NetSpell.SpellChecker.Spelling();
        this.SpellChecker.ShowDialog = false;
        this.SpellChecker.Dictionary = this.WordDictionary;

        // Adding events
        this.SpellChecker.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
        this.SpellChecker.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
        this.SpellChecker.DoubledWord += new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(this.SpellChecker_DoubledWord);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        wordCounterString = GetString("SpellCheck.wordCounterString");

        ScriptHelper.RegisterWOpenerScript(this.Page);
        ScriptHelper.RegisterSpellChecker(this.Page);

        // Control initialization
        IgnoreButton.Text = GetString("SpellCheck.IgnoreButton");
        IgnoreAllButton.Text = GetString("SpellCheck.IgnoreAllButton");
        ReplaceButton.Text = GetString("SpellCheck.ReplaceButton");
        ReplaceAllButton.Text = GetString("SpellCheck.ReplaceAllButton");
        StatusText.Text = GetString("SpellCheck.StatusText");
        RemoveButton.Text = GetString("general.remove");

        lblNotInDictionary.Text = GetString("SpellCheck.lblNotInDictionary");
        lblChangeTo.Text = GetString("SpellCheck.lblChangeTo");
        lblSuggestions.Text = GetString("SpellCheck.lblSuggestions");
        btnCancel.Text = GetString("general.cancel");

        if (mDictExists)
        {
            // Add client side events
            this.Suggestions.Attributes.Add("onChange", "javascript: changeWord(this);");

            // Load spell checker settings
            this.LoadValues();
            switch (this.SpellMode.Value)
            {
                case "start":
                    this.EnableButtons();
                    this.SpellChecker.SpellCheck();
                    break;

                case "suggest":
                    this.EnableButtons();
                    break;

                case "load":
                case "end":
                default:
                    this.DisableButtons();
                    break;
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Set browser class
        this.SpellingBody.Attributes.Add("class", CMSContext.GetBrowserClass());

        // Register the scripts
        ScriptHelper.RegisterStartupScript(this, typeof(string), "Init_" + ClientID, ScriptHelper.GetScript("initialize(\"" + WordIndex.ClientID + "\", \"" + CurrentText.ClientID + "\", \"" + ElementIndex.ClientID + "\", \"" + SpellMode.ClientID + "\", \"" + StatusText.ClientID + "\", \"" + ReplacementWord.ClientID + "\");"));
    }

    #endregion


    #region "Methods"

    protected void EnableButtons()
    {
        this.IgnoreButton.Enabled = true;
        this.IgnoreAllButton.Enabled = true;
        this.btnAdd.Enabled = true;
        this.ReplaceButton.Enabled = true;
        this.ReplaceAllButton.Enabled = true;
        this.ReplacementWord.Enabled = true;
        this.Suggestions.Enabled = true;
        this.RemoveButton.Enabled = true;
    }


    protected void DisableButtons()
    {
        this.IgnoreButton.Enabled = false;
        this.IgnoreAllButton.Enabled = false;
        this.btnAdd.Enabled = false;
        this.ReplaceButton.Enabled = false;
        this.ReplaceAllButton.Enabled = false;
        this.ReplacementWord.Enabled = false;
        this.Suggestions.Enabled = false;
        this.RemoveButton.Enabled = false;
    }


    protected void SaveValues()
    {
        this.CurrentText.Value = this.SpellChecker.Text;
        this.WordIndex.Value = this.SpellChecker.WordIndex.ToString();

        // Save ignore words
        string[] ignore = (string[])this.SpellChecker.IgnoreList.ToArray(typeof(string));

        this.IgnoreList.Value = String.Join("|", ignore);

        // Save replace words
        ArrayList tempArray = new ArrayList(this.SpellChecker.ReplaceList.Keys);
        string[] replaceKey = (string[])tempArray.ToArray(typeof(string));

        this.ReplaceKeyList.Value = String.Join("|", replaceKey);
        tempArray = new ArrayList(this.SpellChecker.ReplaceList.Values);

        string[] replaceValue = (string[])tempArray.ToArray(typeof(string));

        this.ReplaceValueList.Value = String.Join("|", replaceValue);

        // Saving user words
        tempArray = new ArrayList(this.SpellChecker.Dictionary.UserWords.Keys);

        string[] userWords = (string[])tempArray.ToArray(typeof(string));

        CookieHelper.SetValue("UserWords", String.Join("|", userWords), "/", DateTime.Now.AddMonths(1));
    }


    protected void LoadValues()
    {
        if (this.CurrentText.Value.Length > 0)
            this.SpellChecker.Text = this.CurrentText.Value;

        if (this.WordIndex.Value.Length > 0)
            this.SpellChecker.WordIndex = int.Parse(this.WordIndex.Value);

        // Restore ignore list
        if (this.IgnoreList.Value.Length > 0)
        {
            this.SpellChecker.IgnoreList.Clear();
            this.SpellChecker.IgnoreList.AddRange(this.IgnoreList.Value.Split('|'));
        }

        LoadIgnoreList();

        // Restore replace list
        if (this.ReplaceKeyList.Value.Length > 0 && this.ReplaceValueList.Value.Length > 0)
        {
            string[] replaceKeys = this.ReplaceKeyList.Value.Split('|');
            string[] replaceValues = this.ReplaceValueList.Value.Split('|');

            this.SpellChecker.ReplaceList.Clear();
            if (replaceKeys.Length == replaceValues.Length)
            {
                for (int i = 0; i < replaceKeys.Length; i++)
                {
                    if (replaceKeys[i].Length > 0)
                        this.SpellChecker.ReplaceList.Add(replaceKeys[i], replaceValues[i]);
                }
            }
        }

        // Restore user words
        this.SpellChecker.Dictionary.UserWords.Clear();
        if (CookieHelper.RequestCookieExists("UserWords"))
        {
            string[] userWords = CookieHelper.GetValue("UserWords").Split('|');

            for (int i = 0; i < userWords.Length; i++)
            {
                if (userWords[i].Length > 0)
                    this.SpellChecker.Dictionary.UserWords.Add(userWords[i], userWords[i]);
            }
        }
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Loads Ignore list with special characters which should not be checked by spell checker.
    /// </summary>
    protected void LoadIgnoreList()
    {
        string ignoreList = "quot ETH Ntilde Ograve Oacute Ocirc Otilde Ouml times Oslash Ugrave Uacute Ucirc Uuml Yacute THORN szlig agrave aacute acirc atilde auml aring aelig ccedil egrave eacute ecirc euml igrave iacute icirc iuml eth ntilde ograve oacute ocirc otilde ouml divide oslash ugrave uacute ucirc uuml yacute thorn yuml OElig oelig Scaron scaron Yuml fnof circ tilde Alpha Beta Gamma Delta Epsilon Zeta Eta Theta Iota Kappa Lambda Mu Nu Xi Omicron Pi Rho Sigma Tau Upsilon Phi Chi Psi Omega alpha beta gamma delta epsilon zeta eta theta iota kappa lambda mu nu xi omicron pi rho sigmaf sigma tau upsilon phi chi psi omega thetasym upsih piv ensp emsp thinsp zwnj zwj lrm rlm ndash mdash lsquo rsquo sbquo ldquo rdquo bdquo dagger Dagger bull hellip permil prime Prime lsaquo rsaquo oline frasl euro image weierp real trade alefsym larr uarr rarr darr harr crarr lArr uArr rArr dArr hArr forall part exist empty nabla isin notin ni prod sum minus lowast radic prop infin ang and or cap cup int there4 sim cong asymp ne equiv le ge sub sup nsub sube supe oplus otimes perp sdot lceil rceil lfloor rfloor lang rang loz spades clubs hearts diams quot apos amp gt lt nbsp iexcl iquest raquo laquo cent copy micro middot para plusmn pound reg sect yen cedil macr uml curren brvbar ordf ordm sup1 frac12 frac14 frac34 sup2 sup3";

        string[] ignoreArr = ignoreList.Split(new char[] { ' ' });

        foreach (string ingoreItem in ignoreArr)
        {
            this.SpellChecker.IgnoreList.Add(ingoreItem);
        }
    }


    protected void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
    {
        this.SaveValues();
        lblNotInDictionary.Text = GetString("SpellCheck.DoubledWord");
        this.CurrentWord.Text = this.SpellChecker.CurrentWord;
        this.Suggestions.Items.Clear();
        this.ReplacementWord.Text = string.Empty;
        this.SpellMode.Value = "suggest";
        this.StatusText.Text = string.Format(wordCounterString, this.SpellChecker.WordIndex + 1, this.SpellChecker.WordCount);
    }


    protected void SpellChecker_EndOfText(object sender, System.EventArgs e)
    {
        this.SaveValues();
        this.SpellMode.Value = "end";
        this.DisableButtons();
        this.StatusText.Text = string.Format(wordCounterString, this.SpellChecker.WordIndex + 1, this.SpellChecker.WordCount);
    }


    protected void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
    {
        this.SaveValues();
        this.CurrentWord.Text = this.SpellChecker.CurrentWord;
        this.SpellChecker.Suggest();
        this.Suggestions.DataSource = this.SpellChecker.Suggestions;
        this.Suggestions.DataBind();
        this.ReplacementWord.Text = string.Empty;
        this.SpellMode.Value = "suggest";
        this.StatusText.Text = string.Format(wordCounterString, this.SpellChecker.WordIndex + 1, this.SpellChecker.WordCount);
    }


    protected void IgnoreButton_Click(object sender, EventArgs e)
    {
        this.SpellChecker.IgnoreWord();
        this.SpellChecker.SpellCheck();
    }


    protected void IgnoreAllButton_Click(object sender, EventArgs e)
    {
        this.SpellChecker.IgnoreAllWord();
        this.SpellChecker.SpellCheck();
    }


    protected void AddButton_Click(object sender, EventArgs e)
    {
        this.SpellChecker.Dictionary.Add(this.SpellChecker.CurrentWord);
        this.SpellChecker.SpellCheck();
    }


    protected void RemoveButton_Click(object sender, EventArgs e)
    {
        this.SpellChecker.ReplaceWord(String.Empty);
        this.CurrentText.Value = this.SpellChecker.Text;
        this.SpellChecker.SpellCheck();
    }


    protected void ReplaceButton_Click(object sender, EventArgs e)
    {
        this.SpellChecker.ReplaceWord(this.ReplacementWord.Text);
        this.CurrentText.Value = this.SpellChecker.Text;
        this.SpellChecker.SpellCheck();
    }


    protected void ReplaceAllButton_Click(object sender, EventArgs e)
    {
        this.SpellChecker.ReplaceAllWord(this.ReplacementWord.Text);
        this.CurrentText.Value = this.SpellChecker.Text;
        this.SpellChecker.SpellCheck();
    }

    #endregion
}