using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public interface ITextEditor
  {
    Guid Uid { get; set; }
    
    string FileName { get; }

    string Caption { get;set;}
    
    /// <summary>
    /// Save script to file
    /// </summary>
    /// <param name="fileName"></param>
    bool SaveContentToFile( string fileName );
    
    /// <summary>
    /// Load script from file
    /// </summary>
    /// <param name="fileName"></param>
    bool LoadContentFromFile( string fileName );
    
   
    /// <summary>
    /// Insert content from specified position
    /// </summary>
    /// <param name="pos">Start position</param>
    /// <param name="content">Content string</param>
    void InsertContent( CaretPosition startPos, string content );
    
    /// <summary>
    /// Insert content where caret is currently positioned
    /// </summary>
    /// <param name="content">Content string</param>
    void InsertContent( string content );
    
    /// <summary>
    /// Append content
    /// </summary>
    /// <param name="content">Content string</param>
    void AppendContent( string content );
    
    /// <summary>
    /// Remove content in specified caret position boundary
    /// </summary>
    /// <param name="startPos">Start position</param>
    /// <param name="endPos">End position</param>
    void RemoveContent( CaretPosition startPos, CaretPosition endPos );

    /// <summary>
    /// Get content specified by caret position boundary
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    string GetContent( CaretPosition startPos, CaretPosition endPos );

		bool ReadOnly { get; set;}
    /// <summary>
    /// Content
    /// </summary>
    string Content { get;set;}

    /// <summary>
    /// Select content in specified caret position boundary
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    void Select( CaretPosition startPos, CaretPosition endPos );

		/// <summary>
		/// Select content starting from startOffset and ending 
		/// at endOffset
		/// </summary>
		/// <param name="startOffset"></param>
		/// <param name="endOffset"></param>
		void Select(int startOffset, int endOffset);
		
		/// <summary>
    /// Clear current selection
    /// </summary>
    void ClearSelection( );
    
    /// <summary>
    /// Delete selection
    /// </summary>
    void DeleteSelection( );

    /// <summary>
    /// Current caret position
    /// </summary>
    CaretPosition CaretPos{get;set;}
    
    /// <summary>
    /// Get word at cursor
    /// </summary>
    string WordAtCursor{get;}
    
    /// <summary>
    /// Selected text
    /// </summary>
    string SelectedText{get;}


    EditorContentType ContentType { get; }

		int NotificationStripItemCount { get;}
		
		void AddItemToNotificationStrip(ToolStripItem item, int index);
		void AddItemToNotificationStrip(ToolStripItem item);
		void RemoveItemFromNotificationStrip(ToolStripItem item);

		int AddInStripItemCount { get;}
		void AddItemToAddInStrip(ToolStripItem item, int index);
		void AddItemToAddInStrip(ToolStripItem item);
		void RemoveItemFromAddInStrip(ToolStripItem item);

    string CurrentSytaxMode { get; }
    void RefreshSyntaxHighlighter();

    void ShowInfo(string infoText);

		//string FileName { get; }

    #region  Event Definitions

    /// <summary>
    /// Fired after open file is performed by the user
    /// </summary>
    event AfterOpenedFileDelegate AfterOpenedFile;
    
    /// <summary>
    /// Fired before open file is performed. Gives chance to cancel
    /// default open file.
    /// </summary>
    event BeforeOpenedFileDelegate BeforeOpenedFile;

    /// <summary>
    /// Fired after save is performed by the user
    /// </summary>
    event AfterSavedContentDelegate AfterSavedContent;

    /// <summary>
    /// Fired before save is performed by the user.
    /// </summary>
    event BeforeSavedContentDelegate BeforeSavedContent;

    /// <summary>
    /// Fired after save as is performed by the user
    /// </summary>
    event AfterSavedContentToFileDelegate AfterSaveContentToFile;

    /// <summary>
    /// Fired before save as is performed by the user
    /// </summary>
    event BeforeSavedContentToFileDelegate BeforeSaveContentToFile;

    /// <summary>
    /// Fired after caret position changes.
    /// </summary>
    event AfterCaretPositionChangedDelegate AfterCaretPositionChanged;

    /// <summary>
    /// Fired before caret position changes.
    /// </summary>
    event BeforeCaretPositionChangedDelegate BeforeCaretPositionChanged;

    /// <summary>
    /// Fired after text changes
    /// </summary>
    event EventHandler AfterContentChanged;

    /// <summary>
    /// Fired after code completion list is showed.
    /// </summary>
    event AfterCodeCompletionShowedDelegete AfterCodeCompletionShowed;

    /// <summary>
    /// Fired before code completion list is showed.
    /// </summary>
    event BeforeCodeCompletionShowedDelegete BeforeCodeCompletionShowed;


		event FormClosingEventHandler EditorClosing;

		event FormClosedEventHandler EditorClosed;

    #endregion  //Event Definitions

		/// <summary>
		/// Provides access to text editors cursor property
		/// </summary>
		Cursor ActiveTextAreaCursor { get;set;}
		Cursor Cursor { get; set; }
  }

}
