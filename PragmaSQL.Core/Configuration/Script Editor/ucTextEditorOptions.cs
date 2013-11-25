/********************************************************************
  Class      : ucTextEditorOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ICSharpCode.TextEditor;

namespace PragmaSQL.Core
{
  public partial class ucTextEditorOptions : UserControl, IConfigContentEditor
  {
    private bool _isInitializing = false;
    private bool _isModified = false;
    private bool _isContentLoaded = false;
    private Font _selectedFont = null;
    private bool _hasFontChanges = false;

    private TextEditorOptions _textEditorOptions = null;
    public TextEditorOptions TextEditorOptions
    {
      get { return _textEditorOptions; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }

    public ucTextEditorOptions( )
    {
      InitializeComponent();
      InitializeResultViewTypeCombo();
    }

    private void InitializeResultViewTypeCombo( )
    {
      cmbShowResultsAs.Items.Clear();
      Array values = Enum.GetValues(typeof(ResultViewType));
      foreach (ResultViewType type in values)
      {
        cmbShowResultsAs.Items.Add(type);
      }
      cmbShowResultsAs.SelectedIndex = 0;
    }

    #region IConfigurationContentItem Members

    public string ItemClassName
    {
      get
      {
        return "TextEditorOptions";
      }
    }

    public bool ContentLoaded
    {
      get
      {
        return _isContentLoaded;
      }
    }

    public bool Modified
    {
      get
      {
        return _isModified;
      }
    }

    public bool LoadContent( )
    {
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent( ConfigurationContent configContent )
    {
      if (configContent == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (configContent.TextEditorOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain TextEditorOptions item!");
      }

      _configContent = configContent;
      _textEditorOptions = _configContent.TextEditorOptions;
      LoadInitial();
      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      _textEditorOptions.ConvertTabsToSpaces = chkConvertTabsToSpaces.Checked;
      _textEditorOptions.ShowEOLMarkers = chkShowEOLMarkers.Checked;
      _textEditorOptions.ShowInvalidLines = chkShowInvalidLines.Checked;
      _textEditorOptions.ShowLineNumbers = chkShowLineNumbers.Checked;
      _textEditorOptions.ShowMatchingBracket = chkShowMatchingBracket.Checked;
      _textEditorOptions.ShowSpaces = chkShowSpaces.Checked;
			_textEditorOptions.WatchOpenedFiles = chkWatchOpenFiles.Checked;
      _textEditorOptions.AllowCaretBeyondEOL = chkAllowCaretBeyondEOL.Checked;
      _textEditorOptions.ShowTabs = chkShowTabs.Checked;

      _textEditorOptions.IndentStyleDef = cmbIndentStyle.SelectedIndex;
      _textEditorOptions.TabIndent = (int)numfTabIndent.Value;
      _textEditorOptions.VerticalRulerRow = (int)numVRulerRow.Value;
      _textEditorOptions.HighlightSelectAndCase = chkParseSelects.Checked;
      _textEditorOptions.HighlightComments = chkParseComments.Checked;
      _textEditorOptions.ShowFoldMarkers = chkShowFoldMarkers.Checked;
      _textEditorOptions.CustomHighlightersEnabled = chkEnableCustomHighlighter.Checked;
      _textEditorOptions.FoldHighlightedSelectAndCase = chkShowCaseSelectFoldMarkers.Checked;
      _textEditorOptions.FoldComments = chkShowCommentFoldMarkers.Checked;
      
      _textEditorOptions.FoldCodeBlocks = chkFoldCodeBlocks.Checked;

      _textEditorOptions.CaseHighlightColorArgb = panColorCase.BackColor.ToArgb();
      _textEditorOptions.SelectHighlightColorArgb = panColorSelect.BackColor.ToArgb();
			_textEditorOptions.CommentHighlightColorArgb = panColorComment.BackColor.ToArgb();
      
      _textEditorOptions.MarkCaretLine = chkMarkCaretLine.Checked;
      _textEditorOptions.UseAntiAliasedFont = chkUseAntiAliasedFont.Checked;
      _textEditorOptions.AutoRemoveModifiedSignForKnownScriptableObjects = chkAutoRemoveModIndWhenScriptCommited.Checked;
      _textEditorOptions.ResultViewType = (ResultViewType)cmbShowResultsAs.SelectedItem;
      _textEditorOptions.AnalyzeSql = chkAnalyzeSql.Checked;
			_textEditorOptions.ScriptPreviewSticked = chkScriptPreviewSticked.Checked;

      _textEditorOptions.CodeCompCacheTimeout = (int)nudCodeCompCacheTimeout.Value;
      _textEditorOptions.CodeCompCacheCollectInterval = (int)nudCodeCompCollectInterval.Value;
			_textEditorOptions.SearchAndReplaceItemCnt = (int)nudMaxSearchReplaceHistCnt.Value;


      if (_hasFontChanges )
      {
        if (_selectedFont != null)
          _textEditorOptions.CustomTextEditorFont = new FontOptions(_selectedFont);
        else
          _textEditorOptions.CustomTextEditorFont = null;
      }

			_textEditorOptions.EncodingCodePage = (int)cmbEncoding.SelectedValue;
			
      _isModified = false;
      return true;
    }

    public void ShowContent( )
    {
      this.Show();
    }

    public void HideContent( )
    {
      this.Hide();
    }

    #endregion

    #region Methods

    private void LoadInitial( )
    {
      _isInitializing = true;
      chkConvertTabsToSpaces.Checked = _textEditorOptions.ConvertTabsToSpaces;
      chkShowEOLMarkers.Checked = _textEditorOptions.ShowEOLMarkers;
      chkShowInvalidLines.Checked = _textEditorOptions.ShowInvalidLines;
      chkShowLineNumbers.Checked = _textEditorOptions.ShowLineNumbers;
      chkShowMatchingBracket.Checked = _textEditorOptions.ShowMatchingBracket;
      chkShowSpaces.Checked = _textEditorOptions.ShowSpaces;
			chkWatchOpenFiles.Checked = _textEditorOptions.WatchOpenedFiles;
			chkAllowCaretBeyondEOL.Checked = _textEditorOptions.AllowCaretBeyondEOL;
      chkShowTabs.Checked = _textEditorOptions.ShowTabs;

      cmbIndentStyle.SelectedIndex = _textEditorOptions.IndentStyleDef;
      numfTabIndent.Value = (decimal)_textEditorOptions.TabIndent;
      numVRulerRow.Value = (decimal)_textEditorOptions.VerticalRulerRow;
      chkParseSelects.Checked = _textEditorOptions.HighlightSelectAndCase;
      chkParseComments.Checked = _textEditorOptions.HighlightComments;
      
      chkShowCaseSelectFoldMarkers.Checked = _textEditorOptions.FoldHighlightedSelectAndCase;
      chkShowFoldMarkers.Checked = _textEditorOptions.ShowFoldMarkers;
      chkEnableCustomHighlighter.Checked = _textEditorOptions.CustomHighlightersEnabled;
      chkShowCommentFoldMarkers.Checked = _textEditorOptions.FoldComments;
			
      panColorCase.BackColor = _textEditorOptions.CaseHighlightColor;
      panColorSelect.BackColor = _textEditorOptions.SelectHighlightColor;
			panColorComment.BackColor = _textEditorOptions.CommentHighlightColor;
      
      chkMarkCaretLine.Checked = _textEditorOptions.MarkCaretLine;
      chkUseAntiAliasedFont.Checked = _textEditorOptions.UseAntiAliasedFont;
      chkFoldCodeBlocks.Checked = _textEditorOptions.FoldCodeBlocks;
      chkAutoRemoveModIndWhenScriptCommited.Checked = _textEditorOptions.AutoRemoveModifiedSignForKnownScriptableObjects;
      cmbShowResultsAs.SelectedItem = _textEditorOptions.ResultViewType;
      chkAnalyzeSql.Checked = _textEditorOptions.AnalyzeSql;
			chkScriptPreviewSticked.Checked = _textEditorOptions.ScriptPreviewSticked;

      nudCodeCompCacheTimeout.Value = _textEditorOptions.CodeCompCacheTimeout;
      nudCodeCompCollectInterval.Value = _textEditorOptions.CodeCompCacheCollectInterval;
			nudMaxSearchReplaceHistCnt.Value = _textEditorOptions.SearchAndReplaceItemCnt;

      if (_textEditorOptions.CustomTextEditorFont != null)
      {
        lblFontPreview.Font = _textEditorOptions.CustomTextEditorFont.CreateFont();
        lblFontPreview.Text = "Family: " + lblFontPreview.Font.Name + ", Size:" + lblFontPreview.Font.Size.ToString();
      }
      else
      {
        lblFontPreview.Font = new Font("Courier New", 10);
        lblFontPreview.Text = "Family: " + lblFontPreview.Font.Name + ", Size:" + lblFontPreview.Font.Size.ToString();
      }

			cmbEncoding.DataSource = Encoding.GetEncodings();
			cmbEncoding.DisplayMember = "DisplayName";
			cmbEncoding.ValueMember = "CodePage";

			cmbEncoding.SelectedValue = _textEditorOptions.EncodingCodePage;

			_isInitializing = false;
    }

    #endregion

    private void OnCheckedChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    private void cmbIndentStyle_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    private void numVRulerRow_ValueChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    private void panColorSelect_Click( object sender, EventArgs e )
    {
      if (colorDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      panColorSelect.BackColor = colorDialog1.Color;
      _isModified = true;
    }

    private void panColorCase_Click( object sender, EventArgs e )
    {
      if (colorDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      panColorCase.BackColor = colorDialog1.Color;
      _isModified = true;
    }

    private void button1_Click( object sender, EventArgs e )
    {
      if (fontDialog1.ShowDialog() != DialogResult.OK)
        return;
      
      _selectedFont = fontDialog1.Font;
      lblFontPreview.Font = fontDialog1.Font;
      lblFontPreview.Text = "Family: " + lblFontPreview.Font.Name + ", Size:" + lblFontPreview.Font.Size.ToString();
      
      _isModified = true;
      _hasFontChanges = true;
    }


    private void button2_Click_1( object sender, EventArgs e )
    {
      _selectedFont = null;
      
      _isModified = true;
      _hasFontChanges = true;

      TextEditorControl ctrl = new TextEditorControl();

      lblFontPreview.Font = new Font("Courier New", 10);
      lblFontPreview.Text = "Family: " + lblFontPreview.Font.Name + ", Size:" + lblFontPreview.Font.Size.ToString() ;
    }

		private void panColorComment_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			
			panColorComment.BackColor = colorDialog1.Color;
			_isModified = true;
		}

    private void nudCodeCompCacheTimeout_ValueChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

		private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_isInitializing)
			{
				return;
			}
			_isModified = true;
		}

		private void nudMaxSearchReplaceHistCnt_ValueChanged(object sender, EventArgs e)
		{
			if (_isInitializing)
			{
				return;
			}
			_isModified = true;
		}
  }
}
