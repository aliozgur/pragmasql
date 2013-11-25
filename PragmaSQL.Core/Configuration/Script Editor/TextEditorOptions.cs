/********************************************************************
  Class      : TextEditorOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;
using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  [Serializable]
  public class TextEditorOptions
  {
    #region Default Options
    public static bool defConvertTabsToSpaces = true;
    public static bool defShowEOLMarkers = false;
    public static bool defShowInvalidLines = false;
    public static bool defShowLineNumbers = true;
    public static bool defShowMatchingBracket = true;
    public static bool defShowSpaces = false;
    public static bool defShowTabs = false;
    public static int defIndentStyle = (int)IndentStyle.Smart;
    public static int defTabIndent = 2;
    public static int defVRulerRow = 120;
    public static bool defShowFoldMarkers = true;
    public static bool defMarkCaretLine = false;
    public static bool defUseAntiAliasedFont = true;
		public static bool defAllowCaretBeyondEOL = false;
    
    public static bool defFoldHighlightedSelectAndCase = false;
    public static bool defHighligtSelectAndCase = false;

		public static bool defHighligtComments = true;
		public static bool defFoldComments = true;
    
    public static Color defSelectHighlightColor = Color.LightGray;
    public static Color defCaseHighlightColor = Color.LightGray;
		
		public static Color defCommentHighlightColor = Color.LightGray;
  
    public static bool defAutoRemoveModifiedSignForKnownScriptableObjects = true;
    public static bool defFoldCodeBlocks = true;
    public static bool defAnalyzeSql = true;
    public static ResultViewType defResultViewType = ResultViewType.GridsInOwnTabs;
    public static FontOptions defCustomTextEditorFont = new FontOptions();

    public static readonly int defCodeCompCacheTimeout = 30;
    public static readonly int defCodeCompCacheCollectInterval = 30;



		public static int defEncodingCodePage
		{
			get
			{
				return Encoding.Default.CodePage;
			}
		}

		public static bool defScriptPreviewSticked = false;
		public static int defSearchAndReplaceItemCnt = 255;
		public static bool defWatchOpenedFiles = true;
    public static bool defCustomHighlightersEnabled = true;

		#endregion

    #region Fields and Properties

    public bool ConvertTabsToSpaces = defConvertTabsToSpaces;
    public bool ShowEOLMarkers = defShowEOLMarkers;
    public bool ShowInvalidLines = defShowInvalidLines;
    public bool ShowLineNumbers = defShowLineNumbers;
    public bool ShowMatchingBracket = defShowMatchingBracket;
    public bool ShowSpaces = defShowSpaces;
    public bool ShowTabs = defShowTabs;
    public int IndentStyleDef = defIndentStyle;
    public int TabIndent = defTabIndent;
    public int VerticalRulerRow = defVRulerRow;
    public bool ShowFoldMarkers = defShowFoldMarkers;
    public bool MarkCaretLine = defMarkCaretLine;
    public bool UseAntiAliasedFont = defUseAntiAliasedFont;
    
    public bool AutoRemoveModifiedSignForKnownScriptableObjects = defAutoRemoveModifiedSignForKnownScriptableObjects;
    public bool FoldCodeBlocks = defFoldCodeBlocks;
		public bool WatchOpenedFiles = defWatchOpenedFiles;
    public bool CustomHighlightersEnabled = true;

    public bool AnalyzeSql = defAnalyzeSql;
    public ResultViewType ResultViewType = defResultViewType;
    public FontOptions CustomTextEditorFont = null;
		public bool AllowCaretBeyondEOL = defAllowCaretBeyondEOL;

    public bool FoldHighlightedSelectAndCase = defFoldHighlightedSelectAndCase ;
    public bool FoldComments = defFoldComments;
    
    public bool HighlightSelectAndCase = defHighligtSelectAndCase;
		public bool HighlightComments = defHighligtComments;

    public int CodeCompCacheTimeout = defCodeCompCacheTimeout;
    public int CodeCompCacheCollectInterval = defCodeCompCacheCollectInterval;
		public int EncodingCodePage = defEncodingCodePage;
		public Encoding Encoding
		{
			get
			{
				return Encoding.GetEncoding(EncodingCodePage);
			}
		}

    private int _selectHighlightColor = defSelectHighlightColor.ToArgb();    
    public int SelectHighlightColorArgb
    {
      get { return _selectHighlightColor; }
      set { _selectHighlightColor = value; }
    }

    public Color SelectHighlightColor
    {
      get 
      { 
        return Color.FromArgb(SelectHighlightColorArgb); 
      }
    }

    private int _caseHighlightColor = defCaseHighlightColor.ToArgb();
    public int CaseHighlightColorArgb
    {
      get { return _caseHighlightColor; }
      set { _caseHighlightColor = value; }
    }

    public Color CaseHighlightColor
    {
      get { return Color.FromArgb(CaseHighlightColorArgb ); }
    }

		private int _commentHighlightColor = defCommentHighlightColor.ToArgb();
		public int CommentHighlightColorArgb
		{
			get { return _commentHighlightColor; }
			set { _commentHighlightColor = value; }
		}

		public Color CommentHighlightColor
		{
			get { return Color.FromArgb(CommentHighlightColorArgb); }
		}

		public bool ScriptPreviewSticked = defScriptPreviewSticked;
		public int SearchAndReplaceItemCnt = defSearchAndReplaceItemCnt;

    #endregion //Fields and Properties

    public TextEditorOptions()
    {
    }

    #region Methods
    public void ResetToDefaults()
    {
      ConvertTabsToSpaces = defConvertTabsToSpaces;
      ShowEOLMarkers = defShowEOLMarkers;
      ShowInvalidLines = defShowInvalidLines;
      ShowLineNumbers = defShowLineNumbers;
      ShowMatchingBracket = defShowMatchingBracket;
      ShowSpaces = defShowSpaces;
      ShowTabs = defShowTabs;
      IndentStyleDef = defIndentStyle;
      TabIndent = defTabIndent;
      VerticalRulerRow = defVRulerRow; 
      HighlightSelectAndCase = defHighligtSelectAndCase;
      HighlightComments = defHighligtComments;
      
      SelectHighlightColorArgb = defSelectHighlightColor.ToArgb();
      CaseHighlightColorArgb = defCaseHighlightColor.ToArgb();
      CommentHighlightColorArgb = defCommentHighlightColor.ToArgb();
      
      ShowFoldMarkers = defShowFoldMarkers;
      MarkCaretLine = defMarkCaretLine;
      UseAntiAliasedFont = defUseAntiAliasedFont;
      AutoRemoveModifiedSignForKnownScriptableObjects = defAutoRemoveModifiedSignForKnownScriptableObjects;
      FoldHighlightedSelectAndCase = defFoldHighlightedSelectAndCase;
      FoldComments = defFoldComments;
      
      FoldCodeBlocks = defFoldCodeBlocks;
			WatchOpenedFiles = defWatchOpenedFiles;
      CustomHighlightersEnabled = defCustomHighlightersEnabled;
      AllowCaretBeyondEOL = defAllowCaretBeyondEOL;
      
      AnalyzeSql = defAnalyzeSql;
      ResultViewType = defResultViewType;

      CodeCompCacheTimeout = defCodeCompCacheTimeout;
      CodeCompCacheCollectInterval = defCodeCompCacheCollectInterval;
			ScriptPreviewSticked = defScriptPreviewSticked;
			SearchAndReplaceItemCnt = defSearchAndReplaceItemCnt;
    }


    public void CopyFrom(TextEditorOptions source)
    {
      ConvertTabsToSpaces = source.ConvertTabsToSpaces;
      ShowEOLMarkers = source.ShowEOLMarkers;
      ShowInvalidLines = source.ShowInvalidLines;
      ShowLineNumbers = source.ShowLineNumbers;
      ShowMatchingBracket = source.ShowMatchingBracket;
      ShowSpaces = source.ShowSpaces;
      ShowTabs = source.ShowTabs;
      IndentStyleDef = source.IndentStyleDef;
      TabIndent = source.TabIndent;
      VerticalRulerRow = source.VerticalRulerRow; 
      HighlightSelectAndCase = source.HighlightSelectAndCase;   
      SelectHighlightColorArgb = source.SelectHighlightColorArgb;
      CaseHighlightColorArgb = source.CaseHighlightColorArgb;
			CommentHighlightColorArgb = source.CommentHighlightColorArgb;
			HighlightComments = source.HighlightComments;
     
      ShowFoldMarkers = source.ShowFoldMarkers;
      MarkCaretLine = source.MarkCaretLine;
      UseAntiAliasedFont = source.UseAntiAliasedFont;
      AutoRemoveModifiedSignForKnownScriptableObjects = source.AutoRemoveModifiedSignForKnownScriptableObjects;
      FoldHighlightedSelectAndCase = source.FoldHighlightedSelectAndCase;
      FoldComments = source.FoldComments;
      FoldCodeBlocks = source.FoldCodeBlocks;
      AllowCaretBeyondEOL = source.AllowCaretBeyondEOL;
      
      AnalyzeSql = source.AnalyzeSql;
      ResultViewType = source.ResultViewType;
      CustomTextEditorFont = source.CustomTextEditorFont;

      CodeCompCacheTimeout = source.CodeCompCacheTimeout;
      CodeCompCacheCollectInterval = source.CodeCompCacheCollectInterval;
      
    }

    public TextEditorOptions CreateCopy()
    {
      TextEditorOptions newOpts = new TextEditorOptions();
      newOpts.ConvertTabsToSpaces = this.ConvertTabsToSpaces;
      newOpts.ShowEOLMarkers = this.ShowEOLMarkers;
      newOpts.ShowInvalidLines = this.ShowInvalidLines;
      newOpts.ShowLineNumbers = this.ShowLineNumbers;
      newOpts.ShowMatchingBracket = this.ShowMatchingBracket;
      newOpts.ShowSpaces = this.ShowSpaces;
      newOpts.ShowTabs = this.ShowTabs;
      newOpts.IndentStyleDef = this.IndentStyleDef;
      newOpts.TabIndent = this.TabIndent;
      newOpts.VerticalRulerRow = this.VerticalRulerRow;          
      newOpts.HighlightSelectAndCase = this.HighlightSelectAndCase;
      newOpts.HighlightComments = this.HighlightComments;
      
      newOpts.SelectHighlightColorArgb = this.SelectHighlightColorArgb;
      newOpts.CaseHighlightColorArgb = this.CaseHighlightColorArgb;
			newOpts.CommentHighlightColorArgb = this.CommentHighlightColorArgb;
			
			newOpts.ShowFoldMarkers = this.ShowFoldMarkers;
      newOpts.MarkCaretLine = this.MarkCaretLine;
      newOpts.UseAntiAliasedFont = this.UseAntiAliasedFont;
      newOpts.AutoRemoveModifiedSignForKnownScriptableObjects = this.AutoRemoveModifiedSignForKnownScriptableObjects;
      newOpts.FoldHighlightedSelectAndCase = this.FoldHighlightedSelectAndCase;
      newOpts.FoldComments = this.FoldComments;
      
      newOpts.FoldCodeBlocks = this.FoldCodeBlocks;
      newOpts.AllowCaretBeyondEOL = this.AllowCaretBeyondEOL;
      
      newOpts.AnalyzeSql = this.AnalyzeSql;
      newOpts.ResultViewType = this.ResultViewType;
      newOpts.CustomTextEditorFont = this.CustomTextEditorFont;

      newOpts.CodeCompCacheTimeout = CodeCompCacheTimeout;
      newOpts.CodeCompCacheCollectInterval = CodeCompCacheCollectInterval;

      return newOpts;
    }

    public void ApplyToControl(TextEditorControl control)
    {
      if(control == null)
      {
        return;
      }

      control.ConvertTabsToSpaces = ConvertTabsToSpaces;
      control.ShowEOLMarkers = ShowEOLMarkers;
      control.ShowInvalidLines = ShowInvalidLines;
      control.ShowLineNumbers = ShowLineNumbers;
      control.ShowMatchingBracket = ShowMatchingBracket;
      control.ShowSpaces = ShowSpaces;
      control.ShowTabs = ShowTabs;
      control.IndentStyle = (IndentStyle)IndentStyleDef;
      control.TabIndent = TabIndent;
      control.VRulerRow = VerticalRulerRow;
      control.LineViewerStyle = MarkCaretLine ? LineViewerStyle.FullRow : LineViewerStyle.None;
      control.UseAntiAliasFont = UseAntiAliasedFont;
      control.EnableFolding = ShowFoldMarkers;
			control.AllowCaretBeyondEOL = AllowCaretBeyondEOL;
			
      if (CustomTextEditorFont == null)
      {
        control.TextEditorProperties.Font = defCustomTextEditorFont.CreateFont();
      }
      else
      {
        control.TextEditorProperties.Font = CustomTextEditorFont.CreateFont();
      }
    }
    

    #endregion //Methods

    #region Static Methods
    public static void ApplyDefaults(TextEditorControl control)
    {
      if(control == null)
      {
        return;
      }
      
      TextEditorOptions defaults = new TextEditorOptions();
      defaults.ApplyToControl(control);
    }


    #endregion
  }
}
